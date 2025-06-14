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
        List<GetUsersDto> GetAllUsers();
        GetUsersDto GetUserById(int Id);
        string AddUser(AddUserDto newUser);
        string UpdateUser(int Id, UpdateUserDto updatedUser);
        string  DeleteUser(int Id);

    }
}
