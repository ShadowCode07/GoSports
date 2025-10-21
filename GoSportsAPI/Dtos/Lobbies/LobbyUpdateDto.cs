using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Lobbies
{
    public class LobbyUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(14, ErrorMessage = "Name can't be longer than 14 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid SportId { get; set; }
    }
}
