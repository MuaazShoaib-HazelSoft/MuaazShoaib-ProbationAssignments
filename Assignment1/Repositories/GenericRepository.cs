
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;

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
        public async Task<bool> Add(T model)
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
        public async Task<bool> Delete(T model)
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
        public async Task<IEnumerable<T>> GetAll()
        {
           return  await _entity.ToListAsync();
        }

        public async Task<T> GetById(object Id)
        {
            return await _entity.FindAsync(Id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(T model)
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
