<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCheckSession.aspx.cs" Inherits="Monitor_frmCheckSession" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
<asp:ScriptManager id="ScriptManager1" runat="server"> 
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue" class="style1"><b>Database Session <strong>
    <span style="COLOR: white">
        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" 
            Text="Refresh" />
    &nbsp;</span><asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
    </strong></b></DIV>
<DIV>
<asp:SqlDataSource id="sdsDataBaseSession" runat="server" 
        DeleteCommand='DELETE FROM "ERROR_HISTORY" WHERE "ERROR_HISTORY_ID" = :ERROR_HISTORY_ID' 
        InsertCommand='INSERT INTO "ERROR_HISTORY" ("ERROR_HISTORY_ID", "ERROR_ID", "ERROR_HISTORY_TIME", "ERROR_HISTORY_DESC", "ERROR_HISTORY_REMARK") VALUES (:ERROR_HISTORY_ID, :ERROR_ID, :ERROR_HISTORY_TIME, :ERROR_HISTORY_DESC, :ERROR_HISTORY_REMARK)' 
        UpdateCommand='UPDATE "ERROR_HISTORY" SET "ERROR_ID" = :ERROR_ID, "ERROR_HISTORY_TIME" = :ERROR_HISTORY_TIME, "ERROR_HISTORY_DESC" = :ERROR_HISTORY_DESC, "ERROR_HISTORY_REMARK" = :ERROR_HISTORY_REMARK WHERE "ERROR_HISTORY_ID" = :ERROR_HISTORY_ID' 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM V$SESSION WHERE SCHEMANAME IN ('APSNG101','M2B_WAP','M2B_USSD','M2B_SOAP') ORDER BY SCHEMANAME"><DeleteParameters>
<asp:Parameter Name="ERROR_HISTORY_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="ERROR_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_TIME" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_REMARK" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="ERROR_HISTORY_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_TIME" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="ERROR_HISTORY_REMARK" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> 
<asp:GridView id="gdvDatabaseSession" runat="server" 
        DataSourceID="sdsDataBaseSession" AutoGenerateColumns="False" 
         AllowPaging="True" PageSize="20"
CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<Columns>
    <asp:TemplateField>
         <ItemTemplate>
            <asp:Button ID="btnRemoveRow" runat="server" Text="Kill" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SID")%>' OnClick="btnRemoveRow_Click" />
         </ItemTemplate>
      </asp:TemplateField>
<asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID"></asp:BoundField>
<asp:BoundField DataField="SERIAL#" HeaderText="SERIAL#" SortExpression="SERIAL#">
</asp:BoundField>
<asp:BoundField DataField="AUDSID" HeaderText="AUDSID" SortExpression="AUDSID">
</asp:BoundField>
<asp:BoundField DataField="USER#" HeaderText="USER#" SortExpression="USER#">
</asp:BoundField>
    <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" 
        SortExpression="USERNAME" />
    <asp:BoundField DataField="COMMAND" HeaderText="COMMAND" 
        SortExpression="COMMAND" />
    <asp:BoundField DataField="OWNERID" HeaderText="OWNERID" 
        SortExpression="OWNERID" />
    <asp:BoundField DataField="TADDR" HeaderText="TADDR" SortExpression="TADDR" />
    <asp:BoundField DataField="LOCKWAIT" HeaderText="LOCKWAIT" 
        SortExpression="LOCKWAIT" />
    <asp:BoundField DataField="STATUS" HeaderText="STATUS" 
        SortExpression="STATUS" />
    <asp:BoundField DataField="SERVER" HeaderText="SERVER" 
        SortExpression="SERVER" />
    <asp:BoundField DataField="SCHEMA#" HeaderText="SCHEMA#" 
        SortExpression="SCHEMA#" />
    <asp:BoundField DataField="SCHEMANAME" HeaderText="SCHEMANAME" 
        SortExpression="SCHEMANAME" />
    <asp:BoundField DataField="OSUSER" HeaderText="OSUSER" 
        SortExpression="OSUSER" />
    <asp:BoundField DataField="PROCESS" HeaderText="PROCESS" 
        SortExpression="PROCESS" />
    <asp:BoundField DataField="MACHINE" HeaderText="MACHINE" 
        SortExpression="MACHINE" />
    <asp:BoundField DataField="PORT" HeaderText="PORT" SortExpression="PORT" />
    <asp:BoundField DataField="TERMINAL" HeaderText="TERMINAL" 
        SortExpression="TERMINAL" />
    <asp:BoundField DataField="PROGRAM" HeaderText="PROGRAM" 
        SortExpression="PROGRAM" />
    <asp:BoundField DataField="TYPE" HeaderText="TYPE" SortExpression="TYPE" />
    <asp:BoundField DataField="SQL_HASH_VALUE" HeaderText="SQL_HASH_VALUE" 
        SortExpression="SQL_HASH_VALUE" />
    <asp:BoundField DataField="SQL_ID" HeaderText="SQL_ID" 
        SortExpression="SQL_ID" />
    <asp:BoundField DataField="SQL_CHILD_NUMBER" HeaderText="SQL_CHILD_NUMBER" 
        SortExpression="SQL_CHILD_NUMBER" />
    <asp:BoundField DataField="SQL_EXEC_START" HeaderText="SQL_EXEC_START" 
        SortExpression="SQL_EXEC_START" />
    <asp:BoundField DataField="SQL_EXEC_ID" HeaderText="SQL_EXEC_ID" 
        SortExpression="SQL_EXEC_ID" />
    <asp:BoundField DataField="PREV_HASH_VALUE" HeaderText="PREV_HASH_VALUE" 
        SortExpression="PREV_HASH_VALUE" />
    <asp:BoundField DataField="PREV_SQL_ID" HeaderText="PREV_SQL_ID" 
        SortExpression="PREV_SQL_ID" />
    <asp:BoundField DataField="PREV_CHILD_NUMBER" HeaderText="PREV_CHILD_NUMBER" 
        SortExpression="PREV_CHILD_NUMBER" />
    <asp:BoundField DataField="PREV_EXEC_START" HeaderText="PREV_EXEC_START" 
        SortExpression="PREV_EXEC_START" />
    <asp:BoundField DataField="PREV_EXEC_ID" HeaderText="PREV_EXEC_ID" 
        SortExpression="PREV_EXEC_ID" />
    <asp:BoundField DataField="PLSQL_ENTRY_OBJECT_ID" 
        HeaderText="PLSQL_ENTRY_OBJECT_ID" SortExpression="PLSQL_ENTRY_OBJECT_ID" />
    <asp:BoundField DataField="PLSQL_ENTRY_SUBPROGRAM_ID" 
        HeaderText="PLSQL_ENTRY_SUBPROGRAM_ID" 
        SortExpression="PLSQL_ENTRY_SUBPROGRAM_ID" />
    <asp:BoundField DataField="PLSQL_OBJECT_ID" HeaderText="PLSQL_OBJECT_ID" 
        SortExpression="PLSQL_OBJECT_ID" />
    <asp:BoundField DataField="PLSQL_SUBPROGRAM_ID" 
        HeaderText="PLSQL_SUBPROGRAM_ID" SortExpression="PLSQL_SUBPROGRAM_ID" />
    <asp:BoundField DataField="MODULE" HeaderText="MODULE" 
        SortExpression="MODULE" />
    <asp:BoundField DataField="MODULE_HASH" HeaderText="MODULE_HASH" 
        SortExpression="MODULE_HASH" />
    <asp:BoundField DataField="ACTION" HeaderText="ACTION" 
        SortExpression="ACTION" />
    <asp:BoundField DataField="ACTION_HASH" HeaderText="ACTION_HASH" 
        SortExpression="ACTION_HASH" />
    <asp:BoundField DataField="CLIENT_INFO" HeaderText="CLIENT_INFO" 
        SortExpression="CLIENT_INFO" />
    <asp:BoundField DataField="FIXED_TABLE_SEQUENCE" 
        HeaderText="FIXED_TABLE_SEQUENCE" SortExpression="FIXED_TABLE_SEQUENCE" />
    <asp:BoundField DataField="ROW_WAIT_OBJ#" HeaderText="ROW_WAIT_OBJ#" 
        SortExpression="ROW_WAIT_OBJ#" />
    <asp:BoundField DataField="ROW_WAIT_FILE#" HeaderText="ROW_WAIT_FILE#" 
        SortExpression="ROW_WAIT_FILE#" />
    <asp:BoundField DataField="ROW_WAIT_BLOCK#" HeaderText="ROW_WAIT_BLOCK#" 
        SortExpression="ROW_WAIT_BLOCK#" />
    <asp:BoundField DataField="ROW_WAIT_ROW#" HeaderText="ROW_WAIT_ROW#" 
        SortExpression="ROW_WAIT_ROW#" />
    <asp:BoundField DataField="TOP_LEVEL_CALL#" HeaderText="TOP_LEVEL_CALL#" 
        SortExpression="TOP_LEVEL_CALL#" />
    <asp:BoundField DataField="LOGON_TIME" HeaderText="LOGON_TIME" 
        SortExpression="LOGON_TIME" />
    <asp:BoundField DataField="LAST_CALL_ET" HeaderText="LAST_CALL_ET" 
        SortExpression="LAST_CALL_ET" />
    <asp:BoundField DataField="PDML_ENABLED" HeaderText="PDML_ENABLED" 
        SortExpression="PDML_ENABLED" />
    <asp:BoundField DataField="FAILOVER_TYPE" HeaderText="FAILOVER_TYPE" 
        SortExpression="FAILOVER_TYPE" />
    <asp:BoundField DataField="FAILOVER_METHOD" HeaderText="FAILOVER_METHOD" 
        SortExpression="FAILOVER_METHOD" />
    <asp:BoundField DataField="FAILED_OVER" HeaderText="FAILED_OVER" 
        SortExpression="FAILED_OVER" />
    <asp:BoundField DataField="RESOURCE_CONSUMER_GROUP" 
        HeaderText="RESOURCE_CONSUMER_GROUP" SortExpression="RESOURCE_CONSUMER_GROUP" />
    <asp:BoundField DataField="PDML_STATUS" HeaderText="PDML_STATUS" 
        SortExpression="PDML_STATUS" />
    <asp:BoundField DataField="PDDL_STATUS" HeaderText="PDDL_STATUS" 
        SortExpression="PDDL_STATUS" />
    <asp:BoundField DataField="PQ_STATUS" HeaderText="PQ_STATUS" 
        SortExpression="PQ_STATUS" />
    <asp:BoundField DataField="CURRENT_QUEUE_DURATION" 
        HeaderText="CURRENT_QUEUE_DURATION" SortExpression="CURRENT_QUEUE_DURATION" />
    <asp:BoundField DataField="CLIENT_IDENTIFIER" HeaderText="CLIENT_IDENTIFIER" 
        SortExpression="CLIENT_IDENTIFIER" />
    <asp:BoundField DataField="BLOCKING_SESSION_STATUS" 
        HeaderText="BLOCKING_SESSION_STATUS" SortExpression="BLOCKING_SESSION_STATUS" />
    <asp:BoundField DataField="BLOCKING_INSTANCE" HeaderText="BLOCKING_INSTANCE" 
        SortExpression="BLOCKING_INSTANCE" />
    <asp:BoundField DataField="BLOCKING_SESSION" HeaderText="BLOCKING_SESSION" 
        SortExpression="BLOCKING_SESSION" />
    <asp:BoundField DataField="FINAL_BLOCKING_SESSION_STATUS" 
        HeaderText="FINAL_BLOCKING_SESSION_STATUS" 
        SortExpression="FINAL_BLOCKING_SESSION_STATUS" />
    <asp:BoundField DataField="FINAL_BLOCKING_INSTANCE" 
        HeaderText="FINAL_BLOCKING_INSTANCE" SortExpression="FINAL_BLOCKING_INSTANCE" />
    <asp:BoundField DataField="FINAL_BLOCKING_SESSION" 
        HeaderText="FINAL_BLOCKING_SESSION" SortExpression="FINAL_BLOCKING_SESSION" />
    <asp:BoundField DataField="SEQ#" HeaderText="SEQ#" SortExpression="SEQ#" />
    <asp:BoundField DataField="EVENT#" HeaderText="EVENT#" 
        SortExpression="EVENT#" />
    <asp:BoundField DataField="EVENT" HeaderText="EVENT" SortExpression="EVENT" />
    <asp:BoundField DataField="P1TEXT" HeaderText="P1TEXT" 
        SortExpression="P1TEXT" />
    <asp:BoundField DataField="P1" HeaderText="P1" SortExpression="P1" />
    <asp:BoundField DataField="P2TEXT" HeaderText="P2TEXT" 
        SortExpression="P2TEXT" />
    <asp:BoundField DataField="P2" HeaderText="P2" SortExpression="P2" />
    <asp:BoundField DataField="P3TEXT" HeaderText="P3TEXT" 
        SortExpression="P3TEXT" />
    <asp:BoundField DataField="P3" HeaderText="P3" SortExpression="P3" />
    <asp:BoundField DataField="WAIT_CLASS_ID" HeaderText="WAIT_CLASS_ID" 
        SortExpression="WAIT_CLASS_ID" />
    <asp:BoundField DataField="WAIT_CLASS#" HeaderText="WAIT_CLASS#" 
        SortExpression="WAIT_CLASS#" />
    <asp:BoundField DataField="WAIT_CLASS" HeaderText="WAIT_CLASS" 
        SortExpression="WAIT_CLASS" />
    <asp:BoundField DataField="WAIT_TIME" HeaderText="WAIT_TIME" 
        SortExpression="WAIT_TIME" />
    <asp:BoundField DataField="SECONDS_IN_WAIT" HeaderText="SECONDS_IN_WAIT" 
        SortExpression="SECONDS_IN_WAIT" />
    <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
    <asp:BoundField DataField="WAIT_TIME_MICRO" HeaderText="WAIT_TIME_MICRO" 
        SortExpression="WAIT_TIME_MICRO" />
    <asp:BoundField DataField="TIME_REMAINING_MICRO" 
        HeaderText="TIME_REMAINING_MICRO" SortExpression="TIME_REMAINING_MICRO" />
    <asp:BoundField DataField="TIME_SINCE_LAST_WAIT_MICRO" 
        HeaderText="TIME_SINCE_LAST_WAIT_MICRO" 
        SortExpression="TIME_SINCE_LAST_WAIT_MICRO" />
    <asp:BoundField DataField="SERVICE_NAME" HeaderText="SERVICE_NAME" 
        SortExpression="SERVICE_NAME" />
    <asp:BoundField DataField="SQL_TRACE" HeaderText="SQL_TRACE" 
        SortExpression="SQL_TRACE" />
    <asp:BoundField DataField="SQL_TRACE_WAITS" HeaderText="SQL_TRACE_WAITS" 
        SortExpression="SQL_TRACE_WAITS" />
    <asp:BoundField DataField="SQL_TRACE_BINDS" HeaderText="SQL_TRACE_BINDS" 
        SortExpression="SQL_TRACE_BINDS" />
    <asp:BoundField DataField="SQL_TRACE_PLAN_STATS" 
        HeaderText="SQL_TRACE_PLAN_STATS" SortExpression="SQL_TRACE_PLAN_STATS" />
    <asp:BoundField DataField="SESSION_EDITION_ID" HeaderText="SESSION_EDITION_ID" 
        SortExpression="SESSION_EDITION_ID" />
    <asp:BoundField DataField="CREATOR_SERIAL#" HeaderText="CREATOR_SERIAL#" 
        SortExpression="CREATOR_SERIAL#" />
    <asp:BoundField DataField="ECID" HeaderText="ECID" SortExpression="ECID" />
</Columns>

    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />

</asp:GridView>&nbsp;&nbsp; </DIV>
</contenttemplate>
    </asp:UpdatePanel>
</form>
</body>
</html>

