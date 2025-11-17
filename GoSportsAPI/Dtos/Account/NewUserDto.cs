using GoSportsAPI.Dtos.Sports;

namespace GoSportsAPI.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<SportResponceDto> Sports { get; set; }
        public string Token { get; set; }
    }
}
