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
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Repositories.UserRepositories;
using UserManagementSystem.Services;
using UserManagementSystem.Services.AuthenticationService;

namespace UserManagementSystem.Data
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
        /// Register all the users.
        /// </summary>
        public async Task RegisterUser(RegisterUserDto newUser)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(newUser.Email);
            if (existingUser != null)
                throw new Exception(MessagesConstants.EmailAlreadyExists);

            ApplicationUser registerUser = _userMapper.Map<ApplicationUser>(newUser);
            var result = await _authRepository.CreateUserAsync(registerUser, newUser.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            var token = await _authRepository.GenerateEmailConfirmationToken(registerUser);
            var confirmationLink = _linkGenerator.GetUriByAction(
                httpContext: _httpContextAccessor.HttpContext,
                action: "ConfirmEmail",                   // Action name (in controller)
                controller: "Auth",                       // Controller name without 'Controller'
                values: new { email = newUser.Email, token = token },
                scheme: _httpContextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email", $"Click <a href='{confirmationLink}'>here</a> to confirm your email.");
        }

        /// <summary>
        /// Login through Email and Password.
        /// </summary>
        public async Task<string> LoginUser(LoginUserDto loginUser)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(loginUser.Email);
            if (existingUser == null)
                throw new Exception(MessagesConstants.InvalidUser);


            if(!await _authRepository.CheckPasswordAsync(existingUser,loginUser.Password))
                throw new Exception(MessagesConstants.InvalidUser);

            if (!await _authRepository.isEmailConfirmed(existingUser))
                throw new Exception(MessagesConstants.EmailNotConfirmedYet);

            return await CreateToken(existingUser);
        }
        /// <summary>
        ///To create JWT Token.
        /// </summary>
        private async Task<string> CreateToken(ApplicationUser user)
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

        public async Task ConfirmEmail(string email, string token)
        {
            var existingUser = await _authRepository.FindUserByEmailAsync(email);
            if (existingUser == null)
                throw new Exception(MessagesConstants.InvalidUser);

            var confirmUser = _userMapper.Map<ApplicationUser>(existingUser);
            var result = await _authRepository.ConfirmEmail(confirmUser, token);
            if(!result.Succeeded)
                throw new Exception(MessagesConstants.FailedEmailConfirmation);
        }
    }
}

