<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTrafficChannelChart.aspx.cs" Inherits="frmTrafficChannelChart"%>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Traffic Channel Performance</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script language=javascript>
    function CallPrint(strid)
    {
         var prtContent = document.getElementById(strid);
         var WinPrint =
        window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,sta­tus=0');
         WinPrint.document.write(prtContent.innerHTML);
         WinPrint.document.close();
         WinPrint.focus();
         WinPrint.print();
         WinPrint.close();
         prtContent.innerHTML=strOldOne;        

    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<div style="BACKGROUND-COLOR: royalblue"><strong>
    <span class="Font_Color">&nbsp;From Date</span>
               <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="130">
                                <calendartitlestyle  />
                            </cc1:GMDatePicker>
   <span class="Font_Color"> To Date</span>
    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
    &nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />&nbsp;
        <asp:button id="imgBtnPrint" onclientclick="javascript:window.print();" runat="Server" text="Print"/>
        </strong>
        </div>
      <div>
<table class="sampleTable">
				<tr>
					<td class="tdchart">
                        <asp:chart ID="Chart1" runat="server" backcolor="#F3DFC1" 
                            BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid" 
                            BorderWidth="2" Height="296px" 
                            ImageLocation="~\TempImages\ChartPic_#SEQ(300,3)" imagetype="Png" 
                            Palette="BrightPastel" Width="955px">
                            <legends>
                                <asp:legend Alignment="Center" BackColor="Transparent" Docking="Bottom" 
                                    Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" LegendStyle="Row" 
                                    Name="Default">
                                </asp:legend>
                            </legends>
                            <borderskin skinstyle="Emboss" />
                            <chartareas>
                                <asp:chartarea BackColor="Transparent" BackSecondaryColor="White" 
                                    BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" 
                                    ShadowColor="Transparent">
                                    <area3dstyle inclination="15" isclustered="False" isrightangleaxes="False" 
                                        perspective="10" rotation="10" wallwidth="0" />
                                    <position height="73" width="89.43796" x="4.824818" y="10" />
                                    <axisy islabelautofit="False" isstartedfromzero="False" 
                                        linecolor="64, 64, 64, 64">
                                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                                        <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
                                        <majortickmark enabled="False" interval="1" />
                                    </axisy>
                                    <axisx islabelautofit="False" linecolor="64, 64, 64, 64">
                                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" interval="1" />
                                        <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
                                        <majortickmark enabled="False" interval="1" />
                                    </axisx>
                                </asp:chartarea>
                            </chartareas>
                        </asp:chart>
                    </td>
					<td valign="top">
						<table class="controls" cellpadding="4">
							<tr>
								<td class="label48"></td>
								<td><asp:datagrid id="DataGrid" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
										<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
										<headerstyle font-bold="True" horizontalalign="Center" forecolor="White" backcolor="Gainsboro"></headerstyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</div>
</contenttemplate>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
