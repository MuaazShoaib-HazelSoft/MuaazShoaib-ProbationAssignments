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
        Task<GetUsersDto> GetUserById(int Id);
        Task<string> AddUser(AddUserDto newUser);
        Task<string> UpdateUser(int Id, UpdateUserDto updatedUser);
        Task<string>  DeleteUser(int Id);

    }
}
