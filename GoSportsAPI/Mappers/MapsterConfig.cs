using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using Mapster;

namespace GoSportsAPI.Mappers
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Lobby, LobbyResponceDto>
                .NewConfig()
                .Map(dest => dest.Sport, src => src.Sport);

            TypeAdapterConfig<Sport, SportResponceDto>
                .NewConfig();

            TypeAdapterConfig<LocationCreateDto, Location>
                .NewConfig()
                .Ignore(dest => dest.Sports);

            TypeAdapterConfig<Location, LocationResponceDto>
                .NewConfig()
                .Map(dest => dest.Version, src => Convert.ToBase64String(src.Version));

            TypeAdapterConfig<LocationUpdateDto, Location>
                .NewConfig()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LocationId)
                .IgnoreNullValues(true);

            TypeAdapterConfig<LocationType, LocationTypeResponceDto>
    .           NewConfig()
                .Map(dest => dest.Version, src => Convert.ToBase64String(src.Version));

            TypeAdapterConfig<LocationTypeUpdateDto, LocationType>
                .NewConfig()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LocationId)
                .Ignore(dest => dest.LocationTypeId)
                .IgnoreNullValues(true);

            TypeAdapterConfig<Lobby, LobbyResponceDto>
                .NewConfig()
                .Map(dest => dest.Version, src => Convert.ToBase64String(src.Version));

            TypeAdapterConfig<LobbyUpdateDto, Lobby>
                .NewConfig()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LocationId)
                .IgnoreNullValues(true);

            TypeAdapterConfig<Sport, SportResponceDto>
                .NewConfig()
                .Map(dest => dest.Version, src => Convert.ToBase64String(src.Version));

            TypeAdapterConfig<SportUpdateDto, Sport>
                .NewConfig()
                .Ignore(dest => dest.Version)
                .IgnoreNullValues(true);
        }
    }
}
