using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Locations;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Locations
{
    /// <summary>
    /// Represents the data transfer object used to create a new location.
    /// </summary>
    public class LocationCreateDto
    {
        /// <summary>
        /// The name of the location.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 3 and 20 characters long.
        /// </remarks>
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the location.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 20 and 280 characters long.
        /// </remarks>
        [Required]
        [MinLength(20, ErrorMessage = "Description must be at least 20 characters long")]
        [MaxLength(280, ErrorMessage = "Description can't be longer than 280 characters")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The maximum number of lobbies allowed for the location.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 2 and 10.
        /// </remarks>
        [Required]
        [Range(2, 10, ErrorMessage = "The number of players must be between 2 and 10.")]
        public int MaxLobbyCount { get; set; }

        /// <summary>
        /// The location type associated with this location.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public LocationTypeCreateDto LocationType { get; set; }

        /// <summary>
        /// The list of sports available at the location.
        /// </summary>
        /// <remarks>
        /// This field is required and must contain at least one sport.
        /// </remarks>
        [Required]
        [MinLength(1, ErrorMessage = "You must add at least one sport.")]
        public List<string> Sports { get; set; } = new();

        /// <summary>
        /// The latitude of the location
        /// </summary>
        /// <remarks>
        /// This field is required and must be between -90 and 90.
        /// </remarks>
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of the location
        /// </summary>
        /// <remarks>
        /// This field is required and must be between -180 and 180.
        /// </remarks>
        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }

}
