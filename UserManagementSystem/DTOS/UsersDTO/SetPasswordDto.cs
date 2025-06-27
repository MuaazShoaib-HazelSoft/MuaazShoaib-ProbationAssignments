using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.UsersDTO
{
    public class SetPasswordDto
    {

        [Required(ErrorMessage = MessagesConstants.EmailRequired)]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = MessagesConstants.EmailRequired)]
        public string oldPassword { get; set; } = "";

        [Required(ErrorMessage = MessagesConstants.PasswordRequired)]
        public string newPassword { get; set; } = "";
    }
}
