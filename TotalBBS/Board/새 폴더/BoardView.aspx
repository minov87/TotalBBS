<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BoardView.aspx.cs" Inherits="TotalBBS.Board.BoardWrite" %>
<%@ Register Assembly="TotalBBS.Common.WebLib" Namespace="TotalBBS.Common.WebLib" TagPrefix="TB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTotalBBS" runat="server">
    <script type="text/javascript">
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
                        <th><asp:Literal ID="ltThBoardCategory" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:Literal runat="server" CssClass="text" ID="ltBoardCategory"></asp:Literal>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltThWriteCategory" runat="server"></asp:Literal></th>
                        <td colspan="3" >
                            <asp:Literal runat="server" CssClass="text" ID="ltWriteCategory"></asp:Literal>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltThUserId" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:Literal runat="server" CssClass="text" ID="ltUserId"></asp:Literal>
                        </td>
                    </tr>
						
                    <tr class="noedit">
                        <th style="background:#F2F2F2;"><asp:Literal ID="ltThWriter" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:Literal runat="server" CssClass="text" ID="ltWriter"></asp:Literal>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th><asp:Literal ID="ltThSubject" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:Literal runat="server" CssClass="text" ID="ltSubject"></asp:Literal>
                        </td>
                    </tr>

                    <tr class="noedit">
                        <th class="tit"><asp:Literal ID="ltThContent" runat="server"></asp:Literal></th>
                        <td class="edit" colspan="3">
                            <asp:Literal ID="ltContent" runat="server" CssClass="text"></asp:Literal>
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