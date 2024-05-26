<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNLIPaymentDetail.aspx.cs" Inherits="MIS_frmNLIPaymentDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MIS Various Report 4</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }

        .View_Panel {
            width: 550px;
        }

        .Inser_Panel {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }

        .style1 {
            width: 328px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
            AsyncPostBackTimeout="36000">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Width="100%">
                    <table style="width: 100%; text-align: right;">
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label runat="server" ID="panelQ" Text="NLI Transaction Detail"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 50px;">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                    DynamicLayout="True">
                                    <ProgressTemplate>
                                        <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="width: 70px;">Search By</td>
                        <td>
                            <asp:DropDownList ID="ddlSearchType" runat="server" Width="200">
                                <asp:ListItem Value="">--Select Type--</asp:ListItem>
                                <asp:ListItem Value="PROJECT_CODE">Project Code</asp:ListItem>
                                <asp:ListItem Value="PREMIUM_TYPE">Premium Type</asp:ListItem>
                                <asp:ListItem Value="SOURCE_ACCNT_NO">Source Account</asp:ListItem>
                                <asp:ListItem Value="DESTINATION_ACCNT_NO">Destination Account</asp:ListItem>
                                <asp:ListItem Value="CLINT_NAME">Destination Office</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px;"></td>
                        <td>
                            <asp:Panel ID="pnlSearchText" runat="server">
                                <asp:TextBox ID="txtSearchText" runat="server" Width="200"></asp:TextBox>
                                <asp:DropDownList ID="ddlOfficeList" runat="server"></asp:DropDownList>
								<asp:DropDownList ID="ddlServiceList" runat="server"></asp:DropDownList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlDateRange" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 70px;">
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpNliFrDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpNliToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button runat="server" ID="btnNLICollectionDetail" Text="Show Report" OnClick="btnNLICollectionDetail_Click" />
                            <asp:Button runat="server" ID="btnExportReport" Text="Export Report" OnClick="btnExportReport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gdvReportDetails" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data found....">
                                <Columns>
                                    <asp:TemplateField HeaderText="Destination Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestinationAccount" runat="server" Text='<%# Bind("DESTINATION_ACCNT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destination Account Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestinationAccountName" runat="server" Text='<%# Bind("DESTINATION_ACCNT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranDate" runat="server" Text='<%# Bind("REQUEST_TIME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("REQUEST_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectCode" runat="server" Text='<%# Bind("PROJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("SERVICE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Premium Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPremiumType" runat="server" Text='<%# Bind("PREMIUM_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSourceAccount" runat="server" Text='<%# Bind("SOURCE_ACCNT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source Account Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSourceAccountName" runat="server" Text='<%# Bind("SOURCE_ACCNT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportReport" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
