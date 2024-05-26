<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSISubject.aspx.cs" Inherits="Forms_frmSISubject"
    Title="Manage Standing Instruction Subjects" %>

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
                <strong><span style="color: white">Standing Instruction List</span></strong></div>
            <div>
                <asp:SqlDataSource ID="sdsSISubject" runat="server" SelectCommand='SELECT "SI_SUBJECT_ID", "SI_SUBJECT_TITLE", "SI_SUBJECT_DESCRIPTION" FROM "IB_STAND_INSTRUC_SUBJECT" ORDER BY "SI_SUBJECT_TITLE"'
                    UpdateCommand='UPDATE "IB_STAND_INSTRUC_SUBJECT" SET "SI_SUBJECT_TITLE" = :SI_SUBJECT_TITLE, "SI_SUBJECT_DESCRIPTION" = :SI_SUBJECT_DESCRIPTION WHERE "SI_SUBJECT_ID" = :SI_SUBJECT_ID'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "IB_STAND_INSTRUC_SUBJECT" ("SI_SUBJECT_ID", "SI_SUBJECT_TITLE", "SI_SUBJECT_DESCRIPTION") VALUES (:SI_SUBJECT_ID, :SI_SUBJECT_TITLE, :SI_SUBJECT_DESCRIPTION)'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "IB_STAND_INSTRUC_SUBJECT" WHERE "SI_SUBJECT_ID" = :SI_SUBJECT_ID'>
                    <DeleteParameters>
                        <asp:Parameter Name="SI_SUBJECT_ID" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SI_SUBJECT_TITLE" Type="String" />
                        <asp:Parameter Name="SI_SUBJECT_DESCRIPTION" Type="String" />
                        <asp:Parameter Name="SI_SUBJECT_ID" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SI_SUBJECT_ID" Type="String" />
                        <asp:Parameter Name="SI_SUBJECT_TITLE" Type="String" />
                        <asp:Parameter Name="SI_SUBJECT_DESCRIPTION" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="SI_SUBJECT_ID" DataSourceID="sdsSISubject">
                    <Columns>
                        <asp:BoundField DataField="SI_SUBJECT_ID" HeaderText="SI_SUBJECT_ID" ReadOnly="True"
                            SortExpression="SI_SUBJECT_ID" Visible="False" />
                        <asp:BoundField ItemStyle-VerticalAlign="Top" DataField="SI_SUBJECT_TITLE" HeaderText="Subject Title" SortExpression="SI_SUBJECT_TITLE" />
                        <asp:TemplateField HeaderText="Subject Description" SortExpression="SI_SUBJECT_DESCRIPTION">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="52px" Text='<%# Bind("SI_SUBJECT_DESCRIPTION") %>'
                                    TextMode="MultiLine" Width="232px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SI_SUBJECT_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                &nbsp;</div>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Add New&nbsp;Subject&nbsp;</span></strong></div>
            <div>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="SI_SUBJECT_ID"
                    DataSourceID="sdsSISubject" DefaultMode="Insert" Height="50px" Width="125px">
                    <Fields>
                        <asp:BoundField DataField="SI_SUBJECT_TITLE" HeaderText="Subject Title" SortExpression="SI_SUBJECT_TITLE">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-VerticalAlign="Top" HeaderText="Subject Description" SortExpression="SI_SUBJECT_DESCRIPTION">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SI_SUBJECT_DESCRIPTION") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server" Text='<%# Bind("SI_SUBJECT_DESCRIPTION") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SI_SUBJECT_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" InsertText="Add New Subject" ShowInsertButton="True">
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
