using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Busniss
{
    public class SessionManager
    {
        public static bool IsArabic
        {
            get
            {
                var result = Utility.ConvertToBoolean(HttpContext.Current.Session["IsArabic"]);
                return result;

            }
            set
            {
                HttpContext.Current.Session["IsArabic"] = value;
            }
        }

        public static SearchItem searchKey
        {
            get
            {
                var result = (SearchItem)(HttpContext.Current.Session["searchKey"]);
                return result;

            }
            set
            {
               
                    HttpContext.Current.Session["searchKey"] = value;
            }
        }
        public static object Result
        {
            get
            {
                var result = (HttpContext.Current.Session["Result"]);
                return result;

            }
            set
            {
                HttpContext.Current.Session["Result"] = value;
            }
        }
    }
}