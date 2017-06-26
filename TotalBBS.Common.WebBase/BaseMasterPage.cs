using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TotalBBS.Common.WebBase
{
    public class BaseMasterPage : MasterPage
    {
        protected TBHttpContext admin = new TBHttpContext();
        private static int _current_page;

        public string PATH
        {
            get
            {
                if (Request.ApplicationPath == "/")
                    return string.Empty;
                else
                    return Request.ApplicationPath;
            }
        }

        public string HeaderFolderName
        {
            get
            {
                if (PATH == "/")
                    return Request.CurrentExecutionFilePath;
                else
                    return Request.CurrentExecutionFilePath.Replace(PATH, "");
            }
        }

        public int CurrentPage
        {
            get { return _current_page; }
            set { _current_page = value; }
        }

        public string Host
        {
            get { return Request.Url.Host; }
        }

        public string PathAndQuery
        {
            get { return Request.Url.PathAndQuery; }
        }


        public BaseMasterPage()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //

        }

        public string GetImgUrl(string Url, string UrlPath, int Width, int Height, string Menu)
        {
            if (!string.IsNullOrEmpty(Url.Trim()))
                return UrlPath + Url;
            else
                return "/App_Themes/Images/" + Menu + "/" + Width + "_" + Height + ".gif";
        }

        public void LogOut()
        {
            HttpContext.Current.Response.Redirect("/BackOffice/AdminLogin.aspx", false);
        }

        public string GetDomain(string Host)
        {
            string[] arrItem = Host.Split('.');
            if (arrItem.Length == 3)
                return arrItem[arrItem.Length - 2] + "." + arrItem[arrItem.Length - 1];
            else if (arrItem.Length == 4)
                return arrItem[arrItem.Length - 3] + "." + arrItem[arrItem.Length - 2] + "." + arrItem[arrItem.Length - 1];
            else
                return string.Empty;
        }

    }
}