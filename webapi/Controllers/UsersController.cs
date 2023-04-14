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
    public class UsersController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public UsersController(RestoAppContext context)
        {
            _context = context;
        }
        //ViewUsers
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetEmployee()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
           // var Employees = await _context.Employee.Include(p => p.ProfileId).ToListAsync();
            List<UserDto> userDtos = new List<UserDto>();
            foreach (var userDto in _context.Employee) 
            {
                UserDto user = new UserDto
                {
                    UserId = userDto.UserId, 
                    UserName = userDto.UserName,
                    Task = userDto.Task,
                    PersonId = userDto.PersonId,
                    Person=userDto.Person,
                    ProfileId=userDto.ProfileId,
                };
                userDtos.Add(user);
            }
            
            
            return  Ok(userDtos);
        }

        //ViewUser
        // GET: api/User/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<UserDto>> GetUserEntity(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var userEntity = await _context.Employee.FindAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }
            UserDto user = new UserDto()
            {
                UserId=userEntity.UserId,
                UserName = userEntity.UserName,
                Task = userEntity.Task,
                PersonId = userEntity.PersonId,
                Person=userEntity.Person,
                ProfileId=userEntity.ProfileId,

            };
            return user;
        }

    }
}
