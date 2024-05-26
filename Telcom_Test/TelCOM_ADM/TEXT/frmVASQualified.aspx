<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVASQualified.aspx.cs" Inherits="TEXT_frmReconciliation" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MVAS Reports</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 344px;
        }
        .style2
        {
            width: 60px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG>&nbsp;<SPAN style="COLOR: white">&nbsp;VAS Qualified and Disqualified Report&nbsp; &nbsp;
    </STRONG></DIV>
    <DIV><SPAN style="COLOR: Black">
        <table>           
             
             <tr>
             <td class="style2"></td><td class="style1">
                 <asp:RadioButtonList ID="rdbreportchoose" runat="server" AutoPostBack="True" 
                     RepeatDirection="Horizontal">
                     <asp:ListItem Selected="True" Value="QR">Qualified Report</asp:ListItem>
                     <asp:ListItem Value="DR">DisQualified Report</asp:ListItem>
                     <asp:ListItem Value="WR">Waiting Report</asp:ListItem>
                 </asp:RadioButtonList>
                 </td>
             
             </tr>
             <tr>
                <td align="right" class="style2">From Date&nbsp;</td>
               <%-- <td><asp:TextBox ID="txtFromDate" runat="server" Width="218px"></asp:TextBox></td>--%>
                <td class="style1"><span style="COLOR: white"><span style="COLOR: Black">
                    <cc1:GMDatePicker ID="txtFromDate" runat="server" 
                        DateFormat="dd-MMM-yyyy HH:mm:ss" MinDate="1980-01-04" TextBoxWidth="150">
                    </cc1:GMDatePicker>
                    </span></span>
                 </td>
                
            </tr>
             <tr>
                <td align="right" class="style2">To Date&nbsp;</td>
                <%--<td><asp:TextBox ID="txtToDate" runat="server" Width="130px"></asp:TextBox></td>--%>
                <td class="style1"><cc1:GMDatePicker ID="txtToDate" runat="server" TextBoxWidth="150" DateFormat="dd-MMM-yyyy HH:mm:ss" MinDate="1980-01-04">
             </cc1:GMDatePicker>
                 </td>
            </tr>
             
             <tr>
                <td class="style2"></td>
                <td align="left" class="style1">
                    <asp:Button ID="btnPrint" runat="server" Text="Preview" 
        onclick="btnPrint_Click" Width="97px" />
                </td>                
            </tr>
            
            
            
            <tr>
                <td align="right" class="style2">&nbsp;</td>
                <td class="style1"><asp:TextBox ID="txtRequestParty" runat="server" Width="129px" Visible="False"></asp:TextBox></td>
            </tr>
             <tr>
                <td align="right" class="style2">
                    &nbsp;</td>
                <td class="style1"><asp:TextBox ID="txtServiceCode" runat="server" Width="96px" Visible="False"></asp:TextBox></td>
            </tr>
            
            
            
            
        </table></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
