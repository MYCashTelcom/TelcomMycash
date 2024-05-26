<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServicePerformance.aspx.cs" Inherits="TEXT_frmServicePerformance" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue">
    </STRONG>&nbsp; </DIV><DIV>
        <DIV>
        <table>           
             <tr>
                <td align="right">From Date&nbsp;</td>
                <td><asp:TextBox ID="txtFromDate" runat="server" Width="130px"></asp:TextBox></td>
            </tr>
             <tr>
                <td align="right">To Date&nbsp;</td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="130px"></asp:TextBox></td>
            </tr>            
             <tr>
                <td align="right">
                    Service Keyword&nbsp;</td>
                <td><asp:TextBox ID="txtServiceCode" runat="server" Width="96px"></asp:TextBox>&nbsp;(i.e 
                    G for Goon Goon) </td>
            </tr>
            
             <tr>
                <td></td>
                <td align="left">
                    <asp:Button ID="btnPrint" runat="server" Text="Preview" 
        onclick="btnPrint_Click" Width="97px" />
                </td>                
            </tr>
        </table></DIV></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
