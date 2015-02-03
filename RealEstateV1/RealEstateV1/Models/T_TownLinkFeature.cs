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

        public virtual T_TownFeature townFeature { get; set; }

        [Display(Name = "تقييم")]
        public virtual T_TownRate Rate { get; set; }

    }
}