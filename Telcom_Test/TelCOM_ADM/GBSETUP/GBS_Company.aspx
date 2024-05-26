<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GBS_Company.aspx.vb" Inherits="Forms_frmCompanyBranch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CompanyBranch</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Company Information</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsCompany" runat="server" SelectCommand='SELECT * FROM "CM_COMPANY"' UpdateCommand='UPDATE "CM_COMPANY" SET "COMPANY_NAME" = :COMPANY_NAME, "OFFICE_TYPE_ID" = :OFFICE_TYPE_ID, "ADDRESS1" = :ADDRESS1, "ADDRESS2" = :ADDRESS2, "CITY_ID" = :CITY_ID, "COUNTRY_ID" = :COUNTRY_ID, "FAX" = :FAX, "PHONE" = :PHONE, "TAX_NO" = :TAX_NO, "COMPANY_LOGO" = :COMPANY_LOGO, "ABBREVIATED_NAME" = :ABBREVIATED_NAME, "REG_NO" = :REG_NO WHERE "COMPANY_ID" = :COMPANY_ID' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" InsertCommand='INSERT INTO "CM_COMPANY" ("COMPANY_ID", "COMPANY_NAME", "OFFICE_TYPE_ID", "ADDRESS1", "ADDRESS2", "CITY_ID", "COUNTRY_ID", "FAX", "PHONE", "TAX_NO", "COMPANY_LOGO", "ABBREVIATED_NAME", "REG_NO") VALUES (:COMPANY_ID, :COMPANY_NAME, :OFFICE_TYPE_ID, :ADDRESS1, :ADDRESS2, :CITY_ID, :COUNTRY_ID, :FAX, :PHONE, :TAX_NO, :COMPANY_LOGO, :ABBREVIATED_NAME, :REG_NO)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "CM_COMPANY" WHERE "COMPANY_ID" = :COMPANY_ID'><DeleteParameters>
    <asp:Parameter Name="COMPANY_ID" Type="String" />
</DeleteParameters>
<UpdateParameters>
    <asp:Parameter Name="COMPANY_NAME" Type="String" />
    <asp:Parameter Name="OFFICE_TYPE_ID" Type="String" />
    <asp:Parameter Name="ADDRESS1" Type="String" />
    <asp:Parameter Name="ADDRESS2" Type="String" />
    <asp:Parameter Name="CITY_ID" Type="String" />
    <asp:Parameter Name="COUNTRY_ID" Type="String" />
    <asp:Parameter Name="FAX" Type="String" />
    <asp:Parameter Name="PHONE" Type="String" />
    <asp:Parameter Name="TAX_NO" Type="String" />
    <asp:Parameter Name="COMPANY_LOGO" Type="Object" />
    <asp:Parameter Name="ABBREVIATED_NAME" Type="String" />
    <asp:Parameter Name="REG_NO" Type="String" />
    <asp:Parameter Name="COMPANY_ID" Type="String" />
</UpdateParameters>
<InsertParameters>
    <asp:Parameter Name="COMPANY_ID" Type="String" />
    <asp:Parameter Name="COMPANY_NAME" Type="String" />
    <asp:Parameter Name="OFFICE_TYPE_ID" Type="String" />
    <asp:Parameter Name="ADDRESS1" Type="String" />
    <asp:Parameter Name="ADDRESS2" Type="String" />
    <asp:Parameter Name="CITY_ID" Type="String" />
    <asp:Parameter Name="COUNTRY_ID" Type="String" />
    <asp:Parameter Name="FAX" Type="String" />
    <asp:Parameter Name="PHONE" Type="String" />
    <asp:Parameter Name="TAX_NO" Type="String" />
    <asp:Parameter Name="COMPANY_LOGO" Type="Object" />
    <asp:Parameter Name="ABBREVIATED_NAME" Type="String" />
    <asp:Parameter Name="REG_NO" Type="String" />
</InsertParameters>
</asp:SqlDataSource> &nbsp;&nbsp;&nbsp;<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False"
        BorderColor="#E0E0E0" BorderStyle="None" DataKeyNames="COMPANY_ID" DataSourceID="sdsCompany"
        DefaultMode="Edit" Height="50px" Width="125px">
        <Fields>
            <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID" ReadOnly="True" SortExpression="COMPANY_ID"
                Visible="False" />
            <asp:TemplateField HeaderText="Company Name" SortExpression="COMPANY_NAME">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("COMPANY_NAME") %>' Width="400px"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("COMPANY_NAME") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("COMPANY_NAME") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address 1" SortExpression="ADDRESS1">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="43px" Text='<%# Bind("ADDRESS1") %>'
                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ADDRESS1") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ADDRESS1") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address 2" SortExpression="ADDRESS2">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Height="43px" Text='<%# Bind("ADDRESS2") %>'
                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ADDRESS2") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ADDRESS2") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City" SortExpression="CITY_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:BoundField DataField="FAX" HeaderText="Fax" SortExpression="FAX">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="TAX_NO" HeaderText="Tax No" SortExpression="TAX_NO">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="ABBREVIATED_NAME" HeaderText="Short Name" SortExpression="ABBREVIATED_NAME">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="REG_NO" HeaderText="Registration" SortExpression="REG_NO">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Fields>
    </asp:DetailsView>
</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
