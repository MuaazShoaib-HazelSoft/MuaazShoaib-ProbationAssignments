using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Repositories;

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
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        public UserService(IMapper mapper, UserManager<ApplicationUser> userManager,IGenericRepository<ApplicationUser> userRepository)
        {
            _userMapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        // Delete a user by name and email.
        public async Task<string> DeleteUser(string Id)
        {
            ApplicationUser existingUser = await _userRepository.GetById(Id);
            //ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }
            var deleted = await _userRepository.Delete(existingUser);
            if (!deleted)
            {
                return MessagesConstants.DeletionFailed;
            }
             await _userRepository.SaveChangesAsync();
            //var result = await _userManager.DeleteAsync(existingUser);
            return MessagesConstants.UserDeleted;
        }
        // Get all users.
        public async Task<List<GetUsersDto>> GetAllUsers()
        {
            //List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            var users = await _userRepository.GetAll();
            return  _userMapper.Map<List<GetUsersDto>>(users);
        }

        // Get a single user by ID.
        public async Task<GetUsersDto> GetUserById(string Id)
        {
            //ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            ApplicationUser existingUser = await _userRepository.GetById(Id);
            return  _userMapper.Map<GetUsersDto>(existingUser);
        }

        // Update an existing user by ID.
        public async Task<string> UpdateUser(string Id, UpdateUserDto updatedUser)
        {
            //ApplicationUser existingUser = await _userManager.FindByIdAsync(Id);
            ApplicationUser existingUser = await _userRepository.GetById(Id);
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
            //var result = await _userManager.UpdateAsync(existingUser);
            var updated = await _userRepository.Update(existingUser);
            if (!updated)
            {
                return MessagesConstants.UpdationFailed;
            }
            await _userRepository.SaveChangesAsync();
            return  MessagesConstants.UserUpdated;
        }
    }
}
