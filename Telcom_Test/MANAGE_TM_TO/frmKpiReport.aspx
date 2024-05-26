<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKpiReport.aspx.cs" Inherits="MANAGE_TM_TO_frmKpiReport" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kpi Report TM TO</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
       .Font_Color
       {
       	color:White;
       	}
       	.Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         }
         .View_Panel
         {         	
         	 width:100%;
         	 background-color:powderblue;       	
         }
       	
       	.GridViewClass { width: 100%; background-color: #fff; margin: 1px 0 10px 0; 
        border: solid 1px #525252; border-collapse:collapse;
            text-align: left;
        }
            .GridViewClass td { padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
            .GridViewClass th
        {
	        padding: 4px 2px;
	        color: #fff;
	        background: url(../COMMON/grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
        .style1
        {
            width: 157px;
        }
        .style2
        {
            width: 173px;
        }
        .auto-style1 {
            height: 20px;
        }
        .auto-style2 {
            width: 50px;
            height: 20px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
   <contenttemplate>   
    <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
     <table style="width: 100%" align="right" >
       <tr>
         <td align="left" class="auto-style1">
           <asp:Label runat="server" ID="panelQ" Text="Kpi Report TM-TO"></asp:Label> 
         </td>
         <td align="right" class="auto-style1">
           <asp:Label ID="lblMsg" runat="server" ></asp:Label>  
         </td>  
         <td align="right" class="auto-style2">
           <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
             <ProgressTemplate>
               <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;
             </ProgressTemplate>
            </asp:UpdateProgress> 
         </td>  
        </tr>
     </table> 
    </asp:Panel> 

       <table style="width: 100%">
           <tr>
               <td>
                   <fieldset>
                            <legend><strong>KPI Report TO(Utility)</strong></legend>
                            <table >                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthToUt">
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
                                        <asp:DropDownList runat="server" ID="drpYearToUt">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                           
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
                                    <td></td>
                                    <td></td>
                                    <td>

                                    </td>
                                    
                                    
                                    
                                    <td>
                                        <asp:Button ID="btnTOUt" runat="server" Text="Show Report" OnClick="btnTOUt_Click"
                                            Enabled="true" />
                                       
                                    </td>

                                </tr>
                            </table>
                        </fieldset>
               </td>
               <td>
                   <fieldset>
                            <legend><strong>KPI Report TO(Non-Utility)</strong></legend>
                            <table >                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthToNu">
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
                                        <asp:DropDownList runat="server" ID="drpYearToNu">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                           
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
                                    <td></td>
                                    <td></td>
                                    <td>

                                    </td>
                                    
                                    
                                    
                                    <td>
                                        <asp:Button ID="btnTONu" runat="server" Text="Show Report" OnClick="btnTONu_Click"
                                            Enabled="true" />
                                       
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
               </td>
           </tr>
           <tr>
               <td>
                   <fieldset>
                            <legend><strong>KPI Report TM(Utility)</strong></legend>
                            <table >                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthTMut">
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
                                        <asp:DropDownList runat="server" ID="drpYearTmUt">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                           
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
                                    <td></td>
                                    <td></td>
                                    <td>

                                    </td>
                                    
                                    
                                    
                                    <td>
                                        <asp:Button ID="btnTMUt" runat="server" Text="Show Report" OnClick="btnTMUt_Click"
                                            Enabled="true" />
                                       
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
               </td>

               <td>
                   <fieldset>
                            <legend><strong>KPI Report TM(Non-Utility)</strong></legend>
                            <table >                                <tr>
                                    <td>
                                        <strong>Select Month</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpMonthTMNU">
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
                                        <asp:DropDownList runat="server" ID="frpYearTMNu">
                                            <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                           
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
                                    <td></td>
                                    <td></td>
                                    <td>

                                    </td>
                                    
                                    
                                    
                                    <td>
                                        <asp:Button ID="btnTMNu" runat="server" Text="Show Report"                                           
                                             OnClick="btnTMNu_Click"
                                            Enabled="true" />
                                       
                                    </td>
                                </tr>
                            </table>
                        </fieldset>

               </td>
           </tr>
       </table>

    </contenttemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnTOUt" />
            <asp:PostBackTrigger ControlID="btnTONu" /> 
            <asp:PostBackTrigger ControlID="btnTMUt" />  
            <asp:PostBackTrigger ControlID="btnTMNu" />  
            
        </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
