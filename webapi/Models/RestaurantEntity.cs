using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class RestaurantEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [Required]
        [StringLength(50)]
        public string RestaurantName { get; set; }
        [Required]
        public string Speciallity { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Capacity { get; set; }
        //Relacion con mesas
        public ICollection<TableEntity>? Tables { get; set; }
        //Relación con reservas
        public ICollection<ReservationEntity>? Reservations { get; set; }
    }
}
