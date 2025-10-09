using GoSportsAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTypesController : ControllerBase
    {
        private readonly ILocationTypeRepository _repository;

        public LocationTypesController(ILocationTypeRepository locationTypeRepository)
        {
            _repository = locationTypeRepository;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetLocationTypes()
        {
            var locations = await _repository.GetAllAsync();

            var locationDto = locations.Select(l => l.ToLocationResponceDto());

            return Ok(locationDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] Guid id)
        {
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
            var locationModel = createDto.ToLocationFromCreate();

            await _repository.CreateAsync(locationModel);

            return CreatedAtAction(nameof(GetLocation), new { id = locationModel.Id }, locationModel.ToLocationResponceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] Guid id, [FromBody] LocationUpdateDto updateDto)
        {
            var locationModel = await _repository.GetByIdAsync(id);

            if (locationModel == null)
            {
                return NotFound();
            }

            locationModel = await _repository.UpdateAsync(id, updateDto);

            return Ok(locationModel.ToLocationResponceDto());
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationType([FromRoute] Guid id)
        {
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
