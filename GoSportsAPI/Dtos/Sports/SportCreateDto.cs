using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Sports
{
    /// <summary>
    /// Represents the data transfer object used to create a new sport.
    /// </summary>
    public class SportCreateDto
    {
        /// <summary>
        /// The name of the sport.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 3 and 14 characters long.
        /// </remarks>
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(14, ErrorMessage = "Name can't be longer than 14 characters")]
        public string Name { get; set; } = string.Empty;
    }

}
