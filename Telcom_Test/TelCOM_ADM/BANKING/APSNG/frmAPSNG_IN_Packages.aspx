<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAPSNG_IN_Packages.aspx.cs" Inherits="Forms_frmAPSNG_IN_Packages" Title="IN Package" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">IN Package&nbsp;List</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsIN_Packages" runat="server" DeleteCommand='DELETE FROM "APSNG_IN_PACKAGES" WHERE "APSNG_INPKG_ID" = :APSNG_INPKG_ID' InsertCommand='INSERT INTO "APSNG_IN_PACKAGES" ("APSNG_INPKG_ID", "APSNG_INPKG_NAME", "APSNG_INPKG_CODE", "APSNG_INPKG_PKGID", "APSNG_INPKG_TARIF_ENG", "APSNG_INPKG_TARIF_LOC", "APSNG_INPKG_PROM_ENG", "APSNG_INPKG_PROM_LOC") VALUES (:APSNG_INPKG_ID, :APSNG_INPKG_NAME, :APSNG_INPKG_CODE, :APSNG_INPKG_PKGID, :APSNG_INPKG_TARIF_ENG, :APSNG_INPKG_TARIF_LOC, :APSNG_INPKG_PROM_ENG, :APSNG_INPKG_PROM_LOC)' UpdateCommand='UPDATE "APSNG_IN_PACKAGES" SET "APSNG_INPKG_NAME" = :APSNG_INPKG_NAME, "APSNG_INPKG_CODE" = :APSNG_INPKG_CODE, "APSNG_INPKG_PKGID" = :APSNG_INPKG_PKGID, "APSNG_INPKG_TARIF_ENG" = :APSNG_INPKG_TARIF_ENG, "APSNG_INPKG_TARIF_LOC" = :APSNG_INPKG_TARIF_LOC, "APSNG_INPKG_PROM_ENG" = :APSNG_INPKG_PROM_ENG, "APSNG_INPKG_PROM_LOC" = :APSNG_INPKG_PROM_LOC WHERE "APSNG_INPKG_ID" = :APSNG_INPKG_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "APSNG_INPKG_ID", "APSNG_INPKG_NAME", "APSNG_INPKG_CODE", "APSNG_INPKG_PKGID", "APSNG_INPKG_TARIF_ENG", "APSNG_INPKG_TARIF_LOC", "APSNG_INPKG_PROM_ENG", "APSNG_INPKG_PROM_LOC" FROM "APSNG_IN_PACKAGES"'><DeleteParameters>
<asp:Parameter Name="APSNG_INPKG_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="APSNG_INPKG_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_CODE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PKGID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_TARIF_ENG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_TARIF_LOC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PROM_ENG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PROM_LOC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="APSNG_INPKG_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_CODE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PKGID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_TARIF_ENG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_TARIF_LOC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PROM_ENG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_PROM_LOC" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsIN_Packages" DataKeyNames="APSNG_INPKG_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt" AllowPaging="True" PageSize="3"><Columns>
<asp:BoundField DataField="APSNG_INPKG_ID" HeaderText="APSNG_INPKG_ID" ReadOnly="True" SortExpression="APSNG_INPKG_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_NAME" HeaderText="Package Name" SortExpression="APSNG_INPKG_NAME">
<HeaderStyle HorizontalAlign="Center" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_CODE" HeaderText="Package Code" SortExpression="APSNG_INPKG_CODE">
<HeaderStyle HorizontalAlign="Center" Wrap="True" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_PKGID" HeaderText="IN Package ID" SortExpression="APSNG_INPKG_PKGID">
<HeaderStyle HorizontalAlign="Center" Wrap="True" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Tariff Deatil In English" SortExpression="APSNG_INPKG_TARIF_ENG"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_TARIF_ENG") %>' __designer:wfdid="w44" TextMode="MultiLine" MaxLength="280"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox5" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_TARIF_ENG") %>' __designer:wfdid="w42" TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Tariff Deatil In Local" SortExpression="APSNG_INPKG_TARIF_LOC"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_TARIF_LOC") %>' __designer:wfdid="w46" TextMode="MultiLine" MaxLength="280"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox6" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_TARIF_LOC") %>' __designer:wfdid="w26" TextMode="MultiLine" MaxLength="280" ReadOnly="True"></asp:TextBox> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Migration Menu In English" SortExpression="APSNG_INPKG_PROM_ENG"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_PROM_ENG") %>' __designer:wfdid="w48" TextMode="MultiLine" MaxLength="280"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox7" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_PROM_ENG") %>' __designer:wfdid="w29" TextMode="MultiLine" MaxLength="280" ReadOnly="True"></asp:TextBox> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Migration Menu In Local" SortExpression="APSNG_INPKG_PROM_LOC"><EditItemTemplate>
<asp:TextBox id="TextBox4" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_PROM_LOC") %>' __designer:wfdid="w31" TextMode="MultiLine" MaxLength="280"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox8" runat="server" Height="100px" Width="150px" Text='<%# Bind("APSNG_INPKG_PROM_LOC") %>' __designer:wfdid="w32" TextMode="MultiLine" ReadOnly="True"></asp:TextBox> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView> &nbsp; </DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Package</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsIN_Packages" DataKeyNames="APSNG_INPKG_ID" BorderColor="#E0E0E0" Font-Size="11pt" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert"><Fields>
<asp:BoundField DataField="APSNG_INPKG_ID" HeaderText="APSNG_INPKG_ID" ReadOnly="True" SortExpression="APSNG_INPKG_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_NAME" HeaderText="Package Name" SortExpression="APSNG_INPKG_NAME">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_CODE" HeaderText="Package Code" SortExpression="APSNG_INPKG_CODE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_INPKG_PKGID" HeaderText="IN Package ID" SortExpression="APSNG_INPKG_PKGID">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Tariff Detail In English" SortExpression="APSNG_INPKG_TARIF_ENG"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("APSNG_INPKG_TARIF_ENG") %>' __designer:wfdid="w15"></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Height="100px" Width="210px" Text='<%# Bind("APSNG_INPKG_TARIF_ENG") %>' __designer:wfdid="w16" TextMode="MultiLine" MaxLength="280"></asp:TextBox>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("APSNG_INPKG_TARIF_ENG") %>' __designer:wfdid="w14"></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Tariff Detail In Local" SortExpression="APSNG_INPKG_TARIF_LOC"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("APSNG_INPKG_TARIF_LOC") %>' __designer:wfdid="w18"></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Height="100px" Width="210px" Text='<%# Bind("APSNG_INPKG_TARIF_LOC") %>' __designer:wfdid="w19" TextMode="MultiLine" MaxLength="280"></asp:TextBox>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label2" runat="server" Text='<%# Bind("APSNG_INPKG_TARIF_LOC") %>' __designer:wfdid="w17"></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Migration Menu In English" SortExpression="APSNG_INPKG_PROM_ENG"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("APSNG_INPKG_PROM_ENG") %>' __designer:wfdid="w9"></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Height="100px" Width="210px" Text='<%# Bind("APSNG_INPKG_PROM_ENG") %>' __designer:wfdid="w10" TextMode="MultiLine" MaxLength="280"></asp:TextBox>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label3" runat="server" Text='<%# Bind("APSNG_INPKG_PROM_ENG") %>' __designer:wfdid="w8"></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Migration Menu In Local" SortExpression="APSNG_INPKG_PROM_LOC"><EditItemTemplate>
<asp:TextBox id="TextBox4" runat="server" Text='<%# Bind("APSNG_INPKG_PROM_LOC") %>' __designer:wfdid="w12"></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox4" runat="server" Height="100px" Width="210px" Text='<%# Bind("APSNG_INPKG_PROM_LOC") %>' __designer:wfdid="w13" TextMode="MultiLine" MaxLength="280"></asp:TextBox>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label4" runat="server" Text='<%# Bind("APSNG_INPKG_PROM_LOC") %>' __designer:wfdid="w11"></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField InsertText="Add Package" ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView>&nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

