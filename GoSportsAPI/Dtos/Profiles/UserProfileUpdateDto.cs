using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Profiles
{
    public class UserProfileUpdateDto
    {
        [Required]
        public List<string> Sports { get; set; } = [];

        [Required]
        public string ConcurrencyToken { get; set; } = string.Empty;
    }
}
