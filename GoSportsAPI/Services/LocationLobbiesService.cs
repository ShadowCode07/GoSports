using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Services
{
    public class LocationLobbiesService : ILocationLobbiesService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly ILocationRepository _locationRepository;

        public LocationLobbiesService(ILobbyRepository lobbyRepository, ILocationRepository locationRepository)
        {
            _lobbyRepository = lobbyRepository;
            _locationRepository = locationRepository;
        }

        public Task<IEnumerable<LobbyResponseDto>> GetLobbiesForLocationAsync(Guid locationId)
        {
            throw new NotImplementedException();
        }
    }
}
