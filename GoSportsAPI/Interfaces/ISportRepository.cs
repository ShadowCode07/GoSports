using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Interfaces
{
    public interface ISportRepository : IRepository<Sport>
    {
        Task<Sport?> UpdateAsync(Guid id, SportUpdateDto dto);
    }
}
