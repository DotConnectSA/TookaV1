using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_REImage
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Busniss.MyEnumType.imageKind kind { get; set; }
        public string ImageURL { get; set; }
        public string status { get; set; }
        //virual RealEstate
    }
}