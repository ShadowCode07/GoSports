using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Profiles
{
    public class UserProfileCreateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<string> Sports { get; set; } = [];
    }
}
