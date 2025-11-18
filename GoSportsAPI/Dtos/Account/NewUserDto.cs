    using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Users;

namespace GoSportsAPI.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfile Profile { get; set; }
        public string Token { get; set; }
    }
}
