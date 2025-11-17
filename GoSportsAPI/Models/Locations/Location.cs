using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Sports;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models.Locations
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;

        // Coordinates
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Lobby properties
        public ICollection<Lobby> Lobbies { get; set; } = new List<Lobby>();
        public int CurrentLobbyCount { get; set; } = 0;
        public int MaxLobbyCount { get; set; }

        // Sport properties
        public ICollection<Sport> Sports { get; set; } = new List<Sport>();
    }
}
