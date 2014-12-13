using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_OwnerLike
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "الحالة")]
        public string Status { get; set; }
        
        public int CutomerLikeID { get; set; }
        //user,owner
        public virtual T_Customer Customer { get; set; }
    }
}