<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BackOffice/Template/SiteLayout.Master" CodeBehind="Test.aspx.cs" Inherits="TotalBBS.BackOffice.Board.Test" %>
<%@ Register Assembly="TotalBBS.Common.WebLib" Namespace="TotalBBS.Common.WebLib" TagPrefix="TB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTotalBBS" runat="server">
    <div>
        <asp:DropDownList ID="ddlBoardCategory" OnSelectedIndexChanged="ddlBoardCaregory_itemSelected" runat="server" Height="20px" Width="250px" AutoPostBack="true" ></asp:DropDownList>
        <asp:DropDownList ID="ddlWriteCategory" runat="server" Height="20px" Width="250px" ></asp:DropDownList>
    </div>
</asp:Content>