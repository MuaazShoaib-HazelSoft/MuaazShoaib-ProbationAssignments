namespace UserManagementSystem.DTOS.UsersDTO
{
    /// <summary>
    /// Class of Get User Dto
    /// excluding Password.
    /// </summary>
    public class GetUsersDto
    {
        public string Id { get; set; } 
        public string UserName { get; set; } = "";
        public int Age { get; set; } = 0;
        public string Email { get; set; } = "";
    }
}
