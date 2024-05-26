<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankList.aspx.cs" Inherits="Forms_frmMngBankList" Title="Manage Banks" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Bank List</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsBankList" runat="server" 
SelectCommand='SELECT BANK_ID, BANK_NAME, BANK_ADDRESS, BANK_INTERNAL_CODE, BANK_SWIFT_CODE, BANK_CB_CODE, BANK_SATLE_AC, BANK_IS_SATLMNT_BANK FROM BANK_LIST' 
UpdateCommand='UPDATE BANK_LIST SET BANK_NAME = :BANK_NAME, BANK_ADDRESS = :BANK_ADDRESS, BANK_INTERNAL_CODE = :BANK_INTERNAL_CODE, BANK_SWIFT_CODE = :BANK_SWIFT_CODE, BANK_CB_CODE = :BANK_CB_CODE, BANK_SATLE_AC = :BANK_SATLE_AC, BANK_IS_SATLMNT_BANK = :BANK_IS_SATLMNT_BANK WHERE (BANK_ID = :BANK_ID)' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
InsertCommand='INSERT INTO BANK_LIST(BANK_ID, BANK_NAME, BANK_ADDRESS, BANK_INTERNAL_CODE, BANK_SWIFT_CODE, BANK_CB_CODE, BANK_SATLE_AC) VALUES (:BANK_ID, :BANK_NAME, :BANK_ADDRESS, :BANK_INTERNAL_CODE, :BANK_SWIFT_CODE,:BANK_CB_CODE, :BANK_SATLE_AC)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
DeleteCommand='DELETE FROM "BANK_LIST" WHERE "BANK_ID" = :BANK_ID'><DeleteParameters>
<asp:Parameter Type="String" Name="BANK_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="BANK_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_ADDRESS"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_INTERNAL_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_SWIFT_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_CB_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_SATLE_AC"></asp:Parameter>
    <asp:Parameter Name="BANK_IS_SATLMNT_BANK" />
    <asp:Parameter Name="BANK_ID" />
</UpdateParameters>
<InsertParameters>
    <asp:Parameter Name="BANK_ID" />
<asp:Parameter Type="String" Name="BANK_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_ADDRESS"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_INTERNAL_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_SWIFT_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_CB_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="BANK_SATLE_AC"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> 
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="BANK_ID" DataSourceID="sdsBankList" 
            onrowupdated="GridView1_RowUpdated">
        <Columns>
            <asp:BoundField DataField="BANK_ID" HeaderText="BANK_ID" ReadOnly="True" SortExpression="BANK_ID" Visible="False" />
            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" SortExpression="BANK_NAME" />
            <asp:BoundField DataField="BANK_INTERNAL_CODE" HeaderText="Internal Code" SortExpression="BANK_INTERNAL_CODE" />
            <asp:BoundField DataField="BANK_SWIFT_CODE" HeaderText="Swift Code" SortExpression="BANK_SWIFT_CODE" />
            <asp:BoundField DataField="BANK_CB_CODE" HeaderText="Central Bank Code" SortExpression="BANK_CB_CODE" />
            <asp:BoundField DataField="BANK_SATLE_AC" HeaderText="Satlement A/C" SortExpression="BANK_SATLE_AC" />
            <asp:TemplateField HeaderText="Is Settlement Bank" SortExpression="BANK_IS_SATLMNT_BANK">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("BANK_IS_SATLMNT_BANK") %>'>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled="False" SelectedValue='<%# Bind("BANK_IS_SATLMNT_BANK") %>'>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" SortExpression="BANK_ADDRESS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="52px" Text='<%# Bind("BANK_ADDRESS") %>'
                        TextMode="MultiLine" Width="232px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BANK_ADDRESS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Bank&nbsp;</SPAN></STRONG></DIV><DIV>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="BANK_ID"
        DataSourceID="sdsBankList" DefaultMode="Insert" Height="50px" Width="125px" 
            oniteminserted="DetailsView1_ItemInserted">
        <Fields>
            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" SortExpression="BANK_NAME">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BANK_INTERNAL_CODE" HeaderText="Internal Code" SortExpression="BANK_INTERNAL_CODE">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BANK_SWIFT_CODE" HeaderText="Swift Code" SortExpression="BANK_SWIFT_CODE">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BANK_CB_CODE" HeaderText="Central Bank Code" SortExpression="BANK_CB_CODE">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BANK_SATLE_AC" HeaderText="Satlement A/C" SortExpression="BANK_SATLE_AC">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BANK_ADDRESS" HeaderText="Address" SortExpression="BANK_ADDRESS">
                <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" InsertText="Add New Bank" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Fields>
    </asp:DetailsView>
    &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>