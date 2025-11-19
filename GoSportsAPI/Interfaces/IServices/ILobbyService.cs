using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Lobbies;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILobbyService
    {
        Task<LobbyResponseDto> CreateAsync(LobbyCreateDto dto, Guid hostProfileId);
        Task<LobbyResponseDto?> UpdateAsync(Guid id, LobbyUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task<LobbyResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<LobbyResponseDto>> GetAllAsync(LobbyQueryObject queryObject);

        Task<bool> JoinLobbyAsync(Guid lobbyId, Guid userProfileId);
        Task<bool> LeaveLobbyAsync(Guid lobbyId, Guid userProfileId);
    }
}
