using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ISportService
    {
        Task<SportResponseDto> CreateAsync(SportCreateDto dto);
        Task<SportResponseDto?> UpdateAsync(Guid id, SportUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<SportResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<SportResponseDto>> GetAllAsync(SportQueryObject queryObject);
    }
}
