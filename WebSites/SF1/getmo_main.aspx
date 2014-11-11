<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="getmo_main.aspx.vb" Inherits="getmo_main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .style3
        {
            height: 25px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="panelerr" runat="server">
    <asp:Label ID="lblerror" runat="server" style="margin-left: 26px;" 
            ForeColor="Black"></asp:Label>
            <asp:Button ID="cmdrecover" runat="server" Text="Recover" Visible="False" />
    <asp:Label ID="lblrecover" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="machine_name" runat="server"></asp:Label>
    <asp:Label ID="lblnomo" runat="server"></asp:Label>
    </asp:Panel>
    <br /><br />
<table style="width:100%;position: relative;top: -37px;">
        <tr><td style="width:50%" valign="top">
            <table class="operator">
            <tr>
            <td><asp:Label ID="lbloperatorname" runat="server" Text="Operator :"></asp:Label></td>
            <td><asp:TextBox ID="txtoper" runat="server"></asp:TextBox>
            <asp:TextBox ID="oper2" runat="server" Visible="False"></asp:TextBox>
            <asp:Button ID="changeop1" runat="server" Font-Size="Smaller" 
                    Text="Change Operator" Visible="False" style="height: 21px"/></td>
            </tr><tr>
            <td><asp:Label ID="lblassopername" runat="server" Text="Assistant Operator:"></asp:Label></td>
            <td><asp:TextBox ID="txtaoper" runat="server"></asp:TextBox>
            <asp:TextBox ID="aoper2" runat="server" Visible="False"></asp:TextBox>
            <asp:Button ID="cancelchangeop" runat="server" Font-Size="Smaller" Text="Cancel" 
                    Visible="False" Width="123px"/></td>
            </tr><tr>
            <td class="style5"><asp:Label ID="lblhelpername" runat="server" Text="Helper :"></asp:Label></td>
            <td class="style5"><asp:TextBox ID="txthelper" runat="server"></asp:TextBox>
            <asp:TextBox ID="helper2" runat="server" Visible="False"></asp:TextBox></td>
            </tr>
            <tr><td colspan=3>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="cmdgetMO" runat="server" Text="GET MO" style="height: 26px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnnomo" runat="server" Text="GET NON-RELATED JOB DOWNTIME" 
                    style="width: 194px;white-space: normal;" Visible="False" />
            </td></tr>
            </table>
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <div class="mo">
            <h2 class="h2ness">MO</h2>
            &nbsp;BARCODE: 
            <asp:TextBox ID="txtmo" runat="server" Width="216px" AutoPostBack="True" style="height: 22px"></asp:TextBox>
            <asp:Label ID="lbloutmo" runat="server" Visible="False">lbloutmo</asp:Label>
            <asp:Button ID="cmdcancel" runat="server" Text="Cancel" Visible="False" />
            <asp:Label ID="lblcountmo" runat="server" Visible="False"></asp:Label>
            <asp:Panel ID="Panel2" runat="server">
                <asp:Label ID="output" runat="server"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="paneltable" runat="server" Visible="False">
                <table border="1" class="sap_details">
                    <tr class="head">
                        <td>
                            FG CODE</td>
                        <td class="style3">
                            STATUS</td>
                        <td>
                            ROUTING</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="lblfgcode" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblomorcode" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblrouting" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="head">
                        <td colspan="3">
                            DESCRIPTION</td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lbldesc" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="head">
                        <td>
                            WAREHOUSE</td>
                        <td class="style3">
                            PLANNED QTY</td>
                        <td>
                            ACTUAL QTY</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblwarehouse" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="plnqty" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="actualqty" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="head">
                        <td>
                            REQ. DATE</td>
                        <td class="style3">
                            START DATE</td>
                        <td>
                            END DATE</td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblreqdate" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblplnstart" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lblplnend" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="lblperpiece" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblgoodsin" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                            <asp:Label ID="lblsubsplanqty" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblsubsqtyperpiece" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        </asp:Panel>
        </td><td style="width:50%" valign="top">

<asp:Panel ID="Panel3" runat="server" Visible="False">
    <asp:Label ID="titleremarks" runat="server"></asp:Label>
    <asp:Label ID="titletimetype" runat="server"></asp:Label>
    <table style="background-color:rgb(231, 231, 231);width:100%">
        <tr>
            <td colspan=3><asp:Button ID="cmddirect" runat="server" Text="Direct" />
            <asp:Button ID="cmdindirect" runat="server" Text="Indirect" Visible="False" />
            <asp:Button ID="cmdprodd" runat="server" Text="Prod. Downtime" Visible="False" /></td>
        </tr>
        <tr>
            <td colspan=3>
                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="lblremainingseconds" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="lblsecondsinchangeover" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table style="background-color:#E7E7E7">
        <tr>
            <td class="style6">
                <asp:Button ID="cmdstart" runat="server" Text="Start" 
                    OnClientClick="return confirm('Are you sure you want to start now ?')" 
                    style="height: 26px; margin-bottom: 0px;"/></td>
            <td><asp:Button ID="cmdcanc" runat="server" Text="Cancel" 
                    OnClientClick="return confirm('Are you sure you want to cancel ?')" 
                    Visible="False" /></td>
            <td><asp:Button ID="cmdstop" runat="server" Text="Stop" 
                    OnClientClick="return confirm('Are you sure you want to stop now ?')" 
                    Visible="False" />
                <asp:Button ID="cmdok" runat="server" Text="Submit" 
                    OnClientClick="return confirm('Are you sure you want to Okey now ?')" 
                    Enabled="False" Visible="False" />
            </td>
            <td>
                <asp:Button ID="cmdcontinue" runat="server" Text="Cancel" 
                    OnClientClick="return confirm('Are you sure you want to Continue now ?')" 
                    Enabled="False" Visible="False" /></td>
        </tr>
    </table>
    <asp:Label ID="lblerroringoods" runat="server"></asp:Label>
    <asp:Label ID="lblnewgood" runat="server" Visible="False"></asp:Label>
    <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblhms" runat="server">00:00:00</asp:Label>
                    <asp:Label ID="lbltimeNow" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbltimeCount" runat="server" ForeColor="#FF5050" Visible="False"></asp:Label>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <Triggers> 
                    <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000">
                    </asp:Timer>
                        <asp:Label ID="lbler" runat="server" ForeColor="Red"></asp:Label>
                        &nbsp;</Triggers>
                    <asp:Label ID="lbld" runat="server"></asp:Label>
                    <asp:Label ID="lblchangeover" runat="server"></asp:Label>
                </ContentTemplate>
                </asp:UpdatePanel>
        <table style="background-color:#E7E7E7;width:80%">
        <tr>
            <td valign="top">Time Start <br />
                <asp:Label ID="echosd" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:Label ID="lblstartTime" runat="server" Font-Bold="True" 
                    Font-Size="Large"></asp:Label>
            </td>
            <td  valign="top">Time End <br />
                <asp:Label ID="echoed" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:Label ID="lblendTime" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr><td colspan=2><b>Balance Quantity : 
            <asp:Label ID="lblbalance" runat="server" Text="0.00"></asp:Label></b>
         </tr><tr>
            <td><b>Total Good QTY: </b>
            <b><asp:Label ID="lblgoodqty" runat="server" Text="0.00"></asp:Label></b></td>
            <td><b>Total Bad QTY: </b>
            <b><asp:Label ID="lblbadqty" runat="server" Text="0.00"></asp:Label></b></td>
            </tr>
        <tr>
        <td>Good QTY<br />
            <asp:TextBox ID="txtg1" runat="server" Width="85px"></asp:TextBox>
            &nbsp;PCS</td>
        <td>Bad QTY<br />
            <asp:TextBox ID="txtr1" runat="server" Width="85px"></asp:TextBox>
            &nbsp;PCS</td>
        </tr>
        <tr><td valign=top>
            Remarks
            </td><td><asp:TextBox ID="lbloper_remarks" runat="server" TextMode="MultiLine"></asp:TextBox></td></tr>
        <tr>
                <td><asp:Label ID="lblheadcount" runat="server" Text="Head Count: " Visible="False"></asp:Label></td>
                <td><asp:TextBox ID="txtheadcount" runat="server" Visible="False" 
                        AutoPostBack="True">1</asp:TextBox>
                    <asp:Label ID="lblerrinhead" runat="server" ForeColor="#FF3300" Visible="False"></asp:Label>
                </td>
            </tr>
        </table><table>
        </tr>
        <tr>
            <td class="style4">
                <asp:Label ID="lblgood" runat="server" Text="Good" Visible="False"></asp:Label>
                &nbsp;
                <asp:Label ID="uommajor" runat="server" 
                    Visible="False"></asp:Label>
                <asp:TextBox ID="txtgoods" runat="server" ReadOnly="True" Visible="False" 
                    Width="40%"></asp:TextBox>
                <asp:TextBox ID="txtg" runat="server" Visible="False" Width="16px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="lblrejects1" runat="server" Text="Rejects" Visible="False"></asp:Label>
                <asp:Label ID="lbluom" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="txtrejects" runat="server" Width="40%" 
                    AutoPostBack="True" Visible="False">0</asp:TextBox>
            </td>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblgoodsprpieces" runat="server" 
                        style="border:1px solid #a3a3a3;padding:0px 3px;background-color:white" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="lblrejectsinpieces" runat="server" AutoPostBack="True" 
                        Visible="False" Width="79px">0</asp:TextBox>
                </td>
            </tr>
        </tr>
    </table>
    </asp:Panel>
    </td></tr>
    </table>

</asp:Content>

