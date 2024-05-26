<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZoneList.aspx.cs" Inherits="Forms_frmMZoneList" Title="Manage Mobile Zone List" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Zone List</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsMZoneList" runat="server" DeleteCommand='DELETE FROM "MZONE_LIST" WHERE "MZONE_ID" = :MZONE_ID' InsertCommand='INSERT INTO "MZONE_LIST" ("MZONE_ID", "MZONE_TITLE", "MZONE_SEND_CBC", "MZONE_DISCNT_TYPE", "MZONE_DISCNT_VOICE", "MZONE_DISCNT_SMS", "MZONE_DISCNT_MMS", "MZONE_DISCNT_DATA", "MZONE_RATE_INTERVAL", "MZONE_RATE_SLAB", "MZONE_IS_FOR_ALLCELL", "MZONE_STATIC_RATE") VALUES (:MZONE_ID, :MZONE_TITLE, :MZONE_SEND_CBC, :MZONE_DISCNT_TYPE, :MZONE_DISCNT_VOICE, :MZONE_DISCNT_SMS, :MZONE_DISCNT_MMS, :MZONE_DISCNT_DATA, :MZONE_RATE_INTERVAL, :MZONE_RATE_SLAB, :MZONE_IS_FOR_ALLCELL, :MZONE_STATIC_RATE)' UpdateCommand='UPDATE "MZONE_LIST" SET "MZONE_TITLE" = :MZONE_TITLE, "MZONE_SEND_CBC" = :MZONE_SEND_CBC, "MZONE_DISCNT_TYPE" = :MZONE_DISCNT_TYPE, "MZONE_DISCNT_VOICE" = :MZONE_DISCNT_VOICE, "MZONE_DISCNT_SMS" = :MZONE_DISCNT_SMS, "MZONE_DISCNT_MMS" = :MZONE_DISCNT_MMS, "MZONE_DISCNT_DATA" = :MZONE_DISCNT_DATA, "MZONE_RATE_INTERVAL" = :MZONE_RATE_INTERVAL, "MZONE_RATE_SLAB" = :MZONE_RATE_SLAB, "MZONE_IS_FOR_ALLCELL" = :MZONE_IS_FOR_ALLCELL, "MZONE_STATIC_RATE" = :MZONE_STATIC_RATE WHERE "MZONE_ID" = :MZONE_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "MZONE_ID", "MZONE_TITLE", "MZONE_SEND_CBC", "MZONE_DISCNT_TYPE", "MZONE_DISCNT_VOICE", "MZONE_DISCNT_SMS", "MZONE_DISCNT_MMS", "MZONE_DISCNT_DATA", "MZONE_RATE_INTERVAL", "MZONE_RATE_SLAB", "MZONE_IS_FOR_ALLCELL", "MZONE_STATIC_RATE" FROM "MZONE_LIST"'><DeleteParameters>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="MZONE_TITLE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_SEND_CBC" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_TYPE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_VOICE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_SMS" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_MMS" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_DATA" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_RATE_INTERVAL" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_RATE_SLAB" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_IS_FOR_ALLCELL" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_STATIC_RATE" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_TITLE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_SEND_CBC" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_TYPE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_VOICE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_SMS" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_MMS" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_DISCNT_DATA" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_RATE_INTERVAL" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_RATE_SLAB" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="MZONE_IS_FOR_ALLCELL" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_STATIC_RATE" Type="Decimal"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsMZoneList" DataKeyNames="MZONE_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt"><Columns>
<asp:BoundField DataField="MZONE_ID" HeaderText="MZONE_ID" ReadOnly="True" SortExpression="MZONE_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_TITLE" HeaderText="Zone Name" SortExpression="MZONE_TITLE">
<ItemStyle Wrap="False"></ItemStyle>
    <HeaderStyle HorizontalAlign="Center" />
</asp:BoundField>
    <asp:TemplateField HeaderText="Send Rate Through CBC" SortExpression="MZONE_SEND_CBC">
        <EditItemTemplate>
            <asp:DropDownList ID="DropDownList17" runat="server" SelectedValue='<%# Bind("MZONE_SEND_CBC") %>'>
                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:DropDownList ID="DropDownList16" runat="server" Enabled="False" SelectedValue='<%# Bind("MZONE_SEND_CBC") %>'>
                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" />
    </asp:TemplateField>
