using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TotalBBS.Common.Dac.Common;
using TotalBBS.Common.Library;

namespace TotalBBS.BackOffice.Board
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlForm form1 = (HtmlForm)Master.FindControl("form1");

                form1.Method = "post";
                form1.Enctype = "multipart/form-data";
                //form1.Action = "Test.aspx";

                Common_NTx_Dac ddlSet = new Common_NTx_Dac();

                // 게시판 카테고리 필드
                DataTable BoardCategorydt = null;
                BoardCategorydt = ddlSet.TOTALBBS_BOARD_CATEGORY_INFO_SEL(0);
                int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, 0);

                // 게시판 카테고리 필드 - DropDownList
                if (BoardCategorydt.Rows.Count == 0)
                {
                    this.ddlWriteCategory.Items.Clear();
                    this.ddlBoardCategory.Items.Add(new ListItem("등록된 카테고리가 없습니다.", ""));
                    this.ddlBoardCategory.Enabled = false;
                }
                else
                {
                    this.ddlBoardCategory.Items.Clear();
                    //this.ddlBoardCategory.Items.Add(new ListItem("선택하세요", "0")); // 기본값
                    //DropDownListUtil.SetDropDownList(BoardCategorydt, this.ddlBoardCategory, "strCateName", "intIdx");
                    DropDownListUtil.SetDropDownListValue(BoardCategorydt, this.ddlBoardCategory, this.ddlBoardCategory.SelectedValue, DropDownListUtil.DropDownFlag.Select);
                }
            }
        }

        protected void ddlBoardCaregory_itemSelected(object sender, EventArgs e)
        {
            try
            {
                int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, 0);
                this.ddlWriteCategory.Enabled = true;

                Common_NTx_Dac ddlSet = new Common_NTx_Dac();
                DataTable WriteCategorydt = null;

                WriteCategorydt = ddlSet.TOTALBBS_WRITE_CATEGORY_INFO_SEL(ddlBoardCategorySeletedValue);

                // 게시글 카테고리 필드 - DropDownList
                if (WriteCategorydt.Rows.Count == 0)
                {
                    this.ddlWriteCategory.Items.Clear();
                    this.ddlWriteCategory.Items.Add(new ListItem("등록된 카테고리가 없습니다.", ""));
                    this.ddlWriteCategory.Enabled = false;
                }
                else
                {
                    this.ddlWriteCategory.Items.Clear();
                    this.ddlWriteCategory.Items.Add(new ListItem("선택하세요", "0")); // 기본값
                    DropDownListUtil.SetDropDownList(WriteCategorydt, this.ddlWriteCategory, "strCateName", "intIdx");
                }
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex);
                #endregion
            }
        }
    }
}