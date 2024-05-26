<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOS_CDR.aspx.cs" Inherits="TEXT_frmOS_CDR" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">
    CDR From Date
    <asp:TextBox ID="txtFromDate" runat="server" Width="130px"></asp:TextBox>&nbsp;
    To Date
    <asp:TextBox ID="txtToDate" runat="server" Width="130px"></asp:TextBox>&nbsp; A-Party
    <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
    <asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsRequestList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT  S.CDR_TYPE, S.A_PARTY, S.B_PARTY, S.TIME_STAMP, S.DURATION, S.COST, S.TEXT, S.CDR_FILE FROM SERVICE_CDR_FO_PARTY S">
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataSourceID="sdsRequestList" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="CDR_TYPE" HeaderText="CDR Type" SortExpression="CDR_TYPE" />
                <asp:BoundField DataField="A_PARTY" HeaderText="A-Party" SortExpression="A_PARTY" />
                <asp:BoundField DataField="B_PARTY" HeaderText="B-Party" SortExpression="B_PARTY" />
                <asp:BoundField DataField="TIME_STAMP" HeaderText="Time Stamp" SortExpression="TIME_STAMP" />
                <asp:BoundField DataField="COST" HeaderText="Cost" SortExpression="COST" />
                <asp:BoundField DataField="TEXT" HeaderText="Text" SortExpression="TEXT" />
                <asp:BoundField DataField="CDR_FILE" HeaderText="CDR File" SortExpression="CDR_FILE" />
                
            </Columns>
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

