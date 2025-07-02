using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models.UserModel;

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
            CreateMap<ApplicationUser, GetUsersWithRolesDto>();
            CreateMap<RegisterUserDto, ApplicationUser>();
            CreateMap<UpdateUserDto, ApplicationUser>();
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
