using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Repositories
{
    public class SportRepository : Repository<Sport>, ISportRepository
    {
        public SportRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<Sport?> UpdateAsync(Guid id, SportUpdateDto dto)
        {
            var update = dto.ToSportFromUpdate();
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
