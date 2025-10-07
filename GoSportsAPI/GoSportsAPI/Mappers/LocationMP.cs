
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mdels.Locations;
using Microsoft.Identity.Client;

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
                LocationType = locationDto.LocationType,
                MaxLobbyCount = locationDto.MaxLobbyCount,
            };
        }

        public static Location ToLocationFromUpdate(this LocationUpdateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType,
                MaxLobbyCount = locationDto.MaxLobbyCount,
            };
        }
    }
}
