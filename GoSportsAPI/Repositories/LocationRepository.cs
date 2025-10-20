using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace GoSportsAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task AddLobbyToCount(Guid id)
        {
            Location update = await GetByIdAsync(id);
            update.CurrentLobbyCount++;
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckLobbyCount(Guid id)
        {
            var values = await _dbSet
                .Where(l => l.Id == id)
                .Select(l => new { l.CurrentLobbyCount, l.MaxLobbyCount })
                .FirstOrDefaultAsync();

            if (values == null)
            {
                return false;
            }

            return values.CurrentLobbyCount < values.MaxLobbyCount;
        }

        public async Task<List<Location>> GetAllAsync(QueryObject queryObject)
        {
            var locations = _context.locations
                .Include(l => l.Lobbies)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.LobbyName))
            {
                locations = locations.Where(l =>
                        l.Lobbies.Any(lb => lb.Name.Contains(queryObject.LobbyName)));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.LocationName))
            {
                locations = locations.Where(l => l.Name.Contains(queryObject.LocationName));
            }

            return await locations.ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(l => l.Id == id);
        }

        public override async Task<List<Location>> GetAllAsync()
        {
            return await _dbSet.Include(l => l.Lobbies).ToListAsync();
        }
        public override async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(l => l.Lobbies).FirstOrDefaultAsync(l => l.Id == id);
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
