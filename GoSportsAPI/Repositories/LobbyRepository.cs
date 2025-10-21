using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto)
        {
            var update = dto.ToLobbyFromUpdate();
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
