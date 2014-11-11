<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admingetmo.aspx.vb" Inherits="admingetmo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Panel5" runat="server" style="margin-left: 35px;">
    
    <asp:Label ID="lblerr" runat="server" ForeColor="#CC3300"></asp:Label>
    <table class="operator">
        <tr><td class="style2">Resource Name</td><td class="style2">
            <asp:DropDownList ID="ddlrecresource" runat="server">
            </asp:DropDownList>
            </td></tr>
        <tr><td>Operation</td><td>
            <asp:DropDownList ID="ddlrecoper" runat="server">
            </asp:DropDownList>
            </td></tr>
        <tr><td>
            <asp:Button ID="cmdgetmo" runat="server" Text="GET MO" />
            </td><td>
                <asp:Button ID="cmdcancel" runat="server" Text="Cancel" />
            </td></tr>
        <tr><td>
            <asp:Label ID="lblmono" runat="server" Text="MO No." Visible="False"></asp:Label>
            </td><td>
                <asp:TextBox ID="txtrecmo" runat="server" Visible="False" AutoPostBack="True"></asp:TextBox>
            </td></tr>
            <tr>
            <td>REFERENCE NO.</td>
            <td>
                <asp:TextBox ID="txtrefno" runat="server"></asp:TextBox>&nbsp;<asp:Button 
                    ID="cmdgetref" runat="server" Text="GET MO BY REFERENCE" />
                </td>
            </tr>
    </table>
    </asp:Panel>
    <asp:Label ID="lblsbu" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lbloperation"
            runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblresource" 
            runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Panel ID="Panel4" runat="server" Visible="False">
        <asp:Label ID="lblnomo" runat="server"></asp:Label>
    <table style="width:100%">
        <tr><td style="width:50%" valign="top">
            <table class="operator">
            <tr>
            <td><asp:Label ID="lbloperatorname" runat="server" Text="Operator :"></asp:Label></td>
            <td>
            <asp:TextBox ID="txtoper" runat="server"></asp:TextBox></td>
            </tr><tr>
            <td><asp:Label ID="lblassopername" runat="server" Text="Assistant Operator:"></asp:Label></td>
            <td><asp:TextBox ID="txtaoper" runat="server"></asp:TextBox>
                    </td>
            </tr><tr>
            <td><asp:Label ID="lblhelpername" runat="server" Text="Helper :"></asp:Label></td>
            <td><asp:TextBox ID="txthelper" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr><td colspan=3>&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td></tr>
            </table>
            <br />
    <asp:Panel ID="Panel1" runat="server">
        <div class="mo">
            <h2 class="h2ness">MO</h2>
            
            <br />&nbsp;BARCODE: 
            <asp:TextBox ID="txtmo" runat="server" Width="216px" AutoPostBack="True" style="height: 22px"></asp:TextBox>
            <asp:Label ID="lbloutmo" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblcountmo" runat="server"></asp:Label>
            <asp:Panel ID="Panel2" runat="server">
            <asp:Label ID="output" runat="server"></asp:Label>
            <br />
            </asp:Panel>
            <br />
        </div>
        <br />
        </asp:Panel>
        </td><td style="width:50%" valign="top">

