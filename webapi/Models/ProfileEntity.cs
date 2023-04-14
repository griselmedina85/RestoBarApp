using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class ProfileEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProfileName { get; set; }

        //Relación con users
        public ICollection<UserEntity>? Users { get; set; }
    }
}
