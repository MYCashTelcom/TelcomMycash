<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContentList.aspx.cs" Inherits="Forms_frmContentList" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Content List&nbsp; of Group
    <asp:DropDownList ID="ddlContGroup" runat="server" DataSourceID="sdsContentGroup"
        DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource id="sdsContentList" runat="server" 
SelectCommand='SELECT "CONTENT_GRP_ID", "CONTENT_ID", "CONTENT_TITLE", "CONTENT_PAGE_URL", "CONTENT_PRICE", "CONTENT_STATE", "CONTENT_CREATION_DATE", "CONTENT_EXPIRY_DATE" FROM "CONTENT_LIST" WHERE ("CONTENT_GRP_ID" = :CONTENT_GRP_ID) ORDER BY "CONTENT_ID"' 
UpdateCommand='UPDATE "CONTENT_LIST" SET "CONTENT_GRP_ID" = :CONTENT_GRP_ID, "CONTENT_TITLE" = :CONTENT_TITLE, "CONTENT_PAGE_URL" = :CONTENT_PAGE_URL, "CONTENT_PRICE" = :CONTENT_PRICE, "CONTENT_STATE" = :CONTENT_STATE, "CONTENT_EXPIRY_DATE" = :CONTENT_EXPIRY_DATE WHERE "CONTENT_ID" = :CONTENT_ID' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
InsertCommand='INSERT INTO "CONTENT_LIST" ("CONTENT_GRP_ID", "CONTENT_ID", "CONTENT_TITLE", "CONTENT_PAGE_URL", "CONTENT_PRICE", "CONTENT_STATE", "CONTENT_EXPIRY_DATE") VALUES (:CONTENT_GRP_ID, :CONTENT_ID, :CONTENT_TITLE, :CONTENT_PAGE_URL, :CONTENT_PRICE, :CONTENT_STATE, :CONTENT_EXPIRY_DATE)' 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
DeleteCommand='DELETE FROM "CONTENT_LIST" WHERE "CONTENT_ID" = :CONTENT_ID'>
<DeleteParameters>
    <asp:Parameter Name="CONTENT_ID" Type="String" />
</DeleteParameters>
<UpdateParameters>
    <asp:Parameter Name="CONTENT_GRP_ID" Type="String" />
    <asp:Parameter Name="CONTENT_TITLE" Type="String" />
    <asp:Parameter Name="CONTENT_PAGE_URL" Type="String" />
    <asp:Parameter Name="CONTENT_PRICE" Type="String" />
    <asp:Parameter Name="CONTENT_STATE" Type="String" />    
    <asp:Parameter Name="CONTENT_EXPIRY_DATE" Type="DateTime" />
    <asp:Parameter Name="CONTENT_ID" Type="String" />
</UpdateParameters>
<InsertParameters>    
    <asp:Parameter Name="CONTENT_GRP_ID" Type="String" />
    <asp:Parameter Name="CONTENT_ID" Type="String" />
    <asp:Parameter Name="CONTENT_TITLE" Type="String" />
    <asp:Parameter Name="CONTENT_PAGE_URL" Type="String" />
    <asp:Parameter Name="CONTENT_PRICE" Type="String" />
    <asp:Parameter Name="CONTENT_STATE" Type="String" />
    <asp:Parameter Name="CONTENT_EXPIRY_DATE" Type="DateTime" />
</InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlContGroup" Name="CONTENT_GRP_ID" PropertyName="SelectedValue"
            Type="String" />
    </SelectParameters>
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsContentGroup" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CONTENT_GRP_ID", "CONTENT_GRP_NAME" FROM "CONTENT_GROUP"'>
    </asp:SqlDataSource>
    </SPAN></STRONG></DIV>
<DIV>
    <asp:GridView id="GridView1" runat="server" DataSourceID="sdsContentList" DataKeyNames="CONTENT_ID" BorderColor="Silver" AutoGenerateColumns="False" AllowSorting="True"><Columns>
        <asp:TemplateField HeaderText="Content Group" SortExpression="CONTENT_GRP_ID">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsContentGroup"
                    DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" SelectedValue='<%# Bind("CONTENT_GRP_ID") %>'>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsContentGroup"
                    DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" SelectedValue='<%# Bind("CONTENT_GRP_ID") %>' Enabled="False">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_ID" HeaderText="CONTENT_ID" ReadOnly="True" SortExpression="CONTENT_ID" Visible="False" />
        <asp:BoundField DataField="CONTENT_TITLE" HeaderText="Content Title" SortExpression="CONTENT_TITLE" />
        <asp:BoundField DataField="CONTENT_PAGE_URL" HeaderText="Page URL" SortExpression="CONTENT_PAGE_URL" />
        <asp:BoundField DataField="CONTENT_PRICE" HeaderText="Price" SortExpression="CONTENT_PRICE" />
        <asp:TemplateField HeaderText="State" SortExpression="CONTENT_STATE">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("CONTENT_STATE") %>'>
                    <asp:ListItem Value="idl">Idle</asp:ListItem>
                    <asp:ListItem Value="atv">Active</asp:ListItem>
                    <asp:ListItem Value="dsl">Disable</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList3" runat="server" Enabled="False" SelectedValue='<%# Bind("CONTENT_STATE") %>'>
                    <asp:ListItem Value="idl">Idle</asp:ListItem>
                    <asp:ListItem Value="atv">Active</asp:ListItem>
                    <asp:ListItem Value="dsl">Disable</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_CREATION_DATE" HeaderText="Creation Date" SortExpression="CONTENT_CREATION_DATE" ReadOnly="True" />
        <asp:BoundField DataField="CONTENT_EXPIRY_DATE" HeaderText="Expirt Date" SortExpression="CONTENT_EXPIRY_DATE" />
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> &nbsp;
    &nbsp; &nbsp;<BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Content&nbsp; </SPAN></STRONG></DIV><DIV>
                <asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsContentList" DataKeyNames="CONTENT_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px" OnItemInserting="dlvServiceType_ItemInserting"><Fields>
    <asp:BoundField DataField="CONTENT_GRP_ID" HeaderText="CONTENT_GRP_ID" SortExpression="CONTENT_GRP_ID"
        Visible="False" />
    <asp:BoundField DataField="CONTENT_ID" HeaderText="CONTENT_ID" ReadOnly="True" SortExpression="CONTENT_ID"
        Visible="False" />
    <asp:BoundField DataField="CONTENT_TITLE" HeaderText="Content Title" SortExpression="CONTENT_TITLE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_PAGE_URL" HeaderText="Page URL" SortExpression="CONTENT_PAGE_URL">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_PRICE" HeaderText="Price" SortExpression="CONTENT_PRICE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="State" SortExpression="CONTENT_STATE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CONTENT_STATE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList5" runat="server" SelectedValue='<%# Bind("CONTENT_STATE") %>'>
                <asp:ListItem Selected="True" Value="idl">Idle</asp:ListItem>
                <asp:ListItem Value="atv">Active</asp:ListItem>
                <asp:ListItem Value="dsl">Disable</asp:ListItem>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONTENT_STATE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="CONTENT_EXPIRY_DATE" HeaderText="Expiry Date" SortExpression="CONTENT_EXPIRY_DATE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_PROV_ID" HeaderText="CONTENT_PROV_ID" SortExpression="CONTENT_PROV_ID"
        Visible="False" />
    <asp:CommandField ButtonType="Button" ShowInsertButton="True">
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView>
    &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
