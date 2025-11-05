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
        Task<LobbyResponceDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<LobbyResponceDto>> GetAllAsync(LobbyQueryObject queryObject);
    }
}
