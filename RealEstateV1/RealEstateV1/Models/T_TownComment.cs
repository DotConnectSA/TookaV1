using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_TownComment
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "تقييم")]
        public float Rate { get; set; }
        [Display(Name = "تاريخ الانشاء"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        [Display(Name = "التعليق")]
        public string Comment { get; set; }
        [Display(Name = "العنوان")]
        public string Title { get; set; }
        [Display(Name = " الاعجابات")]
        public int LikeNo { get; set; }
        [Display(Name = "عدم الاعجاب ")]
        public int DisLikeNo { get; set; }
        [Display(Name = "عدد التقارير")]
        public int ReportNo { get; set; }
        public virtual T_Customer Customer { get; set; }
        [Display(Name = "الردود")]
        public virtual Collection<T_Replay> Replay { get; set; }
        //virtual user, town
    }
}