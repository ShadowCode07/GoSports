using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Dtos.Lobbies
{
    public class LobbyResponceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
    }
}
