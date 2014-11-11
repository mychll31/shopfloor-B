<%@ Page Title="About Us" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="About.aspx.vb" Inherits="About" %>

<%--<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="navi">
                <asp:Button ID="Button1" runat="server" Text="Get MO" />
    <asp:Button ID="navviewLogs" runat="server" Text="View List" class="curr"/>
    <asp:Button ID="cmdreg" runat="server" Text="Registration" Visible="False" />
                <asp:Button
            ID="cmddowntime" runat="server" Text="Downtime" Visible="False" />
                <asp:Label ID="lbluser" runat="server"></asp:Label>
<asp:Button ID="seslogout" runat="server" Text="Logout" />
    </div>

    <h1 class="h2ness">TASKS</h1>
    <b>Search</b><br />
    <asp:TextBox ID="txtsearch" runat="server" Width="535px"></asp:TextBox>
<asp:Button ID="Button2" runat="server" Text="Search" />
<br />Type Process, Note, MO, Description, Operator, A.Operator, Helper, Start Date or End Date
<br /><br />
<asp:Table ID="Table1" runat="server" class="tbl_task">
        </asp:Table>
    <p></p>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="tasktitle">
    <h1 class="h2ness">TASKS</h1>
    <b>Search</b><br />
        <table>
        <tr>
            <td style="padding: 0px 10px !important;text-align:center">MO</td>
            <td style="padding: 0px 10px !important;text-align:center">Process</td>
            <td style="padding: 0px 10px !important;text-align:center">Resource<br />Name</td>
            <td style="padding: 0px 10px !important;text-align:center">Operator</td>
            <td style="padding: 0px 10px !important;text-align:center">Assistant<br />Operator</td>
            <td style="padding: 0px 10px !important;text-align:center">Helper</td>
            <td style="padding: 0px 10px !important;text-align:center">Start</td>
            <td style="padding: 0px 10px !important;text-align:center">End</td>
            <td style="padding: 0px 10px !important;text-align:center">Operator<br />Remarks</td>
            <td></td>
        </tr>
        <tr>
        <td>
            <asp:TextBox ID="txtmo" runat="server" Width="69px"></asp:TextBox>
            </td>
        <td>
            <asp:TextBox ID="txtprocess" runat="server" Width="69px"></asp:TextBox>
            </td>
        <td>
            <asp:TextBox ID="txtres" runat="server" Width="69px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtoperator" runat="server" Width="69px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtassistant" runat="server" Width="69px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txthelper" runat="server" Width="69px"></asp:TextBox>
            </td>
            <td>
        <asp:Label ID="lblfrom" runat="server"></asp:Label>
        <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" 
            ImageUrl="~/Styles/calendar_icon.png" Width="17px" 
            AlternateText="make sure that the search box is empty to select date range" />
            </td>
            <td>
        <asp:Label ID="lblto" runat="server"></asp:Label>
        <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" 
            ImageUrl="~/Styles/calendar_icon.png" Width="17px" 
            AlternateText="make sure that the search box is empty to select date range" />
            </td>
            <td>
                <asp:TextBox ID="txtoper" runat="server" Width="69px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="clearsearch" runat="server" Text="Clear" />
            </td>
        </tr>
        </table>
        
        <asp:Calendar ID="calfrom" runat="server" BackColor="White" BorderColor="White" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" 
            Height="119px" NextPrevFormat="FullMonth" Visible="False" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
                Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
        <asp:Calendar ID="calto" runat="server" BackColor="White" BorderColor="White" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" 
            Height="190px" NextPrevFormat="FullMonth" Visible="False" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
                Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
        <br />   
<asp:Button ID="Button2" runat="server" Text="Search" />
</div>
<asp:Table ID="Table1" runat="server" class="tbl_task">
        </asp:Table>
    <p></p>
</asp:Content>