<asp:Panel ID="Panel3" runat="server">
    <table style="background-color:rgb(231, 231, 231);width:100%">
        <tr>
            <td><asp:Button ID="cmddirect" runat="server" Text="Direct" /></td>
            <td><asp:Button ID="cmdindirect" runat="server" Text="Indirect" /></td>
            <td><asp:Button ID="cmdprodd" runat="server" Text="Prod. Downtime" /></td>
        </tr>
        <tr>
            <td colspan=3>
                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="lblrem" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="lblrem2" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblop" runat="server"></asp:Label>
                <asp:Label ID="lblddltype" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <table style="background-color:#E7E7E7;width:80%">
        <tr>
            <td>Time Start:</td>
            <td>
                <asp:Label ID="echosd" runat="server"></asp:Label>
                <asp:Label ID="lblsDate" runat="server" ForeColor="Black" BackColor="#99CCFF" Visible="False"></asp:Label>
                <asp:Label ID="lblstartTime" runat="server"></asp:Label>
                <asp:Label ID="lblsTime" runat="server" ForeColor="White" BackColor="#6699FF" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Time End:</td>
            <td>
                <asp:Label ID="echoed" runat="server"></asp:Label>
                <asp:Label ID="lbleDate" runat="server" ForeColor="Black" BackColor="#99CCFF" Visible="False"></asp:Label>
                <asp:Label ID="lblendTime" runat="server"></asp:Label>
                <asp:Label ID="lbleTime" runat="server" ForeColor="White" BackColor="#6699FF" Visible="False"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlmm" runat="server" Height="20px" Width="56px">
                <asp:ListItem Value="01">Jan</asp:ListItem>
                <asp:ListItem Value="02">Feb</asp:ListItem>
                <asp:ListItem Value="03">Mar</asp:ListItem>
                <asp:ListItem Value="04">Apr</asp:ListItem>
                <asp:ListItem Value="05">May</asp:ListItem>
                <asp:ListItem Value="06">Jun</asp:ListItem>
                <asp:ListItem Value="07">Jul</asp:ListItem>
                <asp:ListItem Value="08">Aug</asp:ListItem>
                <asp:ListItem Value="09">Sep</asp:ListItem>
                <asp:ListItem Value="10">Oct</asp:ListItem>
                <asp:ListItem Value="11">Nov</asp:ListItem>
                <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="txtdd" runat="server" Width="24px" 
                    placeholder="dd"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtyy" runat="server" Width="41px" placeholder="yyyy"></asp:TextBox>
                <asp:TextBox ID="txth" runat="server" Width="18px" placeholder="h" 
                    MaxLength="2"></asp:TextBox>
                <asp:TextBox ID="txtm" runat="server" placeholder="m" Width="18px" 
                    MaxLength="2"></asp:TextBox>
                <asp:TextBox ID="txts" runat="server" placeholder="s" Width="18px" 
                    MaxLength="2"></asp:TextBox>
                <br />
                <asp:Button ID="btnupdatetime" runat="server" Text="Update Time" />
                <asp:Label ID="lble" runat="server"></asp:Label>
            </td>
        </tr>
            <tr>
                <td valign="top">Remarks :</td>
                <td><asp:TextBox ID="lbloper_remarks" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblgood" runat="server" Text="Good :"></asp:Label></td>
                <td>
                <asp:TextBox ID="txtgoods" runat="server" Width="86px" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="txtg" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblrejects1" runat="server" Text="Rejects:"></asp:Label></td>
                <td>
                <asp:TextBox ID="txtrejects" runat="server" Width="86px" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lbluom" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top"><asp:Label ID="lbladdsubs" runat="server" 
                        Text="Additional Substrate:" Visible="False"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtaddsubs" Width="86px" runat="server" AutoPostBack="True" 
                        MaxLength="10" ReadOnly="True" Visible="False"></asp:TextBox>
                    <br /><br />
                    <asp:Button ID="btnaddsubs" runat="server" Font-Size="Smaller" 
                        Text="Search Additional Substrate" Visible="False" />
                    <br />
                    <asp:Label ID="lblgD" runat="server"></asp:Label>
                    <asp:Label ID="lblerinadd" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lbloutputaddsubs" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblsubsid" runat="server"></asp:Label>
                    <asp:Label ID="lblsubserr" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblheadcount" runat="server" Text="Head Count: " Visible="False"></asp:Label></td>
                <td><asp:TextBox ID="txtheadcount" runat="server" Width="50px" Visible="False"></asp:TextBox></td>
            </tr>
        </table>
    </asp:Panel>
    </td></tr>
    </table>
        </asp:Panel>
</asp:Content>

