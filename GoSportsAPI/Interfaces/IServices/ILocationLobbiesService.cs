using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationLobbiesService
    {
        Task<IEnumerable<LobbyResponseDto>> GetLobbiesForLocationAsync(Guid locationId);

    }
}
