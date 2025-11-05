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
            var sports = _context.sports
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.SportName))
            {
                sports = sports.Where(l => l.Name.Contains(queryObject.SportName));
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    sports = queryObject.IsDescending ? sports.OrderByDescending(l => l.Name) : sports.OrderBy(l => l.Name);
                }
            }


            return await sports.ToListAsync();
        }


        /// <summary>Updates a sport.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>Sports</returns>
        public async Task<Sport?> UpdateAsync(Guid id, SportUpdateDto dto)
        {
            var update = await _dbSet
                .FirstOrDefaultAsync(s => s.SportId == id);

            if (update == null)
            {
                return null;
            }

            var locationVersionBytes = Convert.FromBase64String(dto.Version);
            _context.Entry(update).Property(l => l.Version).OriginalValue = locationVersionBytes;

            update.Name = dto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Lobby was modified elsewhere. Try again.");
            }

            return update;
        }
    }
}
