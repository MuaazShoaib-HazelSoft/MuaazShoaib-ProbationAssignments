using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem.Services.UserService
{
    /// <summary>
    /// Implementation of All
    /// Users Methods.
    /// </summary>
    public class UserService : IUserService
    {
        
        //private static readonly List<User> s_usersList = new List<User>();

        private readonly IMapper _userMapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userMapper = mapper;
            _userManager = userManager;
        }
        // Delete a user by name and email.
        public async Task<string> DeleteUser(string Id)
        {
           
            ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }
            var result = await _userManager.DeleteAsync(existingUser);
            return result.Succeeded ? MessagesConstants.UserDeleted : MessagesConstants.DeletionFailed;
        }
        // Get all users.
        public async Task<List<GetUsersDto>> GetAllUsers()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            return  _userMapper.Map<List<GetUsersDto>>(users);
        }

        // Get a single user by ID.
        public async Task<GetUsersDto> GetUserById(string Id)
        {
            ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            return  _userMapper.Map<GetUsersDto>(existingUser);
        }

        // Update an existing user by ID.
        public async Task<string> UpdateUser(string Id, UpdateUserDto updatedUser)
        {
            ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }

            // Prevent updating to an email that another user already has.
            var userWithEmail = await _userManager.FindByEmailAsync(updatedUser.Email);
            if (userWithEmail != null && userWithEmail.Id != existingUser.Id)
                return MessagesConstants.EmailAlreadyExists;

            // Map updated fields onto existing user.
            _userMapper.Map(updatedUser, existingUser);
            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded ? MessagesConstants.UserUpdated : MessagesConstants.UpdationFailed;
        }
    }
}
