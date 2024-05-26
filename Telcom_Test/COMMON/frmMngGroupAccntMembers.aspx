<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngGroupAccntMembers.aspx.cs" Inherits="Forms_frmMngGroupAcntMembers" Title="Manage Group Member" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Manage Group Member of&nbsp;<asp:DropDownList ID="ddlGroupList" runat="server" DataSourceID="sdsGroupList" DataTextField="ACCNT_GROUP_NAME"
        DataValueField="ACCNT_GROUP_ID" AutoPostBack="True">
    </asp:DropDownList></SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsGroupList" runat="server" DeleteCommand='DELETE FROM "ACCOUNT_GROUP_LIST" WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "ACCOUNT_GROUP_LIST" ("ACCNT_GROUP_ID", "ACCNT_GROUP_NAME", "ACCNT_ID", "ACCNT_GROUP_CODE") VALUES (:ACCNT_GROUP_ID, :ACCNT_GROUP_NAME, :ACCNT_ID, :ACCNT_GROUP_CODE)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "ACCOUNT_GROUP_LIST" SET "ACCNT_GROUP_NAME" = :ACCNT_GROUP_NAME, "ACCNT_CREATION_DATE" = :ACCNT_CREATION_DATE, "ACCNT_ID" = :ACCNT_ID, "ACCNT_GROUP_CODE" = :ACCNT_GROUP_CODE WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' 
    SelectCommand='SELECT * FROM "ACCOUNT_GROUP_LIST"'><DeleteParameters>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
</DeleteParameters>
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
    <asp:SqlDataSource ID="sdsGroupMember" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "ACCOUNT_GROUP_MEMBERS" WHERE "ACCNT_GROUP_MEM_ID" = :ACCNT_GROUP_MEM_ID'
        InsertCommand='INSERT INTO "ACCOUNT_GROUP_MEMBERS" ("ACCNT_GROUP_ID", "ACCNT_ID", "ACCNT_MEM_NAME", "ACCNT_MEM_MSISDN") VALUES (:ACCNT_GROUP_ID, :ACCNT_ID, :ACCNT_MEM_NAME, :ACCNT_MEM_MSISDN)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand='SELECT "ACCNT_GROUP_MEM_ID", "ACCNT_GROUP_ID", "ACCNT_ID", "ACCNT_MEM_NAME", "ACCNT_MEM_MSISDN" FROM "ACCOUNT_GROUP_MEMBERS" WHERE "ACCNT_GROUP_ID"=:ACCNT_GROUP_ID'
        UpdateCommand='UPDATE "ACCOUNT_GROUP_MEMBERS" SET "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID, "ACCNT_ID" = :ACCNT_ID, "ACCNT_MEM_NAME" = :ACCNT_MEM_NAME, "ACCNT_MEM_MSISDN" = :ACCNT_MEM_MSISDN WHERE "ACCNT_GROUP_MEM_ID" = :ACCNT_GROUP_MEM_ID'>
        <DeleteParameters>
            <asp:Parameter Name="ACCNT_GROUP_MEM_ID" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="ACCNT_GROUP_ID" ControlID="ddlGroupList"></asp:ControlParameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ACCNT_GROUP_ID" Type="String" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_MEM_NAME" Type="String" />
            <asp:Parameter Name="ACCNT_MEM_MSISDN" Type="String" />
            <asp:Parameter Name="ACCNT_GROUP_MEM_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ACCNT_GROUP_ID" Type="String" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_MEM_NAME" Type="String" />
            <asp:Parameter Name="ACCNT_MEM_MSISDN" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
    <asp:GridView id="GridView1" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" BorderColor="White" 
                DataKeyNames="ACCNT_GROUP_MEM_ID" DataSourceID="sdsGroupMember"
    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                onrowdeleted="GridView1_RowDeleted" onrowupdated="GridView1_RowUpdated"><Columns>
    <asp:BoundField DataField="ACCNT_GROUP_MEM_ID" HeaderText="ACCNT_GROUP_MEM_ID" ReadOnly="True"
        SortExpression="ACCNT_GROUP_MEM_ID" Visible="False" />
    <asp:TemplateField HeaderText="Group" SortExpression="ACCNT_GROUP_ID">
        <EditItemTemplate>
            <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsGroupList" DataTextField="ACCNT_GROUP_NAME"
                DataValueField="ACCNT_GROUP_ID" SelectedValue='<%# Bind("ACCNT_GROUP_ID") %>'>
            </asp:DropDownList>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsGroupList" DataTextField="ACCNT_GROUP_NAME"
                DataValueField="ACCNT_GROUP_ID" Enabled="False" SelectedValue='<%# Bind("ACCNT_GROUP_ID") %>'>
            </asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="ACCNT_MEM_NAME" HeaderText="Membe Name" SortExpression="ACCNT_MEM_NAME" />
    <asp:BoundField DataField="ACCNT_MEM_MSISDN" HeaderText="Mobile Number" SortExpression="ACCNT_MEM_MSISDN" />
    <asp:TemplateField HeaderText="Account " SortExpression="ACCNT_ID">
        <EditItemTemplate>
            <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsAccountList"
                DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" SelectedValue='<%# Bind("ACCNT_ID") %>'>
            </asp:DropDownList>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsAccountList"
                DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" Enabled="False" SelectedValue='<%# Bind("ACCNT_ID") %>'>
            </asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Member&nbsp;</SPAN></STRONG></DIV><DIV>
        <asp:DetailsView id="dlvServiceType" runat="server" BorderColor="Silver" 
            DataKeyNames="ACCNT_GROUP_MEM_ID" DataSourceID="sdsGroupMember" Height="50px" 
            Width="125px" AutoGenerateRows="False" DefaultMode="Insert" 
            Font-Names="Times New Roman" Font-Size="11pt" 
            oniteminserted="dlvServiceType_ItemInserted"><Fields>
    <asp:TemplateField HeaderText="Group" SortExpression="ACCNT_GROUP_ID">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_GROUP_ID") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsGroupList" DataTextField="ACCNT_GROUP_NAME"
                DataValueField="ACCNT_GROUP_ID" SelectedValue='<%# Bind("ACCNT_GROUP_ID") %>'>
            </asp:DropDownList>
        </InsertItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACCNT_GROUP_ID") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="ACCNT_MEM_NAME" HeaderText="Memeber Name" SortExpression="ACCNT_MEM_NAME">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="ACCNT_MEM_MSISDN" HeaderText="Mobile Number" SortExpression="ACCNT_MEM_MSISDN">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="Account" SortExpression="ACCNT_ID">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="sdsAccountList"
                DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" SelectedValue='<%# Bind("ACCNT_ID") %>'>
            </asp:DropDownList>
        </InsertItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:CommandField ButtonType="Button" InsertText=" Insert " ShowInsertButton="True">
        <FooterStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>