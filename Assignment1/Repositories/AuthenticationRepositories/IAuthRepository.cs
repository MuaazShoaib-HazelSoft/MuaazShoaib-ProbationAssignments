using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Models;

namespace UserManagementSystem.Repositories.UserRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<string> GenerateEmailConfirmationToken(ApplicationUser user);
        Task<IdentityResult> ConfirmEmail(ApplicationUser user,string token);
        Task<bool> isEmailConfirmed(ApplicationUser user);
    }
}
