<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCheckBacProcess.aspx.cs" Inherits="frmCheckBacProcess" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">
    &nbsp;Backgroud Process: <strong><span style="COLOR: white">&nbsp;<asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" 
            Text="Refresh" />
        </span></strong></SPAN></STRONG></DIV><DIV>
        <asp:SqlDataSource ID="sdsBackgroundProcess" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
                SelectCommand="SELECT SC.SCHEDULE_ID,SC.SCHEDULE_DIS_NAME,JOB_NAME, SCHEDULE_TYPE, STATE, START_DATE, REPEAT_INTERVAL, LAST_START_DATE, RUN_COUNT, FAILURE_COUNT, 
LAST_RUN_DURATION FROM SYS.DBA_SCHEDULER_JOBS,SERVICE_BG_SCHEDULES SC WHERE SC.SCHEDULE_NAME=JOB_NAME(+)">
        </asp:SqlDataSource>
        <asp:GridView ID="grvBackGroundProcess" runat="server"  AutoGenerateColumns="False" DataSourceID="sdsBackgroundProcess"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="SCHEDULE_DIS_NAME" HeaderText="SCHEDULE_DIS_NAME" 
                    SortExpression="SCHEDULE_DIS_NAME">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="JOB_NAME" HeaderText="JOB_NAME" 
                    SortExpression="JOB_NAME">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SCHEDULE_TYPE" HeaderText="SCHEDULE_TYPE" 
                    SortExpression="SCHEDULE_TYPE">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" 
                    SortExpression="START_DATE">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="REPEAT_INTERVAL" HeaderText="REPEAT_INTERVAL" 
                    SortExpression="REPEAT_INTERVAL">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LAST_START_DATE" HeaderText="LAST_START_DATE" 
                    SortExpression="LAST_START_DATE">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RUN_COUNT" HeaderText="RUN_COUNT" 
                    SortExpression="RUN_COUNT">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FAILURE_COUNT" HeaderText="FAILURE_COUNT" 
                    SortExpression="FAILURE_COUNT" />
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
