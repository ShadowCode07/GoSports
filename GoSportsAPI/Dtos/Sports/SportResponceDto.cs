using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Sports
{
    /// <summary>
    /// Represents the data transfer object used to return sport information.
    /// </summary>
    public class SportResponceDto
    {
        /// <summary>
        /// The unique identifier of the sport.
        /// </summary>
        public Guid SportId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the sport.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public string Version { get; set; }
    }

}
