<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="manualmo.aspx.vb" Inherits="manualmo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 30px;
        }
        .style3
        {
            width: 82px;
        }
        .style7
        {
            width: 138px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblerror" runat="server"></asp:Label>
    <asp:Panel ID="panelmo" runat="server"  style="padding-left: 30px;">
    BARCODE : 
        <asp:TextBox ID="txtmo" runat="server" Width="246px" AutoPostBack="True"></asp:TextBox>
        <asp:Button ID="cmdcancel" runat="server" Text="Cancel" Visible="False" />
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel1" runat="server" Visible="False" style="padding-left: 35px;">
            <table class="sap_details" border=1>
                <tr class='head'>
                    <td>FG CODE</td>
                    <td>STATUS</td>
                    <td>ROUTING</td>
                    <td>WAREHOUSE</td>
                    <td>PLANNED QTY</td>
                    <td>PLANNED QTY</td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lblfgcode" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblomorcode" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblrouting" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblwarehouse" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="plnqty" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="actualqty" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class='head'>
                    <td  colspan=6>
                        DESCRIPTION</td>
                    </td>
                </tr>
                <tr>
                    <td colspan=6>
                        <asp:Label ID="lbldesc" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class='head'>
                    <td>
                        REQ. DATE</td>
                    <td>
                        START DATE</td>
                    <td>
                        END DATE</td>
                    <td></td>
                    <td class="style7"></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblreqdate" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblplnstart" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblplnend" runat="server" Text=""></asp:Label>
                    </td>
                    <td>PC PER
                        </td>
                    <td>GOOD Converted
                        </td>
                   <td></td>
                </tr>
            </table>
            <br />sdf<asp:Label ID="lblgoodsin" runat="server" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblsubsplanqty" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblsubsqtyperpiece" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblperpiece" runat="server" Font-Bold="True"></asp:Label>
            <br />
            
            Operator:
            <asp:TextBox ID="txtoper" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Ass.Operator:
            <asp:TextBox ID="txtaoper" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; Helper:
            <asp:TextBox ID="txthelper" runat="server"></asp:TextBox>
            &nbsp; HeadCount:
            <asp:TextBox ID="txtheadcount" runat="server">0</asp:TextBox>
            <br />
        <table width="100%" border=1>
            <tr  class='head'>
                <td>PostingDate</td>
                <td>Document Date</td>
                <td>SBU</td>
                <td>Operation</td>
                <td>Resource</td>
                <td>Process</td>
                <td>Start Date</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblpostingdate" runat="server" width="80px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbldocdate" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlsbu" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddloper" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtresource" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                    <asp:Label ID="lblerrinres" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlprocess" runat="server" width="50%" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtstartmm" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtstartd" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                    <asp:TextBox ID="txtstarty" runat="server" Width="50%" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr  class='head'>
                <td>Start Time</td>
                <td>End Date</td>
                <td>End Time</td>
                <td>No of Resources</td>
                <td>Remarks</td>
                <td>Quantity</td>
                <td class="style3">RejectQty</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtstarth" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtstartm" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtstarts" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtendmm" runat="server" Width="25%" AutoPostBack="True" 
                        Height="22px" MaxLength="2"></asp:TextBox>
                    <asp:TextBox ID="txtendd" runat="server" Width="25%" AutoPostBack="True" 
                        MaxLength="2" style="height: 22px"></asp:TextBox>
                    <asp:TextBox ID="txtendy" runat="server" Width="50%" AutoPostBack="True" 
                        MaxLength="4"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtendh" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtendm" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtends" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblnoresource" runat="server" Width="80%">1</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblremarks" runat="server" Width="100%"></asp:Label>
                    <asp:Label ID="lblrem" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtg" runat="server" Visible="False" Width="16px" 
                        Height="22px"></asp:TextBox>
                    <asp:TextBox ID="txtgoods" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtrejects" runat="server" Width="104%"></asp:TextBox>
                    <asp:Label ID="lbluom" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
            <asp:Label ID="lblerrorindate" runat="server" ForeColor="#FF3300"></asp:Label>
            <br />
        <br /><asp:Button ID="cmdsubmit" runat="server" Text="Submit" />
    </asp:Panel>
</asp:Content>

