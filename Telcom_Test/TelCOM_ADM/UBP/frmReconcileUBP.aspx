<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReconcileUBP.aspx.cs" Inherits="UBP_frmReconcileUBP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 25px;
            font-weight: bold;
            color:White;
            font-size:15px;
        }

        .View_Panel {
            width: 100%;
            background-color: powderblue;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel id="UpdatePanel1" runat="server">
  <contenttemplate>
        <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
            <table style="width: 100%;">
                <tr>
                    <td>Reconcile Utility Bill</td>
                    <td>
                        <asp:Label runat="server" ID="lblMsg"><strong></strong></asp:Label>
                    </td>
                    <td style="width: 50px; text-align: right;">
                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
                            <ProgressTemplate>
                                <%--<img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;--%>
                                <img alt="Loading" src="../Icons/029.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlReconcile" runat="server">
            <table style="border-spacing:10px;">
                <tr>
                    <td>Search By</td>
                    <td>
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Value="AccountNumber" Text="Account Number"></asp:ListItem>
                            <asp:ListItem Value="BillNumber" Text="Bill Number"></asp:ListItem>
                            <asp:ListItem Value="RequestId" Text="Request Id"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <table style="border-spacing:10px;">
                <tr>
                    <td>
                        <asp:GridView ID="gdvReconcileBillDetail" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No data found....." CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="gdvReconcileBillDetail_RowDataBound" OnRowUpdating="gdvReconcileBillDetail_RowUpdating">
                            <Columns>
								<asp:TemplateField HeaderText="Agent Wallet">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgentWallet" runat="server" Text='<%# Bind("SOURCE_ACC_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transaction Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionTime" runat="server" Text='<%# Bind("REQUEST_TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
                                <asp:TemplateField HeaderText="Account Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Bind("ACCNT_NUMBER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillNumber" runat="server" Text='<%# Bind("BILL_NUMBER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Owner Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOwnerCode" runat="server" Text='<%# Bind("OWNER_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Bind("TOTAL_AMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Response Message">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResponseMessage" runat="server" Text='<%# Bind("RESPONSE_MSG_BP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Request Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("SUCCESS_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Success Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSuccessStatus" runat="server" Text='<%# Bind("CAS_REV_STE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" CommandName="update" Text="Reverse" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
      </contenttemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
