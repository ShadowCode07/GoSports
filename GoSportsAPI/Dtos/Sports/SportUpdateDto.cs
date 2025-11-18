using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Sports
{
    /// <summary>
    /// Represents the data transfer object used to update a sport.
    /// </summary>
    public class SportUpdateDto
    {
        /// <summary>
        /// The name of the sport.
        /// </summary>
        /// <remarks>
        /// Must be between 3 and 14 characters long.
        /// </remarks>
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(14, ErrorMessage = "Name can't be longer than 14 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ConcurrencyToken { get; set; } = string.Empty;
    }

}
