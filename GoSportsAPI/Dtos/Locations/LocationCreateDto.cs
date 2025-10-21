using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Mdels.Locations;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Locations
{
    public class LocationCreateDto
    {
        [Required]
        [MinLength (4, ErrorMessage = "Name must be at least 4 characters long")]
        [MaxLength (20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(20, ErrorMessage = "Description must be at least 20 characters long")]
        [MaxLength(280, ErrorMessage = "Description can't be longer than 280 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(2, 10, ErrorMessage = "The number of players must be between 2 and 10.")]
        public int MaxLobbyCount { get; set; }
        [Required]
        public LocationTypeCreateDto LocationType { get; set; }
    }
}
