using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TotalBBS.Common.Library
{
    public class DropDownListUtil
    {
        #region [DropDownList 관련 Util]

        public enum DropDownFlag
        {
            All = 0,    // 전체선택
            Select = 1, // 선택하세요
            None = 2    // 아무것도 출력하지 않을 때
        }

        public static void SetDropDownList(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string selectedValue, DropDownFlag flag)
        {

            htmlSelect.Items.Clear();

            if (flag == DropDownFlag.All)
                htmlSelect.Items.Add(new ListItem("전체선택", "0"));
            else if (flag == DropDownFlag.Select)
                htmlSelect.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        htmlSelect.SelectedIndex = iRowCnt;
                    else
                        htmlSelect.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownList(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string selectedValue, string DefaultText)
        {

            htmlSelect.Items.Clear();

            htmlSelect.Items.Add(new ListItem(DefaultText, "0"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedValue)
                {
                    htmlSelect.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownList(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("전체선택", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][1].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownListValue(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("전체선택", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownList(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string textField, string valueField)
        {
            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][textField].ToString(), dataTable.Rows[iRowCnt][valueField].ToString()));
            }

        }

        public static void SetDropDownListByValue(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string displayText, string displayCode, string selectedValue)
        {

            htmlSelect.Items.Clear();

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    htmlSelect.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownListByValue(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string displayText, string displayCode, string selectedValue)
        {

            dropDownList.Items.Clear();

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));
                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    dropDownList.SelectedIndex = iRowCnt;
                }
            }
        }

        public static void SetDropDownListByText(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string displayText, string displayCode, string selectedText)
        {

            htmlSelect.Items.Clear();

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedText)
                {
                    htmlSelect.SelectedIndex = iRowCnt;
                }
            }
        }

        public static void SetDropDownListByText(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string displayText, string displayCode, string selectedText)
        {
            dropDownList.Items.Clear();

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedText)
                {
                    dropDownList.SelectedIndex = iRowCnt;
                }
            }
        }

        public static void SetDropDownListByText(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string displayText, string displayCode, string selectedText, int CodeCol, int TextCol)
        {

            htmlSelect.Items.Clear();

            if (displayText != "")
                htmlSelect.Items.Add(new ListItem(displayText, "0"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][TextCol].ToString(), dataTable.Rows[iRowCnt][CodeCol].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedText)
                {
                    htmlSelect.SelectedIndex = iRowCnt;
                }
            }
        }

        public static void SetDropDownListByText(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string displayText, string displayCode, string selectedText, int CodeCol, int TextCol, string SetValue)
        {

            htmlSelect.Items.Clear();

            if (displayText != "")
                htmlSelect.Items.Add(new ListItem(displayText, "0"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][TextCol].ToString(), dataTable.Rows[iRowCnt][CodeCol].ToString()));
                if (dataTable.Rows[iRowCnt][1].ToString() == selectedText)
                {
                    htmlSelect.SelectedIndex = iRowCnt;
                }
            }
        }
        #endregion

        #region 년/월/일/시간 관련 Util
        public static void getYear2SelectOption(int syear, int eyear, int chkvalue, HtmlSelect ddl)
        {
            ddl.Items.Clear();

            int j = 0;
            for (int i = eyear; i >= syear; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                if (chkvalue.ToString() == ddl.Items[j].Value)
                    ddl.Items[j].Selected = true;
                j++;
            }
        }

        public static void SetDropDownListUser(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("장르", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownListJopdUser(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("조PD 평점", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownListOnePage(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("전체", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }

        public static void SetDropDownListTviews(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, DropDownFlag flag)
        {

            dropDownList.Items.Clear();

            if (flag == DropDownFlag.All)
                dropDownList.Items.Add(new ListItem("선택하세요", "0"));
            else if (flag == DropDownFlag.Select)
                dropDownList.Items.Add(new ListItem("선택하세요", "-1"));

            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {
                    if (flag == DropDownFlag.None)
                        dropDownList.SelectedIndex = iRowCnt;
                    else
                        dropDownList.SelectedIndex = iRowCnt + 1;
                }
            }
        }
        #endregion

        public static void SetDropDownListOnePage(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, string DefaultTxt, string DefaultValue)
        {

            dropDownList.Items.Clear();
            dropDownList.Items.Add(new ListItem(DefaultTxt, DefaultValue));
            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString().Equals(selectedValue))
                {
                    dropDownList.SelectedValue = selectedValue;
                }
            }
        }

        public static void SetDefaultSettingDropDownList(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, string DefaultTxt, string DefaultValue)
        {

            dropDownList.Items.Clear();
            dropDownList.Items.Add(new ListItem(DefaultTxt, DefaultValue));
            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString().Equals(selectedValue))
                {
                    dropDownList.SelectedValue = selectedValue;
                }
            }
        }

        public static void SetDefaultSettingHtmlSelect(DataTable dataTable, System.Web.UI.HtmlControls.HtmlSelect htmlSelect, string selectedValue, string DefaultTxt, string DefaultValue)
        {
            htmlSelect.Items.Clear();

            htmlSelect.Items.Add(new ListItem(DefaultTxt, DefaultValue));
            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                htmlSelect.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString().Equals(selectedValue))
                {
                    htmlSelect.Value = selectedValue;
                }
            }
        }

        public static void SetDropDownListOnePage(DataTable dataTable, System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue)
        {

            dropDownList.Items.Clear();
            for (int iRowCnt = 0; iRowCnt < dataTable.Rows.Count; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(dataTable.Rows[iRowCnt][1].ToString(), dataTable.Rows[iRowCnt][0].ToString()));

                if (dataTable.Rows[iRowCnt][0].ToString() == selectedValue)
                {

                    dropDownList.SelectedIndex = iRowCnt;

                }
            }
        }

        public static void SetDropDownListYear(System.Web.UI.WebControls.DropDownList dropDownList, string selectedValue, string DefaultTxt, string DefaultValue)
        {

            dropDownList.Items.Clear();
            dropDownList.Items.Add(new ListItem(DefaultTxt, DefaultValue));
            for (int iRowCnt = 2010; iRowCnt < 2020; iRowCnt++)
            {
                dropDownList.Items.Add(new ListItem(iRowCnt.ToString(), iRowCnt.ToString()));

                if (iRowCnt.ToString() == selectedValue)
                {
                    dropDownList.SelectedValue = iRowCnt.ToString();
                }
            }
        }
    }
}