using GoSportsAPI.Dtos.Profiles;

namespace GoSportsAPI.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserProfileResponseDto Profile { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
