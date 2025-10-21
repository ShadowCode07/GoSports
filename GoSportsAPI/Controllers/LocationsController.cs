using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _repository;


        public LocationsController(ILocationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocations([FromQuery] LocationQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locations = await _repository.GetAllAsync(queryObject);

            var locationDto = locations.Select(l => l.ToLocationResponceDto());

            return Ok(locationDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLocation([FromRoute] Guid id)
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

            return Ok(location.ToLocationResponceDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationModel = createDto.ToLocationFromCreate();

            await _repository.CreateAsync(locationModel);

            return CreatedAtAction(
                nameof(GetLocation),
                new { id = locationModel.Id },
                locationModel.ToLocationResponceDto());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] Guid id, [FromBody] LocationUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationModel = await _repository.GetByIdAsync(id);
            
            if(locationModel == null)
            {
                return NotFound();
            }

            locationModel = await _repository.UpdateAsync(id, updateDto);

            return Ok(locationModel.ToLocationResponceDto());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] Guid id)
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
        }
    }
}
