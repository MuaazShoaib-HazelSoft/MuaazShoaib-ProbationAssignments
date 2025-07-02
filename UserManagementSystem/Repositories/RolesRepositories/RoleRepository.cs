using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserManagementSystem.Data;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Repositories.RolesRepositories
{
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// All the Methods related ,
        /// to roles are included
        /// here.
        /// </summary>
        private readonly RoleManager<IdentityRole> _roles;
        private readonly UserManager<ApplicationUser> _users;
        public RoleRepository(RoleManager<IdentityRole> roles,
                          UserManager<ApplicationUser> users)
        {
            _roles = roles;
            _users = users;
        }
        // Method for Adding/Creating Roles.
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roles.CreateAsync(new IdentityRole(roleName));
        }
        // Method for Deleting Roles.
        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
        {
            return await _roles.DeleteAsync(role);
        }
        // Method for Getting all the Roles.
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
           return await _roles.Roles.ToListAsync();
        }
        // Method for Getting the role by Id.
        public async Task<IdentityRole?> GetRoleByIdAsync(string id)
        {
            return await _roles.FindByIdAsync(id);
        }
        // Method for Getting the role by Name.
        public async Task<IdentityRole?> GetRoleByNameAsync(string roleName)
        {
            return await _roles.FindByNameAsync(roleName);
        }
        // Method for Getting the Users By Role Name.
        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
           return await _users.GetUsersInRoleAsync(roleName);
        }
        // Method for .Updating the Role Name
        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
            return await _roles.UpdateAsync(role);
        }
        // Method for Assigning Roles to Users.
        public async Task<IdentityResult> AssignRolesToUser(ApplicationUser user, string roleName)
        {
            return await _users.AddToRoleAsync(user, roleName);
        }
        // Method for Getting all the Roles of Users.
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _users.GetRolesAsync(user);
        }
    }
}
