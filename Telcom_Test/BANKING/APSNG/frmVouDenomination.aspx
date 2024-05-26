<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVouDenomination.aspx.cs" Inherits="Forms_frmVouDenomination" Title="Voucher Denimination" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Service Type</SPAN></STRONG>&nbsp;</DIV><DIV><asp:SqlDataSource id="sdsVouDenomination" runat="server" SelectCommand='SELECT "VOU_DENOMINATION_ID", "VOU_DENOMINATION_VALUE", "VOU_DENOMINATION_TITLE" FROM "VOUCHER_DENOMINATION"' UpdateCommand='UPDATE "VOUCHER_DENOMINATION" SET "VOU_DENOMINATION_VALUE" = :VOU_DENOMINATION_VALUE, "VOU_DENOMINATION_TITLE" = :VOU_DENOMINATION_TITLE WHERE "VOU_DENOMINATION_ID" = :VOU_DENOMINATION_ID' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "VOUCHER_DENOMINATION" ("VOU_DENOMINATION_ID", "VOU_DENOMINATION_VALUE", "VOU_DENOMINATION_TITLE") VALUES (:VOU_DENOMINATION_ID, :VOU_DENOMINATION_VALUE, :VOU_DENOMINATION_TITLE)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "VOUCHER_DENOMINATION" WHERE "VOU_DENOMINATION_ID" = :VOU_DENOMINATION_ID'><DeleteParameters>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="Decimal" Name="VOU_DENOMINATION_VALUE"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_ID"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_ID"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="VOU_DENOMINATION_VALUE"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_TITLE"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="GridView1" runat="server" DataSourceID="sdsVouDenomination" DataKeyNames="VOU_DENOMINATION_ID" BorderColor="White" AutoGenerateColumns="False" AllowSorting="True"><Columns>
<asp:BoundField ReadOnly="True" DataField="VOU_DENOMINATION_ID" Visible="False" SortExpression="VOU_DENOMINATION_ID" HeaderText="VOU_DENOMINATION_ID"></asp:BoundField>
<asp:BoundField DataField="VOU_DENOMINATION_TITLE" SortExpression="VOU_DENOMINATION_TITLE" HeaderText="Denomination Title">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_DENOMINATION_VALUE" SortExpression="VOU_DENOMINATION_TITLE" HeaderText="Face Value">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:CommandField ShowDeleteButton="True" EditText=" Edit " ButtonType="Button" ShowEditButton="True"></asp:CommandField>
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Service Type&nbsp;</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsVouDenomination" DataKeyNames="VOU_DENOMINATION_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
<asp:BoundField ReadOnly="True" DataField="VOU_DENOMINATION_ID" Visible="False" SortExpression="VOU_DENOMINATION_ID" HeaderText="VOU_DENOMINATION_ID"></asp:BoundField>
<asp:BoundField DataField="VOU_DENOMINATION_TITLE" SortExpression="VOU_DENOMINATION_TITLE" HeaderText="Denomination title">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_DENOMINATION_VALUE" SortExpression="VOU_DENOMINATION_VALUE" HeaderText="Face Value">
<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:CommandField InsertText="Add Denomination" ButtonType="Button" ShowInsertButton="True">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

