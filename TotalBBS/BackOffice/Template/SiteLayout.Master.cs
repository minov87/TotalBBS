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
        // private StringBuilder sbLeftHtml; // ���� �޴� ���� 

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ltAccNm.Text = admin.MemberNm + "(" + admin.MemberNm + ")";
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
                #region [Error Logger] �α����� �ϴ°��
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().ED_CO_INSERT_ERROR_LOGGER_INFO(ex, admin.MemberId, admin.MemberId);
                #endregion
            }
        }
    }
}