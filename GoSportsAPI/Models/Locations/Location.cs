using GoSportsAPI.Mdels.Lobbies;

namespace GoSportsAPI.Mdels.Locations
{
    public class Location
    {
        // Locations properties
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;

        // Lobby properties
        public ICollection<Lobby> Lobbies { get; set; } = new List<Lobby>();
        public int CurrentLobbyCount { get; set; } = 0;
        public int MaxLobbyCount { get; set; }
    }
}
