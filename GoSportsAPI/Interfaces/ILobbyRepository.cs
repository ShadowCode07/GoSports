using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Interfaces
{
    public interface ILobbyRepository : IRepository<Lobby>
    {
        Task<List<Lobby>> GetAllAsync(QueryObject queryObject);
        Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto);
    }
}
