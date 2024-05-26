<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMISServiceWiseTransaction.aspx.cs" Inherits="MIS_frmMISServiceWiseTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >        
        .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	 width:500px;       	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }     
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:SqlDataSource ID="sdsServices" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT SERVICE_ACCESS_CODE, SERVICE_ACCESS_CODE||' ('|| SERVICE_TITLE|| ') '  TITLE FROM SERVICE_LIST WHERE SERVICE_STATE = 'A' AND SERVICE_ACCESS_CODE IN('CN','FT','FM') ">            
      </asp:SqlDataSource>    
      <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
       <table width="100%">
        <tr>
         <td>
            All Transaction Report
         </td>
         <td></td>
         <td align="right">
          <asp:Label ID="lblMsg" runat="server" ></asp:Label>
         </td>
         <td align="right">
          <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
         </td>
        </tr>
       </table>
      </asp:Panel>
      <asp:Panel ID="pnlView" runat="server" CssClass="View_Panel">
       <fieldset style="border-color: #FFFFFF; width:500px; height:100px">
             <legend><strong style="color: #666666">Service Wise Transaction Report</strong></legend>
       <asp:Label ID="lblServiceCode" runat="server" Text=" Service Code "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="ddlServiceCode" runat="server" AutoPostBack="True" 
              Style="position: relative; top: 0px; left: 0px;" 
              DataSourceID="sdsServices" DataTextField="TITLE" 
              DataValueField="SERVICE_ACCESS_CODE">
       </asp:DropDownList><br />
       <asp:Label ID="lblDateRange" runat="server" Text=" Date Range "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="lblFromDate" runat="server" Text="From Date" Font-Bold="true"></asp:Label>
       <cc1:GMDatePicker ID="dptFrom" runat="server" CalendarTheme="Silver" 
            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
            TextBoxWidth="100">
          <calendartitlestyle backcolor="#FFFFC0" />
       </cc1:GMDatePicker>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="lblTo" runat="server" Text="To Date " Font-Bold="true"></asp:Label>
       <cc1:GMDatePicker ID="dtpTo" runat="server" CalendarTheme="Silver" 
            DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
            TextBoxWidth="100">
         <calendartitlestyle backcolor="#FFFFC0" />
       </cc1:GMDatePicker>       
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; <br />
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnView" runat="server" Text=" View " Width="83px" 
                 onclick="btnView_Click" />
       </fieldset>
      </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
