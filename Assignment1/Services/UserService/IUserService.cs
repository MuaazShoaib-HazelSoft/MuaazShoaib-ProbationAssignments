using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

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
        Task<string> UpdateUser(string Id, UpdateUserDto updatedUser);
        Task<string>  DeleteUser(string Id);

    }
}
