using GoSportsAPI.Data;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GoSportsAPI.Repositories
{
    /// <summary>
    /// Provides a generic base repository for performing common data access operations.
    /// </summary>
    /// <typeparam name="T">The entity type managed by the repository. Must be a reference type.</typeparam>
    /// <remarks>
    /// Implements the <see cref="IRepository{T}"/> interface and provides shared CRUD functionality 
    /// for derived repository classes.
    /// </remarks>
    public abstract class Repository<T> : IRepository<T> where T : Base
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <inheritdoc/>
        public virtual IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        /// <inheritdoc/>
        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbSet.AddAsync(entity);

            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
                
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <inheritdoc/>
        public virtual async Task<IReadOnlyList<T>> GetAllAsync(bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
                
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Update(entity);
        }

        /// <inheritdoc/>
        public virtual void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);

            return true;
        }

        /// <inheritdoc/>
        public virtual Task<bool> ExistsAsync(Guid id)
        {
            return _dbSet.AnyAsync(e => e.Id == id);
        }

        /// <inheritdoc/>
        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return query.AnyAsync();
        }

        /// <inheritdoc/>
        public virtual Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return query.CountAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}