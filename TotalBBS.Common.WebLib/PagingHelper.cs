using System;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;

namespace TotalBBS.Common.WebLib
{
    [ToolboxData("<{0}:PagingHelper runat=server></{0}:PagingHelper>"), Designer(typeof(TotalBBS.Common.WebLib.PagingHelperDesigner))]
    public class PagingHelper : WebControl, INamingContainer
    {
        public delegate void PagingEventHandler(object sender, PagingEventArgs e);
        public event PagingEventHandler OnPageIndexChanged;

        private int currentPageIndex, pageSize, totalRecordCount;
        private int pageCount;

        private string next, prev, next10, prev10;
        private string nextImg_on;
        private string nextImg_off;
        private string nextBlockImg_on, nextBlockImg_off;
        private string prevImg_on, prevImg_off;
        private string prevBlockImg_on, prevBlockImg_off;

        private Color currnetNumberColor;
        private LinkButton lb;	//페이징 링크용 버튼

        public PagingHelper()
        {
            currentPageIndex = 1;
            pageSize = 10;
            totalRecordCount = 100;

            next = ">";
            prev = "<";
            next10 = ">>";
            prev10 = "<<";

            currnetNumberColor = Color.Silver;
        }

        // 이전, 다음용 이미지 경로 지정하는 속성들 정의부
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string NextImageUrlA
        {
            get { return nextImg_on; }
            set { nextImg_on = value; }
        }

        // 다음 페이지를 위한 비활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string NextImageUrlB
        {
            get { return nextImg_off; }
            set { nextImg_off = value; }
        }

        // 다음 블럭 페이지를 위한 활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string Next10ImageUrlA
        {
            get { return nextBlockImg_on; }
            set { nextBlockImg_on = value; }
        }

        // 다음 블럭 페이지를 위한 비활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string Next10ImageUrlB
        {
            get { return nextBlockImg_off; }
            set { nextBlockImg_off = value; }
        }

        // 이전 페이지를 위한 활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string PrevImageUrlA
        {
            get { return prevImg_on; }
            set { prevImg_on = value; }
        }

        // 이전 페이지를 위한 비활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string PrevImageUrlB
        {
            get { return prevImg_off; }
            set { prevImg_off = value; }
        }

        // 이전 블럭 페이지를 위한 활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string Prev10ImageUrlA
        {
            get { return prevBlockImg_on; }
            set { prevBlockImg_on = value; }
        }

        // 이전 블럭 페이지를 위한 비활성 이미지의 상대 경로
        [
         Editor("System.Web.UI.Design.ImageUrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        ]
        public string Prev10ImageUrlB
        {
            get { return prevBlockImg_off; }
            set { prevBlockImg_off = value; }
        }

        // public 속성 정의 구역
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                ViewState["PageSize"] = pageSize;
            }
        }

