using Microsoft.AspNetCore.Mvc;
using UserManagementSystem;
using UserManagementSystem.Controllers;
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
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
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
        [HttpGet("getuserbyid/{Id?}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest(null, MessagesConstants.InvalidId, false);
                }
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
        /// Adds a new user.
        /// </summary>
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto newUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                var resultMessage = await _userService.AddUser(newUser);

                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest(null, MessagesConstants.EmailAlreadyExists, false);
                }

                return Ok(newUser, MessagesConstants.UserAdded, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        [HttpPut("updateuserdetails/{Id?}")]
        public async Task<IActionResult> UpdateUserDetails(int Id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                if (Id <= 0)
                {
                    return BadRequest(null, MessagesConstants.InvalidId, false);
                }

                var resultMessage = await _userService.UpdateUser(Id, updatedUser);
                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest(null, MessagesConstants.EmailAlreadyExists, false);
                }
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
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
        [HttpDelete("deleteuser/{Id?}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest(null, MessagesConstants.InvalidId, false);
                }
                var resultMessage = await _userService.DeleteUser(Id);
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
                }

                return Ok(null, MessagesConstants.UserDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
      
        }
    }
}
