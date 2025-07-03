using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.CoursesDTO
{
    /// <summary>
    /// Dto for Assign Courses to
    /// Users with Id.
    /// </summary>
    public class AssignUserCourseDto
    {
        [Required (ErrorMessage =MessagesConstants.CourseIdRequired)]
        public int? CourseId { get; set; }

        [Required(ErrorMessage = MessagesConstants.UserIdRequired)]
        public string UserId { get; set; }
    }
}
