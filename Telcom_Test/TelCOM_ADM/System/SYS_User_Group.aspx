<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_User_Group.aspx.cs" Inherits="System_SYS_User_Group" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Group</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<div style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">System User Group</SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource ID="sdsSysUsrGroup" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "CM_SYSTEM_USER_GROUP" WHERE "SYS_USR_GRP_ID" = :original_SYS_USR_GRP_ID AND (("SYS_USR_GRP_TITLE" = :original_SYS_USR_GRP_TITLE) OR ("SYS_USR_GRP_TITLE" IS NULL AND :original_SYS_USR_GRP_TITLE IS NULL)) AND (("SYS_USR_GRP_PARENT" = :original_SYS_USR_GRP_PARENT) OR ("SYS_USR_GRP_PARENT" IS NULL AND :original_SYS_USR_GRP_PARENT IS NULL)) AND (("SYS_USR_GRP_TYPE" = :original_SYS_USR_GRP_TYPE) OR ("SYS_USR_GRP_TYPE" IS NULL AND :original_SYS_USR_GRP_TYPE IS NULL)) AND (("CMP_BRANCH_ID" = :original_CMP_BRANCH_ID) OR ("CMP_BRANCH_ID" IS NULL AND :original_CMP_BRANCH_ID IS NULL))'
        InsertCommand='INSERT INTO "CM_SYSTEM_USER_GROUP" ("SYS_USR_GRP_ID", "SYS_USR_GRP_TITLE", "SYS_USR_GRP_PARENT", "SYS_USR_GRP_TYPE", "CMP_BRANCH_ID") VALUES (:SYS_USR_GRP_ID, :SYS_USR_GRP_TITLE, :SYS_USR_GRP_PARENT, :SYS_USR_GRP_TYPE, :CMP_BRANCH_ID)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "CM_SYSTEM_USER_GROUP"'
        UpdateCommand='UPDATE "CM_SYSTEM_USER_GROUP" SET "SYS_USR_GRP_TITLE" = :SYS_USR_GRP_TITLE, "SYS_USR_GRP_PARENT" = :SYS_USR_GRP_PARENT, "SYS_USR_GRP_TYPE" = :SYS_USR_GRP_TYPE, "CMP_BRANCH_ID" = :CMP_BRANCH_ID WHERE "SYS_USR_GRP_ID" = :original_SYS_USR_GRP_ID AND (("SYS_USR_GRP_TITLE" = :original_SYS_USR_GRP_TITLE) OR ("SYS_USR_GRP_TITLE" IS NULL AND :original_SYS_USR_GRP_TITLE IS NULL)) AND (("SYS_USR_GRP_PARENT" = :original_SYS_USR_GRP_PARENT) OR ("SYS_USR_GRP_PARENT" IS NULL AND :original_SYS_USR_GRP_PARENT IS NULL)) AND (("SYS_USR_GRP_TYPE" = :original_SYS_USR_GRP_TYPE) OR ("SYS_USR_GRP_TYPE" IS NULL AND :original_SYS_USR_GRP_TYPE IS NULL)) AND (("CMP_BRANCH_ID" = :original_CMP_BRANCH_ID) OR ("CMP_BRANCH_ID" IS NULL AND :original_CMP_BRANCH_ID IS NULL))' ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="original_SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TITLE" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_PARENT" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TYPE" Type="String" />
            <asp:Parameter Name="original_CMP_BRANCH_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="SYS_USR_GRP_TITLE" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_PARENT" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_TYPE" Type="String" />
            <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TITLE" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_PARENT" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TYPE" Type="String" />
            <asp:Parameter Name="original_CMP_BRANCH_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_TITLE" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_PARENT" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_TYPE" Type="String" />
            <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CMP_BRANCH_ID", "CMP_BRANCH_NAME" FROM "CM_CMP_BRANCH"'>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsGroupParent" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "SYS_USR_GRP_ID", "SYS_USR_GRP_TITLE" FROM "CM_SYSTEM_USER_GROUP"' OldValuesParameterFormatString="original_{0}">
       
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsGroupType" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "CM_SYSTEM_USER_GROUP" WHERE "SYS_USR_GRP_ID" = :original_SYS_USR_GRP_ID AND (("SYS_USR_GRP_TYPE" = :original_SYS_USR_GRP_TYPE) OR ("SYS_USR_GRP_TYPE" IS NULL AND :original_SYS_USR_GRP_TYPE IS NULL))' InsertCommand='INSERT INTO "CM_SYSTEM_USER_GROUP" ("SYS_USR_GRP_ID", "SYS_USR_GRP_TYPE") VALUES (:SYS_USR_GRP_ID, :SYS_USR_GRP_TYPE)' OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "SYS_USR_GRP_ID", "SYS_USR_GRP_TYPE" FROM "CM_SYSTEM_USER_GROUP"' UpdateCommand='UPDATE "CM_SYSTEM_USER_GROUP" SET "SYS_USR_GRP_TYPE" = :SYS_USR_GRP_TYPE WHERE "SYS_USR_GRP_ID" = :original_SYS_USR_GRP_ID AND (("SYS_USR_GRP_TYPE" = :original_SYS_USR_GRP_TYPE) OR ("SYS_USR_GRP_TYPE" IS NULL AND :original_SYS_USR_GRP_TYPE IS NULL))'>
        <DeleteParameters>
            <asp:Parameter Name="original_SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TYPE" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="SYS_USR_GRP_TYPE" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="original_SYS_USR_GRP_TYPE" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SYS_USR_GRP_ID" Type="String" />
            <asp:Parameter Name="SYS_USR_GRP_TYPE" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="gdvSysUsrGroup" runat="server"  AutoGenerateColumns="False"
        DataKeyNames="SYS_USR_GRP_ID" DataSourceID="sdsSysUsrGroup" 
            BorderColor="#E0E0E0" AllowPaging="True"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            onrowdeleted="gdvSysUsrGroup_RowDeleted" 
            onrowupdated="gdvSysUsrGroup_RowUpdated">
        <Columns>
            <asp:BoundField DataField="SYS_USR_GRP_ID" HeaderText=" Group ID" ReadOnly="True"
                SortExpression="SYS_USR_GRP_ID" Visible="False" />
            <asp:BoundField DataField="SYS_USR_GRP_TITLE" HeaderText="Group Title" SortExpression="SYS_USR_GRP_TITLE" />
            <asp:TemplateField HeaderText="Parent Group" SortExpression="SYS_USR_GRP_PARENT">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsGroupParent"
                        DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID" SelectedValue='<%# Bind("SYS_USR_GRP_PARENT") %>'
                        Style="position: relative">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsGroupParent"
                        DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID" SelectedValue='<%# Bind("SYS_USR_GRP_PARENT") %>'
                        Style="position: relative" Enabled="False">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group Type" SortExpression="SYS_USR_GRP_TYPE">
                <EditItemTemplate>
                    &nbsp;<asp:DropDownList ID="DropDownList5" runat="server" SelectedValue='<%# Bind("SYS_USR_GRP_TYPE") %>'
                        Style="position: relative; left: -2px;" Width="107px">
                        <asp:ListItem Selected="True" Value="GH">Group Header</asp:ListItem>
                        <asp:ListItem Value="AG">Active Group</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("SYS_USR_GRP_TYPE") %>'
                        Style="position: relative" Enabled="False">
                        <asp:ListItem Selected="True" Value="GH">Group Header</asp:ListItem>
                        <asp:ListItem Value="AG">Active Group</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                        DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>' Style="left: 0px;
                        position: relative; top: 0px" Width="90px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="dtdGdvBranch" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                        DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>' Style="left: -1px;
                        position: relative; top: 0px" Enabled="False">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
