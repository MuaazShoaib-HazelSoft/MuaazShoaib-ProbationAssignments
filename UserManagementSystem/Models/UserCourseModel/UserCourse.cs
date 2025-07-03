using UserManagementSystem.Models.CoursesModel;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem.Models.UserCourseModel
{
    /// <summary>
    /// User Courses Model
    /// for navigating many to many
    /// relationship.
    /// </summary>
    public class UserCourse
    {
        public Course Course { get; set; }
        public ApplicationUser User { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}
