using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoSportsAPI.Data;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTypesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LocationTypesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/LocationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationType>>> GetlocationType()
        {
            return await _context.locationType.ToListAsync();
        }

        // GET: api/LocationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationType>> GetLocationType(Guid id)
        {
            var locationType = await _context.locationType.FindAsync(id);

            if (locationType == null)
            {
                return NotFound();
            }

            return locationType;
        }

        // PUT: api/LocationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationType(Guid id, LocationType locationType)
        {
            if (id != locationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LocationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationType>> PostLocationType(LocationType locationType)
        {
            _context.locationType.Add(locationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationType", new { id = locationType.Id }, locationType);
        }

        // DELETE: api/LocationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationType(Guid id)
        {
            var locationType = await _context.locationType.FindAsync(id);
            if (locationType == null)
            {
                return NotFound();
            }

            _context.locationType.Remove(locationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationTypeExists(Guid id)
        {
            return _context.locationType.Any(e => e.Id == id);
        }
    }
}
