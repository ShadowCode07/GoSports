using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Interfaces
{
    public interface ISportRepository : IRepository<Sport>
    {
        Task<List<Sport>> GetAllAsync(SportQueryObject queryObject);
        Task<Sport?> UpdateAsync(Guid id, SportUpdateDto dto);
    }
}
