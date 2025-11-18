using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> CreateAsync(Location entity, List<string> sports)
        {
            return await _locationRepository.CreateAsync(entity, sports);
        }

        public async Task<Location?> DeleteAsync(Guid id)
        {
           return await _locationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LocationResponceDto>> GetAllAsync(LocationQueryObject queryObject)
        {
            var locations = await _locationRepository.GetAllAsync(queryObject);

            var locationDto = locations.Select(l => l.ToLocationResponceDto());

            return locationDto;
        }

        public async Task<Location?> GetByIdAsync(Guid id)
        {
            var location = await _locationRepository.GetByIdAsync(id);

            return location;
        }

        public async Task<Location?> UpdateAsync(Guid id, LocationUpdateDto updateDto)
        {
            var updatedLocation = updateDto.ToLocationFromUpdate();

            var locationModel = await _locationRepository.UpdateAsync(id, updatedLocation, updateDto.Sports, updateDto.Version, updateDto.LocationType.Version);

            return locationModel;
        }
    }
}
