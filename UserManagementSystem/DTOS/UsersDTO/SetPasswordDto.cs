using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.UsersDTO
{
    /// Class of Set Password Dto
    /// for updating the password.
    /// </summary>
    public class SetPasswordDto
    {
        public string OldPassword { get; set; } = "";

        [Required(ErrorMessage = MessagesConstants.PasswordRequired)]
        public string NewPassword { get; set; } = "";
    }
}
