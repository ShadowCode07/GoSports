using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Dtos.Locations
{
    public class LocationRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;
        public int MaxLobbyCount { get; set; }
    }
}
