<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSysUsrGroup.aspx.cs" Inherits="Forms_frmSysUsrGroup" Title="System User Group" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">System User Group</SPAN></STRONG> </DIV><DIV><asp:SqlDataSource id="sdsSysUsrGroup" runat="server" SelectCommand='SELECT "SYS_USR_GROUP_ID", "SYS_USR_GROUP_TITLE", "SYS_USR_GROUP_PARENT" FROM "SYS_USER_GROUP"' UpdateCommand='UPDATE "SYS_USER_GROUP" SET "SYS_USR_GROUP_TITLE" = :SYS_USR_GROUP_TITLE, "SYS_USR_GROUP_PARENT" = :SYS_USR_GROUP_PARENT WHERE "SYS_USR_GROUP_ID" = :SYS_USR_GROUP_ID' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "SYS_USER_GROUP" ("SYS_USR_GROUP_TITLE", "SYS_USR_GROUP_PARENT") VALUES (:SYS_USR_GROUP_TITLE, :SYS_USR_GROUP_PARENT)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "SYS_USER_GROUP" WHERE "SYS_USR_GROUP_ID" = :SYS_USR_GROUP_ID'><DeleteParameters>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_PARENT"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_ID"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USR_GROUP_PARENT"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource>
<asp:GridView id="gdvSysUsrGroup" runat="server" BorderColor="Silver"  AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="SYS_USR_GROUP_ID" DataSourceID="sdsSysUsrGroup" __designer:wfdid="w3"><Columns>
<asp:BoundField ReadOnly="True" DataField="SYS_USR_GROUP_ID" Visible="False" SortExpression="SYS_USR_GROUP_ID" HeaderText="SYS_USR_GROUP_ID"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_GROUP_TITLE" SortExpression="SYS_USR_GROUP_TITLE" HeaderText="Group Name">
<HeaderStyle Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SYS_USR_GROUP_PARENT" HeaderText="Parent"><EditItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" __designer:wfdid="w17" DataSourceID="sdsSysUsrGroup" DataTextField="SYS_USR_GROUP_TITLE" DataValueField="SYS_USR_GROUP_ID" SelectedValue='<%# Bind("SYS_USR_GROUP_PARENT") %>'></asp:DropDownList> 
</EditItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" __designer:wfdid="w16" DataSourceID="sdsSysUsrGroup" DataTextField="SYS_USR_GROUP_TITLE" DataValueField="SYS_USR_GROUP_ID" SelectedValue='<%# Bind("SYS_USR_GROUP_PARENT") %>' Enabled="False"><asp:ListItem Selected="True">No Parent</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ButtonType="Button" ShowEditButton="True"></asp:CommandField>
</Columns>
</asp:GridView></DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New Group </SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" BorderColor="Silver" DataKeyNames="SYS_USR_GROUP_ID" DataSourceID="sdsSysUsrGroup" __designer:wfdid="w4" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
<asp:BoundField ReadOnly="True" DataField="SYS_USR_GROUP_ID" Visible="False" SortExpression="SYS_USR_GROUP_ID" HeaderText="SYS_USR_GROUP_ID"></asp:BoundField>
<asp:BoundField DataField="SYS_USR_GROUP_TITLE" SortExpression="SYS_USR_GROUP_TITLE" HeaderText="Gorup Name">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SYS_USR_GROUP_PARENT" HeaderText="Parent"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w7" Text='<%# Bind("SYS_USR_GROUP_PARENT") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" __designer:wfdid="w9" DataSourceID="sdsSysUsrGroup" DataTextField="SYS_USR_GROUP_TITLE" DataValueField="SYS_USR_GROUP_ID" SelectedValue='<%# Bind("SYS_USR_GROUP_PARENT") %>'></asp:DropDownList>
</InsertItemTemplate>

<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label1" runat="server" __designer:wfdid="w6" Text='<%# Bind("SYS_USR_GROUP_PARENT") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField InsertText=" Insert " ButtonType="Button" ShowInsertButton="True">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
    </form>
</body>
</html>
