<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BoardReplyList.aspx.cs" Inherits="TotalBBS.Board.BoardReplyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTotalBBS" runat="server">
    <asp:Repeater ID="rptGetList" runat="server" OnItemDataBound="rptGetList_ItemDataBound">
        <HeaderTemplate>
            <table>
                <colgroup>
                    <col width="50%" />
                    <col width="50%" />
                </colgroup>
                <thaed>
                    <tr>
                        <th><asp:Literal ID="ltThWriter" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltThRegDate" runat="server"></asp:Literal></th>
                    </tr>
                </thaed>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr>
                        <td colspan="2"><asp:Literal ID="ltContent" runat="server"></asp:Literal></td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Literal ID="ltNoData" runat="server"></asp:Literal>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>

        <!-- 페이징-->                           
    <ul class="paging">
		<TB:PagingHelper ID="PagingHelper1" SkinID="PagingHelper" runat="server" Width="100%" OnOnPageIndexChanged="PagingHelper1_OnPageIndexChanged" />
	</ul>                            
    <!--// 페이징 -->

    <!--// contents end -->
    <!-- hidden -->
    <asp:HiddenField ID="ParamIdx" runat="server" />
    <asp:HiddenField ID="ParamPage" runat="server" Value="" />
    <asp:HiddenField ID="ParamPageViewRow" runat="server" Value="" />
    <!-- //hidden -->
</asp:Content>
