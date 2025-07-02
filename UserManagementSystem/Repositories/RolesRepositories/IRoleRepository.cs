using Microsoft.AspNetCore.Identity;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Repositories.RolesRepositories
{
    /// <summary>
    /// Inteface containing
    /// all role repository
    /// methods.
    /// </summary>
    public interface IRoleRepository
    {
        Task<IdentityRole?> GetRoleByIdAsync(string id);
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        Task<IdentityRole?> GetRoleByNameAsync(string roleName);
        Task<IdentityResult> AssignRolesToUser(ApplicationUser user, string roleName);
    }
}
