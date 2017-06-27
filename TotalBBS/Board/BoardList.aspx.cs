using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotalBBS.Common.Library;
using TotalBBS.Common.WebBase;
using TotalBBS.Common.Dac.Board;
using TotalBBS.Common.Bean.Board;
using TotalBBS.Common.WebLib;
using System.Globalization;
using TotalBBS.Common.Dac.Common;
using System.Data;
using System.Linq;

namespace TotalBBS.Board
{
    public partial class BoardList : Page
    {
        private string strBoardCategory = "-1";
        private string strField = string.Empty;
        private string strKey = string.Empty;
        private string strTxTStartDate = string.Empty;
        private string strTxTEndDate = string.Empty;

        //페이징
        private int iPage = 1;
        private const int PageNo = 1;
        private const int PageSize = 10;
        private int TotalCnt = 0;           // 총 레코드 갯수(글번호 순서 정렬용)
        private int NoDataTotalCnt = 0;     // 총 레코드 갯수(글번호 순서 정렬용)
        private string FileUploadPath = "Board"; // 파일 업로드 폴더 경로 업로드 및 삭제 하기위함
        private int intPageViewRow = PageSize;
        public DataTable BoardCategorydt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.GetPageDefaultSetting();
                this.ltTitle.Text = "게시물 관리";
                PagingHelper1.PageSize = PageSize;

