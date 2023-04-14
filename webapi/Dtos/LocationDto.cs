using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webapi.Models;

namespace webapi.Dtos
{
    public class LocationDto
    {
        [Required]
        [StringLength(50)]
        public string LocationDescription { get; set; }
       
    }
}
