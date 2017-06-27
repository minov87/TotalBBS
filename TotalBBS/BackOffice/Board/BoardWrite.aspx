<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BackOffice/Template/SiteLayout.Master" CodeBehind="BoardWrite.aspx.cs" Inherits="TotalBBS.BackOffice.Board.BoardWrite"%>
<%@ Register Assembly="TotalBBS.Common.WebLib" Namespace="TotalBBS.Common.WebLib" TagPrefix="TB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTotalBBS" runat="server">
    <script type="text/javascript">
        // 시작 첨부파일 동적으로 추가 삭제 하기
        function call() {

            //파일을 첨부하지 않으면 추가 막음
            var iFileUploadList = $("input[name='BoardFileUpload']");
            var TotalCnt = 0;
            var FileListCnt = 0;
            for (i = 0; i < iFileUploadList.length; i++) {
                if ($.trim($("input[name='BoardFileUpload']")[i].value) != "") {
                    FileListCnt = FileListCnt + 1;
                }
                TotalCnt = TotalCnt + 1;
            }
            if (TotalCnt == FileListCnt) {
                var tbl = document.getElementById("tbl");
                var tbl_length = tbl.rows.length;
                var tr1 = tbl.insertRow(tbl_length);
                tr1.id = "tr1_" + tbl_length.toString();
                var td1 = tr1.insertCell(0);
                td1.id = "td1_" + tbl_length;

                td1.innerHTML = "<input type=\"file\" name=\"BoardFileUpload\" id=\"BoardFileUpload_" + (tbl_length + 1) + "\" onchange=\"javascript:FileExtension(this);\" \"  style=\"width: 350px;\" />" + " ";
                td1.innerHTML += "<input type='button' onclick='delRow();' style='width:70px' value='삭제' />"
                td1.style.borderWidth = '0';
            }
            else {
                alert("파일을 선택 후 추가 해주세요");
            }
        }

        function del() {
            var tbl = document.getElementById("tbl");

            var tbl_length = tbl.rows.length;
            tbl.deleteRow(tbl_length - 1);
        }

        function delRow() {
            var currentElement = window.event.srcElement;
            var currentTable = currentElement.parentNode.parentNode.parentNode;
            var currentRowIndex = currentElement.parentNode.parentNode.rowIndex;
            currentTable.deleteRow(currentRowIndex);
        }

        function FileExtension(file) {
            if (file && file.value.length > 0) {
                if (event.srcElement.value.toLowerCase().match(/(.asp|.aspx|.bat|.jsp|.php|.sh)/)) {
                    alert("업로드 할 수 없는 파일 입니다.");
                    file.select();
                    document.selection.clear();
                }
            }
        }

        function InputValidation() {

            var iFileUploadList = $("input[name='BoardFileUpload']");
            var TotalCnt = 0;
            var FileListCnt = 0;
            for (i = 0; i < iFileUploadList.length; i++) {
                if ($.trim($("input[name='BoardFileUpload']")[i].value) != "") {
                    FileListCnt = FileListCnt + 1;
                }
                TotalCnt = TotalCnt + 1;
            }

            if ($.trim($("#ctl00$cphTotalBBS$txtUserId").val()) == "") {
                alert("작성자 아이디를 입력하세요.");
                $("#ctl00$cphTotalBBS$txtUserId").focus();
                return false;
            }

            if ($.trim($("#ctl00$cphTotalBBS$txtWriter").val()) == "") {
                alert("작성자 이름을 입력하세요.");
                $("#ctl00$cphTotalBBS$txtWriter").focus();
                return false;
            }

            if ($.trim($("#ctl00$cphTotalBBS$txtSubject").val()) == "") {
                alert("제목을 입력하세요.");
                $("#ctl00$cphTotalBBS$txtSubject").focus();
                return false;
            }

            /*
            if ($.trim($("#txtContent").text()) == "") {
                alert("내용을 입력하세요.");
                $("#txtContent").focus();
                return false;
            }*/

            if (TotalCnt > 1) {
                if (TotalCnt != FileListCnt) {
                    alert('파일 업로더 컨트롤을 삭제 후 등록해 주세요');
                    return false;
                }
            }

            return true;
        }
    </script>
    <div class="contentArea">
        <div class="contentAreaTop dataTables_wrapper form-inline dt-bootstrap no-footer">
            <!-- contents -->
            <div class="titleArea">
                <h1 class="page-title txt-color-blueDark"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h1>
            </div>

            <table class="dataWrite table table-striped table-bordered table-hover dataTable no-footer">
                <colgroup>
                    <col width="200px" />
                    <col width="280px" />
                    <col width="200px" />
                    <col width="280px" />
                </colgroup>
                <tbody>
                    <tr class="noedit">
                        <th><asp:Literal ID="ltBoardCategory" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList CssClass="form-control input-sm" ID="ddlBoardCategory" OnSelectedIndexChanged="ddlBoardCaregory_itemSelected" runat="server" AutoPostBack="true" style="width:20%"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltWriteCategory" runat="server"></asp:Literal></th>
                        <td colspan="3" >
                            <asp:DropDownList ID="ddlWriteCategory" runat="server" CssClass="form-control input-sm" style="width:20%"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltUserId" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text form-control" ID="txtUserId" MaxLength="50" />
                        </td>
                    </tr>
						
                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltWriter" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text form-control" ID="txtWriter" MaxLength="50" />
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltSubject" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text form-control" ID="txtSubject" MaxLength="100" style="Width:97%" />
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th class="tit"><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
                        <td class="edit" colspan="3">
                            <asp:TextBox runat="server" CssClass="ContentsArea" TextMode="MultiLine" ID="txtContent" MaxLength="100"/>
                        </td>
                    </tr>

                    <tr class="noedit" style="border:none;">
                        <th>첨부파일</th>
                        <td style="border:none;" colspan="3">
                            <table id="tbl" style="border:none;" class="table table-striped table-bordered table-hover dataTable no-footer">
                                <asp:Repeater ID="rptBoard_Attached" runat="server" OnItemDataBound="rptBoard_Attached_OnItemDataBound" >
                                    <ItemTemplate>    
                                        <tr>
                                            <td style="border:none;">
                                                <asp:Literal ID="ltAttachedFile" runat="server" ></asp:Literal>&nbsp; <asp:Literal ID="ltAttachedCheckBox" runat="server" ></asp:Literal>
                                            </td>
                                            <td style="border:none;">
                                
                                            </td>
                                        </tr>
                                     </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td style="border:none;">
                                        <input type="file" class="form-control" name="BoardFileUpload" id="inFileUpload_1" onchange="javascript:FileExtension(this);" style="width: 50%;" />
                                        <input type="button" class="buttons" id="btnFileAdd" name="btnFileAdd" value="추가" style="width: 50%;" onclick="call();"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr class="noedit" id="trVisible_2" runat="server">
                        <th width="25%">
                            <asp:Literal ID="ltRegiDate" runat="server"></asp:Literal>
                        </th>
                        <td width="25%">
                            <asp:Literal ID="ltRegiDateValue" runat="server"></asp:Literal>
                        </td>
                        <th width="25%">
                            <asp:Literal ID="ltViewCnt" runat="server"></asp:Literal>
                        </th>
                        <td width="25%">
                            <asp:Literal ID="ltViewCntValue" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div>
            <div class="buttonwrapper dt-toolbar-footer">
                <div class="col-sm-6 col-xs-12 hidden-xs">
                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="buttons" OnClick="lbtnCancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnValidationSave" CssClass="buttons" runat="server" OnClientClick="return InputValidation();" OnClick="lbtnValidation_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnValidationModify" CssClass="buttons" runat="server" OnClientClick="return InputValidation();" OnClick="lbtnValidation_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnValidationDelete" CssClass="buttons" runat="server" OnClick="lbtnValidation_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
        <!--// contents -->

        <asp:HiddenField ID="hdfCMD" runat="server" />
        <asp:HiddenField ID="hdfIdx" runat="server" />
        <asp:HiddenField ID="hdfParamPage" runat="server" />
        <asp:HiddenField ID="hdfEmailContent" runat="server" />

        <div class="contentAreaBtm"></div>
        <iframe id="ifrmReply" src="" runat="server" style="border:0px;height:500px;width:100%;overflow-x: hidden;margin-top:15px;"></iframe>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphTotalBBS_Footer" runat="server">
<script type="text/javascript" language="javascript">
    $('.ContentsArea').summernote({
        height: 200,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'hr']],
            ['view', ['fullscreen', 'codeview', 'help']]
        ]
    });

</script>
</asp:Content>