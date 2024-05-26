<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQueryMSG.aspx.cs" Inherits="Forms_frmQueryMSG" Title="NITL VAS Portal" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Query Message&nbsp;</SPAN></STRONG> </DIV><DIV><asp:SqlDataSource id="sdsSysUsrGroup" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT SYS_USR_GROUP_ID, SYS_USR_GROUP_TITLE, SYS_USR_GROUP_PARENT FROM SYS_USER_GROUP WHERE (SYS_USR_GROUP_ID <> SYS_USR_GROUP_PARENT)"></asp:SqlDataSource> <asp:SqlDataSource id="sdsSysUsers" runat="server" DeleteCommand='DELETE FROM "SYS_USER_LIST" WHERE "SYS_USR_ID" = :SYS_USR_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "SYS_USER_LIST" ("SYS_USR_GROUP_ID", "SYS_USR_ID", "SYS_USR_LOGIN", "SYS_USR_PASS", "SYS_USR_STATUS", "SYS_USR_CURRENT_STATE") VALUES (:SYS_USR_GROUP_ID, :SYS_USR_ID, :SYS_USR_LOGIN, :SYS_USR_PASS, :SYS_USR_STATUS, :SYS_USR_CURRENT_STATE)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "SYS_USER_LIST" SET "SYS_USR_GROUP_ID" = :SYS_USR_GROUP_ID, "SYS_USR_LOGIN" = :SYS_USR_LOGIN, "SYS_USR_PASS" = :SYS_USR_PASS, "SYS_USR_STATUS" = :SYS_USR_STATUS, "SYS_USR_CURRENT_STATE" = :SYS_USR_CURRENT_STATE WHERE "SYS_USR_ID" = :SYS_USR_ID' SelectCommand='SELECT "SYS_USR_GROUP_ID", "SYS_USR_ID", "SYS_USR_LOGIN", "SYS_USR_PASS", "SYS_USR_STATUS", "SYS_USR_CURRENT_STATE" FROM "SYS_USER_LIST"'><DeleteParameters>
<asp:Parameter Type="String" Name="SYS_USR_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_LOGIN"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_PASS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_STATUS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_CURRENT_STATE"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_ID"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_LOGIN"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_PASS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_STATUS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_CURRENT_STATE"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource>
 <asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsSysUsers" DataKeyNames="SYS_USR_ID" AutoGenerateColumns="False" AllowPaging="True"  BorderColor="Silver"><Columns>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
<asp:BoundField DataField="SYS_USR_GROUP_ID" SortExpression="SYS_USR_GROUP_ID" HeaderText="SYS_USR_GROUP_ID"></asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="SYS_USR_ID" SortExpression="SYS_USR_ID" HeaderText="SYS_USR_ID"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_LOGIN" SortExpression="SYS_USR_LOGIN" HeaderText="SYS_USR_LOGIN"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_PASS" SortExpression="SYS_USR_PASS" HeaderText="SYS_USR_PASS"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_STATUS" SortExpression="SYS_USR_STATUS" HeaderText="SYS_USR_STATUS"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_CURRENT_STATE" SortExpression="SYS_USR_CURRENT_STATE" HeaderText="SYS_USR_CURRENT_STATE"></asp:BoundField>
</Columns>
</asp:GridView>
</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;User </SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsSysUsers" DataKeyNames="SYS_USR_ID" BorderColor="Silver" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert"><Fields>
<asp:BoundField DataField="SYS_USR_GROUP_ID" SortExpression="SYS_USR_GROUP_ID" HeaderText="SYS_USR_GROUP_ID"></asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="SYS_USR_ID" SortExpression="SYS_USR_ID" HeaderText="SYS_USR_ID"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_LOGIN" SortExpression="SYS_USR_LOGIN" HeaderText="SYS_USR_LOGIN"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_PASS" SortExpression="SYS_USR_PASS" HeaderText="SYS_USR_PASS"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_STATUS" SortExpression="SYS_USR_STATUS" HeaderText="SYS_USR_STATUS"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_CURRENT_STATE" SortExpression="SYS_USR_CURRENT_STATE" HeaderText="SYS_USR_CURRENT_STATE"></asp:BoundField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

