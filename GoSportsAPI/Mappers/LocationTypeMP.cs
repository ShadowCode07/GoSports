using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Models.Locations;
using Mapster;

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
        public static LocationTypeResponseDto ToLocationTypeResponceDto(this LocationType locationTypeModel)
        {
            var locationTypeResponceDto = locationTypeModel.Adapt<LocationTypeResponseDto>();

            return locationTypeResponceDto;
        }


        /// <summary>Converts to location type from a create Dto.</summary>
        /// <param name="locationTypeDto">The location type create dto.</param>
        /// <returns>
        ///   <para>LocationType</para>
        /// </returns>
        public static LocationType ToLocationTypeFromCreate(this LocationTypeCreateDto locationTypeDto)
        {
            var locationType = locationTypeDto.Adapt<LocationType>();

            return locationType;
        }


        /// <summary>Converts to location type from an update Dto.</summary>
        /// <param name="locationTypeDto">The location type update dto.</param>
        /// <returns>LocationType</returns>
        public static LocationType ToLocationTypeFromUpdate(this LocationTypeUpdateDto locationTypeDto)
        {
            var locationType = locationTypeDto.Adapt<LocationType>();

            return locationType;
        }
    }
}
