using GoSportsAPI.Data;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mdels.Locations;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Repositories
{
    public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
    {
        public LocationTypeRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<List<LocationType>> GetAllAsync(LocationTypeQueryObject queryObject)
        {
            var locationTypes = _context.locationTypes
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                locationTypes = locationTypes.Where(l => l.Name.Contains(queryObject.Name));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Surface))
            {
                locationTypes = locationTypes.Where(l => l.Name.Contains(queryObject.Surface));
            }

            if (queryObject.HasLights)
            {
                locationTypes = locationTypes.Where(l => l.HasLights == queryObject.HasLights);

            }

            if (queryObject.IsIndoor)
            {

                locationTypes = locationTypes.Where(l => l.HasLights == queryObject.IsIndoor);
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    locationTypes = queryObject.IsDescending ? locationTypes.OrderByDescending(l => l.Name) : locationTypes.OrderBy(l => l.Name);
                }
            }

            return await locationTypes.ToListAsync();
        }
    }
}
