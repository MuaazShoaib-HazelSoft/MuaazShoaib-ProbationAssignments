﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementSystem.Models;
using UserManagementSystem.Models.ResponseModel;


namespace UserManagementSystem.Filters
{
    /// <summary>
    /// Class of Request Validations
    /// related to model states.
    /// </summary>
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
