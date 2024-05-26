<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GBS_Cmp_Branch.aspx.cs" Inherits="GBS_Cmp_Branch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CompanyBranch</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Branch List</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsCompany" runat="server" SelectCommand='SELECT * FROM "CM_COMPANY"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsCmpBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM CM_CMP_BRANCH WHERE (CMP_BRANCH_ID = :CMP_BRANCH_ID)' 
        InsertCommand='INSERT INTO CM_CMP_BRANCH(CMP_BRANCH_NAME, CMP_BRANCH_TYPE_ID, ADDRESS1, ADDRESS2, CITY_ID, COUNTRY_ID, FAX, PHONE, TAX_NO, ABBREVIATED_NAME, REG_NO) VALUES 
                                                (:CMP_BRANCH_NAME, :CMP_BRANCH_TYPE_ID, :ADDRESS1, :ADDRESS2, :CITY_ID, :COUNTRY_ID, :FAX, :PHONE, :TAX_NO, :ABBREVIATED_NAME, :REG_NO)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME, CMP_BRANCH_TYPE_ID, ADDRESS1, ADDRESS2, CITY_ID, COUNTRY_ID, FAX, PHONE, TAX_NO, ABBREVIATED_NAME, REG_NO FROM CM_CMP_BRANCH'
        UpdateCommand='UPDATE CM_CMP_BRANCH SET CMP_BRANCH_NAME = :CMP_BRANCH_NAME, CMP_BRANCH_TYPE_ID = :CMP_BRANCH_TYPE_ID, ADDRESS1 = :ADDRESS1, ADDRESS2 = :ADDRESS2, CITY_ID = :CITY_ID, COUNTRY_ID = :COUNTRY_ID, FAX = :FAX, PHONE = :PHONE, TAX_NO = :TAX_NO, ABBREVIATED_NAME = :ABBREVIATED_NAME, REG_NO = :REG_NO WHERE (CMP_BRANCH_ID = :CMP_BRANCH_ID)'>
        <DeleteParameters>
            <asp:Parameter Name="CMP_BRANCH_ID" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="CMP_BRANCH_NAME" />
            <asp:Parameter Name="CMP_BRANCH_TYPE_ID" />
            <asp:Parameter Name="ADDRESS1" Type="String" />
            <asp:Parameter Name="ADDRESS2" Type="String" />
            <asp:Parameter Name="CITY_ID" Type="String" />
            <asp:Parameter Name="COUNTRY_ID" Type="String" />
            <asp:Parameter Name="FAX" Type="String" />
            <asp:Parameter Name="PHONE" Type="String" />
            <asp:Parameter Name="TAX_NO" Type="String" />
            <asp:Parameter Name="ABBREVIATED_NAME" Type="String" />
            <asp:Parameter Name="REG_NO" Type="String" />
            <asp:Parameter Name="CMP_BRANCH_ID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CMP_BRANCH_NAME" />
            <asp:Parameter Name="CMP_BRANCH_TYPE_ID" />
            <asp:Parameter Name="ADDRESS1" Type="String" />
            <asp:Parameter Name="ADDRESS2" Type="String" />
            <asp:Parameter Name="CITY_ID" Type="String" />
            <asp:Parameter Name="COUNTRY_ID" Type="String" />
            <asp:Parameter Name="FAX" Type="String" />
            <asp:Parameter Name="PHONE" Type="String" />
            <asp:Parameter Name="TAX_NO" Type="String" />
            <asp:Parameter Name="ABBREVIATED_NAME" Type="String" />
            <asp:Parameter Name="REG_NO" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCountry" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "CM_COUNTRY_ID", "CM_COUNTRY_NAME" FROM "CM_COUNTRY"'>
    </asp:SqlDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False"
        BorderColor="#E0E0E0" BorderStyle="None" DataKeyNames="COMPANY_ID" DataSourceID="sdsCompany"
        DefaultMode="Edit" Height="50px" Width="125px">
        <Fields>
            <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID" ReadOnly="True" SortExpression="COMPANY_ID"
                Visible="False" />
            <asp:TemplateField HeaderText="Company Name" SortExpression="COMPANY_NAME">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("COMPANY_NAME") %>' Width="400px" Enabled="False"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("COMPANY_NAME") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("COMPANY_NAME") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsCmpBranch" BorderColor="#E0E0E0" DataKeyNames="CMP_BRANCH_ID"
    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="CMP_BRANCH_ID" HeaderText="CMP_BRANCH_ID" SortExpression="CMP_BRANCH_ID"
                Visible="False" />
            <asp:BoundField DataField="CMP_BRANCH_NAME" HeaderText="Branch" SortExpression="CMP_BRANCH_NAME" />
            <asp:TemplateField HeaderText="Branch Type" SortExpression="CMP_BRANCH_TYPE_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("CMP_BRANCH_TYPE_ID") %>'>
                        <asp:ListItem Selected="True" Value="H">Head Office</asp:ListItem>
                        <asp:ListItem Value="B">Branch</asp:ListItem>
                        <asp:ListItem Value="A">All Branch</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" Enabled="False" SelectedValue='<%# Bind("CMP_BRANCH_TYPE_ID") %>'>
                        <asp:ListItem Selected="True" Value="H">Head Office</asp:ListItem>
                        <asp:ListItem Value="B">Branch</asp:ListItem>
                        <asp:ListItem Value="A">All Branch</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ADDRESS1" HeaderText="Address 1" SortExpression="ADDRESS1" />
            <asp:BoundField DataField="ADDRESS2" HeaderText="Address 2" SortExpression="ADDRESS2" />
            <asp:BoundField DataField="CITY_ID" HeaderText="City" SortExpression="CITY_ID" />
            <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsCountry" DataTextField="CM_COUNTRY_NAME"
                        DataValueField="CM_COUNTRY_ID" SelectedValue='<%# Bind("COUNTRY_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsCountry" DataTextField="CM_COUNTRY_NAME"
                        DataValueField="CM_COUNTRY_ID" Enabled="False" SelectedValue='<%# Bind("COUNTRY_ID") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FAX" HeaderText="Fax" SortExpression="FAX" />
            <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" />
            <asp:BoundField DataField="TAX_NO" HeaderText="Tax No" SortExpression="TAX_NO" />
            <asp:BoundField DataField="ABBREVIATED_NAME" HeaderText="Short Name" SortExpression="ABBREVIATED_NAME" />
            <asp:BoundField DataField="REG_NO" HeaderText="Registration No" SortExpression="REG_NO" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <br />
