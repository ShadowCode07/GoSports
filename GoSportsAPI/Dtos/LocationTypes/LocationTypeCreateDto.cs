using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.LocationTypes
{
    /// <summary>
    /// Represents the data transfer object used to create a new location type.
    /// </summary>
    public class LocationTypeCreateDto
    {
        /// <summary>
        /// The name of the location type.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 3 and 10 characters long.
        /// </remarks>
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(10, ErrorMessage = "Name can't be longer than 10 characters")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location type is indoors.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public bool IsIndoor { get; set; }

        /// <summary>
        /// The surface type of the location.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 4 and 12 characters long.
        /// </remarks>
        [Required]
        [MinLength(4, ErrorMessage = "Surface must be at least 4 characters long")]
        [MaxLength(12, ErrorMessage = "Surface can't be longer than 12 characters")]
        public string Surface { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location type has lighting available.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public bool HasLights { get; set; }
        public string Version { get; set; }
    }

}
