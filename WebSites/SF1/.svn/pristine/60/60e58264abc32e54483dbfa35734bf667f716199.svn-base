<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="settings.aspx.vb" Inherits="settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 135px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Width="508px" style="background:rgb(233, 235, 236);float: left;margin-left: 20px;">
    
    <table class="settings">
        <tr><td>
            <asp:Label ID="lblerrs" runat="server" ForeColor="#CC3300"></asp:Label>
            </td></tr>
        <tr>
        <td>Server Name</td><td><asp:TextBox ID="txtserver" runat="server" 
                Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>Database Name</td><td><asp:TextBox ID="txtdb" runat="server" 
                Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>Integrated Security</td><td>
            <asp:DropDownList ID="ddlsec" runat="server">
                <asp:ListItem>False</asp:ListItem>
                <asp:ListItem>True</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td>User ID</td><td><asp:TextBox ID="txtuid" runat="server" 
                Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>Password</td><td>
            <asp:TextBox ID="txtpass" runat="server" 
                Width="290px" TextMode="Password"></asp:TextBox>
            </td>
            </tr>
            </table>
            <br />
            <table style="margin: 0 auto;">
            <tr>
        <td align="center">
            <asp:Button ID="cmdclear" runat="server" Text="Clear" Width="101px" />
            </td>
            <td>
            <asp:Button ID="saveset" runat="server" Text="Save Settings" Width="178px" />
            </td>
        </tr>
    </table>
                <table>
            <tr><td>
                <asp:Label ID="lbltblname" runat="server" Text="Table Name" Visible="False"></asp:Label>
                </td><td>
                    <asp:TextBox ID="txtlblname" runat="server" Visible="False" Width="133px"></asp:TextBox>
                </td><td>
                    <asp:Button ID="cmdsavetable" runat="server" Text="Save Table" 
                        Visible="False" />
                </td></tr>
            </table>
        </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" style="background:rgb(233, 235, 236); float: right;">
    <table class="settings">
        <tr><td colspan=2>
            <asp:Label ID="lblerrpath" runat="server" ForeColor="#CC3300"></asp:Label>
            </td></tr>
        <tr><td>File Path</td><td>
            <asp:TextBox ID="txtpath" runat="server" Width="290px"></asp:TextBox>
            </td></tr>
    </table>
    <table style="margin:0 auto;"><tr><td>
        <asp:Button ID="Button3" runat="server" Text="Clear" Width="101px" />
        </td><td>
            <asp:Button ID="Button4" runat="server" Text="Save Path" Width="178px" />
        </td></tr></table>
    </asp:Panel>
</asp:Content>

