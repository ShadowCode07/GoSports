using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Dtos.Profiles;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using Mapster;

namespace GoSportsAPI.Mappers
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Sport, SportResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version));

            config.NewConfig<SportCreateDto, Sport>()
                .Ignore(dest => dest.Version)
                .IgnoreNullValues(true);

            config.NewConfig<SportUpdateDto, Sport>()
                .Ignore(dest => dest.Version)
                .IgnoreNullValues(true)
                .AfterMapping((src, dest) =>
                {
                    dest.Version = Convert.FromBase64String(src.ConcurrencyToken);
                });

            config.NewConfig<LocationType, LocationTypeResponseDto>();

            config.NewConfig<LocationTypeCreateDto, LocationType>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LocationId)
                .IgnoreNullValues(true);

            config.NewConfig<LocationTypeUpdateDto, LocationType>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LocationId)
                .IgnoreNullValues(true);

            config.NewConfig<Location, LocationResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version))
                .Map(dest => dest.CurrentLobbyCount, src => src.Lobbies.Count);

            config.NewConfig<LocationCreateDto, Location>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .IgnoreNullValues(true)
                .AfterMapping((src, dest) =>
                {
                    dest.LocationType = new LocationType
                    {
                        Name = src.LocationType.Name,
                        IsIndoor = src.LocationType.IsIndoor,
                        HasLights = src.LocationType.HasLights,
                    };
                });

            config.NewConfig<LocationUpdateDto, Location>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .IgnoreNullValues(true)
                .AfterMapping((src, dest) =>
                {
                    dest.Version = Convert.FromBase64String(src.ConcurencyToken);

                    if (dest.LocationType == null)
                    {
                        dest.LocationType = new LocationType();
                    }

                    dest.LocationType.Name = src.LocationType.Name;
                    dest.LocationType.IsIndoor = src.LocationType.IsIndoor;
                    dest.LocationType.HasLights = src.LocationType.HasLights;
                });

            config.NewConfig<UserProfile, UserProfileResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version))
                .Map(dest => dest.LobbyName, src => src.Lobby != null ? src.Lobby.Name : null)
                .Map(dest => dest.Sports, src => src.Sports);

            config.NewConfig<UserProfileUpdateDto, UserProfile>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.LobbyId)
                .Ignore(dest => dest.Lobby)
                .Ignore(dest => dest.UserId)
                .Ignore(dest => dest.User)
                .IgnoreNullValues(true)
                .AfterMapping((src, dest) =>
                {
                    dest.Version = Convert.FromBase64String(src.ConcurrencyToken);
                });

            config.NewConfig<Lobby, LobbyResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version))
                .Map(dest => dest.LocationId, src => src.LocationId)
                .Map(dest => dest.LocationName, src => src.Location.Name)
                .Map(dest => dest.SportId, src => src.SportId)
                .Map(dest => dest.SportName, src => src.Sport.Name)
                .Map(dest => dest.HostProfileId, src => src.HostProfileId)
                .Map(dest => dest.HostUserName, src => src.HostProfile.User.UserName)
                .Map(dest => dest.CurrentPlayerCount, src => src.CurrentPlayerCount)
                .Map(dest => dest.MaxPlayerCount, src => src.MaxPlayerCount);

            config.NewConfig<LobbyCreateDto, Lobby>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.HostProfileId)
                .Ignore(dest => dest.HostProfile)
                .Ignore(dest => dest.Users)
                .Ignore(dest => dest.CurrentPlayerCount)
                .IgnoreNullValues(true);

            config.NewConfig<LobbyUpdateDto, Lobby>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.HostProfileId)
                .Ignore(dest => dest.HostProfile)
                .Ignore(dest => dest.Users)
                .Ignore(dest => dest.CurrentPlayerCount)
                .IgnoreNullValues(true)
                .AfterMapping((src, dest) =>
                {
                    dest.Version = Convert.FromBase64String(src.ConcurrencyToken);
                });
        }
    }
}
