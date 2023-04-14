using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class ReservationEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        [Required]
        public int NumberDiners { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public bool Attended { get; set; }
        [Required]
        public bool FinishedMeal { get; set; }
        [Required]
        public bool Cancelation { get; set; }
        [StringLength(50)]
        public string? ReasonCancelation { get; set; }

        //relacion con restaurant
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public RestaurantEntity restaurant { get; set; }

        ////relacion con cliente
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public ClientEntity Client { get; set; }

        ////relación con mesas
        [ForeignKey("TableId")]
        public int TableId { get; set; }
        public TableEntity Table { get; set; }

    }
}
