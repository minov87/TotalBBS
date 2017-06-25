using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TotalBBS.Common.Bean.Board;
using TotalBBS.Common.Data;

namespace TotalBBS.Common.Dac.Board
{
    public class Board_NTx_Dac
    {
        #region [게시판 관리] 목록 조회
         public List<BoardBean> TOTALBBS_BOARD_INFO_SEL(int PagePerData, int CurrentPage, string BoardCategory, string GET_TYPE, string FIELD, string KEY)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PagePerData", PagePerData),
                new SqlParameter("@CurrentPage", CurrentPage),
                new SqlParameter("@BoardCategory",  Convert.ToInt32(BoardCategory)),
                new SqlParameter("@GET_TYPE", GET_TYPE),
                new SqlParameter("@FIELD", FIELD),
                new SqlParameter("@KEY", KEY)
            };

            List<BoardBean> GetList = new List<BoardBean>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_INFO_SEL_SP]", parameters))
            {
                while (dr.Read())
                {
                    BoardBean Bean = new BoardBean();

                    Bean.intIdx = Convert.ToInt32(dr["intIdx"]);
                    Bean.intBoardCategory = Convert.ToInt32(dr["intBoardCategory"]);
                    Bean.intWriteCategory = Convert.ToInt32(dr["intWriteCategory"]);
                    Bean.strUserId = dr["strUserId"].ToString();
                    Bean.strWriter = dr["strWriter"].ToString();
                    Bean.strSubject = dr["strSubject"].ToString();
                    Bean.intViewCount = Convert.ToInt32(dr["intViewCount"]);
                    Bean.dateRegDate = dr["dateRegDate"].ToString();

                    GetList.Add(Bean);
                }

                return GetList;
            }
        }
        #endregion

        #region [게시판 관리] 목록 총개수 조회
        public int TOTALBBS_BOARD_INFO_COUNT_SEL(int PagePerData, int CurrentPage, string BoardCategory, string GET_TYPE, string FIELD, string KEY)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PagePerData", PagePerData),
                new SqlParameter("@CurrentPage", CurrentPage),
                new SqlParameter("@BoardCategory", BoardCategory),
                new SqlParameter("@GET_TYPE", GET_TYPE),
                new SqlParameter("@FIELD", FIELD),
                new SqlParameter("@KEY", KEY)
            };

            return Convert.ToInt32(SQLHelper.ExecuteScalar(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_INFO_SEL_SP]", parameters));
        }
        #endregion

        #region [게시판 관리] 목록 상세보기 조회
        public DataSet TOTALBBS_BOARD_VIEW_SEL(int Idx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Idx", Idx)
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_VIEW_SEL_SP]", parameters))
            {
                return ds;
            }
        }
        #endregion

        #region [게시판 관리] 첨부파일 체크박스 선택여부 리스트 조회
        public DataSet TOTALBBS_BOARD_FILE_INFO_SEL(int Idx, string ChkBoxListData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Idx", Idx),
                new SqlParameter("@ChkBoxListData", ChkBoxListData)
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_FILE_INFO_SEL_SP]", parameters))
            {
                return ds;
            }
        }
        #endregion

        #region [게시판 관리] 게시글 상세보기 삭제 시 파일목록 리스트 조회
        public DataSet TOTALBBS_BOARD_VIEW_FILE_INFO_SEL(int Idx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Idx", Idx)
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_VIEW_FILE_INFO_SEL_SP]", parameters))
            {
                return ds;
            }
        }
        #endregion

        #region [게시판 관리] 게시글 리스트 체크박스 선택여부 조회
        public DataSet TOTALBBS_BOARD_VIEW_FILE_INFO_SEL(string ChkBoxListData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ChkBoxListData", ChkBoxListData)
            };

            using (DataSet ds = SQLHelper.ExecuteDataset(Global.DataBaseConnection, "[dbo].[UP_TOTALBBS_BOARD_VIEW_FILE_INFO_SEL_SP]", parameters))
            {
                return ds;
            }
        }
        #endregion
    }
}