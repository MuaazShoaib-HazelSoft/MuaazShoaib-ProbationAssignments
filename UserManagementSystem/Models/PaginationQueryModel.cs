namespace UserManagementSystem.Models
{
    /// <summary>
    /// Class of Pagination
    /// Query Model including
    /// all the pagination attributes.
    /// </summary>
    public class PaginationQueryModel
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string Search { get; set; } = "";
        public string SortColoumn { get; set; } = "";
    }
}
