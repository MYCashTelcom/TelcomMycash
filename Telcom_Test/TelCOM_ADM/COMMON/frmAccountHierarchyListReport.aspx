<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountHierarchyListReport.aspx.cs"
    Inherits="COMMON_frmReplaceMobileNo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Hierarchy List Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
     
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
    <asp:UpdatePanel ID="UDPanel" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
              <table class="Top_Panel" width="100%">
               <tr>
                 <td>
                     <asp:Label ID="Label1" runat="server" Text="Manage Hierarchy Report"></asp:Label>
                 </td>
                 <td></td>
                 <td></td>
                 <td>
                  <asp:Label ID="lblMsg" runat="server" Text="" ></asp:Label>                
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
            <div>  
            <fieldset style="border-color: #FFFFFF; width:310px; height:180px">
            <legend><strong style="color: #666666">Hierarchy List Report</strong></legend>
                <asp:RadioButtonList ID="rblRank" runat="server">                
                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                    <asp:ListItem Value="1">MBL Branch</asp:ListItem>
                    <asp:ListItem Value="2">MBL Distributor</asp:ListItem>
                    <asp:ListItem Value="3">MBL DSE</asp:ListItem>
                    <asp:ListItem Value="4">MBL Agent</asp:ListItem>                
                </asp:RadioButtonList>            
                        &nbsp;&nbsp;  
                
                <asp:TextBox ID="txtWalletID" runat="server"></asp:TextBox>
               <span style="font-size: 12px; font-weight: normal;">(Search by Wallet ID)</span>
               <br />
             &nbsp;&nbsp;
               <asp:Button ID="btnDelete" runat="server" Text="View" Width="60px" 
                    onclick="btnView_Click"  />
           </fieldset>
          </div>           
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
