using GoSportsAPI.Data;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<UserProfile?> GetWithDetailsAsync(Guid profileId)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.LobbyId)
                .FirstOrDefaultAsync(p => p.Id == profileId);
        }

        /// <inheritdoc/>
        public Task<UserProfile?> GetByUserIdAsync(Guid userId)
        {
            return _dbSet
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <inheritdoc/>
        public async Task<List<UserProfile>> GetAllAsync(UserProfileQueryObject queryObject)
        {
            IQueryable<UserProfile> query = _dbSet
                .Include(p => p.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.UserName))
            {
                var name = queryObject.UserName.Trim().ToLower();
                query = query.Where(p => p.User.UserName.ToLower().Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy.Trim().ToLower())
                {
                    case "username":
                        query = queryObject.IsDescending
                            ? query.OrderByDescending(p => p.User.UserName)
                            : query.OrderBy(p => p.User.UserName);
                        break;
                }
            }

            return await query.ToListAsync();
        }
    }
}
