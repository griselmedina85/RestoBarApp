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
    public class TableController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public TableController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Table
        [HttpGet]
        public ActionResult<IEnumerable<TableDto>> GetTables()
        {

            var tablaCompleta = _context.Tables
                    .Include(l => l.Location)
                    //.Include(r => r.Restaurant)
                    .ToList();

            var respuesta = tablaCompleta.Select(t => new TableDto
            {
                TableDescription = t.TableDescription,
                LocationDescription = t.Location.LocationDescription,
                Capacity = t.Capacity,
         
            });      
            
           
            return Ok(respuesta);
        }

        // GET: api/Table/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<TableDto>> GetTableEntity(int id)
        {
           
            var tableEntity =  _context.Tables
                        .Include(t => t.Location)
                        .FirstOrDefault(t=>t.TableId== id);
          

            if (tableEntity == null)
            {
                return NotFound();
            }
            var table = new TableDto()
            {
                Capacity = tableEntity.Capacity,
                LocationDescription = tableEntity.Location.LocationDescription,
                TableDescription = tableEntity.TableDescription,
               
            };
            return table;
        }

        // PUT: api/Table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTableEntity(int id, TableDto tableDto)
        {
            var tableResult = _context.Tables
                            .Include(t=>t.RestaurantId)
                            .Include(m=>m.LocationId)
                            .FirstOrDefaultAsync(t=>t.TableId==id);
            if (tableResult==null)
            {
                return BadRequest();
            }

            _context.Entry(tableResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableEntityExists(id))
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

        // POST: api/Table
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TableDto>> CreateTable(TableDto tableDto)
        {
            //Si no tengo restoranes cargaddos no deberia de poder guardar la mesa
            //por lo tanto antes debo buscar si el campo RestorantId existe en la bd.
            //para nuestro ejemplo solo tenemos 1
            int idRestoran = 1;
            var restoran = await _context.Restaurants.FindAsync(idRestoran);
            if(restoran == null)
            {
                return NotFound("El restaurante no existe");
            }
            //luego debe verificar que las distintas ubicaciones para las mesas ya existan 
            //sino deberian crearse previamente.
            var location = await _context.Locations.FindAsync(tableDto.LocationId);
            if(tableDto.LocationId== null)
            {
                return NotFound("La mesa no dispone de ubicacion");
            }
            //asignamos los valores del TableDto a TableEntity para guardar en la bd
            TableEntity mesa = new TableEntity()
            {
                TableDescription = tableDto.TableDescription,
                Capacity = tableDto.Capacity,
                LocationId = tableDto.LocationId,
                RestaurantId = restoran.RestaurantId,  
            };
            _context.Tables.Add(mesa);
            _context.SaveChangesAsync();
            //luego de guardar devolvemos los datos de la mesa creada
            return CreatedAtAction("GetTableEntity", new { id = mesa.TableId }, tableDto);
        }

        // DELETE: api/Table/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableEntity(int id)
        {
            if (_context.Tables == null)
            {
                return NotFound();
            }
            var tableEntity = await _context.Tables.FindAsync(id);
            if (tableEntity == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(tableEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TableEntityExists(int id)
        {
            return (_context.Tables?.Any(e => e.TableId == id)).GetValueOrDefault();
        }
    }
}
