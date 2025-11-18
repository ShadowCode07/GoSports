using GoSportsAPI.Dtos.Sports;

namespace GoSportsAPI.Dtos.Profiles
{
    public class UserProfileResponseDto
    {
        public Guid Id { get; set; }
        public Guid? LobbyId { get; set; }
        public string? LobbyName { get; set; }
        public List<SportResponseDto>? Sports { get; set; } = [];
        public string ConcurrencyToken { get; set; } = string.Empty;
    }
}
