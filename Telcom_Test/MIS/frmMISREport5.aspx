<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMISREport5.aspx.cs" Inherits="MIS_frmMISREport5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MIS Various Report 5</title>
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
            width: 550px;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
        .style1
        {
            width: 134px;
        }
        .style2
        {
            width: 76px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
        AsyncPostBackTimeout="36000">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand=" ">
    </asp:SqlDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Width="100%">
                <table style="width: 100%" align="right">
                    <tr>
                        <td align="left" class="style1">
                            <asp:Label runat="server" ID="panelQ" Text="Various MIS Report 5"></asp:Label>
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
                            <legend><strong>Date wise TM-TO Transaction Summary Report </strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblDFD"><strong>Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpTmToFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnTmToSumm" Text="Show Report" runat="server" OnClick="btnTmToSumm_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset style="width: 550px">
                            <legend><strong>TM-TO-Distributor Hierarchy Report </strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button Text="Show Report" runat="server" ID="btnTmToDisHierarchy" OnClick="btnTmToDisHierarchy_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>Branch wise Customer Registration Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBBrnFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <strong>To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBBrnToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Select Branch</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpBankBranch" DataSourceID="sdsBranch" DataValueField="BANK_BRNCH_ID"
                                            DataTextField="BRANCH_NAME">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnBrnchregRpt" Text="Show Report" OnClick="btnBrnchregRpt_Click"
                                            Enabled="false" />
                                        <asp:Button runat="server" ID="btnAllBrnchregRpt" Text="All Branch Report" OnClick="btnAllBrnchregRpt_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>Agent Transaction Performance Report(TM-TO wise) </strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label1"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpTmToAgtFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label2"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpTmToAgtToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnTmToAgtReport" runat="server" Text="Show Report" OnClick="btnTmToAgtReport_Click" />
                                        <asp:Button ID="btnTmToAgtReportDetails" runat="server" Text="Show Details" OnClick="btnTmToAgtReportDetails_Click"
                                            Enabled="true" />
                                        <asp:Button ID="btnTmToAgtList" runat="server" Text="Show List" OnClick="btnTmToAgtList_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>TO Monthly Achievement Report(Registration)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpRgMonth">
                                            <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong>Select Year</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpRgYear">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label4"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToRgAcvFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label3"><strong>Current Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToRgAcvToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                        <asp:Button ID="btnToRgReport" runat="server" Text="Show Report" OnClick="btnToRgReport_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>TO Monthly Achievement Report(MyDPS)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthMydps">
                                            <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong>Select Year</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpYearMyDps">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label5"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToMyDpsAcvFromDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label6"><strong>Current Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToMyDpsAcvToDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                        <asp:Button ID="btnToDpsReport" runat="server" Text="Show Report" OnClick="btnToDpsReport_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>TO Monthly Achievement Report(Lifting)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthLifting">
                                            <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong>Select Year</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpYearLifting">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label7"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToLiftingAcvFromDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label8"><strong>Current Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToLiftingAcvToDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                        <asp:Button ID="btnToLiftingReport" runat="server" Text="Show Report" OnClick="btnToLiftingReport_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>TO Monthly Achievement Report(Corporate Collection)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthCorpColl">
                                            <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong>Select Year</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpYearCorpColl">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label9"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToCorpAcvFromDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label10"><strong>Current Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToCorpAcvToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                        <asp:Button ID="btnToCorpReport" runat="server" Text="Show Report" OnClick="btnToCorpReport_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>TO Monthly Achievement Report(Agent Transaction Amount)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthAgtTrxAmt">
                                            <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong>Select Year</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpYearAgtTrxAmt">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label11"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToAgtTrxAmtFromDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label12"><strong>Current Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpToAgtTrxAmtToDate" runat="server" CalendarTheme="Silver"
                                            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100"
                                            Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                        <asp:Button ID="btnToAgtTrxReport" runat="server" Text="Show Report" OnClick="btnToAgtTrxReport_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>Transaction wise Distributor and Other Rank Balance</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpDisBalFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <strong>To Date </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpDisBalToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDis_other" runat="server" Text="Distributor-Other Rank Balance"
                                            OnClick="btnDis_other_Click" Enabled="true" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDis_DSE" runat="server" Text="Distributor-DSE Balance" OnClick="btnDis_DSE_Click"
                                            Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>Distributor Transaction Summary Report(TM-TO wise) </strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>From Date </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpTrxSummFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <strong>To Date </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpTrxSummToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDisTrxSumm" runat="server" Text="Show Report" OnClick="btnDisTrxSumm_Click" />
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>Distributor-DSE Balance Report(TM-TO wise) </strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td class="style2">
                                        <strong>Select Rank </strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpRank" runat="server">
                                            <asp:ListItem Value="00">Select Rank</asp:ListItem>
                                            <asp:ListItem Value="DIS">Distributor</asp:ListItem>
                                            <asp:ListItem>DSE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <strong></strong>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDisDSEBal" runat="server" OnClick="btnDisDSEBal_Click" Text="Show Report" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                    
                    <fieldset style="width: 550px">
                            <legend><strong>Branch wise CN, CCT Transaction Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>
                                            From Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBrnchCnCctFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>
                                            To Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBrnchCnCctToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnBrnchCnCct" runat="server" Text="Show Report" onclick="btnBrnchCnCct_Click" />
                                    
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                    
                    </td>
                </tr>
                
                <tr>
                <td>
                    
                    <fieldset style="width: 550px">
                            <legend><strong>TM-TO wise DPS Pending Status Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>
                                            From Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpPendingFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>
                                            To Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpPendingToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnDpsPending" runat="server" Text="Show Report" onclick="btnDpsPending_Click" />
                                    
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                
                </td>
                <td>
                    
                    <fieldset style="width: 550px">
                            <legend><strong>TM-TO and Opening Date wise DPS Balance Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>
                                            From Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpDpsBalFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>
                                            To Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpDpsBalToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnDpsBalance" runat="server" Text="Show Report" onclick="btnDpsBalance_Click" Enabled="true"/>
                                    
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                
                
                </td>
                </tr>
                
                <tr>
                    <td>
                    
                        <fieldset style="width: 550px">
                            <legend><strong>Distributor Commission Report(Bill Pay Channel Wise)</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <strong>
                                            From Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBpDisCommFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>
                                            To Date
                                        </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpBpDisCommToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnDpdcDsUssd" runat="server" onclick="btnDpdcDsUssd_Click" 
                                            Text="DPDC/DESCO USSD" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnWSAppsWap" runat="server" onclick="btnWSAppsWap_Click" 
                                            Text="Wasa Apps/Wap" />
                                        </td>
                                </tr>
                            </table>
                            </fieldset>
                    
                    
                    
                    </td>
                    
                    <td>
                        <fieldset style="width: 550px">
                            <legend><strong>MBL Customer Transaction Count Report</strong></legend>
                            <table style="width: 550px">
                                
                                <tr>
                                    <td>
                                        <strong>Reg. From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpCustRegFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <strong>Reg. To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpCustRegToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <strong>Trx. From Date </strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtp1stMonthTrxFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <strong>Trx. To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtp1stMonthTrxToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                
                                
                                
                                <tr>
                                    <td>
                                        <strong>Distributor Wallet</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDisCusWallet" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rbAllDis" AutoPostBack="True">
                                                <asp:ListItem Value="AllDistributor">All Distributor</asp:ListItem>
                                            </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCustTRxCount" runat="server" OnClick="btnCustTRxCount_Click" Text="Show Report" />
                                    </td>
                                </tr>
                                
                                
                                
                                
                            </table>
                    </fieldset>
                    </td>
                </tr>
                
                <tr>
                
                <td>
                
                    <fieldset style="width: 550px">
                            <legend><strong>D2D Agentwise Report</strong></legend>
                            <table style="width: 550px">
                                
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpAgetWiseFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpAgetWiseToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnD2dAgentWiseRpt" runat="server" OnClick="btnD2dAgentWiseRpt_Click" Text="Show Report" />
                                </td>
                            
                            </tr>
                            
                            </table>
                        </fieldset>
                </td>
                <td>
                
                    <fieldset style="width: 550px">
                            <legend><strong>D2D Agent Performance Report</strong></legend>
                            <table style="width: 550px">
                                
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpD2DAgentPerfRptFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpD2DAgentPerfRptToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnD2DAgentPerfRpt" runat="server" OnClick="btnD2DAgentPerfRpt_Click" Text="Show Report" />
                                </td>
                            
                            </tr>
                            
                            </table>
                        </fieldset>
                </td>
                </tr>
                <tr>
                     <td>
               <%-- Added by Chamak on 22/09/21--%>
                    <fieldset style="width: 550px">
                            <legend><strong>TM OM BILL PAYMENT REPORT</strong></legend>
                            <table style="width: 550px">
                                
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="tmOmV2FrmDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="tmOmV2ToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnTmOmV2Report" runat="server" OnClick="btnTmOmV2Report_Click" Text="Show Report" />
                                </td>
                            
                            </tr>
                                 <%--added by chamak on 10.12.2021--%>
                
                            
                            </table>
                        </fieldset>

                </td>
                
                <td>
                
                    <fieldset style="width: 550px">
                            <legend><strong>CORPORATE COLLECTION REPORT</strong></legend>
                            <table style="width: 550px">
                                
                                <tr>
                                    <td>
                                        <strong>From Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="FromDateDisAgent" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                                    <td>
                                        <strong>To Date</strong>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="ToDateDisAgent" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    
                                    </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnDisAgent" runat="server" OnClick="btnDisAgent_Click" Text="Show Report" />
                                </td>
                            
                            </tr>
                            
                            </table>
                        </fieldset>
                </td>

                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnTmOmV2Report" />
            <asp:PostBackTrigger ControlID="btnTmToSumm" />
            <asp:PostBackTrigger ControlID="btnBrnchregRpt" />
            <asp:PostBackTrigger ControlID="btnTmToAgtReport" />
            <asp:PostBackTrigger ControlID="btnToRgReport" />
            <asp:PostBackTrigger ControlID="btnToDpsReport" />
            <asp:PostBackTrigger ControlID="btnToLiftingReport" />
            <asp:PostBackTrigger ControlID="btnToCorpReport" />
            <asp:PostBackTrigger ControlID="btnToAgtTrxReport" />
            <asp:PostBackTrigger ControlID="btnTmToDisHierarchy" />
            <asp:PostBackTrigger ControlID="btnTmToAgtReportDetails" />
            <asp:PostBackTrigger ControlID="btnAllBrnchregRpt" />
            <asp:PostBackTrigger ControlID="btnDis_other" />
            <asp:PostBackTrigger ControlID="btnDis_DSE" />
            <asp:PostBackTrigger ControlID="btnTmToAgtList" />
            <asp:PostBackTrigger ControlID="btnDisTrxSumm" />
            <asp:PostBackTrigger ControlID="btnDisDSEBal" />
            <asp:PostBackTrigger ControlID="btnBrnchCnCct" />
            <asp:PostBackTrigger ControlID="btnDpsPending" />
            <asp:PostBackTrigger ControlID="btnDpsBalance" />
            <asp:PostBackTrigger ControlID="btnDpdcDsUssd" />
            <asp:PostBackTrigger ControlID="btnWSAppsWap" />
            <asp:PostBackTrigger ControlID="btnCustTRxCount" />
            <asp:PostBackTrigger ControlID="btnD2dAgentWiseRpt" />
            <asp:PostBackTrigger ControlID="btnD2DAgentPerfRpt" />
                   <%--added by chamak on 10.12.2021--%>
            <asp:PostBackTrigger ControlID="btnDisAgent" />
            
            
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
