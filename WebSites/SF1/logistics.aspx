<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="logistics.aspx.vb" Inherits="logistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="navi">
                <asp:Button
            ID="cmddowntime" runat="server" Text="Downtime" Visible="False" />
                <asp:Label ID="lbluser" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="btnout" runat="server" Text="Logout" />
    </div>
    <br />
    <asp:Panel ID="Panel1" runat="server" style="margin: 12px;">
        MO No :
        <asp:TextBox ID="txtmono" runat="server"></asp:TextBox>
        &nbsp; Resource Name :
        <asp:DropDownList ID="ddlresource" runat="server" Height="19px" Width="147px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button ID="cmdsearch" runat="server" Text="Search" />
    </asp:Panel>
    <asp:Label ID="lbldetails" runat="server"></asp:Label>
    <br />
    &nbsp;<asp:Table ID="Table1" runat="server" class="tblre">
    </asp:Table>
    <asp:Label ID="txtfound" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="Panel2" runat="server" Visible="False">
        <asp:Label ID="lblerror" runat="server" 
    ForeColor="Red"></asp:Label>
        <p style="margin-left: 22px;">Additional Substrate :&nbsp;
        <asp:TextBox ID="txtaddsubs" runat="server"></asp:TextBox>
            <asp:Label ID="lbluomsubs" runat="server"></asp:Label>
    <asp:Button ID="btnaddsubs" runat="server" Text="Add" />
    </p>
    </asp:Panel>
</asp:Content>

