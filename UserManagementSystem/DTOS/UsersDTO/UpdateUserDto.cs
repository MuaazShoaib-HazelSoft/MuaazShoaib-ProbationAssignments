using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.UsersDTO
{
    /// Dto for updating user data
    /// </summary>
    public class UpdateUserDto
    {
        [Required(ErrorMessage = MessagesConstants.NameRequired)]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = MessagesConstants.EmailRequired)]
        [EmailAddress(ErrorMessage = MessagesConstants.InvalidEmail)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = MessagesConstants.AgeRequired)]
        public int? Age { get; set; }
    }
}
