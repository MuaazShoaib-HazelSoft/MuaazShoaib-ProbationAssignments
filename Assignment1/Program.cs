using Microsoft.AspNetCore.Mvc;
using UserManagementSystem;
using UserManagementSystem.Models;
using UserManagementSystem.Services.UserService;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(MappingUserProfile));
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
var app = builder.Build();

// Middleware to check valid HTTP method.

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    response.ContentType = "application/json";
    if (response.StatusCode == StatusCodes.Status405MethodNotAllowed)
    {

        var errorResponse = new ApiResponse<object>
        (
             null,
            "Method Not Allowed. Please check the HTTP verb used for this endpoint.",
             false
        );
        await response.WriteAsJsonAsync(errorResponse);
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
