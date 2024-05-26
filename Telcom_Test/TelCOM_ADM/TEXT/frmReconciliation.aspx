<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReconciliation.aspx.cs" Inherits="TEXT_frmReconciliation" %>

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
    Service Keyword&nbsp;
    <asp:TextBox ID="txtServiceCode" runat="server" Width="56px"></asp:TextBox>
    &nbsp;&nbsp;From Date
    <asp:TextBox ID="txtFromDate" runat="server" Width="130px"></asp:TextBox>&nbsp;
    To Date
    <asp:TextBox ID="txtToDate" runat="server" Width="130px"></asp:TextBox>&nbsp; Request 
    Party
    <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
    <asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN>
    <asp:Button ID="btnPrint" runat="server" Text="Print Preview" 
        onclick="btnPrint_Click" />
    </STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsRequestList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT ROWNUM,SR.REQUEST_ID,SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME, DECODE(SR.REQUEST_STAE, 'P', 'In Que', 'Processed') AS REQ_STATE, SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1, LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1) AS REQ_TEXT, SR.SERVICE_REWARD, DECODE(RSP.RESPONSE_STAE, 'P', 'In Que', NULL, 'Waiting', 'Replied') AS RSP_STATE FROM SERVICE_REQUEST SR LEFT OUTER JOIN SERVICE_RESPONSE RSP ON SR.REQUEST_ID = RSP.REQUEST_ID">
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" 
                    SortExpression="REQUEST_ID" ReadOnly="True" />
                <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Request Party" 
                    SortExpression="REQUEST_PARTY" />
                <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Receipent Party" 
                    SortExpression="RECEIPENT_PARTY" />
                <asp:BoundField DataField="REQUEST_TIME" HeaderText="Request Time" 
                    SortExpression="REQUEST_TIME" />
                <asp:BoundField DataField="REQ_STATE" HeaderText="Request State" 
                    SortExpression="REQ_STATE" />
                <asp:BoundField DataField="REQ_TEXT" HeaderText="Message Content" 
                    SortExpression="REQ_TEXT" />
                <asp:BoundField DataField="SERVICE_REWARD" HeaderText="Commission" 
                    SortExpression="SERVICE_REWARD" />
                
                <asp:BoundField DataField="RSP_STATE" HeaderText="Activation State" 
                    SortExpression="RSP_STATE" />
                
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
