<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZonePackages.aspx.cs" Inherits="Forms_frmMZonePackages" Title="Manage Mobile Zone Packages" %>
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
<TABLE><TBODY><TR><TD style="WIDTH: 425px"><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Zone Package List</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsMZonePackage" runat="server" DeleteCommand='DELETE FROM "MZONE_PACKAGE" WHERE "MZONE_PKG_ID" = :MZONE_PKG_ID' InsertCommand='INSERT INTO "MZONE_PACKAGE" ("MZONE_PKG_ID", "MZONE_PKG_NAME", "MZONE_PKG_SUBS_FEE", "MZONE_PKG_MONTH_FEE", "MZONE_PKG_DFEE_BOM") VALUES (:MZONE_PKG_ID, :MZONE_PKG_NAME, :MZONE_PKG_SUBS_FEE, :MZONE_PKG_MONTH_FEE, :MZONE_PKG_DFEE_BOM)' UpdateCommand='UPDATE "MZONE_PACKAGE" SET "MZONE_PKG_NAME" = :MZONE_PKG_NAME, "MZONE_PKG_SUBS_FEE" = :MZONE_PKG_SUBS_FEE, "MZONE_PKG_MONTH_FEE" = :MZONE_PKG_MONTH_FEE, "MZONE_PKG_DFEE_BOM" = :MZONE_PKG_DFEE_BOM WHERE "MZONE_PKG_ID" = :MZONE_PKG_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "MZONE_PKG_ID", "MZONE_PKG_NAME", "MZONE_PKG_SUBS_FEE", "MZONE_PKG_MONTH_FEE", "MZONE_PKG_DFEE_BOM" FROM "MZONE_PACKAGE"'><DeleteParameters>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        </DeleteParameters>
        <UpdateParameters>
        <asp:Parameter Name="MZONE_PKG_NAME" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_SUBS_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_MONTH_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_DFEE_BOM" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        </UpdateParameters>
        <InsertParameters>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_NAME" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_SUBS_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_MONTH_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_DFEE_BOM" Type="String"></asp:Parameter>
        </InsertParameters>
        </asp:SqlDataSource><asp:GridView id="GridView1" runat="server" DataSourceID="sdsMZonePackage" DataKeyNames="MZONE_PKG_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt"><Columns>
<asp:BoundField DataField="MZONE_PKG_ID" HeaderText="MZONE_PKG_ID" ReadOnly="True" SortExpression="MZONE_PKG_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_PKG_NAME" HeaderText="Zone Package Name" SortExpression="MZONE_PKG_NAME">
<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_PKG_SUBS_FEE" HeaderText="Subcription Fee" SortExpression="MZONE_PKG_SUBS_FEE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_PKG_MONTH_FEE" HeaderText="Monthly Fee" SortExpression="MZONE_PKG_MONTH_FEE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Deduct BOM" SortExpression="MZONE_PKG_DFEE_BOM"><EditItemTemplate>
        <asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("MZONE_PKG_DFEE_BOM") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N">No</asp:ListItem>
        </asp:DropDownList>
        
