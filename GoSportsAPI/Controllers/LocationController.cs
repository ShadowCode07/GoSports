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
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting locations.
    /// </remarks>
    [Authorize]
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
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="LocationResponceDto"/> objects.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationResponceDto>>> GetLocations([FromQuery] LocationQueryObject queryObject)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locations = await _locationService.GetAllAsync(queryObject);

            return Ok(locations);
        }

        /// <summary>
        /// Retrieves a location with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the location to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing the <see cref="LocationResponceDto"/> if found; otherwise, a not found result.
        /// </returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LocationResponceDto>> GetLocation([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _locationService.GetByIdAsync(id);

            if (location == null)
            { 
                return NotFound();
            }

            return Ok(location.ToLocationResponceDto());
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="createDto">The data transfer object containing the details of the location to create.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the result of the create operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDto createDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationModel = createDto.ToLocationFromCreate();

            await _locationService.CreateAsync(locationModel, createDto.Sports);

            return CreatedAtAction(
                nameof(GetLocation),
                new { id = locationModel.LocationId },
                locationModel.ToLocationResponceDto());
        }

        /// <summary>
        /// Updates an existing location with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the location to update.</param>
        /// <param name="updateDto">The data transfer object containing the updated location information.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] Guid id, [FromBody] LocationUpdateDto updateDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationModel = await _locationService.GetByIdAsync(id);
            
            if(locationModel == null)
            {
                return NotFound();
            }

            locationModel = await _locationService.UpdateAsync(id, updateDto);

            return Ok(locationModel.ToLocationResponceDto());
        }

        /// <summary>
        /// Deletes a location with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the location to delete.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            await _locationService.DeleteAsync(id);

            return NoContent();
        }
    }
}
