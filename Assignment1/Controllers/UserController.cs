using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagementSystem;
using UserManagementSystem.Controllers;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Services.UserService;
using UserManagementSystem.Utils;

namespace UserManagement.Controllers
{
    /// <summary>
    /// Controller to handle all user-related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        private readonly IAuthService _authRepo;
        public UserController(IUserService userService, IAuthService authRepo)
        {
            _userService = userService;
            _authRepo = authRepo;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("getallusers")]

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<GetUsersDto> users = await _userService.GetAllUsers();
                if (!users.Any())
                {
                    return BadRequest(null, MessagesConstants.NoUsers, false);
                }
                return Ok(users, MessagesConstants.UsersFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        ///
        [HttpGet("getuserbyid")]
        public async Task<IActionResult> GetUserById()
        {
            try
            { 
                string Id = GetUserId();
                GetUsersDto user = await _userService.GetUserById(Id);
                if (user == null)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
                }
                return Ok(user, MessagesConstants.UserFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }
        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        [HttpPut("updateuserdetails")]
        public async Task<IActionResult> UpdateUserDetails( [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                string Id = GetUserId();
                var resultMessage = await _userService.UpdateUser(Id, updatedUser);
                var passwordUpdate = await _authRepo.UpdatePassword(Id,updatedUser.Password);

                if(passwordUpdate != MessagesConstants.PasswordUpdated) {

                    return BadRequest(null, passwordUpdate, false);
                }
                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest(null, MessagesConstants.EmailAlreadyExists, false);
                }
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
                }
                if (resultMessage == MessagesConstants.UpdationFailed)
                {
                    return BadRequest(null, MessagesConstants.UpdationFailed, false);
                }
                return Ok(updatedUser, MessagesConstants.UserUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

        /// <summary>
        /// Deletes a user by their name and email.
        /// </summary>
        /// 
        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                string Id = GetUserId();
                var resultMessage = await _userService.DeleteUser(Id);
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
                }
                if (resultMessage == MessagesConstants.DeletionFailed)
                {
                    return BadRequest(null, MessagesConstants.DeletionFailed, false);
                }
                return Ok(null, MessagesConstants.UserDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
      
        }
        public string GetUserId()
        {
            return User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
