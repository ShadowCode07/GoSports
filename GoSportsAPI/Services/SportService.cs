using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Services
{
    public class SportService : ISportService
    {
        private readonly ISportRepository _sportRepository;
        public SportService(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public async Task<SportResponseDto> CreateAsync(SportCreateDto dto)
        {
            var sport = dto.Adapt<Sport>();

            await _sportRepository.CreateAsync(sport);
            await _sportRepository.SaveChanges();

            return sport.Adapt<SportResponseDto>();
        }

        public async Task<SportResponseDto?> GetByIdAsync(Guid id)
        {
            var sport = await _sportRepository.GetByIdAsync(id, asNoTracking: true);
            return sport?.Adapt<SportResponseDto>();
        }

        public async Task<IEnumerable<SportResponseDto>> GetAllAsync(SportQueryObject queryObject)
        {
            var sports = await _sportRepository.GetAllAsync(queryObject);
            return sports.Adapt<IEnumerable<SportResponseDto>>();
        }

        public async Task<SportResponseDto?> UpdateAsync(Guid id, SportUpdateDto dto)
        {
            var sport = await _sportRepository.GetByIdAsync(id);
            if (sport is null)
            {
                return null;
            }

            dto.Adapt(sport);

            try
            {
                await _sportRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return sport.Adapt<SportResponseDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _sportRepository.DeleteByIdAsync(id);

            if (!deleted)
            {
                return false;
            }

            await _sportRepository.SaveChanges();
            return true;
        }
    }
}
