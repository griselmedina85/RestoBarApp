using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class PersonDto
    {
        public string? PersonLastName { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonName { get; set; }
        public int? IdentityNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
    }
}
