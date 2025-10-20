using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<List<Location>> GetAllAsync(QueryObject queryObject);
        Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto);
        Task<bool> CheckLobbyCount(Guid id);
        Task AddLobbyToCount(Guid id);
    }
}
