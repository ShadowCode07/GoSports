using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.LocationTypes
{
    public class LocationTypeCreateDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long")]
        [MaxLength(10, ErrorMessage = "Name can't be longer than 10 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool IsIndoor { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Surface must be at least 4 characters long")]
        [MaxLength(12, ErrorMessage = "Surface can't be longer than 12 characters")]
        public string Surface { get; set; } = string.Empty;
        [Required]
        public bool HasLights { get; set; }
    }
}
