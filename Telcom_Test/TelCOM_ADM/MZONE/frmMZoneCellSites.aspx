<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZoneCellSites.aspx.cs" Inherits="Forms_frmMZoneCellSites" Title="Mobile Zone Cell Sites" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Cell Sites&nbsp;</SPAN></STRONG> </DIV><DIV><asp:SqlDataSource id="sdsMZoneCovArea" runat="server" SelectCommand='SELECT "MZONE_AREA_ID", "MZONE_AREA_NAME" FROM "MZONE_AREA_LIST"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" UpdateCommand='UPDATE "MZONE_AREA_LIST" SET "MZONE_AREA_NAME" = :MZONE_AREA_NAME WHERE "MZONE_AREA_ID" = :MZONE_AREA_ID' InsertCommand='INSERT INTO "MZONE_AREA_LIST" ("MZONE_AREA_ID", "MZONE_AREA_NAME") VALUES (:MZONE_AREA_ID, :MZONE_AREA_NAME)' DeleteCommand='DELETE FROM "MZONE_AREA_LIST" WHERE "MZONE_AREA_ID" = :MZONE_AREA_ID'><DeleteParameters>
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
</asp:SqlDataSource> <asp:SqlDataSource id="sqdCellSites" runat="server" SelectCommand='SELECT "MZONE_CELL_ID", "MZONE_CELL_TITLE", "MZONE_CELL_MHT_CAP", "MZONE_CELL_CELLID", "MZONE_CELL_LONGITUDE", "MZONE_CELL_LATITUDE", "MZONE_CELL_DESC", "MZONE_AREA_ID" FROM "MZONE_CELL_LIST"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" UpdateCommand='UPDATE "MZONE_CELL_LIST" SET "MZONE_CELL_TITLE" = :MZONE_CELL_TITLE, "MZONE_CELL_MHT_CAP" = :MZONE_CELL_MHT_CAP, "MZONE_CELL_CELLID" = :MZONE_CELL_CELLID, "MZONE_CELL_LONGITUDE" = :MZONE_CELL_LONGITUDE, "MZONE_CELL_LATITUDE" = :MZONE_CELL_LATITUDE, "MZONE_CELL_DESC" = :MZONE_CELL_DESC, "MZONE_AREA_ID" = :MZONE_AREA_ID WHERE "MZONE_CELL_ID" = :MZONE_CELL_ID' InsertCommand='INSERT INTO "MZONE_CELL_LIST" ("MZONE_CELL_ID", "MZONE_CELL_TITLE", "MZONE_CELL_MHT_CAP", "MZONE_CELL_CELLID", "MZONE_CELL_LONGITUDE", "MZONE_CELL_LATITUDE", "MZONE_CELL_DESC", "MZONE_AREA_ID") VALUES (:MZONE_CELL_ID, :MZONE_CELL_TITLE, :MZONE_CELL_MHT_CAP, :MZONE_CELL_CELLID, :MZONE_CELL_LONGITUDE, :MZONE_CELL_LATITUDE, :MZONE_CELL_DESC, :MZONE_AREA_ID)' DeleteCommand='DELETE FROM "MZONE_CELL_LIST" WHERE "MZONE_CELL_ID" = :MZONE_CELL_ID' __designer:wfdid="w1"><DeleteParameters>
<asp:Parameter Name="MZONE_CELL_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="MZONE_CELL_TITLE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_MHT_CAP" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_CELLID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_LONGITUDE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_LATITUDE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_AREA_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="MZONE_CELL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_TITLE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_MHT_CAP" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_CELLID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_LONGITUDE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_LATITUDE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CELL_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_AREA_ID" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" Font-Size="11pt" BorderColor="#E0E0E0" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MZONE_CELL_ID" DataSourceID="sqdCellSites"><Columns>
<asp:BoundField DataField="MZONE_CELL_ID" HeaderText="MZONE_CELL_ID" ReadOnly="True" SortExpression="MZONE_CELL_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_CELLID" HeaderText="Cell ID" SortExpression="MZONE_CELL_CELLID">
<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_TITLE" HeaderText="Cell Name" SortExpression="MZONE_CELL_TITLE">
<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_MHT_CAP" HeaderText="MHT Capacity / Hour (Minutes)" SortExpression="MZONE_CELL_MHT_CAP">
<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_LONGITUDE" HeaderText="Longitude" SortExpression="MZONE_CELL_LONGITUDE">
<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_LATITUDE" HeaderText="Latitude" SortExpression="MZONE_CELL_LATITUDE">
<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_DESC" HeaderText="Cell Description" SortExpression="MZONE_CELL_DESC">
<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Coverage Area" SortExpression="MZONE_AREA_ID"><EditItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsMZoneCovArea" __designer:wfdid="w10" DataTextField="MZONE_AREA_NAME" DataValueField="MZONE_AREA_ID" SelectedValue='<%# Bind("MZONE_AREA_ID") %>'></asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" DataSourceID="sdsMZoneCovArea" __designer:wfdid="w11" DataTextField="MZONE_AREA_NAME" DataValueField="MZONE_AREA_ID" SelectedValue='<%# Bind("MZONE_AREA_ID") %>' Enabled="False"></asp:DropDownList>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Columns>
</asp:GridView></DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Cell Site</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" Font-Size="11pt" BorderColor="#E0E0E0" DataKeyNames="MZONE_CELL_ID" DataSourceID="sqdCellSites" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
<asp:BoundField DataField="MZONE_CELL_ID" HeaderText="MZONE_CELL_ID" ReadOnly="True" SortExpression="MZONE_CELL_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_CELLID" HeaderText="Cell ID" SortExpression="MZONE_CELL_CELLID">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_TITLE" HeaderText="Cell Name" SortExpression="MZONE_CELL_TITLE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_MHT_CAP" HeaderText="MHT  Capacity / Hour (Minutes)" SortExpression="MZONE_CELL_MHT_CAP">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_LONGITUDE" HeaderText="Longitude" SortExpression="MZONE_CELL_LONGITUDE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_LATITUDE" HeaderText="Latitude" SortExpression="MZONE_CELL_LATITUDE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_CELL_DESC" HeaderText="Cell Description" SortExpression="MZONE_CELL_DESC">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Coverage Area" SortExpression="MZONE_AREA_ID"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w4" Text='<%# Bind("MZONE_AREA_ID") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" DataSourceID="sdsMZoneCovArea" __designer:wfdid="w6" DataTextField="MZONE_AREA_NAME" DataValueField="MZONE_AREA_ID" SelectedValue='<%# Bind("MZONE_AREA_ID") %>'></asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" __designer:wfdid="w3" Text='<%# Bind("MZONE_AREA_ID") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField InsertText="Add Cell" ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>