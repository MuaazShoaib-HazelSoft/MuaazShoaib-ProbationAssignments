using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.Models.UserCourseModel;
using UserManagementSystem.Repositories.GenericRepositories;

namespace UserManagementSystem.Repositories.UserCourseRepository
{
    /// <summary>
    /// Inteface containing
    /// all user course repository
    /// methods.
    /// </summary>
    public interface IUserCourseRepository: IGenericRepository<UserCourse>
    {
        Task<bool> IsUserAssignedToCourseAsync(AssignUserCourseDto assignCourse);
        Task<List<string>> GetUsersForCourseAsync(int courseId);
        Task<List<string>> GetCoursesForUserAsync(string userId);
    }
}
