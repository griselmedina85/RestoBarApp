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
    public class ReservationController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public ReservationController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Reservation
        [HttpGet]
        public ActionResult<IEnumerable<ReservationResponsDto>> GetReservations()
        {
            //devolver una lista de ReservationResponsDto que es reservaciones+cliente+mesa
            //obtengo las reservas
            //recorro reservas y para cada registro busco su mesa y cliente
            //agrego a la lista ReservationResultDto
            var reservation = _context.Reservations
                            .Include(c =>c.Client)
                                .ThenInclude(p=>p.Person)
                            .Include(t => t.Table)
                            .ThenInclude(r => r.Restaurant)
                            .ToList();

            var reservationDtos = reservation.Select(r => new ReservationResponsDto
            {
               ReservationId = r.ReservationId,
               NumberDiners = r.NumberDiners,
               Date = r.Date,
               Time = r.Time,
               Attended = r.Attended,
               FinishedMeal = r.FinishedMeal,
               Cancelation = r.Cancelation,
               ReasonCancelation = r.ReasonCancelation,
               PersonLastName = r.Client?.Person?.PersonLastName,
               PersonName = r.Client?.Person?.PersonName,
               Capacity = r.Table.Capacity,
               TableDescription = r.Table.TableDescription,
               
            }).ToList();

            return  Ok(reservationDtos);
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationResponsDto>> GetReservationEntity(int id)
        {
          if (_context.Reservations == null)
          {
              return NotFound();
          }
          var reservationEntity = await _context.Reservations
                                        .Include(c => c.Client)
                                        .Include(t => t.Table)
                                        .Include(r => r.restaurant)
                                        .FirstOrDefaultAsync();

            if (reservationEntity == null)
            {
                return NotFound();
            }
            ReservationResponsDto responsDto = new ReservationResponsDto
            {
                PersonLastName = reservationEntity.Client?.Person?.PersonLastName,
                PersonName = reservationEntity.Client?.Person?.PersonName,
                Capacity = reservationEntity.Table.Capacity,
                NumberDiners = reservationEntity.NumberDiners,
                Date = reservationEntity.Date,
                Attended = reservationEntity.Attended,
                Cancelation = reservationEntity.Cancelation,
                FinishedMeal = reservationEntity.FinishedMeal,
                ReasonCancelation = reservationEntity.ReasonCancelation,
                TableDescription = reservationEntity.Table.TableDescription,
                Time = reservationEntity.Time,
            };
                return responsDto;
        }
        // PUT: api/Reservation/5
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> PutReservationEntity(int id, ReservationDto reservationDto)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            reservation.NumberDiners = reservationDto.NumberDiners;
            reservation.Date = reservationDto.Date;
            reservation.Attended = reservationDto.Attended;
            reservation.ClientId = reservationDto.ClientId;
            reservation.TableId = reservationDto.TableId;
            reservation.Cancelation = reservationDto.Cancelation;
            reservation.FinishedMeal = reservationDto.FinishedMeal;
            reservation.ReasonCancelation = reservationDto.ReasonCancelation;
            reservation.RestaurantId = reservationDto.RestaurantId;

            // _context.Entry(ReservationEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationEntity", new { id = reservation.ReservationId }, reservationDto);
        }

        
        // PUT: api/Reservation/attended/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("attended/{id}")]
        public async Task<ActionResult<ReservationEntity>> AttendedReservationEntity(int id)
        {
            //primero busco registro ReservationEntity
            //Si existe modificamos el campo attended a true
            
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.ReservationId==id);
            if (reservation==null)
            {
                return NotFound();
            }
            if(reservation.Attended==false && reservation.Cancelation == false)
            {
                reservation.Attended = true;

            }             
            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationEntity", new { id = reservation.ReservationId }, reservation);
        }
        /***************************************************************/
        // PUT: api/Reservation/cancel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<ReservationEntity>> CancelReservationEntity(int id, string reason)
        {
            //primero busco registro ReservationEntity
            //Si existe modificamos el campo attended a true

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            if(reservation.Attended==false) //reservation.Date < DateTime.Now &&
			{
                reservation.Cancelation = true;
                reservation.ReasonCancelation = reason;
            }
            
            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationEntity", new { id = reservation.ReservationId }, reservation);
        }
        /***************************************************************/
        // PUT: api/Reservation/finish/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("finish/{id}")]
        public async Task<ActionResult<ReservationEntity>> FinishReservationEntity(int id)
        {
            //primero busco registro ReservationEntity
            //Si existe y attended == true y time mayor a ahora entonces finish = true

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            //if ((reservation.Date<=DateTime.Now)&&
            //    (DateTime.Parse(reservation.Time) <= (DateTime.Now)) && 
            if(reservation.Attended == true)
            {
                reservation.FinishedMeal = true;
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationEntity", new { id = reservation.ReservationId }, reservation);
        }
        /***************************************************************/
        /***************************************************************/
        // POST: api/Reservation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PostReservationEntity(ReservationDto reservationDto)
        {
            ReservationEntity reservation = new ReservationEntity()
            {
                NumberDiners = reservationDto.NumberDiners,
                Attended = false,
                Date = reservationDto.Date,
                Time = reservationDto.Time,
                FinishedMeal = false,
                Cancelation = false,
                ReasonCancelation = reservationDto.ReasonCancelation,
                ClientId = reservationDto.ClientId,
                TableId = reservationDto.TableId,
                RestaurantId = reservationDto.RestaurantId,

            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            ReservationEntity lastReservation =  _context.Reservations.OrderByDescending(r => r.ReservationId).FirstOrDefault();


            if (lastReservation == null)
            {
                return Problem("Entity set 'RestoAppContext.Reservations'  is null.");
            }

            return CreatedAtAction("GetReservationEntity", new { id = lastReservation }, reservationDto);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationEntity(int id)
        {
            if (_context.Reservations == null)
            {
                return NotFound();
            }
            var reservationEntity = await _context.Reservations.FindAsync(id);
            if (reservationEntity == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservationEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationEntityExists(int id)
        {
            return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
