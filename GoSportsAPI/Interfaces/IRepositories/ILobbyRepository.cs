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
        /// Returns all lobbies filtered and sorted using the provided query object.
        /// </summary>
        Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject);

        /// <summary>
        /// Returns a lobby with all required related data included.
        /// Perfect for service-layer logic such as join/leave operations.
        /// </summary>
        Task<Lobby?> GetWithDetailsAsync(Guid id);

        /// <summary>
        /// Returns all lobbies hosted by a specific user profile.
        /// </summary>
        Task<List<Lobby>> GetByHostProfileAsync(Guid hostProfileId);

    }
}
