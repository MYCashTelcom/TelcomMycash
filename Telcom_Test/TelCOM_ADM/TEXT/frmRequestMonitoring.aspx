<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRequestMonitoring.aspx.cs" Inherits="TEXT_frmRequestMonitoring" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Query Requist Status</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <%--<meta http-equiv="Refresh" content="6" />--%>
    <style type="text/css">
        .Font_Color {
            color: White;
        }

        .GridViewClass {
            width: 100%;
            background-color: #fff;
            margin: 1px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            text-align: left;
        }

            .GridViewClass td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                font-size: 11px;
            }

            .GridViewClass th {
                padding: 4px 2px;
                color: #fff;
                background: url(grd_head1.png) activecaption repeat-x 50% top;
                border-left: solid 0px #525252;
                font-size: 11px;
            }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="true" Interval="3"></asp:Timer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="background-color: royalblue">
                    <strong>
                        <span class="Font_Color">Service Keyword</span>
                        <asp:TextBox ID="txtServiceCode" runat="server" Width="56px"></asp:TextBox>
                        <span class="Font_Color">(i.e. FT For Fund Transfer)&nbsp;From Date</span>
                        <cc1:GMDatePicker ID="dptFromDate" runat="server" CalendarTheme="Silver"
                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative"
                            TextBoxWidth="130">
                            <CalendarTitleStyle />
                        </cc1:GMDatePicker>
                        <span class="Font_Color">To Date</span>
                        <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver"
                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative"
                            TextBoxWidth="130">
                            <CalendarTitleStyle BackColor="#FFFFC0" Font-Size="X-Small" />
                        </cc1:GMDatePicker>
                        <span class="Font_Color">Mobile Number</span>
                        <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
                        <asp:Button
                            ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                    </strong>
                </div>
                <div>
                    <asp:SqlDataSource ID="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource>
                    <asp:SqlDataSource ID="sdsRequestList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                        SelectCommand=" SELECT ROWNUM,SR.REQUEST_ID,SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME,
 DECODE(SR.REQUEST_STAE, 'P', 'In Que', 'Processed') AS REQ_STATE,DECODE(INSTR(SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1,
 LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1),'PIN'),0,SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1, 
 LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1),SUBSTR(SR.REQUEST_TEXT,8,30)||'...') AS REQ_TEXT, SR.SERVICE_COST, RSP.RESPONSE_TIME,RSP.RESPONSE_ID ,
 DECODE(RSP.RESPONSE_STAE, 'P', 'In Que', NULL, 'Waitting', 'Replied') AS RSP_STATE,
 DECODE(INSTR (RSP.RESPONSE_MESSAGE,'token'),0,DECODE(INSTR (RSP.RESPONSE_MESSAGE,'PIN'),0,RESPONSE_MESSAGE, SUBSTR(RESPONSE_MESSAGE,0,30)||'...'),
 SUBSTR(RESPONSE_MESSAGE,0,71)||'...'||SUBSTR(RESPONSE_MESSAGE,79,152))RESPONSE_MESSAGE 
 FROM SERVICE_REQUEST SR LEFT OUTER JOIN SERVICE_RESPONSE RSP ON SR.REQUEST_ID = RSP.REQUEST_ID">

                        <%--SelectCommand="SELECT ROWNUM,SR.REQUEST_ID,SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME, DECODE(SR.REQUEST_STAE, 'P', 'In Que', 'Processed') AS REQ_STATE, SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1, LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1) AS REQ_TEXT, SR.SERVICE_COST, RSP.RESPONSE_TIME,RSP.RESPONSE_ID ,DECODE(RSP.RESPONSE_STAE, 'P', 'In Que', NULL, 'Waitting', 'Replied') AS RSP_STATE,RSP.RESPONSE_MESSAGE FROM SERVICE_REQUEST SR LEFT OUTER JOIN SERVICE_RESPONSE RSP ON SR.REQUEST_ID = RSP.REQUEST_ID">--%>
                    </asp:SqlDataSource>
                    <asp:GridView ID="grvRequestList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" CssClass="GridViewClass" Width="100%"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White" BorderStyle="None"
                        OnRowDataBound="grvRequestList_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" ReadOnly="True"
                                SortExpression="REQUEST_ID" />
                            <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Request Party" SortExpression="REQUEST_PARTY" />
                            <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Receipent Party" SortExpression="RECEIPENT_PARTY" />
                            <asp:BoundField DataField="REQUEST_TIME" HeaderText="Request Time" SortExpression="REQUEST_TIME" />
                            <asp:BoundField DataField="REQ_STATE" HeaderText="Request State" SortExpression="REQ_STATE" />
                            <asp:BoundField DataField="REQ_TEXT" HeaderText="Request Message"
                                SortExpression="REQ_TEXT" />
                            <asp:BoundField DataField="RESPONSE_ID" HeaderText="Reponse ID"
                                SortExpression="RESPONSE_ID" />
                            <asp:BoundField DataField="RSP_STATE" HeaderText="Response State" SortExpression="RSP_STATE" />
                            <asp:BoundField DataField="RESPONSE_TIME" HeaderText="Response Time" SortExpression="RESPONSE_TIME" />
                            <asp:BoundField DataField="RESPONSE_MESSAGE" HeaderText="Response Message" SortExpression="RESPONSE_MESSAGE"></asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
                    &nbsp;<br />
                    &nbsp; &nbsp;
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
