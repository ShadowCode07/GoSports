using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models.Lobbies
{
    public class Lobby
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public Guid SportId { get; set; }
        public Sport Sport { get; set; } = null!;

        public string Code { get; set; } = string.Empty;
        public int CurrentPlayerCount { get; set; } = 0;
        public int MaxPlayerCount { get; set; }
        public ICollection<UserProfile>? Users { get; set; } = new List<UserProfile>();

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
