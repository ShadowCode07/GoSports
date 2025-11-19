using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace GoSportsAPI.Repositories
{
    /// <summary>
    /// Provides repository operations for <see cref="Sport"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD functionality from <see cref="Repository{T}"/> 
    /// and implements additional methods defined in <see cref="ISportRepository"/>.
    /// </remarks>
    public class SportRepository : Repository<Sport>, ISportRepository
    {
        public SportRepository(ApplicationDBContext context) : base(context)
        {
        }


        /// <summary>Gets all sports.</summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>List&lt;Sport&gt;</returns>
        public async Task<List<Sport>> GetAllAsync(SportQueryObject queryObject)
        {
            IQueryable<Sport> sports = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.SportName))
            {
                var name = queryObject.SportName.Trim().ToLower();
                sports = sports.Where(s => s.Name.ToLower().Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy.Trim().ToLower())
                {
                    case "name":
                    case "sportname":
                        sports = queryObject.IsDescending
                            ? sports.OrderByDescending(s => s.Name)
                            : sports.OrderBy(s => s.Name);
                        break;
                }
            }

            return await sports.ToListAsync();
        }

        /// <inheritdoc/>
        public Task<Sport?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult<Sport?>(null);
            }

            return _dbSet.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
