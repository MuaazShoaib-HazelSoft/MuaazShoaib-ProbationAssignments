using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Repositories.UserRepositories
{
    /// <summary>
    /// Interface of auth Repository
    /// containing all auth
    /// methods needs to be
    /// implemented.
    /// </summary>
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<string> GenerateEmailConfirmationToken(ApplicationUser user);
        Task<IdentityResult> ConfirmEmail(ApplicationUser user,string token);
        Task<bool> IsEmailConfirmed(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user,string oldPassword,string newPassword);
        Task<ApplicationUser> FindUserByIdAsync(string userId);
    }
}
