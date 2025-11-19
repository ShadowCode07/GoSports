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
        public async Task<List<Location>> GetAllAsync(LocationQueryObject queryObject)
        {
            IQueryable<Location> query = _dbSet
                .Include(l => l.LocationType)
                .Include(l => l.Sports)
                .Include(l => l.Lobbies)
                .AsQueryable();
    
            if (!string.IsNullOrWhiteSpace(queryObject.LocationName))
            {
                var name = queryObject.LocationName.Trim().ToLower();
                query = query.Where(l => l.Name.ToLower().Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SportName))
            {
                var sportName = queryObject.SportName.Trim().ToLower();
                query = query.Where(l =>
                    l.Sports.Any(s => s.Name.ToLower().Contains(sportName)));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy.Trim().ToLower())
                {
                    case "name":
                    case "locationname":
                        query = queryObject.IsDescending
                            ? query.OrderByDescending(l => l.Name)
                            : query.OrderBy(l => l.Name);
                        break;

                    case "sportname":
                    case "sport":
                        query = queryObject.IsDescending
                            ? query.OrderByDescending(l =>
                                l.Sports.Any()
                                    ? l.Sports.Min(s => s.Name)
                                    : string.Empty)
                            : query.OrderBy(l =>
                                l.Sports.Any()
                                    ? l.Sports.Min(s => s.Name)
                                    : string.Empty);
                        break;

                    case "lobbies":
                    case "lobbycount":
                        query = queryObject.IsDescending
                            ? query.OrderByDescending(l => l.Lobbies.Count)
                            : query.OrderBy(l => l.Lobbies.Count);
                        break;
                }
            }

            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Location?> GetWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(l => l.LocationType)
                .Include(l => l.Sports)
                .Include(l => l.Lobbies)
                    .ThenInclude(lb => lb.Sport)
                .Include(l => l.Lobbies)
                    .ThenInclude(lb => lb.HostProfile)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}