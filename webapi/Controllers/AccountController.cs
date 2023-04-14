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
    public class AccountController : ControllerBase
    {
        private readonly RestoAppContext _context;

        public AccountController(RestoAppContext context)
        {
            _context = context;
        }
        //Login
        [HttpPost("Login")]
        //verificar que funcion cumple FromBody
        public bool Login(string username, string password)
        {
            //Corroborar el username en la base de datos
            //Si existe, traer el usuario
            //comparar las contraseñas ( tiene que coincider)
            //Si son iguales, informo que se logueo correctamente sino mensaje de credenciales incorrectas
            int id = 1;
            var usuario = _context.Employee.Find(id);
            if(usuario == null)
            {
                return false;
            }
            //if(username== "Admin" && password == "Admin1234")
            if(username== usuario.UserName && password == usuario.UserPassword)
            {
                return true;
            }
            return false;

        }

        //Registrer client
        [HttpPost("RegisterCli")]
        public string RegisterCli()
        {
           //verifico que no exista el dni en personas
           //si no existe carga los datos y creo la persona
           //con la persona creada obtengo el id y ese se lo asigno a un cliente nuevo

            return "algo";
        }
        //Registrer employee
        [HttpPost("RegisterEmployee")]
        public string RegisterEmp(EmployeeDto employeeDto)
        {
            //para registrar un empleado verifico que el userName que es el dni no exista en 
            //la tabla employees
           
            
            UserEntity existUser;
            if (_context.Employee != null)
            {
                existUser = _context.Employee.FirstOrDefault(e => e.UserName == employeeDto.UserName);
                if(existUser != null)
                {
                return "No se puede registrar";
                }
            }
                        
           
            //si no existe verifico si esa persona ya existe en la tabla person
            var persona = _context.Persons.Where(t => t.IdentityNumber == employeeDto.IdentityNumber).FirstOrDefault();
            if (persona != null ) 
            {
                return "No se puede registrar";
            }

            //si no existe la persona primero cargo y guarda los datos 
            var person = new PersonEntity()
            {
                PersonLastName = employeeDto.PersonLastName,
                PersonName = employeeDto.PersonName,
                Phone = employeeDto.Phone,
                Birthday = employeeDto.Birthday,
                Email = employeeDto.Email,
                IdentityNumber = employeeDto.IdentityNumber,
                Gender = employeeDto.Gender,
            };
            _context.Persons.Add(person);
            _context.SaveChanges();
            //int idProfile = 1;
            int idPersona = person.PersonId;
            UserEntity employee = new UserEntity()
            {
                UserName = employeeDto.UserName,
                UserPassword = employeeDto.UserPassword,
                PersonId = idPersona,
                ProfileId = employeeDto.ProfileId,
                Task = employeeDto.Task,
            };
            _context.Employee.Add(employee);
            _context.SaveChanges();
            //obtengo el PersonId y luego doy de alta como empleado indicando la tarea
            //si existe la persona pero no como empleado directamente cargo como empleado y la tarea a realizar
            
            return "registro exitoso";
        }
    }
}
