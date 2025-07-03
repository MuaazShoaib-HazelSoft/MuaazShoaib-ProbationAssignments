using UserManagementSystem.Models.UserCourseModel;

namespace UserManagementSystem.Models.CoursesModel
{
    /// <summary>
    /// Course Model
    /// including User Courses List.
    /// </summary>
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public List<UserCourse> UserCourses { get; set; }
    }
}
