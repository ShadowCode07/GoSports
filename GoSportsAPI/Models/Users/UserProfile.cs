using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Models.Users
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; } = default!;

        public Guid? LobbyId { get; set; }
        public Lobby? Lobby { get; set; }
        public ICollection<Sport>? Sports { get; set; } = new List<Sport>();
    }
}
