﻿using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Repositories.UserRepositories
{
    /// <summary>
    /// Auth Repository to
    /// containing all auth
    /// methods.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // To change the users old password to new password.
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user,string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        // To check the users correct password.
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        // To confirm the email of user with the required token.
        public async Task<IdentityResult> ConfirmEmail(ApplicationUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
        // To add the user in database.
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        // To find the user by the email.
        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        // To generate the token for email confirmation.
        public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        // To check that user's email is confirmed or not.
        public async Task<bool> IsEmailConfirmed(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }
    }
}
