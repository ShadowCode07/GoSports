using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Services
{
    public class SportService : ISportService
    {
        private readonly ISportRepository _sprotRepository;
        public SportService(ISportRepository sprotRepository)
        {
            _sprotRepository = sprotRepository;
        }
        public async Task<Sport> CreateAsync(Sport entity)
        {
            return await _sprotRepository.CreateAsync(entity);
        }

        public async Task<Sport?> DeleteAsync(Guid id)
        {
            return await _sprotRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SportResponseDto>> GetAllAsync(SportQueryObject queryObject)
        {
            var sports = await _sprotRepository.GetAllAsync(queryObject);

            var sportsDto = sports.Select(l => l.ToSportResponceDto());

            return sportsDto;
        }

        public async Task<Sport?> GetByIdAsync(Guid id)
        {
            var sport = await _sprotRepository.GetByIdAsync(id);
           
            return sport;
        }

        public async Task<Sport?> UpdateAsync(Guid id, SportUpdateDto updateDto)
        {
            var updatedSport = updateDto.ToSportFromUpdate();

            var update = await _sprotRepository.UpdateAsync(id, updatedSport, updateDto.ConcurrencyToken);

            return update;
        }
    }
}
