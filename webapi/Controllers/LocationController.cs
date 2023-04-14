using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Dtos;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public LocationController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
              if (_context.Locations == null)
              {
                  return NotFound();
              }
             List<LocationEntity> location = await _context.Locations.ToListAsync();
             List<LocationDto> locationDtos = new List<LocationDto>();
            foreach (LocationEntity locationEntity in location)
            {
                    LocationDto locationDto = new LocationDto
                    {
                        LocationDescription = locationEntity.LocationDescription,
                    };
                    locationDtos.Add(locationDto);
            }
             return locationDtos;
        }

        // GET: api/Location/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocationEntity(int id)
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            var locationEntity = await _context.Locations.FindAsync(id);

            if (locationEntity == null)
            {
                return NotFound("No hay registro con ese valor");
            }
            LocationDto locationDto = new LocationDto
            {
                LocationDescription = locationEntity.LocationDescription,
            };
            return locationDto;
        }

        // PUT: api/Location/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationEntity(int id, LocationEntity locationEntity)
        {
            if (id != locationEntity.LocationId)
            {
                return BadRequest();
            }

            _context.Entry(locationEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationEntityExists(id))
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

        // POST: api/Location
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationDto>> PostLocationEntity(LocationDto locationDto)
        {
          if (_context.Locations == null)
          {
              return Problem("Entity set 'RestoAppContext.Locations'  is null.");
          }

            LocationEntity location = new LocationEntity
            {
                LocationDescription = locationDto.LocationDescription,
            };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationEntity", new { id = location.LocationId }, locationDto);
        }

        // DELETE: api/Location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationEntity(int id)
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            var locationEntity = await _context.Locations.FindAsync(id);
            if (locationEntity == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(locationEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationEntityExists(int id)
        {
            return (_context.Locations?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
