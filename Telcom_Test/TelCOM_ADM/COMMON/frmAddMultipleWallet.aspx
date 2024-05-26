<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAddMultipleWallet.aspx.cs" Inherits="COMMON_frmAddMultipleWallet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html>
<head runat="server">
    <title>Add Multiple Account</title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />     
    <style type="text/css" >        
        .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	font-size:12px;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	height:20px;
         	 width:100%;  
         	 font-size:12px;
         	 font-weight:bold; 
         	 color:White;    	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         	 width:100%;
         	 font-size:12px;
         	 font-weight:bold; 
         } 
         .Table
         {
         	 font-size:12px;
         	 text-align:right;         	
         	}    
        .style1
        {
            width: 151px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UDPanel" runat="server">
        <contenttemplate>
        
         <asp:SqlDataSource ID="sdsBranch" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT * FROM CM_CMP_BRANCH">
         </asp:SqlDataSource>
        
        <asp:Panel ID="pnlTop" runat="server" >
           <table width="100%" class="Top_Panel">
            <tr>
             <td class="style1">
              Add Multiple Account
             </td>
             <td align="left">&nbsp;&nbsp;
                 <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                 <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch"  AutoPostBack="true"
                     DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" Enabled="false" >
                 </asp:DropDownList>
             </td>
             <td>             
             </td>
             <td align="left">
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
             </td>
             <td align="left">
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
          <table class="Table" >
             <tr>
               <td>
                  Mobile Number(+880...)
               </td>
               <td>
                   <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="14"></asp:TextBox> 
               </td>
             </tr>
             <tr>
                <td> 
                   Bank Code
                </td>
                <td>
                    <asp:TextBox ID="txtBankCode" runat="server" Enabled="false"></asp:TextBox>
                </td>
             </tr>
             <tr>
                 <td>  
                    Enter Digit               
                 </td>                 
                 <td>
                     <asp:TextBox ID="txtDigit" runat="server"  required='true' MaxLength="2"></asp:TextBox>
                     </td>
                      <td>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a value."
                          ValidationGroup="Save"   ControlToValidate="txtDigit" Display="Dynamic"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="regPercent" runat="server" ErrorMessage="Only numeric value are allowed."
                              ControlToValidate="txtDigit"  ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" ValidationGroup="Save" Display="Dynamic" >
                        </asp:RegularExpressionValidator>
                 </td>
             </tr>
             <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server"  Text=" Save " 
                     onclick="btnSave_Click" ValidationGroup="Save" />    
                      <ajaxToolkit:ConfirmButtonExtender ID="cbeAccntOpen" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                             TargetControlID="btnSave">
                     </ajaxToolkit:ConfirmButtonExtender>  
                     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnSaveCancel" 
                             OkControlID="btnSaveOK" TargetControlID="btnSave" 
                             PopupControlID="pnlSave">
                     </ajaxToolkit:ModalPopupExtender>
                </td>
             </tr>
           </table>
          </asp:Panel> 
          <asp:Panel ID="pnlSave" runat="server"  style=" display:none;width:300px; height:165px; 
                 background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
              <div style="height:95px;"><br />&nbsp;<br />&nbsp;
              Are you sure you want to Save?
                 <br />&nbsp;<br />&nbsp;
              </div>
              <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="btnSaveOK" runat="server" Text="  Yes  "  ValidationGroup="Save"/>
                    <asp:Button ID="btnSaveCancel" runat="server" Text=" Cancel " />
              </div>
       </asp:Panel> 
        </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
