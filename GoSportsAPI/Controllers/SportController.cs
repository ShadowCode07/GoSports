using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing sports.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting sports.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/sport")]
    public class SportController : ControllerBase
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        /// <summary>
        /// Retrieves a collection of sports based on the specified query parameters.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting sports.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="SportResponseDto"/> objects.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportResponseDto>>> GetSports([FromQuery] SportQueryObject queryObject)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sports = await _sportService.GetAllAsync(queryObject);

            return Ok(sports);
        }

        /// <summary>
        /// Retrieves a sport with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sport to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing the <see cref="SportResponseDto"/> if found; otherwise, a not found result.
        /// </returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SportResponseDto>> GetSport([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sport = await _sportService.GetByIdAsync(id);

            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }

        /// <summary>
        /// Creates a new sport.
        /// </summary>
        /// <param name="createDto">The data transfer object containing the details of the sport to create.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the result of the create operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSport([FromBody] SportCreateDto createDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sportModel = createDto.ToSportFromCreate();

            await _sportService.CreateAsync(sportModel);

            return CreatedAtAction(
                nameof(GetSport),
                new { id = sportModel.Id },
                sportModel.ToSportResponceDto());
        }

        /// <summary>
        /// Updates an existing sport with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sport to update.</param>
        /// <param name="updateDto">The data transfer object containing the updated sport information.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSport([FromRoute] Guid id, [FromBody] SportUpdateDto updateDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sportModel = await _sportService.GetByIdAsync(id);

            if (sportModel == null)
            {
                return NotFound();
            }

            sportModel = await _sportService.UpdateAsync(id, updateDto);

            return Ok(sportModel.ToSportResponceDto());
        }

        /// <summary>
        /// Deletes a sport with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sport to delete.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSport([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sport = await _sportService.GetByIdAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            await _sportService.DeleteAsync(id);

            return NoContent();
        }
    }
}
