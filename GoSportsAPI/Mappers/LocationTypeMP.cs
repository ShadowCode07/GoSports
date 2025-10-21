using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Mappers
{
    public static class LocationTypeMP
    {
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
