using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    public class BaseApiController:ControllerBase
    {
        protected IActionResult Ok(object data, string message,bool success)
        {
            return Ok(new ApiResponse<object>(data, message, true));
        }
        protected IActionResult BadRequest(object data, string message,bool success)
        {
            return BadRequest(new ApiResponse<object>(data, message, false));
        }
    }
}
