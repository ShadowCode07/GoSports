using GoSportsAPI.Data;
using GoSportsAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;

namespace GoSportsAPI.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<T?> UpdateAsync(Guid id, T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T?> DeleteAsync(Guid id)
        {
            var model = await GetByIdAsync(id);
            
            if(model == null)
            {
                return null;
            }

            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public virtual Task<bool> Exists(Guid id)
        {
            return _context.locations.AnyAsync(x => x.Id == id);
        }

/*        public virtual IEnumerable<T> FindByFilter(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }*/
    }
}
