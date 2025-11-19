using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GoSportsAPI.Repositories
{
    /// <summary>
    /// Provides repository operations for <see cref="Lobby"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD functionality from <see cref="Repository{T}"/> 
    /// and implements additional methods defined in <see cref="ILobbyRepository"/>.
    /// </remarks>
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
    {

        public LobbyRepository(ApplicationDBContext context) : base(context)
        {
        }


        public async Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject)
        {
            IQueryable<Lobby> lobbies = _dbSet
                .Include(l => l.Location)
                .Include(l => l.Sport)
                .Include(l => l.HostProfile)
                    .ThenInclude(p => p.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.LobbyName))
            {
                var lobbyName = queryObject.LobbyName.Trim().ToLower();
                lobbies = lobbies.Where(l => l.Name.ToLower().Contains(lobbyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SportName))
            {
                var sportName = queryObject.SportName.Trim().ToLower();
                lobbies = lobbies.Where(l => l.Sport.Name.ToLower().Contains(sportName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.LocationName))
            {
                var locationName = queryObject.LocationName.Trim().ToLower();
                lobbies = lobbies.Where(l => l.Location.Name.ToLower().Contains(locationName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy.Trim().ToLower())
                {
                    case "name":
                    case "lobbyname":
                        lobbies = queryObject.IsDescending
                            ? lobbies.OrderByDescending(l => l.Name)
                            : lobbies.OrderBy(l => l.Name);
                        break;

                    case "sport":
                    case "sportname":
                        lobbies = queryObject.IsDescending
                            ? lobbies.OrderByDescending(l => l.Sport.Name)
                            : lobbies.OrderBy(l => l.Sport.Name);
                        break;

                    case "location":
                    case "locationname":
                        lobbies = queryObject.IsDescending
                            ? lobbies.OrderByDescending(l => l.Location.Name)
                            : lobbies.OrderBy(l => l.Location.Name);
                        break;

                    case "players":
                    case "currentplayercount":
                        lobbies = queryObject.IsDescending
                            ? lobbies.OrderByDescending(l => l.CurrentPlayerCount)
                            : lobbies.OrderBy(l => l.CurrentPlayerCount);
                        break;
                }
            }

            return await lobbies.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Lobby?> GetWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(l => l.Location)
                    .ThenInclude(loc => loc.LocationType)
                .Include(l => l.Sport)
                .Include(l => l.HostProfile)
                    .ThenInclude(p => p.User)
                .Include(l => l.Users)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        /// <inheritdoc/>
        public async Task<List<Lobby>> GetByHostProfileAsync(Guid hostProfileId)
        {
            return await _dbSet
                .Where(l => l.HostProfileId == hostProfileId)
                .Include(l => l.Location)
                .Include(l => l.Sport)
                .ToListAsync();
        }
    }
}