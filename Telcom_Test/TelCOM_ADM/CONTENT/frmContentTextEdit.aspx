<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContentTextEdit.aspx.cs" Inherits="Forms_frmContentTextEdit" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Text Content&nbsp; of Group
    <asp:DropDownList ID="ddlContGroup" runat="server" DataSourceID="sdsContentGroup"
        DataTextField="CONTENT_GRP_NAME" DataValueField="CONTENT_GRP_ID" AutoPostBack="True" OnSelectedIndexChanged="ddlContGroup_SelectedIndexChanged">
    </asp:DropDownList></SPAN></STRONG></DIV>
<DIV><asp:SqlDataSource id="sdsContentList" runat="server" 
SelectCommand='SELECT "CONTENT_ID", "CONTENT_TITLE" FROM "CONTENT_LIST" ORDER BY "CONTENT_ID"' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>">
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsContentGroup" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CONTENT_GRP_ID", "CONTENT_GRP_NAME" FROM "CONTENT_GROUP"'>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsContentDetail" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "CONTENT_DETAIL" WHERE "CONTENT_DETAIL_ID" = :CONTENT_DETAIL_ID'
        InsertCommand='INSERT INTO "CONTENT_DETAIL" ("CONTENT_ID", "CONTENT_TEXT_FILE", "CONTENT_WIDTH", "COTENT_CODE","CONTENT_HEIGHT") VALUES (:CONTENT_ID, :CONTENT_TEXT_FILE, :CONTENT_WIDTH, :COTENT_CODE,:CONTENT_HEIGHT)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CONTENT_ID", "CONTENT_DETAIL_ID", "CONTENT_TEXT_FILE", "CONTENT_WIDTH", "COTENT_CODE" ,"CONTENT_HEIGHT" FROM "CONTENT_DETAIL"'
        UpdateCommand='UPDATE "CONTENT_DETAIL" SET "CONTENT_ID" = :CONTENT_ID, "CONTENT_TEXT_FILE" = :CONTENT_TEXT_FILE, "CONTENT_WIDTH" = :CONTENT_WIDTH, "COTENT_CODE" = :COTENT_CODE, "CONTENT_HEIGHT"=:CONTENT_HEIGHT WHERE "CONTENT_DETAIL_ID" = :CONTENT_DETAIL_ID'>
        <DeleteParameters>
            <asp:Parameter Name="CONTENT_DETAIL_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="CONTENT_ID" Type="String" />
            <asp:Parameter Name="CONTENT_TEXT_FILE" Type="String" />
            <asp:Parameter Name="CONTENT_WIDTH" />
            <asp:Parameter Name="COTENT_CODE" Type="String" />
            <asp:Parameter Name="CONTENT_HEIGHT" />
            <asp:Parameter Name="CONTENT_DETAIL_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CONTENT_ID" Type="String" />
            <asp:Parameter Name="CONTENT_TEXT_FILE" Type="String" />
            <asp:Parameter Name="CONTENT_WIDTH" />
            <asp:Parameter Name="COTENT_CODE" Type="String" />
            <asp:Parameter Name="CONTENT_HEIGHT" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:GridView id="grvContentDetail" runat="server" DataSourceID="sdsContentDetail" DataKeyNames="CONTENT_DETAIL_ID" BorderColor="Silver" AutoGenerateColumns="False" AllowSorting="True"><Columns>
        <asp:TemplateField HeaderText="Content Title" SortExpression="CONTENT_ID">
            <EditItemTemplate>
                &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsContentList"
                    DataTextField="CONTENT_TITLE" DataValueField="CONTENT_ID" SelectedValue='<%# Bind("CONTENT_ID") %>'>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsContentList"
                    DataTextField="CONTENT_TITLE" DataValueField="CONTENT_ID" SelectedValue='<%# Bind("CONTENT_ID") %>'>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_DETAIL_ID" HeaderText="CONTENT_DETAIL_ID" ReadOnly="True"
            SortExpression="CONTENT_DETAIL_ID" Visible="False" />
        <asp:TemplateField HeaderText="Content Text/File" SortExpression="CONTENT_TEXT_FILE">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Height="106px" Text='<%# Bind("CONTENT_TEXT_FILE") %>'
                    TextMode="MultiLine" Width="373px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONTENT_TEXT_FILE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CONTENT_WIDTH" HeaderText="Content Width" SortExpression="CONTENT_WIDTH" />
        <asp:BoundField DataField="CONTENT_HEIGHT" HeaderText="Content Height" SortExpression="CONTENT_HEIGHT" />
        <asp:BoundField DataField="COTENT_CODE" HeaderText="Code" SortExpression="COTENT_CODE" />
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Content Text</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsContentDetail" DataKeyNames="CONTENT_DETAIL_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
    <asp:TemplateField HeaderText="Content Title" SortExpression="CONTENT_ID">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CONTENT_ID") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsContentList"
                DataTextField="CONTENT_TITLE" DataValueField="CONTENT_ID" SelectedValue='<%# Bind("CONTENT_ID") %>'>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CONTENT_ID") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="CONTENT_DETAIL_ID" HeaderText="CONTENT_DETAIL_ID" ReadOnly="True"
        SortExpression="CONTENT_DETAIL_ID" Visible="False" />
    <asp:TemplateField HeaderText="Content Text/File" SortExpression="CONTENT_TEXT_FILE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CONTENT_TEXT_FILE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Height="83px" Text='<%# Bind("CONTENT_TEXT_FILE") %>'
                TextMode="MultiLine" Width="280px"></asp:TextBox>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CONTENT_TEXT_FILE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="CONTENT_WIDTH" HeaderText="Content Width" SortExpression="CONTENT_WIDTH">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_HEIGHT" HeaderText="Content Height" SortExpression="CONTENT_HEIGHT">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:BoundField DataField="COTENT_CODE" HeaderText="Content Code" SortExpression="COTENT_CODE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
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
