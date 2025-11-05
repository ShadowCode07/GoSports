using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationLobbiesService
    {
        Task<LobbyResponceDto> CreateAsync(Guid locationGuid, LobbyCreateDto createDto);
        Task<LobbyResponceDto> UpdateAsync(Guid locationGuid, Guid lobbyId, LobbyUpdateDto updateDto);
        Task<bool> CheckLobby(Guid id);
        Task<bool> CheckLobbyCount(Guid locationGuid);
    }
}
