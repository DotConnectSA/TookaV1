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
    public class T_RealEstate
    {
        public T_RealEstate()
        {
          

        }
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "عدد الغرف")]
        public int RoomNo { get; set; }
        [Display(Name = "عدد الحمامات")]
        public int BathroomNo { get; set; }
        [Display(Name = "العملة")]
        public string Currency { get; set; }
        [Display(Name = "الحالة")]
        public string status { get; set; }
        [Display(Name = "حالة العقار")]
        public Busniss.MyEnumType.RealEstateSituation situation { get; set; }
        [Display(Name = "مساحة العقار")]
        public float Area { get; set; }
        [Display(Name = "مساحة الارض")]
        public float landArea { get; set; }
        [Display(Name = "صورة")]
        public string ImageURL { get; set; }
        [DataType(DataType.Date), Display(Name = "تاريخ الانشاء"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "تم الحذف")]
        public bool IsDeleted { get; set; }
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }
        [Display(Name = "وصف")]
        public string Description { get; set; }
        [Display(Name = "عدد التقارير")]
        public int reportNo { get; set; }
        [Display(Name = "العنوان")]
        
        public virtual T_Address address { get; set; }
        [Display(Name = "النوع")]
        
        public virtual T_REKind Rekind { get; set; }
        [Display(Name = "المكتب العقاري")]
        
        public virtual T_Owner Owner { get; set; }
        [Display(Name = "مالك العقار")]

        public virtual T_Customer REOwner { get; set; }
        [Display(Name = "المميزات")]
        public virtual ICollection<T_REFeature> RealEstateFeature { get; set; }
        [Display(Name = "الفيديوهات")]
        public virtual ICollection<T_REVideo> REVideo { get; set; }
        [Display(Name = "الصور")]
        public virtual ICollection<T_REImage> REImageB { get; set; }
       
    }
}