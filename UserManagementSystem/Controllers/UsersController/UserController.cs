using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem;
using UserManagementSystem.Controllers;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Services.UserService;


namespace UserManagementSystem.Controllers.UserController
{
    /// <summary>
    /// Controller to handle user related
    /// operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary
        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users, MessagesConstants.UsersFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        [HttpGet("getuserbyid/{Id?}")]
        public async Task<IActionResult> GetUserById([FromRoute] string Id)
        {
            try
            { 
                var user = await _userService.GetUserById(Id);
                return Ok(user, MessagesConstants.UserFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        [HttpPut("updateuserdetails/{Id?}")]
        public async Task<IActionResult> UpdateUserDetails([FromRoute]  string Id, UpdateUserDto userToUpdate)
        {
            try
            { 
                await _userService.UpdateUser(Id, userToUpdate);
                return Ok(MessagesConstants.UserUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// Deletes a user by their name and email.
        /// </summary>
        [HttpDelete("deleteuser/{Id?}")]
        public async Task<IActionResult> DeleteUser([FromRoute]  string Id)
        {
            try
            {
                 await _userService.DeleteUser(Id);
                 return Ok(MessagesConstants.UserDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// Assign role to user by Id.
        /// </summary>
        [HttpPost("assignrolebyid")]
        public async Task<IActionResult> AssignRolesToUser([FromQuery] string Id, [FromQuery] string roleName)
        {
            try
            {
                await _userService.AssignRoleToUser(Id,roleName);
                return Ok(MessagesConstants.RoleAssigned, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To get Paged users.
        /// </summary>
        [HttpGet("getpagedusers")]
        public async Task<IActionResult> GetPagedUsers([FromQuery] PaginationQueryModel usersViewModel)
        {
            try
            {
                var paginatedResponse = await _userService.GetPagedUsers(usersViewModel);
                
                return Ok(paginatedResponse, MessagesConstants.UsersFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }

    }
}
