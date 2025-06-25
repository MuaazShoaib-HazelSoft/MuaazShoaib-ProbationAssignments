namespace UserManagementSystem.Models
{
    public class PaginationQueryModel
    {
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; } = "";
        public string SortColoumn { get; set; } = "";
    }
}
