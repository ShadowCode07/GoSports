using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Interfaces.IRepositories
{
    /// <summary>
    /// Defines repository operations specific to <see cref="Lobby"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD operations from <see cref="IRepository{T}"/> 
    /// and may include additional methods for managing lobbies.
    /// </remarks>
    public interface ILobbyRepository : IRepository<Lobby>
    {
        /// <summary>
        /// Creates a new lobby.
        /// </summary>
        /// <param name="locationId">The identifier of the location where the lobby belongs.</param>
        /// <param name="entity">The lobby entity to create.</param>
        /// <param name="sportName">The name of the sport associated with the lobby.</param>
        /// <returns>Lobby</returns>
        Task<Lobby> CreateAsync(Guid locationId, Lobby lobby, string sportName);
        Task<Lobby> CreateAsync(Guid locationId, Lobby lobbt, string sportName, Guid hostProfileId);

        /// <summary>
        /// Gets all the lobbies.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting lobbies.</param>
        /// <returns>List&lt;Lobby&gt;</returns>
        Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject);

        /// <summary>
        /// Updates a lobby.
        /// </summary>
        /// <param name="id">The identifier of the lobby.</param>
        /// <param name="dto">The DTO containing updated lobby data.</param>
        /// <param name="sportName">The name of the sport associated with the lobby.</param>
        /// <returns>Lobby</returns>
        Task<Lobby?> UpdateAsync(Guid locationId, Guid id, LobbyUpdateDto dto);

    }
}
