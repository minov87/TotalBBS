using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TotalBBS.Common.Library
{
    public class JavaScript
    {

        /// <summary>
        /// JavaScript AccessAlertLocation
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void AccessAlertLocation(string str)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("location.href=\"/BackOffice/Main/Main.aspx\";");
            sbScript.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sbScript);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertBack(string str)
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\">\n" + "alert(\"" + str + "\");\n" + "history.back(-1);\n</script>";
            System.Web.HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <param name="funcName">실행시킬 스크립트 함수</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertBack(string str, string funcName)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("" + funcName + "");
            sbScript.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sbScript);
        }

        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <param name="page">페이지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertBack(string str, System.Web.UI.Page page)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("history.back(-1);\n");
            sbScript.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", sbScript.ToString());
        }

        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <param name="page">페이지</param>
        /// <param name="CommandName">커멘드내임</param>
        public static void alertBack(string str, System.Web.UI.Page page, string CommandName)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("history.back(-1);\n");
            sbScript.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), CommandName, sbScript.ToString());
        }

        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alert(string str)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sbScript.ToString());
        }


        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alert(string str, System.Web.UI.Page page)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", sbScript.ToString());
        }


        /// <summary>
        /// JavaScript Alert
        /// </summary>
        /// <param name="str">경고 메시지</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alert(string str, System.Web.UI.Page page, string CommandName)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("alert(\"" + str + "\");\n");
            sbScript.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), CommandName, sbScript.ToString());
        }


        /// <summary>
        /// 경고후 창 닫기 실행
        /// </summary>
        /// <param name="page"></param>
        public static void alertSelfClose(string str)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append(" alert(\"" + str + "\");\r");
            sbScript.Append(" if (opener) {\r");
            sbScript.Append("       self.close();\r");
            sbScript.Append("  }\r ");
            sbScript.Append("  else {\r");
            sbScript.Append("   opener=self; ");
            sbScript.Append("   opener.close();\r");
            sbScript.Append("  }\r");
            sbScript.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sbScript.ToString());
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }


        /// <summary>
        /// JavaScript Command
        /// </summary>
        /// <param name="str">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void command(string str)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("       " + str + "\r");
            sbScript.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sbScript.ToString());
        }


        /// <summary>
        /// JavaScript Command
        /// </summary>
        /// <param name="str">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void command(string str, System.Web.UI.Page page)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("       " + str + "\r");
            sbScript.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "command", sbScript.ToString());
        }

        /// <summary>
        /// JavaScript Command
        /// </summary>
        /// <param name="str">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void command(string str, System.Web.UI.Page page, string CommandName)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language=\"javascript\" type=\"text/javascript\">\r");
            sbScript.Append("       " + str + "\r");
            sbScript.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), CommandName, sbScript.ToString());
        }

        #region [알럿 문구후 페이지 이동]
        /// <summary>
        /// AlertToLocation
        /// </summary>
        /// <param name="Msg">경고 메시지</param>
        /// <param name="str2">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static string AlertToLocation(string Msg, string PageUrl)
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("<script language=\"javascript\">\r\n");
            sb.Append("alert('" + Msg + "');\r\n");
            sb.Append("window.location.href='" + PageUrl + "';\r\n");
            sb.Append("</script>");

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// JavaScript Alert and Command
        /// </summary>
        /// <param name="str1">경고 메시지</param>
        /// <param name="str2">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertCommand(string str1, string str2)
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\">\n" + "alert(\"" + str1 + "\");\n" + str2 + "</script>";
            System.Web.HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// JavaScript Alert and Command
        /// </summary>
        /// <param name="str1">경고 메시지</param>
        /// <param name="str2">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertCommand(string str1, string str2, System.Web.UI.Page page)
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\">\n" + "alert(\"" + str1 + "\");\n" + str2 + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "alertCommand", js);
        }


        /// <summary>
        /// JavaScript Alert and Command
        /// </summary>
        /// <param name="str1">경고 메시지</param>
        /// <param name="str2">명령문 ';' 까지 입력</param>
        /// <returns>JavaScript 문자열</returns>
        public static void alertCommand(string str1, string str2, System.Web.UI.Page page, string CommandName)
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\">\n" + "alert(\"" + str1 + "\");\n" + str2 + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), CommandName, js);
        }

        /// <summary>
        /// 창 닫기 실행
        /// </summary>
        public static void selfClose()
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\"> if (opener) { self.close(); } else { opener=self; opener.close(); } </script>";
            System.Web.HttpContext.Current.Response.Write(js);
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 창 닫기 실행
        /// </summary>
        /// <param name="page"></param>
        public static void selfClose(System.Web.UI.Page page)
        {
            string js = "<script language=\"javascript\" type=\"text/javascript\">\n if (opener) { self.close(); } else { opener=self; opener.close(); } </script>";

            page.ClientScript.RegisterStartupScript(page.GetType(), "selfClose", js);
            System.Web.HttpContext.Current.Response.End();
        }

        #region [로그인 여부 체크]
        /// <summary>
        /// 로그인 여부 체크
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="msg"></param>
        public static bool IsLogin(int AuthNo, string msg)
        {
            if (AuthNo == -1)
            {
                JavaScript.alertCommand(msg, "top.location.href='/LoginInfo/';");

                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        /*//
        ' Sub ID		: PrintPage
        ' Description	: 페이지 단위처리
        ' Param			: page			-	페이지 번호	
        ' Param			: page_size		-	페이지 사이즈
        ' Param			: page_cnt		-	페이지 개수
        ' Param			: url			-	이동할 URL	
        */
        public static string PrintPage(int page, int page_size, int page_cnt, string url)
        {

            StringBuilder sbPaging = new StringBuilder(64);

            int n_page, s_page, e_page;

            n_page = Convert.ToInt32(((page - 1) / page_size) + 1); //현재 페이지의 페이지단위 처리시 위치
            s_page = (n_page - 1) * page_size + 1;			//작 페이지
            e_page = n_page * page_size;					//종료 페이지
            if (e_page > page_cnt)
            {
                e_page = page_cnt;
            }

            //이전 출력
            if (page > 1)
            {
                if (page > page_size)
                {
                    sbPaging.Append("<a href=\"" + url + "1');\" class=\"prevEnd\"></a>\r");
                    sbPaging.Append("<a href=\"" + url + (s_page - 1) + "');\" class=\"prev\"></a>\r");
                }
                else
                {
                    sbPaging.Append("<a href=\"javascript:void(0);\" class=\"prevEnd\"></a>\r");
                    sbPaging.Append("<a href=\"javascript:void(0);\" class=\"prev\"></a>\r");
                }
            }
            else
            {
                sbPaging.Append("<a href=\"javascript:void(0);\" class=\"prevEnd\"></a>\r<a href=\"#\" class=\"prev\"></a>\r");
            }

            //페이지 번호 리스트 출력

            for (int i = s_page; i <= e_page; i++)
            {
                if (i == page)
                {
                    if (i == s_page)
                    {
                        sbPaging.Append("<strong >" + i + "</strong>");
                    }
                    else
                    {
                        sbPaging.Append("<strong>" + i + "</strong>");
                    }
                }
                else
                {
                    if (i == s_page)
                    {
                        sbPaging.Append("<a href=\"" + url + i + "');\" >" + i + "</a>\r");
                    }
                    else
                    {
                        sbPaging.Append("<a href=\"" + url + i + "');\" >" + i + "</a>\r");
                    }
                }
            }

            //다음 출력
            if (page < page_cnt)
            {
                if (s_page + page_size <= page_cnt)
                {
                    sbPaging.Append("<a href=\"" + url + (s_page + page_size) + "');\" class=\"next\"></a>\r");
                    sbPaging.Append("<a href=\"" + url + page_cnt + "');\" class=\"nextEnd\"></a>\r");
                }
                else
                {
                    sbPaging.Append("<a href=\"javascript:void(0);\" class=\"next\"></a>\r");
                    sbPaging.Append("<a href=\"javascript:void(0);\" class=\"nextEnd\"></a>\r");
                }
            }
            else
            {
                sbPaging.Append("<a href=\"javascript:void(0);\" class=\"next\"></a>\r");
                sbPaging.Append("<a href=\"javascript:void(0);\" class=\"nextEnd\"></a>");
            }

            return sbPaging.ToString();
        }

        public static int PageCnt(int TotalCnt, int PageSize)
        {
            int PageCnt = 1;
            int iTmp = 0;
            int tmp = 0;
            int Result = 0;

            if (TotalCnt < 1)
            {
                TotalCnt = 1;
            }
            iTmp = TotalCnt / PageSize;

            tmp = Math.DivRem(TotalCnt, PageSize, out Result);

            try
            {
                if (tmp > 0)
                {
                    PageCnt = iTmp + 1;
                }
                else
                {
                    if (iTmp < 1)
                    {
                        PageCnt = 1;
                    }
                    else
                    {
                        PageCnt = iTmp;
                    }
                }
            }
            catch
            {
                PageCnt = 1;
            }
            return PageCnt;
        }
    }
}