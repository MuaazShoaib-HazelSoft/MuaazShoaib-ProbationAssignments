using AutoMapper;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem
{
    public class MappingUserProfile:Profile
    {
        public MappingUserProfile()
        {
            CreateMap<ApplicationUser, GetUsersDto>();
            CreateMap<RegisterUserDto, ApplicationUser>();
        }
    }
}
