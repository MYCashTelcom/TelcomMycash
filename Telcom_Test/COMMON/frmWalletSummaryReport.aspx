<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmWalletSummaryReport.aspx.cs" Inherits="COMMON_frmKYCReport" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>KYC Report</title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />
     </head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            <table></table>
            
            <div style="BACKGROUND-COLOR: royalblue">
               <strong>
                 <span style="COLOR: white">
                  <table style="width: 100%">
                  <tr style="width: 100%">
                   <td align="left" style="width: 200px">
                     <asp:Label runat="server" ID="lblTitle"><strong>Wallet Summary Report</strong></asp:Label>  
                   </td>
                   <td align="left" style="width: 200px">
                    
                   </td>
                   <td align="right">
                     <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>  
                   </td>   
                  </tr>    
                  </table>
                </span>
               </strong>
            </div>
            <div>
              <fieldset style="width: 380px; float: left; height: 62px;">
                  <legend><strong>Wallet Summary Report(For All)</strong></legend>
                  <table style="width: 300px">
                      <tr>
                    <td style="width: 200px">
                        <asp:Label runat="server" ID="l"><strong>Wallet Summary Report(For All)</strong></asp:Label>
                    </td>  
                    <td>
                      <asp:Button ID="btnReport" runat="server"  Text="Report" onclick="btnReport_Click"/>       
                    </td>  
                  </tr>
                  </table>
              </fieldset>  
            </div>
            <div>
              <fieldset style="width: 400px; ">
               <legend><strong>Rankwise Wallet Summary Report</strong></legend>
               <table style="width: 400px">
                 <tr style="width: 400px">
                  <td>
                  <asp:Label runat="server" ID="lblFdate"><strong>From Date</strong></asp:Label>  
                  </td>
                  <td>
                     <cc1:GMDatePicker ID="dtpFromDate" runat="server" CalendarTheme="Silver" 
                      DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                      TextBoxWidth="100">
                      <calendartitlestyle backcolor="#FFFFC0" />
                     </cc1:GMDatePicker> 
                  </td>
                   <td>
                    <asp:Label runat="server" ID="lblTdate"><strong>To Date</strong></asp:Label>  
                  </td>
                  <td>
                     <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                      DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                      TextBoxWidth="100">
                      <calendartitlestyle backcolor="#FFFFC0" />
                     </cc1:GMDatePicker> 
                  </td>   
                 </tr>
                 <tr>
                  <td></td>
                  <td>
                    <asp:Button runat="server" ID="btnCustomizeRpt" Text="Show Report" onclick="btnCustomizeRpt_Click"/>    
                  </td>
                  <td></td>
                  <td></td>
                    
                 </tr>  
               </table>   
              </fieldset>  
            </div>
         </ContentTemplate>
       </asp:UpdatePanel>
    </form>
</body>
</html>
