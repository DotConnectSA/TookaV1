using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_SearchHistory
    {
        public int ID { get; set; }
        public string Quary { get; set; }
        public virtual T_Customer Customer { get; set; }
        public DateTime QuaryDate { get; set; }
        //user
    }
}