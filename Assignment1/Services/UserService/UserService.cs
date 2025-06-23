using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Repositories;
using UserManagementSystem.Utils;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        public UserService(IMapper mapper, UserManager<ApplicationUser> userManager,IGenericRepository<ApplicationUser> userRepository)
        {
            _userMapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        // Delete a user by name and email.
        public async Task DeleteUser(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                throw new Exception(MessagesConstants.InvalidId);

            ApplicationUser existingUser = await _userRepository.GetByIdAsync(Id);
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
            if (string.IsNullOrEmpty(Id))
                throw new Exception(MessagesConstants.InvalidId);

            ApplicationUser existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            return  _userMapper.Map<GetUsersDto>(existingUser);
        }

        // Update an existing user by ID.
        public async Task<ApplicationUser> UpdateUser(string Id, RegisterUserDto updatedUser)
        {
            if(string.IsNullOrEmpty(Id))
                throw new Exception(MessagesConstants.InvalidId);

            var existingUser = await _userRepository.GetByIdAsync(Id);
            if (existingUser == null)
                throw new Exception(MessagesConstants.UserNotFound);

            var userWithEmail = await _userManager.FindByEmailAsync(updatedUser.Email);
            if (userWithEmail != null && userWithEmail.Id != existingUser.Id)
                throw new Exception(MessagesConstants.EmailAlreadyExists);

            _userMapper.Map(updatedUser, existingUser);

            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var result = await _userManager.ResetPasswordAsync(existingUser, token, updatedUser.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var updated = await _userRepository.UpdateAsync(existingUser);
            if (!updated)
                throw new Exception(MessagesConstants.UpdationFailed);

            await _userRepository.SaveChangesAsync();
            return existingUser;
        }
        public async Task<List<GetUsersDto>> GetPagedUsers(PaginationQueryModel usersViewModel)
        {
        
            //if (!string.IsNullOrEmpty(usersViewModel.SearchItem))

            //{
            //    var searchItem = usersViewModel.SearchItem;
            //    bool isNumeric = int.TryParse(searchItem, out int searchAge);
            //    usersQuery = usersQuery.Where(u => u.UserName.Contains(searchItem) || u.Email.Contains(searchItem) || (isNumeric && u.Age == searchAge));
            //}
            //if (!string.IsNullOrEmpty(usersViewModel.SortColoumn))
            //{
            //    if (usersViewModel.isDescending)
            //    {
            //        usersQuery = usersQuery.OrderByDescending(e => EF.Property<object>(e, usersViewModel.SortColoumn));
            //    }

            //    else
            //    {
            //       usersQuery = usersQuery.OrderBy(e => EF.Property<object>(e, usersViewModel.SortColoumn));
            //    }

            //}
            if (usersViewModel.PageNumber < 1)
                usersViewModel.PageNumber = 1;

            if (usersViewModel.PageSize < 1)
                usersViewModel.PageSize = 3;

            //var skipPages = (usersViewModel.PageNumber - 1) * usersViewModel.PageSize;
            //usersQuery = usersQuery.Skip(skipPages).Take(usersViewModel.PageSize);
            //var chList = await usersQuery.ToListAsync();

            //if (!chList.Any())
            //    throw new Exception(MessagesConstants.UnmatchedCriteria);

            //var list = _userMapper.Map<List<GetUsersDto>>(chList);
            //return list;

            var result = await _userRepository.GetPagedDataAsync(usersViewModel);
            if (!result.Any())
                throw new Exception(MessagesConstants.UnmatchedCriteria);
            var userslist =  _userMapper.Map<List<GetUsersDto>>(result);
            return userslist;
        }
    }
}
