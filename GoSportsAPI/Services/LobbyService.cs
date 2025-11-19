using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Sports;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ISportRepository _sportRepository;

        public LobbyService(ILobbyRepository lobbyRepository, IUserProfileRepository userProfileRepository, ILocationRepository locationRepository, ISportRepository sportRepository)
        {
            _lobbyRepository = lobbyRepository;
            _userProfileRepository = userProfileRepository;
            _locationRepository = locationRepository;
            _sportRepository = sportRepository;
        }

        public async Task<LobbyResponseDto> CreateAsync(LobbyCreateDto dto)
        {
            var location = await _locationRepository.GetWithDetailsAsync(dto.LocationId);
            if(location == null)
            {
                throw new Exception("Location not found");
            }

            if(location.Lobbies.Count >= location.MaxLobbyCount)
            {
                throw new Exception("This location is at max capacity");
            }

            var sport = await _sportRepository.GetByNameAsync(dto.SportName);
            if(sport == null)
            {
                throw new Exception("Sport not foubd");
            }

            //var hostProfile = await _userProfileRepository.GetByIdAsync();

            var lobby = dto.Adapt<Lobby>();
            lobby.Sport = sport;
            lobby.Location = location;
            //lobby.HostProfileId = hostProfile.Id;
            //lobby.HostProfile = hostProfile;

            lobby.CurrentPlayerCount = 1;
            //lobby.Users.Add(hostProfile);

            await _lobbyRepository.CreateAsync(lobby);
            await _lobbyRepository.SaveChanges();

            return lobby.Adapt<LobbyResponseDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _lobbyRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }
                
            await _lobbyRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<LobbyResponseDto>> GetAllAsync(LobbyQueryObject queryObject)
        {
            var lobbies = await _lobbyRepository.GetAllAsync(queryObject);

            return lobbies.Adapt<IEnumerable<LobbyResponseDto>>();
        }

        public async Task<LobbyResponseDto?> GetByIdAsync(Guid id)
        {
            var lobby = await _lobbyRepository.GetWithDetailsAsync(id);
            if (lobby is null)
            {
                return null;
            }

            return lobby.Adapt<LobbyResponseDto>();
        }
        public async Task<bool> JoinLobbyAsync(Guid lobbyId, Guid userProfileId)
        {
            var lobby = await _lobbyRepository.GetWithDetailsAsync(lobbyId);
            
            if (lobby is null)
            {
                return false;
            }
                

            var user = await _userProfileRepository.GetWithDetailsAsync(userProfileId);

            if (user is null)
            {
                return false;
            }

            if (lobby.Users.Any(u => u.Id == userProfileId))
            {
                return true;
            }

            if (lobby.CurrentPlayerCount >= lobby.MaxPlayerCount)
            {
                return false;
            }

            lobby.Users.Add(user);
            lobby.CurrentPlayerCount++;

            await _lobbyRepository.SaveChanges();
            return true;
        }

        public async Task<bool> LeaveLobbyAsync(Guid lobbyId, Guid userProfileId)
        {
            var lobby = await _lobbyRepository.GetWithDetailsAsync(lobbyId);
            
            if (lobby is null)
            {
                return false;
            }
                
            var user = lobby.Users.FirstOrDefault(u => u.Id == userProfileId);
            if (user is null)
            {
                return false;
            }

            lobby.Users.Remove(user);
            lobby.CurrentPlayerCount = Math.Max(0, lobby.CurrentPlayerCount - 1);

            if (lobby.HostProfileId == userProfileId)
            {
                await _lobbyRepository.DeleteByIdAsync(lobbyId);
            }

            await _lobbyRepository.SaveChanges();
            return true;
        }

        public async Task<LobbyResponseDto?> UpdateAsync(Guid id, LobbyUpdateDto dto)
        {
            var lobby = await _lobbyRepository.GetWithDetailsAsync(id);

            if (lobby is null)
            {
                return null;
            }

            dto.Adapt(lobby);

            if (lobby.CurrentPlayerCount > lobby.MaxPlayerCount)
            {
                throw new Exception("Max players cannot be less than current player count");
            }

            try
            {
                await _lobbyRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return lobby.Adapt<LobbyResponseDto>();
        }
    }
}
