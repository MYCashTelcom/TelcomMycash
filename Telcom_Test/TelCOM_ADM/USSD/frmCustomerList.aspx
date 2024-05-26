<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCustomerList.aspx.cs" Inherits="USSD_frmCustomerList" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer List</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div style="BACKGROUND-COLOR: royalblue">
      <strong>
       <span style="COLOR: white"> 
             Manage Customer List
       </span>    
      </strong>
     </div>
     From Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:     
     <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
          DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
          TextBoxWidth="110">
          <calendartitlestyle backcolor="Black" Font-Size="X-Small" />
     </cc1:GMDatePicker>  
     <br />    
     To Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
     <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
          DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
          TextBoxWidth="110" >
          <calendartitlestyle  backcolor="#FFFFC0"  Font-Size="X-Small" />
     </cc1:GMDatePicker> 
     <br />
     Customer Type :
     <asp:DropDownList ID="ddlCustomerType" runat="server">
         <asp:ListItem Value="0">Customer List</asp:ListItem>
         <asp:ListItem Value="1">Customer List(Valued)</asp:ListItem>
     </asp:DropDownList>
     <br />
     Report Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:  
     <asp:DropDownList ID="ddlReportType" runat="server">
         <asp:ListItem>CSV</asp:ListItem>
     </asp:DropDownList>
     <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" 
            onclick="btnGenerateReport_Click" />
    </ContentTemplate>
     <Triggers>
     <asp:PostBackTrigger  ControlID="btnGenerateReport" />
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
