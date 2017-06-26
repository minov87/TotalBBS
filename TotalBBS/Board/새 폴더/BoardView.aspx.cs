using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TotalBBS.Common.Dac.Board;
using TotalBBS.Common.Dac.Common;
using TotalBBS.Common.Library;

namespace TotalBBS.Board
{
    public partial class BoardView : System.Web.UI.Page
    {
        //파일업로드 경로(폴더명)
        private string FileUploadPath = "Board";

        //검색어 변수
        private string ParamBoardCategory = string.Empty;
        private string ParamField = string.Empty;
        private string ParamKey = string.Empty;

        //첨부파일 카운트
        private int AttachedFileCnt = 0;

        private int iIdx = 0;
        private int iPage = 0;
        private string DefaultPage = "/Board/BoardList.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlForm form1 = (HtmlForm)Master.FindControl("form1");

                form1.Method = "post";
                form1.Enctype = "multipart/form-data";

                #region [상세보기를 위한 데이터 처리]
                object obj_BoardModify = Cache["BoardModify"];

                string[] ParameterData = new string[5];
                if (obj_BoardModify != null)
                {
                    Response.Redirect(DefaultPage, false);
                }
                else
                {
                    if (obj_BoardModify != null)
                    {
                        //수정 페이지
                        ParameterData = obj_BoardModify as string[];

                        if (ParameterData != null)
                        {
                            ParamBoardCategory = ParameterData[0];
                            ParamField = ParameterData[1];
                            ParamKey = ParameterData[2];
                            iIdx = IntegerUtil.intValid(ParameterData[3], 0);
                            iPage = IntegerUtil.intPage(ParameterData[4], 1);

                            this.GetPageInfoSetting(ParamBoardCategory, ParamField, ParamKey, iIdx, iPage, "V");
                        }
                    }
                    else
                    {
                        StringBuilder sbError = new StringBuilder();
                        sbError.Append("alert('데이터 조회에 실패하였습니다.');history.go(-1);");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageError", sbError.ToString(), true);
                    }
                }
                #endregion
            }

        }

        #region [기본셋팅] 페이지 기본값 정의
        private void GetPageInfoSetting(string ParamBoardCategory, string ParamField, string ParamKey, int Idx, int PageNo, string CMD)
        {
            //페이지 HTML 언어 세팅
            this.ltThBoardCategory.Text = "게시판 카테고리";
            this.ltThWriteCategory.Text = "게시글 카테고리";
            this.ltThUserId.Text = "작성자 아이디";
            this.ltThWriter.Text = "작성자 이름";
            this.ltThSubject.Text = "제목";
            this.ltThContent.Text = "내용";
            this.ltRegiDate.Text = "등록일";
            this.ltViewCnt.Text = "조회수";

            DataSet ds = null;
            if (CMD.Equals("V"))
            {
                Board_NTx_Dac oWS = new Board_NTx_Dac();
                ds = oWS.TOTALBBS_BOARD_VIEW_SEL(Idx);
            }
            else
            {
                this.trVisible_2.Attributes.Add("style", "display:none");
                this.trVisible_2.Visible = false;
            }

            this.GetPageSetting(Idx, ds, CMD);
        }
        #endregion

        #region [기본셋팅] 등록 또는 수정
        private void GetPageSetting(int Idx, DataSet ds, string CMD)
        {
            if (CMD.Equals("V"))
            {
                this.lbtnCancel.Text = "<span>목록</span>";
                this.lbtnCancel.CssClass = "buttons fl_l";

                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];

                Common_NTx_Dac ddlSet = new Common_NTx_Dac();
                DataTable AllCategorydt = ddlSet.TOTALBBS_ALL_CATEGORY_INFO_SEL(-1);

                this.ltBoardCategory.Text = (AllCategorydt.AsEnumerable().Where(p => (p["intIdx"].ToString() == dt1.Rows[0]["intBoardCatrgory"].ToString()) && (p["chrCateGubun"].ToString() == "B")).Select(p => p["strCateName"].ToString())).FirstOrDefault();
                this.ltWriteCategory.Text = (AllCategorydt.AsEnumerable().Where(p => (p["intIdx"].ToString() == dt1.Rows[0]["intWriteCategory"].ToString()) && (p["chrCateGubun"].ToString() == "W")).Select(p => p["strCateName"].ToString())).FirstOrDefault();

                this.ltUserId.Text = dt1.Rows[0]["strUserId"].ToString();
                this.ltWriter.Text = dt1.Rows[0]["strWriter"].ToString();
                this.ltSubject.Text = dt1.Rows[0]["strSubject"].ToString();
                this.ltContent.Text = dt1.Rows[0]["strContent"].ToString();
                this.ltRegiDateValue.Text = dt1.Rows[0]["dateRegDate"].ToString();
                this.ltViewCntValue.Text = dt1.Rows[0]["intViewCount"].ToString();

                //첨부파일 세팅
                if (dt2.Rows.Count == 0)
                {
                    this.rptBoard_Attached.DataSource = dt2;
                    this.rptBoard_Attached.DataBind();
                }
                else
                {
                    AttachedFileCnt = 1;
                    this.rptBoard_Attached.DataSource = dt2;
                    this.rptBoard_Attached.DataBind();
                }
            }

            this.hdfIdx.Value = Idx.ToString();
            this.hdfCMD.Value = CMD;
        }
        #endregion

        #region [첨부파일] 데이터 바인딩
        protected void rptBoard_Attached_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // 데이타 처리
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;

                Literal ltAttachedFile = (Literal)e.Item.FindControl("ltAttachedFile");
                Literal ltAttachedCheckBox = (Literal)e.Item.FindControl("ltAttachedCheckBox");
                ltAttachedFile.Text = drv.Row["strRealFileName"].ToString();
                ltAttachedCheckBox.Text = "<input type=\"checkbox\" name=\"chkAttached_" + drv.Row["intBBSIdx"].ToString() + "\" id=\"chkAttached_" + drv.Row["intIdx"].ToString() + "\" value=\"" + drv.Row["intIdx"].ToString() + "\" class=\"va_3\" /> 삭제";
            }
        }
        #endregion
    }
}