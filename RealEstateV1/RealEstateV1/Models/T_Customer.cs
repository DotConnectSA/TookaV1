using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_Customer
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int ID { get; set; }
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "الايميل")]
        public string Email { get; set; }
        public virtual T_Address address { get; set; }
        public string ImgeUrl { get; set; }
        [Display(Name = "جوال")]
        public string Mobile { get; set; }
        [Display(Name = "هاتف")]
        public string Phone { get; set; }
        public string ProfileID { get; set; }
    }
}