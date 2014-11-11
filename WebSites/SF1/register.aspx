<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblnotaut" runat="server" Font-Size="XX-Large" 
        Text="YOU ARE NOT AUTHORIZED TO ACCESS THIS PAGE." Visible="False"></asp:Label>
        <br />
    <asp:Panel ID="Panel1" runat="server" class="registration">
    <h1 style="color:#3498db;">User Profiles</h1>
            <asp:Label ID="lblerror" runat="server" ForeColor="#FF0066"></asp:Label>
    <table>
        <tr>
            <td>Username</td><td>
            <asp:TextBox ID="txtuser" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Password </td><td>
            <asp:TextBox ID="txtpass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr><td>
            <asp:Label ID="lblretype" runat="server" Text="Re-Type Password"></asp:Label>
            </td><td>
                <asp:TextBox ID="txtretype" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td></tr>
        <tr>
            <td>User Type</td><td>
            <asp:DropDownList ID="ddlutype" runat="server" style="width: 201px;">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Description / Name</td><td>
            <asp:TextBox ID="txtdesc" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Operation</td>
            <td>
                <asp:DropDownList ID="ddloper" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr><td>
            <asp:Label ID="lblactive" runat="server" Text="Active" Visible="False"></asp:Label>
            </td><td>
                <asp:CheckBox ID="chkactive" runat="server" Visible="False" />
            </td></tr>
        <tr><td colspan=2 align="center">
            <asp:Button ID="cmdsave" runat="server" Text="Save" Width="137px" />
            </td></tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
    <div class="tblreg">
    <br />
    <b>Search:</b>
    <br />
        <asp:TextBox ID="txtsearch" runat="server" Width="250px" 
            placeholder="Type Name or Username Here"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Search" />
        <br />
        &nbsp;<asp:Table ID="tblregister" runat="server" class="tblre">
        </asp:Table>
        </div>
    </asp:Panel>
</asp:Content>

