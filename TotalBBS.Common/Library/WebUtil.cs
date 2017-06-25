using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;

namespace TotalBBS.Common.Library
{
    public class WebUtil
    {
        #region [문자 존재 여부 체크 공백 추가 NULL 제거]
        /// <summary>
        /// 문자 존재 여부 체크 없는경우 공백 추가 NULL 제거
        /// </summary>
        /// <param name="ReturnValue">값</param>
        /// <returns></returns>
        public static string CkeckString(string ReturnValue)
        {
            return IsNull(ReturnValue) ? "" : ReturnValue;
        }
        #endregion

        #region [문자 존재 여부 체크 없는경우 기본값 ]
        /// <summary>
        /// 문자 존재 여부 체크 없는경우 기본값
        /// </summary>
        /// <param name="ReturnValue">값</param>
        /// <param name="defaultValue">기본값</param>
        /// <returns></returns>	
        public static string CkeckString(string ReturnValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(ReturnValue))
            {
                return defaultValue;
            }

            return ReturnValue;
        }
        #endregion

        #region [문자 존재 여부 체크 없는경우 기본값 공백 추가 NULL 제거]
        /// <summary>
        /// 문자 존재 여부 체크 없는경우 기본값 공백 추가 NULL 제거
        /// </summary>
        /// <param name="ReturnValue">값</param>
        /// <param name="template"></param>
        /// <param name="defaultValue">기본값</param>
        /// <returns></returns>
        public static string CkeckString(string ReturnValue, string template, string defaultValue)
        {
            if (String.IsNullOrEmpty(ReturnValue))
            {
                if (String.IsNullOrEmpty(defaultValue))
                {
                    return string.Empty;
                }

                return template.Replace("#TEMPLATE", defaultValue);
            }

            return template.Replace("#TEMPLATE", ReturnValue);
        }
        #endregion

        #region [자바스크립트 값 존재 여부 체크]
        /// <summary>
        ///  자바스크립트 값 존재 여부 체크
        /// </summary>
        /// <param name="ReturnValue">변수 값</param>
        /// <param name="Message">메시지</param>
        /// <returns></returns>
        public static bool IsNull(string ReturnValue, string Message)
        {
            bool isNull = false;

            if (ReturnValue == null || ReturnValue == "undefined" || ReturnValue == "")
            {
                isNull = true;
                JavaScript.alert(Message);
            }

            return isNull;
        }
        #endregion

        #region [쿠키 값 존재 여부 체크]
        /// <summary>
        /// 쿠키 값 존재 여부 체크
        /// </summary>
        /// <param name="ReturnValue">쿠키 Value</param>
        /// <returns></returns>
        public static bool IsNull(string ReturnValue)
        {
            if (ReturnValue == null || ReturnValue == "")
            {
                return true;
            }

            return false;
        }
        #endregion

