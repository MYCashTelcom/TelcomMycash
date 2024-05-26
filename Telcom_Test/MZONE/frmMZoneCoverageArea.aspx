<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZoneCoverageArea.aspx.cs" Inherits="Forms_frmMZoneCoverageArea" Title="Mobile Zone Coverage Area" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Coverage Area&nbsp;</SPAN></STRONG> </DIV><DIV><asp:SqlDataSource id="sdsMZoneCovArea" runat="server" SelectCommand='SELECT "MZONE_AREA_ID", "MZONE_AREA_NAME" FROM "MZONE_AREA_LIST"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" UpdateCommand='UPDATE "MZONE_AREA_LIST" SET "MZONE_AREA_NAME" = :MZONE_AREA_NAME WHERE "MZONE_AREA_ID" = :MZONE_AREA_ID' InsertCommand='INSERT INTO "MZONE_AREA_LIST" ("MZONE_AREA_ID", "MZONE_AREA_NAME") VALUES (:MZONE_AREA_ID, :MZONE_AREA_NAME)' DeleteCommand='DELETE FROM "MZONE_AREA_LIST" WHERE "MZONE_AREA_ID" = :MZONE_AREA_ID'><DeleteParameters>
<asp:Parameter Name="MZONE_AREA_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="MZONE_AREA_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_AREA_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="MZONE_AREA_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_AREA_NAME" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" BorderColor="#E0E0E0" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MZONE_AREA_ID" DataSourceID="sdsMZoneCovArea" Font-Size="11pt"><Columns>
<asp:BoundField DataField="MZONE_AREA_ID" HeaderText="MZONE_AREA_ID" ReadOnly="True" SortExpression="MZONE_AREA_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_AREA_NAME" HeaderText="Coverage Area" SortExpression="MZONE_AREA_NAME">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:CommandField EditText=" Edit " ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView></DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Coverage Area</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" BorderColor="#E0E0E0" DataKeyNames="MZONE_AREA_ID" DataSourceID="sdsMZoneCovArea" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px" Font-Size="11pt"><Fields>
<asp:BoundField DataField="MZONE_AREA_ID" HeaderText="MZONE_AREA_ID" ReadOnly="True" SortExpression="MZONE_AREA_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_AREA_NAME" HeaderText="New Coverage Area" SortExpression="MZONE_AREA_NAME">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:CommandField ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

