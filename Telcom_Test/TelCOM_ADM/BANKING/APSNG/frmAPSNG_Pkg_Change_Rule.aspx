<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAPSNG_Pkg_Change_Rule.aspx.cs" Inherits="Forms_frmAPSNG_Pkg_Change_Rule" Title="Package Change Rule" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Package&nbsp;Changing Rule</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsIN_Packages" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "APSNG_INPKG_ID", "APSNG_INPKG_NAME" FROM "APSNG_IN_PACKAGES"'></asp:SqlDataSource> <asp:SqlDataSource id="sdsServiceList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT SERVICE_TITLE, SERVICE_ID FROM SERVICE_LIST SL,SERVICE_TYPE ST WHERE SL.SERVICE_TYPE_ID=ST.SERVICE_TYPE_ID AND ST.SERVICE_TYPE_CODE='APSNG'"></asp:SqlDataSource> 
<asp:SqlDataSource id="sdsIN_Pkg_Chng_rule" runat="server" DeleteCommand='DELETE FROM "APSNG_IN_PKG_CRL" WHERE "APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID' InsertCommand='INSERT INTO "APSNG_IN_PKG_CRL" ("APSNG_IN_PCRL_ID", "APSNG_INPKG_SOURCE", "APSNG_INPKG_DESTINATION", "APSNG_IN_PCRL_CFEE", "APSNG_IN_PCRL_VDAYS", "APSNG_IN_PCRL_GDAYS", "APSNG_IN_PCRL_RNABLE", "APSNG_IN_PCRL_STATUS", "APSNG_IN_PCRL_OSDATE", "APSNG_IN_PCRL_OEDATE", "APSNG_IN_PCRL_DESC", "APSNG_IN_PCRL_MSG","SERVICE_ID","APSNG_IN_PCRL_CODE") VALUES (:APSNG_IN_PCRL_ID, :APSNG_INPKG_SOURCE, :APSNG_INPKG_DESTINATION, :APSNG_IN_PCRL_CFEE, :APSNG_IN_PCRL_VDAYS, :APSNG_IN_PCRL_GDAYS, :APSNG_IN_PCRL_RNABLE, :APSNG_IN_PCRL_STATUS, :APSNG_IN_PCRL_OSDATE, :APSNG_IN_PCRL_OEDATE, :APSNG_IN_PCRL_DESC, :APSNG_IN_PCRL_MSG,:SERVICE_ID,:APSNG_IN_PCRL_CODE)' UpdateCommand='UPDATE "APSNG_IN_PKG_CRL" SET "APSNG_INPKG_SOURCE" = :APSNG_INPKG_SOURCE, "APSNG_INPKG_DESTINATION" = :APSNG_INPKG_DESTINATION, "APSNG_IN_PCRL_CFEE" = :APSNG_IN_PCRL_CFEE, "APSNG_IN_PCRL_VDAYS" = :APSNG_IN_PCRL_VDAYS, "APSNG_IN_PCRL_GDAYS" = :APSNG_IN_PCRL_GDAYS, "APSNG_IN_PCRL_RNABLE" = :APSNG_IN_PCRL_RNABLE, "APSNG_IN_PCRL_STATUS" = :APSNG_IN_PCRL_STATUS, "APSNG_IN_PCRL_OSDATE" = :APSNG_IN_PCRL_OSDATE, "APSNG_IN_PCRL_OEDATE" = :APSNG_IN_PCRL_OEDATE, "APSNG_IN_PCRL_DESC" = :APSNG_IN_PCRL_DESC, "APSNG_IN_PCRL_MSG" = :APSNG_IN_PCRL_MSG, "SERVICE_ID"=:SERVICE_ID, "APSNG_IN_PCRL_CODE"=:APSNG_IN_PCRL_CODE WHERE "APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "APSNG_IN_PCRL_ID", "APSNG_INPKG_SOURCE", "APSNG_INPKG_DESTINATION", "APSNG_IN_PCRL_CFEE", "APSNG_IN_PCRL_VDAYS", "APSNG_IN_PCRL_GDAYS", "APSNG_IN_PCRL_RNABLE", "APSNG_IN_PCRL_STATUS", "APSNG_IN_PCRL_OSDATE", "APSNG_IN_PCRL_OEDATE", "APSNG_IN_PCRL_DESC", "APSNG_IN_PCRL_MSG","SERVICE_ID","APSNG_IN_PCRL_CODE" FROM "APSNG_IN_PKG_CRL"'><DeleteParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="APSNG_INPKG_SOURCE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_DESTINATION" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_CFEE" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_VDAYS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_GDAYS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_RNABLE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_STATUS" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OSDATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OEDATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="SERVICE_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_CODE" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_SOURCE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_INPKG_DESTINATION" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_CFEE" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_VDAYS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_GDAYS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_RNABLE" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_STATUS" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OSDATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OEDATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="SERVICE_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_CODE" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsIN_Pkg_Chng_rule" DataKeyNames="APSNG_IN_PCRL_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt"><Columns>
<asp:BoundField DataField="APSNG_IN_PCRL_ID" HeaderText="APSNG_IN_PCRL_ID" ReadOnly="True" SortExpression="APSNG_IN_PCRL_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Service" SortExpression="SERVICE_ID"><EditItemTemplate>
<asp:DropDownList id="DropDownList15" runat="server" DataSourceID="sdsServiceList" SelectedValue='<%# Bind("SERVICE_ID") %>' DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID"></asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList14" runat="server" DataSourceID="sdsServiceList" SelectedValue='<%# Bind("SERVICE_ID") %>' DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" Enabled="False"></asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_DESC" HeaderText="Rule Desc" SortExpression="APSNG_IN_PCRL_DESC">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
    <asp:BoundField DataField="APSNG_IN_PCRL_CODE" HeaderText="Rule Code" SortExpression="APSNG_IN_PCRL_CODE">
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
    </asp:BoundField>
