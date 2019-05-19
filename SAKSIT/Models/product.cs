using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAKSIT.Models
{
    public class product
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string product_id { get; set; }
        [StringLength(100)]
        public string product_name { get; set; }
    }
}
