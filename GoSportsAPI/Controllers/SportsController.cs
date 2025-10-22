using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISportRepository _repository;


        public SportsController(ISportRepository repository)
        {
            _repository = repository;
        }

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
                new { id = sportModel.Id },
                sportModel.ToSportResponceDto());
        }

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
