using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class UserEntity
    {
        /*
id_person	int	Unchecked
*/

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserPassword { get; set; }
        //public string PasswordHash { get; set; }
        //public string PasswordSalt {get; set;}
        //Relación con perfil
        public int ProfileId { get; set; }
        //Relación con Persona
        [ForeignKey("PersonEntity")]
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }
        //tarea Empleado
        public string? Task { get; set; }
    }
}
