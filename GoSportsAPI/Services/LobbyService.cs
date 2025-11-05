using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;

        public LobbyService(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }


        public async Task<Lobby?> DeleteAsync(Guid id)
        {
            return await _lobbyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LobbyResponceDto>> GetAllAsync(LobbyQueryObject queryObject)
        {
            var lobbies = await _lobbyRepository.GetAllAsync(queryObject);

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return lobbyDto;
        }

        public async Task<LobbyResponceDto?> GetByIdAsync(Guid id)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(id);

            var lobbyDto = lobby.ToLobbyResponceDto();

            return lobbyDto;
        }
    }
}
