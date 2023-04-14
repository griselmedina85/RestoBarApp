using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Models
{
    public class LocationEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50)]
        public string LocationDescription { get; set; }
        //Relacion con mesas
        public List<TableEntity> Tables { get; set; }
    }
}
