using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_TownFeatureKind
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Display(Name = "الاسم")]
        public string Kind { get; set; } //school name
        [Display(Name = "التوصيف")]
        public string Description { get; set; }

        [Display(Name = "الاحداثيات")]
        public virtual T_position position { get; set; }

        public virtual T_TownFeature townFeature { get; set; }
    }
}