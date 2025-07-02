using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Models.UserModel
{
    /// <summary>
    /// Schema of User Class
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        public int Age { get; set; } = 0;
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
