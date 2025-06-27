using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    [ApiController]

    /// <summary>
    /// Base Api Controller for
    /// Ok and Bad Request methods.
    /// </summary>
    public class BaseApiController:ControllerBase
    {
        protected IActionResult Ok(object data, string message,bool success)
        {
            return Ok(new ApiResponse<object>(data, message, success));
        }

        protected IActionResult BadRequest(object data, string message,bool success)
        {
            return BadRequest(new ApiResponse<object>(data, message, success));
        }
    }
}
