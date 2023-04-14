using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class TableEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }
        [Required]
        [StringLength(50)]
        public string? TableDescription { get; set; }
        public int Capacity { get; set; }
        //Relacion con locaciones
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
        
        //Relación con Restaurant
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        //Navigation Property
        public RestaurantEntity Restaurant { get; set; }

        //Relacion con reservas
        public IEnumerable<ReservationEntity>? Reservations { get; set; }



    }
}
