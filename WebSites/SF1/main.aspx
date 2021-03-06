﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="main.aspx.vb" Inherits="main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        .style2
        {
            height: 38px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="panelerr" runat="server">
    <asp:Label ID="lblerror" runat="server" style="margin-left: 26px;" 
            ForeColor="Black"></asp:Label>
            <asp:Button ID="cmdrecover" runat="server" Text="Recover" Visible="False" />
    <asp:Label ID="lblrecover" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblnomo" runat="server"></asp:Label>
        <asp:Label ID="machine_name" runat="server"></asp:Label>
    </asp:Panel>
    <br />
    <table style="width:100%">
        <tr><td style="width:50%" valign="top">
            <table class="operator">
            <tr>
            <td><asp:Label ID="lbloperatorname" runat="server" Text="Operator :"></asp:Label></td>
            <td><asp:TextBox ID="txtoper" runat="server"></asp:TextBox>
            <asp:TextBox ID="oper2" runat="server" Visible="False"></asp:TextBox>
            <asp:Button ID="changeop1" runat="server" Font-Size="Smaller" Text="Change Operator" Visible="False"/></td>
            </tr><tr>
            <td><asp:Label ID="lblassopername" runat="server" Text="Assistant Operator:"></asp:Label></td>
            <td><asp:TextBox ID="txtaoper" runat="server"></asp:TextBox>
            <asp:TextBox ID="aoper2" runat="server" Visible="False"></asp:TextBox></td>
            </tr><tr>
            <td><asp:Label ID="lblhelpername" runat="server" Text="Helper :"></asp:Label></td>
            <td><asp:TextBox ID="txthelper" runat="server"></asp:TextBox>
            <asp:TextBox ID="helper2" runat="server" Visible="False"></asp:TextBox></td>
            </tr>
            <tr><td colspan=3>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="cmdgetMO" runat="server" Text="GET MO" style="height: 26px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnnomo" runat="server" Text="GET NON-RELATED JOB DOWNTIME" style="width: 194px;white-space: normal;" />
            </td></tr>
            </table>
            <br />
    <asp:Panel ID="Panel1" runat="server">
        <div class="mo">
            <h2 class="h2ness">MO</h2>
            &nbsp;BARCODE: 
            <asp:TextBox ID="txtmo" runat="server" Width="216px" AutoPostBack="True" style="height: 22px"></asp:TextBox><asp:Label ID="lbloutmo" runat="server"></asp:Label>
            <asp:Label ID="lblcountmo" runat="server"></asp:Label>
            <asp:Panel ID="Panel2" runat="server">
            <asp:Label ID="output" runat="server"></asp:Label>
            <br />
            <asp:Button ID="cmdcancel" runat="server" Text="Cancel" />
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
                <asp:Label ID="lblrem2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="lblop" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblddltype" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <table style="background-color:#E7E7E7">
        <tr>
            <td class="style2"><asp:Button ID="cmdstart" runat="server" Text="Start" OnClientClick="return confirm('Are you sure you want to start now ?')" /></td>
            <td class="style2"><asp:Button ID="cmdcanc" runat="server" Text="Cancel" OnClientClick="return confirm('Are you sure you want to cancel ?')" /></td>
            <td class="style2"><asp:Button ID="cmdstop" runat="server" Text="Stop" OnClientClick="return confirm('Are you sure you want to stop now ?')" /></td>
        </tr>
    </table>
    <br />
    <table style="background-color:#E7E7E7;width:80%">
        <tr>
            <td>Date</td>
            <td>
                <asp:Label ID="lbldatenow" runat="server"></asp:Label>
                <asp:Label ID="stdt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Time:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblhms" runat="server"></asp:Label>
                    <asp:Label ID="lbltimeNow" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbltimeCount" runat="server" ForeColor="#FF5050" Visible="False"></asp:Label>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <Triggers> 
                    <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000">
                    </asp:Timer>
                        <asp:Label ID="lbler" runat="server" ForeColor="Red"></asp:Label>
                        &nbsp;</Triggers>
                    <asp:Label ID="lbld" runat="server">asdf</asp:Label>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
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
                <asp:Label ID="lblstoreendtime" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlmm" runat="server" Height="20px" Width="56px" Visible="False">
                <asp:ListItem>Jan</asp:ListItem>
                <asp:ListItem>Feb</asp:ListItem>
                <asp:ListItem>Mar</asp:ListItem>
                <asp:ListItem>Apr</asp:ListItem>
                <asp:ListItem>May</asp:ListItem>
                <asp:ListItem>Jun</asp:ListItem>
                <asp:ListItem>Jul</asp:ListItem>
                <asp:ListItem>Aug</asp:ListItem>
                <asp:ListItem>Sep</asp:ListItem>
                <asp:ListItem>Oct</asp:ListItem>
                <asp:ListItem>Nov</asp:ListItem>
                <asp:ListItem>Dec</asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="txtdd" runat="server" Width="24px" Visible="False" placeholder="dd"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtyy" runat="server" Width="41px" Visible="False" placeholder="yyyy"></asp:TextBox>
                <asp:TextBox ID="txth" runat="server" Width="18px" placeholder="h" MaxLength="2" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtm" runat="server" placeholder="m" Width="18px" MaxLength="2" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txts" runat="server" placeholder="s" Width="18px" MaxLength="2" Visible="False"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Update Time" Visible="False" />
                <asp:Label ID="lble" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table style="background-color:#E7E7E7;width:80%">
            <tr>
                <td valign="top">Remarks :</td>
                <td><asp:TextBox ID="lbloper_remarks" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblgood" runat="server" Text="Good :" Visible="False"></asp:Label></td>
                <td>
                <asp:TextBox ID="txtgoods" runat="server" Width="86px" ReadOnly="True" 
                        Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtg" runat="server" Visible="False" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblrejects1" runat="server" Text="Rejects:" Visible="False"></asp:Label></td>
                <td>
                <asp:TextBox ID="txtrejects" runat="server" Width="86px" AutoPostBack="True" 
                        Visible="False"></asp:TextBox>
                <asp:Label ID="lbluom" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top"><asp:Label ID="lbladdsubs" runat="server" 
                        Text="Additional Substrate:" Visible="False"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtaddsubs" Width="86px" runat="server" AutoPostBack="True" MaxLength="10" ReadOnly="True">
                    </asp:TextBox>
                    <asp:Button ID="btnaddsubs" runat="server" Font-Size="Smaller" 
                        Text="Search Additional Substrate" Visible="False" />
                    <br />
                    <asp:Label ID="lblgD" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblerinadd" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    <asp:Label ID="lbloutputaddsubs" runat="server" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblsubsid" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblsubserr" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblheadcount" runat="server" Text="Head Count: " Visible="False"></asp:Label></td>
                <td><asp:TextBox ID="txtheadcount" runat="server" Width="50px" Visible="False" 
                        AutoPostBack="True">1</asp:TextBox>
                    <asp:Label ID="lblerrinhead" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    </td></tr>
    </table>
    <br />
    <asp:Panel ID="paneltable" runat="server" style="width:100%">
        <table class="tbl_task" style="width:100%">
        <tr>
            <td>Process and Remarks</td>
            <td>Mo No.</td>
            <td>Job Description</td>
            <td>Operator</td>
            <td>Assistant <br /> Operator</td>
            <td>Helper</td>
            <td>Start</td>
            <td>End</td>
            <td>Good</td>
            <td>Rejects</td>
            <td style="border:1px solid #3498db">Remarks</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblprorem" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblmo" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblfgdesc" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbloperator" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblassoper" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblhelper" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblstart" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblend" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblgoods" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblrejects" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblremarks" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <br />
    <br />
&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;<br />
    </asp:Content>

