using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GoSportsAPI.Repositories
{
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
    {

        public LobbyRepository(ApplicationDBContext context) : base(context)
        {
        }


        public async Task<Lobby> CreateAsync(Guid locationId, Lobby entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Lobby>> GetAllAsync(LobbyQueryObject queryObject)
        {
            var lobbies = _context.lobbies
                .Include(l => l.Location)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                lobbies = lobbies.Where(l => l.Name.Contains(queryObject.Name));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.LocationName))
            {
                lobbies = lobbies.Where(l => l.Location.Name.Contains(queryObject.LocationName));
            }

            return await lobbies.ToListAsync();
        }

        public async Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto)
        {
            var update = dto.ToLobbyFromUpdate();
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
