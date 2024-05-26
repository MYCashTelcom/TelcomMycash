<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContentGroup.aspx.cs" Inherits="Forms_frmContentGroup" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Content Group</SPAN></STRONG></DIV>
<DIV><asp:SqlDataSource id="sdsContentType" runat="server" 
SelectCommand='SELECT "CONTENT_TYPE_ID", "CONTENT_TYPE_NAME", "CONTENT_TYPE_EXTENTION" FROM "CONTENT_TYPE" ' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsContentGroup" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "CONTENT_GROUP" WHERE "CONTENT_GRP_ID" = :CONTENT_GRP_ID'
        InsertCommand='INSERT INTO CONTENT_GROUP(CONTENT_TYPE_ID, CONTENT_GRP_ID, CONTENT_GRP_NAME, CONTENT_GRP_IMAGE, CONTENT_GROUP_STATE, CONTENT_PAGE_URL, CONTENT_GRP_INDEX, CONTENT_GRP_PRNT_ID, CONTENT_GRP_IS_PARENT) VALUES (:CONTENT_TYPE_ID, :CONTENT_GRP_ID, :CONTENT_GRP_NAME, :CONTENT_GRP_IMAGE, :CONTENT_GROUP_STATE, :CONTENT_PAGE_URL, :CONTENT_GRP_INDEX, :CONTENT_GRP_PRNT_ID, :CONTENT_GRP_IS_PARENT)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT CONTENT_TYPE_ID, CONTENT_GRP_ID, CONTENT_GRP_NAME, CONTENT_GRP_IMAGE, CONTENT_GROUP_STATE, CONTENT_PAGE_URL, CONTENT_GRP_INDEX, CONTENT_GRP_PRNT_ID, CONTENT_GRP_IS_PARENT FROM CONTENT_GROUP ORDER BY CONTENT_GRP_INDEX'
        UpdateCommand='UPDATE CONTENT_GROUP SET CONTENT_TYPE_ID = :CONTENT_TYPE_ID, CONTENT_GRP_NAME = :CONTENT_GRP_NAME, CONTENT_GRP_IMAGE = :CONTENT_GRP_IMAGE, CONTENT_GROUP_STATE = :CONTENT_GROUP_STATE, CONTENT_PAGE_URL = :CONTENT_PAGE_URL, CONTENT_GRP_INDEX = :CONTENT_GRP_INDEX, CONTENT_GRP_PRNT_ID = :CONTENT_GRP_PRNT_ID, CONTENT_GRP_IS_PARENT = :CONTENT_GRP_IS_PARENT WHERE (CONTENT_GRP_ID = :CONTENT_GRP_ID)'>
        <DeleteParameters>
            <asp:Parameter Name="CONTENT_GRP_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="CONTENT_TYPE_ID" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_NAME" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_IMAGE" Type="String" />
            <asp:Parameter Name="CONTENT_GROUP_STATE" Type="String" />
            <asp:Parameter Name="CONTENT_PAGE_URL" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_INDEX" />
            <asp:Parameter Name="CONTENT_GRP_PRNT_ID" />
            <asp:Parameter Name="CONTENT_GRP_IS_PARENT" />
            <asp:Parameter Name="CONTENT_GRP_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CONTENT_TYPE_ID" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_ID" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_NAME" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_IMAGE" Type="String" />
            <asp:Parameter Name="CONTENT_GROUP_STATE" Type="String" />
            <asp:Parameter Name="CONTENT_PAGE_URL" Type="String" />
            <asp:Parameter Name="CONTENT_GRP_INDEX" />
            <asp:Parameter Name="CONTENT_GRP_PRNT_ID" />
            <asp:Parameter Name="CONTENT_GRP_IS_PARENT" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:GridView id="GridView1" runat="server" DataSourceID="sdsContentGroup" DataKeyNames="CONTENT_GRP_ID" BorderColor="Silver" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="8"><Columns>
        <asp:TemplateField HeaderText="Content Type" SortExpression="CONTENT_TYPE_ID">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsContentType"
                    DataTextField="CONTENT_TYPE_NAME" DataValueField="CONTENT_TYPE_ID" SelectedValue='<%# Bind("CONTENT_TYPE_ID") %>'>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsContentType"
                    DataTextField="CONTENT_TYPE_NAME" DataValueField="CONTENT_TYPE_ID" SelectedValue='<%# Bind("CONTENT_TYPE_ID") %>' Enabled="False">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_GRP_ID" HeaderText="CONTENT_GRP_ID" ReadOnly="True"
            SortExpression="CONTENT_GRP_ID" Visible="False" />
        <asp:BoundField DataField="CONTENT_GRP_INDEX" HeaderText="Display Order" SortExpression="CONTENT_GRP_INDEX" />
        <asp:BoundField DataField="CONTENT_GRP_NAME" HeaderText="Content Group" SortExpression="CONTENT_GRP_NAME" />
        <asp:BoundField DataField="CONTENT_GRP_IMAGE" HeaderText="Group Image" SortExpression="CONTENT_GRP_IMAGE" />
        <asp:TemplateField HeaderText="Group State" SortExpression="CONTENT_GROUP_STATE">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList5" runat="server" SelectedValue='<%# Bind("CONTENT_GROUP_STATE") %>'>
                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("CONTENT_GROUP_STATE") %>' Enabled="False">
                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_PAGE_URL" HeaderText="Page URL" SortExpression="CONTENT_PAGE_URL" />
        <asp:TemplateField HeaderText="Parent" SortExpression="CONTENT_GRP_IS_PARENT">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList11" runat="server" SelectedValue='<%# Bind("CONTENT_GRP_IS_PARENT") %>'>
                    <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList10" runat="server" Enabled="False" SelectedValue='<%# Bind("CONTENT_GRP_IS_PARENT") %>'>
                    <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Parent Group" SortExpression="CONTENT_GRP_PRNT_ID">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList9" runat="server" DataSourceID="sdsContentGroup"
                    DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" SelectedValue='<%# Bind("CONTENT_GRP_PRNT_ID") %>'>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList8" runat="server" DataSourceID="sdsContentGroup"
                    DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" Enabled="False"
                    SelectedValue='<%# Bind("CONTENT_GRP_PRNT_ID") %>'>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Content Group</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsContentGroup" DataKeyNames="CONTENT_GRP_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
    <asp:TemplateField HeaderText="Content Type" SortExpression="CONTENT_TYPE_ID">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CONTENT_TYPE_ID") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsContentType"
                DataTextField="CONTENT_TYPE_NAME" DataValueField="CONTENT_TYPE_ID" SelectedValue='<%# Bind("CONTENT_TYPE_ID") %>'>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONTENT_TYPE_ID") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="CONTENT_GRP_INDEX" HeaderText="Displsy Order" SortExpression="CONTENT_GRP_INDEX" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_GRP_ID" HeaderText="CONTENT_GRP_ID" ReadOnly="True"
        SortExpression="CONTENT_GRP_ID" Visible="False" />
    <asp:BoundField DataField="CONTENT_GRP_NAME" HeaderText="Content Group" SortExpression="CONTENT_GRP_NAME">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_GRP_IMAGE" HeaderText="Group Image" SortExpression="CONTENT_GRP_IMAGE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="Group State" SortExpression="CONTENT_GROUP_STATE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CONTENT_GROUP_STATE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList6" runat="server" SelectedValue='<%# Bind("CONTENT_GROUP_STATE") %>'>
                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                <asp:ListItem Value="I">Inactive</asp:ListItem>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CONTENT_GROUP_STATE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="CONTENT_PAGE_URL" HeaderText="Page URL" SortExpression="CONTENT_PAGE_URL">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="Parent" SortExpression="CONTENT_GRP_IS_PARENT">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CONTENT_GRP_IS_PARENT") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList12" runat="server" SelectedValue='<%# Bind("CONTENT_GRP_IS_PARENT") %>'>
                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label4" runat="server" Text='<%# Bind("CONTENT_GRP_IS_PARENT") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Parent Group" SortExpression="CONTENT_GRP_PRNT_ID">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CONTENT_GRP_PRNT_ID") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="sdsContentGroup"
                DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" SelectedValue='<%# Bind("CONTENT_GRP_PRNT_ID") %>'>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text='<%# Bind("CONTENT_GRP_PRNT_ID") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:CommandField ButtonType="Button" ShowInsertButton="True">
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
