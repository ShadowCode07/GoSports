using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Lobbies;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILobbyService
    {
        Task<Lobby?> DeleteAsync(Guid id);
        Task<LobbyResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<LobbyResponseDto>> GetAllAsync(LobbyQueryObject queryObject);
        //Task JoinLobbyAsync(Guid lobbyId, Guid userProfileId);
        //Task LeaveLobbyAsync(Guid lobbyId, Guid userProfileId);
    }
}
