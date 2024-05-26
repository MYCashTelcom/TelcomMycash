<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBL_Various_MIS_Report_3.aspx.cs"
    Inherits="MIS_frmMBL_Various_MIS_Report_3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MIS Various Report 3</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
        AsyncPostBackTimeout="36000">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:SqlDataSource ID="sdsRank" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="  ">
    </asp:SqlDataSource>
    <%--SelectCommand=" SELECT ACCNT_RANK_ID, RANK_TITEL  FROM ACCOUNT_RANK AR WHERE ACCNT_RANK_ID IN ('120519000000000003','120519000000000004','120519000000000005') ">--%>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <table>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Regitration Datewise Customer Registration And Verification List </strong>
                                </legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>All Distributor</strong>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList runat="server" ID="rbtAllDis" AutoPostBack="True">
                                                <asp:ListItem Value="AllDistributor">All Distributor</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDRank"><strong>Select Rank</strong></asp:Label>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList runat="server" ID="ddlDRank" AutoPostBack="True">   
                      <asp:ListItem Value="0" Selected="True">Select Rank</asp:ListItem>
                      <asp:ListItem Value="1">Distributor</asp:ListItem>
                      <asp:ListItem Value="2">DSE</asp:ListItem>
                      <asp:ListItem Value="3">Agent</asp:ListItem>
                     </asp:DropDownList>--%>
                                            <asp:DropDownList runat="server" ID="ddlDRank" AutoPostBack="True" DataSourceID="sdsRank"
                                                DataValueField="ACCNT_RANK_ID" DataTextField="RANK_TITEL">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDNum"><strong>Wallet ID</strong></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDWalletId" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDW" runat="server" ControlToValidate="txtDWalletId"
                                                ErrorMessage="*" ValidationGroup="D">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDFD"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisFrDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDTD"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDWCL" Text="Show Report" OnClick="btnDWCL_Click"
                                                ValidationGroup="D" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDWCLAllDis" Text="All Distributor Report" OnClick="btnDWCLAllDis_Click" Enabled ="true"    />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Business Collection Report 3</strong></legend>
                                <table style="width: 550px; height: 60px;">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFD"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBCLFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblToD"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBCLToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnBusColl3" Text="Show Report" OnClick="btnBusColl3_Click" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Operator wise SMS Count(For All Service)</strong></legend>
                                <table style="width: 550px; height: 70px;">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFDA"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSMSAllFD" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTDA"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpSMSAllToD" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblOprA"><strong>Select Operator</strong></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpAllOpr">
                                                <asp:ListItem Value="0" Selected="True">Select Operator</asp:ListItem>
                                                <asp:ListItem Value="1">GP</asp:ListItem>
                                                <asp:ListItem Value="2">BL</asp:ListItem>
                                                <asp:ListItem Value="3">Robi</asp:ListItem>
                                                <asp:ListItem Value="4">Airtel</asp:ListItem>
                                                <asp:ListItem Value="5">Teletalk</asp:ListItem>
                                                <asp:ListItem Value="6">CityCell</asp:ListItem>
                                                <asp:ListItem Value="7">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAllOprSMS" Text="Show Report" OnClick="btnAllOprSMS_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Operator and Service Code wise SMS Count</strong></legend>
                                <table style="width: 550PX">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFDSAO"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpOPRandSrCFD" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblToDSAO"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpOPRandSrCToD" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label3"><strong>Select Operator</strong></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpOperator">
                                                <asp:ListItem Value="0" Selected="True">Select Operator</asp:ListItem>
                                                <asp:ListItem Value="+88017">GP</asp:ListItem>
                                                <asp:ListItem Value="+88019">BL</asp:ListItem>
                                                <asp:ListItem Value="+88018">Robi</asp:ListItem>
                                                <asp:ListItem Value="+88016">Airtel</asp:ListItem>
                                                <asp:ListItem Value="+88015">Teletalk</asp:ListItem>
                                                <asp:ListItem Value="+88011">CityCell</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSSer"><strong>Select ServiceCode</strong></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpService">
                                                <asp:ListItem Value="0" Selected="True">Select Service Code</asp:ListItem>
                                                <asp:ListItem Value="1">REG</asp:ListItem>
                                                <asp:ListItem Value="2">MTP</asp:ListItem>
                                                <asp:ListItem Value="3">OTP</asp:ListItem>
                                                <asp:ListItem Value="4">RG</asp:ListItem>
                                                <asp:ListItem Value="5">CCT</asp:ListItem>
                                                <asp:ListItem Value="6">FM</asp:ListItem>
                                                <asp:ListItem Value="7">CN</asp:ListItem>
                                                <asp:ListItem Value="8">SW</asp:ListItem>
                                                <asp:ListItem Value="9">IOTP</asp:ListItem>
                                                <asp:ListItem Value="10">CPIN1</asp:ListItem>
                                                <asp:ListItem Value="11">BI</asp:ListItem>
                                                <asp:ListItem Value="12">QT</asp:ListItem>
                                                <asp:ListItem Value="13">BD</asp:ListItem>
                                                <asp:ListItem Value="14">FT</asp:ListItem>
                                                <asp:ListItem Value="15">TXTFS</asp:ListItem>
                                                <asp:ListItem Value="16">MP</asp:ListItem>
                                                <asp:ListItem Value="17">MYHL</asp:ListItem>
                                                <asp:ListItem Value="18">OTHERS</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnOprASerW" Text="Show Number" OnClick="btnOprASerW_Click" />
                                            <asp:Label runat="server" ID="lblResult" Font-Bold="True" ForeColor="#FF3300" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Customer Transaction Count</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCustTrxFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpCustTrxToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnCustTrx" Text="Show Report" OnClick="btnCustTrx_Click" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Business Collection Report 3 (Deatils)</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBus3WtDisFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBus3WtDisToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnBus3WtDis" Text="Show Report" OnClick="btnBus3WtDis_Click" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Agent Transaction Activity Report 1</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>Select Distributor</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistributor" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="1">All Distributor</asp:ListItem>
                                                <asp:ListItem Value="2">Individual Distributor</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblDisWallet" runat="server" Visible="False">Distributor Wallet</asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDisWallet" runat="server" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>No of Transaction</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTrxCount"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Transaction Amount</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAmonut"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>No of Agent</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtNoOfAgent"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAgActFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpAgActToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAgAct" Text="Show Report" OnClick="btnAgAct_Click" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Agent Activity Report 2</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td style="width: 110px">
                                            <asp:Label ID="lvlAg2NoOfAgt" runat="server"><strong>No Of Agent</strong></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAgtNoOfAgt2" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAgtActv2" runat="server" OnClick="btnAgtActv2_Click" Text="Show Report" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset style="width: 550px">
                                <legend><strong>Distributor(Idividual) Wise Agent Performance Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisPerIFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisPerIToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100" Enabled="False">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Distributor No</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDisWall"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDisPer" Text="Show Report" OnClick="btnDisPer_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
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
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>All Bill Pay Transaction Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBpFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBpToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Account Rank</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountRank" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Select Bill Type</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlBillType">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem Value="DPDC">DPDC</asp:ListItem>
                                                <asp:ListItem Value="DS">DESCO</asp:ListItem>
                                                <asp:ListItem Value="WS">WASA</asp:ListItem>
                                                <asp:ListItem Value="WZ">West Zone</asp:ListItem>
												<asp:ListItem Value="KGDCL">KGDCL</asp:ListItem>
												<asp:ListItem Value="BREB">BREB</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button runat="server" ID="btnBp" Text="Show Report" OnClick="btnBp_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Rank and Bill Type wise Bill Payment</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpRandBtFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpRandBtToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Select Rank</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpBpRank">
                                                <asp:ListItem Value="0">Select Rank</asp:ListItem>
                                                <asp:ListItem Value="120519000000000005">MBL Agent</asp:ListItem>
                                                <asp:ListItem Value="120519000000000006">MBL Customer</asp:ListItem>
                                                <asp:ListItem Value="130914000000000001">MBL New Customer</asp:ListItem>
                                                <asp:ListItem Value="161215000000000004">Paywell Agent</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Select Bill Type</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpBpType">
                                                <asp:ListItem>Select Bill Type</asp:ListItem>
                                                <asp:ListItem Value="DPDC">DPDC</asp:ListItem>
                                                <asp:ListItem Value="DS">DESCO</asp:ListItem>
                                                <asp:ListItem Value="WS">WASA</asp:ListItem>
                                                <asp:ListItem Value="WZ">West Zone</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button runat="server" ID="btnBpRptA" Text="Show Report" OnClick="btnBpRptA_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Bill Payment Report Through WAP and APPS(Distributor)</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisWapFromDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDisWapToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDisWapRpt" Text="Show Report" OnClick="btnDisWapRpt_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Bill Pay Failed and Reverse Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>Type</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpType">
                                                <asp:ListItem Value="0">Select Type</asp:ListItem>
                                                <asp:ListItem Value="1">Failed</asp:ListItem>
                                                <asp:ListItem Value="2">Reverse</asp:ListItem>
                                                <asp:ListItem Value="3">Cancel API Reverse</asp:ListItem>
                                                <asp:ListItem Value="4">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBpVarFDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBpVarToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnBpVarShow" Text="Show Report" OnClick="btnBpVarShow_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Response Status 801, 111</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td colspan="4" align="center">
                                            <strong>Response Status 801, 111</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpResponseFromDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpResponseToDate" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
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
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnResponse801" runat="server" Text="Show Report" OnClick="btnResponse801_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Duplicate Bill Number Count</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="BCDatePickerFrom" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="BCDatePickerTo" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
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
                                            <asp:Button ID="btnDuplicateBillCount" runat="server" Text="Show Report" 
                                                onclick="btnDuplicateBillCount_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Desco Prepaid Meter Bill Collection Report</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDSPrepaidBillCollReportFrom" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpDSPrepaidBillCollReportTo" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
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
                                            <asp:Button ID="btnDSPrepaidMeterBillCollReport" runat="server" Text="Show Report" OnClick="btnDSPrepaidMeterBillCollReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
					<tr>
                        <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Business Collection (NLI)</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBCNliFrom" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBCNliTo" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
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
                                            <asp:Button ID="btnBCNliReport" runat="server" Text="Show Report" OnClick="btnBCNliReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
							<fieldset style="width: 550px">
                                <legend><strong>Utility Bill Pay Commission</strong></legend>
                                <table style="width: 550px">
                                    <tr>
                                        <td>
                                            <strong>From Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBPCFromDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <strong>To Date</strong>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="dtpBPCToDate" runat="server" CalendarTheme="Silver"
                                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
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
                                            <asp:Button ID="btnBillPayComReport" runat="server" Text="Show Report" OnClick="btnBillPayComReport_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
						</td>
                    </tr>
                    <tr>
                         <td>
                            <fieldset style="width: 550px">
                                <legend><strong>Business Collection Report(Prime Islami Life Ins)</strong></legend>
                                <table style="width: 550px; height: 60px;">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label1"><strong>From Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="PILILatePicker1" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label2"><strong>To Date</strong></asp:Label>
                                        </td>
                                        <td>
                                            <cc1:GMDatePicker ID="PILILatePicker2" runat="server" CalendarTheme="Silver" DateFormat="dd-MMM-yyyy"
                                                MinDate="1900-01-01" Style="position: relative" TextBoxWidth="100">
                                                <CalendarTitleStyle BackColor="#FFFFC0" />
                                            </cc1:GMDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnPILILReport" Text="Show Report" OnClick="btnPILILReport_Click" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDWCL" />
                <asp:PostBackTrigger ControlID="btnBusColl3" />
                <asp:PostBackTrigger ControlID="btnAllOprSMS" />
                <asp:PostBackTrigger ControlID="btnCustTrx" />
                <asp:PostBackTrigger ControlID="btnAgAct" />
                <asp:PostBackTrigger ControlID="btnAgtActv2" />
                <asp:PostBackTrigger ControlID="btnBus3WtDis" />
                <asp:PostBackTrigger ControlID="btnDisPer" />
                <asp:PostBackTrigger ControlID="btnDisComm" />
                <asp:PostBackTrigger ControlID="btnBp" />
                <asp:PostBackTrigger ControlID="btnBpRptA" />
                <asp:PostBackTrigger ControlID="btnDisWapRpt" />
                <asp:PostBackTrigger ControlID="btnBpVarShow" />
                <asp:PostBackTrigger ControlID="btnResponse801" />
                <asp:PostBackTrigger ControlID="btnDWCLAllDis" />
                <asp:PostBackTrigger ControlID="btnDuplicateBillCount" />
				<asp:PostBackTrigger ControlID="btnDSPrepaidMeterBillCollReport" />
				<asp:PostBackTrigger ControlID="btnBCNliReport" />
				<asp:PostBackTrigger ControlID="btnBillPayComReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
