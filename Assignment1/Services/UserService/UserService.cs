using System.Text.RegularExpressions;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using AutoMapper;
using UserManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

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
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _userMapper = mapper;
            _context = context;
        }

        // Add new user.
        public async Task<string> AddUser(AddUserDto newUser)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == newUser.Email);
            if (emailExists)
            {
                return MessagesConstants.EmailAlreadyExists;
            }
            User addUser = _userMapper.Map<User>(newUser);
            await _context.Users.AddAsync(addUser);
            await _context.SaveChangesAsync();
            return MessagesConstants.UserAdded; 
        }

        // Delete a user by name and email.
        public async Task<string> DeleteUser(int Id)
        {
            User existingUser = await _context.Users.FirstOrDefaultAsync(u=> u.Id == Id);
            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return MessagesConstants.UserDeleted;
        }

        // Get all users.
        public async Task<List<GetUsersDto>> GetAllUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return  _userMapper.Map<List<GetUsersDto>>(users);
        }

        // Get a single user by ID.
        public async Task<GetUsersDto> GetUserById(int Id)
        {
            User existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            return  _userMapper.Map<GetUsersDto>(existingUser);
        }

        // Update an existing user by ID.
        public async Task<string> UpdateUser(int Id, UpdateUserDto updatedUser)
        {
            User existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }
            // Prevent updating to an email that another user already has.
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == updatedUser.Email && u.Id != Id); 
            if (emailExists)
            {
                return MessagesConstants.EmailAlreadyExists; 
            }
            // Map updated fields onto existing user.
            _userMapper.Map(updatedUser, existingUser);
            await _context.SaveChangesAsync();
            return MessagesConstants.UserUpdated;
        }
    }
}
