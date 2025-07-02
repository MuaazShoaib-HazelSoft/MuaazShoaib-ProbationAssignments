using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;
using System.Security.Claims;
using UserManagementSystem.Models.ResponseModel;
namespace UserManagementSystem.Controllers
{
    [ApiController]

    /// <summary>
    /// Base Api Controller for
    /// Ok and Bad Request methods.
    /// </summary>
    public class BaseApiController:ControllerBase
    {
        protected IActionResult Ok(string message, bool success)
        {
            return Ok(new ApiResponse<object>(message, success));
        }

        protected IActionResult Ok(object data, string message, bool success)
        {
            return Ok(new ApiResponse<object>(data, message, success));
        }

        protected IActionResult BadRequest(string message, bool success)
        {
            return BadRequest(new ApiResponse<object>(message, success));
        }
        protected string? GetUserId()
        {
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
