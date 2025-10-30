using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Models.Lobbies;
using Mapster;

namespace GoSportsAPI.Mappers
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Lobby, LobbyResponceDto>
                .NewConfig()
                .Map(dest => dest.sport, src => src.Sport);
        }
    }
}
