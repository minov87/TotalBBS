using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TotalBBS.Common.Data;

namespace TotalBBS.Common.Dac.Board
{
    public class Board_Tx_Dac
    {
        #region [게시판 관리] 게시글 관리 등록
        public int TOTALBBS_BOARD_INFO_INS(int intBoardCategory, int intWriteCategory, string strUserId, string strWriter, string strSubject, string strContent)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@intBoardCategory", intBoardCategory),
                        new SqlParameter("@intWriteCategory", intWriteCategory),
                        new SqlParameter("@strUserId", strUserId),
                        new SqlParameter("@strWriter", strWriter),
                        new SqlParameter("@strSubject", strSubject),
                        new SqlParameter("@strContent", strContent),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[6].Direction = ParameterDirection.ReturnValue;
            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_INFO_INS_SP]", parameters);

            return (int)parameters[6].Value;
        }
        #endregion

        #region [게시판 관리] 게시글 관리 수정
        public int TOTALBBS_BOARD_INFO_UPD(int Idx, int intBoardCategory, int intWriteCategory, string strUserId, string strWriter, string strSubject, string strContent)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@intIdx", Idx),
                        new SqlParameter("@intWriteCategory", intWriteCategory),
                        new SqlParameter("@strUserId", strUserId),
                        new SqlParameter("@strWriter", strWriter),
                        new SqlParameter("@strSubject", strSubject),
                        new SqlParameter("@strContent", strContent),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[6].Direction = ParameterDirection.ReturnValue;
            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_INFO_UPD_SP]", parameters);

            return (int)parameters[6].Value;
        }
        #endregion

        #region [게시판 관리] 게시글 관리 수정 페이지 에서 삭제 (단일 삭제)
        public int TOTALBBS_BOARD_INFO_DEL(int Idx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@Idx", Idx),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[1].Direction = ParameterDirection.ReturnValue;
            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_INFO_DEL_SP]", parameters);

            return (int)parameters[1].Value;
        }
        #endregion

        #region [게시판 관리] 게시글 관리 수정 페이지 에서 삭제 (다중 삭제)
        public int TOTALBBS_BOARD_INFO_MULTI_DEL(string IdxList)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@IdxList", IdxList),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[1].Direction = ParameterDirection.ReturnValue;
            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_INFO_MULTI_DEL_SP]", parameters);

            return (int)parameters[1].Value;
        }
        #endregion

        #region [게시판 관리] 첨부파일 등록
        public int TOTALBBS_BOARD_FILE_INFO_INS(int ParentIdx, string OldFileName, string NewFileName, string FileUploadPath, int Sort)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@ParentIdx", ParentIdx),
                        new SqlParameter("@OldFileName", OldFileName),
                        new SqlParameter("@NewFileName", NewFileName),
                        new SqlParameter("@FileUploadPath", FileUploadPath),
                        new SqlParameter("@Sort", Sort),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[5].Direction = ParameterDirection.ReturnValue;

            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_FILE_INFO_INS_SP]", parameters);

            return (int)parameters[5].Value;
        }
        #endregion

        #region [게시판 관리] 첨부파일 수정
        public int TOTALBBS_BOARD_FILE_INFO_UPD(int ParentIdx, string OldFileName, string NewFileName, string FileUploadPath, int Sort)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@ParentIdx", ParentIdx),
                        new SqlParameter("@OldFileName", OldFileName),
                        new SqlParameter("@NewFileName", NewFileName),
                        new SqlParameter("@FileUploadPath", FileUploadPath),
                        new SqlParameter("@Sort", Sort),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[5].Direction = ParameterDirection.ReturnValue;

            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_FILE_INFO_UPD_SP]", parameters);

            return (int)parameters[5].Value;
        }
        #endregion

        #region [게시판 관리] (체크박스 선택된) 첨부파일 삭제
        public int TOTALBBS_BOARD_FILE_INFO_DEL_SELECTED(string ListIdx)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@ListIdx", ListIdx),
                        new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[1].Direction = ParameterDirection.ReturnValue;

            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_FILE_INFO_DEL_SELECTED_SP]", parameters);

            return (int)parameters[1].Value;
        }
        #endregion

        #region [게시판 관리] 게시글 상세보기 삭제
        public int TOTALBBS_BOARD_FILE_INFO_VIEW_DEL(int Idx, int FileDelCnt)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Idx", Idx),
                new SqlParameter("@FileDelCnt", FileDelCnt),
                new SqlParameter("RETURN_VALUE", SqlDbType.Int)
            };

            parameters[3].Direction = ParameterDirection.ReturnValue;
            SQLHelper.ExecuteNonQuery(Global.DataBaseConnection, CommandType.StoredProcedure, "[dbo].[UP_TOTALBBS_BOARD_FILE_INFO_VIEW_DEL_SP]", parameters);

            return (int)parameters[3].Value;
        }
        #endregion
    }
}