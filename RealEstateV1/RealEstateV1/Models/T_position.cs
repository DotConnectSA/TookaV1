using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateV1.Models
{
    public class T_position{
    
    //    public T_position ()
    //{
    //        Address=new Collection<T_Address>();
    //}
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
       // public Collection<T_Address> Address { get; set; }
    }
}