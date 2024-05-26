<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZoneCB_Message.aspx.cs" Inherits="Forms_frmMZoneCB_Message" Title="Manage Mobile Zone CB Message" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Manage Mobile Zone CB Message</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsMZoneList" runat="server" DeleteCommand='DELETE FROM "MZONE_LIST" WHERE "MZONE_ID" = :MZONE_ID' InsertCommand='INSERT INTO "MZONE_LIST" ("MZONE_ID", "MZONE_TITLE", "MZONE_SEND_CBC", "MZONE_CBC_MESSAGE") VALUES (:MZONE_ID, :MZONE_TITLE, :MZONE_SEND_CBC, :MZONE_CBC_MESSAGE)' UpdateCommand='UPDATE "MZONE_LIST" SET "MZONE_CBC_MESSAGE" = :MZONE_CBC_MESSAGE WHERE "MZONE_ID" = :MZONE_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "MZONE_ID", "MZONE_TITLE", "MZONE_SEND_CBC", "MZONE_CBC_MESSAGE" FROM "MZONE_LIST"'><DeleteParameters>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="MZONE_CBC_MESSAGE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="MZONE_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_TITLE" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_SEND_CBC" Type="String"></asp:Parameter>
<asp:Parameter Name="MZONE_CBC_MESSAGE" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource><asp:GridView id="GridView1" runat="server" __designer:wfdid="w121" DataSourceID="sdsMZoneList" DataKeyNames="MZONE_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt"><Columns>
<asp:BoundField DataField="MZONE_ID" HeaderText="MZONE_ID" ReadOnly="True" SortExpression="MZONE_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MZONE_TITLE" HeaderText="Zone Name" ReadOnly="True" SortExpression="MZONE_TITLE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Send Rate Through CBC" SortExpression="MZONE_SEND_CBC"><EditItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" __designer:wfdid="w133" SelectedValue='<%# Bind("MZONE_SEND_CBC") %>' Enabled="False"><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList> 
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" __designer:wfdid="w132" SelectedValue='<%# Bind("MZONE_SEND_CBC") %>' Enabled="False"><asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="CB Message" SortExpression="MZONE_CBC_MESSAGE"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Width="450px" Height="80px" __designer:wfdid="w130" Text='<%# Bind("MZONE_CBC_MESSAGE") %>' TextMode="MultiLine"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Width="450px" Height="80px" __designer:wfdid="w129" Text='<%# Bind("MZONE_CBC_MESSAGE") %>' TextMode="MultiLine"></asp:TextBox>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView>&nbsp;&nbsp;&nbsp;&nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

