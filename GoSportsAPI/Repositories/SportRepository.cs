using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace GoSportsAPI.Repositories
{
    public class SportRepository : Repository<Sport>, ISportRepository
    {
        public SportRepository(ApplicationDBContext context) : base(context)
        {
        }

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

        public async Task<Sport?> UpdateAsync(Guid id, SportUpdateDto dto)
        {
            var existingSport = await _dbSet.FindAsync(id);

            if(existingSport == null)
            {
                return null;
            }

            existingSport.Name = dto.Name;
            
            await _context.SaveChangesAsync();
            
            return existingSport;
        }
    }
}
