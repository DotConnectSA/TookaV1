using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class TownEmail : Email 
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}