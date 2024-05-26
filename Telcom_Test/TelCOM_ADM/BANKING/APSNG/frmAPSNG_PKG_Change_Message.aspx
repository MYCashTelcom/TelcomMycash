<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAPSNG_PKG_Change_Message.aspx.cs" Inherits="Forms_frmAPSNG_PKG_Change_Message" Title="Package Change Message" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Package&nbsp;Change&nbsp;Rules <asp:DropDownList id="ddlChangRule" runat="server" __designer:wfdid="w1" DataSourceID="sdsIN_Pkg_Chng_rule" DataTextField="APSNG_IN_PCRL_DESC" DataValueField="APSNG_IN_PCRL_ID" AutoPostBack="True"></asp:DropDownList></SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsIN_Pkg_Chng_rule" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "APSNG_IN_PCRL_ID", "APSNG_IN_PCRL_DESC", "APSNG_IN_PCRL_CODE" FROM "APSNG_IN_PKG_CRL"'></asp:SqlDataSource> <asp:SqlDataSource id="sdsIN_Pkg_Chng_Msg" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "APSNG_IN_PCRM_ID", "APSNG_IN_PCRL_ID", "APSNG_IN_PCRM_DAY", "APSNG_IN_PCRM_MSG", "APSNG_IN_SUB_LANG_ID", "APSNG_IN_PCRM_EVENT" FROM "APSNG_IN_PCRL_MESSAGES" WHERE ("APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID)' DeleteCommand='DELETE FROM "APSNG_IN_PCRL_MESSAGES" WHERE "APSNG_IN_PCRM_ID" = :APSNG_IN_PCRM_ID' InsertCommand='INSERT INTO "APSNG_IN_PCRL_MESSAGES" ("APSNG_IN_PCRM_ID", "APSNG_IN_PCRL_ID", "APSNG_IN_PCRM_DAY", "APSNG_IN_PCRM_MSG", "APSNG_IN_SUB_LANG_ID", "APSNG_IN_PCRM_EVENT") VALUES (:APSNG_IN_PCRM_ID, :APSNG_IN_PCRL_ID, :APSNG_IN_PCRM_DAY, :APSNG_IN_PCRM_MSG, :APSNG_IN_SUB_LANG_ID, :APSNG_IN_PCRM_EVENT)' UpdateCommand='UPDATE "APSNG_IN_PCRL_MESSAGES" SET "APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID, "APSNG_IN_PCRM_DAY" = :APSNG_IN_PCRM_DAY, "APSNG_IN_PCRM_MSG" = :APSNG_IN_PCRM_MSG, "APSNG_IN_SUB_LANG_ID" = :APSNG_IN_SUB_LANG_ID, "APSNG_IN_PCRM_EVENT" = :APSNG_IN_PCRM_EVENT WHERE "APSNG_IN_PCRM_ID" = :APSNG_IN_PCRM_ID' __designer:wfdid="w2"><SelectParameters>
<asp:ControlParameter ControlID="ddlChangRule" PropertyName="SelectedValue" Name="APSNG_IN_PCRL_ID" Type="String"></asp:ControlParameter>
</SelectParameters>
<DeleteParameters>
<asp:Parameter Name="APSNG_IN_PCRM_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_DAY" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_SUB_LANG_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_EVENT" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="APSNG_IN_PCRM_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_DAY" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_SUB_LANG_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRM_EVENT" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsLanguageList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "LANGUAGE_ID", "LANGUAGE_TITLE" FROM "LANGUAGE_LIST"' __designer:wfdid="w3"></asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" DataSourceID="sdsIN_Pkg_Chng_Msg" DataKeyNames="APSNG_IN_PCRM_ID" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#E0E0E0" Font-Size="11pt" AllowPaging="True" PageSize="5"><Columns>
<asp:BoundField DataField="APSNG_IN_PCRM_ID" HeaderText="APSNG_IN_PCRM_ID" ReadOnly="True" SortExpression="APSNG_IN_PCRM_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Rule Desc" SortExpression="APSNG_IN_PCRL_ID"><EditItemTemplate>
<asp:DropDownList id="DropDownList4" runat="server" DataSourceID="sdsIN_Pkg_Chng_rule" __designer:wfdid="w26" DataValueField="APSNG_IN_PCRL_ID" DataTextField="APSNG_IN_PCRL_DESC" SelectedValue='<%# Bind("APSNG_IN_PCRL_ID") %>'></asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList3" runat="server" DataSourceID="sdsIN_Pkg_Chng_rule" __designer:wfdid="w60" DataValueField="APSNG_IN_PCRL_ID" DataTextField="APSNG_IN_PCRL_DESC" SelectedValue='<%# Bind("APSNG_IN_PCRL_ID") %>' Enabled="False"></asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRM_DAY" HeaderText="Day" SortExpression="APSNG_IN_PCRM_DAY">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Event" SortExpression="APSNG_IN_PCRM_EVENT"><EditItemTemplate>
<asp:DropDownList id="DropDownList5" runat="server" __designer:wfdid="w55" SelectedValue='<%# Bind("APSNG_IN_PCRM_EVENT") %>'><asp:ListItem Selected="True" Value="ACTSU">Activation Successful</asp:ListItem>
<asp:ListItem Value="ACISF">Account Insufficient</asp:ListItem>
<asp:ListItem Value="MIGNA">Migration Not Allowed</asp:ListItem>
<asp:ListItem Value="REMIN">Reminder</asp:ListItem>
<asp:ListItem Value="RENSU">Renew Successful</asp:ListItem>
<asp:ListItem Value="RENUS">Renew Unsuccessful</asp:ListItem>
<asp:ListItem Value="EXPIR">Expired</asp:ListItem>
</asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList6" runat="server" __designer:wfdid="w54" SelectedValue='<%# Bind("APSNG_IN_PCRM_EVENT") %>' Enabled="False"><asp:ListItem Selected="True" Value="ACTSU">Activation Successful</asp:ListItem>
<asp:ListItem Value="ACISF">Account Insufficient</asp:ListItem>
<asp:ListItem Value="MIGNA">Migration Not Allowed</asp:ListItem>
<asp:ListItem Value="REMIN">Reminder</asp:ListItem>
<asp:ListItem Value="RENSU">Renew Successful</asp:ListItem>
<asp:ListItem Value="RENUS">Renew Unsuccessful</asp:ListItem>
<asp:ListItem Value="EXPIR">Expired</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Message" SortExpression="APSNG_IN_PCRM_MSG"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w62" Width="450px" Height="70px" Text='<%# Bind("APSNG_IN_PCRM_MSG") %>' TextMode="MultiLine"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:TextBox id="TextBox5" runat="server" __designer:wfdid="w61" Width="450px" Height="70px" Text='<%# Bind("APSNG_IN_PCRM_MSG") %>' TextMode="MultiLine"></asp:TextBox>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Language" SortExpression="APSNG_IN_SUB_LANG_ID"><EditItemTemplate>
<asp:DropDownList id="DropDownList8" runat="server" DataSourceID="sdsLanguageList" __designer:wfdid="w38" DataValueField="LANGUAGE_ID" DataTextField="LANGUAGE_TITLE" SelectedValue='<%# Bind("APSNG_IN_SUB_LANG_ID") %>'></asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:DropDownList id="DropDownList7" runat="server" DataSourceID="sdsLanguageList" __designer:wfdid="w37" DataValueField="LANGUAGE_ID" DataTextField="LANGUAGE_TITLE" SelectedValue='<%# Bind("APSNG_IN_SUB_LANG_ID") %>' Enabled="False"></asp:DropDownList>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField EditText=" Edit " ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"></asp:CommandField>
</Columns>
</asp:GridView> &nbsp; </DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Message</SPAN></STRONG></DIV><DIV><asp:DetailsView id="DetailsView1" runat="server" DataSourceID="sdsIN_Pkg_Chng_Msg" DataKeyNames="APSNG_IN_PCRM_ID" BorderColor="#E0E0E0" Font-Size="11pt" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
<asp:BoundField DataField="APSNG_IN_PCRM_ID" HeaderText="APSNG_IN_PCRM_ID" ReadOnly="True" SortExpression="APSNG_IN_PCRM_ID" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Rule Desc" SortExpression="APSNG_IN_PCRL_ID"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w6" Text='<%# Bind("APSNG_IN_PCRL_ID") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList1" runat="server" DataSourceID="sdsIN_Pkg_Chng_rule" __designer:wfdid="w8" DataValueField="APSNG_IN_PCRL_ID" DataTextField="APSNG_IN_PCRL_DESC" SelectedValue='<%# Bind("APSNG_IN_PCRL_ID") %>'></asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label1" runat="server" __designer:wfdid="w56" Text='<%# Bind("APSNG_IN_PCRL_ID") %>'></asp:Label> 
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Wrap="False" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:BoundField DataField="APSNG_IN_PCRM_DAY" HeaderText="Day" SortExpression="APSNG_IN_PCRM_DAY">
<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Event" SortExpression="APSNG_IN_PCRM_EVENT"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" __designer:wfdid="w58" Text='<%# Bind("APSNG_IN_PCRM_EVENT") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList9" runat="server" __designer:wfdid="w59" SelectedValue='<%# Bind("APSNG_IN_PCRM_EVENT") %>'><asp:ListItem Value="ACTSU">Activation Successful</asp:ListItem>
<asp:ListItem Value="ACISF">Account Insufficient</asp:ListItem>
<asp:ListItem Value="MIGNA">Migration Not Allowed</asp:ListItem>
<asp:ListItem Value="REMIN">Reminder</asp:ListItem>
<asp:ListItem Value="RENSU">Renew Successful</asp:ListItem>
<asp:ListItem Value="RENUS">Renew Unsuccessful</asp:ListItem>
<asp:ListItem Value="EXPIR">Expired</asp:ListItem>
</asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label3" runat="server" __designer:wfdid="w57" Text='<%# Bind("APSNG_IN_PCRM_EVENT") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Message" SortExpression="APSNG_IN_PCRM_MSG"><EditItemTemplate>
<asp:TextBox id="TextBox4" runat="server" __designer:wfdid="w47" Text='<%# Bind("APSNG_IN_PCRM_MSG") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w48" Width="450px" Height="100px" Text='<%# Bind("APSNG_IN_PCRM_MSG") %>' TextMode="MultiLine"></asp:TextBox>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label4" runat="server" __designer:wfdid="w46" Text='<%# Bind("APSNG_IN_PCRM_MSG") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Language" SortExpression="APSNG_IN_SUB_LANG_ID"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" __designer:wfdid="w20" Text='<%# Bind("APSNG_IN_SUB_LANG_ID") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsLanguageList" __designer:wfdid="w13" DataValueField="LANGUAGE_ID" DataTextField="LANGUAGE_TITLE" SelectedValue='<%# Bind("APSNG_IN_SUB_LANG_ID") %>'></asp:DropDownList>
</InsertItemTemplate>
<ItemTemplate>
<asp:Label id="Label2" runat="server" __designer:wfdid="w19" Text='<%# Bind("APSNG_IN_SUB_LANG_ID") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:TemplateField>
<asp:CommandField InsertText="Add Message" ShowInsertButton="True" ButtonType="Button">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView>&nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

