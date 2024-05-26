<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDpsFdrScheme.aspx.cs"
    Inherits="Forms_frmDpsFdrScheme" Title="Manage DPS-FDR Scheme" %>

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
                <strong><span style="color: white">Scheme List</span></strong></div>
            <div>
                <asp:SqlDataSource ID="sdsSchemeList" runat="server" SelectCommand='SELECT "SCHEME_ID", "SCHEME_TITLE", "SCHEME_DESCRIPTION", "SCHEME_INTEREST_RATE", "SCHEME_DURATION" FROM "IB_DPS_FDR_SCHEME" ORDER BY "SCHEME_TITLE"'
                    UpdateCommand='UPDATE "IB_DPS_FDR_SCHEME" SET "SCHEME_TITLE" = :SCHEME_TITLE, "SCHEME_DESCRIPTION" = :SCHEME_DESCRIPTION, "SCHEME_INTEREST_RATE" = :SCHEME_INTEREST_RATE, "SCHEME_DURATION" = :SCHEME_DURATION WHERE "SCHEME_ID" = :SCHEME_ID'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "IB_DPS_FDR_SCHEME" ("SCHEME_ID", "SCHEME_TITLE", "SCHEME_DESCRIPTION", "SCHEME_INTEREST_RATE", "SCHEME_DURATION") VALUES (:SCHEME_ID, :SCHEME_TITLE, :SCHEME_DESCRIPTION, :SCHEME_INTEREST_RATE, :SCHEME_DURATION)'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "IB_DPS_FDR_SCHEME" WHERE "SCHEME_ID" = :SCHEME_ID'>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="SCHEME_ID"></asp:Parameter>
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Type="String" Name="SCHEME_TITLE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SCHEME_DESCRIPTION"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SCHEME_INTEREST_RATE"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="SCHEME_DURATION"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SCHEME_ID"></asp:Parameter>
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SCHEME_ID" Type="String" />
                        <asp:Parameter Type="String" Name="SCHEME_TITLE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SCHEME_DESCRIPTION"></asp:Parameter>
                        <asp:Parameter Type="String" Name="SCHEME_INTEREST_RATE"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="SCHEME_DURATION"></asp:Parameter>
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="SCHEME_ID" DataSourceID="sdsSchemeList">
                    <Columns>
                        <asp:BoundField ItemStyle-VerticalAlign="Top" DataField="SCHEME_ID" HeaderText="SCHEME_ID"
                            ReadOnly="True" SortExpression="SCHEME_ID" Visible="False" />
                        <asp:BoundField ItemStyle-VerticalAlign="Top" DataField="SCHEME_TITLE" HeaderText="Scheme Name"
                            SortExpression="SCHEME_TITLE" />
                        <asp:BoundField ItemStyle-VerticalAlign="Top" DataField="SCHEME_INTEREST_RATE" HeaderText="Interest Rate"
                            SortExpression="SCHEME_INTEREST_RATE" />
                        <asp:BoundField ItemStyle-VerticalAlign="Top" DataField="SCHEME_DURATION" HeaderText="Duration (Year)"
                            SortExpression="SCHEME_DURATION" />
                        <asp:TemplateField HeaderText="Scheme Description" SortExpression="SCHEME_DESCRIPTION">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="52px" Text='<%# Bind("SCHEME_DESCRIPTION") %>'
                                    TextMode="MultiLine" Width="232px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SCHEME_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                &nbsp;</div>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Add New&nbsp;Scheme&nbsp;</span></strong></div>
            <div>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="SCHEME_ID"
                    DataSourceID="sdsSchemeList" DefaultMode="Insert" Height="50px" Width="125px">
                    <Fields>
                        <asp:BoundField DataField="SCHEME_TITLE" HeaderText="Scheme Name" SortExpression="SCHEME_TITLE">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SCHEME_INTEREST_RATE" HeaderText="Interest Rate" SortExpression="SCHEME_INTEREST_RATE">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SCHEME_DURATION" HeaderText="Duration (Year)" SortExpression="SCHEME_DURATION">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-VerticalAlign="Top" HeaderText="Scheme Description"
                            SortExpression="SCHEME_DESCRIPTION">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SCHEME_DESCRIPTION") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox TextMode="MultiLine" ID="TextBox1" runat="server" Text='<%# Bind("SCHEME_DESCRIPTION") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SCHEME_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" InsertText="Add New Scheme" ShowInsertButton="True">
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
