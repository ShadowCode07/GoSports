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
        /// Gets all sports based on filtering settings.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] SportQueryObject queryObject)
        {
            var sports = await _sportService.GetAllAsync(queryObject);
            return Ok(sports);
        }

        /// <summary>
        /// Gets a sport by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sport = await _sportService.GetByIdAsync(id);

            return sport is null ? NotFound() : Ok(sport);
        }

        /// <summary>
        /// Creates a new sport.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SportCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _sportService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing sport.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SportUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _sportService.UpdateAsync(id, dto);
            
            if (updated is null)
            {
                return NotFound();
            }


            return Ok(updated);
        }

        /// <summary>
        /// Deletes a sport by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _sportService.GetByIdAsync(id);

            if (exists is null)
            {
                return NotFound();
            }

            var deleted = await _sportService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest("Could not delete sport.");
            }


            return NoContent();
        }
    }
}
