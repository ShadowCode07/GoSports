using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models.Lobbies
{
    public class Lobby
    {
        public Guid LobbyId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public Guid SportId { get; set; }
        public Sport Sport { get; set; } = null!;

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
