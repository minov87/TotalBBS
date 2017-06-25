using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TotalBBS.Common.Data;

namespace TotalBBS.Common.Dac.Common
{
    public class Common_NTx_Dac
    {
        #region [게시판 카테고리 DropDownList]
        public DataTable TOTALBBS_BOARD_CATEGORY_INFO_SEL(int ParentIdx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ParentIdx", ParentIdx),
                new SqlParameter("@CateGubun", "B")
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_CATEGORY_INFO_SEL_SP]", parameters))
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }
        #endregion

        #region [게시글 카테고리 DropDownList]
        public DataTable TOTALBBS_WRITE_CATEGORY_INFO_SEL(int ParentIdx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ParentIdx", ParentIdx),
                new SqlParameter("@CateGubun", "W")
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_CATEGORY_INFO_SEL_SP]", parameters))
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }
        #endregion

        #region [게시판 & 게시글 카테고리 DropDownList]
        public DataTable TOTALBBS_ALL_CATEGORY_INFO_SEL(int ParentIdx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ParentIdx", ParentIdx),
                new SqlParameter("@CateGubun", "A")
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_CATEGORY_INFO_SEL_SP]", parameters))
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }
        #endregion
    }
}