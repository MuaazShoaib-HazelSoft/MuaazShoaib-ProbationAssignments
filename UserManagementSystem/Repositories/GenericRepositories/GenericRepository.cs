
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;
using UserManagementSystem.Models.ResponseModel;

namespace UserManagementSystem.Repositories.GenericRepositories
{
    /// <summary>
    /// All the generic CRUD ,
    /// paginaton methods included
    /// here.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        // Generic Method to add the data in table.
        public async Task<bool> AddAsync(T model)
        {
            try
            {
                await _entity.AddAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // To delete the data from table.
        public async Task<bool> DeleteAsync(T model)
        {
            try
            {
                _entity.Remove(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // To get all data from table.
        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return  await _entity.ToListAsync();
        }
        // To get the data with the help of Id.
        public async Task<T> GetByIdAsync(object Id)
        {
            return await _entity.FindAsync(Id);
        }
        // To get paged data with searching and sorting feature.
        public async Task<PaginatedResponse<T>> GetPagedDataAsync(PaginationQueryModel paginationQueryModel, IQueryable<T>? sourceQuery = null)
        {
            var query = sourceQuery ?? QueryAble();
            if (!string.IsNullOrEmpty(paginationQueryModel.SortColoumn))
             query = query.OrderBy(paginationQueryModel.SortColoumn);

            if (!string.IsNullOrEmpty(paginationQueryModel.Search))
                query = query.Where(paginationQueryModel.Search);

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / paginationQueryModel.PageSize);
            int skip = (paginationQueryModel.PageNumber - 1) * paginationQueryModel.PageSize;
            var items = await query.Skip(skip).Take(paginationQueryModel.PageSize).ToListAsync();
            return new PaginatedResponse<T>(totalPages,paginationQueryModel.PageNumber,paginationQueryModel.PageSize,items);
        }
        // To query the data from database.
        public virtual IQueryable<T> QueryAble()
        {
            return _entity.AsQueryable();
        }
        // To save the data in database.
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        // To update the data from database.
        public async Task<bool> UpdateAsync(T model)
        {
            try
            {
                _entity.Update(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
