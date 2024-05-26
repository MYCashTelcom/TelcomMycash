<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContentType.aspx.cs" Inherits="Forms_frmContentType" %>

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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Content Type</SPAN></STRONG>&nbsp;</DIV>
<DIV><asp:SqlDataSource id="sdsContentType" runat="server" 
SelectCommand='SELECT "CONTENT_TYPE_ID", "CONTENT_TYPE_NAME", "CONTENT_TYPE_EXTENTION" FROM "CONTENT_TYPE"' 
UpdateCommand='UPDATE "CONTENT_TYPE" SET "CONTENT_TYPE_NAME" = :CONTENT_TYPE_NAME, "CONTENT_TYPE_EXTENTION" = :CONTENT_TYPE_EXTENTION WHERE "CONTENT_TYPE_ID" = :CONTENT_TYPE_ID' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
InsertCommand='INSERT INTO "CONTENT_TYPE" ("CONTENT_TYPE_NAME", "CONTENT_TYPE_EXTENTION") VALUES (:CONTENT_TYPE_NAME, :CONTENT_TYPE_EXTENTION)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "CONTENT_TYPE" WHERE "CONTENT_TYPE_ID" = :CONTENT_TYPE_ID'><DeleteParameters>
    <asp:Parameter Name="CONTENT_TYPE_ID" Type="String" />
</DeleteParameters>
<UpdateParameters>
    <asp:Parameter Name="CONTENT_TYPE_NAME" Type="String" />
    <asp:Parameter Name="CONTENT_TYPE_EXTENTION" Type="String" />
    <asp:Parameter Name="CONTENT_TYPE_ID" Type="String" />
</UpdateParameters>
<InsertParameters>    
    <asp:Parameter Name="CONTENT_TYPE_NAME" Type="String" />
    <asp:Parameter Name="CONTENT_TYPE_EXTENTION" Type="String" />
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="GridView1" runat="server" DataSourceID="sdsContentType" DataKeyNames="CONTENT_TYPE_ID" BorderColor="Silver" AutoGenerateColumns="False" AllowSorting="True"><Columns>
    <asp:BoundField DataField="CONTENT_TYPE_ID" HeaderText="CONTENT_TYPE_ID" ReadOnly="True"
        SortExpression="CONTENT_TYPE_ID" Visible="False" />
    <asp:BoundField DataField="CONTENT_TYPE_NAME" HeaderText="Content Name" SortExpression="Type Name">
        <HeaderStyle HorizontalAlign="Center" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_TYPE_EXTENTION" HeaderText="Content Extention"
        SortExpression="CONTENT_TYPE_EXTENTION">
        <HeaderStyle HorizontalAlign="Center" />
    </asp:BoundField>
    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Content Type&nbsp;</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsContentType" DataKeyNames="CONTENT_TYPE_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
    <asp:BoundField DataField="CONTENT_TYPE_ID" HeaderText="CONTENT_TYPE_ID" ReadOnly="True"
        SortExpression="CONTENT_TYPE_ID" Visible="False">
        <ItemStyle HorizontalAlign="Right" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_TYPE_NAME" HeaderText="Content Name" SortExpression="CONTENT_TYPE_NAME">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
        <ItemStyle HorizontalAlign="Right" />
    </asp:BoundField>
    <asp:BoundField DataField="CONTENT_TYPE_EXTENTION" HeaderText="Content Extention"
        SortExpression="CONTENT_TYPE_EXTENTION">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
        <ItemStyle HorizontalAlign="Right" />
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
