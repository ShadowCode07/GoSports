using GoSportsAPI.Dtos.Sports;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Lobbies
{
    /// <summary>
    /// Represents the data transfer object used to return lobby information.
    /// </summary>
    public class LobbyResponceDto
    {
        /// <summary>
        /// The unique identifier of the lobby.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the lobby.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The identifier of the location associated with this lobby.
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// The sport associated with the lobby.
        /// </summary>
        public SportResponceDto Sport { get; set; }

        public string Version { get; set; }
    }
}
