using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;


        public LocationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var locations = await _context.Location.ToListAsync();
            locations.Select(l => l.ToLocationResponceDto());

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(Guid id)
        {
            var location = await _context.Location.FindAsync(id);

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

            _context.Add(locationModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = locationModel.Id }, locationModel.ToLocationResponceDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Location>> UpdateLocation([FromRoute] Guid id, [FromBody] LocationUpdateDto updateDto)
        {
            var locationModel = _context.Location.FirstOrDefault(x => x.Id == id);
            
            if(locationModel == null)
            {
                return NotFound();
            }

            locationModel = updateDto.ToLocationFromUpdate();

            await _context.SaveChangesAsync();

            return Ok(locationModel.ToLocationResponceDto());
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
