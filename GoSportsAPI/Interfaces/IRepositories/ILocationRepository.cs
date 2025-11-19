using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Interfaces.IRepositories
{
    /// <summary>
    /// Defines repository operations specific to <see cref="Location"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD operations from <see cref="IRepository{T}"/> 
    /// and may include additional methods for managing locations.
    /// </remarks>
    public interface ILocationRepository : IRepository<Location>
    {
        /// <summary>
        /// Returns all locations filtered and sorted using the provided query object.
        /// </summary>
        Task<List<Location>> GetAllAsync(LocationQueryObject queryObject);

        /// <summary>
        /// Retrieves a location with all required details.
        /// </summary>
        Task<Location?> GetWithDetailsAsync(Guid id);

    }
}
