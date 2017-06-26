<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/Template/NoneLayout.Master" AutoEventWireup="true" CodeBehind="BoardReplyList.aspx.cs" Inherits="TotalBBS.BackOffice.Board.BoardReplyList" %>
<%@ Register Assembly="TotalBBS.Common.WebLib" Namespace="TotalBBS.Common.WebLib" TagPrefix="TB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTotalBBS" runat="server">
    <div class="titleArea">
        <h1 class="page-title txt-color-blueDark"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h1>
    </div>
    <div>
        <asp:Literal ID="ltTotalPosts" runat="server"></asp:Literal> : <asp:Label ID="ltTotalCnt" runat="server" ></asp:Label>
    </div>
    <div>
        <asp:Repeater ID="rptGetList" runat="server" OnItemDataBound="rptGetList_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover dataTable no-footer">
                    <colgroup>
                        <col width="50%" />
                        <col width="50%" />
                    </colgroup>
                    <thaed>
                    </thaed>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                        <tr>
                            <th style="text-align:center;"><asp:Literal ID="ltThWriter" runat="server"></asp:Literal></th>
                            <th style="text-align:center;" class="sorting"><asp:Literal ID="lbtThRegDate" runat="server"></asp:Literal></th>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2"><asp:Literal ID="ltContent" runat="server"></asp:Literal></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Literal ID="ltNoData" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <!--// 목록 -->
        <div class="buttonwrapper dt-toolbar-footer">
            <div clss="col-sm-6 col-xs-12 hidden-xs">&nbsp;</div>
            <!-- 페이징-->         
            <div class="col-xs-12 col-sm-6">
                <div class="dataTables_paginate paging_simple_numbers" id="datatable_tabletools_paginate">
                    <ul class="paging pagination">
			            <TB:PagingHelper ID="PagingHelper1" SkinID="PagingHelper" runat="server" Width="100%" OnOnPageIndexChanged="PagingHelper1_OnPageIndexChanged" />
		            </ul>  
                </div>
            </div>
            <!--// 페이징 -->
        </div>


        <!--// contents end -->
        <!-- hidden -->
        <asp:HiddenField ID="ParamIdx" runat="server" />
        <asp:HiddenField ID="ParamPage" runat="server" Value="" />
        <asp:HiddenField ID="ParamPageViewRow" runat="server" Value="" />
        <!-- //hidden -->
    </div>
</asp:Content>