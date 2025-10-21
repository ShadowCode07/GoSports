using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
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

       [HttpGet]
        public async Task<IActionResult> GetLocationTypes([FromQuery] LocationTypeQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationTypes = await _repository.GetAllAsync(queryObject);

            var locationTypesDto = locationTypes.Select(l => l.ToLocationTypeResponceDto());

            return Ok(locationTypesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationType([FromRoute] Guid id)
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

        [HttpDelete("{id}")]
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
        }
    }
}
