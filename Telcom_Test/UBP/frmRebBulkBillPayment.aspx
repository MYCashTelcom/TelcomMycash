<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRebBulkBillPayment.aspx.cs" Inherits="COMMON_frmRebBulkBillPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>REB Bulk Bill Pay</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 25px;
            font-weight: bold;
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

        .style1 {
            width: 166px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form2" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
                    <table width="100%">
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="REB Bulk Bill Payment"></asp:Label>
                                &nbsp;
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td align="right">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSearch" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="PendingBillCount" runat="server" Width="70px" Height="22px" autocomplete="off" placeholder="0" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnPendingBill" runat="server" Text="Refresh" Width="100px"
                                    OnClick="btnCheckPendingBill" Height="26px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtBalance" runat="server" Width="150px" Height="22px" autocomplete="off" style="margin-left:660px" ReadOnly="true"></asp:TextBox>
                                <asp:Button ID="btnCheckBalance" runat="server" Text="Check Balance" Width="100px"
                                    OnClick="btnCheckBalance_Click" Height="26px"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlGridView" runat="server">
                    <asp:GridView ID="gdvBillPendingList" runat="server" AutoGenerateColumns="False" DataKeyNames="UBP_REB_BBP_ID"
                        CssClass="mGrid" PageSize="10" AllowPaging="true" Width="80%"
                        EmptyDataText="No data found....."
                        OnPageIndexChanging="gdvBillPendingList_PageIndexChanging">
                        
                        <Columns>
                            <asp:TemplateField HeaderText="UBP_REB_BBP_ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_BBP_ID" Visible="false" runat="server" Text='<%#Bind("UBP_REB_BBP_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UBP_REB_BBP_ID" ControlStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_BBP_ID" runat="server" Text='<%#Bind("UBP_REB_BBP_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UBP_REB_ACCOUNT_ID" ControlStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_ACCOUNT_ID" runat="server" Text='<%#Bind("UBP_REB_ACCOUNT_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="gvtxtBranchName" runat="server" Text='<%#Bind("BR_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UBP_REB_ACCOUNT_STATUS" ControlStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_ACCOUNT_STATUS" runat="server" Text='<%#Bind("UBP_REB_ACCOUNT_STATUS") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="gvtxtEmployeeName" runat="server" Text='<%#Bind("EMP_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UBP_REB_REMARKS" ControlStyle-Width="300">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_REMARKS" runat="server" Text='<%#Bind("UBP_REB_REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="gvtxtEmployeeMobile" runat="server" Text='<%#Bind("EMP_MOBILE") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UBP_REB_PURPOSE" ControlStyle-Width="120">
                                <ItemTemplate>
                                    <asp:Label ID="lblUBP_REB_PURPOSE" runat="server" Text='<%#Bind("UBP_REB_PURPOSE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="gvtxtDesignation" runat="server" Text='<%#Bind("DESIGNATION") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IS_INQUIRED" ControlStyle-Width="120">
                                <ItemTemplate>
                                    <asp:Label ID="lblIS_INQUIRED" runat="server" Text='<%#Bind("IS_INQUIRED") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IS_PAID" ControlStyle-Width="75">
                                <ItemTemplate>
                                    <asp:Label ID="lblIS_PAID" runat="server" Text='<%#Bind("IS_PAID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStatus" runat="server" Text='<%#Bind("BR_STATUS") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="Gray" ForeColor="White" />
                        <EditRowStyle BorderStyle="Outset" />
                    </asp:GridView>
                </asp:Panel>



                <br />

                <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Bill List for Payment"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <table>
                        <tr>
                             <td>
                                <asp:TextBox ID="PaidBillList" runat="server" Width="100px" Height="22px" autocomplete="off" placeholder="0" ReadOnly="true"></asp:TextBox>
                            </td>
                              <td>
                                <asp:Button ID="btnPaidBill" runat="server" Text="Refresh" Width="100px"
                                    OnClick="btnCheckPaidBill" Height="26px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLPaymentMonth" runat="server" Width="175px" Height="26px" style="margin-left:645px">
                                    <asp:ListItem Value="">--select--</asp:ListItem>
                                    <asp:ListItem Value="1">Paid with Current Month</asp:ListItem>
                                    <asp:ListItem Value="2">Paid with Previous Month</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td>
                                <asp:Button ID="Button2" runat="server" Text="Paid" Width="100px"
                                    OnClick="btnPaid_Click" Height="26px" OnClientClick="return confirm('Are you sure you want to paid Bulk Bill??');"/>
                                  <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                                            Text=""></asp:Label>
                                 <asp:Label ID="LabelSucc" runat="server" 
                                            Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="gdvPaidBillList" runat="server" AutoGenerateColumns="False" DataKeyNames="UTILITY_TRAN_ID"
                        CssClass="mGrid" PageSize="10" AllowPaging="true" Width="80%"
                        EmptyDataText="No data found....."
                        OnPageIndexChanging="gdvPaidBillList_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="UTILITY_TRAN_ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUTILITY_TRAN_ID" Visible="false" runat="server" Text='<%#Bind("UTILITY_TRAN_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UTILITY_TRAN_ID" ControlStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblUTILITY_TRAN_ID" runat="server" Text='<%#Bind("UTILITY_TRAN_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ACCOUNT_NUMBER" ControlStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblACCOUNT_NUMBER" runat="server" Text='<%#Bind("ACCOUNT_NUMBER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BILL_NUMBER" ControlStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblBILL_NUMBER" runat="server" Text='<%#Bind("BILL_NUMBER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BILL_MONTH" ControlStyle-Width="120">
                                <ItemTemplate>
                                    <asp:Label ID="lblBILL_MONTH" runat="server" Text='<%#Bind("BILL_MONTH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BILL_YEAR" ControlStyle-Width="120">
                                <ItemTemplate>
                                    <asp:Label ID="lblBILL_YEAR" runat="server" Text='<%#Bind("BILL_YEAR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REQUEST_ID" ControlStyle-Width="120">
                                <ItemTemplate>
                                    <asp:Label ID="lblREQUEST_ID" runat="server" Text='<%#Bind("REQUEST_ID") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PBS_NAME" ControlStyle-Width="130">
                                <ItemTemplate>
                                    <asp:Label ID="lblPBS_NAME" runat="server" Text='<%#Bind("PBS_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="TOTAL_BILL_AMOUNT" ControlStyle-Width="75">
                                <ItemTemplate>
                                    <asp:Label ID="lblTOTAL_BILL_AMOUNT" runat="server" Text='<%#Bind("TOTAL_BILL_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                        </Columns>
                        <HeaderStyle BackColor="Gray" ForeColor="White" />
                        <EditRowStyle BorderStyle="Outset" />
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
