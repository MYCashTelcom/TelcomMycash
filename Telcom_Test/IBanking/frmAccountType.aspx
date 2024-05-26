<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountType.aspx.cs" Inherits="Forms_frmAccountType"
    Title="Manage Account Type" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Account Type List</span></strong></div>
            <div>
                <asp:SqlDataSource ID="sdsAccountType" runat="server" SelectCommand='SELECT "ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME", "TYPE_CODE" FROM "IB_ACCOUNT_TYPE" ORDER BY "ACCOUNT_TYPE_NAME"'
                    UpdateCommand='UPDATE "IB_ACCOUNT_TYPE" SET "ACCOUNT_TYPE_NAME" = :ACCOUNT_TYPE_NAME, "TYPE_CODE" = :TYPE_CODE WHERE "ACCOUNT_TYPE_ID" = :ACCOUNT_TYPE_ID'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "IB_ACCOUNT_TYPE" ("ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME", "TYPE_CODE") VALUES (:ACCOUNT_TYPE_ID, :ACCOUNT_TYPE_NAME, :TYPE_CODE)'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "IB_ACCOUNT_TYPE" WHERE "ACCOUNT_TYPE_ID" = :ACCOUNT_TYPE_ID'>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="ACCOUNT_TYPE_ID"></asp:Parameter>
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Type="String" Name="ACCOUNT_TYPE_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="TYPE_CODE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="ACCOUNT_TYPE_ID"></asp:Parameter>
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ACCOUNT_TYPE_ID" Type="String" />
                        <asp:Parameter Type="String" Name="ACCOUNT_TYPE_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="TYPE_CODE"></asp:Parameter>
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="ACCOUNT_TYPE_ID" DataSourceID="sdsAccountType">
                    <Columns>
                        <asp:BoundField DataField="ACCOUNT_TYPE_ID" HeaderText="ACCOUNT_TYPE_ID" ReadOnly="True"
                            SortExpression="ACCOUNT_TYPE_ID" Visible="False" />
                        <asp:BoundField DataField="ACCOUNT_TYPE_NAME" HeaderText="Type Name" SortExpression="ACCOUNT_TYPE_NAME" />
                        <asp:BoundField DataField="TYPE_CODE" HeaderText="Short Code" SortExpression="TYPE_CODE" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                &nbsp;</div>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Add New&nbsp;Account Type&nbsp;</span></strong></div>
            <div>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ACCOUNT_TYPE_ID"
                    DataSourceID="sdsAccountType" DefaultMode="Insert" Height="50px" Width="125px">
                    <Fields>
                        <asp:BoundField DataField="ACCOUNT_TYPE_NAME" HeaderText="Type Name" SortExpression="ACCOUNT_TYPE_NAME">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TYPE_CODE" HeaderText="Short Code" SortExpression="TYPE_CODE">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" InsertText="Add New Type" ShowInsertButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Fields>
                </asp:DetailsView>
                &nbsp;</div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
