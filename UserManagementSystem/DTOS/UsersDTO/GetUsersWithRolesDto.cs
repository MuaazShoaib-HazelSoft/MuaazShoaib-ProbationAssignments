namespace UserManagementSystem.DTOS.UsersDTO
{
    /// <summary>
    /// DTO for getting 
    /// users data with roles .
    /// </summary>
    public class GetUsersWithRolesDto
    {
        public string Id { get; set; } = " ";
        public string UserName { get; set; } = "";
        public int Age { get; set; } = 0;
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new();
    }
}