                if (!IsPostBack)
                {
                    this.ParameterSetting();
                    // 사용자 로그인 체크
                    //if (admin.MemberAuth.Equals("1"))
                    {
                        if (Request.QueryString["Page"] != null)
                        {
                            //페이지값을 유지하기위해서
                            iPage = IntegerUtil.intValid(Request.QueryString["Page"].ToString(), 1);
                            this.GetListInfo(PageSize, iPage, strBoardCategory, strField, strKey);
                            Page_Move(iPage, PagingHelper1);
                        }
                        else
                        {
                            this.GetListInfo(PageSize, 1, strBoardCategory, strField, strKey);
                            Page_Move(1, PagingHelper1);
                        }
                    }
                    //}
                    //else
                    //{
                    //    JavaScript.AccessAlertLocation("로그인 후 이용가능 합니다.");
                    //}
                }
            }
            catch (Exception ex)
            {
                StringBuilder sbError = new StringBuilder();
                sbError.Append("alert('데이터 조회에 실패하였습니다.');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageError", sbError.ToString(), true);
            }
        }

        #region [기본셋팅] 페이지 기본값 정의
        private void GetPageDefaultSetting()
        {
            this.ltTitle.Text = "게시글 관리";
            this.ltTotalPosts.Text = "전체 게시글 수";

            #region [버튼 설정]
            this.lbtnCreate.Text = "<span>등록</span>";
            this.lbtnSearch.Text = "<span>검색</span>";

            #endregion

            #region [DropDownList 설정]
            this.FIELD.Items.Clear();
            this.FIELD.Items.Add(new ListItem("작성자", "WRITER"));
            this.FIELD.Items.Add(new ListItem("제목", "SUBJECT"));

            Common_NTx_Dac ddlSet = new Common_NTx_Dac();

            // 게시판 카테고리 필드
            BoardCategorydt = ddlSet.TOTALBBS_BOARD_CATEGORY_INFO_SEL(-1);
            int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, -1);

            // 게시판 카테고리 필드 - DropDownList
            if (BoardCategorydt.Rows.Count == 0)
            {
                this.ddlBoardCategory.Items.Add(new ListItem("등록된 카테고리가 없습니다.", ""));
                this.ddlBoardCategory.Enabled = false;
            }
            else
            {
                this.ddlBoardCategory.Items.Clear();
                DropDownListUtil.SetDropDownListValue(BoardCategorydt, this.ddlBoardCategory, this.ddlBoardCategory.SelectedValue, DropDownListUtil.DropDownFlag.Select);
            }
            #endregion
        }
        #endregion

        #region [기본셋팅] 파라메타 설정
        private void ParameterSetting()
        {
            strField = WebUtil.SCRequestFormString("FIELD", "ALL");
            strKey = WebUtil.SCRequestFormString("KEY", string.Empty);
            strTxTEndDate = WebUtil.SCRequestFormString("TxTEndDate", string.Empty);
            iPage = IntegerUtil.intPage(WebUtil.SCRequestFormString("ParamPage", string.Empty), 1);
            intPageViewRow = IntegerUtil.intPage(WebUtil.SCRequestFormString("ParamPageViewRow", string.Empty), PageSize);
            strBoardCategory = WebUtil.SCRequestFormString("ddlBoardCategory", "-1");
        }
        #endregion

        #region [게시판] 페이지값 이동 관련
        protected void Page_Move(int Page, PagingHelper PagingHelper)
        {
            PagingHelper1.PageSize = PageSize;
            PagingHelper1.CurrentPageIndex = Page;
            PagingHelper1.RenderPageLink();
        }
        #endregion

        #region [게시판] 페이지값 변경 관련
        protected void PagingHelper1_OnPageIndexChanged(object sender, PagingEventArgs e)
        {
            try
            {
                this.GetListInfo(PageSize, e.PageIndex, strBoardCategory, strField, strKey);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }
        #endregion

        #region [게시판] 게시글 List 객체 담기
        private void GetListInfo(int PagePerData, int CurrentPage, string BoardCategory, string FIELD, string KEY)
        {
            Board_NTx_Dac oWS = new Board_NTx_Dac();

            string strKey = TextControl.TextCutString(HttpUtility.HtmlEncode(KEY), 20, "");
            if(IntegerUtil.intValid(this.ParamPageViewRow.Value, 10) > 10)
            {
                PagePerData = IntegerUtil.intValid(this.ParamPageViewRow.Value, 10);
            }

            List<BoardBean> GetNotiList = oWS.TOTALBBS_BOARD_NOTICE_INFO_SEL(PagePerData, "L");
            List<BoardBean> GetList = oWS.TOTALBBS_BOARD_INFO_SEL(PagePerData, CurrentPage, BoardCategory, "L", "IDX", FIELD, strKey);
            
            GetNotiList.AddRange(GetList);

            int BoardTotalCnt = oWS.TOTALBBS_BOARD_INFO_COUNT_SEL(PagePerData, CurrentPage, BoardCategory, "T", "IDX", FIELD, strKey);
            NoDataTotalCnt = BoardTotalCnt;
            TotalCnt = BoardTotalCnt - ((CurrentPage - 1) * PagePerData);
            this.ltTotalCnt.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", BoardTotalCnt) + "개";

            //Repeater 바인딩
            if (NoDataTotalCnt == 0)
            {
                this.rptGetList.DataSource = GetNotiList;
                this.rptGetList.DataBind();
            }
            else
            {
                this.rptGetList.DataSource = GetNotiList;
                this.rptGetList.DataBind();
            }

            PagingHelper1.VirtualItemCount = NoDataTotalCnt;
            ParamPage.Value = CurrentPage.ToString();
        }
        #endregion

        #region [게시판] 데이터 바인딩
        protected void rptGetList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Literal ltThChkBoxAll = (Literal)e.Item.FindControl("ltThChkBoxAll");
                Literal ltThIdx = (Literal)e.Item.FindControl("ltThIdx");
                Literal ltThBoardCate = (Literal)e.Item.FindControl("ltThBoardCate");
                Literal ltThWriteCate = (Literal)e.Item.FindControl("ltThWriteCate");
                Literal ltThSubject = (Literal)e.Item.FindControl("ltThSubject");
                Literal ltThViewCount = (Literal)e.Item.FindControl("ltThViewCount");
                Literal ltThWriter = (Literal)e.Item.FindControl("ltThWriter");
                Literal ltThRegdate = (Literal)e.Item.FindControl("ltThRegdate");

                ltThIdx.Text = "<input type=\"checkbox\" onclick=\"SelectAllCheckBoxes(this);\" id=\"SelectAllCheckBox\" />";
                ltThBoardCate.Text = "게시판 카테고리";
                ltThWriteCate.Text = "게시글 카테고리";
                ltThSubject.Text = "제목";
                ltThViewCount.Text = "조회수";
                ltThWriter.Text = "작성자";
                ltThRegdate.Text = "등록일";
            }
            // 데이타 처리
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BoardBean GetItems = (BoardBean)e.Item.DataItem;

                Literal ltChkBoxList = (Literal)e.Item.FindControl("ltChkBoxList");
                Literal ltIdx = (Literal)e.Item.FindControl("ltIdx");
                Literal ltBoardCate = (Literal)e.Item.FindControl("ltBoardCate");
                Literal ltWriteCate = (Literal)e.Item.FindControl("ltWriteCate");
                LinkButton lbtSubject = (LinkButton)e.Item.FindControl("lbtSubject");
                Literal ltViewCount = (Literal)e.Item.FindControl("ltViewCount");
                Literal ltWriter = (Literal)e.Item.FindControl("ltWriter");
                Literal ltRegdate = (Literal)e.Item.FindControl("ltRegdate");

                ltChkBoxList.Text = "<input type=\"checkbox\" name=\"ChkBoxList\" id=\"ChkBoxList\" value=\"" + GetItems.intIdx.ToString() + "\" />";
                ltIdx.Text = Convert.ToString(TotalCnt--);

                lbtSubject.Text = GetItems.strSubject;
                lbtSubject.OnClientClick = "if(!FrmView('" + GetItems.intIdx.ToString() + "','')) return false;";

                Common_NTx_Dac ddlSet = new Common_NTx_Dac();
                DataTable AllCategorydt = ddlSet.TOTALBBS_ALL_CATEGORY_INFO_SEL(-1);

                string strBoardCategory = (AllCategorydt.AsEnumerable().Where(p => (p["intIdx"].ToString() == Convert.ToString(GetItems.intBoardCategory)) && (p["chrCateGubun"].ToString() == "B")).Select(p => p["strCateName"].ToString())).FirstOrDefault();
                string strWriteCategory = (AllCategorydt.AsEnumerable().Where(p => (p["intIdx"].ToString() == Convert.ToString(GetItems.intWriteCategory)) && (p["chrCateGubun"].ToString() == "W")).Select(p => p["strCateName"].ToString())).FirstOrDefault();

                //ltBoardCate.Text = Convert.ToString(GetItems.intBoardCategory);
                ltBoardCate.Text = strBoardCategory;
                //ltWriteCate.Text = Convert.ToString(GetItems.intWriteCategory);
                ltWriteCate.Text = strWriteCategory;
                ltWriter.Text = GetItems.strWriter;
                ltViewCount.Text = Convert.ToString(GetItems.intViewCount);
                
                ltRegdate.Text = String.Format("{0:yyyy/MM/dd HH:mm:ss}", GetItems.dateRegDate);
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (NoDataTotalCnt == 0)
                {
                    Literal ltNoData = (Literal)e.Item.FindControl("ltNoData"); //데이타가 없는경우
                    ltNoData.Text = string.Format("<tr><td colspan=\"{ 0}\">{1}</td></tr>", "7", "조회된 데이터가 없습니다.");
                }
            }
        }
        #endregion

        #region [lbtnCreate_Click] 등록 버튼
        protected void lbtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                object obj_BoardCreate = Cache["BoardCreate"];
                object obj_BoardModify = Cache["BoardModify"];

                if (obj_BoardCreate != null)
                {
                    Cache.Remove("BoardCreate");
                }

                if (obj_BoardModify != null)
                {
                    Cache.Remove("BoardModify");
                }

                string[] ParameterData = new string[5];

                ParameterData[0] = strBoardCategory;
                ParameterData[1] = strField;
                ParameterData[2] = strKey;
                ParameterData[3] = WebUtil.SCRequestFormString("ParamIdx", string.Empty);
                ParameterData[4] = WebUtil.SCRequestFormString("ParamPage", string.Empty);

                Cache.Insert("BoardCreate", ParameterData, null, DateTime.Now.AddHours(24), TimeSpan.Zero);

                Response.Redirect("BoardWrite.aspx");
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }
        #endregion

        #region [lbtnBoardCategorySearch_Click] 검색 버튼
        protected void lbtnBoardCategorySearch_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnSearch = (LinkButton)sender;
                if (btnSearch.ID.Equals("lbtnBoardCategorySearch_Click"))
                {
                    strBoardCategory = WebUtil.SCRequestFormString("ddlBoardCategory", "-1");
                    strField = WebUtil.SCRequestFormString("FIELD", "A");
                    strKey = WebUtil.SCRequestFormString("KEY", string.Empty);
                }

                this.GetListInfo(PageSize, 1, strBoardCategory, strField, strKey);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }
        #endregion

        #region [lbtnSearch_Click] 검색 버튼
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnSearch = (LinkButton)sender;
                if (btnSearch.ID.Equals("lbtnSearch"))
                {
                    strBoardCategory = WebUtil.SCRequestFormString("ddlBoardCategory", "-1");
                    strField = WebUtil.SCRequestFormString("FIELD", "A");
                    strKey = WebUtil.SCRequestFormString("KEY", string.Empty);
                }

                this.GetListInfo(PageSize, 1, strBoardCategory, strField, strKey);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }
        #endregion

        #region [게시판] 검색 입력 체크
        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (args.Value.Length > 0)
                {
                    this.GetListInfo(PageSize, 1, strBoardCategory, strField, strKey);
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 하는경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberId);
                #endregion
            }
        }
        #endregion

        #region [ddlBoardCaregory_itemSelected]
        protected void ddlPageViewRow_itemSelected(object sender, EventArgs e)
        {
            try
            {
                int ddlPageViewRowSeletedValue = IntegerUtil.intValid(this.ddlPageViewRow.SelectedValue, 10);
                PagingHelper1.PageSize = ddlPageViewRowSeletedValue;
                this.ParamPageViewRow.Value = ddlPageViewRowSeletedValue.ToString();
                this.GetListInfo(ddlPageViewRowSeletedValue, iPage, strBoardCategory, strField, strKey);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex);
                #endregion
            }
        }
        #endregion
    }
}