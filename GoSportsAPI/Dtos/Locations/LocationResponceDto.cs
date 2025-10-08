using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Dtos.Locations
{
    public class LocationResponceDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;
        //public ICollection<Lobby> Lobbies { get; set; } = new List<Lobby>();
        public int CurrentLobbyCount { get; set; }
        public int MaxLobbyCount { get; set; }
    }
}
