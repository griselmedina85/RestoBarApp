using webapi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Dtos
{
    public class ClientDto
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //public int ClientId { get; set; }
        public string? PersonLastName { get; set; }
        public string PersonName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
     
        //Relación con personas 
        //[ForeignKey("PersonEntity")]
        public int PersonId { get; set; }
        //public PersonEntity Person { get; set; }
        //Relacion con Reservas
       
    }
}
