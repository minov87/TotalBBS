using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TotalBBS.Common.Library;

namespace TotalBBS.Common.WebBase
{
    public class TBHttpContext : Page
    {
        private HttpContext current = null;
        private static string hmacKey = "dldkdldkaghghk";
        private static byte[] hmacKeyByte = new ASCIIEncoding().GetBytes(hmacKey);
        private static string loginCookieName = "LoginInfo";
        private static readonly Encoding ms949Encoding = Encoding.GetEncoding(0x3b5);

        internal TBHttpContext()
        {
            this.current = HttpContext.Current;
            if (this.current.Equals(null))
            {
                throw new InvalidOperationException("Cannot read HttpContext");
            }
        }
        private string GetCookieHMAC(string cookieName)
        {
            byte[] inArray = null;
            byte[] buffer = null;
            string s = string.Empty;
            s = HttpUtility.UrlDecode(this.current.Request.Cookies[loginCookieName].Value);
            buffer = new UTF8Encoding().GetBytes(s);
            inArray = new HMACSHA1(hmacKeyByte).ComputeHash(buffer);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        private string GetMemberInfo(string key)
        {
            if ((key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Valid arguement value required.", "key");
            }
            if (this.current == null)
            {
                throw new InvalidOperationException("HttpContext not exists.");
            }
            if (this.current.Items[key] != null)
            {
                return this.current.Items[key].ToString();
            }

            string ReturnValue = string.Empty;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string str2 = ticket.UserData;

                if (str2.Length == 0)
                {
                    return string.Empty;
                }
                int index = str2.IndexOf(key + "=");
                if (index < 0)
                {
                    return string.Empty;
                }
                if (!str2.EndsWith("&"))
                {
                    str2 = str2 + "&";
                }
                int startIndex = (index + key.Length) + 1;
                int length = str2.IndexOf("&", startIndex) - startIndex;
                ReturnValue = str2.Substring(startIndex, length);
            }

            return ReturnValue;
        }

        public virtual bool IsExpire()
        {
            return (((this.ExpireDate == null) || (this.ExpireDate == "")) || (Convert.ToDateTime(this.ExpireDate) < DateTime.Now));
        }

        public virtual bool IsLogged()
        {
            if (LoginBool)
            {
                if (!string.IsNullOrEmpty(this.MemberId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            HttpCookie cookie = this.current.Response.Cookies[loginCookieName];
            if (cookie != null)
            {
                cookie.Domain = "totalbbs.com";         //Request.Url.Authority;
                cookie.Expires = DateTime.MinValue;
            }
            cookie = this.current.Response.Cookies["hmac"];
            if (cookie != null)
            {
                cookie.Domain = "totalbbs.com";         //Request.Url.Authority;
                cookie.Expires = DateTime.MinValue;
            }
        }

        public void SetCookie(string cookieName, string[] cookieValues)
        {
            this.SetCookie(cookieName, cookieValues, '|');
        }

        public void SetCookie(string cookieName, int cookieIndex, string cookieValue)
        {
            this.SetCookie(cookieName, cookieIndex, cookieValue, '|');
        }

        public void SetCookie(string cookieName, string[] cookieValues, char separator)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < cookieValues.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(separator);
                }
                builder.Append(HttpUtility.UrlEncodeUnicode(cookieValues[i]));
            }
            if (builder.Length > 0)
            {
                HttpCookie cookie = this.current.Response.Cookies[cookieName];
                if (cookie == null)
                {
                    cookie = new HttpCookie(cookieName);
                }
                cookie.Value = builder.ToString();
                cookie.Path = "/";
                cookie.Domain = "totalbbs.com";   //Request.Url.Authority;
                this.current.Response.Cookies.Add(cookie);
                this.SetCookieHMAC();
            }
            else
            {
                this.SetCookieExpire(cookieName);
            }
        }

        public void SetCookie(string cookieName, int cookieIndex, string cookieValue, char separator)
        {
            int length = 0;
            int startIndex = 0;
            HttpCookie cookie = this.current.Request.Cookies[cookieName];
            try
            {
                if ((cookie != null) && (cookie.Value.Trim().Length != 0))
                {
                    for (int i = 0; i < cookieIndex; i++)
                    {
                        length = cookie.Value.IndexOf(separator, length + 1);
                    }
                    if (length >= 0)
                    {
                        string str = cookie.Value.Substring(0, length) + separator + cookieValue;
                        startIndex = cookie.Value.IndexOf(separator, length + 1);
                        if (startIndex >= 0)
                        {
                            str = str + cookie.Value.Substring(startIndex);
                        }
                        new HttpCookie(cookieName);
                        cookie.Value = str;
                        cookie.Path = "/";
                        cookie.Domain = "totalbbs.com"; //Request.Url.Authority;;
                        this.current.Response.Cookies.Add(cookie);
                        this.SetCookieHMAC();
                    }
                }
            }
            catch
            {
            }
        }

        public void SetCookieExpire(string cookieName)
        {
            HttpCookie cookie = this.current.Response.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Domain = "totalbbs.com"; //Request.Url.Authority;
                cookie.Expires = DateTime.MinValue;
            }
        }

