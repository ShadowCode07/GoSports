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
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _context.locations.ToListAsync();

            var locationDto = locations.Select(l => l.ToLocationResponceDto());

            return Ok(locationDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] Guid id)
        {
            var location = await _context.locations.FindAsync(id);

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

            await _context.AddAsync(locationModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = locationModel.Id }, locationModel.ToLocationResponceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] Guid id, [FromBody] LocationUpdateDto updateDto)
        {
            var locationModel = await _context.locations.FirstOrDefaultAsync(x => x.Id == id);
            
            if(locationModel == null)
            {
                return NotFound();
            }

            locationModel = updateDto.ToLocationFromUpdate();

            await _context.SaveChangesAsync();

            return Ok(locationModel.ToLocationResponceDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] Guid id)
        {
            var location = await _context.locations.FirstOrDefaultAsync(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _context.locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
