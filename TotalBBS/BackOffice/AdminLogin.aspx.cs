using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotalBBS.Common.WebBase;
using TotalBBS.Common.Bean.Login;
using TotalBBS.Common.Dac.Admin;

namespace TotalBBS
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        private string PageLoadError = string.Empty;                        // 페이지 로드 에러
        private string LoginBeen = string.Empty;                            // 로그인 되었습니다.
        private string LoginFailed = string.Empty;                          // 로그인 실패 하였습니다.

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //변수에 메세지 담기
                PageLoadError = "페이지 로드 에러";
                LoginBeen = "로그인 되었습니다.";
                LoginFailed = "로그인 실패 하였습니다.";

                if (!IsPostBack)
                {
                    FormsAuthentication.SignOut();
                }
            }
            catch (Exception)
            {
                StringBuilder sbPageError = new StringBuilder();
                sbPageError.Append("alert('"+ PageLoadError + "');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageError", sbPageError.ToString(), true);

                return;
            }
        }

        #region [로그인]
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Login_NTx oView = new Login_NTx();

                string strTxtLoginId = string.Empty;
                string strTxtLoginPw = string.Empty;

                if(Request.Form["TxtLoginId"] != null)
                {
                    strTxtLoginId = Request.Form["TxtLoginId"].ToString();
                }

                if(Request.Form["TxtLoginPw"] != null)
                {
                    strTxtLoginPw = Request.Form["TxtLoginPw"].ToString();
                }

                LoginBean Bean = oView.TOTALBBS_ADMIN_SEL(strTxtLoginId, strTxtLoginPw);

                if(!string.IsNullOrEmpty(Bean.strAdminId) && Bean.chrCheckLogin.Equals("0"))
                {
                    CookieInfo AdminInfo = new CookieInfo();
                    CookieInfo.setAdminCookieSetting(Bean, ".totalbbs.com");

                    StringBuilder sbLoginOk = new StringBuilder();
                    sbLoginOk.Append("alert('"+ LoginBeen + "');");
                    sbLoginOk.Append("location.href=\"/BackOffice/Main.aspx\"");
                    this.LoginOK_Process(sbLoginOk);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sbLoginError = new StringBuilder();
                sbLoginError.Append("alert('" + LoginFailed + "');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoginError", sbLoginError.ToString(), true);

                return;
            }
        }
        #endregion

        private void LoginOK_Process(StringBuilder sbLoginOk)
        {
            try
            {
                ClientScriptManager CM = this.Page.ClientScript;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Login", sbLoginOk.ToString(), true);
            }
            catch (Exception ex)
            {
                StringBuilder sbLoginError = new StringBuilder();
                sbLoginError.Append("alert('"+ LoginFailed + "');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoginFail", sbLoginError.ToString(), true);
            }
        }
    }
}