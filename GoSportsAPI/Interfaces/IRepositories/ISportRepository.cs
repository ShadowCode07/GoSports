using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Interfaces.IRepositories
{
    /// <summary>
    /// Defines repository operations specific to <see cref="Sport"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD operations from <see cref="IRepository{T}"/> 
    /// and may include additional methods for managing sports.
    /// </remarks>
    public interface ISportRepository : IRepository<Sport>
    {

        /// <summary>Gets all the sports.</summary>
        /// <param name="queryObject">The query object used for filtering.</param>
        /// <returns>List&lt;Sport&gt;</returns>
        Task<List<Sport>> GetAllAsync(SportQueryObject queryObject);

        /// <summary>
        /// Retrieves a sport by its exact name, or null if not found.
        /// </summary>
        Task<Sport?> GetByNameAsync(string name);
    }
}
