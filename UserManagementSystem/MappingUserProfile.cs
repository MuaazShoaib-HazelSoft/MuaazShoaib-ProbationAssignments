using AutoMapper;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem
{
    /// <summary>
    /// Class for Mapping User Profile
    /// to map the classes.
    /// </summary>
    public class MappingUserProfile:Profile
    {
        public MappingUserProfile()
        {
            CreateMap<ApplicationUser, GetUsersDto>();
            CreateMap<RegisterUserDto, ApplicationUser>();
        }
    }
}
