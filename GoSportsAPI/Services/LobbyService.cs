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
        private readonly IUserProfileRepository _userProfileRepository;

        public LobbyService(ILobbyRepository lobbyRepository, IUserProfileRepository userProfileRepository)
        {
            _lobbyRepository = lobbyRepository;
            _userProfileRepository = userProfileRepository;
        }


        public async Task<Lobby?> DeleteAsync(Guid id)
        {
            return await _lobbyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LobbyResponseDto>> GetAllAsync(LobbyQueryObject queryObject)
        {
            var lobbies = await _lobbyRepository.GetAllAsync(queryObject);

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return lobbyDto;
        }

        public async Task<LobbyResponseDto?> GetByIdAsync(Guid id)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(id);

            var lobbyDto = lobby.ToLobbyResponceDto();

            return lobbyDto;
        }

        /*public async Task JoinLobbyAsync(Guid lobbyId, Guid userProfileId)
        {
            var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found.");

            if (profile.LobbyId != null)
                throw new InvalidOperationException("User is already in a lobby.");

            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            if (lobby == null)
                throw new InvalidOperationException("Lobby not found.");

            if (lobby.Users.Count >= lobby.MaxPlayerCount)
                throw new InvalidOperationException("Lobby is full.");

            lobby.Users.Add(profile);
            profile.LobbyId = lobby.Id;

            await _lobbyRepository.UpdateAsync(lobby);
            await _userProfileRepository.UpdateAsync(profile);
        }

        public async Task LeaveLobbyAsync(Guid lobbyId, Guid userProfileId)
        {
            var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found.");

            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            if (lobby == null)
                throw new InvalidOperationException("Lobby not found.");

            if (!lobby.Users.Any(u => u.Id == userProfileId))
                return;

            if (lobby.HostProfileId == userProfileId)
            {
                lobby.IsCanceled = true;
            }

            lobby.Users.Remove(profile);
            profile.LobbyId = null;

            await _lobbyRepository.UpdateAsync(lobby);
            await _userProfileRepository.UpdateAsync(profile);
        }*/
    }
}
