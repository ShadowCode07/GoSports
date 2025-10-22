using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task AddLobbyToCount(Guid id, Guid lobbyId)
        {
            Location update = await GetByIdAsync(id);
            Lobby addedLobby = await _context.lobbies.FindAsync(lobbyId);

            update.Lobbies.Add(addedLobby);
            update.CurrentLobbyCount += 1;

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

        public async Task<Location> CreateAsync(Location location, List<string> sports)
        {
            var locationSports = await _context.sports.Where(s => sports.Contains(s.Name)).ToListAsync();

            var missingSports = sports.Except(locationSports.Select(s => s.Name)).ToList();
           
            if (missingSports.Any())
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", missingSports)}");
            }    

            location.Sports = locationSports;

            await _dbSet.AddAsync(location);

            await _context.SaveChangesAsync();

            return location;
        }

        public async Task<List<Location>> GetAllAsync(LocationQueryObject queryObject)
        {
            var locations = _context.locations
                .Include(l => l.LocationType)
                .Include(l => l.Sports)
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

            if (!string.IsNullOrWhiteSpace(queryObject.LocationTypeName))
            {
                locations = locations.Where(l =>
                        l.LocationType.Name.Contains(queryObject.LocationTypeName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Surface))
            {
                locations = locations.Where(l =>
                         l.LocationType.Surface.Contains(queryObject.Surface));
            }

            if (queryObject.HasLights)
            {
                locations = locations.Where(l =>
                        l.LocationType.HasLights == queryObject.HasLights);
            }

            if (queryObject.IsIndoor)
            {
                locations = locations.Where(l =>
                        l.LocationType.HasLights == queryObject.IsIndoor);
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    locations = queryObject.IsDescending ? locations.OrderByDescending(l => l.Name) : locations.OrderBy(l => l.Name);
                }
            }

            return await locations.ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(l => l.Id == id);
        }

        public override async Task<List<Location>> GetAllAsync()
        {
            return await _dbSet.Include(l => l.Lobbies).Include(l => l.Sports).Include(l => l.LocationType).ToListAsync();
        }
        public override async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(l => l.Lobbies).Include(l => l.Sports).Include(l => l.LocationType).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Location?> UpdateAsync(Guid id, LocationUpdateDto dto)
        {
            var locationSports = await _context.sports.Where(s => dto.Sports.Contains(s.Name)).ToListAsync();

            var missingSports = dto.Sports.Except(locationSports.Select(s => s.Name)).ToList();

            if (missingSports.Any())
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", missingSports)}");
            }

            var update = await _dbSet.FindAsync(id);

            update.Name = dto.Name;
            update.Description = dto.Description;
            update.LocationType = dto.LocationType.ToLocationTypeFromUpdate();
            update.MaxLobbyCount = dto.MaxLobbyCount;
            update.Sports = locationSports;

            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }

    }

}
