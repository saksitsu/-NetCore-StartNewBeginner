using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SAKSIT.Models
{
    public class order_detail
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(50)]
        public string order_id { get; set; }
        [StringLength(10)]
        public string product_id { get; set; }
        public decimal qty { get; set; }
        public decimal amount { get; set; }

        [ForeignKey("product_id")]
        public product product { get; set; }
        [ForeignKey("order_id")]
        public order_list order_List { get; set; }
    }
}
