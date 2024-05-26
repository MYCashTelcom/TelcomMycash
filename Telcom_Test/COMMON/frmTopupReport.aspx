<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTopupReport.aspx.cs" Inherits="COMMON_frmTopupReport" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Topup Report</title>
      <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            width: 422px;
        }
        .style2
        {
            height: 30px;
            width: 525px;
        }
        .style3
        {
            width: 525px;
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
         	background-color:powderblue;
         	width:817px;         	
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
     <asp:UpdatePanel id="UpdatePanel1" runat="server">
     <Triggers>
       <asp:PostBackTrigger ControlID="btnViewReport" /> 
     </Triggers>
     <ContentTemplate>
         <asp:SqlDataSource ID="sdsOwnerCode" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT DISTINCT &quot;OWNER_CODE&quot; FROM &quot;TOPUP_TRANSACTION&quot;">
         </asp:SqlDataSource>
     <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
     <table width="100%">
      <tr>
        <td>
         Manage Topup Report
        </td>
        <td></td>
        <td>
         <asp:Label ID="lblMsg" runat="server" ></asp:Label>   
        </td>
        <td>
          <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
        </td>
      </tr>
     </table>
     </asp:Panel>
     <asp:Panel ID="Panel1" runat="server">
      <fieldset style="border-color: #FFFFFF;width:500px; ">
      <legend><strong>Top Up Report</strong></legend>
       <table>        
        <tr>
         <td class="style2">
          <table>
           <tr>
              <td >
               &nbsp; Select Owner Code
              </td>
              <td>
                  <asp:DropDownList ID="ddlOwnerCode" runat="server" DataSourceID="sdsOwnerCode" 
                      DataTextField="OWNER_CODE" DataValueField="OWNER_CODE" AppendDataBoundItems="true">
                      <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                  </asp:DropDownList>
              </td>
           </tr>
           <tr>
            <td>
             <asp:RadioButtonList ID="rbtnAllDateRange" runat="server" 
                RepeatDirection="Horizontal" Width="120px">
                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                <asp:ListItem Value="1">Date Range</asp:ListItem>
             </asp:RadioButtonList>
            </td>
            <td style="font-size:10px;" class="style1">
             From Date
             <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
               DateFormat="dd/MMM/yyyy " MinDate="1900-01-01" Style="position: relative" 
               TextBoxWidth="90">
            <calendartitlestyle  />
           </cc1:GMDatePicker>
           To Date
            <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="90" >
                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
            </cc1:GMDatePicker>
           </td>
          </tr>
          </table>               
            </td>
            </tr>
            <tr>
              <td class="style3" >
                <asp:RadioButtonList ID="rbtnSelectState" RepeatDirection="Horizontal" AutoPostBack="true"
                    runat="server" onselectedindexchanged="rbtnSelectState_SelectedIndexChanged">
                    <asp:ListItem Value="S" Selected="True">Succeccfull</asp:ListItem>
                    <asp:ListItem Value="U">Unsuccessfull</asp:ListItem>
                    <asp:ListItem Value="R">Reverse</asp:ListItem>
                    <asp:ListItem Value="RS">Resubmit</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Button ID="btnViewReport" runat="server" Text="Show Report" OnClick="btnViewReport_Click" />
              </td>
            </tr>
        </table>
        </fieldset>
        <fieldset style="width: 500px">
         <legend><strong>SuccessfulTop Up Account Statement Report</strong></legend>   
         <table style="width: 500px">
          <tr>
           <td>
            <asp:Label runat="server" ID="lblFDate"><strong>From Date</strong></asp:Label>   
           </td>
           <td>
            <cc1:GMDatePicker ID="dtpAccFDate" runat="server" CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="90" >
                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
            </cc1:GMDatePicker>   
           </td>
           <td>
            <asp:Label runat="server" ID="lblToDate"><strong>To Date</strong></asp:Label>   
           </td>
           <td>
            <cc1:GMDatePicker ID="dtpAccToDate" runat="server" CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="90" >
                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
            </cc1:GMDatePicker>   
           </td>   
          </tr>
          <tr>
           <td></td>
           <td>
            <asp:Button runat="server" ID="btnTopUpAccStm" Text="Print Report" onclick="btnTopUpAccStm_Click"/>   
           </td>
           <td></td>
           <td></td>   
          </tr>   
         </table>
        </fieldset></asp:Panel>
    </ContentTemplate>
    <Triggers>
       <asp:PostBackTrigger ControlID="btnTopUpAccStm" />
      </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
