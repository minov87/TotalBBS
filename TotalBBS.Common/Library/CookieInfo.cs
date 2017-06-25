using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TotalBBS.Common.Bean.Login;

namespace TotalBBS.Common.Library
{
    public class CookieInfo
    {
        public static void getCookieLogout()
        {
            FormsAuthentication.SignOut();
        }

        public static void setUserCookieSetting()
        {
            //
        }

        public static void setAdminCookieSetting(LoginBean Bean, string strDomain)
        {
            string strCookieName = "LoginInfo";
            string FormNameSettoing = Bean.strAdminId;
            string data = string.Concat(new object[]
            {
                "AdminId=", Bean.strAdminId,
                "&AdminName=", Bean.strAdminName,
                "&AdminAuth=", Bean.chrAdminAuth,
                "&Expire=", DateTime.Now.AddDays(1)
            });

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, strCookieName, DateTime.Now, DateTime.Now.AddDays(1), true, data, FormsAuthentication.FormsCookiePath);

            string hash = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
            cookie.Expires = ticket.Expiration;
            cookie.Domain = strDomain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}