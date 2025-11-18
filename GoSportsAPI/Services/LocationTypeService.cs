using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Services
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly ILocationTypeRepository _locationTypeRepository;
        public LocationTypeService(ILocationTypeRepository locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }
        public async Task<LocationTypeResponseDto> GetLocationTypeById(Guid id)
        {
            var location = await _locationTypeRepository.GetByIdAsync(id);

            return location.ToLocationTypeResponceDto();
        }

        public async Task<IEnumerable<LocationTypeResponseDto>> GetLocationTypes(LocationTypeQueryObject queryObject)
        {
            var locationTypes = await _locationTypeRepository.GetAllAsync(queryObject);

            var locationTypesDto = locationTypes.Select(l => l.ToLocationTypeResponceDto());

            return locationTypesDto;
        }
    }
}
