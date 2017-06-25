using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace TotalBBS.Common.Library
{
    public class LocalFileControls
    {
        //파일 업로드 경로 설정 기본값 D:\,E:\, F:\
        public static readonly string AppLocalPath = ConfigurationSettings.AppSettings["LocalPath"].ToString();

        //파일 업로드 기본 폴더 경로 설정 필수값 기본 FileUpload
        public static readonly string AppFilePath = ConfigurationSettings.AppSettings["FilePath"].ToString();

        #region 기존경로에서 새로운 경로로 파일을 이동시키고 기존 파일 삭제
        public static void FileMove(string strOriginalPath, string strMovedNewPath)
        {
            FileInfo fInfo = new FileInfo(strOriginalPath);
            fInfo.MoveTo(strMovedNewPath);

            if (File.Exists(strOriginalPath))
            {
                File.Delete(strOriginalPath);
            }
        }

        public static void FileCopy(string strOriginalPath, string strMovedNewPath)
        {
            string fileToCopy = strOriginalPath;
            string newLocation = strMovedNewPath;

            if (System.IO.File.Exists(AppLocalPath + fileToCopy))
            {
                System.IO.File.Copy(AppLocalPath + fileToCopy, AppLocalPath + newLocation, true);

            }
        }

        public static void FileDelete(string strOriginalPath)
        {
            if (File.Exists(strOriginalPath))
            {
                File.Delete(strOriginalPath);
            }
        }
        #endregion

        #region [파일 삭제]
        public static void FileDelete(string FilePath, string NewFileName)
        {
            StringBuilder sbOriginalPath = new StringBuilder();
            sbOriginalPath.Append(AppLocalPath);
            sbOriginalPath.Append(AppFilePath);
            sbOriginalPath.Append("/");
            sbOriginalPath.Append(FilePath);
            sbOriginalPath.Append("/");
            sbOriginalPath.Append(NewFileName);

            if (File.Exists(sbOriginalPath.ToString()))
            {
                File.Delete(sbOriginalPath.ToString());
            }
        }
        #endregion

        #region [파일 삭제]
        public static void FileDelete_New(string FilePath, string NewFileName)
        {
            StringBuilder sbOriginalPath = new StringBuilder();
            sbOriginalPath.Append(AppLocalPath);
            sbOriginalPath.Append(FilePath);
            sbOriginalPath.Append(NewFileName);

            if (File.Exists(sbOriginalPath.ToString()))
            {
                File.Delete(sbOriginalPath.ToString());
            }
        }
        #endregion

        #region 새로운 폴더 생성
        public static bool MakeFolder(string strDir)
        {
            if (string.IsNullOrEmpty(strDir))
            {
                return false;
            }
            else
            {
                if (Directory.Exists(strDir) == false)
                {
                    Directory.CreateDirectory(strDir);
                }

                return true;
            }
        }

        public static bool MakeFolder(string strDriveNm, string strDir)
        {
            if (string.IsNullOrEmpty(strDir))
            {
                return false;
            }

            string strTempDir = "";
            string strPartDir = "";

            strTempDir = strDir.Replace("\\", "/");

            // 맨 앞에 "/"가 있을 경우 처리하는 부분
            strPartDir = strTempDir.Substring(0, 1);

            if (strPartDir == "/")
            {
                strTempDir = strTempDir.Substring(1);
            }

            // 맨 뒤에 "/"가 있을 경우 처리하는 부분
            strPartDir = strTempDir.Substring(strTempDir.Length - 1, 1);

            if (strPartDir == "/")
            {
                strTempDir = strTempDir.Substring(0, strTempDir.Length - 1);
            }

            // "/"를 기준으로 배열에 담는 부분
            char[] chSpliter = { '/' };

            string[] arrStrDir = strTempDir.Split(chSpliter);

            // 배열에 담긴 정보로 폴더생성하는 부분
            string strText = "";

            int intArrLength = arrStrDir.Length;
            int intTempI = 0;

            strText = strDriveNm + ":/";

            for (intTempI = 0; intTempI < intArrLength; intTempI++)
            {
                strText = strText + "/" + arrStrDir[intTempI];
                DirectoryInfo drInfo = new DirectoryInfo(strText);

                if (!drInfo.Exists)
                {
                    drInfo.Create();
                }
            }

            return true;
        }
        #endregion

        #region 기존파일명을 변경해줄 경우 사용, 두번째 인자는 파일 Full경로 혹은 파일명 둘다 가능함.
        public static string MakeFileName(string strFileNm)
        {
            string strReturnNm = string.Empty;

            string fileName = Path.GetFileName(strFileNm);
            strReturnNm = DateTime.Now.Ticks.ToString() + Path.GetExtension(fileName);

            return strReturnNm;
        }
        #endregion

        #region 기존파일명을 변경해줄 경우 사용, 두번째 인자는 파일 Full경로 혹은 파일명 둘다 가능함.
        public static string MakeFileName(string strFileNm, int No)
        {
            string strReturnNm = string.Empty;

            string fileName = Path.GetFileName(strFileNm);
            strReturnNm = (DateTime.Now.Ticks + No).ToString() + Path.GetExtension(fileName);

            return strReturnNm;
        }
        #endregion

        #region [UploadFiles] 서버 컨트롤 파일 업로드 
        public static string[] UploadFiles(FileUpload fuploads, string SaveFilePath)
        {
            string[] ArrReturn = new string[3];
            string OriginalFileName = string.Empty; //원본 파일명
            StringBuilder sbFielUploadPath = new StringBuilder();
            StringBuilder sbDBFilePath = new StringBuilder();
            string NewFileName = string.Empty;
            string strFileNm = string.Empty;
            string strFullNm = string.Empty;
            string strPreNm = string.Empty;
            string strDriveNm = string.Empty;
            string strSaveNm = string.Empty;
            string strPartialDir = string.Empty;
            string[] strArrDiv = { ":/" };
            string[] strArrPath;
            bool isMapPath = false;
            bool isMakeFolder = false;

            #region [업로드 할 로컬 폴더 경로 설정]
            sbFielUploadPath.Append(AppLocalPath);
            sbFielUploadPath.Append(AppFilePath);
            sbFielUploadPath.Append("/");
            sbFielUploadPath.Append(SaveFilePath);
            sbFielUploadPath.Append("/");
            #endregion

            #region [실제 사이트에서 사용할 경로]
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(AppFilePath);
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(SaveFilePath);
            sbDBFilePath.Append("/");
            #endregion

            ArrReturn[0] = string.Empty;
            ArrReturn[1] = string.Empty;
            ArrReturn[2] = string.Empty;

            if (fuploads.HasFile)
            {
                string[] ArrFileNames = fuploads.FileName.Split('\\');

                OriginalFileName = ArrFileNames[ArrFileNames.Length - 1];

                // 독립된 파일명 생성
                NewFileName = LocalFileControls.MakeFileName(fuploads.FileName);
                strFileNm = NewFileName;
                strPreNm = sbFielUploadPath.ToString().Replace("\\", "/");
                strArrPath = strPreNm.Split(strArrDiv, System.StringSplitOptions.RemoveEmptyEntries);
                strDriveNm = strArrPath[0];

                strPartialDir = strArrPath[1].Substring(strArrPath[1].Length - 1, 1);

                if (strPartialDir == "/")
                {
                    strArrPath[1] = strArrPath[1].Substring(0, strArrPath[1].Length - 1);
                }

                strPreNm = strArrPath[1] + "/";
                isMakeFolder = LocalFileControls.MakeFolder(sbFielUploadPath.ToString());

                if (isMakeFolder)
                {
                    string strTempDir = "";
                    string strPartDir = "";

                    strTempDir = sbFielUploadPath.ToString().Replace("\\", "/");

                    // 맨 앞에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(0, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(1);
                    }

                    // 맨 뒤에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(strTempDir.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(0, strTempDir.Length - 1);
                    }

                    strPartDir = strPreNm.Substring(strPreNm.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strPreNm = strPreNm.Substring(0, strPreNm.Length - 1);
                    }

                    // 파일 Full경로 생성
                    if (isMapPath)
                    {
                        strFullNm = HttpContext.Current.Server.MapPath("~/") + strTempDir + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }
                    else
                    {
                        strFullNm = strDriveNm + ":/" + strPreNm + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }

                    FileInfo fileInfo = new FileInfo(strFullNm);

                    string strNewFileNm = "";

                    // 기존이미지가 존재할 경우
                    if (fileInfo.Exists)
                    {
                        int intFileIndex = 0;

                        // 확장자 분리
                        string strFileExtension = fileInfo.Extension;

                        // 확장자를 제외한 파일명 추출
                        string strRealNm = strFileNm.Replace(strFileExtension, "");

                        // 다른 파일명이 생성될때까지 Loop
                        do
                        {
                            intFileIndex++;
                            strNewFileNm = strRealNm + intFileIndex.ToString() + strFileExtension;
                            fileInfo = new FileInfo(sbFielUploadPath.ToString() + strNewFileNm);
                        }
                        while (fileInfo.Exists);

                        // 파일 Full경로 생성
                        strFullNm = sbFielUploadPath.ToString() + strNewFileNm;
                        NewFileName = strNewFileNm;
                        // 파일 업로드
                        fuploads.PostedFile.SaveAs(strFullNm);
                    }
                    else
                    {
                        fuploads.PostedFile.SaveAs(strFullNm);

                    }

                    ArrReturn[0] = OriginalFileName;    // 원본 파일명
                    ArrReturn[1] = NewFileName;           //업로드된 파일명
                    ArrReturn[2] = sbDBFilePath.ToString();          // 파일 업로드 경로

                }
                else
                {
                    // 파일 업로드
                    ArrReturn = null;
                }

                return ArrReturn;
            }
            else
            {
                ArrReturn = null;
                return ArrReturn;
            }
        }
        #endregion

        #region [UploadFiles] 서버 컨트롤 파일 업로드
        public static string[] UploadFiles(FileUpload fuploads, string SaveFilePath, string NewFileName)
        {
            string[] ArrReturn = new string[2];
            string OriginalFileName = string.Empty; //원본 파일명
            StringBuilder sbFielUploadPath = new StringBuilder();
            StringBuilder sbDBFilePath = new StringBuilder();
            string strFileNm = string.Empty;
            string strFullNm = string.Empty;
            string strPreNm = string.Empty;
            string strDriveNm = string.Empty;
            string strSaveNm = string.Empty;
            string strPartialDir = string.Empty;
            string[] strArrDiv = { ":/" };
            string[] strArrPath;
            bool isMapPath = false;
            bool isMakeFolder = false;

            #region [업로드 할 로컬 폴더 경로 설정]
            sbFielUploadPath.Append(AppLocalPath);
            sbFielUploadPath.Append(AppFilePath);
            sbFielUploadPath.Append("/");
            sbFielUploadPath.Append(SaveFilePath);
            sbFielUploadPath.Append("/");
            #endregion

            #region [실제 사이트에서 사용할 경로]
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(AppFilePath);
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(SaveFilePath);
            sbDBFilePath.Append("/");
            #endregion

            if (fuploads.HasFile)
            {
                string[] ArrFileNames = fuploads.FileName.Split('\\');

                OriginalFileName = ArrFileNames[ArrFileNames.Length - 1];
                strFileNm = NewFileName;
                strPreNm = sbFielUploadPath.ToString().Replace("\\", "/");
                strArrPath = strPreNm.Split(strArrDiv, System.StringSplitOptions.RemoveEmptyEntries);
                strDriveNm = strArrPath[0];

                strPartialDir = strArrPath[1].Substring(strArrPath[1].Length - 1, 1);

                if (strPartialDir == "/")
                {
                    strArrPath[1] = strArrPath[1].Substring(0, strArrPath[1].Length - 1);
                }

                strPreNm = strArrPath[1] + "/";
                isMakeFolder = LocalFileControls.MakeFolder(sbFielUploadPath.ToString());
                if (isMakeFolder)
                {
                    string strTempDir = "";
                    string strPartDir = "";

                    strTempDir = sbFielUploadPath.ToString().Replace("\\", "/");

                    // 맨 앞에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(0, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(1);
                    }

                    // 맨 뒤에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(strTempDir.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(0, strTempDir.Length - 1);
                    }

                    strPartDir = strPreNm.Substring(strPreNm.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strPreNm = strPreNm.Substring(0, strPreNm.Length - 1);
                    }

                    // 파일 Full경로 생성
                    if (isMapPath)
                    {
                        strFullNm = HttpContext.Current.Server.MapPath("~/") + strTempDir + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }
                    else
                    {
                        strFullNm = strDriveNm + ":/" + strPreNm + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }

                    FileInfo fileInfo = new FileInfo(strFullNm);

                    string strNewFileNm = "";

                    // 기존이미지가 존재할 경우
                    if (fileInfo.Exists)
                    {
                        int intFileIndex = 0;

                        // 확장자 분리
                        string strFileExtension = fileInfo.Extension;

                        // 확장자를 제외한 파일명 추출
                        string strRealNm = strFileNm.Replace(strFileExtension, "");

                        // 다른 파일명이 생성될때까지 Loop
                        do
                        {
                            intFileIndex++;
                            strNewFileNm = strRealNm + intFileIndex.ToString() + strFileExtension;
                            fileInfo = new FileInfo(sbFielUploadPath.ToString() + strNewFileNm);
                        }
                        while (fileInfo.Exists);

                        // 파일 Full경로 생성
                        strFullNm = sbFielUploadPath.ToString() + strNewFileNm;

                        // 파일 업로드
                        fuploads.PostedFile.SaveAs(strFullNm);
                    }
                    else
                    {
                        fuploads.PostedFile.SaveAs(strFullNm);

                    }

                    ArrReturn[0] = OriginalFileName;    // 원본 파일명
                    ArrReturn[1] = sbDBFilePath.ToString();          // 파일 업로드 경로

                }
                else
                {
                    // 파일 업로드
                    ArrReturn = null;
                }

                return ArrReturn;
            }
            else
            {
                ArrReturn = null;
                return ArrReturn;
            }
        }
        #endregion

        #region [UploadFiles] HttpPostedFile 파일 업로드
        public static string[] UploadFiles(HttpPostedFile fuploads, string SaveFilePath)
        {
            string[] ArrReturn = new string[3];
            string OriginalFileName = string.Empty; //원본 파일명
            StringBuilder sbFielUploadPath = new StringBuilder();
            StringBuilder sbDBFilePath = new StringBuilder();
            string strFileNm = string.Empty;
            string strFullNm = string.Empty;
            string strPreNm = string.Empty;
            string strDriveNm = string.Empty;
            string strSaveNm = string.Empty;
            string strPartialDir = string.Empty;
            string[] strArrDiv = { ":/" };
            string[] strArrPath;
            bool isMapPath = false;
            bool isMakeFolder = false;

            #region [업로드 할 로컬 폴더 경로 설정]
            sbFielUploadPath.Append(AppLocalPath);
            sbFielUploadPath.Append(AppFilePath);
            sbFielUploadPath.Append("/");
            sbFielUploadPath.Append(SaveFilePath);
            sbFielUploadPath.Append("/");
            #endregion

            #region [실제 사이트에서 사용할 경로]
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(AppFilePath);
            sbDBFilePath.Append("/");
            sbDBFilePath.Append(SaveFilePath);
            sbDBFilePath.Append("/");
            #endregion

            if (!string.IsNullOrEmpty(fuploads.FileName))
            {
                string[] ArrFileNames = fuploads.FileName.Split('\\');

                OriginalFileName = ArrFileNames[ArrFileNames.Length - 1];

                // 독립된 파일명 생성
                strFileNm = LocalFileControls.MakeFileName(fuploads.FileName);

                strPreNm = sbFielUploadPath.ToString().Replace("\\", "/");
                strArrPath = strPreNm.Split(strArrDiv, System.StringSplitOptions.RemoveEmptyEntries);
                strDriveNm = strArrPath[0];

                strPartialDir = strArrPath[1].Substring(strArrPath[1].Length - 1, 1);

                if (strPartialDir == "/")
                {
                    strArrPath[1] = strArrPath[1].Substring(0, strArrPath[1].Length - 1);
                }

                strPreNm = strArrPath[1] + "/";
                isMakeFolder = LocalFileControls.MakeFolder(sbFielUploadPath.ToString());

                if (isMakeFolder)
                {
                    string strTempDir = "";
                    string strPartDir = "";

                    strTempDir = sbFielUploadPath.ToString().Replace("\\", "/");

                    // 맨 앞에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(0, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(1);
                    }

                    // 맨 뒤에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(strTempDir.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(0, strTempDir.Length - 1);
                    }

                    strPartDir = strPreNm.Substring(strPreNm.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strPreNm = strPreNm.Substring(0, strPreNm.Length - 1);
                    }

                    // 파일 Full경로 생성
                    if (isMapPath)
                    {
                        strFullNm = HttpContext.Current.Server.MapPath("~/") + strTempDir + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }
                    else
                    {
                        strFullNm = strDriveNm + ":/" + strPreNm + "/" + strFileNm;
                        strSaveNm = strFileNm;
                    }

                    FileInfo fileInfo = new FileInfo(strFullNm);

                    string strNewFileNm = "";

                    // 기존이미지가 존재할 경우
                    if (fileInfo.Exists)
                    {
                        int intFileIndex = 0;

                        // 확장자 분리
                        string strFileExtension = fileInfo.Extension;

                        // 확장자를 제외한 파일명 추출
                        string strRealNm = strFileNm.Replace(strFileExtension, "");

                        // 다른 파일명이 생성될때까지 Loop
                        do
                        {
                            intFileIndex++;
                            strNewFileNm = strRealNm + intFileIndex.ToString() + strFileExtension;
                            fileInfo = new FileInfo(sbFielUploadPath.ToString() + strNewFileNm);
                        }
                        while (fileInfo.Exists);
                        strSaveNm = strNewFileNm;
                        // 파일 Full경로 생성
                        strFullNm = sbFielUploadPath.ToString() + strNewFileNm;

                        // 파일 업로드
                        fuploads.SaveAs(strFullNm);
                    }
                    else
                    {
                        fuploads.SaveAs(strFullNm);
                    }

                    ArrReturn[0] = OriginalFileName;    // 원본 파일명
                    ArrReturn[1] = strSaveNm;           //업로드된 파일명
                    ArrReturn[2] = sbDBFilePath.ToString();          // 파일 업로드 경로
                }
                else
                {
                    // 파일 업로드
                    ArrReturn = null;
                }

                return ArrReturn;
            }
            else
            {
                ArrReturn = null;
                return ArrReturn;
            }
        }
        #endregion

        #region [파일 읽기]
        public static string ProductRead(string FileName)
        {
            string FilePath = AppLocalPath + "/" + AppFilePath + "/Html/" + FileName;
            if (!File.Exists(FilePath))
            {
                return string.Empty;
            }
            else
            {
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);

                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                return str;
            }
        }
        #endregion
    }
}