using System.Text.RegularExpressions;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using AutoMapper;

namespace UserManagementSystem.Services.UserService
{
    /// <summary>
    /// Implementation of All
    /// Users Methods.
    /// </summary>
    public class UserService : IUserService
    {
        
        private static readonly List<User> s_usersList = new List<User>();

        private readonly IMapper _userMapper;

        public UserService(IMapper mapper)
        {
            _userMapper = mapper;
        }

        // Add new user.
        public string AddUser(AddUserDto newUser)
        {
            if (s_usersList.Any(u => u.Email == newUser.Email))
            {
                return MessagesConstants.EmailAlreadyExists; 
            }

            var addUser = _userMapper.Map<User>(newUser);
            addUser.Id = s_usersList.Any() ? s_usersList.Max(u => u.Id) + 1 : 1;

            s_usersList.Add(addUser);

            return MessagesConstants.UserAdded; 
        }

        // Delete a user by name and email.
        public string DeleteUser(int Id)
        {
            var existingUser = s_usersList.FirstOrDefault(u =>
               u.Id == Id);

            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }

            s_usersList.Remove(existingUser);

            return MessagesConstants.UserDeleted;
        }

        // Get all users.
        public List<GetUsersDto> GetAllUsers()
        {
          
            return _userMapper.Map<List<GetUsersDto>>(s_usersList);
        }

        // Get a single user by ID.
        public GetUsersDto? GetUserById(int id)
        {

            var user = s_usersList.FirstOrDefault(u => u.Id == id);

            return user == null ? null : _userMapper.Map<GetUsersDto>(user);
        }

        // Update an existing user by ID.
        public string UpdateUser(int id, UpdateUserDto updatedUser)
        {
            var existingUser = s_usersList.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return MessagesConstants.UserNotFound;
            }

            // Prevent updating to an email that another user already has.
            bool emailExists = s_usersList.Any(u => u.Email == updatedUser.Email && u.Id != id);

            if (emailExists)
            {
                return MessagesConstants.EmailAlreadyExists; 
            }

            // Map updated fields onto existing user.
            _userMapper.Map(updatedUser, existingUser);

            return MessagesConstants.UserUpdated;
        }
    }
}
