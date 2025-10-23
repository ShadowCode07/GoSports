using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Dtos.LocationTypes;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing location types.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting location types.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTypesController : ControllerBase

    {
        private readonly ILocationTypeRepository _repository;

        public LocationTypesController(ILocationTypeRepository locationTypeRepository)
        {
            _repository = locationTypeRepository;
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

            var locationTypes = await _repository.GetAllAsync(queryObject);

            var locationTypesDto = locationTypes.Select(l => l.ToLocationTypeResponceDto());

            return Ok(locationTypesDto);
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

            var location = await _repository.GetByIdAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location.ToLocationTypeResponceDto());
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
