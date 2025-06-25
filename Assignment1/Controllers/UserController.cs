using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem;
using UserManagementSystem.Controllers;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Services.UserService;


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
        private readonly IMapper _mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary
        [HttpGet("getallusers")]

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<GetUsersDto> users = await _userService.GetAllUsers();
                return Ok(users, MessagesConstants.UsersFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        ///
        [HttpGet("getuserbyid/{Id?}")]
        public async Task<IActionResult> GetUserById([FromRoute] string? Id)
        {
            try
            { 
                GetUsersDto user = await _userService.GetUserById(Id);
                return Ok(user, MessagesConstants.UserFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }
        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        [HttpPut("updateuserdetails/{Id?}")]
        public async Task<IActionResult> UpdateUserDetails([FromRoute]  string? Id, RegisterUserDto userToUpdate)
        {
            try
            { 
                var updatedUser = await _userService.UpdateUser(Id, userToUpdate);
                var updatedDto = _mapper.Map<GetUsersDto>(updatedUser);
                return Ok(updatedDto, MessagesConstants.UserUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }

        /// <summary>
        /// Deletes a user by their name and email.
        /// </summary>
        /// 
        [HttpDelete("deleteuser/{Id?}")]
        public async Task<IActionResult> DeleteUser([FromRoute]  string? Id)
        {
            try
            {
                 await _userService.DeleteUser(Id);
                 return Ok(null, MessagesConstants.UserDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }
        [HttpGet("getpagedusers")]
        public async Task<IActionResult> GetPagedUsers([FromQuery] PaginationQueryModel usersViewModel)
        {
            try
            {
                var paginatedResponse = await _userService.GetPagedUsers(usersViewModel);
                
                return Ok(paginatedResponse, MessagesConstants.UserFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }

    }
}
