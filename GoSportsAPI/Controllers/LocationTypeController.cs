using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
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
        private readonly ILocationTypeService _LocationTypeService;

        public LocationTypeController(ILocationTypeService LocationTypeService)
        {
            _LocationTypeService = LocationTypeService;
        }

        /// <summary>
        /// Retrieves a collection of location types based on the specified query parameters.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting location types.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="LocationTypeResponceDto"/> objects.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationTypeResponceDto>>> GetLocationTypes([FromQuery] LocationTypeQueryObject queryObject)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationTypes = await _LocationTypeService.GetLocationTypes(queryObject);

            return Ok(locationTypes);
        }

        /// <summary>
        /// Retrieves a location type with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the location type to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing the <see cref="LocationTypeResponceDto"/> if found; otherwise, a not found result.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationTypeResponceDto>> GetLocationType([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _LocationTypeService.GetLocationTypeById(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _repository.GetByIdAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }*/
    }
}