</DIV>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New Branch</SPAN></STRONG></DIV>
<div>
    <asp:DetailsView ID="dtvNewBranch" runat="server" AutoGenerateRows="False" DataKeyNames="BRANCH_ID"
        DataSourceID="sdsCmpBranch" Height="50px" Width="125px" BorderColor="#E0E0E0" DefaultMode="Insert">
        <Fields>
            <asp:BoundField DataField="CMP_BRANCH_NAME" HeaderText="Branch" SortExpression="CMP_BRANCH_NAME" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Branch Type" SortExpression="BRANCH_TYPE_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CMP_BRANCH_TYPE_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("CMP_BRANCH_TYPE_ID") %>'>
                        <asp:ListItem Value="H">Head Office</asp:ListItem>
                        <asp:ListItem Value="B">Branch</asp:ListItem>
                        <asp:ListItem Value="A">All Branch</asp:ListItem>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CMP_BRANCH_TYPE_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:TemplateField>
            <asp:BoundField DataField="ADDRESS1" HeaderText="Address 1" SortExpression="ADDRESS1" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="ADDRESS2" HeaderText="Address 2" SortExpression="ADDRESS2" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="CITY_ID" HeaderText="City" SortExpression="CITY_ID" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="sdsCountry" DataTextField="CM_COUNTRY_NAME"
                        DataValueField="CM_COUNTRY_ID" SelectedValue='<%# Bind("COUNTRY_ID") %>'>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:TemplateField>
            <asp:BoundField DataField="FAX" HeaderText="Fax" SortExpression="FAX" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="TAX_NO" HeaderText="Tax No" SortExpression="TAX_NO" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="ABBREVIATED_NAME" HeaderText="Short Name" SortExpression="ABBREVIATED_NAME" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="REG_NO" HeaderText="Registration" SortExpression="REG_NO" >
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Fields>
    </asp:DetailsView>
    &nbsp;</div>
</contenttemplate>
    </asp:UpdatePanel>
        &nbsp;
    </form>
</body>
</html>

