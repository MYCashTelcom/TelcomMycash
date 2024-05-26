<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngServiceType.aspx.cs" Inherits="Forms_frmServiceType" Title="Manage Service Type" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Service Type</SPAN></STRONG>&nbsp;</DIV>
<DIV>
<asp:SqlDataSource id="sdsSserviceType" runat="server" 
DeleteCommand='DELETE FROM "SERVICE_TYPE" WHERE "SERVICE_TYPE_ID" = :SERVICE_TYPE_ID' 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
InsertCommand='INSERT INTO "SERVICE_TYPE" ("SERVICE_TYPE_ID", "SERVICE_TYPE_NAME", "SERVICE_TYPE_DETAIL","SERVICE_TYPE_CODE") VALUES (:SERVICE_TYPE_ID, :SERVICE_TYPE_NAME, :SERVICE_TYPE_DETAIL,:SERVICE_TYPE_CODE)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
UpdateCommand='UPDATE "SERVICE_TYPE" SET "SERVICE_TYPE_NAME" = :SERVICE_TYPE_NAME, "SERVICE_TYPE_DETAIL" = :SERVICE_TYPE_DETAIL WHERE "SERVICE_TYPE_ID" = :SERVICE_TYPE_ID' SelectCommand='SELECT "SERVICE_TYPE_ID", "SERVICE_TYPE_NAME", "SERVICE_TYPE_DETAIL","SERVICE_TYPE_CODE" FROM "SERVICE_TYPE"'><DeleteParameters>
    <asp:Parameter Name="SERVICE_TYPE_ID" Type="String" />
</DeleteParameters>
<UpdateParameters>
    <asp:Parameter Name="SERVICE_TYPE_NAME" Type="String" />
    <asp:Parameter Name="SERVICE_TYPE_DETAIL" Type="String" />
    <asp:Parameter Name="SERVICE_TYPE_ID" Type="String" />
</UpdateParameters>
<InsertParameters>
    <asp:Parameter Name="SERVICE_TYPE_ID" Type="String" />
    <asp:Parameter Name="SERVICE_TYPE_NAME" Type="String" />
    <asp:Parameter Name="SERVICE_TYPE_DETAIL" Type="String" />
    <asp:Parameter Name="SERVICE_TYPE_CODE" Type="String" />
</InsertParameters>
</asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="SERVICE_TYPE_ID" DataSourceID="sdsSserviceType"
                        BorderColor="#E0E0E0" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" onrowupdated="GridView1_RowUpdated">
        <Columns>
            <asp:BoundField DataField="SERVICE_TYPE_ID" HeaderText="SERVICE_TYPE_ID" ReadOnly="True"
                SortExpression="SERVICE_TYPE_ID" Visible="False" />
            <asp:BoundField DataField="SERVICE_TYPE_NAME" HeaderText="Service Type" SortExpression="SERVICE_TYPE_NAME">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="SERVICE_TYPE_CODE" HeaderText="Internal Code" SortExpression="SERVICE_TYPE_CODE" ReadOnly="True">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Service Type Detail" SortExpression="SERVICE_TYPE_DETAIL">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="81px" Text='<%# Bind("SERVICE_TYPE_DETAIL") %>'
                        TextMode="MultiLine" Width="376px"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SERVICE_TYPE_DETAIL") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField EditText=" Edit " ShowEditButton="True" />
        </Columns>        
    </asp:GridView>
    <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Service Type&nbsp;</SPAN></STRONG></DIV><DIV>
    <asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsSserviceType" 
            BorderColor="Silver" Height="50px" Width="125px" AutoGenerateRows="False" 
            DefaultMode="Insert" Font-Names="Times New Roman" Font-Size="11pt" 
            oniteminserted="dlvServiceType_ItemInserted"><Fields>
<asp:BoundField ReadOnly="True" DataField="SERVICE_TYPE_ID" Visible="False" SortExpression="SERVICE_TYPE_ID" HeaderText="SERVICE_TYPE_ID"></asp:BoundField>
<asp:BoundField DataField="SERVICE_TYPE_NAME" SortExpression="SERVICE_TYPE_NAME" HeaderText="Type Caption">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_TYPE_CODE" SortExpression="SERVICE_TYPE_CODE" HeaderText=" Internal Code">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SERVICE_TYPE_DETAIL" HeaderText="Type Detail"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SERVICE_TYPE_DETAIL") %>'></asp:TextBox>
</EditItemTemplate>
<InsertItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Height="110px" Width="362px" TextMode="MultiLine"></asp:TextBox>
</InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("SERVICE_TYPE_DETAIL") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField InsertText=" Insert " ButtonType="Button" ShowInsertButton="True">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
