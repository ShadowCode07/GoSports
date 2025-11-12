using GoSportsAPI.Models.Sports;
using Microsoft.AspNetCore.Identity;

namespace GoSportsAPI.Models.Users
{
    public class AppUser : IdentityUser<Guid>
    {
        public UserProfile Profile { get; set; } = default!;
    }
}
