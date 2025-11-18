using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationLobbiesService
    {
        Task<LobbyResponseDto> CreateAsync(Guid locationGuid, LobbyCreateDto createDto, Guid hostId);
        Task<LobbyResponseDto> UpdateAsync(Guid locationGuid, Guid lobbyId, LobbyUpdateDto updateDto);
        Task<bool> CheckLocation(Guid id);
        Task<bool> CheckLobbyCount(Guid locationGuid);
    }
}
