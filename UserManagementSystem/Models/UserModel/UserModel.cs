using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UserManagementSystem.Models.UserCourseModel;

namespace UserManagementSystem.Models.UserModel
{
    /// <summary>
    /// Schema of User Class
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        public int Age { get; set; } = 0;
        public List<UserRole> UserRoles { get; set; }

        public List<UserCourse> UserCourses { get; set; }
    }
}
