using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Utils;

namespace UserManagementSystem.Services.UserService
{
    /// <summary>
    /// Interface having all
    /// methods of Users.
    /// </summary>
    public interface IUserService
    {
        Task<List<GetUsersDto>> GetAllUsers();
        Task<GetUsersDto> GetUserById(string Id);
        Task<ApplicationUser>  UpdateUser(string Id, RegisterUserDto updatedUser);
        Task DeleteUser(string Id);
        Task<(List<GetUsersDto> Items, int TotalPages)> GetPagedUsers(PaginationQueryModel paginationQueryModel);

    }
}
