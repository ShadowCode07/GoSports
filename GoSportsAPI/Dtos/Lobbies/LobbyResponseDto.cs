using GoSportsAPI.Dtos.Sports;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Lobbies
{
    /// <summary>
    /// Represents the data transfer object used to return lobby information.
    /// </summary>
    public class LobbyResponseDto
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
        public Guid LocationName { get; set; }

        /// <summary>
        /// The sport associated with the lobby.
        /// </summary>
        public Guid SportId { get; set; }
        public string SportName { get; set; } = null!;

        public Guid HostProfileId { get; set; }
        public string HostUserName { get; set; } = string.Empty;
        public int CurrentPlayerCount { get; set; } = 0;
        public int MaxPlayerCount { get; set; }

        public string Code { get; set; } = string.Empty;

        public string ConcurrencyToken { get; set; } = string.Empty;
    }
}
