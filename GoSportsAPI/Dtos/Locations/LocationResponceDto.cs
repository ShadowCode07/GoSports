using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Dtos.Locations
{
    public class LocationResponceDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;
        public ICollection<LobbyResponceDto> Lobbies { get; set; }
        public int CurrentLobbyCount { get; set; }
        public int MaxLobbyCount { get; set; }
    }
}
