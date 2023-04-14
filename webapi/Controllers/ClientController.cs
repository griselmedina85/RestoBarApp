using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Models;
using webapi.Dtos;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public ClientController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            var cliente = _context.Clients.Include(t => t.Person).ToList();
            if(cliente == null)
            {
                return Problem("no se encontro registro");
            }
            var client = cliente.Select(p => new ClientDto
            {
                PersonName = p.Person.PersonName,
                PersonLastName = p.Person.PersonLastName,
                Phone = p.Person.Phone,
                Email = p.Person.Email,
                PersonId = p.PersonId,     
        });

            //muesto los datos cargador manualmente


            return Ok(client);
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public ClientDto GetClientEntity(int id)
        {
            //realizo la busqueda pero antes utilizo Include para cargar la relacion mediante la propiedad de navegacion
            var clientEntity = _context.Clients
                .Include(c=>c.Person)
                .FirstOrDefault(c => c.PersonId == id);

            if(clientEntity == null)
            {
                return null;
            }
            var cliente = new ClientDto
            {

                PersonName = clientEntity.Person.PersonName,
                PersonLastName = clientEntity.Person.PersonLastName,
                Phone = clientEntity.Person.Phone,
                Email = clientEntity.Person.Email,
            };

            return cliente;
           
            

        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientEntity(int id, ClientEntity clientEntity)
        {
            if (id != clientEntity.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(clientEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientEntityExists(id))
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

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientDto>> PostClientEntity(ClientDto clientDto)
        {
          
           ClientEntity persona = new ClientEntity()
            {
                PersonId = clientDto.PersonId,
            };
            _context.Clients.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientEntity", new { id = persona.PersonId }, clientDto);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientEntity(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var clientEntity = await _context.Clients.FindAsync(id);
            if (clientEntity == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clientEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientEntityExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
