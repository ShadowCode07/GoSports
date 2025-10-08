using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto);
    }
}
