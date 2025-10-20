using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Interfaces
{
    public interface ILobbyRepository : IRepository<Lobby>
    {
        Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject);
        Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto);
    }
}
