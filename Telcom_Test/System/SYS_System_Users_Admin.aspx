<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_System_Users_Admin.aspx.cs" Inherits="System_SYS_System_Users_Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>System Users</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">

        <asp:SqlDataSource ID="sdsSysUsr" runat="server"
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            DeleteCommand='DELETE FROM CM_SYSTEM_USERS WHERE (SYS_USR_ID = :SYS_USR_ID)'
            InsertCommand='INSERT INTO CM_SYSTEM_USERS(SYS_USR_GRP_ID, SYS_USR_DNAME, SYS_USR_LOGIN_NAME, SYS_USR_PASS, SYS_USR_EMAIL, CMP_BRANCH_ID,ACCNT_ID) VALUES (:SYS_USR_GRP_ID, :SYS_USR_DNAME, :SYS_USR_LOGIN_NAME, :SYS_USR_PASS, :SYS_USR_EMAIL, :CMP_BRANCH_ID,:ACCNT_ID)'
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand='SELECT * FROM "CM_SYSTEM_USERS"'
            UpdateCommand='UPDATE CM_SYSTEM_USERS SET SYS_USR_GRP_ID = :SYS_USR_GRP_ID, SYS_USR_DNAME = :SYS_USR_DNAME, SYS_USR_LOGIN_NAME = :SYS_USR_LOGIN_NAME, ACCNT_ID = :ACCNT_ID, CMP_BRANCH_ID = :CMP_BRANCH_ID WHERE (SYS_USR_ID = :SYS_USR_ID)'>
            <DeleteParameters>
                <asp:Parameter Name="SYS_USR_ID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="SYS_USR_GRP_ID" Type="String" />
                <asp:Parameter Name="SYS_USR_DNAME" Type="String" />
                <asp:Parameter Name="SYS_USR_LOGIN_NAME" Type="String" />
                <%--<asp:Parameter Name="SYS_USR_PASS" Type="String" />--%>
                <asp:Parameter Name="ACCNT_ID" Type="String" />
                <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
                <asp:Parameter Name="SYS_USR_ID" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="SYS_USR_GRP_ID" Type="String" />
                <asp:Parameter Name="SYS_USR_DNAME" Type="String" />
                <asp:Parameter Name="SYS_USR_LOGIN_NAME" Type="String" />
                <asp:Parameter Name="SYS_USR_PASS" Type="String" />
                <asp:Parameter Name="SYS_USR_EMAIL" Type="String" />
                <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
                <asp:Parameter Name="ACCNT_ID" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsUsrGrp" runat="server"
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand='SELECT "SYS_USR_GRP_ID", "SYS_USR_GRP_TITLE" FROM "CM_SYSTEM_USER_GROUP"'></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsBranch" runat="server"
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH WHERE (CMP_BRANCH_STATUS = 'A')"></asp:SqlDataSource>


        <table width="100%">
            <tr>
                <td>
                    <asp:Button ID="btnSystemUser" runat="server" ForeColor="Black" Text="System User" Width="120px"
                        CausesValidation="False" class="tabbutton" OnClick="btnSystemUser_Click" />

                    <asp:Button ID="btnUserAccountId" runat="server" ForeColor="Black" Text="User Account ID" Width="120px" CausesValidation="False" class="tabbutton" OnClick="btnUserAccountId_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="vwSystemUser" runat="server">
                            <div style="background-color: royalblue">
                                <strong>
                                    <span style="color: white">System Users
                                    </span>
                                </strong>
                            </div>
                            <div>
                                <asp:GridView ID="gdvSysUsr" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataKeyNames="SYS_USR_ID" DataSourceID="sdsSysUsr"
                                    BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:BoundField DataField="SYS_USR_ID" HeaderText="User ID" ReadOnly="True" SortExpression="SYS_USR_ID" />
                                        <asp:BoundField DataField="SYS_USR_DNAME" HeaderText="User Name" SortExpression="SYS_USR_DNAME" />
                                        <asp:BoundField DataField="SYS_USR_LOGIN_NAME" HeaderText="Login Name" SortExpression="SYS_USR_LOGIN_NAME" />
                                        <%--<asp:TemplateField HeaderText="Password"  SortExpression="SYS_USR_PASS">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SYS_USR_PASS") %>' 
                                            Width="90px" Enabled="false" TextMode="Password">
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server"  Text='<%# Bind("SYS_USR_PASS") %>'
                                         Width="90px" Enabled="false" TextMode="Password">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                        <asp:BoundField DataField="ACCNT_ID" HeaderText="Account ID" SortExpression="ACCNT_ID" />
                                        <asp:TemplateField HeaderText="Group" SortExpression="SYS_USR_GRP_ID">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsUsrGrp"
                                                    DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID"
                                                    SelectedValue='<%# Bind("SYS_USR_GRP_ID") %>'
                                                    Style="position: relative">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsUsrGrp"
                                                    DataTextField="SYS_USR_GRP_TITLE"
                                                    DataValueField="SYS_USR_GRP_ID" SelectedValue='<%# Bind("SYS_USR_GRP_ID") %>'
                                                    Style="position: relative" Enabled="False">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsBranch"
                                                    DataTextField="CMP_BRANCH_NAME"
                                                    DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsBranch"
                                                    DataTextField="CMP_BRANCH_NAME"
                                                    DataValueField="CMP_BRANCH_ID" Enabled="False" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </div>
                            <div style="background-color: royalblue">
                                <strong>
                                    <span style="color: white">New System User &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </span>
                                </strong>
                            </div>
                            <div>
                                <asp:DetailsView ID="dtvSysUsr" runat="server" Height="90px" Width="250px" AutoGenerateRows="False"
                                    DataKeyNames="SYS_USR_ID" DataSourceID="sdsSysUsr" DefaultMode="Insert"
                                    BorderColor="#E0E0E0" ForeColor="Black"
                                    OnItemInserting="dtvSysUsr_ItemInserting">
                                    <Fields>
                                        <asp:BoundField DataField="SYS_USR_ID" HeaderText="User ID" ReadOnly="True" SortExpression="SYS_USR_ID"
                                            Visible="False" />
                                        <asp:BoundField DataField="SYS_USR_DNAME" HeaderText="User Name" SortExpression="SYS_USR_DNAME">
                                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SYS_USR_LOGIN_NAME" HeaderText="Login Name" SortExpression="SYS_USR_LOGIN_NAME">
                                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Password" SortExpression="SYS_USR_PASS">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SYS_USR_PASS") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SYS_USR_PASS") %>' TextMode="Password"></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("SYS_USR_PASS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SYS_USR_EMAIL" HeaderText="Email" SortExpression="SYS_USR_EMAIL">
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Account ID" SortExpression="ACCNT_ID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="dtvAccntID" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group" SortExpression="SYS_USR_GRP_ID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SYS_USR_GRP_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsUsrGrp" DataTextField="SYS_USR_GRP_TITLE"
                                                    DataValueField="SYS_USR_GRP_ID" SelectedValue='<%# Bind("SYS_USR_GRP_ID") %>'
                                                    Style="position: relative">
                                                </asp:DropDownList>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SYS_USR_GRP_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CMP_BRANCH_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                                                    DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>'>
                                                </asp:DropDownList>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("CMP_BRANCH_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </asp:View>
                        <asp:View ID="vwUserAccountId" runat="server">
                            <div style="background-color: royalblue">
                                <strong>
                                    <span style="color: white; font-weight: bold;">System Account ID
                                    </span>
                                </strong>
                            </div>
                            <div>
                                <asp:Label ID="lblWalletID" runat="server" Text="Wallet ID"></asp:Label>&nbsp;
                   <asp:TextBox ID="txtAccountNo" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblAccountID" runat="server" Text=""></asp:Label>
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

