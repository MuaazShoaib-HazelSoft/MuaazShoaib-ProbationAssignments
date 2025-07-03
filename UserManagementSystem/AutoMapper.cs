using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models.CoursesModel;
using UserManagementSystem.Models.UserCourseModel;
using UserManagementSystem.Models.UserModel;

namespace UserManagementSystem
{
    /// <summary>
    /// Class for Mapping User Profile
    /// to map the classes.
    /// </summary>
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ApplicationUser, GetUsersWithRolesAndCourses>();
            CreateMap<ApplicationUser, GetUsersDto>();
            CreateMap<RegisterUserDto, ApplicationUser>();
            CreateMap<UpdateUserDto, ApplicationUser>();
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Course, GetCoursesDto>();
            CreateMap<AssignUserCourseDto, UserCourse>()
             .ForMember(dest => dest.User, opt => opt.Ignore())     
              .ForMember(dest => dest.Course, opt => opt.Ignore());  
        }
    }
}
