namespace UserManagementSystem.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object Id);
        Task<bool> Add(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
        Task  SaveChangesAsync();
    }
}
