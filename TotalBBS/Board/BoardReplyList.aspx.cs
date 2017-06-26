using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TotalBBS.Board
{
    public partial class BoardReplyList : System.Web.UI.Page
    {
        //페이징
        private int iPage = 1;
        private const int PageNo = 1;
        private const int PageSize = 10;
        private int TotalCnt = 0;           // 총 레코드 갯수(글번호 순서 정렬용)
        private int NoDataTotalCnt = 0;     // 총 레코드 갯수(글번호 순서 정렬용)
        private string FileUploadPath = "Board"; // 파일 업로드 폴더 경로 업로드 및 삭제 하기위함

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {/*
                this.GetPageDefaultSetting();
                this.ltTitle.Text = "댓글 관리";
                PagingHelper1.PageSize = PageSize;*/
            }
            catch (Exception ex)
            {

            }
        }
    }
}