using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationService
    {
        Task<Location> CreateAsync(Location entity, List<string> sports);
        Task<Location> UpdateAsync(Guid id, LocationUpdateDto updateDto);
        Task<Location?> DeleteAsync(Guid id);
        Task<Location?> GetByIdAsync(Guid id);
        Task<IEnumerable<LocationResponceDto>> GetAllAsync(LocationQueryObject queryObject);
    }
}
