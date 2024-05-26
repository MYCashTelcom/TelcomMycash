<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQueryBankSatlementTran.aspx.cs" Inherits="Forms_frmQueryBankSatlementTran" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Query Settlement Transaction&nbsp;<asp:DropDownList ID="ddlAccountList" runat="server" DataSourceID="sdsAccountList"
        DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" OnSelectedIndexChanged="ddlAccountList_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>&nbsp; From Date
    <asp:TextBox ID="txtFromDate" runat="server" Width="120px"></asp:TextBox>&nbsp;
    To Date
    <asp:TextBox ID="txtToDate" runat="server" Width="120px"></asp:TextBox>&nbsp;<asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsBankTrans" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT BANK_TRAN_ID, BANK_TRAN_DESC, BANK_TRAN_DTL_ID, BANK_ACCOUNT_NO, AC_NAME, CLINT_BANK_ACC_NO, BANK_INTERNAL_CODE, BANK_TRAN_DATE, DEBIT, CREDIT, IS_SATLEMENT_TRAN FROM ALL_BANK_TRANSACTION">
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsBankTrans" Font-Size="11pt">
            <Columns>
                <asp:BoundField DataField="BANK_TRAN_DESC" HeaderText="Description" SortExpression="BANK_TRAN_DESC" />
                <asp:BoundField DataField="BANK_ACCOUNT_NO" HeaderText="Account No" SortExpression="BANK_ACCOUNT_NO" />
                <asp:BoundField DataField="AC_NAME" HeaderText="Account Name" SortExpression="AC_NAME" />
                <asp:BoundField DataField="CLINT_BANK_ACC_NO" HeaderText="ACC No" SortExpression="CLINT_BANK_ACC_NO" />
                <asp:BoundField DataField="BANK_INTERNAL_CODE" HeaderText="Bank Code" SortExpression="BANK_INTERNAL_CODE" />
                <asp:BoundField DataField="BANK_TRAN_DATE" HeaderText="Date" SortExpression="BANK_TRAN_DATE" />
                <asp:BoundField DataField="DEBIT" HeaderText="Debit" SortExpression="DEBIT" />
                <asp:BoundField DataField="CREDIT" HeaderText="Credit" SortExpression="CREDIT" />
            </Columns>
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
