namespace UserManagementSystem.Models
{
    public class PaginationQueryModel
    {
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
        public string SearchItem { get; set; } = "";
        public string SortColoumn { get; set; } = "";
    }
}
