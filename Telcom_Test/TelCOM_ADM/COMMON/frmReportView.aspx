<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReportView.aspx.cs" Inherits="frmReportView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" /> 
    <link href="../aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer4/css/default.css"
        rel="stylesheet" type="text/css" />   
</head>
<body style="background-color:White" >
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
        
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <Triggers>
          <asp:PostBackTrigger ControlID="CrystalReportViewer1" />
          <asp:PostBackTrigger ControlID="imbPrint" /> 
          <asp:PostBackTrigger ControlID="imbExport" />
     </Triggers>
    <contenttemplate>
<table><tr>
    <td style="background-color: menu" align="left">
        <asp:Button ID="btnGoBack" runat="server" Text=" << " OnClick="btnGoBack_Click" Visible="False" /></td>
    <td style="background-color: menu" align="right">
    &nbsp;<asp:ImageButton ID="imbPrint" runat="server" ImageUrl="~/Icons/printer_32.gif"
        OnClick="imbPrint_Click" Height="25px" Width="30px" Visible="False" />&nbsp;
    <asp:ImageButton ID="imbExport" runat="server" ImageUrl="~/Icons/Reports.gif" OnClick="imbExport_Click" Height="25px" Width="23px" Visible="False" /></td></tr>
    <tr><td colspan="2">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
        EnableDatabaseLogonPrompt="False" HasRefreshButton="True" Height="1032px" ReportSourceID="CrystalReportSource1"
        oninit="CrystalReportViewer1_Init" GroupTreeStyle-BackColor="White" HasCrystalLogo="False" AutoDataBind="True" PrintMode="ActiveX" OnNavigate="CrystalReportViewer1_Navigate" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="Forms\crptClientList.rpt">
        </Report>
    </CR:CrystalReportSource>
    </td></tr>
    </table>
</contenttemplate>
 <Triggers>
          <asp:PostBackTrigger ControlID="CrystalReportViewer1" /> 
    </Triggers>
    </asp:UpdatePanel>
        &nbsp;&nbsp;
    </form>
</body>
</html>
