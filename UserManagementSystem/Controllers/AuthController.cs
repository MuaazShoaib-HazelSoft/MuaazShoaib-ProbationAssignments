using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;



namespace UserManagementSystem.Controllers
{
    /// <summary>
    /// Controller to handle all authentication
    /// operations.
    /// </summary>
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
        /// <summary>
        /// To register all users.
        /// </summary
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
        /// <summary>
        /// To login all users.
        /// </summary
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
        /// <summary>
        /// To confirm the email
        /// of users.
        /// </summary
        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                await _authService.ConfirmEmail(email, token);
                return Ok(null, MessagesConstants.EmailConfirmationSuccess,true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }
        /// <summary>
        /// To set the 
        /// new Password for user.
        /// </summary
        [HttpPost("set-new-password")]
        public async Task<IActionResult> SetNewPassword(SetPasswordDto setPasswordDto)
        {
            try
            {
                await _authService.SetNewPassword(setPasswordDto);
                return Ok(null, MessagesConstants.PasswordUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, ex.Message, false);
            }
        }

    }
}
