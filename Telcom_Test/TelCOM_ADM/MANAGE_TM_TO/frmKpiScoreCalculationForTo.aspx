<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKpiScoreCalculationForTo.aspx.cs"
    Inherits="MANAGE_TM_TO_frmKpiScoreCalculationForTo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPI Score Calculation for TO</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
   
    <style type="text/css">
        .Font_Color
        {
            color: White;
        }
        .Top_Panel
        {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            width: 100%;
            background-color: powderblue;
        }
        .GridViewClass
        {
            width: 100%;
            background-color: #fff;
            margin: 1px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            text-align: left;
        }
        .GridViewClass td
        {
            padding: 2px;
            border: solid 1px #c1c1c1;
            color: #717171;
            font-size: 11px;
        }
        .GridViewClass th
        {
            padding: 4px 2px;
            color: #fff;
            background: url(../COMMON/grd_head1.png) activecaption repeat-x 50% top;
            border-left: solid 0px #525252;
            font-size: 11px;
        }
        .style3
        {
            width: 137px;
        }
    </style>
   
   
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
                <table style="width: 100%" align="right">
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="panelQ" Text="KPI Score Calculation of TO"></asp:Label>
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
            <table style="width: 400px">
                <tr>
                    <td class="style3">
                        <strong>Account No</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToAccNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <strong>Select Month and Year</strong>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpMonth">
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
                        <asp:DropDownList runat="server" ID="drpYear">
                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdfActiveAgent" runat="server" />
                        <asp:HiddenField ID="hdfAgtTrx" runat="server" />
                        <asp:HiddenField ID="hdfFromDate" runat="server" />
                        <asp:HiddenField ID="hdfToDate" runat="server" />
                    </td>
                    <td>
                        &nbsp;<asp:Button runat="server" ID="btnShow" Text="Show " OnClick="btnShow_Click"  />
                    </td>
                </tr>
            </table>
            <div id="divCalculation" runat="server" Visible="False">
                <table style="width: 95%" class="GridViewClass">
                    <tr>
                        <td>
                            <strong>Sl No</strong>
                        </td>
                        <td>
                            <strong>KPI Parameters</strong>
                        </td>
                        <td>
                            <strong>Benchmark</strong>
                        </td>
                        <td>
                            <strong>Target Given</strong>
                        </td>
                        <td>
                            <strong>If Achieved</strong>
                        </td>
                        <td>
                            <strong>Achieved Marks</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>1</strong>
                        </td>
                        <td>
                            <strong>Customer Registration</strong>
                            (200%)</td>
                        <td>
                            <asp:TextBox runat="server" ID="bench1" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target1" Enabled="False"></asp:TextBox>
                            </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved1" Enabled="False"></asp:TextBox>
                            </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvCAcq" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td>
                            <strong>2</strong>
                        </td>
                        <td>
                            <strong>MY DpS Account Acquisition</strong>
                            (200%)</td>
                        <td>
                            <asp:TextBox runat="server" ID="bench2" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target2" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved2" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvDpsAcq" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <strong>2</strong>
                        </td>
                        <td>
                            <strong>Transaction Amount</strong>
                            (200%)</td>
                        <td>
                            <asp:TextBox runat="server" ID="bench3" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target3" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved3" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvTrxAmt" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>3</strong>
                        </td>
                        <td>
                            <strong>Active Agent No.</strong>
                        </td>
                        <td valign="top">
                            <asp:TextBox runat="server" ID="bench4" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target4" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved4" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvActAgt" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td>
                            <strong>4</strong>
                        </td>
                        <td>
                            <strong>Corporate Collection</strong>
                            (200%)</td>
                        <td>
                            <asp:TextBox runat="server" ID="bench5" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target5" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved5" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvLftRfd" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            <strong>5</strong>
                        </td>
                        <td>
                            <strong>Lifting Amount</strong>
                            </td>

                        <td>
                            <asp:TextBox runat="server" ID="bench6" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target6" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved6" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvLfting" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                   

                     <tr>
                        <td>
                            <strong>6</strong>
                        </td>
                        <td>
                            <strong>Utility Bill</strong>
                            </td>
                        <td>
                            <asp:TextBox runat="server" ID="bench7" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target7" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved7" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvUtBll" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>

                    <%--<tr>
                        <td>
                            <strong>6</strong>
                        </td>
                        <td>
                            <strong>Compliance</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="bench6" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target6" Enabled="False"></asp:TextBox>
                            50(min reg)</td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved6" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvComp" Enabled="False"></asp:TextBox>
                            &nbsp;&gt; 69%</td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <strong>7</strong>
                        </td>
                        <td>
                            <strong>Visibility</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="bench7" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="target7" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="achieved7" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMAcvVisi" Enabled="False" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                      <td></td>  
                      <td></td>  
                      <td></td>  
                      <td></td>  
                      <td>
                         <strong>Total Value: </strong> 
                      </td>  
                      <td>
                          <asp:TextBox runat="server" ID="txtResult" Enabled="False"></asp:TextBox>
                      </td>  
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
