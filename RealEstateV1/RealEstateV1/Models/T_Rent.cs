using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Rent
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "السعر")]
        public float Price { get; set; }
        [Display(Name = "سعر الايجار")]
        public float RentPrice { get; set; }
        [Display(Name = "من التاريخ"),DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }
        [Display(Name = "إلى التاريخ"),DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }
        [Display(Name = "الحالة")]
        public Busniss.MyEnumType.RentRealEstateStatus Status { get; set; }
        [Display(Name = "الملاحظات")]
        public string Note { get; set; }
        [Display(Name = "الوصف")]
        public string Description { get; set; }
        [Display(Name = "العقار")]
        
        public virtual T_RealEstate RealEstate { get; set; }
        
        //RE
    }
}