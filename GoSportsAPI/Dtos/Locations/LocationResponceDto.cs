using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Dtos.Locations
{
    /// <summary>
    /// Represents the data transfer object used to return location information.
    /// </summary>
    public class LocationResponceDto
    {
        /// <summary>
        /// The unique identifier of the location.
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// The name of the location.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the location.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The type of the location.
        /// </summary>
        public LocationType LocationType { get; set; } = null!;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>
        /// The collection of lobbies associated with the location.
        /// </summary>
        public ICollection<LobbyResponceDto> Lobbies { get; set; }

        /// <summary>
        /// The collection of sports available at the location.
        /// </summary>
        public ICollection<SportResponceDto> Sports { get; set; }

        /// <summary>
        /// The current number of active lobbies at the location.
        /// </summary>
        public int CurrentLobbyCount { get; set; }

        /// <summary>
        /// The maximum number of lobbies allowed at the location.
        /// </summary>
        public int MaxLobbyCount { get; set; }
    }

}
