using GoSportsAPI.Data;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Repositories
{
    public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
    {
        public LocationTypeRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
