using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_SearchHistory
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Quary { get; set; }
        public virtual T_Customer Customer { get; set; }
        public DateTime QuaryDate { get; set; }

        public virtual T_RealEstate realEstate { get; set; }
    }
}