<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_Sytem_Menu.aspx.cs" Inherits="System_SYS_Sytem_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>System Menu</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
    <table>
        <tr>
            <td style="width: 483px"><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">System Menu</SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource ID="sdsSysMenu" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "CM_SYSTEM_MENU" WHERE "SYS_MENU_ID" = :SYS_MENU_ID'
        InsertCommand='INSERT INTO CM_SYSTEM_MENU(SYS_MENU_ID, SYS_MENU_TITLE, SYS_MENU_FILE, SYS_MENU_PARENT, CMP_BRANCH_ID, SYS_MENU_TYPE, SYS_MENU_SERIAL) VALUES (:SYS_MENU_ID, :SYS_MENU_TITLE, :SYS_MENU_FILE, :SYS_MENU_PARENT, :CMP_BRANCH_ID, :SYS_MENU_TYPE, :SYS_MENU_SERIAL)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "CM_SYSTEM_MENU" ORDER BY SYS_MENU_SERIAL'
        UpdateCommand='UPDATE CM_SYSTEM_MENU SET SYS_MENU_ID = SYS_MENU_ID, SYS_MENU_TITLE = :SYS_MENU_TITLE, SYS_MENU_FILE = :SYS_MENU_FILE, SYS_MENU_PARENT = :SYS_MENU_PARENT, CMP_BRANCH_ID = :CMP_BRANCH_ID, SYS_MENU_TYPE = :SYS_MENU_TYPE, SYS_MENU_SERIAL = :SYS_MENU_SERIAL WHERE (SYS_MENU_ID = :SYS_MENU_ID)'>
        <DeleteParameters>
            <asp:Parameter Name="SYS_MENU_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="SYS_MENU_TITLE" Type="String" />
            <asp:Parameter Name="SYS_MENU_FILE" Type="String" />
            <asp:Parameter Name="SYS_MENU_PARENT" Type="String" />
            <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
            <asp:Parameter Name="SYS_MENU_TYPE" />
            <asp:Parameter Name="SYS_MENU_SERIAL" />
            <asp:Parameter Name="SYS_MENU_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SYS_MENU_ID" Type="String" />
            <asp:Parameter Name="SYS_MENU_TITLE" Type="String" />
            <asp:Parameter Name="SYS_MENU_FILE" Type="String" />
            <asp:Parameter Name="SYS_MENU_PARENT" Type="String" />
            <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
            <asp:Parameter Name="SYS_MENU_TYPE" />
            <asp:Parameter Name="SYS_MENU_SERIAL" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CMP_BRANCH_ID", "CMP_BRANCH_NAME" FROM "CM_CMP_BRANCH"'>
    </asp:SqlDataSource>
    <asp:GridView ID="grvSystemMenu" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="SYS_MENU_ID" DataSourceID="sdsSysMenu" BorderColor="#E0E0E0" AllowPaging="True" PageSize="15"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="SYS_MENU_ID" HeaderText="Menu ID" ReadOnly="True"
                SortExpression="SYS_MENU_ID" Visible="False" >
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="SYS_MENU_SERIAL" HeaderText="Order" SortExpression="SYS_MENU_SERIAL" />
            <asp:BoundField DataField="SYS_MENU_TITLE" HeaderText="Menu Title" SortExpression="SYS_MENU_TITLE" >
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="SYS_MENU_FILE" HeaderText="Menu File" SortExpression="SYS_MENU_FILE" >
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Parent Menu" SortExpression="SYS_MENU_PARENT">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsSysMenu" DataTextField="SYS_MENU_TITLE"
                        DataValueField="SYS_MENU_ID" SelectedValue='<%# Bind("SYS_MENU_PARENT") %>' Style="position: relative">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsSysMenu" DataTextField="SYS_MENU_TITLE"
                        DataValueField="SYS_MENU_ID" Enabled="False" SelectedValue='<%# Bind("SYS_MENU_PARENT") %>'
                        Style="position: relative">
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                        DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>' Style="position: relative">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                        DataValueField="CMP_BRANCH_ID" Enabled="False" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'
                        Style="position: relative">
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Menu Type" SortExpression="SYS_MENU_TYPE">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList6" runat="server" SelectedValue='<%# Bind("SYS_MENU_TYPE") %>'
                        Style="position: relative">
                        <asp:ListItem Selected="True" Value="RT">Root</asp:ListItem>
                        <asp:ListItem Value="GR">Group</asp:ListItem>
                        <asp:ListItem Value="MN">Menu</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList5" runat="server" Enabled="False" SelectedValue='<%# Bind("SYS_MENU_TYPE") %>'
                        Style="position: relative">
                        <asp:ListItem Selected="True" Value="RT">Root</asp:ListItem>
                        <asp:ListItem Value="GR">Group</asp:ListItem>
                        <asp:ListItem Value="MN">Menu</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" NextPageText="Next"
            PageButtonCount="7" PreviousPageText="Previous" />
        <FooterStyle BackColor="#FFC0C0" />
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
</DIV></td>
            <td> &nbsp;&nbsp;</td>
            
            <td valign="top"><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">New System Menu</SPAN></STRONG></DIV>
        <asp:DetailsView ID="dtvSystemMenu" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" BorderColor="#E0E0E0" DataKeyNames="SYS_MENU_ID" DataSourceID="sdsSysMenu" DefaultMode="Insert">
            <Fields>
                <asp:TemplateField HeaderText="SYS_MENU_ID" SortExpression="SYS_MENU_ID" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SYS_MENU_ID") %>'></asp:Label>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SYS_MENU_ID") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SYS_MENU_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SYS_MENU_SERIAL" HeaderText="Order" SortExpression="SYS_MENU_SERIAL">
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SYS_MENU_TITLE" HeaderText="Menu Title" SortExpression="SYS_MENU_TITLE" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="SYS_MENU_FILE" HeaderText="Menu File" SortExpression="SYS_MENU_FILE" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Parent Menu" SortExpression="SYS_MENU_PARENT">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList9" runat="server" DataSourceID="sdsSysMenu"
                            DataTextField="SYS_MENU_TITLE" DataValueField="SYS_MENU_ID" SelectedValue='<%# Bind("SYS_MENU_PARENT") %>'
                            Style="position: relative; left: 0px;">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList11" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                            DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>' Style="position: relative">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Menu Type" SortExpression="SYS_MENU_TYPE">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList10" runat="server" SelectedValue='<%# Bind("SYS_MENU_TYPE") %>'
                            Style="position: relative">
                            <asp:ListItem Selected="True" Value="RT">ROOT</asp:ListItem>
                            <asp:ListItem Value="GR">GROUP</asp:ListItem>
                            <asp:ListItem Value="MN">MENU</asp:ListItem>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" ButtonType="Button" >
                    <FooterStyle BackColor="#8080FF" BorderColor="Blue" BorderStyle="Solid" ForeColor="#FFC0C0" />
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:CommandField>
            </Fields>
            <EmptyDataTemplate>
                <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" DataSourceID="sdsBranch"
                    DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Eval("CMP_BRANCH_ID") %>'
                    Style="position: relative">
                </asp:DropDownList>
            </EmptyDataTemplate>
            <PagerTemplate>
                <asp:DropDownList ID="DropDownList8" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                    DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Eval("CMP_BRANCH_ID") %>' Style="position: relative">
                </asp:DropDownList>
            </PagerTemplate>
        </asp:DetailsView></td>
        </tr>
    </table>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
