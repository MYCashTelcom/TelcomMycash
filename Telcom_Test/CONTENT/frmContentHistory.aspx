<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContentHistory.aspx.cs" Inherits="Forms_frmContentHistory" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Content Download History&nbsp; From Date
    <asp:TextBox ID="txtFromDate" runat="server" Width="120px"></asp:TextBox>&nbsp;
    To Date
    <asp:TextBox ID="txtToDate" runat="server" Width="120px"></asp:TextBox>&nbsp;<asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></SPAN></STRONG></DIV><DIV>
        <asp:SqlDataSource ID="sdsContHistory" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT CG.CONTENT_GRP_NAME, CL.CONTENT_TITLE, CUH.CONTENT_USER_MSISDN, CUH.CONTENT_USED_TIME, CUH.CONTENT_USER_HANDSET, CUH.CONTENT_PRICE_DEDUCTED, CUH.CONTENT_BILLING_INFO FROM CONTENT_USED_HISTORY CUH, CONTENT_LIST CL, CONTENT_GROUP CG WHERE CUH.CONTENT_ID = CL.CONTENT_ID AND CL.CONTENT_GRP_ID = CG.CONTENT_GRP_ID">
        </asp:SqlDataSource>
        <asp:GridView ID="grvContentDLHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsContHistory" Font-Size="11pt">
            <Columns>
                <asp:BoundField DataField="CONTENT_GRP_NAME" HeaderText="Content Group" SortExpression="CONTENT_GRP_NAME" />
                <asp:BoundField DataField="CONTENT_TITLE" HeaderText="Content" SortExpression="CONTENT_TITLE" />
                <asp:BoundField DataField="CONTENT_USER_MSISDN" HeaderText="User MSISDN" SortExpression="CONTENT_USER_MSISDN" />
                <asp:BoundField DataField="CONTENT_USED_TIME" HeaderText="Download Time" SortExpression="CONTENT_USED_TIME" />
                <asp:BoundField DataField="CONTENT_USER_HANDSET" HeaderText="Handset" SortExpression="CONTENT_USER_HANDSET" />
                <asp:BoundField DataField="CONTENT_PRICE_DEDUCTED" HeaderText="Price" SortExpression="CONTENT_PRICE_DEDUCTED" />
                <asp:BoundField DataField="CONTENT_BILLING_INFO" HeaderText="Billing Detail" SortExpression="CONTENT_BILLING_INFO" />
            </Columns>
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
