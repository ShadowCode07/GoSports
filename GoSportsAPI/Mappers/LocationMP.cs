using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for locations.</summary>
    public static class LocationMP
    {

        /// <summary>Converts to LocationResponceDto from a location model.</summary>
        /// <param name="locationModel">The location model.</param>
        /// <returns>LocationResponceDto</returns>
        public static LocationResponceDto ToLocationResponceDto(this Location locationModel)
        {
            return new LocationResponceDto
            {
                LocationId = locationModel.LocationId,
                Name = locationModel.Name,
                Description = locationModel.Description,
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
                LocationType = locationModel.LocationType,
                Lobbies = locationModel.Lobbies.Select(l => l.ToLobbyResponceDto()).ToList(),
                Sports = locationModel.Sports.Select(s => s.ToSportResponceDto()).ToList(),
                CurrentLobbyCount = locationModel.CurrentLobbyCount,
                MaxLobbyCount = locationModel.MaxLobbyCount
            };
        }


        /// <summary>Converts to location from a create Dto.</summary>
        /// <param name="locationDto">The location create dto.</param>
        /// <returns>Location</returns>
        public static Location ToLocationFromCreate(this LocationCreateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                Latitude = locationDto.Latitude,
                Longitude = locationDto.Longitude,
                LocationType = locationDto.LocationType.ToLocationTypeFromCreate(),
                MaxLobbyCount = locationDto.MaxLobbyCount
            };
        }


        /// <summary>Converts to location from an update Dto.</summary>
        /// <param name="locationDto">The location update dto.</param>
        /// <returns>Location</returns>
        public static Location ToLocationFromUpdate(this LocationUpdateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType.ToLocationTypeFromUpdate(),
                MaxLobbyCount = locationDto.MaxLobbyCount
            };
        }
    }
}