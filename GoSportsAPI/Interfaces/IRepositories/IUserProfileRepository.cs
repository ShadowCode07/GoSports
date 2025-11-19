using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Users;

namespace GoSportsAPI.Interfaces.IRepositories
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        /// <summary>
        /// Gets the profile with all related details (User, Lobbies, etc.).
        /// </summary>
        Task<UserProfile?> GetWithDetailsAsync(Guid profileId);

        /// <summary>
        /// Gets a profile linked to a specific AppUser.
        /// Useful when user logs in and you need their profile.
        /// </summary>
        Task<UserProfile?> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Returns profiles filtered and sorted using query object.
        /// (Optional – depends on your needs)
        /// </summary>
        Task<List<UserProfile>> GetAllAsync(UserProfileQueryObject queryObject);
    }
}
