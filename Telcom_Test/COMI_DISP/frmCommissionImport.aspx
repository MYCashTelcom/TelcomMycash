<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCommissionImport.aspx.cs" Inherits="TEXT_frmCommissionImport" %>


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
    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date"></asp:Label>
    <asp:TextBox ID="txtProcessDate" runat="server"></asp:TextBox>
    <STRONG><SPAN style="COLOR: white">
    <asp:Button
        ID="btnRefresh" runat="server" Text="View Commission" 
            onclick="btnRefresh_Click" /></SPAN>
    </STRONG></DIV><DIV>
        <asp:GridView ID="grvCommission" runat="server" AllowSorting="True" 
                CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt">
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
