using AutoMapper;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem
{
    public class MappingUserProfile:Profile
    {
        public MappingUserProfile()
        {
            CreateMap<User, GetUsersDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
