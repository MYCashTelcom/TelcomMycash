<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngRobiClientAccount.aspx.cs" Inherits="COMMON_frmMngRobiClientAccount" %>

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
        <asp:Button ID="btnAccountList" runat="server" OnClick="btnAccountList_Click" Text="Account List" BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False" ForeColor="Black" />
        <asp:Button
            ID="btnNewAccount" runat="server" OnClick="btnNewAccount_Click" Text="New Account" BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False" ForeColor="Black" /><asp:UpdatePanel id="udpMngService" runat="server">
        <contenttemplate>
<DIV>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div style="background-color: royalblue; text-align: right;">
        <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">:: Manage Client Account&nbsp;    ::</span></div>
            <asp:SqlDataSource id="sdsClientList" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "CLINT_ID", "CLINT_NAME" FROM "CLIENT_LIST"'>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsClientAccount" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT * FROM "ACCOUNT_LIST"' 
DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' 
InsertCommand='INSERT INTO "ACCOUNT_LIST" ("CLINT_ID", "ACCNT_ID", "ACCNT_NO", "ACCNT_PIN", "ACCNT_PIN_POLICY", "ACCNT_CHARGE_TYPE", "ACCNT_STATE", "ACCNT_BALANCE", "ACCNT_EXPIRY_DATE", "ACCNT_BONUS_ON_NET", "ACCNT_BONUS_OFF_NET", "ACCNT_BONUS_INT","SERVICE_PKG_ID","ACCNT_MSISDN") VALUES (:CLINT_ID, :ACCNT_ID, :ACCNT_NO, :ACCNT_PIN, :ACCNT_PIN_POLICY, :ACCNT_CHARGE_TYPE, :ACCNT_STATE, :ACCNT_BALANCE, :ACCNT_EXPIRY_DATE, :ACCNT_BONUS_ON_NET, :ACCNT_BONUS_OFF_NET, :ACCNT_BONUS_INT, :SERVICE_PKG_ID, :ACCNT_MSISDN)' 
UpdateCommand='UPDATE "ACCOUNT_LIST" SET "CLINT_ID" = :CLINT_ID, "ACCNT_NO" = :ACCNT_NO, "ACCNT_PIN" = :ACCNT_PIN, "ACCNT_PIN_POLICY" = :ACCNT_PIN_POLICY, "ACCNT_CHARGE_TYPE" = :ACCNT_CHARGE_TYPE, "ACCNT_STATE" = :ACCNT_STATE, "ACCNT_BALANCE" = :ACCNT_BALANCE, "ACCNT_EXPIRY_DATE" = :ACCNT_EXPIRY_DATE, "ACCNT_BONUS_ON_NET" = :ACCNT_BONUS_ON_NET, "ACCNT_BONUS_OFF_NET" = :ACCNT_BONUS_OFF_NET, "ACCNT_BONUS_INT" = :ACCNT_BONUS_INT, "SERVICE_PKG_ID" = :SERVICE_PKG_ID, "ACCNT_MSISDN" = :ACCNT_MSISDN WHERE "ACCNT_ID" = :ACCNT_ID'>
    <DeleteParameters>
        <asp:Parameter Name="ACCNT_ID" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="CLINT_ID" Type="String" />
        <asp:Parameter Name="ACCNT_NO" Type="String" />
        <asp:Parameter Name="ACCNT_PIN" Type="String" />
        <asp:Parameter Name="ACCNT_PIN_POLICY" Type="String" />
        <asp:Parameter Name="ACCNT_CHARGE_TYPE" Type="String" />
        <asp:Parameter Name="ACCNT_STATE" Type="String" />
        <asp:Parameter Name="ACCNT_BALANCE" Type="Decimal" />
        <asp:Parameter Name="ACCNT_EXPIRY_DATE" Type="DateTime" />
        <asp:Parameter Name="ACCNT_BONUS_ON_NET" Type="Decimal" />
        <asp:Parameter Name="ACCNT_BONUS_OFF_NET" Type="Decimal" />
        <asp:Parameter Name="ACCNT_BONUS_INT" Type="Decimal" />
        <asp:Parameter Name="SERVICE_PKG_ID" Type="String" /> 
        <asp:Parameter Name="ACCNT_MSISDN" Type="String" />               
        <asp:Parameter Name="ACCNT_ID" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="CLINT_ID" Type="String" />
        <asp:Parameter Name="ACCNT_ID" Type="String" />
        <asp:Parameter Name="ACCNT_NO" Type="String" />
        <asp:Parameter Name="ACCNT_PIN" Type="String" />
        <asp:Parameter Name="ACCNT_PIN_POLICY" Type="String" />
        <asp:Parameter Name="ACCNT_CHARGE_TYPE" Type="String" />
        <asp:Parameter Name="ACCNT_STATE" Type="String" />
        <asp:Parameter Name="ACCNT_BALANCE" Type="Decimal" />
        <asp:Parameter Name="ACCNT_EXPIRY_DATE" Type="DateTime" />
        <asp:Parameter Name="ACCNT_BONUS_ON_NET" Type="Decimal" />
        <asp:Parameter Name="ACCNT_BONUS_OFF_NET" Type="Decimal" />
        <asp:Parameter Name="ACCNT_BONUS_INT" Type="Decimal" />
        <asp:Parameter Name="SERVICE_PKG_ID" Type="String" /> 
        <asp:Parameter Name="ACCNT_MSISDN" Type="String" />       
    </InsertParameters>
