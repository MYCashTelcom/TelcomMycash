<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngMerchatFee.aspx.cs" Inherits="BANKING_frmMngMerchatFee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bank Service Fee</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 24px;
            font-weight: bold;
            font-size: 12px;
            color: White;
        }

        .View_Panel {
            background-color: powderblue;
        }

        .Inser_Panel {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }

        .textbox-style
        {
            text-align:right;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form2" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Panel ID="pnlTop" runat="server" Width="100%">
                    <table width="100%" class="Top_Panel">
                        <tr>
                            <td>Merchant Service Fee
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Branch 
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Bank  List    
            <asp:DropDownList ID="ddlBankList" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td align="left">Service List
            <asp:DropDownList ID="ddlService" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlService_SelectedIndexChanged">
            </asp:DropDownList>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left">
                                <asp:Label ID="lblRank" runat="server" Text="Account Rank"></asp:Label>

                                <asp:DropDownList ID="ddlAccountRank" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlAccountRank_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td align="left">
                                <asp:Label ID="lblChannelName" runat="server" Text="Channel Name"></asp:Label>
                                <asp:DropDownCheckBoxes ID="ddlChannelName" runat="server" AppendDataBoundItems="true"  AutoPostBack="true" UseButtons="true" Width="200" OnSelectedIndexChanged="ddlChannelName_SelectedIndexChanged">
                                    </asp:DropDownCheckBoxes>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div style="overflow-y:scroll" runat="server" id="dvGridView">

                    <asp:GridView ID="gdvServiceFee" runat="server" AllowPaging="True" PageSize="5"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MERCHANT_ID"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowUpdating="gdvServiceFee_RowUpdating" BorderStyle="None"
                        OnRowDeleted="gdvServiceFee_RowDeleted"
                        OnRowUpdated="gdvServiceFee_RowUpdated"
                        OnRowCancelingEdit="gdvServiceFee_RowCancelingEdit"
                        OnRowEditing="gdvServiceFee_RowEditing" OnPageIndexChanging="gdvServiceFee_PageIndexChanging" OnRowDeleting="gdvServiceFee_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Merchant Wallet" SortExpression="ACCNT_NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblMerchantWallet" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel Type" SortExpression="CHANNEL_TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblChannelType" runat="server" Text='<%# Bind("CHANNEL_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee Name" SortExpression="BANK_SRV_FEE_TITLE">
                                <ItemTemplate>
                                    <asp:Label ID="lblFeeName" runat="server" Text='<%# Bind("BANK_SRV_FEE_TITLE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Amount" SortExpression="START_AMOUNT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStartAmount" runat="server" Text='<%# Bind("START_AMOUNT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartAmount" runat="server" Text='<%# Bind("START_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Max Amount" SortExpression="MAX_AMOUNT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMaxAmount" runat="server" Text='<%# Bind("MAX_AMOUNT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMaxAmount" runat="server" Text='<%# Bind("MAX_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee" SortExpression="BANK_SRV_FEE_AMOUNT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFee" runat="server" Text='<%# Bind("BANK_SRV_FEE_AMOUNT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFee" runat="server" Text='<%# Bind("BANK_SRV_FEE_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimum Fee" SortExpression="MIN_FEE">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMinimumFee" runat="server" Text='<%# Bind("MIN_FEE") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumFee" runat="server" Text='<%# Bind("MIN_FEE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat &amp; Tax" SortExpression="VAT_TAX">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVatTax" runat="server" Text='<%# Bind("VAT_TAX") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVatTax" runat="server" Text='<%# Bind("VAT_TAX") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AIT (%)" SortExpression="AIT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAIT" runat="server" Text='<%# Bind("AIT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAIT" runat="server" Text='<%# Bind("AIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fees Paid By Bank (%)" SortExpression="FEES_PAID_BY_BANK">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFeesPaidByBank" runat="server" Text='<%# Bind("FEES_PAID_BY_BANK") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFeesPaidByBank" runat="server" Text='<%# Bind("FEES_PAID_BY_BANK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fees Paid By Initiator(%)" SortExpression="FEES_PAID_BY_CUSTOMER">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFeesPaidByInitiator" runat="server" Text='<%# Bind("FEES_PAID_BY_CUSTOMER") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFeesPaidByInitiator" runat="server" Text='<%# Bind("FEES_PAID_BY_CUSTOMER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fees Paid By Receipent(%)" SortExpression="FEES_PAID_BY_AGENT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFeesPaidByReceipent" runat="server" Text='<%# Bind("FEES_PAID_BY_AGENT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFeesPaidByReceipent" runat="server" Text='<%# Bind("FEES_PAID_BY_AGENT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Commission (%)" SortExpression="BANK_COMM_AMOUNT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankCommission" runat="server" Text='<%# Bind("BANK_COMM_AMOUNT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBankCommission" runat="server" Text='<%# Bind("BANK_COMM_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent Commission (%)" SortExpression="AGENT_COMM_AMOUNT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAgentCommission" runat="server" Text='<%# Bind("AGENT_COMM_AMOUNT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAgentCommission" runat="server" Text='<%# Bind("AGENT_COMM_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pool Adjustment (%)" SortExpression="POOL_ADJUSTMENT">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPoolAdjustment" runat="server" Text='<%# Bind("POOL_ADJUSTMENT") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPoolAdjustment" runat="server" Text='<%# Bind("POOL_ADJUSTMENT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Commission (%)" SortExpression="VENDOR_COMMISSION">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVendorCommission" runat="server" Text='<%# Bind("VENDOR_COMMISSION") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorCommission" runat="server" Text='<%# Bind("VENDOR_COMMISSION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Third Party Commission (%)" SortExpression="THIRD_PARTY_COMMISSION">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtThirdPartyCommission" runat="server" Text='<%# Bind("THIRD_PARTY_COMMISSION") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblThirdPartyCommission" runat="server" Text='<%# Bind("THIRD_PARTY_COMMISSION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel Commission (%)" SortExpression="CHANNEL_COMMISSION">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChannelCommission" runat="server" Text='<%# Bind("CHANNEL_COMMISSION") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChannelCommission" runat="server" Text='<%# Bind("CHANNEL_COMMISSION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent Operator Commission (%)" SortExpression="AGENT_OPERATOR_COMI">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAgentOperatorCommission" runat="server" Text='<%# Bind("AGENT_OPERATOR_COMI") %>' Width="60"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAgentOperatorCommission" runat="server" Text='<%# Bind("AGENT_OPERATOR_COMI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Tax Paid By" ItemStyle-Width="60px" SortExpression="TAX_PAID_BY">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTaxPaidByEdit" runat="server" DataTextField="TAX_PAID_BY" AppendDataBoundItems="true"
                                        DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>'>
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                        <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                        <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTaxPaidBy" runat="server" DataTextField="TAX_PAID_BY" AppendDataBoundItems="true"
                                        DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>' Enabled="false">
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
                                    <asp:DropDownList ID="ddlFeesPaidbyEdit" runat="server" DataTextField="FEES_PAID_BY" AppendDataBoundItems="true"
                                        DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>'>
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                        <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                        <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFeesPaidby" runat="server" DataTextField="FEES_PAID_BY" AppendDataBoundItems="true"
                                        DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>' Enabled="false">
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
                                    <asp:DropDownList ID="ddlFeeIncludeVatTaxEdit" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                        DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'>
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFeeIncludeVatTax" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                        DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>' Enabled="false">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fees Paid by Mother Merchant" ItemStyle-Width="50px" SortExpression="FEES_PAID_BY_MM">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFeesPaidByMMEdit" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                        DataValueField="FEES_PAID_BY_MM" SelectedValue='<%# Bind("FEES_PAID_BY_MM") %>'>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFeesPaidByMM" runat="server" DataTextField="FEES_PAID_BY_MM"
                                        DataValueField="FEES_PAID_BY_MM" SelectedValue='<%# Bind("FEES_PAID_BY_MM") %>' Enabled="false">
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:Button ID="btnBankUpdate" runat="server" CausesValidation="True"
                                        CommandName="Update" Text="Update" />
                                    &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Cancel" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeBankUpdate" runat="server"
                                        DisplayModalPopupID="ModalPopupExtender2" OnClientCancel="cancelClick"
                                        TargetControlID="btnBankUpdate">
                                    </ajaxToolkit:ConfirmButtonExtender>
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                        BackgroundCssClass="modalBackground" CancelControlID="btnBankUpdateCancel"
                                        OkControlID="btnBankUpdateOK" TargetControlID="btnBankUpdate"
                                        PopupControlID="pnlPopUpBankUpdate">
                                    </ajaxToolkit:ModalPopupExtender>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="False"
                                        CommandName="Edit" Text="Edit" />
                                    &nbsp;<asp:Button ID="btnDeleteBank" runat="server" CausesValidation="False"
                                        CommandName="Delete" Text="Delete" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeBankDelete" runat="server"
                                        DisplayModalPopupID="ModalPopupExtender3" OnClientCancel="cancelClick"
                                        TargetControlID="btnDeleteBank">
                                    </ajaxToolkit:ConfirmButtonExtender>
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                        BackgroundCssClass="modalBackground" CancelControlID="btnBankDeleteCancel"
                                        OkControlID="btnBankDeleteOK" TargetControlID="btnDeleteBank"
                                        PopupControlID="pnlPopUpBankDelete">
                                    </ajaxToolkit:ModalPopupExtender>

                                </ItemTemplate>
                                <ControlStyle Width="60px" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
                </div>

                    <asp:Panel ID="pnlPopUpBankUpdate" runat="server" Style="display: none; width: 300px; height: 165px; background-color: White; border-width: 1px; border-color: Silver; border-style: solid; padding: 1px;">
                        <div style="height: 95px;">
                            <br />
                            &nbsp;<br />
                            &nbsp;
                      Are you sure you want to update?
                         <br />
                            &nbsp;<br />
                            &nbsp;
                        </div>
                        <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                            <br />
                            &nbsp;
                            <asp:Button ID="btnBankUpdateOK" runat="server" Text="  Yes  " />
                            <asp:Button ID="btnBankUpdateCancel" runat="server" Text=" Cancel " />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlPopUpBankDelete" runat="server" Style="display: none; width: 300px; height: 165px; background-color: White; border-width: 1px; border-color: Silver; border-style: solid; padding: 1px;">
                        <div style="height: 95px;">
                            <br />
                            &nbsp;<br />
                            &nbsp;
                       Are you sure you want to delete?
                         <br />
                            &nbsp;<br />
                            &nbsp;
                        </div>
                        <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                            <br />
                            &nbsp;
                        <asp:Button ID="btnBankDeleteOK" runat="server" Text="  Yes  " />
                            <asp:Button ID="btnBankDeleteCancel" runat="server" Text=" Cancel " />
                        </div>
                    </asp:Panel>

                
                <asp:Panel ID="pnlView" runat="server" CssClass="View_Panel">
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Search by Merchant Account:" Font-Size="12px"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchWalletID" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:Button ID="btnSearchWallet" runat="server" Text="Search" OnClick="btnSearchWallet_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExportReport" runat="server" Text="Export Report" OnClick="btnExportReport_Click" />
                                </td>
                            </tr>

                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlInsert" runat="server">
                    <div class="Inser_Panel">
                        <strong><span style="color: white">Add New Service Fee</span></strong>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td class="textbox-style">Merchant Account</td>
                                <td>
                                    <asp:TextBox ID="txtWalletAccount" runat="server" Enabled="true" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblSearchWalletAccntID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvWalletAccount" runat="server" ErrorMessage="*" ControlToValidate="txtWalletAccount" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fee Name</td>
                                <td>
                                    <asp:TextBox ID="txtFeeName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvFeeName" runat="server" ErrorMessage="*" ControlToValidate="txtFeeName" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Start Amount</td>
                                <td>
                                    <asp:TextBox ID="txtStartAmount" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvStartAmount" runat="server" ErrorMessage="*" ControlToValidate="txtStartAmount" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Max Amount</td>
                                <td>
                                    <asp:TextBox ID="txtMaxAmount" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvMaxAmount" runat="server" ErrorMessage="*" ControlToValidate="txtMaxAmount" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fee</td>
                                <td>
                                    <asp:TextBox ID="txtFee" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvFee" runat="server" ErrorMessage="*" ControlToValidate="txtFee" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Minimum Fee</td>
                                <td>
                                    <asp:TextBox ID="txtMinimumFee" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvMinimumFee" runat="server" ErrorMessage="*" ControlToValidate="txtMinimumFee" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">VAT & TAX</td>
                                <td>
                                    <asp:TextBox ID="txtVATTAX" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvVATTAX" runat="server" ErrorMessage="*" ControlToValidate="txtVATTAX" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">AIT(%)</td>
                                <td>
                                    <asp:TextBox ID="txtAIT" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvAIT" runat="server" ErrorMessage="*" ControlToValidate="txtAIT" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fees Paid By Bank (%)</td>
                                <td>
                                    <asp:TextBox ID="txtFeesPaidByBank" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvFeesPaidByBank" runat="server" ErrorMessage="*" ControlToValidate="txtFeesPaidByBank" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fess Paid By Initiator(%)</td>
                                <td>
                                    <asp:TextBox ID="txtFeesPaidByInitiator" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvFeesPaidByInitiator" runat="server" ErrorMessage="*" ControlToValidate="txtFeesPaidByInitiator" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fees Paid By Receipent(%)</td>
                                <td>
                                    <asp:TextBox ID="txtFeesPaidByReceipent" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvFeesPaidByReceipent" runat="server" ErrorMessage="*" ControlToValidate="txtFeesPaidByReceipent" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Bank Commission (%)</td>
                                <td>
                                    <asp:TextBox ID="txtBankCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvBankCommission" runat="server" ErrorMessage="*" ControlToValidate="txtBankCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Agent Commission(%)</td>
                                <td>
                                    <asp:TextBox ID="txtAgentCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvAgentCommission" runat="server" ErrorMessage="*" ControlToValidate="txtAgentCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Pool Adjustment (%)</td>
                                <td>
                                    <asp:TextBox ID="txtPoolAdjustment" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvPoolAdjustment" runat="server" ErrorMessage="*" ControlToValidate="txtPoolAdjustment" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Vendor Commission(%)</td>
                                <td>
                                    <asp:TextBox ID="txtVendorCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvVendorCommission" runat="server" ErrorMessage="*" ControlToValidate="txtVendorCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Third Party Commission(%)</td>
                                <td>
                                    <asp:TextBox ID="txtThirdPartyCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvThirdPartyCommission" runat="server" ErrorMessage="*" ControlToValidate="txtThirdPartyCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Channel Commission(%)</td>
                                <td>
                                    <asp:TextBox ID="txtChannelCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvChannelCommission" runat="server" ErrorMessage="*" ControlToValidate="txtChannelCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Agent Operator Commission(%)</td>
                                <td>
                                    <asp:TextBox ID="txtAgentOperatorCommission" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvAgentOperatorCommission" runat="server" ErrorMessage="*" ControlToValidate="txtAgentOperatorCommission" ForeColor="Red" Display="Dynamic" ValidationGroup="CreateMerchant"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Tax Paid By</td>
                                <td>
                                    <asp:DropDownList ID="ddlTaxPaidBy" runat="server">
                                        <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                        <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                        <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fees Paid By</td>
                                <td>
                                    <asp:DropDownList ID="ddlFeesPaidBy" runat="server">
                                        <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                        <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                        <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fees Include Vat/Tax</td>
                                <td>
                                    <asp:DropDownList ID="ddlFeesIncludeVatTax" runat="server">
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="textbox-style">Fees Paid By Mother Merchant
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFeesPaidByMotherMerchant" runat="server">
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text=" Insert " OnClick="btnSave_Click" ValidationGroup="CreateMerchant"/>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnExportReport" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