<asp:TemplateField HeaderText="Discount Type" SortExpression="MZONE_DISCNT_TYPE"><EditItemTemplate>
<asp:DropDownList id="DropDownList11" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_TYPE") %>'><asp:ListItem Selected="True" Value="D">Dynamic</asp:ListItem>
<asp:ListItem Value="S">Static</asp:ListItem>
<asp:ListItem Value="H">Hybrid</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList7" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_TYPE") %>' Enabled="False"><asp:ListItem Selected="True" Value="D">Dynamic</asp:ListItem>
<asp:ListItem Value="S">Static</asp:ListItem>
<asp:ListItem Value="H">Hybrid</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="MZONE_STATIC_RATE" HeaderText="Static Discount Rate" SortExpression="MZONE_STATIC_RATE"></asp:BoundField>
<asp:TemplateField HeaderText="Voice Discount for" SortExpression="MZONE_DISCNT_VOICE"><EditItemTemplate>
<asp:DropDownList id="DropDownList9" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_VOICE") %>'><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList8" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_VOICE") %>' Enabled="False"><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="SMS Discount For" SortExpression="MZONE_DISCNT_SMS"><EditItemTemplate>
<asp:DropDownList id="DropDownList10" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_SMS") %>'><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_SMS") %>' Enabled="False"><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="MMS Discount For" SortExpression="MZONE_DISCNT_MMS"><EditItemTemplate>
<asp:DropDownList id="DropDownList13" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_MMS") %>'><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList12" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_MMS") %>' Enabled="False"><asp:ListItem Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Data Discount" SortExpression="MZONE_DISCNT_DATA"><EditItemTemplate>
<asp:DropDownList id="DropDownList6" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_DATA") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList5" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_DATA") %>' Enabled="False"><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="MZONE_RATE_INTERVAL" HeaderText="Rate Refresh Interval (m)" SortExpression="MZONE_RATE_INTERVAL"></asp:BoundField>
<asp:BoundField DataField="MZONE_RATE_SLAB" HeaderText="Rate Slab" SortExpression="MZONE_RATE_SLAB"></asp:BoundField>
<asp:TemplateField HeaderText="Zone for All Cells" SortExpression="MZONE_IS_FOR_ALLCELL"><EditItemTemplate>
    &nbsp;<asp:DropDownList ID="DropDownList15" runat="server" SelectedValue='<%# Bind("MZONE_IS_FOR_ALLCELL") %>'>
        <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N">No</asp:ListItem>
    </asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
    &nbsp;<asp:DropDownList ID="DropDownList14" runat="server" Enabled="False" SelectedValue='<%# Bind("MZONE_IS_FOR_ALLCELL") %>'>
        <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N">No</asp:ListItem>
    </asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView>
    &nbsp;
</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Zone</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsMZoneList" DataKeyNames="MZONE_ID" BorderColor="#E0E0E0" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert" Font-Size="11pt"><Fields>
<asp:BoundField DataField="MZONE_ID" HeaderText="MZONE_ID" ReadOnly="True" SortExpression="MZONE_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_TITLE" HeaderText="Zone Name" SortExpression="MZONE_TITLE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
    <asp:TemplateField HeaderText="Send rate Through CBC" SortExpression="MZONE_SEND_CBC">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("MZONE_SEND_CBC") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="DropDownList18" runat="server" SelectedValue='<%# Bind("MZONE_SEND_CBC") %>'>
                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label7" runat="server" Text='<%# Bind("MZONE_SEND_CBC") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
<asp:TemplateField HeaderText="Discount Type" SortExpression="MZONE_DISCNT_TYPE"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("MZONE_DISCNT_TYPE") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_TYPE") %>'><asp:ListItem Value="D">Dynamic</asp:ListItem>
<asp:ListItem Value="S">Static</asp:ListItem>
<asp:ListItem Value="H">Hybrid</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label2" runat="server" Text='<%# Bind("MZONE_DISCNT_TYPE") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="MZONE_STATIC_RATE" HeaderText="Static Discount rate" SortExpression="MZONE_STATIC_RATE">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Voice Discount For" SortExpression="MZONE_DISCNT_VOICE"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("MZONE_DISCNT_VOICE") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_VOICE") %>'><asp:ListItem Selected="True" Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList> 
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label3" runat="server" Text='<%# Bind("MZONE_DISCNT_VOICE") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="SMS Discount For" SortExpression="MZONE_DISCNT_SMS"><EditItemTemplate>
<asp:TextBox id="TextBox4" runat="server" Text='<%# Bind("MZONE_DISCNT_SMS") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList31" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_SMS") %>'><asp:ListItem Selected="True" Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label4" runat="server" Text='<%# Bind("MZONE_DISCNT_SMS") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="MMS Discount For" SortExpression="MZONE_DISCNT_MMS"><EditItemTemplate>
<asp:TextBox id="TextBox5" runat="server" Text='<%# Bind("MZONE_DISCNT_MMS") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList41" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_MMS") %>'><asp:ListItem Selected="True" Value="111">All</asp:ListItem>
<asp:ListItem Value="100">Only On-Net</asp:ListItem>
<asp:ListItem Value="010">Only Off-Net</asp:ListItem>
<asp:ListItem Value="001">Only International</asp:ListItem>
<asp:ListItem Value="110">On-Net + Off-Net</asp:ListItem>
<asp:ListItem Value="101">On-Net+Internation</asp:ListItem>
<asp:ListItem Value="011">Off-Net+International</asp:ListItem>
<asp:ListItem Value="000">None</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label5" runat="server" Text='<%# Bind("MZONE_DISCNT_MMS") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Data Discount" SortExpression="MZONE_DISCNT_DATA"><EditItemTemplate>
<asp:TextBox id="TextBox6" runat="server" Text='<%# Bind("MZONE_DISCNT_DATA") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" SelectedValue='<%# Bind("MZONE_DISCNT_DATA") %>'><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label6" runat="server" Text='<%# Bind("MZONE_DISCNT_DATA") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="MZONE_RATE_INTERVAL" HeaderText="Rate Refresh Interval (m)" SortExpression="MZONE_RATE_INTERVAL">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MZONE_RATE_SLAB" HeaderText="Rate Slab" SortExpression="MZONE_RATE_SLAB">
<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Zone for All Cells" SortExpression="MZONE_IS_FOR_ALLCELL"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("MZONE_IS_FOR_ALLCELL") %>'></asp:TextBox> 
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList4" runat="server" SelectedValue='<%# Bind("MZONE_IS_FOR_ALLCELL") %>'><asp:ListItem Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList> 
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("MZONE_IS_FOR_ALLCELL") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowInsertButton="True" ButtonType="Button" InsertText="Add Zone">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView>&nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>