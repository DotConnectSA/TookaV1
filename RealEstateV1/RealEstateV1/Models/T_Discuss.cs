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
    public class T_Discuss
    {
        public T_Discuss()
        {
            Replay = new Collection<T_Replay>();
        }
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DataType(DataType.Date),Display(Name = "تاريخ الانشاء"),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "الموضوع")]
        public string Topic { get; set; }
        [Display(Name = " الاعجابات")]
        public int LikeNo { get; set; }
        [Display(Name = "عدم الاعجاب ")]
        public int DisLikeNo { get; set; }
        [Display(Name = "عدد التقارير")]
        public int ReportNo { get; set; }
        public virtual T_Customer Customer { get; set; }
        [Display(Name = "الردود")]
        public virtual Collection<T_Replay> Replay { get; set; }
    }
}