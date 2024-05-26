<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngGroupAccount.aspx.cs" Inherits="Forms_Default" Title="Manage Group" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Manage Group of</SPAN></STRONG>&nbsp;<asp:DropDownList
        ID="ddlAccountList" runat="server" DataSourceID="sdsAccountList" DataTextField="ACCNT_NO"
        DataValueField="ACCNT_ID" AutoPostBack="True">
    </asp:DropDownList></DIV>
    <DIV><asp:SqlDataSource id="sdsGroupList" runat="server" DeleteCommand='DELETE FROM "ACCOUNT_GROUP_LIST" WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "ACCOUNT_GROUP_LIST" ("ACCNT_GROUP_ID", "ACCNT_GROUP_NAME", "ACCNT_ID", "ACCNT_GROUP_CODE") VALUES (:ACCNT_GROUP_ID, :ACCNT_GROUP_NAME, :ACCNT_ID, :ACCNT_GROUP_CODE)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "ACCOUNT_GROUP_LIST" SET "ACCNT_GROUP_NAME" = :ACCNT_GROUP_NAME, "ACCNT_CREATION_DATE" = :ACCNT_CREATION_DATE, "ACCNT_ID" = :ACCNT_ID, "ACCNT_GROUP_CODE" = :ACCNT_GROUP_CODE WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' 
    SelectCommand='SELECT * FROM "ACCOUNT_GROUP_LIST" WHERE "ACCNT_ID"=:ACCNT_ID'><DeleteParameters>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<SelectParameters>
    <asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="ACCNT_ID" ControlID="ddlAccountList"></asp:ControlParameter>
</SelectParameters>
<UpdateParameters>
<asp:Parameter Name="ACCNT_GROUP_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_CREATION_DATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="ACCNT_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_CODE" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_CODE" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> 
<asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:GridView id="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BorderColor="White" DataKeyNames="ACCNT_GROUP_ID" DataSourceID="sdsGroupList"
CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            onrowdeleted="GridView1_RowDeleted" onrowupdated="GridView1_RowUpdated"><Columns>
<asp:BoundField DataField="ACCNT_GROUP_ID" HeaderText="ACCNT_GROUP_ID" ReadOnly="True" SortExpression="ACCNT_GROUP_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="ACCNT_GROUP_NAME" HeaderText="Group Name" SortExpression="ACCNT_GROUP_NAME"></asp:BoundField>
<asp:BoundField DataField="ACCNT_GROUP_CODE" HeaderText="Group Code" SortExpression="ACCNT_GROUP_CODE"></asp:BoundField>
<asp:TemplateField HeaderText="Account" SortExpression="ACCNT_ID"><EditItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" DataSourceID="sdsAccountList" SelectedValue='<%# Bind("ACCNT_ID") %>' DataTextField="ACCNT_NO" DataValueField="ACCNT_ID"></asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsAccountList" SelectedValue='<%# Bind("ACCNT_ID") %>' DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" Enabled="False"></asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ACCNT_CREATION_DATE" HeaderText="Creation Date" SortExpression="ACCNT_CREATION_DATE"></asp:BoundField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
</Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Group&nbsp;</SPAN></STRONG></DIV><DIV>
        <asp:DetailsView id="dlvServiceType" runat="server" BorderColor="Silver" 
            DataKeyNames="ACCNT_GROUP_ID" DataSourceID="sdsGroupList" Height="50px" 
            Width="125px" AutoGenerateRows="False" DefaultMode="Insert" 
            Font-Names="Times New Roman" Font-Size="11pt" 
            oniteminserted="dlvServiceType_ItemInserted"><Fields>
<asp:BoundField DataField="ACCNT_GROUP_ID" HeaderText="ACCNT_GROUP_ID" ReadOnly="True" SortExpression="ACCNT_GROUP_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="ACCNT_GROUP_NAME" HeaderText="Group Name" SortExpression="ACCNT_GROUP_NAME">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACCNT_GROUP_CODE" HeaderText="Group Code" SortExpression="ACCNT_GROUP_CODE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Account" SortExpression="ACCNT_ID"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" DataSourceID="sdsAccountList" SelectedValue='<%# Bind("ACCNT_ID") %>' DataTextField="ACCNT_NO" DataValueField="ACCNT_ID"></asp:DropDownList> 
</InsertItemTemplate>
<ItemTemplate>
&nbsp;
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField InsertText="Add Group" ShowInsertButton="True" ButtonType="Button"></asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>