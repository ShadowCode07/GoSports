
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

        public static LocationRequestDto ToLocationRequestDto(this Location locationModel)
        {
            return new LocationRequestDto
            {
                Name = locationModel.Name,
                Description = locationModel.Description,
                LocationType = locationModel.LocationType,
                MaxLobbyCount = locationModel.MaxLobbyCount
            };
        }

        public static Location ToLocationFromRequest(this LocationRequestDto locationModel)
        {
            return new Location
            {
                Name = locationModel.Name,
                Description = locationModel.Description,
                LocationType = locationModel.LocationType,
                MaxLobbyCount = locationModel.MaxLobbyCount,
                CurrentLobbyCount = 0
            };
        }
    }
}
