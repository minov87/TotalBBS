using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TotalBBS.Common.Library;
using TotalBBS.Common.WebBase;
using TotalBBS.Common.Dac.Common;
using TotalBBS.Common.Dac.Board;
using TotalBBS.Common.Bean.Board;

namespace TotalBBS.Board
{
    public partial class BoardWrite : Page
    {
        //파일업로드 경로(폴더명)
        private string FileUploadPath = "Board";

        //검색어 변수
        private string ParamBoardCategory = string.Empty;
        private string ParamField = string.Empty;
        private string ParamKey = string.Empty;

        //첨부파일 카운트
        private int AttachedFileCnt = 0;

        private int iIdx = 0;
        private int iPage = 0;
        private string DefaultPage = "/Board/BoardList.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlForm form1 = (HtmlForm)Master.FindControl("form1");

                form1.Method = "post";
                form1.Enctype = "multipart/form-data";

                #region [등록 및 수정 하기위한 데이터 처리]
                object obj_BoardCreate = Cache["BoardCreate"];
                object obj_BoardModify = Cache["BoardModify"];

                string[] ParameterData = new string[5];
                if (obj_BoardCreate != null && obj_BoardModify != null)
                {
                    Response.Redirect(DefaultPage, false);
                }
                else
                {
                    if (obj_BoardCreate != null)
                    {
                        //등록 페이지
                        ParameterData = obj_BoardCreate as string[];

                        if (ParameterData != null)
                        {
                            ParamBoardCategory = ParameterData[0];
                            ParamField = ParameterData[1];
                            ParamKey = ParameterData[2];
                            iIdx = IntegerUtil.intValid(ParameterData[3], 0);
                            iPage = IntegerUtil.intPage(ParameterData[4], 1);

                            this.GetPageInfoSetting(ParamBoardCategory, ParamField, ParamKey, iIdx, iPage, "C");
                        }
                    }
                    else
                    {
                        //수정 페이지
                        if (obj_BoardModify != null)
                        {
                            ParameterData = obj_BoardModify as string[];

                            if (ParameterData != null)
                            {
                                ParamBoardCategory = ParameterData[0];
                                ParamField = ParameterData[1];
                                ParamKey = ParameterData[2];
                                iIdx = IntegerUtil.intValid(ParameterData[3], 0);
                                iPage = IntegerUtil.intPage(ParameterData[4], 1);

                                this.GetPageInfoSetting(ParamBoardCategory, ParamField, ParamKey, iIdx, iPage, "M");
                            }
                        }
                    }
                }
                #endregion
            }
        }

        #region [기본셋팅] 페이지 기본값 정의
        private void GetPageInfoSetting(string ParamBoardCategory, string ParamField, string ParamKey, int Idx, int PageNo, string CMD)
        {
            //페이지 HTML 언어 세팅
            this.ltBoardCategory.Text = "게시판 카테고리";
            this.ltWriteCategory.Text = "게시글 카테고리";
            this.ltUserId.Text = "작성자 아이디";
            this.ltWriter.Text = "작성자 이름";
            this.ltSubject.Text = "제목";
            this.ltContent.Text = "내용";
            this.ltRegiDate.Text = "등록일";
            this.ltViewCnt.Text = "조회수";

            #region [DropDownList 설정]
            Common_NTx_Dac ddlSet = new Common_NTx_Dac();

            // 게시판 카테고리 필드
            DataTable BoardCategorydt = null;
            BoardCategorydt = ddlSet.TOTALBBS_BOARD_CATEGORY_INFO_SEL(-1);
            int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, -1);

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
                DropDownListUtil.SetDropDownListValue(BoardCategorydt, this.ddlBoardCategory, this.ddlBoardCategory.SelectedValue, DropDownListUtil.DropDownFlag.Select);
            }
            
            // 게시글 카테고리 필드
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
                this.ddlBoardCategory.Enabled = true;
                this.ddlWriteCategory.Items.Clear();
                DropDownListUtil.SetDropDownListValue(WriteCategorydt, this.ddlWriteCategory, this.ddlWriteCategory.SelectedValue, DropDownListUtil.DropDownFlag.Select);
            }
            #endregion

            DataSet ds = null;
            if (CMD.Equals("M"))
            {
                Board_NTx_Dac oWS = new Board_NTx_Dac();
                ds = oWS.TOTALBBS_BOARD_VIEW_SEL(Idx);
            }
            else
            {
                this.trVisible_2.Attributes.Add("style", "display:none");
                this.trVisible_2.Visible = false;
            }
            
            this.GetPageSetting(Idx, ds, CMD);  
        }
        #endregion

        #region [기본셋팅] 등록 또는 수정
        private void GetPageSetting(int Idx, DataSet ds, string CMD)
        {

            if (CMD.Equals("C"))
            {
                this.lbtnValidationSave.Text = "<span>확인</span>";
                this.lbtnValidationSave.CssClass = "buttons";

                this.lbtnCancel.Text = "<span>취소</span>";
                this.lbtnCancel.CssClass = "buttons mg_l5";
                this.lbtnValidationDelete.Visible = false;
            }
            else if (CMD.Equals("M"))
            {
                this.lbtnValidationModify.Text = "<span>수정</span>";
                this.lbtnValidationModify.CssClass = "buttons fl_r";
                this.lbtnCancel.Text = "<span>목록</span>";
                this.lbtnCancel.CssClass = "buttons fl_l";

                this.lbtnValidationDelete.Text = "<span>삭제</span>";
                this.lbtnValidationDelete.CssClass = "buttons mg_l5 fl_r";
                this.lbtnValidationDelete.OnClientClick = "return fnDelConfirm();";

                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];

                this.ddlBoardCategory.SelectedValue = dt1.Rows[0]["intBoardCategory"].ToString();

                Common_NTx_Dac ddlSet = new Common_NTx_Dac();

                // 게시판 카테고리 필드
                int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, -1);

                // 게시글 카테고리 필드
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
                    DropDownListUtil.SetDropDownListValue(WriteCategorydt, this.ddlWriteCategory, dt1.Rows[0]["intWriteCategory"].ToString(), DropDownListUtil.DropDownFlag.Select);
                }

                this.ddlWriteCategory.SelectedValue = dt1.Rows[0]["intWriteCategory"].ToString();

                this.txtUserId.Text = dt1.Rows[0]["strUserId"].ToString();
                this.txtWriter.Text = dt1.Rows[0]["strWriter"].ToString();
                this.txtSubject.Text = dt1.Rows[0]["strSubject"].ToString();
                this.txtContent.Text = dt1.Rows[0]["strContent"].ToString();
                this.ltRegiDateValue.Text = dt1.Rows[0]["dateRegDate"].ToString();
                this.ltViewCntValue.Text = dt1.Rows[0]["intViewCount"].ToString();

                //첨부파일 세팅
                if (dt2.Rows.Count == 0)
                {
                    this.rptBoard_Attached.DataSource = dt2;
                    this.rptBoard_Attached.DataBind();
                }
                else
                {
                    AttachedFileCnt = 1;
                    this.rptBoard_Attached.DataSource = dt2;
                    this.rptBoard_Attached.DataBind();
                }
            }
            
            this.hdfIdx.Value = Idx.ToString();
            this.hdfCMD.Value = CMD;
        }
        #endregion

        #region [lbtnCancel_Click] 취소 버튼
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string strhdfCMD = WebUtil.SCRequestFormString("hdfCMD", string.Empty);
                object obj_BoardCreate = Cache["BoardCreate"];
                object obj_BoardModify = Cache["BoardModify"];

                if (obj_BoardCreate != null)
                {
                    Cache.Remove("BoardCreate");
                }
                if (obj_BoardModify != null)
                {
                    Cache.Remove("BoardModify");
                }

                Response.Redirect("/Board/BoardList.aspx", false);
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion
            }
        }
        #endregion

        #region [lbtnValidationSave_Click] 등록 버튼
        protected void lbtnValidation_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnValidation = (LinkButton)sender;

                #region [캐쉬 삭제 처리]
                object obj_BoardCreate = Cache["BoardCreate"];

                if (obj_BoardCreate != null)
                {
                    Cache.Remove("BoardCreate");
                }

                object obj_BoardModify = Cache["BoardModify"];

                if (obj_BoardModify != null)
                {
                    Cache.Remove("BoardModify");
                }
                #endregion

                int ReturnValue = 0;

                ClientScriptManager CM = this.Page.ClientScript;

                if (lbtnValidation.ID.Equals("lbtnValidationSave"))
                {
                    #region [등록 처리]
                    ReturnValue = this.InsertBoard();

                    if (ReturnValue > 0)
                    {
                        CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('성공적으로 데이타가 저장되었습니다.');location.href = '" + DefaultPage + "';</script>", false);
                    }
                    else
                    {
                        if (ReturnValue.Equals(-10))
                        {
                            //마스터 테이블은 정상적으로 수정. 첨부파일은 한개도 없을시 -10으로 리턴받음
                            CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('성공적으로 데이타가 저장되었습니다.');location.href = '" + DefaultPage + "';</script>", false);
                        }
                        else
                        {
                            CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터 저장에 실패하였습니다.');location.href = '" + DefaultPage + "';</script>", false);
                        }

                    }
                    #endregion
                }
                else if (lbtnValidation.ID.Equals("lbtnValidationModify"))
                {
                    #region [수정 처리]
                    ReturnValue = this.UpdateBoard();

                    if (ReturnValue > 0)
                    {
                        CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터가 수정되었습니다.');location.href = '" + DefaultPage + "';</script>", false);
                    }
                    else
                    {
                        if (ReturnValue.Equals(-10))
                        {
                            //마스터 테이블은 정상적으로 수정. 이미지 첨부파일은 한개도 없을시 -10으로 리턴받음
                            CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터가 수정되었습니다.');location.href = '" + DefaultPage + "';</script>", false);
                        }
                        else
                        {
                            CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터 수정에 실패하였습니다.');location.href = '" + DefaultPage + "';</script>", false);
                        }

                    }
                    #endregion
                }
                else if (lbtnValidation.ID.Equals("lbtnValidationDelete"))
                {
                    #region [삭제 처리]
                    ReturnValue = this.DeleteBoard();

                    if (ReturnValue > 0)
                    {
                        CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터가 삭제되었습니다.');location.href = '" + DefaultPage + "';</script>", false);
                    }
                    else
                    {
                        CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('데이터 삭제에 실패하였습니다.');location.href = '" + DefaultPage + "';</script>", false);
                    }
                    #endregion
                }
                else
                {
                    Response.Redirect(DefaultPage, false);
                }
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion

                ClientScriptManager CM = this.Page.ClientScript;
                CM.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'> alert('오류 발생 관리자에게 문의 하세요.');location.href = '" + DefaultPage + "';</script>", false);
            }
        }
        #endregion

        #region [게시판] 게시글 등록
        private int InsertBoard()
        {
            try
            {
                Board_Tx_Dac oWS = new Board_Tx_Dac();

                int intBoardCategory = Convert.ToInt32(WebUtil.SCRequestFormString("ddlBoardCategory", "1"));
                int intWriteCategory = Convert.ToInt32(WebUtil.SCRequestFormString("ddlWriteCategory", "1"));
                string strUserId = WebUtil.SCRequestFormString("txtUserId", string.Empty);
                string strWriter = WebUtil.SCRequestFormString("txtWriter", string.Empty);
                string strSubject = WebUtil.SCRequestFormString("txtSubject", string.Empty);
                string strContent = WebUtil.SCRequestFormString("txtContent", string.Empty);

                string[] ContentType = null; // 첨부파일 타입 구분배열 변수

                #region [첨부파일 등록]

                HttpFileCollection files = HttpContext.Current.Request.Files;
                int FileTotalCnt = files.Count;

                string[] OldFileName = new string[FileTotalCnt];
                string[] NewFileName = new string[FileTotalCnt];
                string[] FileUploadPath = new string[FileTotalCnt];

                if (FileTotalCnt > 0)
                {
                    for (int i = 0; i < FileTotalCnt; i++)
                    {
                        HttpPostedFile postedFile = files[i];
                        ContentType = postedFile.ContentType.Split('/');
                        // 빈파일일때 
                        if (postedFile.ContentLength > 1)
                        {
                            string[] FilesInfo1 = new string[3];
                            FilesInfo1 = FileName(postedFile);
                            // 첨부가 하나라도 선택이 되어 있다면
                            if (FilesInfo1 != null)
                            {

                                OldFileName[i] = FilesInfo1[0];       // 파일 원본 이름
                                NewFileName[i] = FilesInfo1[1];       // 파일 업로드후 생성된 새이름
                                FileUploadPath[i] = FilesInfo1[2];    // 파일 업로드 경로 디비에 넣지 않는경우 /FileUpload/  + FileUploadPath경로 수동 입력 해도됨
                            }
                            else
                            {
                                //첨부가 없다면 카운트를 0으로
                                FileTotalCnt = 0;
                            }
                        }
                        else
                        {
                            //첨부가 없다면 카운트를 0으로
                            FileTotalCnt = 0;
                        }
                    }
                }

                #endregion

                int MasterReturnValue = 0;
                int ReturnValue = 0;

                MasterReturnValue = oWS.TOTALBBS_BOARD_INFO_INS(intBoardCategory, intWriteCategory, strUserId, strWriter, strSubject, strContent);

                if (MasterReturnValue > 0)
                {
                    //파일카운트가 1이상이면 첨부파일 테이블 등록
                    if (FileTotalCnt > 0)
                    {
                        for (int i = 0; i < FileTotalCnt; i++)
                        {
                            ReturnValue = oWS.TOTALBBS_BOARD_FILE_INFO_INS(MasterReturnValue, OldFileName[i], NewFileName[i], FileUploadPath[i], i + 1);
                        }
                    }
                    else
                    {
                        //마스터 정상 등록이며 첨부파일이 없다면 -10 고정
                        ReturnValue = -10;
                    }
                }
                else
                {
                    //오류시 파일 삭제하기
                }

                return ReturnValue;
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion

                return 0;
            }
        }
        #endregion

        #region [게시판] 게시글 수정
        private int UpdateBoard()
        {
            try
            {
                Board_Tx_Dac oWS = new Board_Tx_Dac();
                Board_NTx_Dac oWSN = new Board_NTx_Dac();

                int ModIdx = IntegerUtil.intValid(WebUtil.SCRequestFormString("hdfIdx", string.Empty), 0);
                int intBoardCategory = Convert.ToInt32(WebUtil.SCRequestFormString("ddlBoardCategory", "1"));
                int intWriteCategory = Convert.ToInt32(WebUtil.SCRequestFormString("ddlWriteCategory", "1"));
                string strUserId = WebUtil.SCRequestFormString("txtUserId", string.Empty);
                string strWriter = WebUtil.SCRequestFormString("txtWriter", string.Empty);
                string strSubject = WebUtil.SCRequestFormString("txtSubject", string.Empty);
                string strContent = WebUtil.SCRequestFormString("txtContent", string.Empty);

                string ChkBoxListData = string.Empty;

                DataSet ds = null;
                DataTable dt = null;        // 삭제할 목록 조회
                DataTable dt1 = null;       // 삭제하지 않는 목록 조회
                int NoDeleteFileCnt = 0;    // 삭제하지 않는 파일 수

                //파일 리스트 조회(삭제할 목록과 삭제 하지 않을 목록 분리하여 가져옴) 
                //체크박스 선택 여부
                if (ModIdx > 0 && Request.Form["chkAttached_" + ModIdx + ""] != null)
                {
                    ChkBoxListData = Request.Form["chkAttached_" + ModIdx + ""].ToString();
                    ds = oWSN.TOTALBBS_BOARD_FILE_INFO_SEL(ModIdx, ChkBoxListData);
                }
                else
                {
                    ChkBoxListData = "0";
                    ds = oWSN.TOTALBBS_BOARD_FILE_INFO_SEL(ModIdx, ChkBoxListData);
                }

                string OldFileName = string.Empty;
                string NewFileName = string.Empty;
                string FileUploadPath = string.Empty;

                if (ds != null)
                {
                    dt = ds.Tables[0];    // 삭제할 목록 조회
                    dt1 = ds.Tables[1];   // 삭제하지 않는 목록 조회
                    if (dt1.Rows.Count > 0)
                    {
                        NoDeleteFileCnt = dt1.Rows.Count;
                    }
                }

                int iData = 0;
                List<FileBean> GecAttachedList = new List<FileBean>();
                if (ds != null)
                {
                    //삭제 하지 않을 목록을 조회 하여 처리
                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (iData == 0)
                        {
                            OldFileName = dr["strRealFileName"].ToString();
                            NewFileName = dr["strFileName"].ToString();
                            FileUploadPath = dr["strFilePath"].ToString();
                        }
                        iData = iData + 1;
                        FileBean AFBean = new FileBean();
                        AFBean.strRealFileName = dr["strRealFileName"].ToString();      // 파일 원본 이름
                        AFBean.strFileName = dr["strFileName"].ToString();          // 파일 업로드후 생성된 새이름
                        AFBean.strFilePath = dr["strFilePath"].ToString();             // 파일 업로드 경로 디비에 넣지 않는경우 /FileUpload/  + FileUploadPath경로 수동 입력 해도됨
                        AFBean.intFileSort = iData;
                        GecAttachedList.Add(AFBean);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                FileUploadPath = dr["strFilePath"].ToString();
                            }
                        }
                    }
                }

                HttpFileCollection files = HttpContext.Current.Request.Files;

                int FileTotalCnt = files.Count;
                int FileSort = 0;


                // 웹 취약성 추가 파일 첨부시 aspx 에서 검사 후 cs 단에서 한번 더 검사
                string[] ContentType = null;        // 첨부파일 타입 구분배열 변수

                #region [파일 업로드 정보를 조회]
                if (FileTotalCnt > 0)
                {
                    for (int i = 0; i < FileTotalCnt; i++)
                    {
                        HttpPostedFile postedFile = files[i];
                        FileBean AFBean = new FileBean();

                        FileSort = iData + i;
                        string[] FilesInfo1 = new string[3];

                        ContentType = postedFile.ContentType.Split('/');
                        //빈파일일때 
                        if (postedFile.ContentLength > 1)
                        {
                            FilesInfo1 = FileName(postedFile);

                            if (FilesInfo1 != null)
                            {
                                if (FileSort == 0)
                                {
                                    OldFileName = FilesInfo1[0];
                                    NewFileName = FilesInfo1[1];
                                    FileUploadPath = FilesInfo1[2];
                                }
                                AFBean.strRealFileName = FilesInfo1[0];     // 파일 원본 이름
                                AFBean.strFileName = FilesInfo1[1];         // 파일 업로드후 생성된 새이름
                                AFBean.strFilePath = FilesInfo1[2];         // 파일 업로드 경로 디비에 넣지 않는경우 /FileUpload/  + FileUploadPath경로 수동 입력 해도됨
                                AFBean.intFileSort = FileSort + 1;
                                GecAttachedList.Add(AFBean);
                            }
                            else
                            {
                                FileTotalCnt = 0;
                            }
                        }
                        else
                        {
                            FileTotalCnt = 0;
                        }
                    }
                }
                #endregion

                int MasterReturnValue = 0;
                int ReturnValue = 0;

                MasterReturnValue = oWS.TOTALBBS_BOARD_INFO_UPD(ModIdx, intBoardCategory, intWriteCategory, strUserId, strWriter, strSubject, strContent);

                if (MasterReturnValue > 0)
                {
                    if (GecAttachedList.Count > 0)
                    {
                        foreach (FileBean Item in GecAttachedList)
                        {
                            ReturnValue = oWS.TOTALBBS_BOARD_FILE_INFO_UPD(ModIdx, Item.strRealFileName, Item.strFileName, Item.strFilePath, Item.intFileSort);
                        }
                    }
                    else
                    {
                        //전체 제크 및 한개만 있을때 체크 했을때
                        ReturnValue = oWS.TOTALBBS_BOARD_FILE_INFO_DEL_SELECTED(ChkBoxListData);
                    }
                }

                //첨부파일 등록이 하나도 없을시
                if (GecAttachedList.Count.Equals(0))
                {
                    ReturnValue = -10;
                }
                //체크박스 선택 파일 삭제 시키기
                if (!ChkBoxListData.Equals("0"))
                {
                    //삭제
                    foreach (DataRow dr in dt.Rows)
                    {
                        //경로를 넘기는데 \\ 수가 맞지 않아 메서드 추가함 수정시 삭제에 사용
                        LocalFileControls.FileDelete_New(FileUploadPath, dr["strFileName"].ToString());
                    }
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion

                return 0;
            }
        }
        #endregion

        #region [게시판] 게시글 삭제
        private int DeleteBoard()
        {
            try
            {
                Board_Tx_Dac oWS = new Board_Tx_Dac();
                Board_NTx_Dac oWSN = new Board_NTx_Dac();
                int RemoveIdx = IntegerUtil.intValid(WebUtil.SCRequestFormString("hdfIdx", string.Empty), 0);
                int ReturnValue = 0;

                DataSet ds = oWSN.TOTALBBS_BOARD_VIEW_FILE_INFO_SEL(RemoveIdx);
                int FileDelCnt = ds.Tables[0].Rows.Count;

                ReturnValue = oWS.TOTALBBS_BOARD_FILE_INFO_VIEW_DEL(RemoveIdx, FileDelCnt);
                if (ReturnValue > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        LocalFileControls.FileDelete(FileUploadPath, dr["strFileName"].ToString());
                    }
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                #region [Error Logger] 로그인을 한경우
                //ErrorLogger_Tx_Dac.GetErrorLogger_Tx_Dac().TB_TOTABBS_ERROR_LOGGER_INFO_INS_SP(ex, admin.MemberId, admin.MemberNm);
                #endregion

                return 0;
            }
        }
        #endregion

        #region [ddlBoardCaregory_itemSelected]
        protected void ddlBoardCaregory_itemSelected(object sender, EventArgs e)
        {
            try
            {
                int ddlBoardCategorySeletedValue = IntegerUtil.intValid(this.ddlBoardCategory.SelectedValue, -1);
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
        #endregion

        #region [첨부파일] 데이터 바인딩
        protected void rptBoard_Attached_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // 데이타 처리
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;

                Literal ltAttachedFile = (Literal)e.Item.FindControl("ltAttachedFile");
                Literal ltAttachedCheckBox = (Literal)e.Item.FindControl("ltAttachedCheckBox");
                ltAttachedFile.Text = drv.Row["strRealFileName"].ToString();
                ltAttachedCheckBox.Text = "<input type=\"checkbox\" name=\"chkAttached_" + drv.Row["intBBSIdx"].ToString() + "\" id=\"chkAttached_" + drv.Row["intIdx"].ToString() + "\" value=\"" + drv.Row["intIdx"].ToString() + "\" class=\"va_3\" /> 삭제";
            }
        }
        #endregion

        # region [파일 업로드]
        private string[] FileName(HttpPostedFile fuFiles)
        {
            string[] strReturn = new string[3];
            strReturn = LocalFileControls.UploadFiles(fuFiles, FileUploadPath);

            if (strReturn != null)
            {
                return strReturn;
            }
            else
            {
                return strReturn;
            }
        }
        #endregion
    }
}