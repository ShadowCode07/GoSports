using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Sports;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models.Users
{
    public class UserProfile : Base
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid? LobbyId { get; set; }
        public Lobby? Lobby { get; set; }
        public ICollection<Sport> Sports { get; set; } = new List<Sport>();
    }
}
