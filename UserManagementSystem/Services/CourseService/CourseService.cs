using AutoMapper;
using EllipticCurve;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models.CoursesModel;
using UserManagementSystem.Models.UserCourseModel;
using UserManagementSystem.Models.UserModel;
using UserManagementSystem.Repositories.GenericRepositories;
using UserManagementSystem.Repositories.UserCourseRepository;
using UserManagementSystem.Repositories.UserRepositories;

namespace UserManagementSystem.Services.CourseService
{
    /// <summary>
    /// Course Service containing all Courses methods.
    /// </summary>
    public class CourseService :ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public CourseService(IGenericRepository<Course> courseRepository,IMapper mapper,IUserRepository userRepository,IUserCourseRepository userCourseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userCourseRepository = userCourseRepository;
        }
        /// <summary>
        /// For adding course after checking all the validations.
        /// </summary>
        public async Task AddCourse(CourseDto addCourse)
        {
            bool courseExists = await _courseRepository.QueryAble().AnyAsync(c=> c.CourseName == addCourse.CourseName);
            if (courseExists) 
                throw new Exception(MessagesConstants.CourseAlreadyExists);

            var Course = _mapper.Map<Course>(addCourse);
            var courseAdded = await _courseRepository.AddAsync(Course);
            if (!courseAdded)
            {
                throw new Exception(MessagesConstants.CourseAddFailed);
            }
            await _courseRepository.SaveChangesAsync();
        }
        /// <summary>
        /// For Assigning Courses to Users.
        /// </summary>
        public async Task AssignCourseToUser(AssignUserCourseDto assignCourse)
        {
            var user = await _userRepository.GetByIdAsync(assignCourse.UserId);
            if (user == null)
                throw new Exception(MessagesConstants.UserNotFound);

            var course = await _courseRepository.QueryAble()
                .FirstOrDefaultAsync(c => c.Id == assignCourse.CourseId);

            if (course == null)
                throw new Exception(MessagesConstants.CourseNotFound);

            if (await _userCourseRepository.IsUserAssignedToCourseAsync(assignCourse))
                throw new Exception(MessagesConstants.CourseEnrolled);

            var assignedCourse = _mapper.Map<UserCourse>(assignCourse);
            if (!await _userCourseRepository.AddAsync(assignedCourse))
                throw new Exception(MessagesConstants.CourseAssignFailed);

            await _userCourseRepository.SaveChangesAsync();
        }
        /// <summary>
        /// For Deleting the Course on the base of Id.
        /// </summary>
        public async Task DeleteCourse(int? Id)
        {
            var existingcourse = await _courseRepository.GetByIdAsync(Id.Value);
            if (existingcourse == null)
                throw new Exception(MessagesConstants.CourseNotFound);

            var courseDeleted = await _courseRepository.DeleteAsync(existingcourse);
            if(!courseDeleted)
            throw new NotImplementedException(MessagesConstants.CourseDeletionFailed);

            await _courseRepository.SaveChangesAsync();
        }
        /// <summary>
        /// For getting all the courses with its associated users.
        /// </summary>
        public async Task<List<GetCoursesDto>> GetAllCourses()
        {
            var courses = (await _courseRepository.GetAllAsync()).ToList();
            if (courses.Count == 0)
                throw new NotImplementedException(MessagesConstants.NoCourseFound);

            var courseWithUsers = new List<GetCoursesDto>();

            foreach (var course in courses)
            {
                var courseDto = _mapper.Map<GetCoursesDto>(course);
                var users = await _userCourseRepository.GetUsersForCourseAsync(course.Id);
                courseDto.Users = users;
                courseWithUsers.Add(courseDto);
            }
            return courseWithUsers;
        }
        /// <summary>
        /// For updating the course name on the base of Id.
        /// </summary>
        public async Task UpdateCourse(int? Id, CourseDto updatedCourse)
        {
            bool courseExists = await _courseRepository.QueryAble().AnyAsync(c => c.CourseName == updatedCourse.CourseName && c.Id != Id.Value);
            if (courseExists)
                throw new Exception(MessagesConstants.CourseAlreadyExists);

            var existingCourse = await _courseRepository.GetByIdAsync(Id.Value);
            if (existingCourse == null)
                throw new Exception(MessagesConstants.CourseNotFound);

            _mapper.Map(updatedCourse, existingCourse);
            var courseUpdated = await _courseRepository.UpdateAsync(existingCourse);
            if (!courseUpdated)
                throw new Exception(MessagesConstants.CourseUpdationFailed);

            await _courseRepository.SaveChangesAsync();
        }
    }
}
