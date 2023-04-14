using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webapi.Models;

namespace webapi.Dtos
{
    public class TableDto
    {
        [Required]
        [StringLength(50)]
        public string? TableDescription { get; set; }
        public string LocationDescription { get; set; }
        public int Capacity { get; set; }
        //public string RestaurantName { get; set; }
        //public string Address { get; set; }

        //public string Phone { get; set; }
        ////Relación con Restaurant
        public int RestaurantId { get; set; }
        //Relacion con locaciones
        public int LocationId { get; set; }
       
        //Relacion con reservas

    }
}
