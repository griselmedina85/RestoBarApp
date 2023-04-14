using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webapi.Models;

namespace webapi.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }

        //Relación con perfil
        //este campo me interesa para mostrar que tipo de usuario es
        //public string ProfileName { get; set; }
        public int ProfileId { get; set; }
        //Relación con Persona
        [ForeignKey("PersonEntity")]
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }
        //tarea Empleado
        public string? Task { get; set; }
    }
}
