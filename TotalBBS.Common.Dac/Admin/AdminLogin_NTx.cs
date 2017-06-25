using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TotalBBS.Common.Bean.Board;
using TotalBBS.Common.Bean.Login;
using TotalBBS.Common.Data;

namespace TotalBBS.Common.Dac.Admin
{
    public class Login_NTx
    {
        #region [관리자 조회]
        public LoginBean TOTALBBS_ADMIN_SEL(string AdminId, string AdminPw)
        {
            DataRow drRow = null;

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@AdminId", AdminId),
                new SqlParameter("@AdminPw", AdminPw)
            };

            LoginBean Bean = new LoginBean();

            using (DataSet dsView = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_ADMIN_INFO_SEL_SP]", parameter))
            {
                drRow = dsView.Tables[0].Rows.Count == 0 ? null : dsView.Tables[0].Rows[0];
            }

            if(drRow != null)
            {
                Bean.intIdx = Convert.ToInt32(drRow["intIdx"]);
                Bean.strAdminId = Convert.ToString(drRow["strAdminId"]);
                Bean.strAdminName = Convert.ToString(drRow["strAdminName"]);
                Bean.chrAdminAuth = Convert.ToString(drRow["chrAdminAuth"]);
                Bean.chrCheckLogin = Convert.ToString(drRow["chrCheckLogin"]);
                Bean.dateRegDate = Convert.ToString(drRow["dateRegDate"]);
            }

            return Bean;
        }
        #endregion
    }
}