using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Interfaces
{
    public interface ILobbyRepository : IRepository<Lobby>
    {
        Task<Lobby> CreateAsync(Lobby entity, string sportName);
        Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject);
        Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto, string sportName);
    }
}
