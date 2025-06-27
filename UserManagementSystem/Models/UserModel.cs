using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Models
{
    /// <summary>
    /// Schema of User Class
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        public int Age { get; set; } = 0;

    }
}
