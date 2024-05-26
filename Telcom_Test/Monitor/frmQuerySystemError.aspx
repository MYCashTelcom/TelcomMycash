<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQuerySystemError.aspx.cs" Inherits="Forms_frmQuerySystemError" Title="System Alarm" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">System Alarm</SPAN>
</STRONG></DIV>
<DIV>
<asp:SqlDataSource id="sdsSystemAlarm" runat="server" DeleteCommand='DELETE FROM "ERROR_HISTORY" WHERE "ERROR_HISTORY_ID" = :ERROR_HISTORY_ID' InsertCommand='INSERT INTO "ERROR_HISTORY" ("ERROR_HISTORY_ID", "ERROR_ID", "ERROR_HISTORY_TIME", "ERROR_HISTORY_DESC", "ERROR_HISTORY_REMARK") VALUES (:ERROR_HISTORY_ID, :ERROR_ID, :ERROR_HISTORY_TIME, :ERROR_HISTORY_DESC, :ERROR_HISTORY_REMARK)' UpdateCommand='UPDATE "ERROR_HISTORY" SET "ERROR_ID" = :ERROR_ID, "ERROR_HISTORY_TIME" = :ERROR_HISTORY_TIME, "ERROR_HISTORY_DESC" = :ERROR_HISTORY_DESC, "ERROR_HISTORY_REMARK" = :ERROR_HISTORY_REMARK WHERE "ERROR_HISTORY_ID" = :ERROR_HISTORY_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ERROR_HISTORY_ID", "ERROR_ID", "ERROR_HISTORY_TIME", "ERROR_HISTORY_DESC", "ERROR_HISTORY_REMARK" FROM "ERROR_HISTORY"'><DeleteParameters>
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
<asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsSystemAlarm" 
        DataKeyNames="ERROR_HISTORY_ID" AutoGenerateColumns="False"  
        AllowPaging="True" PageSize="20"
CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        onrowdeleted="gdvSysUsrGroup_RowDeleted" 
        onrowupdated="gdvSysUsrGroup_RowUpdated">
<Columns>
<asp:BoundField DataField="ERROR_HISTORY_ID" HeaderText="ERROR_HISTORY_ID" ReadOnly="True" SortExpression="ERROR_HISTORY_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="ERROR_ID" HeaderText="Alarm Code" SortExpression="ERROR_ID">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ERROR_HISTORY_TIME" HeaderText="Alarm Time" SortExpression="ERROR_HISTORY_TIME">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ERROR_HISTORY_DESC" HeaderText="Alarm Description" SortExpression="ERROR_HISTORY_DESC">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Feedback &amp; Remarks" SortExpression="ERROR_HISTORY_REMARK"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("ERROR_HISTORY_REMARK") %>' TextMode="MultiLine" Width="353px" Height="88px"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("ERROR_HISTORY_REMARK") %>' ></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
</Columns>

</asp:GridView>&nbsp;&nbsp; </DIV>
</contenttemplate>
    </asp:UpdatePanel>
</form>
</body>
</html>
