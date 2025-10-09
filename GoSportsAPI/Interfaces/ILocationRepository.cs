using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto);
        Task<bool> CheckLobbyCount(Guid id);
        Task AddLobbyToCount(Guid id);
    }
}
