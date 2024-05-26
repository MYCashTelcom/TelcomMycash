<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngClientAccount.aspx.cs" Inherits="Forms_frmMngClientAccount" Title="Manage Client Account" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="scmMsgService" runat="server">
    </asp:ScriptManager>
        <asp:Button ID="btnAccountList" runat="server" 
        OnClick="btnAccountList_Click" Text="ERS Account List" 
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" 
        Font-Bold="False" ForeColor="Black" />
        <asp:Button
            ID="btnNewAccount" runat="server" OnClick="btnNewAccount_Click" 
        Text="New ERS Account" BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False" ForeColor="Black" /><asp:UpdatePanel id="udpMngService" runat="server">
        <contenttemplate>
<DIV>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div style="background-color: royalblue; text-align: right;">
        <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">:: Manage ERS Account&nbsp; ::</span></div>
            <asp:SqlDataSource id="sdsClientList" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "CLINT_ID", "CLINT_NAME" FROM "CLIENT_LIST"'>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsClientAccount" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT * FROM "ACCOUNT_LIST"' 
DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' 
InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN)' 

                    UpdateCommand='UPDATE ACCOUNT_LIST SET CLINT_ID = :CLINT_ID, ACCNT_NO = :ACCNT_NO, ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID, ACCNT_MSISDN = :ACCNT_MSISDN WHERE (ACCNT_ID = :ACCNT_ID)'>
    <DeleteParameters>
        <asp:Parameter Name="ACCNT_ID" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="CLINT_ID" Type="String" />
        <asp:Parameter Name="ACCNT_NO" Type="String" />
        <asp:Parameter Name="ACCNT_STATE" Type="String" />
        <asp:Parameter Name="SERVICE_PKG_ID" Type="String" /> 
        <asp:Parameter Name="ACCNT_MSISDN" Type="String" />               
        <asp:Parameter Name="ACCNT_ID" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="CLINT_ID" Type="String" />
        <asp:Parameter Name="ACCNT_NO" Type="String" />
        <asp:Parameter Name="ACCNT_STATE" Type="String" />
        <asp:Parameter Name="SERVICE_PKG_ID" Type="String" /> 
        <asp:Parameter Name="ACCNT_MSISDN" Type="String" />       
    </InsertParameters>
</asp:SqlDataSource> 
            <asp:SqlDataSource ID="sdsServicePackage" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                    SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE'>
            </asp:SqlDataSource>
 
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                    DataKeyNames="ACCNT_ID" DataSourceID="sdsClientAccount" 
                    PagerStyle-CssClass="pgr" PageSize="15" 
                    onrowdeleted="GridView1_RowDeleted" onrowupdated="GridView1_RowUpdated">
                    <Columns>
                        <asp:BoundField DataField="ACCNT_ID" HeaderText="ACCNT_ID" ReadOnly="True" 
                            SortExpression="ACCNT_ID" Visible="False" />
                        <asp:TemplateField HeaderText="Service Package" SortExpression="SERVICE_PKG_ID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList6" runat="server" 
                                    DataSourceID="sdsServicePackage" DataTextField="SERVICE_PKG_NAME" 
                                    DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList5" runat="server" 
                                    DataSourceID="sdsServicePackage" DataTextField="SERVICE_PKG_NAME" 
                                    DataValueField="SERVICE_PKG_ID" Enabled="False" 
                                    SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POS Name" SortExpression="CLINT_ID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList8" runat="server" 
                                    DataSourceID="sdsClientList" DataTextField="CLINT_NAME" 
                                    DataValueField="CLINT_ID" SelectedValue='<%# Bind("CLINT_ID") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList7" runat="server" 
                                    DataSourceID="sdsClientList" DataTextField="CLINT_NAME" 
                                    DataValueField="CLINT_ID" Enabled="False" 
                                    SelectedValue='<%# Bind("CLINT_ID") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ACCNT_NO" HeaderText="POS Code" 
                            SortExpression="ACCNT_NO">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="MSISDN" 
                            SortExpression="ACCNT_MSISDN">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="State" SortExpression="ACCNT_STATE">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList14" runat="server" 
                                    SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                    <asp:ListItem Value="I">Idle</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="L">Locked</asp:ListItem>
                                    <asp:ListItem Value="E">Expired</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList13" runat="server" Enabled="False" 
                                    SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                    <asp:ListItem Value="I">Idle</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="L">Locked</asp:ListItem>
                                    <asp:ListItem Value="E">Expired</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" 
                            ShowEditButton="True" />
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
 
            </asp:View>
            <asp:View ID="View2" runat="server">
            <DIV style="BACKGROUND-COLOR: royalblue; text-align: right;">
                <STRONG>
                <SPAN style="COLOR: white; font-size: 11px;">:: Add ERS Account ::</SPAN></STRONG></DIV>
                <asp:DetailsView id="DetailsView1" runat="server" DataKeyNames="ACCNT_ID" 
                    Font-Size="11pt" Font-Names="Times New Roman" BorderColor="#E0E0E0" 
                    DataSourceID="sdsClientAccount" DefaultMode="Insert" AutoGenerateRows="False" 
                    Width="250px" Height="50px" oniteminserted="DetailsView1_ItemInserted"><Fields>
