using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Interfaces
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
        /// Creates a new location.
        /// </summary>
        /// <param name="location">The location entity to create.</param>
        /// <param name="sports">The list of sports associated with the location.</param>
        /// <returns>Location</returns>
        Task<Location> CreateAsync(Location location, List<string> sports);

        /// <summary>
        /// Gets all the locations.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting locations.</param>
        /// <returns>List&lt;Location&gt;</returns>
        Task<List<Location>> GetAllAsync(LocationQueryObject queryObject);

        /// <summary>
        /// Updates a location.
        /// </summary>
        /// <param name="id">The identifier of the location.</param>
        /// <param name="dto">The DTO containing updated location data.</param>
        /// <returns>Location</returns>
        Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto);

        /// <summary>
        /// Checks the lobby count for a location.
        /// </summary>
        /// <param name="id">The identifier of the location.</param>
        /// <returns>Bool</returns>
        Task<bool> CheckLobbyCount(Guid id);

        /// <summary>
        /// Adds a lobby to the lobby count for a location.
        /// </summary>
        /// <param name="id">The identifier of the location.</param>
        /// <param name="lobbyId">The identifier of the lobby to add.</param>
        /// <returns>Task</returns>
        Task AddLobbyToCount(Guid id, Guid lobbyId);

    }
}
