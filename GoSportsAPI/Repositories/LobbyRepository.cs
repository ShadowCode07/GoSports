﻿using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;
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


        /// <summary>Creates a lobby at the given location.</summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="lobby">The lobby.</param>
        /// <param name="sportName">Name of the sport.</param>
        /// <returns>Lobby<br /></returns>
        /// <exception cref="System.Exception">The following sports were not found: {sportName}
        /// or
        /// The following location does not practise this sport: {sportName}</exception>
        public async Task<Lobby> CreateAsync(Guid locationId, Lobby lobby, string sportName)
        {

            var lobbySports = _context.sports.Where(s => sportName.Contains(s.Name)).FirstOrDefault();

            if (lobbySports == null)
            {
                throw new Exception($"The following sports were not found: {sportName}");
            }

            var location = await _context.locations
                .Include(l => l.Sports)
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            var locationSports = location.Sports.Where(s => sportName.Contains(s.Name));


            if (!locationSports.Any())
            {
                throw new Exception($"The following location does not practise this sport: {sportName}");
            }

            lobby.Sport = lobbySports;

            await _dbSet.AddAsync(lobby);
            await _context.SaveChangesAsync();

            return lobby;
        }


        /// <summary>Returs all lobbies</summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>List&lt;Lobby&gt;<br /></returns>
        public async Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject)
        {
            var lobbies = _context.lobbies
                .Include(l => l.Sport)
                .Include(l => l.Location)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.LobbyName))
            {
                lobbies = lobbies.Where(l => l.Name.Contains(queryObject.LobbyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.LocationName))
            {
                lobbies = lobbies.Where(l => l.Location.Name.Contains(queryObject.LocationName));
            }

            if(!string.IsNullOrEmpty(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    lobbies = queryObject.IsDescending ? lobbies.OrderByDescending(l => l.Name) : lobbies.OrderBy(l => l.Name);
                }
            }


            return await lobbies.ToListAsync();
        }


        /// <summary>Updates a lobby.</summary>
        /// <param name="locationId">The location to which the lobby belongs.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="sportName">Name of the sport.</param>
        /// <returns>Lobby</returns>
        /// <exception cref="System.Exception">The following sports were not found: {string.Join(", ", sportName)}</exception>
        public async Task<Lobby?> UpdateAsync(Guid locationId, Guid id, LobbyUpdateDto dto, string sportName)
        {
            var lobbySports = _context.sports.Where(s => sportName.Contains(s.Name)).FirstOrDefault();

            if (lobbySports == null)
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", sportName)}");
            }

            var location = await _context.locations
                .Include(l => l.Sports)
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            var locationSports = location.Sports.Where(s => sportName.Contains(s.Name));

            if (!locationSports.Any())
            {
                throw new Exception($"The following location does not practise this sport: {sportName}");
            }

            var update = await _dbSet.FindAsync(id);

            update.Name = dto.Name;
            update.Sport = lobbySports;

            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