</EditItemTemplate>
<ItemTemplate>
        <asp:DropDownList id="DropDownList1" runat="server" SelectedValue='<%# Bind("MZONE_PKG_DFEE_BOM") %>' Enabled="False"><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N">No</asp:ListItem>
        </asp:DropDownList>
        
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button">
<ItemStyle Wrap="False"></ItemStyle>
</asp:CommandField>
</Columns>
</asp:GridView>&nbsp; </DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Package</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsMZonePackage" DataKeyNames="MZONE_PKG_ID" BorderColor="#E0E0E0" Font-Size="11pt" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert"><Fields>
        <asp:BoundField DataField="MZONE_PKG_ID" HeaderText="MZONE_PKG_ID" ReadOnly="True" SortExpression="MZONE_PKG_ID" Visible="False"></asp:BoundField>
        <asp:BoundField DataField="MZONE_PKG_NAME" HeaderText="Zone Package Name" SortExpression="MZONE_PKG_NAME">
        <HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
        </asp:BoundField>
        <asp:BoundField DataField="MZONE_PKG_SUBS_FEE" HeaderText="Subscription Fee" SortExpression="MZONE_PKG_SUBS_FEE">
        <HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
        </asp:BoundField>
        <asp:BoundField DataField="MZONE_PKG_MONTH_FEE" HeaderText="Monthly Fee" SortExpression="MZONE_PKG_MONTH_FEE">
        <HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
        </asp:BoundField>
        <asp:TemplateField HeaderText="Deduct BOM" SortExpression="MZONE_PKG_DFEE_BOM"><EditItemTemplate>
        <asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("MZONE_PKG_DFEE_BOM") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
        <asp:DropDownList id="DropDownList3" runat="server" SelectedValue='<%# Bind("MZONE_PKG_DFEE_BOM") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N">No</asp:ListItem>
        </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
        <asp:Label id="Label1" runat="server" Text='<%# Bind("MZONE_PKG_DFEE_BOM") %>'></asp:Label>
        </ItemTemplate>

        <HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
        </asp:TemplateField>
        <asp:CommandField InsertText="Add Package" ShowInsertButton="True" ButtonType="Button">
        <ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:CommandField>
        </Fields>
        </asp:DetailsView>&nbsp;</DIV></TD><TD vAlign=top><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">&nbsp;Package <asp:DropDownList id="DropDownList4" runat="server" DataSourceID="sdsMZonePackage" DataValueField="MZONE_PKG_ID" DataTextField="MZONE_PKG_NAME"></asp:DropDownList> <asp:SqlDataSource id="sdsMZonePkgZoneID" runat="server" DeleteCommand='DELETE FROM "MZONE_PACKAGE_ZONE" WHERE "MZONE_PKG_ZONE_ID" = :MZONE_PKG_ZONE_ID' InsertCommand='INSERT INTO "MZONE_PACKAGE_ZONE" ("MZONE_PKG_ZONE_ID", "MZONE_PKG_ID", "MZONE_ID") VALUES (:MZONE_PKG_ZONE_ID, :MZONE_PKG_ID, :MZONE_ID)' UpdateCommand='UPDATE "MZONE_PACKAGE_ZONE" SET "MZONE_PKG_ID" = :MZONE_PKG_ID, "MZONE_ID" = :MZONE_ID WHERE "MZONE_PKG_ZONE_ID" = :MZONE_PKG_ZONE_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "MZONE_PKG_ZONE_ID", "MZONE_PKG_ID", "MZONE_ID" FROM "MZONE_PACKAGE_ZONE"'>
                <DeleteParameters>
                    <asp:Parameter Name="MZONE_PKG_ZONE_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="MZONE_PKG_ID" Type="String" />
                    <asp:Parameter Name="MZONE_ID" Type="String" />
                    <asp:Parameter Name="MZONE_PKG_ZONE_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MZONE_PKG_ZONE_ID" Type="String" />
                    <asp:Parameter Name="MZONE_PKG_ID" Type="String" />
                    <asp:Parameter Name="MZONE_ID" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource> <asp:SqlDataSource id="sdsZoneList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "MZONE_ID", "MZONE_TITLE" FROM "MZONE_LIST"'>
            </asp:SqlDataSource> </SPAN></STRONG></DIV><DIV><asp:GridView id="GridView2" runat="server" DataSourceID="sdsMZonePkgZoneID" DataKeyNames="MZONE_PKG_ZONE_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0"><Columns>
<asp:BoundField DataField="MZONE_PKG_ZONE_ID" HeaderText="MZONE_PKG_ZONE_ID" ReadOnly="True" SortExpression="MZONE_PKG_ZONE_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_PKG_ID" HeaderText="MZONE_PKG_ID" SortExpression="MZONE_PKG_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Zone Name" SortExpression="MZONE_ID"><EditItemTemplate>
                <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="sdsZoneList" DataTextField="MZONE_TITLE"
                    DataValueField="MZONE_ID" SelectedValue='<%# Bind("MZONE_ID") %>'>
                </asp:DropDownList>
            
</EditItemTemplate>
<ItemTemplate>
                <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsZoneList" DataTextField="MZONE_TITLE"
                    DataValueField="MZONE_ID" Enabled="False" SelectedValue='<%# Bind("MZONE_ID") %>'>
                </asp:DropDownList>
            
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView>&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Zone in Package</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView2" runat="server" DataSourceID="sdsMZonePkgZoneID" DataKeyNames="MZONE_PKG_ZONE_ID" BorderColor="#E0E0E0" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert"><Fields>
<asp:BoundField DataField="MZONE_PKG_ZONE_ID" HeaderText="MZONE_PKG_ZONE_ID" ReadOnly="True" SortExpression="MZONE_PKG_ZONE_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_PKG_ID" HeaderText="MZONE_PKG_ID" SortExpression="MZONE_PKG_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Zone Name" SortExpression="MZONE_ID"><EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MZONE_ID") %>'></asp:TextBox>
                
</EditItemTemplate>
<InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="sdsZoneList" DataTextField="MZONE_TITLE"
                        DataValueField="MZONE_ID" SelectedValue='<%# Bind("MZONE_ID") %>'>
                    </asp:DropDownList>
                
</InsertItemTemplate>
<ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MZONE_ID") %>'></asp:Label>
                
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField InsertText="Add Zone" ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView> </DIV></TD></TR></TBODY></TABLE>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