</asp:SqlDataSource> 
            <asp:SqlDataSource ID="sdsServicePackage" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "SERVICE_PKG_ID", "SERVICE_PKG_NAME" FROM "SERVICE_PACKAGE"'>
            </asp:SqlDataSource>
 
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                    DataKeyNames="ACCNT_ID" DataSourceID="sdsClientAccount" 
                    PagerStyle-CssClass="pgr" PageSize="15">
                    <Columns>
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
                        <asp:TemplateField HeaderText="Client" SortExpression="CLINT_ID">
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
                        <asp:BoundField DataField="ACCNT_ID" HeaderText="ACCNT_ID" ReadOnly="True" 
                            SortExpression="ACCNT_ID" Visible="False" />
                        <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" 
                            SortExpression="ACCNT_NO">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="PIN" SortExpression="ACCNT_PIN">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox11" runat="server" MaxLength="5" 
                                    Text='<%# Bind("ACCNT_PIN") %>' TextMode="Password" Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                        
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox12" runat="server" Enabled="False" MaxLength="5" 
                                    Text='<%# Bind("ACCNT_PIN") %>' TextMode="Password" Width="50px"></asp:TextBox>
                            </ItemTemplate>
                        
                        
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PIN Policy" SortExpression="ACCNT_PIN_POLICY">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList10" runat="server" 
                                    SelectedValue='<%# Bind("ACCNT_PIN_POLICY") %>'>
                                    <asp:ListItem Selected="True" Value="S">Simple</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList9" runat="server" Enabled="False" 
                                    SelectedValue='<%# Bind("ACCNT_PIN_POLICY") %>'>
                                    <asp:ListItem Selected="True" Value="S">Simaple</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="MSISDN" 
                            SortExpression="ACCNT_MSISDN">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Charging Type" 
                            SortExpression="ACCNT_CHARGE_TYPE">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList12" runat="server" 
                                    SelectedValue='<%# Bind("ACCNT_CHARGE_TYPE") %>'>
                                    <asp:ListItem Selected="True" Value="PRP">Pre Paid</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList11" runat="server" Enabled="False" 
                                    SelectedValue='<%# Bind("ACCNT_CHARGE_TYPE") %>'>
                                    <asp:ListItem Selected="True" Value="PRP">Pre Paid</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
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
                        <asp:TemplateField HeaderText="Balance" SortExpression="ACCNT_BALANCE">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("ACCNT_BALANCE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("ACCNT_BALANCE") %>'></asp:Label>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:BoundField DataField="ACCNT_EXPIRY_DATE" HeaderText="Expiry Date" 
                            SortExpression="ACCNT_EXPIRY_DATE">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Bonus On-Net" 
                            SortExpression="ACCNT_BONUS_ON_NET">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox10" runat="server" 
                                    Text='<%# Bind("ACCNT_BONUS_ON_NET") %>'></asp:TextBox>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("ACCNT_BONUS_ON_NET") %>'></asp:Label>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bonus Off-Net" 
                            SortExpression="ACCNT_BONUS_OFF_NET">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox9" runat="server" 
                                    Text='<%# Bind("ACCNT_BONUS_OFF_NET") %>'></asp:TextBox>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("ACCNT_BONUS_OFF_NET") %>'></asp:Label>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bonus International" 
                            SortExpression="ACCNT_BONUS_INT">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("ACCNT_BONUS_INT") %>'></asp:TextBox>
                            </EditItemTemplate>
                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("ACCNT_BONUS_INT") %>'></asp:Label>
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
                <SPAN style="COLOR: white; font-size: 11px;">:: Add New Account ::</SPAN></STRONG></DIV><asp:DetailsView id="DetailsView1" runat="server" DataKeyNames="ACCNT_ID" Font-Size="11pt" Font-Names="Times New Roman" BorderColor="#E0E0E0" DataSourceID="sdsClientAccount" DefaultMode="Insert" AutoGenerateRows="False" Width="250px" Height="50px"><Fields>
<asp:TemplateField SortExpression="SERVICE_PKG_ID" HeaderText="Service Package">
    <EditItemTemplate> 

<asp:TextBox id="TextBox4" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:TextBox> 
                    </EditItemTemplate>
<InsertItemTemplate> 

<asp:DropDownList id="DropDownList4" runat="server" DataValueField="SERVICE_PKG_ID" DataTextField="SERVICE_PKG_NAME" DataSourceID="sdsServicePackage" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'></asp:DropDownList> 
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label4" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:Label> 
 
                    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="CLINT_ID" HeaderText="Client Name">
    <EditItemTemplate>

<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
<InsertItemTemplate>

<asp:DropDownList id="DropDownList1" runat="server" DataValueField="CLINT_ID" DataTextField="CLINT_NAME" DataSourceID="sdsClientList" SelectedValue='<%# Bind("CLINT_ID") %>'></asp:DropDownList>
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label1" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:Label>

                    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField ReadOnly="True" DataField="ACCNT_ID" Visible="False" SortExpression="ACCNT_ID" HeaderText="ACCNT_ID">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="ACCNT_NO" HeaderText="Account No">
    <EditItemTemplate>

<asp:TextBox id="TextBox7" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:TextBox>
                    </EditItemTemplate>
<InsertItemTemplate> 

<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("ACCNT_NO") %>' 
        MaxLength="20"></asp:TextBox> 
                    &#160;* 
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label7" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:Label>

                    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_PIN" HeaderText="Account PIN">
    <EditItemTemplate>

<asp:TextBox id="TextBox5" runat="server" Text='<%# Bind("ACCNT_PIN") %>'></asp:TextBox>
                    </EditItemTemplate>
<InsertItemTemplate> 

<asp:TextBox id="TextBox1" runat="server" Width="69px" TextMode="Password" Text='<%# Bind("ACCNT_PIN") %>' MaxLength="4"></asp:TextBox> 
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label5" runat="server" Text='<%# Bind("ACCNT_PIN") %>'></asp:Label>

                    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_PIN_POLICY" HeaderText="PIN Policy">
    <EditItemTemplate>

<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("ACCNT_PIN_POLICY") %>'></asp:TextBox>
                    </EditItemTemplate>
<InsertItemTemplate>

<asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("ACCNT_PIN_POLICY") %>'>

    <asp:ListItem Selected="True" Value="S">Simple</asp:ListItem>
<asp:ListItem Value="L">Left Combiniation</asp:ListItem>
<asp:ListItem Value="M">Middle Combination</asp:ListItem>
<asp:ListItem Value="R">Right Combination</asp:ListItem>
    </asp:DropDownList>
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label2" runat="server" Text='<%# Bind("ACCNT_PIN_POLICY") %>'></asp:Label>

                    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ACCNT_MSISDN" SortExpression="ACCNT_MSISDN" HeaderText="MSISDN">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="ACCNT_CHARGE_TYPE" HeaderText="Charging Type">
    <EditItemTemplate> 

<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("ACCNT_CHARGE_TYPE") %>'></asp:TextBox> 
                    </EditItemTemplate>
<InsertItemTemplate> 

<asp:DropDownList id="DropDownList3" runat="server" SelectedValue='<%# Bind("ACCNT_CHARGE_TYPE") %>'>

    <asp:ListItem Selected="True" Value="PRP">Pre Paid</asp:ListItem>
    <asp:ListItem Value="POP">Post Paid</asp:ListItem>
    <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
    <asp:ListItem Value="FRE">Free</asp:ListItem>
    </asp:DropDownList> 
                    </InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
<ItemTemplate>
<asp:Label id="Label3" runat="server" Text='<%# Bind("ACCNT_CHARGE_TYPE") %>'></asp:Label> 
 
                    </ItemTemplate>
</asp:TemplateField>
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
<asp:BoundField DataField="ACCNT_BALANCE" SortExpression="ACCNT_BALANCE" HeaderText="Account Balance">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACCNT_EXPIRY_DATE" SortExpression="ACCNT_EXPIRY_DATE" HeaderText="Expiry Date">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACCNT_BONUS_ON_NET" SortExpression="ACCNT_BONUS_ON_NET" HeaderText="Bonus On-Net">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACCNT_BONUS_OFF_NET" SortExpression="ACCNT_BONUS_OFF_NET" HeaderText="Bonus Off-Net">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACCNT_BONUS_INT" SortExpression="ACCNT_BONUS_INT" HeaderText="Bonus International">
<HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
</asp:BoundField>
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
