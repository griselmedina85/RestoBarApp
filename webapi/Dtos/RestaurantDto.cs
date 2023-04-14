using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class RestaurantDto
    {
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
    }
}
