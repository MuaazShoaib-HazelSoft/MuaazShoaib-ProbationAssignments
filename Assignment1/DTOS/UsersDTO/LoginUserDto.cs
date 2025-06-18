using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.UsersDTO
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = MessagesConstants.EmailRequired)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = MessagesConstants.PasswordRequired)]
        public string Password { get; set; } = "";
    }
}
