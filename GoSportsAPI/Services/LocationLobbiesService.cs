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

        public Task<bool> CheckLobby(Guid id)
        {
            return _lobbyRepository.Exists(id);
        }

        public Task<bool> CheckLobbyCount(Guid locationGuid)
        {
            return _locationRepository.CheckLobbyCount(locationGuid);
        }

        public async Task<LobbyResponceDto> CreateAsync(Guid locationGuid, LobbyCreateDto createDto, Guid hostId)
        {
            var lobbyModel = createDto.ToLobbyFromCreate(locationGuid);

            await _lobbyRepository.CreateAsync(locationGuid, lobbyModel, createDto.SportName, hostId);

            await _locationRepository.AddLobbyToCount(locationGuid, lobbyModel.Id);

            return lobbyModel.ToLobbyResponceDto();
        }

        public async Task<LobbyResponceDto> UpdateAsync(Guid locationGuid, Guid lobbyId, LobbyUpdateDto updateDto)
        {
            var lobby =  await _lobbyRepository.UpdateAsync(locationGuid, lobbyId, updateDto);

            return lobby.ToLobbyResponceDto();
        }
    }
}
