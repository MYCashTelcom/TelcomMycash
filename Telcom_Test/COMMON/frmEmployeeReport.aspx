<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEmployeeReport.aspx.cs" Inherits="COMMON_frmEmployeeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>    
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
         .table
         {
         	background-color:#fcfcfc ;
         	margin: 5px 0 10px 0;
         	border: solid 1px #525252;
            text-align: left;
            border-collapse:collapse;
            border-color:White;
         	}
        .table td{ padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
         .div
         {
         	margin:5px 0 0 0;
         	}	
         .td
         {
         	text-align:right;
         	width:125px;
         	}	
         .style1
         {
             width: 672px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        
         <asp:SqlDataSource ID="sdsEmployee" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT SERVICE_PKG_ID,SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_ID IN('1209270001','1211220002')">
         </asp:SqlDataSource>
     
         <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
          <table width="100%">
          <tr>
            <td>         
              Manage Employee Report 
            </td>
            <td>
            </td>
            <td>
            </td>
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
         <asp:Panel ID="pnlView" runat="server">
          <table>
           <tr>
             <td>
                Select Type
             </td>
             <td>
               
             </td>
             <td></td>
           </tr>
           <tr>
             <td>
                
             </td>
             <td>
                 <asp:DropDownList ID="ddlEmployee" runat="server" DataSourceID="sdsEmployee"  AutoPostBack="true"
                     DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID">
                 </asp:DropDownList>
             </td>
             <td></td>
           </tr>
           <tr>
            <td></td>
            <td>
                <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" 
                    Width="79px" />
            </td>
           </tr>
          </table>
         </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
