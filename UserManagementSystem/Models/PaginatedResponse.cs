namespace UserManagementSystem.Models
{
    /// <summary>
    /// Paginaton Response class to
    /// handle paginated responses.
    /// </summary>
    public class PaginatedResponse<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PaginatedResponse(int totalPages,int currentPage,int pageSize, IEnumerable<T> items)
        {
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Items = items;
        }

    }
}
