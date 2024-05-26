<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServiceTesting.aspx.cs" Inherits="COMMON_frmServiceTesting"
 Title="Service Testing" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"--%>
<!DOCTYPE html>
<html ><%--xmlns="http://www.w3.org/1999/xhtml"--%>
<head runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <title>Service Testing</title>
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
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
     </style>
</head>
<body style="background-color: lightgrey;font-family:Times New Roman;font-size:12px;">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True" ></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
         <asp:SqlDataSource ID="sdsServiceList" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT SERVICE_ID, SERVICE_ACCESS_CODE, SERVICE_TITLE FROM SERVICE_LIST WHERE SERVICE_STATE='A' ORDER BY SERVICE_TITLE">
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="sdsChannelType" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE">
        </asp:SqlDataSource>
        
        <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel" 
             meta:resourcekey="pnlTopResource1">
            <table width="100%" >
             <tr>
              <td>
                Manage Service Testing &nbsp;&nbsp;&nbsp;&nbsp;
              </td>
              <td></td>
              <td></td>
              <td align="right">
               <asp:Label ID="lblMessage" runat="server" ></asp:Label>
              </td>
              <td>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                   <ProgressTemplate>
                     <img alt="Loading" src="../resources/images/loading.gif" />                    
                   </ProgressTemplate>
                 </asp:UpdateProgress>
              </td>
             </tr>
            </table>  
         </asp:Panel>
        
        
         <asp:Panel runat="server" ID="panelBody" meta:resourcekey="panelBodyResource1">
         <div id="fildsetcontainer" style="margin-left: 20px;">
             <fieldset id="fieldset1" runat="server" style="width: 400px;">
             <legend><strong>USSD</strong></legend>
             <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
           <table>
               
            <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" ><strong>Service List</strong></asp:Label>
             </td>
             <td>
                 <asp:DropDownList ID="ddlServiceList" runat="server"  AutoPostBack="True"
                     DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                     DataValueField="SERVICE_ACCESS_CODE" Width="220px" 
                     meta:resourcekey="ddlServiceListResource1">
                 </asp:DropDownList>
             </td>
             
            </tr>
            
            <tr>
             <td>
                 <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" ><strong>Channel Type List</strong></asp:Label>
             </td>
             <td>
                 <asp:DropDownList ID="ddlChannelTypeList" runat="server"
                     DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                     DataValueField="CHANNEL_TYPE" Width="220px" 
                     meta:resourcekey="ddlChannelTypeListResource1">
                 </asp:DropDownList>
             </td>
             
            </tr>
            <tr>
               <td>
                   <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" ><strong>Source Wallet ID</strong></asp:Label>
               </td>
               <td>
                   <asp:TextBox ID="txtSourceWallet" runat="server" 
                       placeholder="Please Insert Source Wallet ." Width="220px" 
                       meta:resourcekey="txtSourceWalletResource1"></asp:TextBox>
               </td>
               
            </tr>
            <tr>
              <td>
                  <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" ><strong>Destination Wallet ID</strong></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDestinationWallet" runat="server" 
                      placeholder="Please Insert Destination Wallet ." Width="220px" 
                      meta:resourcekey="txtDestinationWalletResource1"></asp:TextBox>                
              </td>
              
            </tr>
            <tr>
              <td>
                  <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" ><strong>Amount</strong></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAmount" runat="server" placeholder="Please Insert Amount ." 
                      Width="220px" meta:resourcekey="txtAmountResource1"></asp:TextBox>
              </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDPSAcc" Visible="False" 
                        meta:resourcekey="lblDPSAccResource1"><strong>DPS Account no</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDPSAccNo" Visible="False" Width="220px" 
                        meta:resourcekey="txtDPSAccNoResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblOTPPin" Visible="False" 
                        meta:resourcekey="lblOTPPinResource1"><strong>PIN No</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPinForOTP" Width="220px" Visible="False" 
                        meta:resourcekey="txtPinForOTPResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblOldPIn" Visible="False" 
                        meta:resourcekey="lblOldPInResource1"><strong><strong>Old PIN No</strong></strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOldPIN" Width="220px" Visible="False" 
                        meta:resourcekey="txtOldPINResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblNewPIN" Visible="False" 
                        meta:resourcekey="lblNewPINResource1"><strong>New PIN No</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNewPIN" Width="220px" Visible="False" 
                        meta:resourcekey="txtNewPINResource1"></asp:TextBox>
                </td>
                <tr>
                <td>
                    <asp:Label runat="server" ID="lblMP" Visible="False"><strong>MP Ref No./Code</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMPRef" Width="220px" Visible="False"></asp:TextBox>
                </td>
                </tr>
            </tr>
            <tr>
             <td>              
             </td>
             <td>
                 <asp:Button ID="btnSave" runat="server" Text="Submit" onclick="btnSave_Click" />
             </td>
            </tr>
           </table>
         </asp:Panel>
         </fieldset>
         
         
         &nbsp;
         
         
         
         
         
         <fieldset id="fieldset2" style="width: 400px;" runat="server">
          <legend><strong>Using OTP</strong></legend>
           <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">
           <table>
            <tr>
             <td>
                 <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" ><strong>Service List</strong></asp:Label>
             </td>
             <td>
                 <asp:DropDownList ID="ddlServiceListOTP" runat="server"  AutoPostBack="True"
                     DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                     DataValueField="SERVICE_ACCESS_CODE" Width="220px" 
                     meta:resourcekey="ddlServiceListOTPResource1">
                 </asp:DropDownList>
             </td>
            </tr>
            <tr>
             <td>
                 <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" ><strong>Channel Type List</strong></asp:Label>
             </td>
             <td>
                 <asp:DropDownList ID="ddlChannelTypeListOTP" runat="server"  AutoPostBack="True"
                     DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                     DataValueField="CHANNEL_TYPE" Width="220px" 
                     meta:resourcekey="ddlChannelTypeListOTPResource1">
                 </asp:DropDownList>
             </td>
            </tr>
            <tr>
               <td>
                   <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" ><strong>Source Wallet</strong></asp:Label>
               </td>
               <td>
                   <asp:TextBox ID="txtSourceWalletOTP" runat="server" 
                       placeholder="Please Insert Source Wallet ." Width="220px" 
                       meta:resourcekey="txtSourceWalletOTPResource1"></asp:TextBox>
               </td>
            </tr>
            <tr>
              <td>
                  <asp:Label ID="Label9" runat="server" meta:resourcekey="Label9Resource1" ><strong>Destination Wallet</strong></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDestinationWalletOTP" runat="server" 
                      placeholder="Please Insert Destination Wallet ." Width="220px" 
                      meta:resourcekey="txtDestinationWalletOTPResource1"></asp:TextBox>                
              </td>
            </tr>
            <tr>
              <td>
                  <asp:Label ID="Label10" runat="server" meta:resourcekey="Label10Resource1" ><strong>Amount</strong></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAmountOTP" runat="server" 
                      placeholder="Please Insert Amount ." Width="220px" 
                      meta:resourcekey="txtAmountOTPResource1"></asp:TextBox>
              </td>
            </tr>
            <tr>
             <td>
                 <asp:Label ID="Label11" runat="server" meta:resourcekey="Label11Resource1" ><strong>Initiator's OTP</strong></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtOTP" runat="server" placeholder="Please Insert OTP ." 
                     Width="220px" meta:resourcekey="txtOTPResource1"></asp:TextBox>
             </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDpsAccNoOTP" Visible="False"> <strong>DPS Account No.</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDpsAccOtp" Width="220px" Visible="False" placeholder="Please Insert DPS Account No."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblPINforOTP" Visible="False"><strong>PIN No</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPINOTP" Width="220px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblMpRef" Visible="False"><strong>MP Ref No/Code</strong></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMpRefOtp" Visible="False" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td></td>
             <td>
                 <asp:Button ID="btnSaveOTP" runat="server" Text="Submit" 
                     onclick="btnSaveOTP_Click"   />
             </td>
            </tr>
           </table>
         </asp:Panel>
             
         </fieldset>
             
         </div>
         
         </asp:Panel>  
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
