<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmClient_Zone.aspx.cs" Inherits="COMMON_frmClient_Zone" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Manage 
    Channel Zone Area</SPAN></STRONG>&nbsp;</DIV>
    <DIV><asp:SqlDataSource id="sdsBranch" runat="server" 
            DeleteCommand='DELETE FROM "ACCOUNT_GROUP_LIST" WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            InsertCommand='INSERT INTO "ACCOUNT_GROUP_LIST" ("ACCNT_GROUP_ID", "ACCNT_GROUP_NAME", "ACCNT_ID", "ACCNT_GROUP_CODE") VALUES (:ACCNT_GROUP_ID, :ACCNT_GROUP_NAME, :ACCNT_ID, :ACCNT_GROUP_CODE)' 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "ACCOUNT_GROUP_LIST" SET "ACCNT_GROUP_NAME" = :ACCNT_GROUP_NAME, "ACCNT_CREATION_DATE" = :ACCNT_CREATION_DATE, "ACCNT_ID" = :ACCNT_ID, "ACCNT_GROUP_CODE" = :ACCNT_GROUP_CODE WHERE "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID' 
    
            SelectCommand='SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME, CMP_BRANCH_TYPE_ID, ADDRESS1, ADDRESS2, CITY_ID, COUNTRY_ID, FAX, PHONE, TAX_NO, ABBREVIATED_NAME, REG_NO, IS_OPERATING, CMP_BRANCH_STATUS FROM CM_CMP_BRANCH'><DeleteParameters>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="ACCNT_GROUP_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_CREATION_DATE" Type="DateTime"></asp:Parameter>
<asp:Parameter Name="ACCNT_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_CODE" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="ACCNT_GROUP_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_NAME" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="ACCNT_GROUP_CODE" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> 
<asp:SqlDataSource id="sdsCleintZone" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand='SELECT * FROM "CLIENT_ZONE" ORDER BY "CLI_ZONE_ID"' 
            DeleteCommand='DELETE FROM "CLIENT_ZONE" WHERE "CLI_ZONE_ID" = :CLI_ZONE_ID' 
            InsertCommand='INSERT INTO "CLIENT_ZONE" ("CLI_ZONE_TITLE", "CLI_ZONE_PARENT", "CLI_ZONE_TYPE", "CMP_BRANCH_ID") VALUES (:CLI_ZONE_TITLE, :CLI_ZONE_PARENT, :CLI_ZONE_TYPE, :CMP_BRANCH_ID)' 
            UpdateCommand='UPDATE "CLIENT_ZONE" SET "CLI_ZONE_TITLE" = :CLI_ZONE_TITLE, "CLI_ZONE_PARENT" = :CLI_ZONE_PARENT, "CLI_ZONE_TYPE" = :CLI_ZONE_TYPE, "CMP_BRANCH_ID" = :CMP_BRANCH_ID WHERE "CLI_ZONE_ID" = :CLI_ZONE_ID'>
    <DeleteParameters>
        <asp:Parameter Name="CLI_ZONE_ID" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="CLI_ZONE_TITLE" Type="String" />
        <asp:Parameter Name="CLI_ZONE_PARENT" Type="String" />
        <asp:Parameter Name="CLI_ZONE_TYPE" Type="String" />
        <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
        <asp:Parameter Name="CLI_ZONE_ID" Type="String" />
    </UpdateParameters>
    <InsertParameters>        
        <asp:Parameter Name="CLI_ZONE_TITLE" Type="String" />
        <asp:Parameter Name="CLI_ZONE_PARENT" Type="String" />
        <asp:Parameter Name="CLI_ZONE_TYPE" Type="String" />
        <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
    </InsertParameters>
        </asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsParentZone" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT CLI_ZONE_ID, CLI_ZONE_TITLE FROM CLIENT_ZONE WHERE (CLI_ZONE_TYPE = 'ZON') ORDER BY CLI_ZONE_ID">
        </asp:SqlDataSource>
        <asp:GridView id="GridView1" runat="server" 
            AllowSorting="True" AutoGenerateColumns="False" BorderColor="White" 
            DataKeyNames="CLI_ZONE_ID" DataSourceID="sdsCleintZone"
CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            AllowPaging="True"><Columns>
<asp:BoundField DataField="CLI_ZONE_ID" HeaderText="Zone ID" ReadOnly="True" 
                    SortExpression="CLI_ZONE_ID"></asp:BoundField>
<asp:BoundField DataField="CLI_ZONE_TITLE" HeaderText="Zone Name" 
                    SortExpression="CLI_ZONE_TITLE"></asp:BoundField>
                <asp:TemplateField HeaderText="Parent Zone" SortExpression="CLI_ZONE_PARENT">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList9" runat="server" 
                            DataSourceID="sdsParentZone" DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("CLI_ZONE_PARENT") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList10" runat="server" 
                            DataSourceID="sdsParentZone" DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID" Enabled="False" 
                            SelectedValue='<%# Bind("CLI_ZONE_PARENT") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" SortExpression="CLI_ZONE_TYPE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            SelectedValue='<%# Bind("CLI_ZONE_TYPE") %>'>
                            <asp:ListItem Value="ZON">Zone</asp:ListItem>
                            <asp:ListItem Value="ARE">Area</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" Enabled="False" 
                            SelectedValue='<%# Bind("CLI_ZONE_TYPE") %>'>
                            <asp:ListItem Value="ZON">Zone</asp:ListItem>
                            <asp:ListItem Value="ARE">Area</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsBranch" 
                            DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" 
                            SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsBranch" 
                            DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" Enabled="False" 
                            SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Group&nbsp;</SPAN></STRONG></DIV><DIV>
        <asp:DetailsView id="dlvServiceType" runat="server" BorderColor="Silver" 
            DataKeyNames="CLI_ZONE_ID" DataSourceID="sdsCleintZone" Height="50px" 
            Width="125px" AutoGenerateRows="False" DefaultMode="Insert" 
            Font-Names="Times New Roman" Font-Size="11pt"><Fields>
<asp:BoundField DataField="CLI_ZONE_TITLE" HeaderText="Zone Name" 
                    SortExpression="CLI_ZONE_TITLE">
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Parent Zone" SortExpression="CLI_ZONE_PARENT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CLI_ZONE_PARENT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList11" runat="server" 
                            DataSourceID="sdsParentZone" DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("CLI_ZONE_PARENT") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CLI_ZONE_PARENT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" SortExpression="CLI_ZONE_TYPE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList5" runat="server" 
                            SelectedValue='<%# Bind("CLI_ZONE_TYPE") %>'>
                            <asp:ListItem Value="ZON">Zone</asp:ListItem>
                            <asp:ListItem Value="ARE">Area</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList6" runat="server" Enabled="False" 
                            SelectedValue='<%# Bind("CLI_ZONE_TYPE") %>'>
                            <asp:ListItem Value="ZON">Zone</asp:ListItem>
                            <asp:ListItem Value="ARE">Area</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="sdsBranch" 
                            DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" 
                            SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList8" runat="server" DataSourceID="sdsBranch" 
                            DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" Enabled="False" 
                            SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" ButtonType="Button" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
