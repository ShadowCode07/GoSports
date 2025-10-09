using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Mappers
{
    public static class LobbyMP
    {
        public static LobbyResponceDto ToLobbyResponceDto(this Lobby lobbyModel)
        {
            return new LobbyResponceDto
            {
                Id = lobbyModel.Id,
                Name = lobbyModel.Name,
                LocationId = lobbyModel.LocationId
            };
        }

        public static Lobby ToLobbyFromCreate(this LobbyCreateDto lobbyDto, Guid locationID)
        {
            return new Lobby
            {
                Name = lobbyDto.Name,
                LocationId = locationID
            };
        }

        public static Lobby ToLobbyFromUpdate(this LobbyUpdateDto lobbyDto)
        {
            return new Lobby
            {
                Name = lobbyDto.Name
            };
        }
    }
}
