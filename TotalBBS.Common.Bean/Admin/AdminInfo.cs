using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalBBS.Common.Bean.Admin
{
    public class AdminInfo
    {
        public string AdminId
        {
            get
            {
                if (HttpContext.Current.Session["AdminId"] != null)
                {
                    return (string)System.Web.HttpContext.Current.Session["AdminId"];
                }
                else
                {
                    return null;
                }
            }

            set { System.Web.HttpContext.Current.Session["AdminId"] = value; }
        }

        public string AdminName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["AdminName"] != null)
                {
                    return (string)System.Web.HttpContext.Current.Session["AdminName"];
                }
                else
                {
                    return null;
                }
            }

            set { System.Web.HttpContext.Current.Session["AdminName"] = value; }
        }

        public string AdminAuth
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["AdminAuth"] != null)
                {
                    return (string)System.Web.HttpContext.Current.Session["AdminAuth"];
                }
                else
                {
                    return null;
                }
            }

            set { System.Web.HttpContext.Current.Session["AdminAuth"] = value; }
        }
    }
}