<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCommissionDisburse.aspx.cs" Inherits="TEXT_frmCommissionDisburse" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="SCManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UDPanel" runat="server">
        <contenttemplate>
        <DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">&nbsp;Report List&nbsp; &nbsp;<asp:SqlDataSource 
                ID="sdsDisburseList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM SERVICE_REWARD_DISBURSE'>
            </asp:SqlDataSource>
        </SPAN></STRONG></DIV><DIV>
            <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"
                BorderColor="Silver" DataKeyNames="SRV_REWARD_DISB_ID" DataSourceID="sdsDisburseList" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="SRV_REWARD_DISB_ID" HeaderText="Disburse ID" 
                        ReadOnly="True" SortExpression="SRV_REWARD_DISB_ID" />
                    <asp:BoundField DataField="REWARD_DISB_TITLE" HeaderText="Disbursement Note" 
                        SortExpression="REWARD_DISB_TITLE" />
                    <asp:BoundField DataField="REWARD_DISB_FDATE" HeaderText="From Date" 
                        SortExpression="REWARD_DISB_FDATE" />
                    <asp:BoundField DataField="REWARD_DISB_TDATE" HeaderText="To Date" 
                        SortExpression="REWARD_DISB_TDATE" />
                    <asp:CommandField SelectText="Print" ShowSelectButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            &nbsp;<BR /><BR />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
