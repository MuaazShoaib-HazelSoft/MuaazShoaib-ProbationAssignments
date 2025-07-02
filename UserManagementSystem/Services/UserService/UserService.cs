using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Models.ResponseModel;
using UserManagementSystem.Repositories.GenericRepositories;
using UserManagementSystem.Repositories.RolesRepositories;
using UserManagementSystem.Repositories.UserRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagementSystem.Services.UserService
{
    /// <summary>
    /// Implementation of All
    /// Users Methods.
    /// </summary>
    public class UserService : IUserService
    {  
        private readonly IMapper _userMapper;
       
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper,IRoleRepository roleRepository,IUserRepository userRepository)
        {
            _userMapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        // Delete a user by name and email.
        public async Task DeleteUser(string Id)
        { 
            var existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);
            
            var deleted = await _userRepository.DeleteAsync(existingUser);
            if (!deleted)
               throw new Exception(MessagesConstants.DeletionFailed);
            
             await _userRepository.SaveChangesAsync();
        }
        // Get all users.
        public async Task<List<GetUsersWithRolesDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            if(users == null)
                throw new Exception(MessagesConstants.NoUsers);
            var userWithRoles = new List<GetUsersWithRolesDto>();

            foreach (var user in users)
            {
                var dto = _userMapper.Map<GetUsersWithRolesDto>(user);
                var roles = await _roleRepository.GetUserRolesAsync(user);
                dto.Roles = roles.ToList();
                userWithRoles.Add(dto);
            }
            return userWithRoles;
        }
        // Get a single user by ID.
        public async Task<GetUsersWithRolesDto> GetUserById(string Id)
        {
            var existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);
            var roles = await _roleRepository.GetUserRolesAsync(existingUser);
            var userDto = _userMapper.Map<GetUsersWithRolesDto>(existingUser);
            userDto.Roles = roles.ToList();
            return userDto;
        }
        // Update an existing user by ID.
        public async Task UpdateUser(string Id, UpdateUserDto updatedUser)
        {
            var existingUser = await    _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            _userMapper.Map(updatedUser, existingUser);
            var updated = await _userRepository.UpdateAsync(existingUser);
            if (!updated)
                throw new Exception(MessagesConstants.UpdationFailed);

            await _userRepository.SaveChangesAsync();
        }
        // Get Paged Users by sending query model.
        public async Task<PaginatedResponse<GetUsersWithRolesDto>> GetPagedUsers(PaginationQueryModel usersViewModel)
        {
            if (usersViewModel.PageSize <= 0 || usersViewModel.PageNumber <= 0)
                throw new Exception(MessagesConstants.InvalidPages);

            var userQuery = _userRepository.QueryAble(); 
            var paginatedResponse = await _userRepository.GetPagedDataAsync(usersViewModel, userQuery);

            if (!paginatedResponse.Items.Any())
                throw new Exception(MessagesConstants.UnmatchedCriteria);

            var userWithRolesList = new List<GetUsersWithRolesDto>();

            foreach (var user in paginatedResponse.Items)
            {
                var dto = _userMapper.Map<GetUsersWithRolesDto>(user);
                var roles = await _roleRepository.GetUserRolesAsync(user);
                dto.Roles = roles.ToList();
                userWithRolesList.Add(dto);
            }
            return new PaginatedResponse<GetUsersWithRolesDto>(
                paginatedResponse.TotalPages,
                paginatedResponse.CurrentPage,
                paginatedResponse.PageSize,
                userWithRolesList
            );
        }
        // Assign Role Name to User by User Id.
        public async Task AssignRoleToUser(string userId,string roleName)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            var result =  await _roleRepository.AssignRolesToUser(existingUser, roleName);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
