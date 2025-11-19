using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing location types.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting location types.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/locationType")]
    public class LocationTypeController : ControllerBase
    {
        private readonly ILocationTypeService _locationTypeService;

        public LocationTypeController(ILocationTypeService locationTypeService)
        {
            _locationTypeService = locationTypeService;
        }

        /// <summary>
        /// Gets all location types based on filtering and sorting options.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] LocationTypeQueryObject queryObject)
        {
            var types = await _locationTypeService.GetLocationTypes(queryObject);
            return Ok(types);
        }

        /// <summary>
        /// Gets a specific location type by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var type = await _locationTypeService.GetLocationTypeById(id);
            if (type is null)
                return NotFound();

            return Ok(type);
        }

    }
}
