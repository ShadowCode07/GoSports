using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for lobbies.</summary>
    public static class LobbyMP
    {

        /// <summary>Converts to LobbyResponceDto from a lobby model</summary>
        /// <param name="lobbyModel">The lobby model.</param>
        /// <returns>LobbyResponceDto</returns>
        public static LobbyResponceDto ToLobbyResponceDto(this Lobby lobbyModel)
        {
            return new LobbyResponceDto
            {
                Id = lobbyModel.Id,
                Name = lobbyModel.Name,
                LocationId = lobbyModel.LocationId,
                sport = lobbyModel.Sport.ToSportResponceDto()
            };
        }


        /// <summary>Converts to lobby from a create Dto.</summary>
        /// <param name="lobbyDto">The lobby create dto.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns>Lobby</returns>
        public static Lobby ToLobbyFromCreate(this LobbyCreateDto lobbyDto, Guid locationID)
        {
            return new Lobby
            {
                Name = lobbyDto.Name,
                LocationId = locationID
            };
        }


        /// <summary>Converts to lobby from an update Dto.</summary>
        /// <param name="lobbyDto">The lobby update dto.</param>
        /// <returns>Lobby</returns>
        public static Lobby ToLobbyFromUpdate(this LobbyUpdateDto lobbyDto)
        {
            return new Lobby
            {
                Name = lobbyDto.Name
            };
        }
    }
}
