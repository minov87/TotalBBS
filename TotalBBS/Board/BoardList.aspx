<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BoardList.aspx.cs" Inherits="TotalBBS.Board.BoardList" %>
<%@ Register Assembly="TotalBBS.Common.WebLib" Namespace="TotalBBS.Common.WebLib" TagPrefix="TB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTotalBBS" runat="server">

<script language="javascript" type="text/javascript">
<!--
    function SearchValidation(frm) {
        if ($.trim($("#KEY").val()) == "") {
            alert("검색어를 입력하세요.");
            $("#KEY").focus();
            return false;
        }

        return true;
    }

    function FrmView(frm1, frm2) {
        $("#cphTotalBBS_ParamIdx").val(frm1);
        __doPostBack('ctl00$cphTotalBBS$lbtnModify', '');
    }
//-->
</script>
<div class="contentArea">
    <div class="contentAreaTop">
        <div class="titleArea">
            <h3 class="tit"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h3>
        </div>
                           
        <div class="searchArea ov_fl">
            <span class="fl_l">
                <asp:Literal ID="ltBoardCategory" runat="server" ></asp:Literal>&nbsp;<asp:DropDownList ID="ddlBoardCategory" runat="server" Height="20px" Width="130px"></asp:DropDownList>
            </span>
            <span class="fl_r">
                <asp:DropDownList ID="FIELD" runat="server" Height="20px" Width="100px"></asp:DropDownList>                            
                <asp:TextBox ID="KEY" CssClass="text" Width="200px" ValidationGroup="ListSearch" runat="server" MaxLength="20" ClientIDMode="Static"></asp:TextBox>        
                <asp:LinkButton ID="lbtnSearch" runat="server" OnClientClick="return SearchValidation(this);" ValidationGroup="ListSearch" OnClick="lbtnSearch_Click"></asp:LinkButton>
            </span>
            <span style="float:right">
                <asp:DropDownList ID="ddlPageViewRow" runat="server" OnSelectedIndexChanged="ddlPageViewRow_itemSelected" AutoPostBack="true">
                    <asp:ListItem Text="10줄 보기" Value="10" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="20줄 보기" Value="20"></asp:ListItem>
                    <asp:ListItem Text="30줄 보기" Value="30"></asp:ListItem>
                    <asp:ListItem Text="40줄 보기" Value="40"></asp:ListItem>
                    <asp:ListItem Text="50줄 보기" Value="50"></asp:ListItem>
                    <asp:ListItem Text="100줄 보기" Value="100"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </div>

        <p><asp:Literal ID="ltTotalPosts" runat="server"></asp:Literal> : <asp:Label ID="ltTotalCnt" runat="server" ></asp:Label></p>

        <div>
            <!-- 기본 게시글 노출 //-->
            <asp:Repeater ID="rptGetList" runat="server" OnItemDataBound="rptGetList_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <colgroup>
                            <col width="5%" />
                            <col width="10%" />c
                            <col width="10%" />
                            <col width="10%" />
                            <col width="%" />
                            <col width="7%" />
                            <col width="7%" />
                            <col width="5%" />
                        </colgroup>
                        <thaed>
                            <tr>
                                <th><asp:Literal ID="ltThChkBoxAll" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThIdx" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThBoardCate" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThWriteCate" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThSubject" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThViewCount" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThWriter" runat="server"></asp:Literal></th>
                                <th><asp:Literal ID="ltThRegdate" runat="server"></asp:Literal></th>
                            </tr>
                        </thaed>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td><asp:Literal ID="ltChkBoxList" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltIdx" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltBoardCate" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltWriteCate" runat="server"></asp:Literal></td>
                                <td><asp:LinkButton ID="lbtSubject" runat="server"></asp:LinkButton></td>
                                <td><asp:Literal ID="ltViewCount" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltWriter" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltRegdate" runat="server"></asp:Literal></td>
                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="ltNoData" runat="server"></asp:Literal>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <!--// 목록 -->
            <div class="buttonwrapper">
                <asp:LinkButton ID="lbtnCreate" CssClass="buttons" runat="server" OnClick="lbtnCreate_Click"></asp:LinkButton>
            </div>
                            
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
            <asp:HiddenField ID="hdfBoardCategory" runat="server" Value="" />
            <asp:LinkButton ID="lbtnBoardCategorySearch" runat="server" OnClick="lbtnBoardCategorySearch_Click"></asp:LinkButton>
            <!-- //hidden -->
        </div>
        <!--// contents -->
        <div class="contentAreaBtm"></div>
    </div>
</div>
</asp:Content>