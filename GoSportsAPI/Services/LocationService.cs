using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ISportRepository _sportRepository;

        public LocationService(ILocationRepository locationRepository, ISportRepository sportRepository)
        {
            _locationRepository = locationRepository;
            _sportRepository = sportRepository;
        }

        public async Task<LocationResponseDto> CreateAsync(LocationCreateDto dto)
        {
            var location = dto.Adapt<Location>();

            location.Sports = await ResolveSportsByNames(dto.Sports);

            await _locationRepository.CreateAsync(location);
            await _locationRepository.SaveChanges();

            return location.Adapt<LocationResponseDto>();
        }

        public async Task<LocationResponseDto?> GetByIdAsync(Guid id)
        {
            var location = await _locationRepository.GetWithDetailsAsync(id);
                    
            return location?.Adapt<LocationResponseDto>();
        }

        public async Task<IEnumerable<LocationResponseDto>> GetAllAsync(LocationQueryObject queryObject)
        {
            var locations = await _locationRepository.GetAllAsync(queryObject);

            return locations.Adapt<IEnumerable<LocationResponseDto>>();
        }

        public async Task<LocationResponseDto?> UpdateAsync(Guid id, LocationUpdateDto dto)
        {
            var location = await _locationRepository.GetWithDetailsAsync(id);
            if (location is null)
            {
                return null;
            }

            dto.Adapt(location);

            location.Sports = await ResolveSportsByNames(dto.Sports);

            try
            {
                await _locationRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return location.Adapt<LocationResponseDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _locationRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }

            await _locationRepository.SaveChanges();
            return true;
        }

        private async Task<List<Sport>> ResolveSportsByNames(IEnumerable<string> names)
        {
            var result = new List<Sport>();

            if (names == null)
            {
                return result;
            }
                
            foreach (var name in names.Where(n => !string.IsNullOrWhiteSpace(n)))
            {
                var sport = await _sportRepository.GetByNameAsync(name.Trim());
                if (sport != null)
                {
                    if (!result.Any(s => s.Id == sport.Id))
                    {
                        result.Add(sport);
                    }

                }
            }

            return result;
        }
    }
}
