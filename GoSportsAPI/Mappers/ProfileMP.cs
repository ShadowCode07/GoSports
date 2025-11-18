using GoSportsAPI.Dtos.Profiles;
using GoSportsAPI.Models.Users;
using Mapster;

namespace GoSportsAPI.Mappers
{
    public static class ProfileMP
    {
        public static UserProfileResponseDto ToProfileResponceDto(this UserProfile profileModel)
        {
            var profileResponceDto = profileModel.Adapt<UserProfileResponseDto>();

            return profileResponceDto;
        }
        public static UserProfile ToProfileFromCreate(this UserProfileCreateDto profileDto)
        {
            var profile = profileDto.Adapt<UserProfile>();

            return profile;
        }
        public static UserProfile ToProfileFromUpdate(this UserProfileUpdateDto profileDto)
        {
            var profile = profileDto.Adapt<UserProfile>();

            return profile;
        }
    }
}
