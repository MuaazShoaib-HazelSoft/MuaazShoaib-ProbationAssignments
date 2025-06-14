using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    public class BaseApiController:ControllerBase
    {
        protected IActionResult Ok<T>(T data, string message,bool success)
        {
            return Ok(new ApiResponse<T>(data, message, true));
        }
        protected IActionResult BadRequest<T>(T data, string message,bool success)
        {
            return BadRequest(new ApiResponse<T>(data, message, false));
        }
    }
}
