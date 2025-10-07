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
                LocationId = lobbyModel.LocationId,
                Location = lobbyModel.Location
            };
        }

        public static Lobby ToLobbyFromCreate(this LobbyCreateDto lobbyDto)
        {
            return new Lobby
            {
                Name = lobbyDto.Name,
                LocationId = lobbyDto.LocationId
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
