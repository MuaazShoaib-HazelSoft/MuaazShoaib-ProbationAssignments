using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Repositories.GenericRepositories;
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
        private readonly IAuthRepository _authRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        public UserService(IMapper mapper,IGenericRepository<ApplicationUser> userRepository,IAuthRepository authRepository)
        {
            _userMapper = mapper;
            _userRepository = userRepository;
            _authRepository = authRepository;
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
        public async Task<List<GetUsersDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            if(users == null)
                throw new Exception(MessagesConstants.NoUsers);

            return  _userMapper.Map<List<GetUsersDto>>(users);
        }
        // Get a single user by ID.
        public async Task<GetUsersDto> GetUserById(string Id)
        {
            var existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            return  _userMapper.Map<GetUsersDto>(existingUser);
        }
        // Update an existing user by ID.
        public async Task UpdateUser(string Id, RegisterUserDto updatedUser)
        {
            var existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            _userMapper.Map(updatedUser, existingUser);
            var token = await _authRepository.GeneratePasswordResetTokenAsync(existingUser);
            var result = await _authRepository.ResetPasswordAsync(existingUser, token, updatedUser.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var updated = await _userRepository.UpdateAsync(existingUser);
            if (!updated)
                throw new Exception(MessagesConstants.UpdationFailed);

            await _userRepository.SaveChangesAsync();
        }
        // Get Paged Users by sending query model.
        public async Task<PaginatedResponse<GetUsersDto>> GetPagedUsers(PaginationQueryModel paginationQueryModel)
        {
            if (paginationQueryModel.PageSize <= 0 || paginationQueryModel.PageNumber <= 0)
                throw new Exception("PageSize or PageNumber must be a valid positive integer.");

            var paginatedResponse =  await _userRepository.GetPagedDataAsync(paginationQueryModel);
            if (!paginatedResponse.Items.Any())
                throw new Exception(MessagesConstants.UnmatchedCriteria);

            var usersList = _userMapper.Map<List<GetUsersDto>>(paginatedResponse.Items);
            return new PaginatedResponse<GetUsersDto>(paginatedResponse.TotalPages, paginatedResponse.CurrentPage, paginatedResponse.PageSize, usersList);
        }
    }
}
