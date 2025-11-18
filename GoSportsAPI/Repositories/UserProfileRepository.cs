using GoSportsAPI.Data;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Models.Users;

namespace GoSportsAPI.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
