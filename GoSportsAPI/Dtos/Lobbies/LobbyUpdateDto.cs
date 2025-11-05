using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Lobbies
{
    /// <summary>
    /// Represents the data transfer object used to update a lobby.
    /// </summary>
    public class LobbyUpdateDto
    {
        /// <summary>
        /// The name of the lobby.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 3 and 14 characters long.
        /// </remarks>
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(14, ErrorMessage = "Name can't be longer than 14 characters")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The name of the sport associated with the lobby.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public string SportName { get; set; } = string.Empty;

        [Required]
        public string Version { get; set; }
    }

}
