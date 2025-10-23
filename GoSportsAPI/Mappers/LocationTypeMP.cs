using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for location types.</summary>
    public static class LocationTypeMP
    {

        /// <summary>
        ///   <para>
        /// Converts to LocationTypeResponceDto from a location model.=</para>
        /// </summary>
        /// <param name="locationTypeModel">The location type model.</param>
        /// <returns>LocationTypeResponceDto</returns>
        public static LocationTypeResponceDto ToLocationTypeResponceDto(this LocationType locationTypeModel)
        {
            return new LocationTypeResponceDto
            {
                Id = locationTypeModel.Id,
                Name = locationTypeModel.Name,
                LocationId = locationTypeModel.LocationId,
                Surface = locationTypeModel.Surface,
                IsIndoor = locationTypeModel.IsIndoor,
                HasLights = locationTypeModel.HasLights
            };
        }


        /// <summary>Converts to location type from a create Dto.</summary>
        /// <param name="locationTypeDto">The location type create dto.</param>
        /// <returns>
        ///   <para>LocationType</para>
        /// </returns>
        public static LocationType ToLocationTypeFromCreate(this LocationTypeCreateDto locationTypeDto)
        {
            return new LocationType
            {
                Name = locationTypeDto.Name,
                Surface = locationTypeDto.Surface,
                HasLights = locationTypeDto.HasLights,
                IsIndoor = locationTypeDto.IsIndoor
            };
        }


        /// <summary>Converts to location type from an update Dto.</summary>
        /// <param name="locationTypeDto">The location type update dto.</param>
        /// <returns>LocationType</returns>
        public static LocationType ToLocationTypeFromUpdate(this LocationTypeUpdateDto locationTypeDto)
        {
            return new LocationType
            {
                Name = locationTypeDto.Name,
                Surface = locationTypeDto.Surface,
                HasLights = locationTypeDto.HasLights,
                IsIndoor = locationTypeDto.IsIndoor
            };
        }
    }
}
