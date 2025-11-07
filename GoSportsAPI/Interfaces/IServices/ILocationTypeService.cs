using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Locations;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationTypeService
    {
        Task<IEnumerable<LocationTypeResponceDto>> GetLocationTypes(LocationTypeQueryObject queryObject);
        Task<LocationTypeResponceDto> GetLocationTypeById(Guid id);
    }
}
