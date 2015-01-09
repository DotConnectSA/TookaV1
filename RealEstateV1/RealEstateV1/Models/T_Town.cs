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
    public class T_Town
    {
        public T_Town()
        {
            Comment = new Collection<T_TownComment>();
            townLinkFeature = new Collection<T_TownLinkFeature>();
        }
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "الحي")]
        public string TownName { get; set; }
        [Display(Name = "الوصف")]
        public string Description { get; set; }
        
        [Display(Name = "التعليقات")]
        public virtual Collection<T_TownComment> Comment { get; set; }
        
        [Display(Name = "الاحداثيات")]
        public virtual T_position position { get; set; }
        
        [Display(Name = "المدينة")]
        public virtual T_City City { get; set; }
       
        public virtual Collection<T_TownLinkFeature> townLinkFeature { get; set; }
    }
}