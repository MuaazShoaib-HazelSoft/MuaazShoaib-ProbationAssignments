using Microsoft.AspNetCore.Identity;

namespace UserManagementSystem.Models.UserModel
{
    /// Schema of ApplicationRole
    /// so that it contain many users.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
