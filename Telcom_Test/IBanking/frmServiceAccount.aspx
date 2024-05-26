<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServiceAccount.aspx.cs"
    Inherits="Forms_frmServiceAccount" Title="Manage Utility Service Accounts" %>

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
                <strong><span style="color: white">Service Account List</span></strong></div>
            <div>
                <asp:SqlDataSource ID="sdsServiceAccount" runat="server" SelectCommand='SELECT "SERVICE_ID", "SERVICE_ACCOUNT_NAME", "SERVICE_SHORT_CODE", "SERVICE_ACCOUNT_NO" FROM "IB_UTILITY_ACCOUNT" ORDER BY "SERVICE_ACCOUNT_NAME"'
                    UpdateCommand='UPDATE "IB_UTILITY_ACCOUNT" SET "SERVICE_ACCOUNT_NAME" = :SERVICE_ACCOUNT_NAME, "SERVICE_SHORT_CODE" = :SERVICE_SHORT_CODE, "SERVICE_ACCOUNT_NO" = :SERVICE_ACCOUNT_NO WHERE "SERVICE_ID" = :SERVICE_ID'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "IB_UTILITY_ACCOUNT" ("SERVICE_ID", "SERVICE_ACCOUNT_NAME", "SERVICE_SHORT_CODE", "SERVICE_ACCOUNT_NO") VALUES (:SERVICE_ID, :SERVICE_ACCOUNT_NAME, :SERVICE_SHORT_CODE, :SERVICE_ACCOUNT_NO)'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "IB_UTILITY_ACCOUNT" WHERE "SERVICE_ID" = :SERVICE_ID'>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="SERVICE_ID"></asp:Parameter>
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Type="String" Name="SERVICE_ACCOUNT_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SERVICE_SHORT_CODE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SERVICE_ACCOUNT_NO"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SERVICE_ID"></asp:Parameter>
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SERVICE_ID" Type="String" />
                        <asp:Parameter Type="String" Name="SERVICE_ACCOUNT_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SERVICE_SHORT_CODE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SERVICE_ACCOUNT_NO"></asp:Parameter>
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="SERVICE_ID" DataSourceID="sdsServiceAccount">
                    <Columns>
                        <asp:BoundField DataField="SERVICE_ID" HeaderText="SERVICE_ID" ReadOnly="True" SortExpression="SERVICE_ID"
                            Visible="False" />
                        <asp:BoundField DataField="SERVICE_ACCOUNT_NAME" HeaderText="Account Name" SortExpression="SERVICE_ACCOUNT_NAME" />
                        <asp:BoundField DataField="SERVICE_SHORT_CODE" HeaderText="Short Code" SortExpression="SERVICE_SHORT_CODE" />
                        <asp:BoundField DataField="SERVICE_ACCOUNT_NO" HeaderText="Account Number" SortExpression="SERVICE_ACCOUNT_NO" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                &nbsp;</div>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Add New Service&nbsp;Account&nbsp;</span></strong></div>
            <div>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="SERVICE_ID"
                    DataSourceID="sdsServiceAccount" DefaultMode="Insert" Height="50px" Width="125px">
                    <Fields>
                        <asp:BoundField DataField="SERVICE_ACCOUNT_NAME" HeaderText="Account Name" SortExpression="SERVICE_ACCOUNT_NAME">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SERVICE_SHORT_CODE" HeaderText="Short Code" SortExpression="SERVICE_SHORT_CODE">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SERVICE_ACCOUNT_NO" HeaderText="Account Number" SortExpression="SERVICE_ACCOUNT_NO">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" InsertText="Add New Account" ShowInsertButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Fields>
                </asp:DetailsView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
