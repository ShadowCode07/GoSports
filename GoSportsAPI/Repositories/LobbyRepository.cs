using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoSportsAPI.Repositories
{
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
    {

        public LobbyRepository(ApplicationDBContext context) : base(context)
        {
        }


        public async Task<Lobby> CreateAsync(Lobby lobby, string sportName)
        {
            var lobbySports = _context.sports.Where(s => sportName.Contains(s.Name)).FirstOrDefault();

            if (lobbySports == null)
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", sportName)}");
            }

            lobby.Sport = lobbySports;

            await _dbSet.AddAsync(lobby);
            await _context.SaveChangesAsync();
            return lobby;
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

        public async Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto, string sportName)
        {
            var lobbySports = _context.sports.Where(s => sportName.Contains(s.Name)).FirstOrDefault();

            if (lobbySports == null)
            {
                throw new Exception($"The following sports were not found: {string.Join(", ", sportName)}");
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
