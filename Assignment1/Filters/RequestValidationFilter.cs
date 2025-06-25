using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementSystem.Models;


namespace UserManagementSystem.Filters
{
    public class RequestValidationFilter : ActionFilterAttribute
    {
     
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result =  new BadRequestObjectResult(new ApiResponse<object>
                (
                    errors,
                    "Validation failed",
                    false
                ));
            }
        }
    }
}
