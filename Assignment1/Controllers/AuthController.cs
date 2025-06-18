using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Utils;

namespace UserManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:BaseApiController
    {
        private readonly IAuthService _authRepo;

        public AuthController(IAuthService authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto newUser)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;

                var resultMessage = await _authRepo.RegisterUser(newUser);

                if (resultMessage == MessagesConstants.EmailAlreadyExists)
                {
                    return BadRequest(null, MessagesConstants.EmailAlreadyExists, false);
                }
                if(resultMessage != MessagesConstants.UserAdded)
                {
                    return BadRequest(null, resultMessage, false);
                }

                return Ok(newUser, resultMessage, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }

        }
        [HttpPost("loginuser")]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            try
            {
                var validationResult = RequestValidator.ValidateRequest(ModelState);
                if (validationResult != null) return validationResult;
                var resultMessage = await _authRepo.LoginUser(loginUserDto);
                if (resultMessage == MessagesConstants.UserNotFound)
                {
                    return BadRequest(null, MessagesConstants.UserNotFound, false);
                }
                if (resultMessage == MessagesConstants.UnmatchedPasswords)
                {
                    return BadRequest(null, MessagesConstants.UnmatchedPasswords, false);
                }

                return Ok(resultMessage, MessagesConstants.UserLogin, true);
            }
            catch (Exception ex)
            {
                return BadRequest(null, MessagesConstants.ErrorOccured + ex.Message, false);
            }
        }

    }

}
