namespace UserManagementSystem.DTOS.UsersDTO
{
    /// <summary>
    /// Class of Get User Dto
    /// excluding Password.
    /// </summary>
    public class GetUsersDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public string Email { get; set; } = "";
    }
}
