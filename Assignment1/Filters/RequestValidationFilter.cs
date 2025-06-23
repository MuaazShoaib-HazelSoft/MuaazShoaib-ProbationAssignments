using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementSystem.Utils;

namespace UserManagementSystem.Filters
{
    public class RequestValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = RequestValidator.ValidateRequest(context.ModelState);
            if (result != null)
            {
                context.Result = result;
            }
        }
    }
}
