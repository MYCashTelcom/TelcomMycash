<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountActivation.aspx.cs" Inherits="Forms_frmAccountActivation" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Account Activation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="scmMsgService" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel id="udpMngService" runat="server">
        <contenttemplate>
<div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                     
        <div style="background-color: royalblue;"> <strong><span style="color: white">Idle Account List</span></strong></div>
        
            <asp:SqlDataSource id="sdsClientList" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "CLINT_ID", "CLINT_NAME" FROM "CLIENT_LIST"'>
</asp:SqlDataSource> 

<asp:SqlDataSource ID="sdsClientIdleList" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                    SelectCommand="SELECT * FROM ACCOUNT_LIST WHERE (ACCNT_STATE = &#039;I&#039;) ORDER BY ACCNT_ID DESC">
                </asp:SqlDataSource>

<asp:SqlDataSource id="sdsClientAccount" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT * FROM ACCOUNT_LIST WHERE (ACCNT_STATE = &#039;I&#039;) ORDER BY ACCNT_ID DESC'
DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' 
InsertCommand='INSERT INTO "ACCOUNT_LIST" ("CLINT_ID", "ACCNT_ID", "ACCNT_NO", "ACCNT_PIN", "ACCNT_PIN_POLICY", "ACCNT_CHARGE_TYPE", "ACCNT_STATE", "ACCNT_BALANCE", "ACCNT_EXPIRY_DATE", "ACCNT_BONUS_ON_NET", "ACCNT_BONUS_OFF_NET", "ACCNT_BONUS_INT","SERVICE_PKG_ID","ACCNT_MSISDN") VALUES (:CLINT_ID, :ACCNT_ID, :ACCNT_NO, :ACCNT_PIN, :ACCNT_PIN_POLICY, :ACCNT_CHARGE_TYPE, :ACCNT_STATE, :ACCNT_BALANCE, :ACCNT_EXPIRY_DATE, :ACCNT_BONUS_ON_NET, :ACCNT_BONUS_OFF_NET, :ACCNT_BONUS_INT, :SERVICE_PKG_ID, :ACCNT_MSISDN)' 

                    
                    UpdateCommand='UPDATE ACCOUNT_LIST SET CLINT_ID = :CLINT_ID, ACCNT_NO = :ACCNT_NO, ACCNT_PIN = :ACCNT_PIN, ACCNT_PIN_POLICY = :ACCNT_PIN_POLICY, ACCNT_CHARGE_TYPE = :ACCNT_CHARGE_TYPE, ACCNT_STATE = :ACCNT_STATE, ACCNT_BALANCE = :ACCNT_BALANCE, ACCNT_EXPIRY_DATE = :ACCNT_EXPIRY_DATE, ACCNT_BONUS_ON_NET = :ACCNT_BONUS_ON_NET, ACCNT_BONUS_OFF_NET = :ACCNT_BONUS_OFF_NET, ACCNT_BONUS_INT = :ACCNT_BONUS_INT, SERVICE_PKG_ID = :SERVICE_PKG_ID, ACCNT_MSISDN = :ACCNT_MSISDN WHERE (ACCNT_ID = :ACCNT_ID)'>
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
                <asp:Button ID="btnRefreshData" runat="server" Text="Refresh" 
                    onclick="btnRefreshData_Click" />
                    <asp:Label ID="lblMessage" runat="server" Text="lblMessage" Font-Size="11px" ForeColor="Red"></asp:Label>
        <asp:GridView id="GridView1" runat="server" DataKeyNames="ACCNT_ID" Font-Size="11pt" 
                    Font-Names="Times New Roman" BorderColor="White" AllowSorting="True" 
                    AutoGenerateColumns="False" DataSourceID="sdsClientAccount" 
                    AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging"><Columns>
<asp:TemplateField SortExpression="SERVICE_PKG_ID" HeaderText="Service Package"><EditItemTemplate><asp:DropDownList id="DropDownList6" runat="server" DataValueField="SERVICE_PKG_ID" DataTextField="SERVICE_PKG_NAME" DataSourceID="sdsServicePackage" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'></asp:DropDownList></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:DropDownList id="DropDownList5" runat="server" DataValueField="SERVICE_PKG_ID" DataTextField="SERVICE_PKG_NAME" DataSourceID="sdsServicePackage" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>' Enabled="False"></asp:DropDownList></ItemTemplate></asp:TemplateField>
<asp:TemplateField SortExpression="CLINT_ID" HeaderText="Client"><EditItemTemplate><asp:DropDownList id="DropDownList8" runat="server" DataValueField="CLINT_ID" DataTextField="CLINT_NAME" DataSourceID="sdsClientList" SelectedValue='<%# Bind("CLINT_ID") %>'></asp:DropDownList></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:DropDownList id="DropDownList7" runat="server" DataValueField="CLINT_ID" DataTextField="CLINT_NAME" DataSourceID="sdsClientList" SelectedValue='<%# Bind("CLINT_ID") %>' Enabled="False"></asp:DropDownList></ItemTemplate></asp:TemplateField>
<asp:BoundField ReadOnly="True" DataField="ACCNT_ID" Visible="False" SortExpression="ACCNT_ID" HeaderText="ACCNT_ID"></asp:BoundField>
<asp:BoundField DataField="ACCNT_NO" SortExpression="ACCNT_NO" HeaderText="Account No"><HeaderStyle HorizontalAlign="Center"></HeaderStyle></asp:BoundField>
<asp:TemplateField SortExpression="ACCNT_PIN" HeaderText="PIN" Visible="False"><EditItemTemplate><asp:TextBox id="TextBox3" runat="server" Width="50px" TextMode="Password" Text='<%# Bind("ACCNT_PIN") %>' MaxLength="5"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle></asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_PIN_POLICY" HeaderText="PIN Policy" 
                Visible="False"><EditItemTemplate><asp:DropDownList id="DropDownList10" runat="server" SelectedValue='<%# Bind("ACCNT_PIN_POLICY") %>'><asp:ListItem Selected="True" Value="S">Simple</asp:ListItem></asp:DropDownList></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:DropDownList id="DropDownList9" runat="server" SelectedValue='<%# Bind("ACCNT_PIN_POLICY") %>' Enabled="False"><asp:ListItem Selected="True" Value="S">Simaple</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>

<asp:BoundField DataField="ACCNT_MSISDN" SortExpression="ACCNT_MSISDN" HeaderText="MSISDN"><HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle></asp:BoundField>

<asp:TemplateField SortExpression="ACCNT_CHARGE_TYPE" HeaderText="Charging Type"><EditItemTemplate><asp:DropDownList id="DropDownList12" runat="server" SelectedValue='<%# Bind("ACCNT_CHARGE_TYPE") %>'><asp:ListItem Selected="True" Value="PRP">Pre Paid</asp:ListItem></asp:DropDownList></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:DropDownList id="DropDownList11" runat="server" SelectedValue='<%# Bind("ACCNT_CHARGE_TYPE") %>' Enabled="False"><asp:ListItem Selected="True" Value="PRP">Pre Paid</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_STATE" HeaderText="State"><EditItemTemplate><asp:DropDownList id="DropDownList14" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'><asp:ListItem Value="I">Idle</asp:ListItem><asp:ListItem Selected="True" Value="A">Active</asp:ListItem><asp:ListItem Value="L">Locked</asp:ListItem><asp:ListItem Value="E">Expired</asp:ListItem></asp:DropDownList></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:DropDownList id="DropDownList13" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>' Enabled="False"><asp:ListItem Value="I">Idle</asp:ListItem><asp:ListItem Selected="True" Value="A">Active</asp:ListItem><asp:ListItem Value="L">Locked</asp:ListItem><asp:ListItem Value="E">Expired</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_BALANCE" HeaderText="Balance"><EditItemTemplate><asp:TextBox runat="server" Text='<%# Bind("ACCNT_BALANCE") %>' id="TextBox7"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:Label runat="server" Text='<%# Bind("ACCNT_BALANCE") %>' id="Label7"></asp:Label></ItemTemplate></asp:TemplateField>
<asp:BoundField DataField="ACCNT_EXPIRY_DATE" SortExpression="ACCNT_EXPIRY_DATE" HeaderText="Expiry Date"><HeaderStyle HorizontalAlign="Center"></HeaderStyle></asp:BoundField>
<asp:TemplateField SortExpression="ACCNT_BONUS_ON_NET" HeaderText="Bonus On-Net"><EditItemTemplate><asp:TextBox runat="server" Text='<%# Bind("ACCNT_BONUS_ON_NET") %>' id="TextBox10"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:Label runat="server" Text='<%# Bind("ACCNT_BONUS_ON_NET") %>' id="Label10"></asp:Label></ItemTemplate></asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_BONUS_OFF_NET" HeaderText="Bonus Off-Net"><EditItemTemplate><asp:TextBox runat="server" Text='<%# Bind("ACCNT_BONUS_OFF_NET") %>' id="TextBox9"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:Label runat="server" Text='<%# Bind("ACCNT_BONUS_OFF_NET") %>' id="Label9"></asp:Label></ItemTemplate></asp:TemplateField>
<asp:TemplateField SortExpression="ACCNT_BONUS_INT" HeaderText="Bonus International"><EditItemTemplate><asp:TextBox runat="server" Text='<%# Bind("ACCNT_BONUS_INT") %>' id="TextBox8"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center"></HeaderStyle><ItemTemplate><asp:Label runat="server" Text='<%# Bind("ACCNT_BONUS_INT") %>' id="Label8"></asp:Label></ItemTemplate></asp:TemplateField>
</Columns>
</asp:GridView> 
        </span></strong>
            <%--<DIV style="BACKGROUND-COLOR: royalblue; text-align: right;">
                <STRONG>
                <SPAN style="COLOR: white; font-size: 11px;">:: Account Activation ::</SPAN></STRONG></DIV>--%>
                
                 <div style="background-color: royalblue"> <strong><span style="color: white">Account Activation List</span></strong></div>
                
            <div>
                <table>
                    <tr>
                           <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Search by Mobile No" Width="143px" 
                                Font-Size="11px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchAccountMSISDN" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchAccountMSISDN" runat="server" Text="Search" 
                                    onclick="btnSearchAccountMSISDN_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lbllstOfMobileNo" runat="server" Text="List of Mobile No:" Width="143px" 
                                Font-Size="11px"></asp:Label>
                            </td>
                           <td>
                            <asp:DropDownList ID="ddlAccountMsisdnList" runat="server" DataSourceID="sdsClientIdleList"
                                DataTextField="ACCNT_MSISDN" DataValueField="ACCNT_ID">
                            </asp:DropDownList></td>
                        <td>
                            <asp:Button ID="btnActivation" runat="server" OnClick="btnActivation_Click" Text="Active" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            
                        </td>                        
                    </tr>
                    
                </table>
            </div>
            </asp:View>
        </asp:MultiView>    
    </div>
</contenttemplate>
    </asp:UpdatePanel>
        &nbsp;    
    </form>
</body>
</html>
