using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Owner
    {
        public T_Owner()
        {
            OwnerLike = new Collection<T_OwnerLike>();
        }
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Display(Name = "تفاصيل")]
        public string Description { get; set; }
        [Display(Name = "المهارات")]
        public string Skills { get; set; }
       
        [Display(Name = "الشعار")]
        public string logo { get; set; }

        [Display(Name = "النوع")]
        public Busniss.MyEnumType.ownerKind kind { get; set; }

        public int ShowNumber { get; set; }
        public virtual Collection<T_OwnerLike> OwnerLike { get; set; }
        public virtual T_Customer customer { get; set; }
       
    }
}