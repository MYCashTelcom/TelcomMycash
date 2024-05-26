<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBL_Various_MIS_Report_3_1.aspx.cs"
    Inherits="MIS_frmMBL_Various_MIS_Report_3_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MIS Various Report 3(New)</title>
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
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxtoolkit:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" enablepartialrendering="True"
        asyncpostbacktimeout="36000">
    </ajaxtoolkit:toolkitscriptmanager>
    <asp:updatepanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Width="100%">
                    <table style="width: 100%" align="right">
                        <tr>
                            <td align="left">
                                <asp:Label runat="server" ID="panelQ" Text="Various MIS Report 3"></asp:Label>
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
                
                <table style="vertical-align:top">
                    <tr style="vertical-align:top;">
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Distributor(Individual) Commission Report </strong></legend>
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
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDisComm" Text="Show Report" OnClick="btnDisComm_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            
                             <fieldset style="width: 550px">
                                <legend><strong>Bank/SRL Commission Statement for WEST ZONE</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDCSFrDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDCSToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Select Channel Type</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlChannelType" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="1">Apps & WAP</asp:ListItem>
                                                <asp:ListItem Value="2">USSD(Robi, Airtel, BL)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDCSReport" runat="server" Text="Show Report" OnClick="btnDCSReport_Click" />
											<asp:Button ID="btnDCSPBazarReport" runat="server" Text="PBazar Report" OnClick="btnDCSPBazarReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            
                        </td>
                        <td>
                        
                            <fieldset style="width: 550px">
                                                <legend><strong>WEST ZONE, EDU-SMART Debit/Credit Successful and Failed Report</strong></legend>
                                                <table style="width: 550px">
                                                    <tr>
                                                        <td>
                                                            <strong>From Date</strong>
                                                        </td>
                                                        <td>
                                                            <cc1:GMDatePicker ID="dtpDisDCFrDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                                            </cc1:GMDatePicker>
                                                        </td>
                                                        <td>
                                                            <strong>To Date</strong>
                                                        </td>
                                                        <td>
                                                            <cc1:GMDatePicker ID="dtpDisDCToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                                            </cc1:GMDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Organization</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrganization" runat="server">
                                                                <asp:ListItem Value="UBPED">Edu-Smart</asp:ListItem>
                                                                <asp:ListItem Value="UWZP">West Zone</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <strong>Status</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                                                <asp:ListItem Value="S">Debit/Credit Success</asp:ListItem>
                                                                <asp:ListItem Value="F">Debit/Credit Failed</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:Button ID="btnDisDCReport" runat="server" Text="Show Report" OnClick="btnDisDCReport_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <fieldset style="width: 550px">
                        <legend><strong>Distributor Related Information For Bangladesh Bank</strong></legend>
                        <table style="width: 550px">
                            <tr>
                             <td>
                                <strong>From Date</strong>   
                             </td>   
                             <td>
                                <cc1:GMDatePicker ID="dtpDRIFrDate" runat="server" CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                        TextBoxWidth="100">
                                        <calendartitlestyle backcolor="#FFFFC0" />
                                        </cc1:GMDatePicker>  
                             </td>   
                             <td>
                               <strong>To Date</strong>  
                             </td>   
                             <td>
                                <cc1:GMDatePicker ID="dtpDRIToDate" runat="server" CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                        TextBoxWidth="100">
                                        <calendartitlestyle backcolor="#FFFFC0" />
                                        </cc1:GMDatePicker> 
                             </td>   
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnDRISearch" runat="server" Text="Show Report" onclick="btnDRISearch_Click"/>
                                    <asp:Button ID="btnDisList" runat="server" Text="Distributor Info" onclick="btnDisList_Click" />
                                
                                </td>
                                <td></td>
                                <td>
                                    
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                        <legend><strong>MBL Agent Transaction Report of Previous Month For Bangladesh Bank</strong></legend>
                        <table style="width: 550px">
                            <tr>
                             <td>
                                <strong>From Date</strong>   
                             </td>   
                             <td>
                                <cc1:GMDatePicker ID="dtpARIFrDate" runat="server" CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                        TextBoxWidth="100">
                                        <calendartitlestyle backcolor="#FFFFC0" />
                                        </cc1:GMDatePicker>  
                             </td>   
                             <td>
                               <strong>To Date</strong>  
                             </td>   
                             <td>
                                <cc1:GMDatePicker ID="dtpARIToDate" runat="server" CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                        TextBoxWidth="100">
                                        <calendartitlestyle backcolor="#FFFFC0" />
                                        </cc1:GMDatePicker> 
                             </td>   
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnARISearch" runat="server" Text="New Report" onclick="btnARISearch_Click" />
                                     <asp:Button ID="btnAgtInfo" runat="server" Text="Agent Info" onclick="btnAgtInfo_Click" />                           
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnARISearchOld" runat="server" Text="Show Old Report" onclick="btnARISearchOld_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                        <legend><strong>Distributor Commission Comparison Report</strong></legend>
                        <table style="width: 550px">
                            <tr>
                             <td>
                                <strong>First Month</strong>   
                             </td>   
                             <td>
                                <asp:DropDownList runat="server" ID="ddlFirstMonth">
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
                                <asp:DropDownList runat="server" ID="ddlFirstYear">
                                    <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                             </tr>
                             <tr>
                             <td>
                               <strong>Second Month</strong>  
                             </td>   
                             <td>
                                <asp:DropDownList runat="server" ID="ddlSecondMonth">
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
                                <asp:DropDownList runat="server" ID="ddlSecondYear">
                                    <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                </asp:DropDownList>
                             </td>   
                            </tr>
                            <tr>
                                <td>
                                    <strong>Third Month</strong>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlThirdMonth">
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
                                <asp:DropDownList runat="server" ID="ddlThirdYear">
                                    <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnDCCReport" runat="server" Text="Show Report" 
                                        onclick="btnDCCReport_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>Microtech Agent(Gemini, Standard) Transaction Performance Report </strong></legend>
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
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>Institute Payment Detail</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lbldtInsPaymentFrom"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtInsPaymentFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbldtInsPaymentTo"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtInsPaymentTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnInsPaymentDetail" runat="server" Text="Show Report" 
                                            onclick="btnInsPaymentDetail_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>Current Balance by OM Rank</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblBalanceFrom"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtBalanceFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblBalanceTo"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtBalanceTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnOMBalanceDetail" runat="server" Text="Show Report" OnClick="btnOMBalanceDetail_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                    </tr>
                   <tr>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>OM Transaction Details</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label3"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtOtdFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label4"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtOtdTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnOmTranDetail" runat="server" Text="Show Report" OnClick="btnOmTranDetail_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>OM Merchant Commission</strong></legend>
                            <table style="width: 550px">
                                <%--<tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label5"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtOmcFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label6"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtOmcTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOmCommission" runat="server" Text="Show Report" OnClick="btnOmCommission_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>PBazar Customer Registration Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label7"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPcRegReportFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label8"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPcRegReportTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnPBazarCusRegReport" runat="server" Text="Show Report" OnClick="btnPBazarCusRegReport_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>PBazar Account Hierarchy Report</strong></legend>
                            <table style="width: 550px">
                                <%--<tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label9"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtAccountHierarchyFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label10"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtAccountHierarchyTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPBazarAccountHierarchy" runat="server" Text="Show Report" OnClick="btnPBazarAccountHierarchy_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>Rank & Wallet wise Balance Details</strong></legend>
                            <table style="width: 550px">
                                <%--<tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label9"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtRWBalDetailFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label10"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtRWBalDetailTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblRankList"><strong>Rank Name</strong></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAccountRankList" runat="server">
											<asp:ListItem Value="120519000000000003" Text="MBL Distributor"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000006" Text="PBazar Distributor"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000007" Text="PBazar DSE"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000008" Text="PBazar Agent"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRankWalletWiseBalance" runat="server" Text="Show Report" OnClick="btnRankWalletWiseBalance_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>PBazar Summary Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblPBazarSumReportFr"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPBazarSumReportFr" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPBazarSumReportTo"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPBazarSumReportTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnPBazarSummaryReport" runat="server" Text="Show Report" OnClick="btnPBazarSummaryReport_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>PBAZAR Transaction Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label9"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPBazarTranReportFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label10"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtPBazarTranReportTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnPBazarTranReport" runat="server" Text="Show Report" OnClick="btnPBazarTranReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>PBAZAR KYC Updated Report</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblKYCUpdateFrom"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtKYCUpdateFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblKYCUpdateTo"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtKYCUpdateTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:Label runat="server" ID="Label5"><strong>Rank Name</strong></asp:Label>--%>
                                    </td>
                                    <td>
                                        <%--<asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem Value="180128000000000006" Text="PBazar Distributor"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000007" Text="PBazar DSE"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000008" Text="PBazar Agent"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnKYCUpdatedReport" runat="server" Text="Show Report" OnClick="btnKYCUpdatedReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>OM Transaction Summary Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblOMTSRFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtOMTSRFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblOMTSRTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtOMTSRTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<asp:Label runat="server" ID="Label5"><strong>Rank Name</strong></asp:Label>--%>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem Value="180128000000000006" Text="PBazar Distributor"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000007" Text="PBazar DSE"></asp:ListItem>
                                            <asp:ListItem Value="180128000000000008" Text="PBazar Agent"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnOMTranSummary" runat="server" Text="Show Report" OnClick="btnOMTranSummary_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>MBL and Aamra Bill Pay Commission Report(Prepaid)</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblBPCPrepaidFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBPCPrepaidFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBPCPrepaidTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBPCPrepaidTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Account Rank</td>
                                        <td>
                                            <asp:DropDownList ID="ddlReportType" runat="server">
                                                <asp:ListItem Value="Distributor" Text="Distributor"></asp:ListItem>
                                                <asp:ListItem Value="Agent" Text="Agent"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnPrepaidBillPayComm" runat="server" Text="Show Report" OnClick="btnPrepaidBillPayComm_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Active / Inactive customer and agent details</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblaicaDetailFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCADetailFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblaicaDetailTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCADetailTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Account Rank</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCASearch" runat="server">
                                                <asp:ListItem Value="C" Text="Customer"></asp:ListItem>
                                                <asp:ListItem Value="A" Text="Agent"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnAICAStatus" runat="server" Text="Show Report" OnClick="btnAICAStatus_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Active / Inactive customer balance</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblAICBFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAICBFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblAICBTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAICBTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                            <asp:Button ID="btnAICB" runat="server" Text="Show Report" OnClick="btnAICB_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Cash in report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblCashInFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCashInFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCashInTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCashInTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                            <asp:Button ID="btnCashInReport" runat="server" Text="Show Report" OnClick="btnCashInReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Cash out report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblCashOutFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCashOutFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCashOutTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCashOutTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                            <asp:Button ID="btnCashOutReport" runat="server" Text="Show Report" OnClick="btnCashOutReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Bank Deposit report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblBdFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBdFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBdT"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBdTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                            <asp:Button ID="btnBankDeposit" runat="server" Text="Show Report" OnClick="btnBankDeposit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Salary disbursement report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblSdFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSdFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSdTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSdTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSalaryDisburse" runat="server" Text="Show Report" OnClick="btnSalaryDisburse_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Merchant Payment report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblMpFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMpFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblMpTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMpTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnMerchantPayment" runat="server" Text="Show Report" OnClick="btnMerchantPayment_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Mobile Topup report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblMtpFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMtpFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblMtpTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMtpTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnMobileTopup" runat="server" Text="Show Report" OnClick="btnMobileTopup_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Business Collection Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblBcFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBcFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBcTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBcTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnBusinessCollection" runat="server" Text="Show Report" OnClick="btnBusinessCollection_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Utility Transaction Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblUtFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpUtFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblUtTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpUtTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnUtilityTransaction" runat="server" Text="Show Report" OnClick="btnUtilityTransaction_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Fund Transfer report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFtFrom"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpFtFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblFtTo"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpFtTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnFundTransfer" runat="server" Text="Show Report" OnClick="btnFundTransfer_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Sub Merchant FM Transaction Detail Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label5"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSMFMTDRFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label6"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSMFMTDRTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSubMerchantFMTranReport" runat="server" Text="Show Report" OnClick="btnSubMerchantFMTranReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Maxis report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label11"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMaxisFrom" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label12"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpMaxisTo" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkMM" runat="server" Text="Merchant"/> 

                                        </td>
                                        <td>
                                            <asp:Button ID="btnMaxis" runat="server" Text="Show Report" OnClick="btnMaxis_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                       <%-- Added by chamak on 12/09/2021--%>
                        <td>
                            <fieldset style="width: 550px">
                            <legend><strong>MAXIS RANKWISE BALANCE REPORT</strong></legend>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label13"><strong>From Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="fromDateMaxisTotal" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                            MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="true">
                                            <CalendarTitleStyle BackColor="#FFFFC0" />
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label14"><strong>To Date</strong></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:GMDatePicker ID="toDateMaxisTotal" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <asp:Button ID="btnMaxisTotal" runat="server" Text="Show Report" OnClick="btnMaxisTotal_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        </tr>
                </table>
                
                
                
                
            </ContentTemplate>
            
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDisComm" />  
                <asp:PostBackTrigger ControlID="btnDisDCReport" />
                <asp:PostBackTrigger ControlID="btnDCSReport" /> 
                <asp:PostBackTrigger ControlID="btnARISearch" />
	            <asp:PostBackTrigger ControlID="btnDRISearch" />             
	            <asp:PostBackTrigger ControlID="btnDisList" />
	            <asp:PostBackTrigger ControlID="btnAgtInfo" />
	            <asp:PostBackTrigger ControlID="btnARISearchOld" />
	            <asp:PostBackTrigger ControlID="btnDCCReport" />
	            <asp:PostBackTrigger ControlID="btnTmToAgtReport" />
	            <asp:PostBackTrigger ControlID="btnTmToAgtReportDetails" />
	            <asp:PostBackTrigger ControlID="btnTmToAgtList" />
	            <asp:PostBackTrigger ControlID="btnInsPaymentDetail" />
	            <asp:PostBackTrigger ControlID="btnOMBalanceDetail" />
				<asp:PostBackTrigger ControlID="btnOmTranDetail" />
                <asp:PostBackTrigger ControlID="btnOmCommission" />
                <asp:PostBackTrigger ControlID="btnPBazarCusRegReport" />
				<asp:PostBackTrigger ControlID="btnPBazarAccountHierarchy" />
                <asp:PostBackTrigger ControlID="btnRankWalletWiseBalance" />
				<asp:PostBackTrigger ControlID="btnPBazarSummaryReport" />
				<asp:PostBackTrigger ControlID="btnPBazarTranReport" />
                <asp:PostBackTrigger ControlID="btnKYCUpdatedReport" />
				<asp:PostBackTrigger ControlID="btnOMTranSummary" />
				<asp:PostBackTrigger ControlID="btnPrepaidBillPayComm" />
				<asp:PostBackTrigger ControlID="btnDCSPBazarReport" />
				<asp:PostBackTrigger ControlID="btnDCSPBazarReport" />
                <asp:PostBackTrigger ControlID="btnAICAStatus" />
                <asp:PostBackTrigger ControlID="btnAICB" />
                <asp:PostBackTrigger ControlID="btnCashInReport" />
                <asp:PostBackTrigger ControlID="btnCashOutReport" />
                <asp:PostBackTrigger ControlID="btnBankDeposit" />
                <asp:PostBackTrigger ControlID="btnSalaryDisburse" />
                <asp:PostBackTrigger ControlID="btnMerchantPayment" />
                <asp:PostBackTrigger ControlID="btnMobileTopup" />
                <asp:PostBackTrigger ControlID="btnUtilityTransaction" />
				<asp:PostBackTrigger ControlID="btnBusinessCollection" />
				<asp:PostBackTrigger ControlID="btnFundTransfer" />
				<asp:PostBackTrigger ControlID="btnSubMerchantFMTranReport" />
				<asp:PostBackTrigger ControlID="btnMaxis" />
                <asp:PostBackTrigger ControlID="btnMaxisTotal" />


            </Triggers>
            
            
            
        </asp:updatepanel>
    </form>
</body>
</html>
