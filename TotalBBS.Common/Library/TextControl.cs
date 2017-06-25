using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TotalBBS.Common.Library
{
    public class TextControl
    {
        #region [글자수 자르고 말줄임 .. 넣기]
        /// <summary>
        /// 문자열을 일정길이의 바이트로 자르고 말줄임표를 붙임.
        /// </summary>
        /// <param name="strText">문자열</param>
        /// <param name="strBtyLength">최대치길이(바이트단위)</param>
        /// <param name="strCutText">말줄임표</param>
        /// <returns>string</returns>
        public static string TextCutString(string strText, int strBtyLength, string strCutText)
        {
            // 문자열을 인코딩 - 유니코드를 ks_c_5601-1987로 변환
            System.Text.Encoding TextEncoding = System.Text.Encoding.GetEncoding("ks_c_5601-1987");

            int iSize = 0;
            string CutTextResult = "";

            for (int intTmpI = 0; intTmpI < strText.Length; intTmpI++)
            {
                byte[] arrBty = TextEncoding.GetBytes(strText[intTmpI].ToString());
                iSize += arrBty.Length;

                if (iSize > strBtyLength)
                {
                    break;
                }
                else
                {
                    CutTextResult += strText[intTmpI].ToString();
                }
            }

            // 줄임말 텍스트 합친 후 리턴
            if (iSize > strBtyLength)
            {
                CutTextResult = CutTextResult + strCutText.ToString();
            }
            return CutTextResult;
        }
        #endregion

        #region 글등록시 인젝션 관련[Html Entity 처리]
        /// <summary>
        /// Html Entity
        /// </summary>
        /// <param name="TextValue">Html Entity 하기위한 데이터(제목 또는 내용)</param>
        /// <returns></returns>
        public static string StringEncoder(string TextValue)
        {
            try
            {
                TextValue = TextValue.Replace("'", "''");
                TextValue = TextValue.Replace("\"", "&quot;");
                TextValue = TextValue.Replace("-", "&#45;");
                TextValue = TextValue.Replace("<", "&lt;");
                TextValue = TextValue.Replace(">", "&gt;");
                // TextValue = TextValue.Replace("\r\n", "<br />");
            }
            catch
            {
                TextValue = string.Empty;
            }

            return TextValue;
        }
        #endregion

        #region 화면에 보일때 [Html Entity 처리 원래 대로 돌리기]
        /// <summary>
        /// Html Entity
        /// </summary>
        /// <param name="TextValue">Html Entity 하기위한 데이터(제목 또는 내용)</param>
        /// <returns></returns>
        public static string StringDecoder(string TextValue)
        {
            try
            {
                TextValue = TextValue.Replace("&#39;", "'");
                TextValue = TextValue.Replace("&quot;", "\"");
                TextValue = TextValue.Replace("&#45;", "-");
                TextValue = TextValue.Replace("&lt;", "<");
                TextValue = TextValue.Replace("&gt;", ">");
            }
            catch
            {
                TextValue = string.Empty;
            }

            return TextValue;
        }
        #endregion

        #region 글등록시 인젝션 관련[Html Entity Encoder 처리]
        /// <summary>
        /// Html Entity Encoder
        /// </summary>
        /// <param name="TextValue">Html Entity 하기위한 데이터(제목 또는 내용)</param>
        /// <returns></returns>
        public static string HtmlEntityEncoder(string TextValue)
        {
            try
            {
                if (string.IsNullOrEmpty(TextValue))
                {
                    TextValue = string.Empty;
                }
                else
                {
                    TextValue = TextValue.Replace("'", "&#39;");
                    TextValue = TextValue.Replace("\"", "&quot;");
                    TextValue = TextValue.Replace("-", "&#45;");
                    TextValue = TextValue.Replace("<", "&lt;");
                    TextValue = TextValue.Replace(">", "&gt;");
                }
            }
            catch
            {
                TextValue = string.Empty;
            }

            return TextValue;
        }
        #endregion

        #region 화면에 보일때 [Html Entity Decoder 처리 원래 대로 돌리기]
        /// <summary>
        /// Html Entity Decoder
        /// </summary>
        /// <param name="TextValue">Html Entity 하기위한 데이터(제목 또는 내용)</param>
        /// <returns></returns>
        public static string HtmlEntityDecoder(string TextValue)
        {
            try
            {
                if (string.IsNullOrEmpty(TextValue))
                {
                    TextValue = string.Empty;
                }
                else
                {
                    TextValue = TextValue.Replace("&amp;", "&");
                    TextValue = TextValue.Replace("&#39;", "'");
                    TextValue = TextValue.Replace("&quot;", "\"");
                    TextValue = TextValue.Replace("&#45;", "-");
                    TextValue = TextValue.Replace("&lt;", "<");
                    TextValue = TextValue.Replace("&gt;", ">");
                    TextValue = TextValue.Replace("&nbsp;", " ");

                }
            }
            catch
            {
                TextValue = string.Empty;
            }

            return TextValue;
        }
        #endregion

        #region [MHT 및 HTML 생성시 제목 특수 문자 제거]
        /// <summary>
        /// MHT 및 HTML 생성시 제목 특수 문자 제거
        /// </summary>
        /// <param name="GetText"></param>
        /// <returns></returns>
        public static string HhtEntityEncoder(string TextValue)
        {
            if (string.IsNullOrEmpty(TextValue))
            {
                TextValue = string.Empty;
            }
            else
            {
                TextValue = TextValue.Replace("&amp;", "&");
                TextValue = TextValue.Replace("&#39;", "'");
                TextValue = TextValue.Replace("&quot;", "\"");
                TextValue = TextValue.Replace("&#45;", "-");
                TextValue = TextValue.Replace("&lt;", "<");
                TextValue = TextValue.Replace("&gt;", ">");
                TextValue = TextValue.Replace("\\", "");
                TextValue = TextValue.Replace("\r", "");
                TextValue = TextValue.Replace("\n", "");
                TextValue = TextValue.Replace("''", "'");
                TextValue = TextValue.Replace("\"", " ");
                TextValue = TextValue.Replace("?", " ");
                TextValue = TextValue.Replace("/", " ");
                TextValue = TextValue.Replace("*", " ");
                TextValue = TextValue.Replace("<", " ");
                TextValue = TextValue.Replace(">", " ");
                TextValue = TextValue.Replace("|", " ");
                TextValue = TextValue.Replace(":", " ");
            }

            return TextValue;
        }
        #endregion

        #region Tag를 제외함
        public static string RemoveTag(string strText)
        {
            string strReturns = string.Empty;
            Regex regex = new Regex("<[^>]+>");

            strReturns = regex.Replace(strText, "");

            return strReturns;
        }
        #endregion

        #region [암호화 로직]
        public static string EncryptData(string str)
        {
            byte[] iv = { 16, 29, 51, 112, 210, 78, 98, 186 };
            byte[] key = { 57, 129, 125, 118, 233, 60, 13, 94, 153, 156, 188, 9, 109, 20, 138, 7, 31, 221, 215, 91, 241, 82, 254, 189 };

            string encryptStr = string.Empty;

            byte[] bytIn = null;
            byte[] bytOut = null;
            MemoryStream ms = null;
            TripleDESCryptoServiceProvider tcs = null;
            ICryptoTransform ct = null;
            CryptoStream cs = null;

            try
            {
                bytIn = System.Text.Encoding.UTF8.GetBytes(str);
                ms = new MemoryStream();
                tcs = new TripleDESCryptoServiceProvider();
                ct = tcs.CreateEncryptor(key, iv);
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();

                bytOut = ms.ToArray();
                encryptStr = System.Convert.ToBase64String(bytOut, 0, bytOut.Length);
            }
            catch
            {
                encryptStr = string.Empty;
            }
            finally
            {
                if (cs != null) { cs.Clear(); cs = null; }
                if (ct != null) { ct.Dispose(); ct = null; }
                if (tcs != null) { tcs.Clear(); tcs = null; }
                if (ms != null) { ms = null; }
            }

            return encryptStr;
        }
        #endregion

        #region [복화화 로직]
        public static string DecryptData(string str)
        {
            byte[] iv = { 16, 29, 51, 112, 210, 78, 98, 186 };
            byte[] key = { 57, 129, 125, 118, 233, 60, 13, 94, 153, 156, 188, 9, 109, 20, 138, 7, 31, 221, 215, 91, 241, 82, 254, 189 };
            string decryptStr = string.Empty;

            byte[] bytIn = null;
            MemoryStream ms = null;
            TripleDESCryptoServiceProvider tcs = null;
            CryptoStream cs = null;
            ICryptoTransform ct = null;
            StreamReader sr = null;

            try
            {
                bytIn = System.Convert.FromBase64String(str);
                ms = new MemoryStream(bytIn, 0, bytIn.Length);
                tcs = new TripleDESCryptoServiceProvider();
                ct = tcs.CreateDecryptor(key, iv);
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                sr = new StreamReader(cs);

                decryptStr = sr.ReadToEnd();
            }
            catch
            {
                decryptStr = string.Empty;
            }
            finally
            {
                if (sr != null) { sr.Close(); sr = null; }
                if (cs != null) { cs.Clear(); cs = null; }
                if (ct != null) { ct.Dispose(); ct = null; }
                if (tcs != null) { tcs.Clear(); tcs = null; }
                if (ms != null) { ms.Close(); ms = null; }
            }

            return decryptStr;
        }
        #endregion

        /// <summary>
        /// 문자열이 Null 또는 Empty인지 검사한다.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getDefaultString(object str, string def)
        {
            if (str == null || str == System.DBNull.Value)
                return def;
            else if (str.ToString() == string.Empty || str.ToString().Equals(""))
                return def;
            else return str.ToString();
        }

        /// <summary>
        /// 쿼리스트링 값을 생성합니다.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string CreateQueryString(string[,] arr)
        {
            string strRtn = "";
            int arrLength = 0;

            arrLength = arr.Length / 2;

            for (int i = 0; i < arrLength; i++)
            {
                strRtn += arr[i, 0].ToString() + "=" + arr[i, 1].ToString() + "&";
            }

            strRtn = strRtn.Substring(0, strRtn.Length - 1);

            return strRtn;
        }


        public static string SetComma(string str)
        {
            string tmp = string.Format("{0:#,0}", IntegerUtil.intValid(str, 0));

            return tmp;
        }

        public static string strDecording(string strWord)
        {
            int nCountOfString = Int32.Parse(strWord.Split('ª')[0]);
            string context = strWord.Split('ª')[1];
            char[] b = new char[context.Length];

            for (int i = 0; i < nCountOfString; i++)
            {
                b[i] = (char)(context[i] ^ 64);
            }//end for

            context = new String(b);

            return context;
        }

        public static string ProcessDecoding(string m_strMimeValue, string strSavePath, string strSaveUrl, int nRename)
        {
            string strCRLF = "\r\n";
            string DecodedHTML = "";
            string FileList = string.Empty;
            int ActualFileCount = 0;

            if (m_strMimeValue != null)
            {
                if (m_strMimeValue.Length != 0)
                {
                    string m_strBoundary = FindBoundaryString(m_strMimeValue);
                    int iHeaderTemp = m_strMimeValue.IndexOf(m_strBoundary);
                    int iHeaderEnd = iHeaderTemp + m_strBoundary.Length;
                    m_strMimeValue = m_strMimeValue.Substring(iHeaderEnd);
                    string[] arrText = System.Text.RegularExpressions.Regex.Split(m_strMimeValue, m_strBoundary);
                    for (int i = 0; i < arrText.Length; i++)
                    {
                        string m_strEncodePartData = arrText[i];
                        //2. 파트 구분 처리가 바뀌었음
                        if (m_strEncodePartData.Length > 9)
                        {
                            int indexCRLF = m_strEncodePartData.IndexOf(strCRLF + strCRLF);
                            string strHeader = m_strEncodePartData.Substring(0, indexCRLF).Trim();
                            string strContent = m_strEncodePartData.Substring(indexCRLF).Trim();
                            string[] strHeaders = System.Text.RegularExpressions.Regex.Split(strHeader, strCRLF);
                            string strContentID = "";
                            string strContentLocation = "";
                            string strContentTransferEncoding = "";
                            string strContentMainType = "";
                            string strContentSubType = "";
                            for (int k = 0; k < strHeaders.Length; k++)
                            {
                                string strHeaderItem = strHeaders[k];
                                int indexParam = strHeaderItem.IndexOf(":");
                                if (indexParam != -1)
                                {
                                    string strParam = strHeaderItem.Substring(0, indexParam).Trim();
                                    string strValue = strHeaderItem.Substring(indexParam + 1).Trim();
                                    if (string.Compare(strParam, "content-id", true) == 0)
                                        strContentID = strValue;
                                    if (string.Compare(strParam, "content-location", true) == 0)
                                        strContentLocation = strValue;
                                    if (string.Compare(strParam, "content-transfer-encoding", true) == 0)
                                        strContentTransferEncoding = strValue;
                                    if (string.Compare(strParam, "content-type", true) == 0)
                                    {
                                        int iSlash = strValue.IndexOf("/");
                                        if (iSlash == -1)
                                            strContentMainType = strValue;
                                        else
                                        {
                                            strContentMainType = strValue.Substring(0, iSlash).Trim();
                                            int iColon = strValue.IndexOf(";", iSlash);
                                            if (iColon == -1)
                                                strContentSubType = strValue.Substring(iSlash + 1);
                                            else
                                            {
                                                strContentSubType = strValue.Substring(0, iColon);
                                                strContentSubType = strContentSubType.Substring(iSlash + 1);
                                            }
                                        }
                                    }
                                }
                            }
                            string strName = "";
                            if (string.Compare(strContentSubType, "html", true) == 0)
                            {
                                DecodedHTML = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(strContent.ToString()));
                            }
                            else
                            {
                                int _Buffer_size = 1048576;

                                MemoryStream inStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(strContent));

                                StreamReader CharReader = new StreamReader(inStream, System.Text.Encoding.Default, true, _Buffer_size);
                                int indexFileName = strContentLocation.LastIndexOf("\\");
                                if (indexFileName == -1) indexFileName = strContentLocation.LastIndexOf("/");
                                strName = strContentLocation.Substring(indexFileName + 1);
                                //must be url decode
                                strName = strName.Replace("%20", " ");
                                strName = strName.Replace("%5B", "[");
                                strName = strName.Replace("%5D", "]");
                                strName = strName.Replace("%60", "`");
                                int indexQ = strName.IndexOf("?");
                                if (indexQ != -1)
                                {
                                    strName = strName.Substring(0, indexQ);
                                }
                                if (strName.Length != 0)
                                {
                                    string strFilePath = strSavePath + "\\" + strName;
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(strFilePath);
                                    strName = fileInfo.Name;
                                    bool bExist = fileInfo.Exists;
                                    int nNumbering = 1;
                                    string strFileName = "";
                                    int indexDot = strName.IndexOf(".");
                                    if (indexDot != -1)
                                    {
                                        strFileName = strName.Substring(0, indexDot);
                                    }
                                    if (nRename == 1)
                                    {
                                        while (bExist)
                                        {
                                            string strRenamedFileName = strFileName + "_(" + nNumbering + ")";
                                            strFilePath = fileInfo.DirectoryName + "\\" + strRenamedFileName + fileInfo.Extension;
                                            strName = strRenamedFileName + fileInfo.Extension;
                                            bExist = File.Exists(strFilePath);
                                            nNumbering++;
                                        }
                                    }
                                    //1. 폴더 자동 생성 로직
                                    if (strFilePath != null || strFilePath != string.Empty)
                                    {
                                        FileInfo path = new FileInfo(strFilePath);
                                        Directory.CreateDirectory(path.DirectoryName);
                                        FileStream outStream = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, _Buffer_size);

                                        string line;
                                        byte[] ByteArray;
                                        while ((line = CharReader.ReadLine()) != null)
                                        {
                                            ByteArray = System.Convert.FromBase64String(line);
                                            outStream.Write(ByteArray, 0, ByteArray.Length);
                                        }
                                        outStream.Close();
                                        if (ActualFileCount == 0) FileList = FileList + strName;
                                        else FileList = FileList + ";" + strName;
                                        ActualFileCount++;
                                    }
                                    //cid 처리
                                    if (strContentID.Length != 0)
                                    {
                                        if (DecodedHTML.Length != 0)
                                        {
                                            strContentID = strContentID.Substring(1);
                                            strContentID = strContentID.Substring(0, strContentID.Length - 1);
                                            strContentID = "cid:" + strContentID;
                                            DecodedHTML = DecodedHTML.Replace(strContentID, strSaveUrl + "/" + strName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return DecodedHTML;
        }
        private static string FindBoundaryString(string m_strMimeValue)
        {
            string strCRLF = "\r\n";

            string strBoundary = "boundary=";
            int iBoundStart = m_strMimeValue.IndexOf(strBoundary, 0);
            if (iBoundStart != -1)
            {
                int iBoundEnd = m_strMimeValue.IndexOf(strCRLF, iBoundStart);
                if (iBoundEnd != -1)
                {
                    string strTemp = m_strMimeValue.Substring(iBoundStart + strBoundary.Length, iBoundEnd - (iBoundStart + strBoundary.Length));
                    strTemp = strTemp.Trim();
                    return "--" + strTemp;
                }
            }
            return "";
        }

        /// <summary>
        /// Request Param
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
                    arrResult[i] = HttpContext.Current.Request[strParam[i]];
                else
                    arrResult[i] = null;
            }

            return arrResult;
        }

        public static string Reverse(string strValue, char seperate)
        {
            StringBuilder sbReturnValue = new StringBuilder("");
            string[] arrString = strValue.Split(seperate);

            int count = arrString.Length - 1;

            for (int i = count; i >= 0; i--)
            {
                if (i < count)
                {
                    sbReturnValue.Append(seperate);
                }

                sbReturnValue.Append(arrString[i]);
            }

            return sbReturnValue.ToString();
        }

        public static int StringLength(string strName)
        {
            Encoding objEncoding = Encoding.Default;

            byte[] btName = objEncoding.GetBytes(strName);

            return btName.Length;
        }

        public static string FolderEntityEncoder(string ResultValue)
        {
            if (!String.IsNullOrEmpty(ResultValue))
            {
                ResultValue = ResultValue.Replace("\r", "");
                ResultValue = ResultValue.Replace("\n", "");
                ResultValue = ResultValue.Replace("''", "'");
                ResultValue = ResultValue.Replace("\"", " ");
                ResultValue = ResultValue.Replace("?", " ");
                ResultValue = ResultValue.Replace("\\", " ");
                ResultValue = ResultValue.Replace("/", " ");
                ResultValue = ResultValue.Replace("*", " ");
                ResultValue = ResultValue.Replace("<", " ");
                ResultValue = ResultValue.Replace(">", " ");
                ResultValue = ResultValue.Replace("|", " ");
            }
            else
            {
                ResultValue = string.Empty;
            }

            return ResultValue;
        }

        public static string Reverse(string strValue)
        {
            char seperate = ',';

            StringBuilder sbResult = new StringBuilder("");
            string[] arrString = strValue.Split(seperate);

            int count = arrString.Length - 1;

            for (int i = count; i >= 0; i--)
            {
                if (i < count)
                {
                    sbResult.Append(seperate);
                }

                sbResult.Append(arrString[i]);
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// 개행문자 처리 기타 문자 처리 [','',"]
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetSafeTag(string tag)
        {
            tag = tag.Replace("\r", "<BR>");
            tag = tag.Replace("\n", "");
            tag = tag.Replace("''", "'");
            tag = tag.Replace("\"", "'");

            return tag;
        }

        /// <summary>
        /// 개행문자 처리 기타 문자 처리 [','',"]
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string[] GetArray(string Arrays, string UserName)
        {
            string[] ReturnValue = new string[3];
            char[] chDiv = { '→' };
            string[] strArrUtl;

            strArrUtl = Arrays.Split(chDiv);

            if (strArrUtl.Length > 0)
            {
                List<string> PathNames = new List<string>();

                int TotalCnt = 0;
                for (int i = 0; i < strArrUtl.Length; i++)
                {
                    PathNames.Add(strArrUtl[i].ToString().Trim());

                    TotalCnt = TotalCnt + 1;
                }

                ReturnValue[0] = strArrUtl[0].ToString();

                int indexOfArray = PathNames.BinarySearch(UserName.Trim());

                ReturnValue[1] = UserName;

                if (TotalCnt > 2)
                {
                    ReturnValue[2] = strArrUtl[2].ToString();
                }
                else
                {
                    ReturnValue[2] = string.Empty;
                }
            }
            else
            {
                ReturnValue[0] = string.Empty;
                ReturnValue[1] = string.Empty;
                ReturnValue[2] = string.Empty;
            }

            return ReturnValue;
        }

        public static bool bool_DataSet_RowCount(DataSet ds, int intIndex)
        {
            bool isB = false;

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[intIndex].Rows.Count > 0)
                {
                    isB = true;
                }
            }

            return isB;
        }

        /// <summary>
        /// 화면에 보여실때 Replace
        /// </summary>
        /// <param name="GetText"></param>
        /// <returns></returns>
        public static string StringAmpDecode2(string GetText)
        {
            string TempString = GetText;
            TempString = TempString.Replace("&#39;", "'");
            TempString = TempString.Replace("&quot;", "\"");
            TempString = TempString.Replace("&#45;", "-");
            TempString = TempString.Replace("&amp;", "&");
            TempString = TempString.Replace("&lt;", "<");
            TempString = TempString.Replace("&gt;", ">");
            TempString = TempString.Replace("&apos;", "`");
            TempString = TempString.Replace("\r\n", "<br />");
            //TempString = TempString.Replace("<hHTML", "<html");
            //TempString = TempString.Replace("</hHTML", "</html");
            //TempString = TempString.Replace("<hMETA", "<meta");
            //TempString = TempString.Replace("<hLink", "<link");
            //TempString = TempString.Replace("<hHEAD", "<head");
            //TempString = TempString.Replace("</hHEAD", "</head");
            //TempString = TempString.Replace("<hBODY", "<body");
            //TempString = TempString.Replace("</hBODY", "</body");
            //TempString = TempString.Replace("<hFORM", "<form");
            //TempString = TempString.Replace("</hFORM", "</form");
            //TempString = TempString.Replace("<hSCRIPT", "<script");
            //TempString = TempString.Replace("</hSCRIPT", "</script");
            //TempString = TempString.Replace("<hSTYLE", "<style");
            //TempString = TempString.Replace("</hSTYLE", "</style");
            //TempString = TempString.Replace("script :", "script:");
            //TempString = TempString.Replace("cook!e", "cookie");
            //TempString = TempString.Replace("d0cument.", "document.");
            //다시 보여줄때는 공백으로 표시
            TempString = TempString.Replace("<hHTML", "");
            TempString = TempString.Replace("</hHTML", "");
            TempString = TempString.Replace("<hMETA", "");
            TempString = TempString.Replace("<hLink", "");
            TempString = TempString.Replace("<hHEAD", "");
            TempString = TempString.Replace("</hHEAD", "");
            TempString = TempString.Replace("<hBODY", "");
            TempString = TempString.Replace("</hBODY", "");
            TempString = TempString.Replace("<hFORM", "");
            TempString = TempString.Replace("</hFORM", "");
            TempString = TempString.Replace("<hSCRIPT", "");
            TempString = TempString.Replace("</hSCRIPT", "");
            TempString = TempString.Replace("<hSTYLE", "");
            TempString = TempString.Replace("</hSTYLE", "");
            TempString = TempString.Replace("script :", "");
            TempString = TempString.Replace("cook!e", "");
            TempString = TempString.Replace("d0cument.", "");


            return TempString;
        }

        /// <summary>
        /// 태그 제거 함수
        /// </summary>
        /// <param name="GetText"></param>
        /// <returns></returns>
        public static string Delete_Tag(string GetText)
        {
            string comp_str = GetText;
            comp_str = StringAmpDecode2(comp_str);
            if (!String.IsNullOrEmpty(comp_str))
            {
                string strReturns = string.Empty;
                Regex regex1 = new Regex("<html(.*|)<body([^>]*)<table([^>]*)>");
                Regex regex2 = new Regex("</table(.*)</body(.*)</html>(.*)");
                Regex regex3 = new Regex("<img(.*)>");
                Regex regex4 = new Regex("<[/]*(body|html|head|table|meta|form|input|select|textarea|base|p|br)[^>]*>");
                Regex regex5 = new Regex("<(|iframe|script|title|link)(.*)</(iframe|script|title)>");
                Regex regex6 = new Regex("<[/]*(script|style|title|xmp|iframe)>");
                Regex regex7 = new Regex("([a-z0-9]*script:)");
                Regex regex8 = new Regex("(\n*[\n])");
                Regex regex9 = new Regex("<[^>]*>");

                Regex regex10 = new Regex("<HTML(.*|)<BODY([^>]*)<TABLE([^>]*)>");
                Regex regex11 = new Regex("</TABLE(.*)</BODY(.*)</HTML>(.*)");
                Regex regex12 = new Regex("<IMG(.*)>");
                Regex regex13 = new Regex("<[/]*(BODY|HTML|HEAD|TABLE|META|FORM|INPUT|SELECT|TEXTAREA|BASE|P|BR)[^>]*>");
                Regex regex14 = new Regex("<(|iframe|script|title|link)(.*)</(iframe|script|title)>");
                Regex regex15 = new Regex("<[/]*(SCRIPT|STYLE|TITLE|XMP|IFRAME)>");
                Regex regex16 = new Regex("([A-Z0-9]*SCRIPT:)");

                comp_str = regex1.Replace(comp_str, "");
                comp_str = regex2.Replace(comp_str, "");
                comp_str = regex3.Replace(comp_str, "");
                comp_str = regex4.Replace(comp_str, "");
                comp_str = regex5.Replace(comp_str, "");
                comp_str = regex6.Replace(comp_str, "");
                comp_str = regex7.Replace(comp_str, "");
                comp_str = regex8.Replace(comp_str, "");
                comp_str = regex9.Replace(comp_str, "");
                comp_str = regex10.Replace(comp_str, "");
                comp_str = regex11.Replace(comp_str, "");
                comp_str = regex12.Replace(comp_str, "");
                comp_str = regex13.Replace(comp_str, "");
                comp_str = regex14.Replace(comp_str, "");
                comp_str = regex15.Replace(comp_str, "");
                comp_str = regex16.Replace(comp_str, "");
                comp_str = comp_str.Replace("\\", "");
            }
            return comp_str;
        }

        /// <summary>
        /// 이미지 태그만 추출
        /// </summary>
        /// <param name="GetText"></param>
        /// <returns></returns>
        public static string Img_Tag(string GetText)
        {
            string comp_str = GetText;
            comp_str = StringAmpDecode2(comp_str);
            string img = string.Empty;

            if (!String.IsNullOrEmpty(comp_str))
            {
                string strReturns = string.Empty;

                Regex regex3 = new Regex("<img [^<>]*>", RegexOptions.IgnoreCase);
                MatchCollection matches = regex3.Matches(comp_str);

                if (matches.Count > 0)
                {
                    int i = 0;
                    foreach (Match match in matches)
                    {
                        if (i == 0)
                        {
                            img = match.Value;
                        }
                        i = i + 1;
                    }
                }
            }
            return img;
        }

        #region [문자열 길이 체크]
        /// <summary>
        /// 문자열 길이가 다른경우 기본값 리턴
        /// </summary>
        /// <param name="ReturnValue">값</param>
        /// <param name="StartIndex">지정된 문자열 시작위치</param>
        /// <param name="Length">길이</param>
        /// <returns></returns>
        public static string SubstringDefault(string ReturnValue, int StartIndex, int Length, string DefaultValue)
        {
            if (ReturnValue.Length < StartIndex + Length)
            {
                return DefaultValue;
            }
            else
            {
                return ReturnValue.Substring(StartIndex, Length);
            }
        }
        #endregion

        #region [문자열 길이 체크]
        /// <summary>
        /// 문자열 길이가 다른경우 0값을 더해서 리턴
        /// </summary>
        /// <param name="ReturnValue">값</param>
        /// <param name="StartIndex">지정된 문자열 시작위치</param>
        /// <param name="Length">길이</param>
        /// <returns></returns>
        public static string CkeckSubstring(string ReturnValue, int StartIndex, int Length)
        {
            int ChkCnt = 0;

            StringBuilder sbResult = new StringBuilder();

            sbResult.Append(ReturnValue);

            if (ReturnValue.Length < StartIndex + Length)
            {
                ChkCnt = (StartIndex + Length) - ReturnValue.Length;

                for (int i = 0; i < ChkCnt; i++)
                {
                    sbResult.Append("0");
                }
            }

            return sbResult.ToString();
        }
        #endregion

        public static string RemoveHtmlTag(string html_str)
        {
            // 정규표현을 이용한 HTML태그 삭제
            return Regex.Replace(html_str, @"[<][a-z|A-Z|?|/](.|\n)*?[>]", "");
        }
    }
}