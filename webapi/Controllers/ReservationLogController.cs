using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationLogController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public ReservationLogController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/ReservationLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationLogEntity>>> GetReservationsLog()
        {
          if (_context.ReservationsLog == null)
          {
              return NotFound();
          }
            return await _context.ReservationsLog.ToListAsync();
        }

        // GET: api/ReservationLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationLogEntity>> GetReservationLogEntity(int id)
        {
          if (_context.ReservationsLog == null)
          {
              return NotFound();
          }
            var reservationLogEntity = await _context.ReservationsLog.FindAsync(id);

            if (reservationLogEntity == null)
            {
                return NotFound();
            }

            return reservationLogEntity;
        }

        // PUT: api/ReservationLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationLogEntity(int id, ReservationLogEntity reservationLogEntity)
        {
            if (id != reservationLogEntity.ReservationLogId)
            {
                return BadRequest();
            }

            _context.Entry(reservationLogEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationLogEntityExists(id))
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

        // POST: api/ReservationLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservationLogEntity>> PostReservationLogEntity(ReservationLogEntity reservationLogEntity)
        {
          if (_context.ReservationsLog == null)
          {
              return Problem("Entity set 'RestoAppContext.ReservationsLog'  is null.");
          }
            _context.ReservationsLog.Add(reservationLogEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationLogEntity", new { id = reservationLogEntity.ReservationLogId }, reservationLogEntity);
        }

        // DELETE: api/ReservationLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationLogEntity(int id)
        {
            if (_context.ReservationsLog == null)
            {
                return NotFound();
            }
            var reservationLogEntity = await _context.ReservationsLog.FindAsync(id);
            if (reservationLogEntity == null)
            {
                return NotFound();
            }

            _context.ReservationsLog.Remove(reservationLogEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationLogEntityExists(int id)
        {
            return (_context.ReservationsLog?.Any(e => e.ReservationLogId == id)).GetValueOrDefault();
        }
    }
}
