using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.Dtos
{
    public class EmployeeDto 
    {
        public string? PersonLastName { get; set; }
        public string PersonName { get; set; }
        public int? IdentityNumber { get; set; } //se usa el dni como userName para los empleados
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string UserName { get; set; }
        public string? UserPassword { get; set; }
        //public string PasswordHash { get; set; }
        //public string PasswordSalt {get; set;}
        
        //Relación con perfil
        public int ProfileId { get; set; }
        //Relación con Persona
        [ForeignKey("PersonEntity")]
        //public int PersonId { get; set; }
        //public PersonEntity Person { get; set; }
        //tarea Empleado
        public string? Task { get; set; }
    }
}
