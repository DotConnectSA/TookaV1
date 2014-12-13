using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_TownLinkFeature
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "تقييم")]
        public string Kind { get; set; } //school name
        [Display(Name = "الاسم")]
        public string Description { get; set; }
        [Display(Name = "التوصيف")]
        public virtual T_TownFeature townFeature { get; set; }
        
        [Display(Name = "الاحداثيات")]
        public virtual T_position position { get; set; }
        public virtual T_TownRate Rate { get; set; }
       
    }
}