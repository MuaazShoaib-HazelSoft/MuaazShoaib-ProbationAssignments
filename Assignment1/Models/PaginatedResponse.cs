namespace UserManagementSystem.Models
{
    public class PaginatedResponse<T>
    {
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
}
