using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotalBBS.Common.WebBase;

namespace TotalBBS.BackOffice.Template
{
    public partial class SiteLayout : BaseMasterPage
    {
        // private StringBuilder sbLeftHtml; // 좌측 메뉴 관련 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(admin.MemberId.Equals(null) || admin.MemberId.Equals(""))
                {
                    CookieInfo.getCookieLogout();
                    Response.Redirect("/BackOffice/AdminLogin.aspx", false);
                }
                else
                {
                    this.ltAccNm.Text = admin.MemberNm + "(" + admin.MemberNm + ")";
                }
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 하는경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberId);
                #endregion
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                CookieInfo.getCookieLogout();
                Response.Redirect("/BackOffice/AdminLogin.aspx", false);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 하는경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberId);
                #endregion
            }
        }
    }
}