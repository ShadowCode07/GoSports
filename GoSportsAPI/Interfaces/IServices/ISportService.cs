using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ISportService
    {
        Task<Sport> CreateAsync(Sport entity);
        Task<Sport?> UpdateAsync(Guid id, SportUpdateDto updateDto);
        Task<Sport?> DeleteAsync(Guid id);
        Task<Sport?> GetByIdAsync(Guid id);
        Task<IEnumerable<SportResponceDto>> GetAllAsync(SportQueryObject queryObject);
    }
}
