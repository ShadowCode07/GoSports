using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing locations.
    /// <summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting locations.
    /// </remarks>
    //[Authorize]
    [ApiController]
    [Route("api/location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Retrieves a collection of locations based on the specified query parameters.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting locations.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="LocationResponseDto"/> objects.
        /// </returns>
        /// <summary>
        /// Gets all locations based on filtering and sorting options.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] LocationQueryObject queryObject)
        {
            var locations = await _locationService.GetAllAsync(queryObject);
            return Ok(locations);
        }

        /// <summary>
        /// Gets a location by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location is null)
                return NotFound();

            return Ok(location);
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _locationService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing location.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LocationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _locationService.UpdateAsync(id, dto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a location by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _locationService.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            var deleted = await _locationService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Could not delete location.");

            return NoContent();
        }
    }
}