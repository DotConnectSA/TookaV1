using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Address
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "الشارع")]
        public string Street { get; set; }

        [Display(Name = "البناء")]
        public string building { get; set; }
        [Display(Name = "الطابق")]
        public string floor { get; set; }
        [Display(Name = "الباب رقم")]
        public string door { get; set; }
        [Display(Name = "الملاحظات")]
        public string Note { get; set; }
        [Display(Name = "الاحداثيات")]
        
        public virtual T_position Postion { get; set; }
        [Display(Name = "الحي")]

        public virtual T_Town Town { get; set; }
        

       
    }
}