using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;

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

        public async Task<Lobby?> UpdateAsync(Guid id, LobbyUpdateDto dto)
        {
            var update = dto.ToLobbyFromUpdate();
            _dbSet.Update(update);
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
