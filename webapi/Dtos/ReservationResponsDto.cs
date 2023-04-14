using System.ComponentModel.DataAnnotations;
using webapi.Models;

namespace webapi.Dtos
{
    public class ReservationResponsDto
    {
        public int ReservationId { get; set; }
        public int NumberDiners { get; set; }
       
        public DateTime Date { get; set; }
      
        public string Time { get; set; }
       
        public bool Attended { get; set; }
      
        public bool FinishedMeal { get; set; }
     
        public bool Cancelation { get; set; }
      
        public string? ReasonCancelation { get; set; }

        public string? PersonLastName { get; set; }

        public string PersonName { get; set; }
        public string? TableDescription { get; set; }
        public int Capacity { get; set; }
    }
}
