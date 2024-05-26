<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVoucherGeneration.aspx.cs" Inherits="Forms_frmVoucherGeneration" Title="Voucher Generation" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Voucher Batches</SPAN></STRONG>&nbsp;</DIV><DIV><asp:SqlDataSource id="sdsVouDenomination" runat="server" DeleteCommand='DELETE FROM "VOUCHER_DENOMINATION" WHERE "VOU_DENOMINATION_ID" = :VOU_DENOMINATION_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "VOUCHER_DENOMINATION" ("VOU_DENOMINATION_ID", "VOU_DENOMINATION_VALUE", "VOU_DENOMINATION_TITLE") VALUES (:VOU_DENOMINATION_ID, :VOU_DENOMINATION_VALUE, :VOU_DENOMINATION_TITLE)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "VOUCHER_DENOMINATION" SET "VOU_DENOMINATION_VALUE" = :VOU_DENOMINATION_VALUE, "VOU_DENOMINATION_TITLE" = :VOU_DENOMINATION_TITLE WHERE "VOU_DENOMINATION_ID" = :VOU_DENOMINATION_ID' SelectCommand='SELECT "VOU_DENOMINATION_ID", "VOU_DENOMINATION_VALUE", "VOU_DENOMINATION_TITLE" FROM "VOUCHER_DENOMINATION"'><DeleteParameters>
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
</asp:SqlDataSource> <asp:SqlDataSource id="sdsVouBatch" runat="server" DeleteCommand='DELETE FROM "VOUCHER_BATCH_LIST" WHERE "VOU_BATCH_ID" = :VOU_BATCH_ID' ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "VOUCHER_BATCH_LIST" ("VOU_BATCH_ID", "VOU_BATCH_TITLE", "VOU_BATCH_QUANTITY", "VOU_BATCH_CREATION_DATE", "VOU_BATCH_EXPIRY_DATE", "VOU_BATCH_ACTIVATION_DATE", "VOU_BATCH_STATUS", "SYS_USER_ID_REQ", "SYS_USER_IS_ACT", "VOU_DENOMINATION_ID") VALUES (:VOU_BATCH_ID, :VOU_BATCH_TITLE, :VOU_BATCH_QUANTITY, :VOU_BATCH_CREATION_DATE, :VOU_BATCH_EXPIRY_DATE, :VOU_BATCH_ACTIVATION_DATE, :VOU_BATCH_STATUS, :SYS_USER_ID_REQ, :SYS_USER_IS_ACT, :VOU_DENOMINATION_ID)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "VOUCHER_BATCH_LIST" SET "VOU_BATCH_TITLE" = :VOU_BATCH_TITLE, "VOU_BATCH_QUANTITY" = :VOU_BATCH_QUANTITY, "VOU_BATCH_CREATION_DATE" = :VOU_BATCH_CREATION_DATE, "VOU_BATCH_EXPIRY_DATE" = :VOU_BATCH_EXPIRY_DATE, "VOU_BATCH_ACTIVATION_DATE" = :VOU_BATCH_ACTIVATION_DATE, "VOU_BATCH_STATUS" = :VOU_BATCH_STATUS, "SYS_USER_ID_REQ" = :SYS_USER_ID_REQ, "SYS_USER_IS_ACT" = :SYS_USER_IS_ACT, "VOU_DENOMINATION_ID" = :VOU_DENOMINATION_ID WHERE "VOU_BATCH_ID" = :VOU_BATCH_ID' SelectCommand='SELECT * FROM "VOUCHER_BATCH_LIST"'><DeleteParameters>
<asp:Parameter Type="String" Name="VOU_BATCH_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="VOU_BATCH_TITLE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="VOU_BATCH_QUANTITY"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_CREATION_DATE"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_EXPIRY_DATE"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_ACTIVATION_DATE"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_BATCH_STATUS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USER_ID_REQ"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USER_IS_ACT"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_BATCH_ID"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="VOU_BATCH_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_BATCH_TITLE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="VOU_BATCH_QUANTITY"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_CREATION_DATE"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_EXPIRY_DATE"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="VOU_BATCH_ACTIVATION_DATE"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_BATCH_STATUS"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USER_ID_REQ"></asp:Parameter>
<asp:Parameter Type="String" Name="SYS_USER_IS_ACT"></asp:Parameter>
<asp:Parameter Type="String" Name="VOU_DENOMINATION_ID"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource><asp:GridView id="gdwVoucherBatch" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="VOU_BATCH_ID" DataSourceID="sdsVouBatch" AllowPaging="True" BorderColor="White"><Columns>
<asp:BoundField ReadOnly="True" DataField="VOU_BATCH_ID" Visible="False" SortExpression="VOU_BATCH_ID" HeaderText="VOU_BATCH_ID"></asp:BoundField>
<asp:TemplateField SortExpression="VOU_DENOMINATION_ID" HeaderText="Denomination"><EditItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" DataSourceID="sdsVouDenomination" DataTextField="VOU_DENOMINATION_TITLE" DataValueField="VOU_DENOMINATION_ID" SelectedValue='<%# Bind("VOU_DENOMINATION_ID") %>'></asp:DropDownList>
</EditItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsVouDenomination" DataTextField="VOU_DENOMINATION_TITLE" DataValueField="VOU_DENOMINATION_ID" SelectedValue='<%# Bind("VOU_DENOMINATION_ID") %>'></asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="VOU_BATCH_TITLE" SortExpression="VOU_BATCH_TITLE" HeaderText="Batach Title">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_BATCH_QUANTITY" SortExpression="VOU_BATCH_QUANTITY" HeaderText="Batch Qauantity">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_BATCH_CREATION_DATE" SortExpression="VOU_BATCH_CREATION_DATE" HeaderText="Creation date">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_BATCH_EXPIRY_DATE" SortExpression="VOU_BATCH_EXPIRY_DATE" HeaderText="Expiry Date">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VOU_BATCH_ACTIVATION_DATE" SortExpression="VOU_BATCH_ACTIVATION_DATE" HeaderText="Activation Date">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="VOU_BATCH_STATUS" HeaderText="Status"><EditItemTemplate>
<asp:DropDownList id="DropDownList5" runat="server" SelectedValue='<%# Bind("VOU_BATCH_STATUS") %>'><asp:ListItem Selected="True" Value="I">Idle</asp:ListItem>
<asp:ListItem Value="A">Active</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList4" runat="server" SelectedValue='<%# Bind("VOU_BATCH_STATUS") %>'><asp:ListItem Selected="True" Value="I">Idle</asp:ListItem>
<asp:ListItem Value="A">Active</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField EditText=" Edit " ButtonType="Button" ShowEditButton="True"></asp:CommandField>
</Columns>
</asp:GridView></DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add&nbsp;New&nbsp;Batch&nbsp;</SPAN></STRONG></DIV><DIV><TABLE border="0" cellpadding="1" cellspacing="1" style="WIDTH: 141px; border-right: whitesmoke 1px solid; border-top: whitesmoke 1px solid; border-left: whitesmoke 1px solid; border-bottom: whitesmoke 1px solid;"><TBODY><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Denomination</STRONG></TD><TD style="WIDTH: 4px"><asp:DropDownList id="DropDownList1" runat="server" DataSourceID="sdsVouDenomination" DataTextField="VOU_DENOMINATION_TITLE" DataValueField="VOU_DENOMINATION_ID"></asp:DropDownList></TD></TR><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Batch Title</STRONG></TD><TD style="WIDTH: 4px"><asp:TextBox id="TextBox1" runat="server"></asp:TextBox></TD></TR><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Batch</STRONG> <STRONG>Quantity</STRONG></TD><TD style="WIDTH: 4px"><asp:TextBox id="TextBox2" runat="server"></asp:TextBox></TD></TR><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Creation Date</STRONG></TD><TD style="WIDTH: 4px"><asp:TextBox id="TextBox3" runat="server"></asp:TextBox></TD></TR><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Expiry Dayte</STRONG></TD><TD style="WIDTH: 4px"><asp:TextBox id="TextBox4" runat="server"></asp:TextBox></TD></TR><TR><TD style="TEXT-ALIGN: right" noWrap><STRONG>Activation Date</STRONG></TD><TD style="WIDTH: 4px"><asp:TextBox id="TextBox5" runat="server"></asp:TextBox></TD></TR><TR><TD colSpan=2></TD></TR><TR><TD style="TEXT-ALIGN: center" colSpan=2><asp:Button id="btnGenerateVou" runat="server" Text="Generate Voucher"></asp:Button></TD></TR></TBODY></TABLE></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>