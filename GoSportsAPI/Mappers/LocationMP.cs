using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Models.Locations;
using Mapster;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for locations.</summary>
    public static class LocationMP
    {

        /// <summary>Converts to LocationResponceDto from a location model.</summary>
        /// <param name="locationModel">The location model.</param>
        /// <returns>LocationResponceDto</returns>
        public static LocationResponseDto ToLocationResponceDto(this Location locationModel)
        {
            var locationResponceDto = locationModel.Adapt<LocationResponseDto>();

            return locationResponceDto;
        }


        /// <summary>Converts to location from a create Dto.</summary>
        /// <param name="locationDto">The location create dto.</param>
        /// <returns>Location</returns>
        public static Location ToLocationFromCreate(this LocationCreateDto locationDto)
        {
            var location = locationDto.Adapt<Location>();

            return location;
        }


        /// <summary>Converts to location from an update Dto.</summary>
        /// <param name="locationDto">The location update dto.</param>
        /// <returns>Location</returns>
        public static Location ToLocationFromUpdate(this LocationUpdateDto locationDto)
        {
            var location = locationDto.Adapt<Location>();

            return location;
        }
    }
}