using System.Linq.Expressions;
using UserManagementSystem.Models;

namespace UserManagementSystem.Repositories.GenericRepositories
{
    /// <summary>
    /// Inteface containing
    /// all generic repository
    /// methods.
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object Id);
        Task<bool> AddAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(T model);
        Task  SaveChangesAsync();
        IQueryable<T> QueryAble();
        Task<PaginatedResponse<T>> GetPagedDataAsync(PaginationQueryModel paginationQueryModel);

    }
}
