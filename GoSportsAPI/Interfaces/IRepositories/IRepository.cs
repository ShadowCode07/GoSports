namespace GoSportsAPI.Interfaces.IRepositories
{
    /// <summary>
    /// Defines the contract for basic asynchronous CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type handled by the repository.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the created entity.
        /// </returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the updated entity if found; otherwise, <c>null</c>.
        /// </returns>
        Task<T?> UpdateAsync(Guid id, T entity);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the deleted entity if found; otherwise, <c>null</c>.
        /// </returns>
        Task<T?> DeleteAsync(Guid id);

        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the entity if found; otherwise, <c>null</c>.
        /// </returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, containing a list of all entities.
        /// </returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Checks whether an entity with the specified identifier exists asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to check.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing <c>true</c> if the entity exists; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> Exists(Guid id);

        /// <summary>
        /// Saves all pending changes asynchronously.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, containing <c>true</c> if changes were saved successfully; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> SaveChanges();
    }
}
