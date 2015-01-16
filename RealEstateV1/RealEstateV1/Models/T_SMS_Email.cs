using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_SMS_Email
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public bool IsSent { get; set; }
        public bool IsSMS { get; set; }
    }
}