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
    public class PersonController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public PersonController(RestoAppContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public List<PersonEntity> GetPersons()
        {
            return _context.Persons.ToList();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonEntity>> GetPersonEntity(int id)
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            var personEntity = await _context.Persons.FindAsync(id);

            if (personEntity == null)
            {
                return NotFound();
            }

            return personEntity;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonEntity(int id, PersonDto personDto)
        {

            //se concidera que el metodo es llamado desde una entidad en la cual previamente se cargaron todos los datos del dto
            //y en ese caso se verifica si es igual para hacer la actualizacion
            var persona = await _context.Persons.FindAsync(id);

            if (persona !=null)
            {
                if( (personDto.PersonLastName)!= persona.PersonLastName)
                {
                    persona.PersonLastName = personDto.PersonLastName;
                }
                if (personDto.PersonName!=persona.PersonName)
                {
                    persona.PersonName = personDto.PersonName;
                }
                if(personDto.IdentityNumber!= persona.IdentityNumber)
                {
                    persona.IdentityNumber = personDto.IdentityNumber;  
                }
                if (personDto.Birthday != persona.Birthday)
                { 
                    persona.Birthday = personDto.Birthday;
                }
                if(personDto.Gender!=persona.Gender)
                {
                    persona.Gender = personDto.Gender;
                }
                if (personDto.Phone!=persona.Phone)
                {
                    persona.Phone = personDto.Phone;
                }
                if( personDto.Email!=persona.Email)
                {
                persona.Email = personDto.Email;
                }

              
               _context.Persons.Update(persona);
                _context.SaveChangesAsync();
            }
           

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostPersons(PersonDto personaDto)
        {
            if(personaDto != null) {
                var persona = new PersonEntity()
                {
                    PersonLastName = personaDto.PersonLastName,
                    PersonName = personaDto.PersonName,
                    IdentityNumber = personaDto.IdentityNumber,
                    Birthday = personaDto.Birthday,
                    Phone = personaDto.Phone,
                    Email = personaDto.Email,
                    Gender = personaDto.Gender,

                };
                _context.Persons.Add(persona);
                _context.SaveChanges();
            }
            
        }


        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonEntity(int id)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var personEntity = await _context.Persons.FindAsync(id);
            if (personEntity == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonEntityExists(int id)
        {
            return (_context.Persons?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
