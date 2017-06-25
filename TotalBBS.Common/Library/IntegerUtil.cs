using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TotalBBS.Common.Library
{
    public class IntegerUtil
    {
        /// <summary>
        /// Integer 체크
        /// </summary>
        /// <param name="str">값</param>
        /// <returns></returns>
        public static bool IntegerBoolValid(string str)
        {
            bool tmp = false;
            int IsNumbers = 0;

            if (!string.IsNullOrEmpty(str))
            {
                for (int ii = 0; ii <= str.Length - 1; ii++)
                {
                    if (!Char.IsNumber(str, ii))
                    {
                        IsNumbers = IsNumbers + 1;
                    }
                }

                if (IsNumbers == 0)
                {
                    tmp = true;
                }
            }
            else
            {
                tmp = false;
            }

            return tmp;
        }

        public static int intValid(string str, int intDefault)
        {
            int tmp = 0;
            int Result = 0;

            if (Int32.TryParse(str, out Result))
            {
                tmp = Result;
            }
            else
            {
                tmp = intDefault;
            }

            return tmp;
        }

        public static int intPage(string str, int intDefault)
        {
            int tmp = 0;
            int Result = 0;

            if (Int32.TryParse(str, out Result))
            {
                if (Result > 0)
                {
                    tmp = Result;
                }
                else
                {
                    tmp = intDefault;
                }
            }
            else
            {
                tmp = intDefault;
            }

            return tmp;
        }

        public static long longValid(string str, long Default)
        {
            long tmp = 0;
            long Result = 0;

            if (long.TryParse(str, out Result))
            {
                tmp = Result;
            }
            else
            {
                tmp = Default;
            }

            return tmp;
        }

        public static Int64 BigintValid(string str, Int64 Default)
        {
            Int64 tmp = 0;
            Int64 Result = 0;

            if (Int64.TryParse(str, out Result))
            {
                tmp = Result;
            }
            else
            {
                tmp = Default;
            }

            return tmp;
        }

        public static Decimal MoneyValid(string str, Decimal Default)
        {


            Decimal tmp = 0;
            Decimal Result = 0;

            if (Decimal.TryParse(str, out Result))
            {
                tmp = Result;
            }
            else
            {
                tmp = Default;
            }

            return tmp;
        }

        public static int getDefaultint(object strParameter, int Defaults)
        {
            if (strParameter == null || strParameter == System.DBNull.Value)
            {
                return Defaults;
            }
            else if (strParameter.ToString() == string.Empty || strParameter.ToString().Equals(""))
            {
                return Defaults;
            }
            else
            {
                return IntegerUtil.intPage(strParameter.ToString(), Defaults);
            }
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

        /// <summary>
        /// 퍼센트 계산 소스점 자리수 지정
        /// </summary>
        /// <param name="TotalCnt">총개수</param>
        /// <param name="Single">개수</param>
        /// <param name="Digits">자리수</param>
        /// <returns></returns>
        public static string OneDecimalPercenTage(int TotalCnt, int Single, int Digits)
        {
            if (!TotalCnt.Equals(0))
            {
                return string.Format("{0:F" + Digits.ToString() + "}%", (Single * 100.0 / TotalCnt));
            }
            else
            {
                if (Digits.Equals(0))
                {
                    return "0%";
                }
                else
                {
                    StringBuilder sbDigits = new StringBuilder();
                    sbDigits.Append("0.");
                    for (int i = 0; i < Digits; i++)
                    {
                        sbDigits.Append("0");
                    }
                    sbDigits.Append("%");
                    return sbDigits.ToString();
                }
            }
        }
    }
}