        public int VirtualItemCount
        {
            get { return totalRecordCount; }
            set
            {
                totalRecordCount = value;
                ViewState["VirtualItemCount"] = totalRecordCount;
            }
        }

        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                ViewState["currentPageIndex"] = value;
                currentPageIndex = (int)ViewState["currentPageIndex"];
            }
        }

        public string NextText
        {
            get { return next; }
            set { next = value; }
        }

        public string PrevText
        {
            get { return prev; }
            set { prev = value; }
        }

        public string NextBlockText
        {
            get { return next10; }
            set { next10 = value; }
        }

        public string PrevBlockText
        {
            get { return prev10; }
            set { prev10 = value; }
        }

        public Color CurrnetNumberColor
        {
            get { return currnetNumberColor; }
            set { currnetNumberColor = value; }
        }

        public void PageIndexChanged(object sender)
        {
            if (OnPageIndexChanged != null)
                OnPageIndexChanged(sender, new PagingEventArgs(currentPageIndex));
        }

        public void PageMove(int PageIndex)
        {
            this.CurrentPageIndex = PageIndex;
            this.RenderPageLink();
        }

        protected override void Render(HtmlTextWriter output)
        {
            this.EnsureChildControls();
            base.Render(output);
        }

        protected override void CreateChildControls()
        {
            if (ViewState["currentPageIndex"] != null)
                currentPageIndex = (int)ViewState["currentPageIndex"];

            if (ViewState["PageSize"] != null)
                pageSize = (int)ViewState["PageSize"];
            else
                ViewState["PageSize"] = pageSize;

            if (ViewState["VirtualItemCount"] != null)
                totalRecordCount = (int)ViewState["VirtualItemCount"];
            else
                ViewState["VirtualItemCount"] = totalRecordCount;

            RenderPageLink();
        }

        public void RenderPageLink()
        {
            int Page_Size = 10;
            Controls.Clear();

            int page = currentPageIndex;
            int currentBlodkEndNo;		// 현재 블럭에서의 페이지 단위 값
            int endNum;					// 현재 페이지 번호의 뒷자리 값
            int endNoOfLoop;			// 페이지 블럭에서 마지막 페이지 번호의 값		

            string tempStr, tempImgStr;

            // 총 페이지 수 구하기
            pageCount = ((totalRecordCount - 1) / pageSize) + 1;

            string strPage = page.ToString();

            // 현재 페이지 번호의 뒷자리 값 구하기
            endNum = (int.Parse(strPage) % Page_Size);

            // 현재 자신의 페이지 블럭에서 마지막 페이지 값 구하기
            if ((page % 10) == 0)
            {
                currentBlodkEndNo = page;
            }
            else
            {
                currentBlodkEndNo = (page + Page_Size) - endNum;   // 13 + 10 - 3  / 23 + 10 -3
            }

            // 페이지 블럭에서 마지막 페이지 번호 값 구하기
            if (pageCount > currentBlodkEndNo)
            {
                endNoOfLoop = currentBlodkEndNo;
            }
            else
            {
                endNoOfLoop = pageCount;
            }

            int NowPage = ((page - 1) / Page_Size) + 1;
            int StartPage = (NowPage - 1) * Page_Size + 1;
            int EndPage = NowPage * Page_Size;

            if (EndPage > pageCount)
            {
                EndPage = pageCount;
            }

            lb = new LinkButton();
            lb.CausesValidation = false;
            lb.Command += new CommandEventHandler(this.btnPage_Command);

            // 이전 10개 기능 적용
            if(pageCount > 10) 
            {
                if (currentBlodkEndNo > 10)
                {
                    lb.CommandArgument = (page - 10 - endNum + 1).ToString();
                    lb.Text = prevBlockImg_on == null ? prev10 : "<img src=\"" + prevBlockImg_on + "\" border=\"0\">";

                    Controls.Add(lb);
                }
                else
                {
                    tempStr = "<font color=silver>" + prev10 + "</font>";
                    tempImgStr = "<img src=\"" + prevBlockImg_off + "\" border=\"0\">";
                    Controls.Add(new LiteralControl(prevBlockImg_off == null ? tempStr : tempImgStr));
                }

                //구분 공백 삽입
                Controls.Add(new LiteralControl(" "));
            }

            lb = new LinkButton();
            lb.CausesValidation = false;
            lb.Command += new CommandEventHandler(this.btnPage_Command);

            // 이전 페이지로 가기 기능 적용
            if (page <= 1)
            {
                tempStr = "<font color=silver>" + prev + "</font>";
                tempImgStr = "<img src=\"" + prevImg_off + "\" border=\"0\">";
                Controls.Add(new LiteralControl(prevImg_off == null ? tempStr : tempImgStr));
            }
            else
            {
                lb.CommandArgument = (page - 1).ToString();
                lb.Text = prevImg_on == null ? prev : "<img src=\"" + prevImg_on + "\" border=\"0\">";

                Controls.Add(lb);
            }
            // 구분 공백 삽입
            Controls.Add(new LiteralControl(" "));

            // 1,2,3,4,5,6,7,8,9,10
            for (int i = StartPage; i <= EndPage; i++)
            {
                if (i == page)
                {
                    Controls.Add(new LiteralControl("<strong>" + i + "</strong>"));
                }
                else
                {
                    lb = new LinkButton();
                    lb.CausesValidation = false;
                    lb.Command += new CommandEventHandler(this.btnPage_Command);
                    lb.CommandArgument = i.ToString();
                    lb.Text = " " + i.ToString() + " ";
                    Controls.Add(lb);
                }
            }

            lb = new LinkButton();
            lb.CausesValidation = false;
            lb.Command += new CommandEventHandler(this.btnPage_Command);

            // 구분 공백 삽입
            Controls.Add(new LiteralControl(" "));

            // 다음 페이지로 가기 기능 적용
            if (page == pageCount)
            {
                tempStr = "<font color=silver>" + next + "</font>";
                tempImgStr = "<img src=\"" + nextImg_off + "\" border=\"0\">";
                Controls.Add(new LiteralControl(nextImg_off == null ? tempStr : tempImgStr));
            }
            else
            {
                lb.CommandArgument = (page + 1).ToString();
                lb.Text = nextImg_on == null ? next : "<img src=\"" + nextImg_on + "\" border=\"0\">";

                Controls.Add(lb);
            }
            // 구분 공백 삽입
            Controls.Add(new LiteralControl(" "));

            if(pageCount > 10)  
            {
                //구분 공백 삽입
                Controls.Add(new LiteralControl(" "));

                lb = new LinkButton();
                lb.CausesValidation = false;
                lb.Command += new CommandEventHandler(this.btnPage_Command);


                if (pageCount > currentBlodkEndNo)
                {
                    lb.CommandArgument = (currentBlodkEndNo + 1).ToString();
                    lb.Text = nextBlockImg_on == null ? next10 : "<img src=\"" + nextBlockImg_on + "\" border=\"0\">";

                    Controls.Add(lb);
                }
                else
                {
                    tempStr = "<font color=silver>" + next10 + "</font>";
                    tempImgStr = "<img src=\"" + nextBlockImg_off + "\" border=\"0\">";
                    Controls.Add(new LiteralControl(nextBlockImg_off == null ? tempStr : tempImgStr));
                }
            }
        }

        protected void btnPage_Command(object sender, CommandEventArgs e)
        {
            string page = ((LinkButton)sender).CommandArgument;
            int iPage = int.Parse(page);

            if (iPage < 1)
            {
                iPage = 1;
            }

            if (iPage > pageCount)
            {
                iPage = pageCount;
            }

            ((LinkButton)sender).CommandArgument = iPage.ToString();

            currentPageIndex = iPage ;
            ViewState["currentPageIndex"] = currentPageIndex;

            RenderPageLink();
            PageIndexChanged(sender);
        }
    }

    public class PagingEventArgs : EventArgs
    {
        protected int pageIndex;

        public PagingEventArgs(int page)
            : base()
        {
            pageIndex = page;
        }

        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
        }
    }

    public class PagingHelperDesigner : ControlDesigner
    {
        public PagingHelperDesigner() { }

        public override string GetDesignTimeHtml()
        {
            PagingHelper ph = (PagingHelper)Component;

            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            if (ph.VirtualItemCount == 0) ph.VirtualItemCount = 100;
            ph.RenderPageLink();
            ph.RenderControl(tw);

            return sw.ToString();
        }
    }
}