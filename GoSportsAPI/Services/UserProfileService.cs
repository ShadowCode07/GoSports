using GoSportsAPI.Dtos.Profiles;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using GoSportsAPI.Repositories;
using Mapster;

namespace GoSportsAPI.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ISportRepository _sportRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository, ISportRepository sportRepository)
        {
            _userProfileRepository = userProfileRepository;
            _sportRepository = sportRepository;
        }

        public async Task<UserProfileResponseDto?> GetByIdAsync(Guid id)
        {
            var profile = await _userProfileRepository.GetWithDetailsAsync(id);
            return profile?.Adapt<UserProfileResponseDto>();
        }

        public async Task<UserProfileResponseDto?> GetByUserIdAsync(Guid userId)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);
            return profile?.Adapt<UserProfileResponseDto>();
        }

        public async Task<UserProfileResponseDto> CreateAsync(UserProfileCreateDto dto)
        {
            var profile = new UserProfile();

            profile.Sports = await ResolveSports(dto.Sports);

            await _userProfileRepository.CreateAsync(profile);
            await _userProfileRepository.SaveChanges();

            return profile.Adapt<UserProfileResponseDto>();
        }

        public async Task<UserProfileResponseDto?> UpdateAsync(Guid id, UserProfileUpdateDto dto)
        {
            var profile = await _userProfileRepository.GetWithDetailsAsync(id);
            if (profile is null)
                return null;

            profile.Sports = await ResolveSports(dto.Sports);

            await _userProfileRepository.SaveChanges();

            return profile.Adapt<UserProfileResponseDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _userProfileRepository.DeleteByIdAsync(id);
            if (!deleted)
                return false;

            await _userProfileRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Converts a list of sport names to Sport entities.
        /// </summary>
        private async Task<List<Sport>> ResolveSports(IEnumerable<string>? sportNames)
        {
            var result = new List<Sport>();

            if (sportNames is null)
                return result;

            foreach (var name in sportNames.Where(n => !string.IsNullOrWhiteSpace(n)))
            {
                var sport = await _sportRepository.GetByNameAsync(name.Trim());
                if (sport != null && !result.Any(s => s.Id == sport.Id))
                    result.Add(sport);
            }

            return result;
        }
    }
}
