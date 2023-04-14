using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class ClientEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        //public int canReser { get; set; }
        //Relación con personas
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }
        //Relacion con Reservas
        
        public IEnumerable<ReservationEntity>? Reservations { get; set; }

    }
}
