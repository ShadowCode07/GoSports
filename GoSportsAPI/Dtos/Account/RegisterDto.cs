using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public List<string> Sports { get; set; } = [];
    }
}
