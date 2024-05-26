<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFailedUbpTrx.aspx.cs" Inherits="UBP_frmFailedUbpTrx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>	Bill Pay Transaction Reverse</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       .Font_Color
       {
       	color:White;
       	}
       	.Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	<%--color:White;--%>
         }
         .View_Panel
         {         	
         	 width:100%;
         	 background-color:powderblue;       	
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
	        background: url(../COMMON/grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True" AsyncPostBackTimeout="36000"></ajaxToolkit:ToolkitScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Height="35px">
             <table style="width: 100%; " align="right" >
               <tr>
                 <td align="left">
                   <asp:Label runat="server" ID="panelQ" Text="Bill Pay Transaction Reverse" 
                         ForeColor="White"></asp:Label> 
                 </td>
                 <td>
                   <strong>
                       <asp:Label runat="server" ID="lbl1" ForeColor="White">Select Biller Type</asp:Label>
                   </strong>
               </td>  
               <td>
                 <asp:DropDownList runat="server" ID="drpBillerType" DataTextField="BILLPAY_TYPE_NAME" DataValueField="BILLPAY_TYPE_SHORT_CODE"/>  
               </td>
               <td>
                   <strong>
                       <asp:Label runat="server" ID="lbl2" ForeColor="White">From Date</asp:Label>
                   </strong>
               </td>  
               <td>
                   
                      
                      <cc1:GMDatePicker ID="dtpFrDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
               </td>  
               <td>
                   <strong>
                       <asp:Label runat="server" ID="lbl3" ForeColor="white">To Date</asp:Label>
                   </strong>
               </td>  
               <td>
                   
                      
                      <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
               </td> 
               
               <td>
                  <asp:Button runat="server" ID="btnShow" Text="Search" onclick="btnShow_Click"/>   
                 </td>
                 <td>
                   <asp:Button runat="server" ID="btnExport" Text="Export" 
                         onclick="btnExport_Click" />  
                 </td>
               
                 <td align="right">
                   <asp:Label ID="lblMsg" runat="server" ></asp:Label>  
                 </td>  
                 <td align="right" style="width: 50px;">
                   <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
                     <ProgressTemplate>
                       <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;
                     </ProgressTemplate>
                    </asp:UpdateProgress> 
                 </td>  
                </tr>
             </table> 
           </asp:Panel>
           
           
           <asp:GridView ID="grvNoResponseList" runat="server" AllowSorting="True" 
          AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
          BorderStyle="None" CssClass="GridViewClass" DataKeyNames="UTILITY_TRAN_ID"
          HeaderStyle-ForeColor="White" PagerStyle-CssClass="pgr" Width="100%" 
              onpageindexchanging="grvNoResponseList_PageIndexChanging" PageSize="15">
          <Columns>
              <asp:TemplateField HeaderText="Id" Visible="False">
                  <%--<EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:TextBox>
                  </EditItemTemplate>--%>
                  <ItemTemplate>
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="OWNER_CODE" HeaderText="Biller Type" />
              <asp:BoundField DataField="TRANSA_DATE" HeaderText="Transaction Date" />
              
              <asp:BoundField DataField="BILL_NUMBER" HeaderText="Bill No" />
              <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Bill Account No" />
              <asp:BoundField DataField="REQUEST_ID" HeaderText="Transaction Id" />
              <asp:BoundField DataField="SOURCE_ACC_NO" HeaderText="Source Account No" />
              <asp:BoundField DataField="TOTAL_DPDC_AMOUNT" HeaderText="Bill Amount" />              
              <asp:BoundField DataField="RESPONSE_STATUS_BP" HeaderText="Response Status" />
              
              <asp:TemplateField HeaderText="Actions">
                 <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkButtonDetails" Text="Details" OnClick="lnkButtonDetails_Click" ></asp:LinkButton> 
                    ||
                    <%--<asp:LinkButton runat="server" ID="lnkButtonResubmit" Text="Resubmit" OnClick="lnkButtonResubmit_Click" OnClientClick="javascript: return confirm('Are you sure, you want to Resubmit the transaction?');"></asp:LinkButton>
                    ||
                    <asp:LinkButton runat="server" ID="lnkButtonReverse" Text="Reverse" OnClick="lnkButtonReverse_Click" OnClientClick="javascript: return confirm('Are you sure, you want to Reverse the transaction?');"></asp:LinkButton>--%>
                 </ItemTemplate>
              </asp:TemplateField>
          </Columns>
          <PagerStyle CssClass="pgr" />
          <HeaderStyle ForeColor="White" />
          <AlternatingRowStyle CssClass="alt" />
      </asp:GridView>
      <asp:DetailsView ID="dtvBillDtls" runat="server" AutoGenerateRows="False" DataKeyNames="UTILITY_TRAN_ID"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                               BorderStyle="None" Visible="False" Width="100%">   
                           <PagerStyle CssClass="pgr" /> 
                           
                           <Fields>
                               <asp:BoundField DataField="UTILITY_TRAN_ID" HeaderText="Id" Visible="False" />
                               <asp:BoundField DataField="STAKEHOLDER_ID" HeaderText="Stake Holder" />
                               <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Bill Account No" />
                               <asp:BoundField DataField="BILL_NUMBER" HeaderText="Bill No" />
                               <asp:BoundField DataField="LOCATION_ID" HeaderText="Location" />
                               <asp:BoundField DataField="TOTAL_DPDC_AMOUNT" HeaderText="Total Amount" />
                               <asp:BoundField DataField="VAT_AMOUNT" HeaderText="VAT" />
                               <asp:BoundField DataField="TRANSA_DATE" HeaderText="Trx Date" />
                               <asp:BoundField DataField="BILL_MONTH" HeaderText="Bill Month" />
                               <asp:BoundField DataField="BILL_YEAR" HeaderText="Bill Year" />
                               <asp:BoundField DataField="REQUEST_ID" HeaderText="Trx Id" />
                               <asp:BoundField DataField="OWNER_CODE" HeaderText="Biller Code" />
                               <asp:BoundField DataField="BILL_STATUS" HeaderText="Bill Status" />
                               <asp:BoundField DataField="TRANSACTION_STATUS" HeaderText="Trx Status" />
                               <asp:BoundField DataField="FINAL_STATUS" HeaderText="Final Status" />
                               <asp:BoundField DataField="SOURCE_ACC_NO" HeaderText="Source Account" />
                               <asp:BoundField DataField="REQUEST_LOG" HeaderText="Request Log" />
                               <asp:BoundField DataField="RESPONSE_LOG" HeaderText="Response Log" />
                               <asp:BoundField DataField="REQUEST_LOG_BP" HeaderText="SSL Request Log" />
                               <asp:BoundField DataField="RESPONSE_LOG_BP" HeaderText="SSL Response Log" />
                               <asp:BoundField DataField="RESPONSE_MSG_BP" HeaderText="SSL Response Message" />
                               <asp:BoundField DataField="RESPONSE_STATUS_BP" HeaderText="SSL Response Status" />
                               <asp:BoundField DataField="REVERSE_STATUS" HeaderText="Reverse Status" />
                               <asp:BoundField DataField="REQUEST_PARTY_TYPE" HeaderText="Requested Channel" />
                               <asp:BoundField DataField="PAYER_MOBILE_NO" HeaderText="Payer Mobile No" />
                           </Fields>
                           
                           <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>
           
            
           
           
           
           </ContentTemplate>
           
           <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
           </Triggers>
           
           </asp:UpdatePanel>
    
    
    </form>
</body>
</html>
