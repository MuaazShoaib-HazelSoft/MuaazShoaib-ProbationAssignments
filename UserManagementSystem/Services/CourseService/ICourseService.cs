using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Models.ResponseModel;

namespace UserManagementSystem.Services.CourseService
{
    /// <summary>
    /// All the courses interfaces that has to be implemented.
    /// </summary>
    public interface ICourseService
    {
        Task<List<GetCoursesDto>> GetAllCourses();
        Task UpdateCourse(int? Id, CourseDto updatedCourse);
        Task DeleteCourse(int? Id);
        Task AddCourse(CourseDto addCourse);
        Task AssignCourseToUser(AssignUserCourseDto assignCourse);
    }
}
