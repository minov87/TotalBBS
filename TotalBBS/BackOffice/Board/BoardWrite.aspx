<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BackOffice/Template/SiteLayout.Master" CodeBehind="BoardWrite.aspx.cs" Inherits="TotalBBS.BackOffice.Board.BoardWrite" %>
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

            var iFileUploadList = $("input[name='CatalogFileUpload']");
            var TotalCnt = 0;
            var FileListCnt = 0;
            for (i = 0; i < iFileUploadList.length; i++) {
                if ($.trim($("input[name='CatalogFileUpload']")[i].value) != "") {
                    FileListCnt = FileListCnt + 1;
                }
                TotalCnt = TotalCnt + 1;
            }

            if ($.trim($("#txtProductName").val()) == "") {
                alert("제품명을 입력하세요. ");
                $("#txtProductName ").focus();
                return false;
            }
            if ($.trim($("#txtPublicationCompany").val()) == "") {
                alert("발행사를 입력하세요.");
                $("#txtPublicationCompany ").focus();
                return false;
            }
            if ($.trim($("#txtPublicationYear").val()) == "") {
                alert("발행연도를 입력하세요.");
                $("#txtPublicationYear ").focus();
                return false;
            }
            if ($.trim($("#txtPublicationLang").val()) == "") {
                alert("발행언어를 입력하세요.");
                $("#txtPublicationLang ").focus();
                return false;
            }
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
        <div class="contentAreaTop">
            <!-- contents -->
            <div class="titleArea">
                <h3 class="tit"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h3>
            </div>

            <table class="dataWrite">
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
                            <asp:DropDownList ID="ddlBoardCategory" OnSelectedIndexChanged="ddlBoardCaregory_itemSelected" runat="server" Height="20px" Width="250px" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltWriteCategory" runat="server"></asp:Literal></th>
                        <td colspan="3" >
                            <asp:DropDownList ID="ddlWriteCategory" runat="server" Height="20px" Width="250px" ></asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltUserId" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text" ID="txtUserId" MaxLength="50" style="Width:97%" />
                        </td>
                    </tr>
						
                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltWriter" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text" ID="txtWriter" MaxLength="50" style="Width:97%" />
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltSubject" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox runat="server" CssClass="text" ID="txtSubject" MaxLength="100" style="Width:97%" />
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th class="tit"><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
                        <td class="edit" colspan="3">
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="97%" Height="300px" ></asp:TextBox>
                        </td>
                    </tr>

                    <tr class="noedit" style="border:none;">
                        <th>첨부파일</th>
                        <td  style="border:none;" colspan="3">
                            <table id="tbl" style="border:none;">
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
                                        <input type="file" name="BoardFileUpload" id="inFileUpload_1" onchange="javascript:FileExtension(this);" style="width: 350px;" />
                                        <input type="button" id="btnFileAdd" name="btnFileAdd" style="width:70px" value="추가" onclick="call();"/>
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

            <div class="buttonwrapper">
                <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbtnCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnValidationDelete" runat="server" OnClick="lbtnValidation_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnValidationSave" runat="server" OnClientClick="return InputValidation();" OnClick="lbtnValidation_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnValidationModify" runat="server" OnClientClick="return InputValidation();" OnClick="lbtnValidation_Click"></asp:LinkButton>
            </div>
        </div>
        <!--// contents -->

        <asp:HiddenField ID="hdfCMD" runat="server" />
        <asp:HiddenField ID="hdfIdx" runat="server" />
        <asp:HiddenField ID="hdfParamPage" runat="server" />
        <asp:HiddenField ID="hdfEmailContent" runat="server" />

        <div class="contentAreaBtm"></div>
    </div>
</asp:Content>