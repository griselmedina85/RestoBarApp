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
    public class ProfileController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public ProfileController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles()
        {
          if (_context.Profiles == null)
          {
              return NotFound();
          }
            var perfiles = await _context.Profiles.ToArrayAsync();
            List<ProfileDto> profileDtos = new List<ProfileDto>();
            foreach (var perfil in perfiles)
            {
                ProfileDto profileDto = new ProfileDto()
                {
                    ProfileName = perfil.ProfileName,
                };
               profileDtos.Add(profileDto);
            }

            return profileDtos;
          
        }

        // GET: api/Profile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetProfileEntity(int id)
        {

          if (_context.Profiles == null)
          {
              return NotFound();
          }
            var profileEntity = await _context.Profiles.FindAsync(id);

            if (profileEntity == null)
            {
                return NotFound();
            }
            ProfileDto profileDto = new ProfileDto() 
            { 
                ProfileName=profileEntity.ProfileName,
            };
            return profileDto;
        }

        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfileEntity(int id, ProfileEntity profileEntity)
        {
            if (id != profileEntity.ProfileId)
            {
                return BadRequest();
            }

            _context.Entry(profileEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileEntityExists(id))
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

        // POST: api/Profile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfileDto>> PostProfileEntity(ProfileDto profileDto)
        {
          if (_context.Profiles == null)
          {
              return Problem("Entity set 'RestoAppContext.Profiles'  is null.");
          }
            ProfileEntity profile = new ProfileEntity()
            {
                ProfileName = profileDto.ProfileName,
            };
            
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileEntity", new { id = profile.ProfileId }, profileDto);
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileEntity(int id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profileEntity = await _context.Profiles.FindAsync(id);
            if (profileEntity == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profileEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileEntityExists(int id)
        {
            return (_context.Profiles?.Any(e => e.ProfileId == id)).GetValueOrDefault();
        }
    }
}
