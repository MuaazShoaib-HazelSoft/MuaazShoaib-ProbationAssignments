
using Azure;
using UserManagementSystem.Models;

namespace UserManagementSystem
{
    public class ContentTypeValidationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (HttpMethods.IsPost(context.Request.Method) ||
           HttpMethods.IsPut(context.Request.Method) ||
           HttpMethods.IsPatch(context.Request.Method))
            {
                var hasContentType = context.Request.Headers.ContainsKey("Content-Type");

                // Check if body exists
                var hasBody = context.Request.ContentLength > 0;

                if (!hasContentType || !hasBody)
                {
                    context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                    var errorResponse = new ApiResponse<object>(
                     null,
                    MessagesConstants.InvalidContentType,
                    false
           );
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }

            await next(context); // Call the next middleware
        }
    }
}
