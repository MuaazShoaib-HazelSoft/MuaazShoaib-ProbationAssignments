using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.CoursesDTO
{
    /// <summary>
    /// Dto for Adding,Updating
    /// Courses.
    /// </summary>
    public class CourseDto
    {
        [Required (ErrorMessage =MessagesConstants.CourseNameRequired)]
        public string CourseName { get; set; }
    }
}
