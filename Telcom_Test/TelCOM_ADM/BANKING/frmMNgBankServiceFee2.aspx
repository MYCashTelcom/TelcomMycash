<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMNgBankServiceFee2.aspx.cs"
    Inherits="BANKING_frmMNgBankServiceFee2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bank Service Fee</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .Top_Panel
        {
            background-color: royalblue;
            height: 24px;
            font-weight: bold;
            font-size: 12px;
            color: White;
        }
        .View_Panel
        {
            background-color: powderblue;
            width: 817px;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBankList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsService" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAccountRank" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsChannelType" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAgntOperatorCommi" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT AGENT_OPARETOR_COMI_ID,COMMISSION_NAME||' '||AGENT_OPERATOR_COMI COMMISSION_NAME  FROM  AGENT_OPARETOR_COMMISSION WHERE (SERVICE_ID=:SERVICE_ID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTop" runat="server" Width="100%">
                <table width="100%" class="Top_Panel">
                    <tr>
                        <td>
                            Manage Service Rate
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Branch
                            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Bank List
                            <asp:DropDownList ID="ddlBankList" runat="server" DataSourceID="sdsBankList" AutoPostBack="true"
                                DataTextField="BANK_NAME" DataValueField="BANK_INTERNAL_CODE">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            Service List
                            <asp:DropDownList ID="ddlService" runat="server" DataSourceID="sdsService" DataTextField="SERVICE_TITLE"
                                DataValueField="SERVICE_ID" __designer:wfdid="w13" AutoPostBack="True" 
                                onselectedindexchanged="ddlService_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                <ProgressTemplate>
                                    <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:Label runat="server" ID="lblMsg"><strong></strong></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblRank" runat="server" Text="Account Rank"></asp:Label>
                            <asp:DropDownList ID="ddlAccountRank" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" DataSourceID="sdsAccountRank" DataTextField="RANK_TITEL"
                                DataValueField="ACCNT_RANK_ID" OnSelectedIndexChanged="ddlAccountRank_SelectedIndexChanged">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblChannelName" runat="server" Text="Channel Name"></asp:Label>
                            <asp:DropDownList ID="ddlChannelName" runat="server" AppendDataBoundItems="true"
                                DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" AutoPostBack="true"
                                DataValueField="CHANNEL_TYPE_ID" OnSelectedIndexChanged="ddlChannelName_SelectedIndexChanged">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="gdvServiceFee" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="BANK_SRV_FEE_ID" PageSize="5"
                CssClass="mGrid" PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt" BorderStyle="None" OnPageIndexChanging="gdvServiceFee_PageIndexChanging"
                OnRowCancelingEdit="gdvServiceFee_RowCancelingEdit" 
                onrowediting="gdvServiceFee_RowEditing" 
                onrowupdating="gdvServiceFee_RowUpdating" 
                onrowdeleting="gdvServiceFee_RowDeleting">
                <Columns>
                    
                    
                    <asp:TemplateField HeaderText="BANK_SRV_FEE_ID" SortExpression="BANK_SRV_FEE_ID" Visible="False">
                        <%--<EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BANK_SRV_FEE_ID") %>'></asp:Label>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BANK_SRV_FEE_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Service" SortExpression="SERVICE_ID" 
                        Visible="False">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    
                    
                    
                    <asp:TemplateField HeaderText="Fee Name" SortExpression="BANK_SRV_FEE_TITLE">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" 
                                Text='<%# Bind("BANK_SRV_FEE_TITLE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("BANK_SRV_FEE_TITLE") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="80px" />
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Start Amount" SortExpression="START_AMOUNT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("START_AMOUNT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("START_AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Max Amount" SortExpression="MAX_AMOUNT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("MAX_AMOUNT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("MAX_AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Fee" SortExpression="BANK_SRV_FEE_AMOUNT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" 
                                Text='<%# Bind("BANK_SRV_FEE_AMOUNT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("BANK_SRV_FEE_AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Minimum Fee" SortExpression="MIN_FEE">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("MIN_FEE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("MIN_FEE") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Vat &amp; Tax" SortExpression="VAT_TAX">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("VAT_TAX") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("VAT_TAX") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="AIT (%)" SortExpression="AIT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("AIT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("AIT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Fees Paid By Bank (%)" 
                        SortExpression="FEES_PAID_BY_BANK">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("FEES_PAID_BY_BANK") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("FEES_PAID_BY_BANK") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Fees Paid By Initiator (%)" 
                        SortExpression="FEES_PAID_BY_CUSTOMER">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" 
                                Text='<%# Bind("FEES_PAID_BY_CUSTOMER") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" 
                                Text='<%# Bind("FEES_PAID_BY_CUSTOMER") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Fees Paid By Receipent(%)" 
                        SortExpression="FEES_PAID_BY_AGENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" 
                                Text='<%# Bind("FEES_PAID_BY_AGENT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("FEES_PAID_BY_AGENT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Bank Commission (%)" 
                        SortExpression="BANK_COMM_AMOUNT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" 
                                Text='<%# Bind("BANK_COMM_AMOUNT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("BANK_COMM_AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Agent Commission (%)" 
                        SortExpression="AGENT_COMM_AMOUNT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" 
                                Text='<%# Bind("AGENT_COMM_AMOUNT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("AGENT_COMM_AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Pool Adjustment (%)" 
                        SortExpression="POOL_ADJUSTMENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox14" runat="server" 
                                Text='<%# Bind("POOL_ADJUSTMENT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("POOL_ADJUSTMENT") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Vendor Commission (%)" 
                        SortExpression="VENDOR_COMMISSION">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("VENDOR_COMMISSION") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("VENDOR_COMMISSION") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Third Party Commission (%)" 
                        SortExpression="THIRD_PARTY_COMMISSION">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox16" runat="server" 
                                Text='<%# Bind("THIRD_PARTY_COMMISSION") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label17" runat="server" 
                                Text='<%# Bind("THIRD_PARTY_COMMISSION") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Channel Commission (%)" 
                        SortExpression="CHANNEL_COMMISSION">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("CHANNEL_COMMISSION") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("CHANNEL_COMMISSION") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Common Operator Commission (%)" 
                        SortExpression="AGENT_OPERATOR_COMI">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("AGENT_OPERATOR_COMI") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label19" runat="server" 
                                Text='<%# Bind("AGENT_OPERATOR_COMI") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Sub Service Code" SortExpression="SUB_SERVICE_CODE">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSubServiceCodeGv" runat="server" Text='<%# Bind("SUB_SERVICE_CODE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubServiceCodeGv" runat="server" Text='<%# Bind("SUB_SERVICE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField> 
					
					<asp:TemplateField HeaderText="Sub Wallet Number" SortExpression="SUB_WALLET_NUMBER">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSubWalletNumberGv" runat="server" Text='<%# Bind("SUB_WALLET_NUMBER") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubWalletNumberGv" runat="server" Text='<%# Bind("SUB_WALLET_NUMBER") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Agent Operator Commission" SortExpression="AGENT_OPARETOR_COMI_ID">
                        
                        
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAgntOpertrComID91" runat="server" DataSourceID="sdsAgntOperatorCommi"
                                AppendDataBoundItems="true" DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"
                                SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        
                        
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList187" runat="server" DataSourceID="sdsAgntOperatorCommi"
                                AppendDataBoundItems="True" DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"
                                Enabled="False" SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="250px" />
                        <ControlStyle Width="250px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Tax Paid By" ItemStyle-Width="60px" SortExpression="TAX_PAID_BY">
                        
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList8" runat="server" DataTextField="TAX_PAID_BY" AppendDataBoundItems="true"
                                DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList10" runat="server" DataTextField="TAX_PAID_BY"
                                AppendDataBoundItems="true" DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>'
                                Enabled="false">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                        <ControlStyle Width="80px" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Fees Paid By" ItemStyle-Width="60px" SortExpression="FEES_PAID_BY">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlFeesPaidby1" runat="server" DataTextField="FEES_PAID_BY"
                                AppendDataBoundItems="true" DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFeesPaidby2" runat="server" DataTextField="FEES_PAID_BY"
                                AppendDataBoundItems="true" DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>'
                                Enabled="false">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                        <ControlStyle Width="80px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Fees Include Vat/Tax" ItemStyle-Width="50px" SortExpression="FEE_INCLUDE_VAT_TAX">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList11" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList13" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'
                                Enabled="false">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="50px" />
                        <ControlStyle Width="50px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Operations</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CausesValidation="true"
                                OnClientClick="return confirm('Are you sure to Delete?')" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure to Update?')"/>
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                        </EditItemTemplate>
                        
                        <ItemStyle Width="150px" />
                        <ControlStyle Width="150px" />
                        
                        
                    </asp:TemplateField>
                    
                    
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            
            <br/>
            
            <fieldset style="width: 500px">
                <legend><strong>Add Service Fee</strong></legend>
                
                <table style="width: 500px">
                    <tr>
                        <td>
                            <strong>Fee Name</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFeeName" Placeholder="Fee Name" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtFeeName" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Start Amount</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtStartAmount" Placeholder="Start Amount" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtStartAmount" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Max Amount</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMaxAmount" Placeholder="Maximum Amount" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtMaxAmount" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fee(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFeePercnt" Placeholder="Fee(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtFeePercnt" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Minimum Fee</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMinFee" Placeholder="Minimum Fee" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtMinFee" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>VAT & TAX(%) </strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtVatNTaxPercent" Placeholder="Vat & Tax(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="txtVatNTaxPercent" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>AIT(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAitPercent" Placeholder="AIT(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtAitPercent" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fee Paid By Bank(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFeePaidByBankPercent" Placeholder="Fee Paid By Bank(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="txtFeePaidByBankPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fees Paid By Initiator(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFeePaidByInitPercent" Placeholder="Fee Paid by Initiator(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                ControlToValidate="txtFeePaidByInitPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fees Paid By Receipent(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFeePaidByReceptPercent" Placeholder="Fee Paid by Receipent(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                ControlToValidate="txtFeePaidByReceptPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Bank Commission(%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBankCommissionPercent" Placeholder="Bank Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                ControlToValidate="txtBankCommissionPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Agent Commission (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAgentCommissionPercent" Placeholder="Agent Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                ControlToValidate="txtAgentCommissionPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td>
                            <strong>Pool Adjustment (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPoolAdjustmentPercent" Placeholder="Pool Adjustment(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtPoolAdjustmentPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Vendor Commission (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtVendorCommissionPercent" Placeholder="Vendor Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtVendorCommissionPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Third Party Commission (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txt3rdPartyCommiPercent" Placeholder="Third Party Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                ControlToValidate="txt3rdPartyCommiPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Channel Commission (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtChannelCommiPercent" Placeholder="Channel Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                ControlToValidate="txtChannelCommiPercent" ErrorMessage="*" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td>
                            <strong>Common Operator Commission (%)</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCommonOperatorCommPercent" Placeholder="CommonOperator Commission(%)" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                ControlToValidate="txtCommonOperatorCommPercent" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Sub Service Code</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSubServiceCode" Placeholder="Sub Service Code" Width="250px"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                ControlToValidate="txtSubServiceCode" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
					<tr>
                        <td>
                            <strong>Sub Wallet Number</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSubWalletNumber" Placeholder="Sub Wallet Number" Width="250px"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                ControlToValidate="txtSubWalletNumber" ErrorMessage="*" 
                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
					
                    <tr>
                        <td>
                            <strong>Agent Operator Commission(%)</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAgntOpCommissionIDAdd" runat="server" DataSourceID="sdsAgntOperatorCommi" 
                                    DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID" >
                                </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Tax Paid By</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaxPaidByAdd" runat="server" DataTextField="TAX_PAID_BY" DataValueField="TAX_PAID_BY"
                                SelectedValue='<%# Bind("TAX_PAID_BY") %>' Enabled="True" >
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fees Paid By</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeesPaidByAdd" runat="server" DataTextField="FEES_PAID_BY" DataValueField="FEES_PAID_BY"
                                SelectedValue='<%# Bind("FEES_PAID_BY") %>' Enabled="True" >
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <strong>Fees Include Vat/Tax</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeeIncludeVatTaxAdd" runat="server"  DataTextField="FEE_INCLUDE_VAT_TAX" DataValueField="FEE_INCLUDE_VAT_TAX"  SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>' >
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>                                  
                                </asp:DropDownList>
                            </td>
                    </tr>
                    
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button runat="server" ID="btnAdd" Text="Add Service Fee"  onclick="btnAdd_Click"  ValidationGroup="a"/>
                        </td>
                    </tr>
                    
                </table>
                
            </fieldset>
            
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
