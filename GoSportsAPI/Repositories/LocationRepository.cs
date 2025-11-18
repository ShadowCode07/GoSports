using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Repositories
{
    /// <summary>
    /// Provides repository operations for <see cref="Location"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD functionality from <see cref="Repository{T}"/> 
    /// and implements additional methods defined in <see cref="ILocationRepository"/>.
    /// </remarks>
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
        }


        /// <summary>Adds a lobby to the location's lobby list</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="lobbyId">The lobby identifier.</param>
        public async Task AddLobbyToCount(Guid id, Guid lobbyId)
        {
            Location update = await GetByIdAsync(id);
            Lobby addedLobby = await _context.Lobbies.FindAsync(lobbyId);

            update.Lobbies.Add(addedLobby);
            update.CurrentLobbyCount += 1;

            _dbSet.Update(update);
            await _context.SaveChangesAsync();
        }


        /// <summary>Checks the lobby count.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
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


        /// <summary>Creates a location</summary>
        /// <param name="location">The location.</param>
        /// <param name="sports">The sports.</param>
        /// <returns>Location</returns>
        /// <exception cref="System.Exception">The following sports were not found: {string.Join(", ", missingSports)}</exception>
        public async Task<Location> CreateAsync(Location location, List<string> sports)
        {
            var locationSports = await _context.Sports.Where(s => sports.Contains(s.Name)).ToListAsync();

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


        /// <summary>Gets all locations</summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        ///   <para>List&lt;Location&gt;</para>
        /// </returns>
        public async Task<List<Location>> GetAllAsync(LocationQueryObject queryObject)
        {
            var locations = _context.Locations
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

            if (!string.IsNullOrWhiteSpace(queryObject.SportName))
            {
                locations = locations.Where(l =>
                            l.Lobbies.Any(lb => lb.Name.Contains(queryObject.SportName)));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Surface))
            {
                locations = locations.Where(l =>
                            l.LocationType.Surface.Contains(queryObject.Surface));
            }

            if (queryObject.HasLights != null)
            {
                locations = locations.Where(l =>
                            l.LocationType.HasLights == queryObject.HasLights);
            }


            if (queryObject.IsIndoor != null)
            {
                locations = locations.Where(l =>
                            l.LocationType.IsIndoor == queryObject.IsIndoor);
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


        /// <summary>Checks if the location exists</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        public override async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(l => l.Id == id);
        }


        /// <summary>Gets the location by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Location</returns>
        public override async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(l => l.Lobbies).Include(l => l.Sports).Include(l => l.LocationType).FirstOrDefaultAsync(l => l.Id == id);
        }


        /// <summary>Updates given location</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>Location</returns>
        /// <exception cref="System.Exception">The following sports were not found: {string.Join(", ", missingSports)}</exception>
        public async Task<Location?> UpdateAsync(Guid id, Location updatedLocation, List<string> sports, string version, string typeVersion)
        {
            var update = await _dbSet
                .Include(l => l.Sports)
                .Include(l => l.LocationType)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (update == null) return null;

            var locationVersionBytes = Convert.FromBase64String(version);
            _context.Entry(update).Property(l => l.Version).OriginalValue = locationVersionBytes;

            var locationSports = await _context.Sports
                .Where(s => sports.Contains(s.Name))
                .ToListAsync();

            var missingSports = sports.Except(locationSports.Select(s => s.Name)).ToList();

            if (missingSports.Any())
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", missingSports)}");
            }

            if (!string.IsNullOrWhiteSpace(typeVersion))
            {
                var ltVersionBytes = Convert.FromBase64String(typeVersion);
                _context.Entry(update.LocationType).Property(lt => lt.Version).OriginalValue = ltVersionBytes;
            }

            update.Name = updatedLocation.Name;
            update.Description = updatedLocation.Description;

            var existingLt = update.LocationType;

            update.LocationType = updatedLocation.LocationType;

            var tempLt = update.LocationType;
            if (existingLt == null)
                throw new Exception("LocationType not found.");

            existingLt.Name = tempLt.Name;
            existingLt.HasLights = tempLt.HasLights;
            existingLt.IsIndoor = tempLt.IsIndoor;
            existingLt.Surface = tempLt.Surface;

            update.LocationType = existingLt;

            _context.Entry(tempLt).State = EntityState.Detached;

            update.MaxLobbyCount = updatedLocation.MaxLobbyCount;

            update.Sports.Clear();
            foreach (var s in locationSports)
                update.Sports.Add(s);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Location was modified elsewhere. Try again.");
            }

            return update;
        }

    }
}