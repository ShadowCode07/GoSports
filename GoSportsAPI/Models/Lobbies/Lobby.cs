using GoSportsAPI.Mdels.Locations;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Mdels.Lobbies
{
    public class Lobby
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public Guid SportId { get; set; }
        public Sport Sport { get; set; } = null!;
    }
}
