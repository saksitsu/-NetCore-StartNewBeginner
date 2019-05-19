using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SAKSIT.Models
{
    public class order_list
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string order_id { get; set; }
        public string customer_id { get; set; }
        public DateTime order_date { get; set; }
        public bool status { get; set; }

        [ForeignKey("customer_id")]
        public customer customer { get; set; }
    }
}
