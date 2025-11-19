using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationService
    {
        Task<LocationResponseDto> CreateAsync(LocationCreateDto dto);
        Task<LocationResponseDto?> UpdateAsync(Guid id, LocationUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task<LocationResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<LocationResponseDto>> GetAllAsync(LocationQueryObject queryObject);
    }
}
