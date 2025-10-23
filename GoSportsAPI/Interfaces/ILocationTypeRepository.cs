using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Interfaces
{
    /// <summary>
    /// Defines repository operations specific to <see cref="LocationType"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD operations from <see cref="IRepository{T}"/> 
    /// and may include additional methods for managing location types.
    /// </remarks>
    public interface ILocationTypeRepository : IRepository<LocationType>
    {
        /// <summary>
        /// Gets all the location types.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting location types.</param>
        /// <returns>List&lt;LocationType&gt;</returns>
        Task<List<LocationType>> GetAllAsync(LocationTypeQueryObject queryObject);
    }
}
