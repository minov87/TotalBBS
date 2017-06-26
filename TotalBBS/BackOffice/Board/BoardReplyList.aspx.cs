using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotalBBS.Common.Bean.Board;
using TotalBBS.Common.Dac.Board;
using TotalBBS.Common.Library;
using TotalBBS.Common.WebBase;
using TotalBBS.Common.WebLib;

namespace TotalBBS.BackOffice.Board
{
    public partial class BoardReplyList : Page
    {
        private string strField = string.Empty;
        private string strKey = string.Empty;
        private int intIdx = -1;

        //페이징
        private int iPage = 1;
        private const int PageNo = 1;
        private const int PageSize = 10;
        private int TotalCnt = 0;           // 총 레코드 갯수(글번호 순서 정렬용)
        private int NoDataTotalCnt = 0;     // 총 레코드 갯수(글번호 순서 정렬용)
        private int intPageViewRow = PageSize;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.GetPageDefaultSetting();
                this.ltTitle.Text = "댓글 관리";
                PagingHelper1.PageSize = PageSize;

                if (!IsPostBack)
                {
                    this.ParameterSetting();
                    {
                        if (Request.QueryString["Page"] != null)
                        {
                            //페이지값을 유지하기위해서
                            iPage = IntegerUtil.intValid(Request.QueryString["Page"].ToString(), 1);
                            this.GetListInfo(PageSize, iPage, intIdx);
                            Page_Move(iPage, PagingHelper1);
                        }
                        else
                        {
                            this.GetListInfo(PageSize, 1, intIdx);
                            Page_Move(1, PagingHelper1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sbError = new StringBuilder();
                sbError.Append("alert('데이터 조회에 실패하였습니다.');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageError", sbError.ToString(), true);
            }
        }


        private void GetPageDefaultSetting()
        {
            this.ltTitle.Text = "댓글 관리";
            this.ltTotalPosts.Text = "전체 댓글 수";
        }

        private void ParameterSetting()
        {
            strField = WebUtil.SCRequestFormString("FIELD", "ALL");
            strKey = WebUtil.SCRequestFormString("KEY", string.Empty);
            iPage = IntegerUtil.intPage(WebUtil.SCRequestFormString("ParamPage", string.Empty), 1);
            intPageViewRow = IntegerUtil.intPage(WebUtil.SCRequestFormString("ParamPageViewRow", string.Empty), PageSize);
            intIdx = WebUtil.RequestInt("intIdx", 0);
        }

        protected void Page_Move(int Page, PagingHelper PagingHelper)
        {
            PagingHelper1.PageSize = PageSize;
            PagingHelper1.CurrentPageIndex = Page;
            PagingHelper1.RenderPageLink();
        }

        protected void PagingHelper1_OnPageIndexChanged(object sender, PagingEventArgs e)
        {
            try
            {
                this.GetListInfo(PageSize, e.PageIndex, intIdx);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }

        private void GetListInfo(int PagePerData, int CurrentPage, int intIdx)
        {
            Board_NTx_Dac oWS = new Board_NTx_Dac();

            if (IntegerUtil.intValid(this.ParamPageViewRow.Value, 10) > 10)
            {
                PagePerData = IntegerUtil.intValid(this.ParamPageViewRow.Value, 10);
            }

            List<BoardReplyBean> GetList = oWS.TOTALBBS_BOARD_REPLY_INFO_SEL(PagePerData, CurrentPage, "L", intIdx);

            int BoardTotalCnt = oWS.TOTALBBS_BOARD_REPLY_INFO_COUNT_SEL(PagePerData, CurrentPage, "T", intIdx);
            NoDataTotalCnt = BoardTotalCnt;
            TotalCnt = BoardTotalCnt - ((CurrentPage - 1) * PagePerData);
            this.ltTotalCnt.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", BoardTotalCnt) + "개";

            //Repeater 바인딩
            if (NoDataTotalCnt == 0)
            {
                this.rptGetList.DataSource = GetList;
                this.rptGetList.DataBind();
            }
            else
            {
                this.rptGetList.DataSource = GetList;
                this.rptGetList.DataBind();
            }

            PagingHelper1.VirtualItemCount = NoDataTotalCnt;
            ParamPage.Value = CurrentPage.ToString();
        }

        protected void rptGetList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // 데이타 처리
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BoardReplyBean GetItems = (BoardReplyBean)e.Item.DataItem;

                Literal ltThWriter = (Literal)e.Item.FindControl("ltThWriter");
                Literal ltContent = (Literal)e.Item.FindControl("ltContent");
                Literal lbtThRegDate = (Literal)e.Item.FindControl("lbtThRegDate");

                ltThWriter.Text = GetItems.strWriter;
                ltContent.Text = GetItems.strContent;
                lbtThRegDate.Text = GetItems.dateRegDate;
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (NoDataTotalCnt == 0)
                {
                    Literal ltNoData = (Literal)e.Item.FindControl("ltNoData"); //데이타가 없는경우
                    ltNoData.Text = "<tr><td colspan=2>조회된 데이터가 없습니다.</td></tr>";
                }
            }
        }
    }
}