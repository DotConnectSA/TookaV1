using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Favorit
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "الحالة")]
        public Busniss.MyEnumType.FavoritStatus status { get; set; }
        public virtual T_Customer Customer { get; set; }
        [Display(Name = "العقار")]
        
        public virtual T_RealEstate RealEstate { get; set; }
        
        
        //customer, RE
    }
}