        #region [쿠키 값 존재 여부 체크]
        /// <summary>
        /// 글자 길이 초과시 말줄임 ....
        /// </summary>
        /// <param name="ReturnValue"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string ReturnValue, int length)
        {
            if (IsNull(ReturnValue))
            {
                return "";
            }

            if (ReturnValue.Length > length)
            {
                return ReturnValue.Substring(0, length) + "...";
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region QueryString
        /// <summary>
        /// QueryString
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static string ToSQLSafe(string strSQL)
        {
            if (IsNull(strSQL))
            {
                return "";
            }

            return strSQL.Replace("'", "''");
        }

        #region Replace
        /// <summary>
        /// Replace
        /// </summary>
        /// <param name="strRaw"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns>string</returns>
        public static string Replace(string ReturnValue, string oldValue, string newValue)
        {
            if (IsNull(ReturnValue)) return "";

            return Regex.Replace(ReturnValue, oldValue, newValue, RegexOptions.IgnoreCase);
        }
        #endregion

        public static string Reverse(string ReturnValue, char seperate)
        {
            string result = "";
            string[] arrString = ReturnValue.Split(seperate);

            int count = arrString.Length - 1;

            for (int i = count; i >= 0; i--)
            {
                if (i < count)
                {
                    result += seperate;
                }

                result += arrString[i];
            }

            return result;
        }


        public static string Reverse(string ReturnValue)
        {
            return Reverse(ReturnValue, ',');
        }


        public static string ConvertStringToDate(string strDate, string seperate)
        {
            if (IsNull(strDate)) return "";

            if (strDate.Length == 8)
            {
                return strDate.Substring(0, 4) + seperate + strDate.Substring(4, 2) + seperate + strDate.Substring(6, 2);
            }
            else
            {
                return strDate;
            }
        }

        public static string ConvertStringToDate(string strDate)
        {
            if (IsNull(strDate)) return "";

            return ConvertStringToDate(strDate, "-");
        }


        public static string ConvertDateToString(string strDate)
        {
            if (IsNull(strDate)) return "";

            if (strDate.Length > 10)
                strDate = strDate.Substring(0, 10);

            if (strDate.Length == 10)
            {
                return strDate.Substring(0, 4) + strDate.Substring(5, 2) + strDate.Substring(8, 2);
            }
            else
            {
                return strDate;
            }
        }
        #endregion

        #region [HtmlSelect 셋팅]
        /// <summary>
        /// HtmlSelect 셋팅
        /// </summary>
        /// <param name="objSelect"></param>
        /// <param name="val"></param>
        public static void SetSelected(object objSelect, string val)
        {
            HtmlSelect temp = (HtmlSelect)objSelect;

            for (int i = 0; i < temp.Items.Count; i++)
            {
                if (temp.Items[i].Value == val)
                {
                    temp.Items[i].Selected = true;
                }
            }
        }
        #endregion

        #region [리디오 셋팅]
        /// <summary>
        ///  리디오 버턴
        /// </summary>
        /// <param name="objPage"></param>
        /// <param name="strPre"></param>
        /// <param name="nCount"></param>
        /// <returns><returns>
        public static string GetRadioButton(System.Web.UI.Page objPage, string strPre, int nCount)
        {
            string strControlName;

            for (int nLoop = 1; nLoop <= nCount; nLoop++)
            {
                strControlName = strPre + nLoop.ToString();
                System.Web.UI.WebControls.RadioButton objRadioButton = (System.Web.UI.WebControls.RadioButton)objPage.FindControl(strControlName);
                if (null != objRadioButton)
                {
                    if (objRadioButton.Checked)
                    {
                        return nLoop.ToString();
                    }
                }
            }

            return "0";
        }
        #endregion

        #region [RadioButton 셋팅]
        /// <summary>
        /// RadioButton 셋팅
        /// </summary>
        /// <param name="objPage"></param>
        /// <param name="strPre"></param>
        /// <param name="nCount"></param>
        /// <param name="ReturnValue"></param>
        public static void SetRadioButton(System.Web.UI.Page objPage, string strPre, int nCount, string ReturnValue)
        {
            string strControlName;

            for (int nLoop = 1; nLoop <= nCount; nLoop++)
            {
                strControlName = strPre + nLoop.ToString();
                System.Web.UI.WebControls.RadioButton objRadioButton = (System.Web.UI.WebControls.RadioButton)objPage.FindControl(strControlName);
                if (null != objRadioButton)
                {
                    objRadioButton.Checked = (ReturnValue == nLoop.ToString()) ? true : false;
                }
            }
        }
        #endregion

        #region [CheckBox 선택 셋팅]
        /// <summary>
        /// CheckBox 선택 셋팅
        /// </summary>
        /// <param name="objCheckBox"></param>
        /// <returns></returns>
        public static string GetCheckBox(System.Web.UI.WebControls.CheckBox objCheckBox)
        {
            if (objCheckBox.Checked == true)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region [CheckBox 선택 셋팅]
        /// <summary>
        /// CheckBox 선택 셋팅
        /// </summary>
        /// <param name="objCheckBox"></param>
        /// <param name="strChecked"></param>
        public static void SetCheckBox(System.Web.UI.WebControls.CheckBox objCheckBox, string strChecked)
        {
            objCheckBox.Checked = (strChecked == "1") ? true : false;
        }

        #endregion

        #region [DropDownList 셋팅]
        /// <summary>
        /// DropDownList 셋팅
        /// </summary>
        /// <param name="objSelect"></param>
        /// <param name="strSelectedValue"></param>
        public static void SetSelectObject(System.Web.UI.WebControls.DropDownList objSelect, string strSelectedValue)
        {
            for (int nLoop = 0; nLoop < objSelect.Items.Count; nLoop++)
            {
                if (objSelect.Items[nLoop].Value == strSelectedValue)
                {
                    objSelect.SelectedIndex = nLoop;
                    return;
                }
            }
        }
        #endregion

        #region [HTML 디코딩]
        /// <summary>
        /// HTML 디코딩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string DecodeHTML(string strSource)
        {
            return HttpContext.Current.Server.HtmlDecode(strSource);
        }
        #endregion

        #region [HTML 인코딩]
        /// <summary>
        /// HTML 인코딩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string EncodeHTML(string strSource)
        {
            return HttpContext.Current.Server.HtmlEncode(strSource);
        }
        #endregion

        #region [html 인코딩 패턴]
        /// <summary>
        /// html 인코딩 패턴
        /// </summary>
        /// <param name="HtmlSource"></param>
        /// <returns></returns>
        public static string PreventHTML(string HtmlSource)
        {
            HtmlSource = Regex.Replace(HtmlSource, "<applet", "<a pplet", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</applet", "</a pplet", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<meta", "<m eta", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</meta", "</m eta", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<script", "<s cript", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</script", "</s cript", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<onclick", "<o nclick", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</onclick", "</o nclick", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<form", "<f orm", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</form", "</f orm", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<input", "<i nput", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</input", "</i nput", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<xmp", "<x mp", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</xmp", "</x mp", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "<xml", "<x ml", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "</xml", "</x ml", RegexOptions.IgnoreCase);
            HtmlSource = Regex.Replace(HtmlSource, "__VIEWSTATE", "VIEWSTATE", RegexOptions.IgnoreCase);

            return HtmlSource;
        }
        #endregion

        #region [Html 테이블 채크]
        /// <summary>
        /// Html 테이블 채크
        /// </summary>
        /// <param name="HtmlSource"></param>
        /// <returns></returns>
        public static bool CheckTable(string HtmlSource)
        {
            bool success = false;

            string Html = HtmlSource.ToLower();

            string[] arrTable = { "<table", "</table", "<td", "</td", "<tr", "</tr", "<th", "</th" };

            for (int i = 0; i < arrTable.Length; i++)
            {
                string[] tb1 = Regex.Split(Html, arrTable[i++]);
                string[] tb2 = Regex.Split(Html, arrTable[i]);

                if (tb1.Length == tb2.Length)
                {
                    success = true;
                }
            }

            return success;
        }
        #endregion

        #region [Html Replace]
        /// <summary>
        /// Html Replace
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ConvertHTMLtoPlain(string html)
        {
            if (html == null) return "";
            return html.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;");
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static string RemoveInsideBlockTag(string strRaw)
        {
            char aChar;
            bool flag = false;
            StringBuilder buf = new StringBuilder();

            for (int i = 0; i < strRaw.Length; i++)
            {
                aChar = strRaw[i];

                if (aChar == '<')
                {
                    flag = true;
                }
                else if (aChar == '>')
                {
                    flag = false;
                }
                else if (!flag)
                {
                    buf.Append(aChar);
                }
            }

            return buf.ToString();
        }


        public static String RemoveHTMLTag2(String s)
        {
            return s.Replace("(?:<!.*?(?:--.*?--\\s*)*.*?>)|(?:<(?:[^>'\"]*|\".*?\"|'.*?')+>)", "");
        }


        public static String RemoveATag(String s)
        {
            return s.Replace("(?:<\\s*[a|A]\\s*(?:[^>'\"]*|\".*?\"|'.*?')+>)|(?:<\\s*/[a|A]\\s*>)", "");
        }

        #region [Html Replace]
        /// <summary>
        /// Html Replace
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlFilter(string strHtml)
        {
            string tmpHtml = strHtml;

            tmpHtml = tmpHtml.Replace("\\", "\\\\");
            tmpHtml = tmpHtml.Replace("''", "'");
            tmpHtml = tmpHtml.Replace("\r\n", "");
            tmpHtml = tmpHtml.Replace("</script>", "<\\/script>");

            return tmpHtml;
        }
        #endregion

        #region [Html Replace]
        /// <summary>
        /// Html Replace
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlFilterEdit(string strHtml)
        {
            string tmpHtml = strHtml;

            tmpHtml = tmpHtml.Replace("\\", "\\\\");
            tmpHtml = tmpHtml.Replace("\"", "\\\"");
            tmpHtml = tmpHtml.Replace("''", "'");
            tmpHtml = tmpHtml.Replace("\r\n", "");
            tmpHtml = tmpHtml.Replace("</script>", "<\\/script>");

            return tmpHtml;
        }
        #endregion

        #region Cookies
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            HttpCookie userCookie = new HttpCookie(cookieName, cookieValue);

            HttpContext.Current.Response.Cookies.Add(userCookie);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            string cookieValue;

            try { cookieValue = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value); }
            catch { cookieValue = ""; }

            return cookieValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string keyname)
        {
            string cookieValue;

            try { cookieValue = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[cookieName][keyname]); }
            catch { cookieValue = ""; }

            return cookieValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="isSubKey"></param>
        /// <returns></returns>
        public static string GetCookieValueEx(string ParamName, bool isSubKey)
        {
            string ReturnValue;

            if (isSubKey == true)
            {
                ReturnValue = GetCookieValue(ParamName);
            }
            else
            {
                try
                {
                    ReturnValue = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[ParamName].Value);
                }
                catch (Exception)
                {
                    ReturnValue = "";
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSubKey"></param>
        /// <returns></returns>
        public static string GetCookieValue(string strSubKey)
        {
            string ReturnValue;
            try
            {
                ReturnValue = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["LoginInfo"][strSubKey].ToString());
            }
            catch (Exception)
            {
                ReturnValue = "";
            }

            return ReturnValue;
        }
        #endregion

        #region Request get방식 데이터 처리
        /// <summary>
        /// Request get방식 데이터 처리 (null 또는 공백이면 defalut로 처리)
        /// </summary>
        /// <param name="ParamName">Request Item</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Request Value</returns>
        public static string Request(string ParamName, string defaultValue)
        {
            string ReturnValue = HttpContext.Current.Request[ParamName];

            return (ReturnValue == null || ReturnValue == "") ? defaultValue : ReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName">Request Item</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Request Value</returns>
        public static string Request(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request[ParamName];

            if (!string.IsNullOrEmpty(ReturnValue))
            {
                return ReturnValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 파라메타값 추출
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <param name="defaultValue">공백인경우 기본값</param>
        /// <returns></returns>
        public static int RequestInt(string ParamName, int DefaultValue)
        {
            int ReturnValue = 0;

            string strReturnValue = HttpContext.Current.Request[ParamName];

            if (!string.IsNullOrEmpty(strReturnValue))
            {
                Int32.TryParse(strReturnValue, out ReturnValue);

                return ReturnValue;
            }
            else
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 파라메타값 추출
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <returns></returns>
        public static int RequestInt(string ParamName)
        {
            string strReturnValue = HttpContext.Current.Request[ParamName];

            int ReturnValue = 0;

            Int32.TryParse(strReturnValue, out ReturnValue);

            return ReturnValue;
        }

        /// <summary>
        /// 파라메타값 추출
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <returns></returns>
        public static string DecodeRequest(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request[ParamName];

            if (!string.IsNullOrEmpty(ReturnValue))
            {
                return HttpContext.Current.Server.UrlDecode(ReturnValue);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region [get방식 파라메타값 추출] 공백 또는 null인경우 기본값 리턴
        /// <summary>
        /// get방식 파라메타값 추출 공백 또는 null인경우 기본값 리턴
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <param name="defaultValue">기본값</param>
        /// <returns></returns>
        public static string RequestQueryString(string ParamName, string defaultValue)
        {
            string ReturnValue = HttpContext.Current.Request.QueryString[ParamName];

            if (string.IsNullOrEmpty(ReturnValue))
            {
                return defaultValue;
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region [get방식 파라메타값 추출] 공백 또는 null 인경우 string.Empty 리턴
        /// <summary>
        /// get방식 파라메타값  추출
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <returns></returns>
        public static string RequestQueryString(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request.QueryString[ParamName];

            if (string.IsNullOrEmpty(ReturnValue))
            {
                return string.Empty;
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region [get방식 파라메타값  추출 후 숫자 리턴] 숫자 값이 0인경우 기본값을 리턴함
        /// <summary>
        /// get방식 파라메타값  추출 후 숫자 리턴
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <returns></returns>
        public static int RequestQueryInt(string ParamName, int DefaultValue)
        {
            int ReturnValue = 0;

            string strReturnValue = HttpContext.Current.Request.QueryString[ParamName];

            Int32.TryParse(strReturnValue, out ReturnValue);

            if (ReturnValue == 0)
            {
                return DefaultValue;
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region [get방식 파라메타값  추출 후 숫자 리턴]
        /// <summary>
        /// get방식 파라메타값  추출 후 숫자 리턴
        /// </summary>
        /// <param name="ParamName">변수명</param>
        /// <returns></returns>
        public static int RequestQueryInt(string ParamName)
        {
            int ReturnValue = 0;

            string strReturnValue = HttpContext.Current.Request.QueryString[ParamName];

            Int32.TryParse(strReturnValue, out ReturnValue);

            return ReturnValue;
        }
        #endregion

        #region [get 방식 데이터 추출후] 디코딩
        /// <summary>
        /// [get 방식 데이터 추출후] 디코딩
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string DecodeRequestQueryString(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request.QueryString[ParamName];

            if (CkeckString(ReturnValue) != "")
            {
                return HttpContext.Current.Server.UrlDecode(ReturnValue);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region [post 값 리턴] 공백 또는 null 인경우 기본값 리턴
        /// <summary>
        /// [post 값 리턴] 공백 또는 null 인경우 기본값 리턴
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string RequestForm(string ParamName, string defaultValue)
        {
            string ReturnValue = HttpContext.Current.Request.Form[ParamName];

            if (string.IsNullOrEmpty(ReturnValue))
            {
                return defaultValue;
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region [post 값 리턴]
        /// <summary>
        /// post 값 리턴
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string RequestForm(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request.Form[ParamName].ToString();

            return (ReturnValue == null) ? "" : ReturnValue;
        }
        #endregion

        #region [post 값 추출 숫자 값 리턴] 숫자가 아닌경우 기본값 리턴
        /// <summary>
        /// post 데이터 중 숫자값 추출 숫자가 아닌경우 기본값 리턴
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int RequestFormInt(string ParamName, int DefaultValue)
        {
            int ReturnValue = 0;

            string strReturnValue = HttpContext.Current.Request.Form[ParamName];

            Int32.TryParse(strReturnValue, out ReturnValue);

            if (ReturnValue == 0)
            {
                return DefaultValue;
            }
            else
            {
                return ReturnValue;
            }
        }
        #endregion

        #region [post 값 추출 숫자 값 리턴]
        /// <summary>
        /// post 값 추출 숫자 값 리턴
        /// </summary>
        /// <param name="strKey">Request Item</param>
        /// <returns>Request Value</returns>
        public static int RequestFormInt(string ParamName)
        {
            int ReturnValue = 0;

            string strReturnValue = HttpContext.Current.Request.Form[ParamName];

            Int32.TryParse(strReturnValue, out ReturnValue);

            return ReturnValue;
        }
        #endregion

        #region [post 값 추출 후 디코딩]
        /// <summary>
        /// post 값 추출 후 디코딩
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string DecodeRequestForm(string ParamName)
        {
            string ReturnValue = HttpContext.Current.Request.Form[ParamName];

            if (CkeckString(ReturnValue) != "")
            {
                return HttpContext.Current.Server.UrlDecode(ReturnValue);
            }
            else
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="parseString"></param>
        /// <returns></returns>
        public static string RequestFormGetValues(string ParamName, string parseString)
        {
            string itemIDs = "";
            string[] arritemID = HttpContext.Current.Request.Form.GetValues(ParamName);

            if (arritemID != null && arritemID.Length > 0)
            {
                for (int i = 0; i < arritemID.Length; i++)
                {
                    itemIDs += arritemID[i];

                    if (i < arritemID.Length - 1)
                    {
                        itemIDs += parseString;
                    }
                }
            }

            return itemIDs;
        }

        #region [Request 변수 배열 처리]
        /// <summary>
        /// Request 변수 배열 처리
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string RequestFormGetValues(string ParamName)
        {
            StringBuilder sbItemId = new StringBuilder(string.Empty);

            string[] arritemID = HttpContext.Current.Request.Form.GetValues(ParamName);

            if (arritemID != null && arritemID.Length > 0)
            {
                for (int i = 0; i < arritemID.Length; i++)
                {
                    sbItemId.Append(arritemID[i]);

                    if (i < arritemID.Length - 1)
                    {
                        sbItemId.Append(",");
                    }
                }
            }

            return sbItemId.ToString();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="parseString"></param>
        /// <returns></returns>
        public static string RequestGetValues(string ParamName, string parseString)
        {
            string itemIDs = "";
            string[] arritemID = HttpContext.Current.Request.QueryString.GetValues(ParamName);

            if (arritemID != null && arritemID.Length > 0)
            {
                for (int i = 0; i < arritemID.Length; i++)
                {
                    itemIDs += arritemID[i];

                    if (i < arritemID.Length - 1)
                    {
                        itemIDs += parseString;
                    }
                }
            }

            return itemIDs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string RequestGetValues(string ParamName)
        {
            string itemIDs = "";
            string[] arritemID = HttpContext.Current.Request.QueryString.GetValues(ParamName);

            if (arritemID != null && arritemID.Length > 0)
            {
                for (int i = 0; i < arritemID.Length; i++)
                {
                    itemIDs += arritemID[i];

                    if (i < arritemID.Length - 1)
                    {
                        itemIDs += ",";
                    }
                }
            }

            return itemIDs;
        }

        #region [넘어온 Parameter 배열처리]
        /// <summary>
        /// 넘어온 Parameter 배열처리
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static string[] RequestParam(params string[] strParam)
        {
            int intLength = strParam.Length;

            string[] arrResult = new String[intLength];

            for (int i = 0; i < intLength; i++)
            {
                if (HttpContext.Current.Request[strParam[i]] != null && HttpContext.Current.Request[strParam[i]] != String.Empty)
                    arrResult[i] = HttpContext.Current.Request[strParam[i]];
                else
                    arrResult[i] = "";
            }

            return arrResult;
        }
        #endregion

        public static string GetSafeScript(string strSource)
        {
            if (IsNull(strSource)) return "";

            return strSource.Replace("\"", "\\\"").Replace("\n", "").Replace("\r", "");
        }

        #region [현재 도메인 추출]
        public static string GetServerName()
        {
            return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        }
        #endregion

        #region [br 태그 인코딩]
        /// <summary>
        /// Web.Config AppSettings 관련 정보 리턴
        /// </summary>
        /// <param name="key">이름</param>
        /// <returns></returns>
        public static string GetConfiguration(string keyName)
        {
            if (IsNull(keyName))
            {
                return "";
            }
            else
            {
                return System.Configuration.ConfigurationSettings.AppSettings[keyName];
            }
        }
        #endregion

        #region [br 태그 인코딩]
        /// <summary>
        /// br 태그 인코딩
        /// </summary>
        /// <param name="encodeString"></param>
        /// <returns></returns>
        public static string BREncode(string encodeString)
        {
            string strBR = encodeString;
            strBR = Replace(strBR, " ", "&nbsp;");
            strBR = Replace(strBR, "\r\n", "<br />");

            return strBR;
        }
        #endregion

        #region [BR 및 태그 디코딩]
        /// <summary>
        /// BR 및 태그 디코딩
        /// </summary>
        /// <param name="decodeString"></param>
        /// <returns></returns>
        public static string BRDecode(string decodeString)
        {
            string strBR = decodeString;

            strBR = Replace(strBR, "&nbsp;", " ");
            strBR = Replace(strBR, "&amp;nbsp;", " ");
            strBR = Replace(strBR, "<br />", "\r\n");
            strBR = Replace(strBR, "&lt;br&gt;", "\r\n");
            strBR = Replace(strBR, "&lt;br /&gt;", "\r\n");

            return strBR;
        }
        #endregion

        #region [넘어온 Parameter 값을 배열에 담음]
        /// <summary>
        /// 넘어온 Parameter 값을 배열에 담음
        /// </summary>
        /// <param name="strParam">Reques 동적변수</param>
        /// <returns>Request 배열 변수</returns>
        public static string[] getRequest(params string[] strParam)
        {
            int intLength = strParam.Length;

            string[] arrResult = new String[intLength];

            for (int i = 0; i < intLength; i++)
            {
                if (HttpContext.Current.Request[strParam[i]] != String.Empty && HttpContext.Current.Request[strParam[i]] != null)
                {
                    arrResult[i] = HttpContext.Current.Request[strParam[i]];
                }
                else
                {
                    arrResult[i] = null;
                }
            }

            return arrResult;
        }
        #endregion

        #region [업로드 웹 파일 경로]
        /// <summary>
        /// 업로드 웹 파일 경로
        /// </summary>
        /// <returns>파일 웹 경로 위치</returns>
        public static string getHttpRoot(string strServerName)
        {
            return String.Concat("http://", strServerName);
        }
        #endregion

        public static string[] GetRequest(string[] arrRequest)
        {
            int intLen = arrRequest.Length;

            string[] arrReturn = new string[intLen];

            for (int i = 0; i < intLen; i++)
            {
                arrReturn[i] = HttpContext.Current.Request[arrRequest[i]];

                if (arrReturn[i] == null || arrReturn[i] == String.Empty)
                {
                    arrReturn[i] = null;
                }
            }

            return arrReturn;
        }

        public static object GetRequest(string strRequest, object objInit)
        {
            string strParam = HttpContext.Current.Request[strRequest];

            object objValue = objInit;

            if (strParam != null && strParam != String.Empty)
            {
                if (objInit == null) return null;

                switch (objInit.GetType().ToString())
                {
                    case "System.Int32":
                        objValue = int.Parse(strParam);
                        break;
                    case "System.Boolean":
                        objValue = Convert.ToBoolean(strParam);
                        break;
                    case "System.Byte":
                        objValue = Convert.ToByte(strParam);
                        break;
                    default:
                        objValue = strParam;
                        break;
                }
            }

            return objValue;
        }

        public static string GetRequestString(string strRequest, string strInit)
        {
            return (strRequest == null) ? strInit : strRequest;
        }

        public static string[] setData()
        {
            string[] arrTime = new string[48];
            for (int i = 0; i < arrTime.Length; i++)
            {
                arrTime[i] = "<td width='8' height='20' class='basicTD'>&nbsp;</td>";
            }
            return arrTime;
        }

        #region [Server Control Post로 데이타 받기]
        /// <summary>
        /// Server Control Post로 데이타 받기
        /// </summary>
        /// <param name="Key">Server Control ID</param>
        /// <param name="DefaultValue">기본값</param>
        /// <returns></returns>
        public static string SCRequestFormString(string Key, string DefaultValue)
        {
            string ReturnValue = DefaultValue;

            char[] Splits = { '$' };
            string[] NameData;

            NameValueCollection nc = HttpContext.Current.Request.Form;

            for (int i = 0; i < nc.Count; i++)
            {
                if (nc.Keys[i] != null)
                {
                    NameData = nc.Keys[i].ToString().ToUpper().Split(Splits);

                    if (NameData.Length > 0)
                    {
                        for (int d = 0; d <= NameData.Length - 1; d++)
                        {
                            if (NameData[d].Equals(Key.ToUpper()))
                            {
                                ReturnValue = nc[i];
                            }
                        }
                    }
                }
            }

            return ReturnValue;
        }
        #endregion

        #region [Server Control Post로 데이타 받기]
        /// <summary>
        /// Server Control Post로 데이타 받기
        /// </summary>
        /// <param name="Key">Server Control ID</param>
        /// <param name="DefaultValue">기본값</param>
        /// <returns></returns>
        public static string SCRequestFormDefaul(string Key, string DefaultValue)
        {
            string ReturnValue = string.Empty;

            char[] Splits = { '$' };
            string[] NameData;

            NameValueCollection nc = HttpContext.Current.Request.Form;

            for (int i = 0; i < nc.Count; i++)
            {
                if (nc.Keys[i] != null)
                {
                    NameData = nc.Keys[i].ToString().ToUpper().Split(Splits);

                    if (NameData.Length > 0)
                    {
                        for (int d = 0; d <= NameData.Length - 1; d++)
                        {
                            if (NameData[d].Equals(Key.ToUpper()))
                            {
                                ReturnValue = nc[i];
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(ReturnValue))
            {
                return ReturnValue;
            }
            else
            {
                return DefaultValue;
            }


        }
        #endregion
    }
}