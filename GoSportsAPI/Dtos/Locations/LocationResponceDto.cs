using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Mdels.Locations;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Dtos.Locations
{
    public class LocationResponceDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;
        public ICollection<LobbyResponceDto> Lobbies { get; set; }
        public ICollection<SportResponceDto> Sports { get; set; }
        public int CurrentLobbyCount { get; set; }
        public int MaxLobbyCount { get; set; }
    }
}