<asp:BoundField ReadOnly="True" DataField="ACCNT_ID" Visible="False" SortExpression="ACCNT_ID" HeaderText="ACCNT_ID">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SERVICE_PKG_ID" HeaderText="Service Package">
    <EditItemTemplate>
        <asp:TextBox id="TextBox7" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:TextBox>
    
                        </EditItemTemplate>
    <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
    <InsertItemTemplate>
        <asp:DropDownList ID="DropDownList16" runat="server" 
            DataSourceID="sdsServicePackage" DataTextField="SERVICE_PKG_NAME" 
            DataValueField="SERVICE_PKG_ID" 
            SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
        </asp:DropDownList>
     
                        </InsertItemTemplate>
                    
<ItemTemplate>
<asp:Label id="Label7" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:Label>

                        </ItemTemplate>

</asp:TemplateField>
<asp:TemplateField SortExpression="CLINT_ID" HeaderText="POS Name">
    <EditItemTemplate>
        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:TextBox>
                        </EditItemTemplate>
                    
    <InsertItemTemplate>
        <asp:DropDownList ID="DropDownList17" runat="server" 
            DataSourceID="sdsClientList" DataTextField="CLINT_NAME" 
            DataValueField="CLINT_ID" SelectedValue='<%# Bind("CLINT_ID") %>'>
        </asp:DropDownList>
                        </InsertItemTemplate>
                    
    <ItemTemplate>
        <asp:Label ID="Label8" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:Label>
                        </ItemTemplate>
                    
                    
    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
    <ItemStyle Font-Bold="False" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="POS Code" 
                        SortExpression="ACCNT_NO">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                    </asp:BoundField>
<asp:BoundField DataField="ACCNT_MSISDN" SortExpression="ACCNT_MSISDN" HeaderText="MSISDN">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="ACCNT_STATE" HeaderText="Account State">
    <EditItemTemplate>

<asp:TextBox id="TextBox6" runat="server" Text='<%# Bind("ACCNT_STATE") %>'></asp:TextBox>
                        </EditItemTemplate>
<InsertItemTemplate>

<asp:DropDownList id="DropDownList15" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'>

    <asp:ListItem Value="I">Idle</asp:ListItem>
    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
<asp:ListItem Value="L">Locked</asp:ListItem>
<asp:ListItem Value="E">Expired</asp:ListItem>
    </asp:DropDownList>
                        </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label6" runat="server" Text='<%# Bind("ACCNT_STATE") %>'></asp:Label>

                        </ItemTemplate>
</asp:TemplateField>
<asp:CommandField InsertText="Add Account" ButtonType="Button" ShowInsertButton="True">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView> 
            </asp:View>
        </asp:MultiView>

    
    </DIV>
</contenttemplate>
    </asp:UpdatePanel>
        &nbsp;

    
    </form>
</body>
</html>