        public void SetCookieHMAC()
        {
            string cookieHMAC = string.Empty;
            cookieHMAC = this.GetCookieHMAC(loginCookieName);
            HttpCookie cookie = this.current.Request.Cookies["hmac"];
            if (cookie == null)
            {
                cookie = new HttpCookie("hmac");
                cookie.Value = HttpUtility.UrlEncode(cookieHMAC);
                cookie.Domain = "totalbbs.com"; //Request.Url.Authority;
                this.current.Response.Cookies.Add(cookie);
            }
            else
            {
                cookie.Value = HttpUtility.UrlEncode(cookieHMAC);
                cookie.Domain = "totalbbs.com"; //Request.Url.Authority;
                this.current.Response.Cookies.Add(cookie);
            }
        }

        public virtual void SetExpireDate()
        {
            string str = DateTime.Now.AddHours(3.0).ToString("yyyy-MM-dd HH:mm:ss");
            string data = string.Concat(new object[] 
            {
                "MemberId=", this.MemberId, "&MemberNm=", this.MemberNm, "&MemberAuth=",this.MemberAuth,
                "&MemberDepart=",this.MemberDepart, "&Expire=", DateTime.Now.AddDays(1)
            });

            //str

            data = HttpUtility.UrlEncode(new TripleDES().EncryptData(data));
            HttpCookie cookie = this.current.Response.Cookies[loginCookieName];
            cookie.Domain = "totalbbs.com"; // Request.Url.Authority;
            cookie.Value = data;
            this.current.Response.Cookies.Set(cookie);
            this.SetCookieHMAC();
        }

        private void SetMemberInfo(string key, string value)
        {
            bool flag = false;
            if ((key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Valid arguement value required.", "key");
            }
            if (this.current == null)
            {
                throw new InvalidOperationException("HttpContext not exists.");
            }
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string str = ticket.UserData;
            string data = "";
            if ((str != null) && (str.Length != 0))
            {
                string str3 = str;
                if ((str3 != null) && (str3.Length != 0))
                {
                    string[] strArray2 = str3.Split(new char[] { '&' });
                    foreach (string str6 in strArray2)
                    {
                        if (str6.Trim() != "")
                        {
                            string[] strArray = str6.Split(new char[] { '=' });
                            string str4 = strArray[0];
                            string str5 = strArray[1];
                            if (str4 == key)
                            {
                                str5 = value;
                                flag = true;
                            }
                            if (data.Length == 0)
                            {
                                data = str4 + '=' + str5;
                            }
                            else
                            {
                                data = string.Concat(new object[] { data, '&', str4, '=', str5 });
                            }
                        }
                    }
                    if (!flag)
                    {
                        if (data.Length == 0)
                        {
                            data = key + '=' + value;
                        }
                        else
                        {
                            data = string.Concat(new object[] { data, '&', key, '=', value });
                        }
                    }
                }
                else
                {
                    data = key + '=' + value;
                }
            }

            if (!string.IsNullOrEmpty(data))
            {
                data = new TripleDES().EncryptData(data);

                this.current.Response.Cookies[loginCookieName].Value = HttpUtility.UrlEncode(data);
                this.current.Response.Cookies[loginCookieName].Path = "/";
                this.current.Response.Cookies[loginCookieName].Domain = "totalbbs.com"; //Request.Url.Authority;
            }

            this.SetCookieHMAC();
        }

        #region [만료일]
        public virtual string ExpireDate
        {
            get
            {
                return this.GetMemberInfo("expire");
            }
        }
        #endregion

        #region [로그인 인증 여부 체크] LoginBool
        public virtual bool LoginBool
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        #endregion

        #region [부서명] MemberDepart
        public virtual string MemberDepart
        {
            get
            {
                return this.GetMemberInfo("MemberDepart");
            }
        }
        #endregion

        #region [회원 아이디] MemberId
        public virtual string MemberId
        {
            get
            {
                return this.GetMemberInfo("MemberId");
            }
        }
        #endregion

        #region [회원 권한] MemberAuth
        public virtual string MemberAuth
        {
            get
            {
                return this.GetMemberInfo("MemberAuth");
            }
        }
        #endregion

        #region [회원 이름] MemberNm
        public virtual string MemberNm
        {
            get
            {
                return this.GetMemberInfo("MemberNm");
            }
            set
            {
                this.SetMemberInfo("MemberNm", Convert.ToString(value));
            }
        }
        #endregion
    }
}