<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNLIDeviceInfoActiveDeactive.aspx.cs"
    Inherits="COMMON_frmNLIDeviceInfoActiveDeactive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Device Active/De Active</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Top_Panel
        {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            background-color: powderblue;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
        .txt_width
        {
            width: 250px;
        }
    </style>
</head>
<body style="background-color: lightgrey; font-family: Times New Roman; font-size: 12px;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<asp:SqlDataSource ID="sdsRequest" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         SelectCommand="" 
         >
     </asp:SqlDataSource>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
                <table width="100%">
                    <tr>
                        <td>
                            Manage Device Active/De Active
                        </td>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                <ProgressTemplate>
                                    <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                <table>
                    <tr>
                        <td>
                            &nbsp; Select Device Status
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="ALL" Selected="True">All Device</asp:ListItem>
                                <asp:ListItem Value="I">De Active</asp:ListItem>
                                <asp:ListItem Value="A">Active</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Search Terminal Serial No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSerach" runat="server" CausesValidation="false" Text="Search" OnClick="btnSerach_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnRefresh" runat="server" Text="Refresh" OnClick="BtnRefresh_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnExportReport" runat="server" Text="Export Report" CausesValidation="false" 
                                onclick="btnExportReport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="gdvRequest" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                AutoGenerateColumns="False" BorderColor="#E0E0E0" BorderStyle="None" CssClass="mGrid"
                DataKeyNames="ACCNT_POS_ID, ACTIVE_STATUS" OnPageIndexChanging="gdvRequest_PageIndexChanging"
                OnRowCancelingEdit="gdvRequest_RowCancelingEdit" OnRowEditing="gdvRequest_RowEditing"
                OnRowUpdating="gdvRequest_RowUpdating" OnRowUpdated="gdvRequest_RowUpdated" PagerStyle-CssClass="pgr"
                PageSize="10">
                <Columns>
                    <asp:TemplateField HeaderText="Account Id" SortExpression="ACCNT_POS_ID">
                        <%--  <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ACCNT_POS_ID") %>'></asp:Label>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACCNT_POS_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terminal Name" SortExpression="TERMINAL_NAME">
                        <%--  <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TERMINAL_NAME") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("TERMINAL_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terminal Serial No." SortExpression="TERMINAL_SERIAL_NO">
                        <%-- <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("TERMINAL_SERIAL_NO") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("TERMINAL_SERIAL_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="ACTIVE STATUS" SortExpression="ACTIVE_STATUS">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ACTIVE_STATUS") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("ACTIVE_STATUS") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Active Status" SortExpression="ACTIVE_STATUS">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatusE" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("ACTIVE_STATUS") %>'>
                                <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatusI" runat="server" AppendDataBoundItems="true" Enabled="false"
                                SelectedValue='<%# Bind("ACTIVE_STATUS") %>'>
                                <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Channel Type" SortExpression="CHANNEL_TYPE">
                        <%-- <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CHANNEL_TYPE") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("CHANNEL_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Office Name" SortExpression="OFFICE_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOfficeName" runat="server" Text='<%# Bind("OFFICE_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOfficeName" runat="server" Text='<%# Bind("OFFICE_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zone Name" SortExpression="ZONE_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZoneName" runat="server" Text='<%# Bind("ZONE_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblZoneName" runat="server" Text='<%# Bind("ZONE_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cashier Name" SortExpression="CASHIER_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCashierName" runat="server" Text='<%# Bind("CASHIER_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCashierName" runat="server" Text='<%# Bind("CASHIER_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation" SortExpression="DESIGNATION">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDesignation" runat="server" Text='<%# Bind("DESIGNATION") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("DESIGNATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NLIC Cashier ID No." SortExpression="NLIC_CASHIER_ID_NO">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNlicCIdNo" runat="server" Text='<%# Bind("NLIC_CASHIER_ID_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNlicCIdNo" runat="server" Text='<%# Bind("NLIC_CASHIER_ID_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile No." SortExpression="MOBILE_NO">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMobileNo" runat="server" Text='<%# Bind("MOBILE_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MOBILE_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IMEI 1" SortExpression="IMEI_NO_1">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtImei1" runat="server" Text='<%# Bind("IMEI_NO_1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblImei1" runat="server" Text='<%# Bind("IMEI_NO_1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IMEI 2" SortExpression="IMEI_NO_2">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtImei2" runat="server" Text='<%# Bind("IMEI_NO_2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblImei2" runat="server" Text='<%# Bind("IMEI_NO_2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Info Entry Date" SortExpression="NLI_INFO_ENTRY_DATE">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtInfoEntry" runat="server" Text='<%# Bind("NLI_INFO_ENTRY_DATE", "{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblInfoEntry" runat="server" Text='<%# Bind("NLI_INFO_ENTRY_DATE", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update" />
                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            <fieldset style="width: 450px; padding: 10px;">
                <legend><strong>Add Device Information</strong></legend>
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:HiddenField ID="hfAccountPosId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Terminal Name</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTerminalName" CssClass="txt_width" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Terminal Serial No.</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTerminalSerial" CssClass="txt_width" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Active Status</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlActiveStatus" runat="server">
                                <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Channel Type</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtChannelType" CssClass="txt_width" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Office Name</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOfficeName" CssClass="txt_width" runat="server"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOfficeName" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Zone Name</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtZoneName" CssClass="txt_width" runat="server"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtZoneName" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Cashier Name</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCashierName" CssClass="txt_width" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Designation</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesignation" CssClass="txt_width" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>NLIC Cashier ID</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNLICCashierID" CssClass="txt_width" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Mobile No.</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobileNo" CssClass="txt_width" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>IMEI 1</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtIMEI1" CssClass="txt_width" runat="server"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtIMEI1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>IMEI 2</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtIMEI2" CssClass="txt_width" runat="server"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtIMEI2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnAddNewDevice" runat="server" Text=" Save " 
                                onclick="btnAddNewDevice_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportReport" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
