using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TotalBBS.Common.Bean.Login;
using TotalBBS.Common.WebBase;

namespace TotalBBS.Common.WebBase
{
    public class AdminBasePage : Page
    {
        HttpCookie AdminUserInfo;
        private int _page_per_data = 10;
        protected TBHttpContext admin = new TBHttpContext();

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
                {
                    if (!string.IsNullOrEmpty(PATH))
                        return Request.CurrentExecutionFilePath.Replace(PATH, "");
                    else
                        return Request.CurrentExecutionFilePath;
                }
            }
        }

        public int CharacterIdx
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CharacterIdx"]))
                    return int.Parse(Request.QueryString["CharacterIdx"]);
                else
                    return 1;
            }
        }

        public int PagePerData
        {
            get { return _page_per_data; }
            set { _page_per_data = value; }
        }

        public AdminBasePage()
        {
            //
        }
    }
}