using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Dtos;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public RestaurantController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
            List<RestaurantEntity> restaurantEntity = await _context.Restaurants.ToListAsync();
            List<RestaurantDto> restaurantDtos = new List<RestaurantDto>();
            foreach(RestaurantEntity restoran in restaurantEntity)
            {
                RestaurantDto restaurantDto = new RestaurantDto
                {
                    RestaurantName = restoran.RestaurantName,
                    Address = restoran.Address,
                    Capacity = restoran.Capacity,
                    Email = restoran.Email,
                    Phone = restoran.Phone,
                    Speciallity = restoran.Speciallity,
                };
                restaurantDtos.Add(restaurantDto);
            }
            
            return restaurantDtos;
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantEntity>> GetRestaurantEntity(int id)
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
            var restaurantEntity = await _context.Restaurants.FindAsync(id);

            if (restaurantEntity == null)
            {
                return NotFound();
            }

            return restaurantEntity;
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantEntity(int id, RestaurantEntity restaurantEntity)
        {
            if (id != restaurantEntity.RestaurantId)
            {
                return BadRequest();
            }

            _context.Entry(restaurantEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantEntityExists(id))
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

        // POST: api/Restaurant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurantEntity(RestaurantDto restaurantDto)
        {
          if (_context.Restaurants == null)
          {
              return Problem("Entity set 'RestoAppContext.Restaurants'  is null.");
          }
            RestaurantEntity restaurantEntity = new RestaurantEntity
            {
                Address = restaurantDto.Address,
                Capacity = restaurantDto.Capacity,
                Email = restaurantDto.Email,
                Phone = restaurantDto.Phone,
                RestaurantName = restaurantDto.RestaurantName,
                Speciallity = restaurantDto.Speciallity,
            };
            _context.Restaurants.Add(restaurantEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantEntity", new { id = restaurantEntity.RestaurantId }, restaurantDto);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantEntity(int id)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }
            var restaurantEntity = await _context.Restaurants.FindAsync(id);
            if (restaurantEntity == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurantEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantEntityExists(int id)
        {
            return (_context.Restaurants?.Any(e => e.RestaurantId == id)).GetValueOrDefault();
        }
    }
}
