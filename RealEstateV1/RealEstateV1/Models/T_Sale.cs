using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Sale
    {
         [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "السعر")]
        public float Price { get; set; }
        [Display(Name = "سعر البيع")]
        public float SoldPrice { get; set; }
        [Display(Name = "تاريخ البيع"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SoldDate { get; set; }
        [Display(Name = "الحالة")]
        public Busniss.MyEnumType.SaleRealEstateStatus Status { get; set; }
        [Display(Name = "الملاحظات")]
        public string Note { get; set; }
        [Display(Name = "تقسيط")]
        public float Taksit { get; set; }
        [Display(Name = "الوصف")]
        public string Description { get; set; }
        [Display(Name = "العقار")]
        
        public virtual T_RealEstate RealEstate { get; set; }
        
        //RE

    }
}