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
        public IActionResult GetAllUsers()
        {
            // Validate ModelState if any query/body params exist
            try
            {
                List<GetUsersDto> users = _userService.GetAllUsers();
                if (!users.Any())
                {
                    return BadRequest<string>(null, MessagesConstants.NoUsers, false);
                }
                return Ok<List<GetUsersDto>>(users, MessagesConstants.UsersFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }

        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        [HttpGet("getuserbyid/{Id?}")]
        public IActionResult GetUserById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest<string>(null, MessagesConstants.InvalidId, false);
                }
                GetUsersDto user = _userService.GetUserById(Id);
                if (user == null)
                {
                    return BadRequest<string>(null, MessagesConstants.UserNotFound, false);
                }
                return Ok<GetUsersDto>(user, MessagesConstants.UserFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        [HttpPost("adduser")]
        public IActionResult AddUser([FromBody] AddUserDto newUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                var resultMessage = _userService.AddUser(newUser);

                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest<string>(null, MessagesConstants.EmailAlreadyExists, false);
                }

                return Ok<AddUserDto>(newUser, MessagesConstants.UserAdded, true);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        [HttpPut("updateuserdetails/{Id}")]
        public IActionResult UpdateUserDetails(int Id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                if (Id <= 0)
                {
                    return BadRequest<string>(null, MessagesConstants.InvalidId, false);
                }

                var resultMessage = _userService.UpdateUser(Id, updatedUser);
                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest<string>(null, MessagesConstants.EmailAlreadyExists, false);
                }
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest<string>(null, MessagesConstants.UserNotFound, false);
                }
                return Ok<UpdateUserDto>(updatedUser, MessagesConstants.UserUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

        /// <summary>
        /// Deletes a user by their name and email.
        /// </summary>
        [HttpDelete("deleteuser/{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest<string>(null, MessagesConstants.InvalidId, false);
                }
                var resultMessage = _userService.DeleteUser(Id);
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest<string>(null, MessagesConstants.UserNotFound, false);
                }

                return Ok<string>(null, MessagesConstants.UserDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
      
        }
    }
}
