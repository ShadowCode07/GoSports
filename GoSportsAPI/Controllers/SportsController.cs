using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IRepositories;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing sports.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting sports.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase

    {
        private readonly ISportRepository _repository;


        public SportsController(ISportRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a collection of sports based on the specified query parameters.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting sports.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="SportResponceDto"/> objects.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportResponceDto>>> GetLocations([FromQuery] SportQueryObject queryObject)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sports = await _repository.GetAllAsync(queryObject);

            var sportsDto = sports.Select(l => l.ToSportResponceDto());

            return Ok(sportsDto);
        }

        /// <summary>
        /// Retrieves a sport with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sport to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing the <see cref="SportResponceDto"/> if found; otherwise, a not found result.
        /// </returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SportResponceDto>> GetSport([FromRoute] Guid id)

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

            return Ok(location.ToSportResponceDto());
        }

        /// <summary>
        /// Creates a new sport.
        /// </summary>
        /// <param name="createDto">The data transfer object containing the details of the sport to create.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the result of the create operation.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateSport([FromBody] SportCreateDto createDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sportModel = createDto.ToSportFromCreate();

            await _repository.CreateAsync(sportModel);

            return CreatedAtAction(
                nameof(GetSport),
                new { id = sportModel.SportId },
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
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSport([FromRoute] Guid id, [FromBody] SportUpdateDto updateDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sportModel = await _repository.GetByIdAsync(id);

            if (sportModel == null)
            {
                return NotFound();
            }

            sportModel = await _repository.UpdateAsync(id, updateDto);

            return Ok(sportModel.ToSportResponceDto());
        }

        /// <summary>
        /// Deletes a sport with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sport to delete.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSport([FromRoute] Guid id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sport = await _repository.GetByIdAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
