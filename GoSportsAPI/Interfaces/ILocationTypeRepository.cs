using GoSportsAPI.Helpers;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Interfaces
{
    public interface ILocationTypeRepository : IRepository<LocationType>
    {
        Task<List<LocationType>> GetAllAsync(LocationTypeQueryObject queryObject);
    }
}
