using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Models.Locations;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ILocationTypeService
    {
        Task<IEnumerable<LocationTypeResponseDto>> GetLocationTypes(LocationTypeQueryObject queryObject);
        Task<LocationTypeResponseDto?> GetLocationTypeById(Guid id);
    }
}
