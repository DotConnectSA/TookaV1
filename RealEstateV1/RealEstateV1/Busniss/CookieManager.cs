using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Busniss
{
    public class CookieManager
    {
        public static void SetLikeCookie(string CookieName, string str)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
            cookie.Values[str] = "true";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetDsLikeCookie(string CookieName, string str)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
            cookie.Values[str] = "false";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool SearchLikeCookie(string CookieName, string str)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                string val = cookie.Values[str];
                if (val != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}