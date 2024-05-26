<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBGScheduleMaintain.aspx.cs" Inherits="Monitor_frmBGScheduleMaintain" %>

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
    &nbsp;Backgroud Process: <strong><span style="COLOR: white">&nbsp;<strong><span style="COLOR: white">&nbsp;</span></strong><asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" 
            Text="Refresh" />
        &nbsp;
        </span>
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#CC0000" 
        Text="Label"></asp:Label>
    </strong></SPAN></STRONG></DIV><DIV>
        <asp:SqlDataSource ID="sdsBackgroundProcess" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
                SelectCommand="SELECT * FROM &quot;SERVICE_BG_SCHEDULES&quot;" 
                DeleteCommand="DELETE FROM &quot;SERVICE_BG_SCHEDULES&quot; WHERE &quot;SCHEDULE_ID&quot; = :SCHEDULE_ID" 
                InsertCommand="INSERT INTO &quot;SERVICE_BG_SCHEDULES&quot; (&quot;SCHEDULE_ID&quot;, &quot;SCHEDULE_DIS_NAME&quot;, &quot;SCHEDULE_NAME&quot;, &quot;SCHEDULE_START_SCRIPT&quot;, &quot;SCHEDULE_STOP_SCRIPT&quot;, &quot;SCHEDULE_STATUS&quot;, &quot;SCHEDULE_INTERVAL&quot;, &quot;SCHEDULE_REMARKS&quot;) VALUES (:SCHEDULE_ID, :SCHEDULE_DIS_NAME, :SCHEDULE_NAME, :SCHEDULE_START_SCRIPT, :SCHEDULE_STOP_SCRIPT, :SCHEDULE_STATUS, :SCHEDULE_INTERVAL, :SCHEDULE_REMARKS)" 
                UpdateCommand="UPDATE &quot;SERVICE_BG_SCHEDULES&quot; SET &quot;SCHEDULE_DIS_NAME&quot; = :SCHEDULE_DIS_NAME, &quot;SCHEDULE_NAME&quot; = :SCHEDULE_NAME, &quot;SCHEDULE_START_SCRIPT&quot; = :SCHEDULE_START_SCRIPT, &quot;SCHEDULE_STOP_SCRIPT&quot; = :SCHEDULE_STOP_SCRIPT, &quot;SCHEDULE_STATUS&quot; = :SCHEDULE_STATUS, &quot;SCHEDULE_INTERVAL&quot; = :SCHEDULE_INTERVAL, &quot;SCHEDULE_REMARKS&quot; = :SCHEDULE_REMARKS WHERE &quot;SCHEDULE_ID&quot; = :SCHEDULE_ID">
            <DeleteParameters>
                <asp:Parameter Name="SCHEDULE_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="SCHEDULE_DIS_NAME" Type="String" />
                <asp:Parameter Name="SCHEDULE_NAME" Type="String" />
                <asp:Parameter Name="SCHEDULE_START_SCRIPT" Type="String" />
                <asp:Parameter Name="SCHEDULE_STOP_SCRIPT" Type="String" />
                <asp:Parameter Name="SCHEDULE_STATUS" Type="String" />
                <asp:Parameter Name="SCHEDULE_INTERVAL" Type="String" />
                <asp:Parameter Name="SCHEDULE_REMARKS" Type="String" />
                <asp:Parameter Name="SCHEDULE_ID" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="SCHEDULE_ID" Type="String" />
                <asp:Parameter Name="SCHEDULE_DIS_NAME" Type="String" />
                <asp:Parameter Name="SCHEDULE_NAME" Type="String" />
                <asp:Parameter Name="SCHEDULE_START_SCRIPT" Type="String" />
                <asp:Parameter Name="SCHEDULE_STOP_SCRIPT" Type="String" />
                <asp:Parameter Name="SCHEDULE_STATUS" Type="String" />
                <asp:Parameter Name="SCHEDULE_INTERVAL" Type="String" />
                <asp:Parameter Name="SCHEDULE_REMARKS" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="grvBackGroundProcess" runat="server"  
                AutoGenerateColumns="False" DataSourceID="sdsBackgroundProcess"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                DataKeyNames="SCHEDULE_ID">
            <Columns>
                <asp:TemplateField>
                     <ItemTemplate>
                        <asp:Button ID="btnStart" runat="server" Text="Start" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SCHEDULE_START_SCRIPT")%>' OnClick="btnStartProcess_Click" />
                     </ItemTemplate>
                  </asp:TemplateField>
                <asp:TemplateField>
                     <ItemTemplate>
                        <asp:Button ID="btnStop" runat="server" Text="Stop" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SCHEDULE_STOP_SCRIPT")%>' OnClick="btnStopProcess_Click" />
                     </ItemTemplate>
                  </asp:TemplateField>
                <asp:BoundField DataField="SCHEDULE_DIS_NAME" HeaderText="Schedule Display Name" 
                    SortExpression="SCHEDULE_DIS_NAME">
                </asp:BoundField>
                <asp:BoundField DataField="SCHEDULE_NAME" HeaderText="Schedule Name" 
                    SortExpression="SCHEDULE_NAME">
                </asp:BoundField>
                <asp:BoundField DataField="SCHEDULE_STATUS" HeaderText="Status" 
                    SortExpression="SCHEDULE_STATUS">
                </asp:BoundField>
                <asp:BoundField DataField="SCHEDULE_REMARKS" HeaderText="Remarks" 
                    SortExpression="SCHEDULE_REMARKS">
                </asp:BoundField>
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

