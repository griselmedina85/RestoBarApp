using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class ReservationDto
    {
       
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
        public int RestaurantId { get; set; }


        //relacion con cliente
        public int ClientId { get; set; }


        //relación con mesas
        public int TableId { get; set; }
    }
}
