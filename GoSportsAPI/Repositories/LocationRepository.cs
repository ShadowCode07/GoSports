using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Locations;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
        }

        public override async Task<List<Location>> GetAllAsync()
        {
            return await _dbSet.Include(l => l.Lobbies).ToListAsync();
        }
        public override async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(l => l.Lobbies).FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<Location?> UpdateAsync(Guid id, Location entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto)
        {
            var update = dto.ToLocationFromUpdate();
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }

}