<asp:TemplateField HeaderText="Soure Package" SortExpression="APSNG_INPKG_SOURCE"><EditItemTemplate>
<asp:DropDownList id="DropDownList6" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_SOURCE") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID"></asp:DropDownList> 
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList5" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_SOURCE") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID" Enabled="False"></asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Destination Package" SortExpression="APSNG_INPKG_DESTINATION"><EditItemTemplate>
<asp:DropDownList id="DropDownList8" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_DESTINATION") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID"></asp:DropDownList> 
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList7" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_DESTINATION") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID" Enabled="False"></asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_CFEE" HeaderText="Changing Fee" SortExpression="APSNG_IN_PCRL_CFEE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_VDAYS" HeaderText="Valid Days" SortExpression="APSNG_IN_PCRL_VDAYS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_GDAYS" HeaderText="Grace Days" SortExpression="APSNG_IN_PCRL_GDAYS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Renuable" SortExpression="APSNG_IN_PCRL_RNABLE"><EditItemTemplate>
<asp:DropDownList id="DropDownList10" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_RNABLE") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList> 
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList9" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_RNABLE") %>' Enabled="False"><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Status" SortExpression="APSNG_IN_PCRL_STATUS"><EditItemTemplate>
<asp:DropDownList id="DropDownList12" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_STATUS") %>'><asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
<asp:ListItem Value="I">Inactive</asp:ListItem>
<asp:ListItem Value="C">Close</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList11" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_STATUS") %>' Enabled="False"><asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
<asp:ListItem Value="I">Inactive</asp:ListItem>
<asp:ListItem Value="C">Closed</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_OSDATE" HeaderText="Start Date" SortExpression="APSNG_IN_PCRL_OSDATE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_OEDATE" HeaderText="End Date" SortExpression="APSNG_IN_PCRL_OEDATE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView> &nbsp; </DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Rule</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsIN_Pkg_Chng_rule" DataKeyNames="APSNG_IN_PCRL_ID" BorderColor="#E0E0E0" Font-Size="11pt" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert"><Fields>
<asp:BoundField DataField="APSNG_IN_PCRL_ID" HeaderText="APSNG_IN_PCRL_ID" ReadOnly="True" SortExpression="APSNG_IN_PCRL_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Service" SortExpression="SERVICE_ID"><EditItemTemplate>
<asp:TextBox id="TextBox5" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList13" runat="server" DataSourceID="sdsServiceList" SelectedValue='<%# Bind("SERVICE_ID") %>' DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID"></asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label5" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_DESC" HeaderText="Rule Desc" SortExpression="APSNG_IN_PCRL_DESC">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
    <asp:BoundField DataField="APSNG_IN_PCRL_CODE" HeaderText="Rule Code" SortExpression="APSNG_IN_PCRL_CODE">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
<asp:TemplateField HeaderText="Source Package" SortExpression="APSNG_INPKG_SOURCE"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("APSNG_INPKG_SOURCE") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_SOURCE") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID"></asp:DropDownList> 
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("APSNG_INPKG_SOURCE") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Destination Package" SortExpression="APSNG_INPKG_DESTINATION"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("APSNG_INPKG_DESTINATION") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsIN_Packages" SelectedValue='<%# Bind("APSNG_INPKG_DESTINATION") %>' DataTextField="APSNG_INPKG_NAME" DataValueField="APSNG_INPKG_ID"></asp:DropDownList> 
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label2" runat="server" Text='<%# Bind("APSNG_INPKG_DESTINATION") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_CFEE" HeaderText="Changing Fee" SortExpression="APSNG_IN_PCRL_CFEE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_VDAYS" HeaderText="Valid Days" SortExpression="APSNG_IN_PCRL_VDAYS">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_GDAYS" HeaderText="Grace Days" SortExpression="APSNG_IN_PCRL_GDAYS">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Reneable" SortExpression="APSNG_IN_PCRL_RNABLE"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("APSNG_IN_PCRL_RNABLE") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_RNABLE") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label3" runat="server" Text='<%# Bind("APSNG_IN_PCRL_RNABLE") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Status" SortExpression="APSNG_IN_PCRL_STATUS"><EditItemTemplate>
<asp:TextBox id="TextBox4" runat="server" Text='<%# Bind("APSNG_IN_PCRL_STATUS") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList4" runat="server" SelectedValue='<%# Bind("APSNG_IN_PCRL_STATUS") %>'><asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
<asp:ListItem Value="I">Inactive</asp:ListItem>
<asp:ListItem Value="C">Closed</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label4" runat="server" Text='<%# Bind("APSNG_IN_PCRL_STATUS") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRL_OSDATE" HeaderText="Start Date" SortExpression="APSNG_IN_PCRL_OSDATE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_OEDATE" HeaderText="End Date" SortExpression="APSNG_IN_PCRL_OEDATE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:CommandField InsertText="Add Rule" ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView>&nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

