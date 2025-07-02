using Microsoft.AspNetCore.Identity;
using System.Data;

namespace UserManagementSystem.Models.UserModel
{
    /// <summary>
    /// Navigation of Many to
    /// Many Relationship
    /// </summary>
    public class UserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
