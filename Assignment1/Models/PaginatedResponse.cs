namespace UserManagementSystem.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }
}
