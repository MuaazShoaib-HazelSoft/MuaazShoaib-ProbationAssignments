using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.Services.RoleService;

namespace UserManagementSystem.Controllers.RolesController
{
    /// <summary>
    /// Controller to handle all Role related
    /// operations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoleController:BaseApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// To add/create all roles.
        /// </summary
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto dto)
        {
            try
            {
                await _roleService.CreateRoleAsync(dto);
                return Ok(MessagesConstants.RoleAdded, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message,false);
            }
        }
        /// <summary>
        /// To get all roles.
        /// </summary
        [HttpGet("getallroles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                return Ok(roles, MessagesConstants.RoleFetched, true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
         /// To update the role
         /// with role Id.
         /// </summary
        [HttpPut("updaterole/{id?}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleDto dto)
        {
            try
            {
                await _roleService.UpdateRoleAsync(id, dto.Name);
                return Ok(MessagesConstants.RoleUpdated, true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To Delete the role with role 
        /// id.
        /// </summary
        [HttpDelete("deleterole/{id?}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return Ok(MessagesConstants.RoleDeleted, true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To get all users from the 
        /// specifed role name.
        /// </summary
        [HttpGet("getusersfromrolename")]
        public async Task<IActionResult> UsersInRole([FromQuery] string roleName)
        {
            try
            {
                var usersList = await _roleService.GetUsersInRoleAsync(roleName);
                return Ok(usersList, MessagesConstants.UsersFetched, true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message, false);
            }  
        }
    }
}
