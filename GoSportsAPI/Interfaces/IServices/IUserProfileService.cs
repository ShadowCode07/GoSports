using GoSportsAPI.Dtos.Profiles;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDto?> GetByIdAsync(Guid id);
        Task<UserProfileResponseDto?> GetByUserIdAsync(Guid userId);

        Task<UserProfileResponseDto> CreateAsync(UserProfileCreateDto dto);
        Task<UserProfileResponseDto?> UpdateAsync(Guid id, UserProfileUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
