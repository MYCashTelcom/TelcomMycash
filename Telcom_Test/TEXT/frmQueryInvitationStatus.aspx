<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQueryInvitationStatus.aspx.cs" Inherits="Forms_frmQueryInvitationStatus" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Query Invitation Status: &nbsp;Account
    <asp:DropDownList ID="ddlAccountList" runat="server" DataSourceID="sdsAccountList"
        DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" OnSelectedIndexChanged="ddlAccountList_SelectedIndexChanged">
    </asp:DropDownList>&nbsp; From Date
    <asp:TextBox ID="txtFromDate" runat="server" Width="120px"></asp:TextBox>&nbsp;
    To Date
    <asp:TextBox ID="txtToDate" runat="server" Width="120px"></asp:TextBox>&nbsp;<asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsRequestList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT INVS.REQUEST_ID, INVS.RECEIPENT_PARTY, INVS.REQUEST_TIME, DECODE(INVS.REQ_STATE, 'P', 'In Que', 'Processwed') AS REQ_STATE, INVS.REQ_TEXT, INVS.SERVICE_COST ,INVS.RSP_STATE ,  INVR.REQUEST_TIME AS RESPONSE_TIME, DECODE(UPPER(INVR.ACCESS_CODE), 'Y', 'Accepted', 'N', 'Declained', 'Pending') AS INV_STATE FROM ALL_INVITATION_STATUS INVS, ALL_INVITATION_RESPONSE INVR WHERE INVS.RECEIPENT_PARTY = INVR.REQUEST_PARTY (+) AND (SUBSTR(INVS.REQUEST_ID, 7, 6) = INVR.REQ_NO (+))">
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" Font-Size="11pt">
            <Columns>
                <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Send To" SortExpression="RECEIPENT_PARTY" />
                <asp:BoundField DataField="REQUEST_TIME" HeaderText="Submission Time" SortExpression="REQUEST_TIME" />
                <asp:BoundField DataField="REQ_STATE" HeaderText="Submission State" SortExpression="REQ_STATE" />
                <asp:BoundField DataField="REQ_TEXT" HeaderText="Invitation Message" SortExpression="REQ_TEXT" />
                <asp:BoundField DataField="SERVICE_COST" HeaderText="Service Cost" SortExpression="SERVICE_COST" />
                <asp:BoundField DataField="RSP_STATE" HeaderText="Invitation State" SortExpression="RSP_STATE" />
                <asp:BoundField DataField="RESPONSE_TIME" HeaderText="Invitation Sent  Time" SortExpression="RESPONSE_TIME" />
                <asp:BoundField DataField="INV_STATE" HeaderText="Invitation Response" SortExpression="INV_STATE" />
            </Columns>
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
