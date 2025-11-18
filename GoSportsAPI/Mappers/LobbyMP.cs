using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;
using Mapster;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for lobbies.</summary>
    public static class LobbyMP
    {
        /// <summary>Converts to LobbyResponceDto from a lobby model</summary>
        /// <param name="lobbyModel">The lobby model.</param>
        /// <returns>LobbyResponceDto</returns>
        public static LobbyResponseDto ToLobbyResponceDto(this Lobby lobbyModel)
        {
            var lobbyResponceDto = lobbyModel.Adapt<LobbyResponseDto>();

            return lobbyResponceDto;
        }


        /// <summary>Converts to lobby from a create Dto.</summary>
        /// <param name="lobbyDto">The lobby create dto.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns>Lobby</returns>
        public static Lobby ToLobbyFromCreate(this LobbyCreateDto lobbyDto, Guid locationID)
        {
            var lobby = lobbyDto.Adapt<Lobby>();

            return lobby;
        }


        /// <summary>Converts to lobby from an update Dto.</summary>
        /// <param name="lobbyDto">The lobby update dto.</param>
        /// <returns>Lobby</returns>
        public static Lobby ToLobbyFromUpdate(this LobbyUpdateDto lobbyDto)
        {
            var lobby = lobbyDto.Adapt<Lobby>();

            return lobby;
        }
    }
}
