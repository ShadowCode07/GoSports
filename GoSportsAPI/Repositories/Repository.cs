using GoSportsAPI.Data;
using GoSportsAPI.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

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
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        /// <summary>Create entity of type T.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        /// <summary>Updates an entity of type T.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<T?> UpdateAsync(Guid id, T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        /// <summary>Deletes an entity of type T.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<T?> DeleteAsync(Guid id)
        {
            var model = await GetByIdAsync(id);

            if (model == null)
            {
                return null;
            }

            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }


        /// <summary>Gets all entities of type T.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        /// <summary>Gets an entity of type T by Identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }


        /// <summary>Saves changes to the context.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<bool> SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        /// <summary>Checks if entity exists by Identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual Task<bool> Exists(Guid id)
        {
            return _dbSet.AnyAsync();
        }
    }
}
