using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Busniss
{
    public class CookieManager
    {
        public static void SetCookie(string CookieName, string key,string value,int day)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
            cookie.Values[key] = value;
            cookie.Expires = DateTime.Now.AddDays(day);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void RemoveCookie(string CookieName)
        {
            //HttpContext.Current.Response.Cookies.Remove(CookieName);
            if (HttpContext.Current.Request[CookieName] != null)
            {
                var c = new HttpCookie(CookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }

        public static bool findCookie(string CookieName, string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            
            if (cookie != null)
            {
                string val = cookie.Values[key];
                if (val != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> getKeyList(string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                List<string> list = cookie.Values.AllKeys.ToList();
                return list;
            }
            return null;
        }

        public static string CookieValue(string CookieName, string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            
            if (cookie != null)
            {
                string val = cookie.Values[key];
                if (val != null)
                {
                    return val;
                }
            }
            return null;
        }
    }
}