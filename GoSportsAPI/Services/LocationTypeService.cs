using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Locations;
using Mapster;

namespace GoSportsAPI.Services
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly ILocationTypeRepository _locationTypeRepository;
        public LocationTypeService(ILocationTypeRepository locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }

        public async Task<IEnumerable<LocationTypeResponseDto>> GetLocationTypes(LocationTypeQueryObject queryObject)
        {
            var locationTypes = await _locationTypeRepository.GetAllAsync(queryObject);
            return locationTypes.Adapt<IEnumerable<LocationTypeResponseDto>>();
        }

        public async Task<LocationTypeResponseDto?> GetLocationTypeById(Guid id)
        {
            var locationType = await _locationTypeRepository.GetByIdAsync(id, asNoTracking: true);
            return locationType?.Adapt<LocationTypeResponseDto>();
        }
    }
}

