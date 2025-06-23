
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using UserManagementSystem.Data;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Models;

namespace UserManagementSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return  await _entity.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object Id)
        {
            return await _entity.FindAsync(Id);
        }
        public async Task<IEnumerable<T>> GetPagedDataAsync( PaginationQueryModel paginationQueryModel)
        {
            var query = QueryAble();
            if (!string.IsNullOrEmpty(paginationQueryModel.SortColoumn))  
                query = query.OrderBy(paginationQueryModel.SortColoumn);

            if (!string.IsNullOrEmpty(paginationQueryModel.SearchItem))
                query = query.Where(paginationQueryModel.SearchItem);

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / paginationQueryModel.PageSize);
            int skip = (paginationQueryModel.PageNumber - 1) * paginationQueryModel.PageSize;
            var result = await query.Skip(skip).Take(paginationQueryModel.PageSize).ToListAsync();        
            return result;
        }

        public IQueryable<T> QueryAble()
        {
            return _entity.AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

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
