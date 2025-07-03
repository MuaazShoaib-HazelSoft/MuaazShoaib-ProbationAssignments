
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.Models.UserCourseModel;
using UserManagementSystem.Repositories.GenericRepositories;

namespace UserManagementSystem.Repositories.UserCourseRepository
{
    /// <summary>
    /// All the Methods related ,
    /// to user courses are included
    /// here.
    /// </summary>
    public class UserCourseRepository :GenericRepository<UserCourse>, IUserCourseRepository
    {
        private readonly DataContext _context;

        public UserCourseRepository(DataContext context) :base (context)
        {
            _context = context;
        }
        // Get all the courses of the specified user.
        public async Task<List<string>> GetCoursesForUserAsync(string userId)
        {
            return await _context.UserCourses
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.Course)                 
            .Select(uc => uc.Course.CourseName)
            .ToListAsync();
        }
        // Get all the users of the specified course.
        public async Task<List<string>> GetUsersForCourseAsync(int courseId)
        {
            return await _context.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Include(uc => uc.User)
                .Select(uc => uc.User.UserName)
                .ToListAsync();
        }
        // Check if the user is already assigned to the course or not.
        public async Task<bool> IsUserAssignedToCourseAsync(AssignUserCourseDto assignCourse)
        {
            return await _context.UserCourses
                .AnyAsync(uc => uc.UserId == assignCourse.UserId && uc.CourseId == assignCourse.CourseId);
        }
    }
}
