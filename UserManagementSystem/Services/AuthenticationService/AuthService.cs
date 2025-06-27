using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Repositories.UserRepositories;

namespace UserManagementSystem.Services.AuthenticationService
{
    /// <summary>
    /// Auth Repository containing all auth methods..
    /// </summary>
    public class AuthService : IAuthService
    {
    
        private readonly IConfiguration _configuration;
        private readonly IMapper _userMapper;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public AuthService(IConfiguration configuration,IMapper userMapper,IAuthRepository authRepository,IEmailSender emailSender, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _configuration = configuration;
            _userMapper = userMapper;
            _authRepository = authRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }
        /// <summary>
        /// Register all the users and send email confirmation link to each user.
        /// </summary>
        public async Task RegisterUser(RegisterUserDto newUser)
        {
            
            var registerUser = _userMapper.Map<ApplicationUser>(newUser);
            var result = await _authRepository.CreateUserAsync(registerUser, newUser.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            var token = await _authRepository.GenerateEmailConfirmationToken(registerUser);
            var confirmationLink = _linkGenerator.GetUriByAction(
                httpContext: _httpContextAccessor.HttpContext,
                action: "ConfirmEmail",                   
                controller: "Auth",                   
                values: new { email = newUser.Email, token },
                scheme: _httpContextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(newUser.Email, MessagesConstants.EmailConfirm, $"Click <a href='{confirmationLink}'>here</a> to confirm your email. Your Password is :{newUser.Password}");

        }
        /// <summary>
        /// Login through Email and Password and check 
        /// email confirmation.
        /// </summary>
        public async Task<string> LoginUser(LoginUserDto loginUser)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(loginUser.Email);

            if(!await _authRepository.CheckPasswordAsync(existingUser,loginUser.Password))
                throw new Exception(MessagesConstants.InvalidUser);

            if (!await _authRepository.isEmailConfirmed(existingUser))
                throw new Exception(MessagesConstants.EmailNotConfirmedYet);

            return CreateToken(existingUser);
        }
        /// <summary>
        ///To create JWT Token.
        /// </summary>
        private string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
           {
           new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
           new Claim(ClaimTypes.Name, user.UserName)
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token =  tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// To confirm the email by providing
        /// the email and confirmaton token.
        /// </summary>
        public async Task ConfirmEmail(string email, string token)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(email);
            var result = await _authRepository.ConfirmEmail(existingUser, token);
            if(!result.Succeeded)
                throw new Exception(MessagesConstants.FailedEmailConfirmation);
        }
        /// <summary>
        /// To set the new password
        /// for users.
        /// </summary>
        public async Task SetNewPassword(SetPasswordDto setPasswordDto)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(setPasswordDto.Email);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);
           
            var result = await _authRepository.ChangePasswordAsync(existingUser, setPasswordDto.oldPassword, setPasswordDto.newPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

