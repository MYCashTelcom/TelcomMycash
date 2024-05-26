<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBL_Various_MIS_Report_4.aspx.cs"
    Inherits="MIS_frmMBL_Various_MIS_Report_4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    <table style="width: 100%" align="right">
                        <tr>
                            <td align="left">
                                <asp:Label runat="server" ID="panelQ" Text="Various MIS Report 4"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                            <td align="right" style="width: 50px;">
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


                <table style="width: 1100px">
                    <tr>
                        <td>

                            <fieldset style="width: 550px">
                                <legend><strong>MobiCash Agent Point Cashout List </strong>
                                </legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMobiFrDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMobiToDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnMobCCT" Text="Show Report" OnClick="btnMobCCT_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>


                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>MobiCash Agent Point Cashin List</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCnMobiFrDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCnMobiToDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCn" runat="server" Text="Show Report"
                                                OnClick="btnCn_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>

                        </td>

                    </tr>

                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Distributor wise Corporate Agent List</strong></legend>

                                <table style="width: 550px">
                                    <tr>

                                        <td>
                                            <asp:Button ID="btnDisCorpList" runat="server" Text="Show Report"
                                                OnClick="btnDisCorpList_Click" />
                                        </td>
                                    </tr>

                                </table>

                            </fieldset>



                        </td>

                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Paywell Agent Performance Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpPwFromDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpPwToDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative"
                                                TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Show Report" OnClick="Button1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>


                        </td>

                    </tr>

                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Manage Service Fee Report</strong></legend>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>Branch</strong>
                                        </td>
                                        <td>
                                            <strong>Bank List</strong>
                                        </td>
                                        <td>
                                            <strong>Service List</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBranch" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBankList" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlService" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Account Rank</strong>
                                        </td>
                                        <td>
                                            <strong>Channel Name</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlAccountRank" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlChannelName" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnServiceReport" runat="server" Text="Show Report" OnClick="btnServiceReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Distributor(Individual) Transaction Amount Report </strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisCommFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisCommToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Distributor Wallet</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDisCommWallet"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDisComm" Text="Show Report" OnClick="btnDisAmt_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>All Distributor Transaction Report </strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAllDisAmtFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAllDisAmtTDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAllDisAmt" Text="Show Report"
                                                OnClick="btnAllDisAmt_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>MBL Distributor Bill Pay Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpADBPReportFr" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpADBPReportTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnADBPReport" Text="Show Report" OnClick="btnADBPReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Service wise Distributor Commission Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth" runat="server">
                                                <asp:ListItem Text="January" Value="JAN"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="FEB"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="MAR"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="APR"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="MAY"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="JUN"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="JUL"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="AUG"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="SEP"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="OCT"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="NOV"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="DEC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlYear" runat="server">
                                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlServiceCode" runat="server">
                                                <asp:ListItem Text="Cash In" Value="CN"></asp:ListItem>
                                                <asp:ListItem Text="Cash Out" Value="CCT"></asp:ListItem>
                                                <asp:ListItem Text="Utility Payment" Value="UBP"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnServiceWiseDisComm" Text="Show Report" OnClick="btnServiceWiseDisComm_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>MBL Rank Wise Balance Detail</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtRankWiseBalanceFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtRankWiseBalanceTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnRankWiseBalance" Text="Show Report" OnClick="btnRankWiseBalance_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>CBS Transaction Detail</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtCBSTranFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtCBSTranTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnCBSTransactionDetail" Text="Show Report" OnClick="btnCBSTransactionDetail_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>QR Transaction Detail</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAppTranFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAppTranTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAppTranDetail" Text="Show Report" OnClick="btnAppTranDetail_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>OM Submerchant Setup Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpOSSFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpOSSToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnOmSubMerchantSetupDetail" Text="Show Report" OnClick="btnOmSubMerchantSetupDetail_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Transcom Transaction Detail</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtTranscomFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtTranscomTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btndtTranscom" Text="Show Report" OnClick="btndtTranscom_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
						<td>
							<fieldset style="width: 550px">
                                <legend><strong>Provita Corporate Transaction Detail</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtProvitaCorporateFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtProvitaCorporateTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btndtProvitaCorporate" Text="Show Report" OnClick="btndtProvitaCorporate_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
						</td>
						<td>
							<fieldset style="width: 550px">
                                <legend><strong>Agent Fund Management Details</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAFMDetailFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAFMDetailTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Agent Number</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtAgentNumber" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAgentFundManagementReport" Text="Show Report" OnClick="btnAgentFundManagementReport_Click" />
                                            <asp:Button runat="server" ID="btnDPDCPAgent" Text="DPDC Agent Report" OnClick="btnDPDCPAgent_Click"  />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
						</td>
					</tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Agent Transaction Summary</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpATSFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpATSToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Agent Number</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtATSAgentNumber" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAgentTransactionSummary" Text="Show Report" OnClick="btnAgentTransactionSummary_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>PBazar Current Balance Details</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td><strong>Distributor</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtPBazarDistributor" Enabled="false"  runat="server"></asp:TextBox>
                                        </td>
                                        <td><asp:CheckBox ID="chkAllDistributor" AutoPostBack="true" runat="server" Checked="true" OnCheckedChanged="chkAllDistributor_CheckedChanged" />All</td>
                                        <td>
                                            <asp:Button runat="server" ID="btnPBazarReport" Text="Show Report" OnClick="btnPBazarReport_Click"  />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                      
                      
                    </tr>
                    <tr>

    <td>
                         <td>
                            <fieldset style="width: 550px">
                                <legend><strong>DPDC BILL COL REPORT</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="GMDatePicker1" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="GMDatePicker2" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="DPDCbtn" Text="Show Report" OnClick="DPDCbtn_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                            </td>
                    </tr>
                </table>




            </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="btnMobCCT" />
                <asp:PostBackTrigger ControlID="btnCn" />
                <asp:PostBackTrigger ControlID="btnDisCorpList" />
                <asp:PostBackTrigger ControlID="Button1" />
                <asp:PostBackTrigger ControlID="btnServiceReport" />
                <asp:PostBackTrigger ControlID="btnDisComm" />
                <asp:PostBackTrigger ControlID="btnAllDisAmt" />
                <asp:PostBackTrigger ControlID="btnADBPReport" />
				<asp:PostBackTrigger ControlID="btnServiceWiseDisComm" />
				<asp:PostBackTrigger ControlID="btnRankWiseBalance" />
				<asp:PostBackTrigger ControlID="btnCBSTransactionDetail" />
				<asp:PostBackTrigger ControlID="btnAppTranDetail" />
				<asp:PostBackTrigger ControlID="btnOmSubMerchantSetupDetail" />
                <asp:PostBackTrigger ControlID="btndtTranscom" />
				<asp:PostBackTrigger ControlID="btndtProvitaCorporate" />
				<asp:PostBackTrigger ControlID="btnAgentFundManagementReport" />
				<asp:PostBackTrigger ControlID="btnAgentTransactionSummary" />
				<asp:PostBackTrigger ControlID="btnPBazarReport" />
                <asp:PostBackTrigger ControlID="btnDPDCPAgent" />
                 <asp:PostBackTrigger ControlID="DPDCbtn" />
            </Triggers>

        </asp:UpdatePanel>
    </form>
</body>
</html>
