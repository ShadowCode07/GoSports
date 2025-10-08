using GoSportsAPI.Data;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Repositories
{
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
    {
        public LobbyRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
