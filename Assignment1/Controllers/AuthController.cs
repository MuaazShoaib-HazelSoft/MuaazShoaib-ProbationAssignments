using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Utils;

namespace UserManagementSystem.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController:BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto newUser)
        {
            try
            {
                 await _authService.RegisterUser(newUser);
                 return Ok(null, MessagesConstants.UserAdded, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }

        }
        [AllowAnonymous]
        [HttpPost("loginuser")]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            try
            {
                var token = await _authService.LoginUser(loginUserDto);
                return Ok(token, MessagesConstants.UserLogin, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }

    }

}