</div>
<div style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">New System User Group</SPAN></STRONG></DIV>
        <asp:DetailsView ID="dtvSysUsrGroup" runat="server" Height="50px" Width="125px" 
            AutoGenerateRows="False" BorderColor="#E0E0E0" DataKeyNames="SYS_USER_GRP_ID" 
            DataSourceID="sdsSysUsrGroup" DefaultMode="Insert" 
            oniteminserted="dtvSysUsrGroup_ItemInserted">
            <Fields>
                <asp:BoundField DataField="SYS_USR_GRP_TITLE" HeaderText="Group Title" SortExpression="SYS_USR_GRP_TITLE" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Group Parent" SortExpression="SYS_USR_GRP_PARENT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SYS_USR_GRP_PARENT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="sdsGroupParent"
                            DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID" SelectedValue='<%# Bind("SYS_USR_GRP_PARENT") %>'
                            Style="position: relative">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("SYS_USR_GRP_PARENT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group Type" SortExpression="SYS_USR_GRP_TYPE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SYS_USR_GRP_TYPE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate><asp:DropDownList ID="DropDownList10" runat="server" SelectedValue='<%# Bind("SYS_USR_GRP_TYPE") %>'
                        Style="position: relative">
                        <asp:ListItem Value="GH">Group Header</asp:ListItem>
                        <asp:ListItem Value="AG">Active Group</asp:ListItem>
                    </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SYS_USR_GRP_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch" SortExpression="CMP_BRANCH_ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CMP_BRANCH_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlDtvBranch" runat="server" DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME"
                            DataValueField="CMP_BRANCH_ID" SelectedValue='<%# Bind("CMP_BRANCH_ID") %>' Style="position: relative">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CMP_BRANCH_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowInsertButton="True" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:CommandField>
            </Fields>
        </asp:DetailsView>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
