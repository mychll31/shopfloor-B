<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="downtime.aspx.vb" Inherits="downtime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 204px;
        }
        .style2
        {
            width: 70px;
        }
        .style3
        {
            width: 78px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <asp:Panel ID="Panel1" runat="server" class="registration" Width="397px">
    <h1 style="color:#3498db;">DownTime:</h1>
        <p style="color:#3498db;">
            <asp:Label ID="lblerror" runat="server"></asp:Label>
        </p>
    <table class="tbldt">
    <tr><td valign="top" class="style3">Code :</td><td>
        <asp:TextBox ID="txtcode" runat="server" Width="160px"></asp:TextBox>
        &nbsp;<asp:Label ID="lblnonjob" runat="server" Font-Size="Smaller"></asp:Label>
        </td></tr>
    <tr><td class="style3">Reason :</td><td>
        <asp:TextBox ID="txtreason" runat="server" Width="160px"></asp:TextBox>
        </td></tr>
    <tr><td class="style3">Type :</td><td >
        <asp:DropDownList ID="ddltype" runat="server" Width="160px">
        </asp:DropDownList>
        </td></tr>
    <tr><td class="style3">Description :</td><td>
        <asp:TextBox ID="txtdesc" runat="server" Width="160px"></asp:TextBox>
        </td></tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label1" runat="server" Text="Active :" Visible="False"></asp:Label>
        </td><td>
            <asp:CheckBox ID="chkactive" runat="server" Visible="False" Checked="True" />
        </td>
    </tr>
        <tr><td colspan=2 align="center">
            <asp:Button ID="Button2" runat="server" Text="Save" Width="137px" />
            </td></tr>
    </table>
     </asp:Panel>
     <br /><br />
     <div class="divdt">
     <div class="dttitle">
                 <b>SEARCH<br /></b>
                 <asp:TextBox ID="txtsearch" runat="server" placeholder="Type Code or Name Here" 
                     Width="213px"></asp:TextBox>
                 &nbsp;<asp:Button ID="cmdsearch" runat="server" Text="Search" />
        <br /><br />
        </div>
     <asp:Table ID="tbldt" runat="server" class="tbldt">
         </asp:Table>
         <br />
         </div>


     </asp:Content>

