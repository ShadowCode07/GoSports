using GoSportsAPI.Models;
using System.Linq.Expressions;

namespace GoSportsAPI.Interfaces.IRepositories
{
    /// <summary>
    /// Defines the contract for basic asynchronous CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type handled by the repository.</typeparam>
    public interface IRepository<T> 
        where T : Base
    {
        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> for advanced querying scenarios.
        /// </summary>
        IQueryable<T> Query();

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="asNoTracking">
        /// If true, the entity is returned with AsNoTracking for read-only operations.
        /// </param>
        Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <param name="asNoTracking">
        /// If true, entities are returned with AsNoTracking for read-only operations.
        /// </param>
        Task<IReadOnlyList<T>> GetAllAsync(bool asNoTracking = false);

        /// <summary>
        /// Marks an entity as modified.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Removes an entity from the set.
        /// </summary>
        void Remove(T entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Checks if an entity with the given identifier exists.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Checks if any entities match the given predicate.
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// Counts entities that match the given predicate.
        /// </summary>
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// Saves all pending changes asynchronously.
        /// Returns true if at least one change was saved.
        /// </summary>
        Task<bool> SaveChanges();
    }
}
