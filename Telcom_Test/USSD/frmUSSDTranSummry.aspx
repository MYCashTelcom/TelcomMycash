<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUSSDTranSummry.aspx.cs" Inherits="USSD_frmUSSDTranSummry" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USSD Tranasction Summary</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div>
      <div>
     <fieldset style="border-color: #FFFFFF;width:500px; height:120px;">
                      <legend><strong style="color: #666666">&nbsp;&nbsp;Select Account</strong></legend>
                      
                      
                      <table>
                                     <tr>
                                       <td>
                                    <asp:RadioButtonList ID="rbtnAllDateRange" runat="server" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1">Date Range</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                    <td style="font-size:10px;">
                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date :" ></asp:Label>                                   
                                     <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy " MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="80" Font-Size="X-Small">
                                       <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                                     </cc1:GMDatePicker>
                                   To Date :
                                    <strong>
                                                    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="80" Font-Size="X-Small">
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
                                                    </strong>
                                      </td>
                                     </tr>
                                    </table>
                                    
                                    <asp:Button ID="btnViewReport" runat="server" Text="Show Report" OnClick="btnViewReport_Click" />
                       </fieldset>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
