<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BackOffice/Template/SiteLayout.Master" CodeBehind="BoardList.aspx.cs" Inherits="TotalBBS.BackOffice.Board.BoardList" %>
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

    function FrmModify(frm1, frm2) {
        $("#cphTotalBBS_ParamIdx").val(frm1);
        __doPostBack('ctl00$cphTotalBBS$lbtnModify', '');
    }

    function FrmSort(frm1) {
        $("#cphTotalBBS_ParamSort").val(frm1);
        __doPostBack('ctl00$cphTotalBBS$lbtnSort', '');
    }
//-->
</script>
<div class="contentArea">
    <div class="contentAreaTop dataTables_wrapper form-inline dt-bootstrap no-footer">
        <div class="titleArea">
            <h1 class="page-title txt-color-blueDark"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h1>
        </div>
        
        <div>
            <asp:Literal ID="ltTotalPosts" runat="server"></asp:Literal> : <asp:Label ID="ltTotalCnt" runat="server" ></asp:Label>
        </div>
        
        <div class="searchArea ov_fl dt-toolbar">
            <div class="col-xs-12 col-sm-6">
                <div class="dataTables_filter">
                    <span class="fl_l">
                        <asp:Literal ID="ltBoardCategory" runat="server" ></asp:Literal>&nbsp;<asp:DropDownList ID="ddlBoardCategory" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </span>
                    <span class="fl_r">
                        <asp:DropDownList ID="FIELD" CssClass="form-control input-sm" runat="server"></asp:DropDownList>                            
                        <asp:TextBox ID="KEY" CssClass="text form-control" ValidationGroup="ListSearch" runat="server" MaxLength="20" ClientIDMode="Static"></asp:TextBox>        
                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClientClick="return SearchValidation(this);" ValidationGroup="ListSearch" OnClick="lbtnSearch_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnList" runat="server" OnClick="lbtnList_Click"></asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-sm-6 col-xs-12 hidden-xs">
                <span style="float:right">
                    <asp:DropDownList ID="ddlPageViewRow" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlPageViewRow_itemSelected" AutoPostBack="true">
                        <asp:ListItem Text="10줄 보기" Value="10" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="20줄 보기" Value="20"></asp:ListItem>
                        <asp:ListItem Text="30줄 보기" Value="30"></asp:ListItem>
                        <asp:ListItem Text="40줄 보기" Value="40"></asp:ListItem>
                        <asp:ListItem Text="50줄 보기" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100줄 보기" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
        </div>

        <div>
            <asp:Repeater ID="rptGetList" runat="server" OnItemDataBound="rptGetList_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-striped table-bordered table-hover dataTable no-footer">
                        <colgroup>
                            <col width="5%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="%" />
                            <col width="5%" />
                            <col width="7%" />
                            <col width="11%" />
                        </colgroup>
                        <thaed>
                            <tr>
                                <th style="text-align:center;"><asp:Literal ID="ltThChkBoxAll" runat="server"></asp:Literal></th>
                                <th style="text-align:center;" class="sorting"><asp:LinkButton ID="lbtThIdx" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;" class="sorting"><asp:LinkButton ID="lbtThBoardCate" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;" class="sorting"><asp:LinkButton ID="lbtThWriteCate" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;" class="sorting"><asp:LinkButton ID="lbtThSubject" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;"><asp:LinkButton ID="lbtThViewCount" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;"><asp:LinkButton ID="lbtThWriter" runat="server"></asp:LinkButton></th>
                                <th style="text-align:center;" class="sorting"><asp:LinkButton ID="lbtThRegdate" runat="server"></asp:LinkButton></th>
                            </tr>
                        </thaed>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td style="text-align:center;"><asp:Literal ID="ltChkBoxList" runat="server"></asp:Literal></td>
                                <td style="text-align:center;"><asp:Literal ID="ltIdx" runat="server"></asp:Literal></td>
                                <td style="text-align:center;"><asp:Literal ID="ltBoardCate" runat="server"></asp:Literal></td>
                                <td style="text-align:center;"><asp:Literal ID="ltWriteCate" runat="server"></asp:Literal></td>
                                <td><asp:LinkButton ID="lbtSubject" runat="server"></asp:LinkButton></td>
                                <th style="text-align:center;"><asp:Literal ID="ltViewCount" runat="server"></asp:Literal></th>
                                <td style="text-align:center;"><asp:Literal ID="ltWriter" runat="server"></asp:Literal></td>
                                <td style="text-align:center;"><asp:Literal ID="ltRegdate" runat="server"></asp:Literal></td>
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
                <div clss="col-sm-6 col-xs-12 hidden-xs" style="width:50%;float:left;">
                    <div class="">
                        <asp:LinkButton ID="lbtnCreate" CssClass="buttons" runat="server" OnClick="lbtnCreate_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" CssClass="buttons mg_l5" runat="server" OnClick="lbtnDelete_Click" OnClientClick="return fnCheckBoxCustomerRemove('ChkBoxList')"></asp:LinkButton>
                    </div>
                </div>
                <!-- 페이징-->         
                <div class="col-xs-12 col-sm-6" style="width:50%">
                    <div class="dataTables_paginate paging_simple_numbers" id="datatable_tabletools_paginate">
			            <TB:PagingHelper ID="PagingHelper1" SkinID="PagingHelper" runat="server" Width="100%" OnOnPageIndexChanged="PagingHelper1_OnPageIndexChanged" />
                    </div>
                </div>
                <!--// 페이징 -->
            </div>


            <!--// contents end -->
            <!-- hidden -->
            <asp:HiddenField ID="ParamIdx" runat="server" />
            <asp:HiddenField ID="ParamPage" runat="server" Value="" />
            <asp:HiddenField ID="ParamPageViewRow" runat="server" Value="" />
            <asp:HiddenField ID="ParamSort" runat="server" Value="" />
            <asp:LinkButton ID="lbtnSort" runat="server" OnClick="lbtnSort_Click"></asp:LinkButton>
            <asp:LinkButton ID="lbtnModify" runat="server" OnClick="lbtnModify_Click"></asp:LinkButton>
            <asp:HiddenField ID="hdfBoardCategory" runat="server" Value="" />
            <asp:LinkButton ID="lbtnBoardCategorySearch" runat="server" OnClick="lbtnBoardCategorySearch_Click"></asp:LinkButton>
            <!-- //hidden -->
        </div>
        <!--// contents -->
        <div class="contentAreaBtm"></div>
    </div>
</div>
</asp:Content>