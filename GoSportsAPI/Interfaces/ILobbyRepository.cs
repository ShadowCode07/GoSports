using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Interfaces
{
    public interface ILobbyRepository : IRepository<Lobby>
    {
        Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto);
    }
}
