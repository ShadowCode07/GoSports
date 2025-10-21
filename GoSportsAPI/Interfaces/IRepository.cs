namespace GoSportsAPI.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(Guid id, T entity);
        Task<T?> DeleteAsync(Guid id);
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
    }
}
