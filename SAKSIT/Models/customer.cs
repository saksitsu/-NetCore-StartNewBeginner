using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAKSIT.Models
{
    public class customer
    {
        [Key]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="รหัสลูกค้า")]
        [StringLength(50)]
        public string customer_id { get; set; }
        [StringLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "ชื่อ")]
        public string firstname { get; set; }
        [StringLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "นามสกุล")]
        public string lastname { get; set; }
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "ที่อยู่")]
        public string address { get; set; }
    }
}
