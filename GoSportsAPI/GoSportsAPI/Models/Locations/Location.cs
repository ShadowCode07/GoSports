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
        public int CurrentLobbyCount { get; set; }
        public int MaxLobbyCount { get; set; }


        private Location()
        {
            
        }

        public Location(string name, string description, LocationType type)
        {
            Name = name;
            Description = description;
            LocationType = type;
        }
    }
}
