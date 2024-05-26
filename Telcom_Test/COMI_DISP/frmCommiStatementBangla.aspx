<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCommiStatementBangla.aspx.cs"
    Inherits="COMI_DISP_frmCommiStatementBangla" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script language="javascript" type="text/javascript">
        function ShowProgressBar() {
            var iFrame = document.getElementById('Iframe1');
            iFrame.src = "frmShowMessage.aspx";
        }

         

    </script>

    <title>Bangla Statement</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style2
        {
            height: 11px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <%--<div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>--%>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Bangla Commission Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblResult" runat="server" Font-Bold="True"></asp:Label>
                </span></strong>
            </div>
            <table width="100%">
                <tr style="background-color: lightgrey">
                    <td>
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lbltype" runat="server" Font-Size="10pt" Text="Commission Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdblbcrtype" Font-Size="10pt" runat="server" AutoPostBack="True"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="CO">Compliance</asp:ListItem>
                                        <asp:ListItem Value="RE">Refill &amp; Usage</asp:ListItem>
                                        <asp:ListItem Value="US">Refill WC</asp:ListItem>
                                        <asp:ListItem Value="BO">Both</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="160">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                                        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="
                                        SELECT INITCAP(COMMISSION_TYPE)||'-'||' Disbursement Date '||SUBSTR(COMI_MASTER_NAME,'14')|| ' Activation Date'||' ['||TO_char(COMI_STRAT_DATE,upper('DD-MM-YYYY'))||']'||' TO'
                                        ||' ['||TO_char(COMI_END_DATE,upper('DD-MM-YYYY'))||']'DISBURSEMENT_DATE,COMI_MASTER_ID,COMI_STRAT_DATE,COMI_END_DATE 
                                        FROM COMMISSION_MASTER ORDER BY COMI_MASTER_ID DESC"></asp:SqlDataSource>
                                                                        </td>
                            </tr>
                            <tr>
                                <td align="right" width="160">
                                    <asp:Label ID="Label1" runat="server" Text="Activition From Date"></asp:Label>
                                </td>
                                <td>
                                    <cc1:GMDatePicker ID="txtFromDate" runat="server" DateFormat="dd-MMM-yyyy" MinDate="1980-01-04">
                                    </cc1:GMDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="160">
                                    <asp:Label ID="Label2" runat="server" Text="Activition To Date"></asp:Label>
                                </td>
                                <td>
                                    <cc1:GMDatePicker ID="txtToDate" runat="server" DateFormat="dd-MMM-yyyy" MinDate="1980-01-04">
                                    </cc1:GMDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right" width="160">
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblfilter" runat="server" Font-Size="10pt" Text="Statement Folder Type"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:RadioButtonList ID="rdblbcrOption" runat="server" AutoPostBack="True" CssClass="alt"
                                        Font-Size="10pt" OnSelectedIndexChanged="rdblbcrOption_SelectedIndexChanged"
                                        RepeatLayout="Flow" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="RG" Selected="True">Region </asp:ListItem>
                                        <asp:ListItem Value="DS">Distributor </asp:ListItem>
                                        <asp:ListItem Value="RS">RSP </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" rowspan="3" valign="middle" width="160">
                                    <font size="2">Region Code & Name</font><br />
                                    <br />
                                    <font size="2">Distributor Code & Name</font><br />
                                    <br />
                                    <font size="2">RSP Code & Name</font>
                                </td>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                            Width="146px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;<asp:Button ID="btnLoadData" runat="server" Text="Load Data" OnClick="btnLoadData_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlDistributer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistributer_SelectedIndexChanged"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:DropDownList ID="ddlRSPCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRSPCode_SelectedIndexChanged"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnZone" runat="server" OnClick="btnZone_Click" OnClientClick="javascript:ShowProgressBar()"
                            Text="Report Generate" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <iframe id="Iframe1" runat="server" frameborder="0" src="Blank.aspx" height="30"
                            width="300" scrolling="no" style="border-style: none"></iframe>
                    </td>
                </tr>
            </table>
            </td> </tr> </table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
