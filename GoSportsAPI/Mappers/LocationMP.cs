using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Mappers
{
    public static class LocationMP
    {
        public static LocationResponceDto ToLocationResponceDto(this Location locationModel)
        {
            return new LocationResponceDto
            {
                Id = locationModel.Id,
                Name = locationModel.Name,
                Description = locationModel.Description,
                LocationType = locationModel.LocationType,
                Lobbies = locationModel.Lobbies.Select(l => l.ToLobbyResponceDto()).ToList(),
                CurrentLobbyCount = locationModel.CurrentLobbyCount,
                MaxLobbyCount = locationModel.MaxLobbyCount
            };
        }

        public static Location ToLocationFromCreate(this LocationCreateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType.ToLocationTypeFromCreate(),
                MaxLobbyCount = locationDto.MaxLobbyCount,
            };
        }

        public static Location ToLocationFromUpdate(this LocationUpdateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType.ToLocationTypeFromUpdate(),
                MaxLobbyCount = locationDto.MaxLobbyCount,
            };
        }
    }
}
