using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem.Data
{
    /// <summary>
    /// All the auth interfaces that has to be implemented.
    /// </summary>
    public interface IAuthService
    {
        Task  RegisterUser(RegisterUserDto newUser);
        Task<string> LoginUser(LoginUserDto loginUser);
        Task ConfirmEmail(string email,string token);
        Task SetNewPassword(SetPasswordDto setPasswordDto);
    }
}
