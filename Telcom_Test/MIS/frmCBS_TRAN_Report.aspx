<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCBS_TRAN_Report.aspx.cs" Inherits="MIS_frmCBS_TRAN_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>REB Bill Inquiry Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

     <style type="text/css">
       .font_Color
       {
       	color:White;
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
	        background: url(grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
    </style> 

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
            AsyncPostBackTimeout="36000">
        </ajaxToolkit:ToolkitScriptManager>   
    <asp:UpdatePanel id="upnlREBBillPayInquiry" runat="server">
          <Triggers>
                <asp:PostBackTrigger ControlID="btnExportReport" />
            </Triggers>
    <contenttemplate>
        
<div class="row"  style="background-color: royalblue">
               <table>
       <tr>
        <td class="auto-style15">
   
    
            <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Request Id"></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"   ID="requestIdTxt"></asp:TextBox></span></strong>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          
             </td>
                        <td class="auto-style13">
                          <strong><span class="Font_Color" style="color:white;">From</span></strong>  
                            <cc1:GMDatePicker ID="dtpFromDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01"  Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
                            </td>
                            <td class="auto-style14">
                            <strong><span class="Font_Color" style="color:white;">To  </span></strong>
                            <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="130">
                                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                            </cc1:GMDatePicker>
                                &nbsp;
                            
    

 
                        </td>
            <td>
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Value="BDLA" Text=" BDLA"></asp:ListItem>
                            <asp:ListItem Value="WD" Text="WD"></asp:ListItem>
                            <asp:ListItem Value="BDCP" Text="BDCP"></asp:ListItem>
                            <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                        </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                    </td>
                         <td class="auto-style7">
                             <asp:Button
        ID="btnInquiry" runat="server"   Text="Search" UseSubmitBehavior="false" Width="110px" OnClick="btnInquiry_Click"/>&nbsp;&nbsp;
        <asp:Button
        ID="Button1" runat="server"   Text="Show All" UseSubmitBehavior="false" Width="110px" OnClick="btnAll_Click"/>

            <asp:Button
        ID="btnExportReport" runat="server"  Text="Export" UseSubmitBehavior="false" Width="110px" OnClick="btnAllExport_Click"/> 
             &nbsp;&nbsp;           
            
                         </td>
                         
              
                        <td class="auto-style4">  
                               
                            </strong><strong><span class="Font_Color" style="color:white;">
                            &nbsp;
                                <asp:Label ID="lblprocessqty" runat="server"></asp:Label>
                            </span></strong></td>

                            
                          
                                       <td>
                                           <asp:UpdateProgress  ID="upbREBBiilInquiry" runat="server">
                                               <ProgressTemplate>
                                                   
                                                   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                   <img alt="Loading"  src="../resources/images/loading.gif" />
                                                   &nbsp;&nbsp;
                                               </ProgressTemplate>
                                           </asp:UpdateProgress>
                                       </td>
                                       
                    
           </tr>
                   </table>
        </div>




        <asp:GridView ID="gdvDPSReport" runat="server" BorderColor="#E0E0E0" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="20" AllowSorting="True" OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting" EmptyDataText="No data" >
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
</contenttemplate>      
    </asp:UpdatePanel>
    </form>
</body>
</html>
