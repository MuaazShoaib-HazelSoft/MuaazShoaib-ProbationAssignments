using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem.Data
{
    /// <summary>
    /// Auth Repository containing all auth methods..
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _userMapper;
        public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper userMapper)
        {
            _configuration = configuration;
            _userMapper = userMapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Register all the users.
        /// </summary>
        public async Task<string> RegisterUser(RegisterUserDto newUser)
        {
            var userExists = await _userManager.FindByEmailAsync(newUser.Email);
            if (userExists != null)
                return MessagesConstants.EmailAlreadyExists;
            ApplicationUser registerUser = _userMapper.Map<ApplicationUser>(newUser);
            var result =  await _userManager.CreateAsync(registerUser, newUser.Password);
            if (result.Succeeded)
            {
                return MessagesConstants.UserAdded;
            }
            string errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return $"Registration failed: {errors}";
        }

        /// <summary>
        /// Login through Email and Password.
        /// </summary>
        public async Task<string> LoginUser(LoginUserDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user == null)
                return MessagesConstants.UserNotFound;

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginUser.Password);
            if (!isValidPassword)
                return MessagesConstants.UnmatchedPasswords;

            return await CreateToken(user);
        }
        /// <summary>
        ///To create JWT Token.
        /// </summary>
        public async Task<string> CreateToken(ApplicationUser user)
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
        /// Method for Updating Password.
        /// </summary>
        public async Task<string> UpdatePassword(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return MessagesConstants.UserNotFound;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            string errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return result.Succeeded ? MessagesConstants.PasswordUpdated : errors;
        }
    }
}

