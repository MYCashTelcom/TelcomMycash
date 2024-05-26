<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDpdcDescoManualReverse.aspx.cs" Inherits="UBP_frmDpdcDescoManualReverse" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>	UBP Manual Reverse</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    
    <style type="text/css">
       .Font_Color
       {
       	color:White;
       	}
       	.Top_Panel
         {
         	background-color: royalblue;          	
         	height:30px;
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
	        background: url(grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
        .style1
        {
            width: 63px;
        }
        .style3
        {
            width: 48px;
        }
        .style5
        {
            width: 197px;
        }
        .style6
        {
            width: 149px;
        }
        .style7
        {
            width: 113px;
        }
        .style8
        {
            width: 130px;
        }
    </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        
       <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel id="UpdatePanel1" runat="server">
  <contenttemplate>
   <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" >
    <table style="width: 100%; " align="right" he>
     <tr>
      
      <td class="style1">
                    <strong>
                        <asp:Label runat="server" ID="lbl1" ForeColor="White" >From Date</asp:Label>
                        
                    </strong>
                </td>
                <td class="style6">
                    <cc1:GMDatePicker ID="dtpFrDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                </td>
                
                <td class="style3">
                    <strong>
                        <asp:Label runat="server" ID="Label2" ForeColor="White" >To Date</asp:Label>
                    </strong>
                </td>
                <td class="style8">
                    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                </td>
                <td class="style7">
                    <asp:Button runat="server" ID="btnSearch" Text="Search " 
                        onclick="btnSearch_Click"/>
                </td>
                
                
                <td align="left" class="style5">
                  <asp:Label ID="lblMessage" runat="server"  Text=""></asp:Label>
                </td>
      
      
      <td>
        <asp:Label runat="server" ID="lblMsg"><strong></strong></asp:Label>  
      </td> 
      <td align="right" style="width: 50px;">
       <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
         <ProgressTemplate>
           <%--<img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;--%>
           <img alt="Loading" src="../Icons/029.gif" />
         </ProgressTemplate>
        </asp:UpdateProgress> 
      </td>  
     </tr>
    </table> 
   </asp:Panel>
   
   
   <asp:GridView ID="grvRequestList" runat="server" AllowSorting="True" 
          AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
          BorderStyle="None" CssClass="GridViewClass" DataKeyNames="UTILITY_TRAN_ID" 
          HeaderStyle-ForeColor="White" PagerStyle-CssClass="pgr" Width="100%" 
              onpageindexchanging="grvRequestList_PageIndexChanging">
          <Columns>
              
              <asp:TemplateField HeaderText="Utl Trx Id">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Request Id">
                  <EditItemTemplate>
                      <asp:TextBox ID="txtRequestId" runat="server" Text='<%# Bind("REQUEST_ID") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("REQUEST_ID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <%--<asp:BoundField DataField="REQUEST_ID" HeaderText="Request Id" />--%>
              <asp:BoundField DataField="TRANSA_DATE" HeaderText="Transaction Date" />
              <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Account No" />
              <asp:BoundField DataField="TOTAL_BILL_AMOUNT" HeaderText="Bill Amount" />
              <asp:BoundField DataField="BILL_NUMBER" HeaderText="Bill No" />
              <asp:BoundField DataField="OWNER_CODE" HeaderText="Biller Code" />
              <asp:BoundField DataField="SOURCE_ACC_NO" HeaderText="Source Account" />
              <asp:BoundField DataField="REVERSE_STATUS" HeaderText="Reverse Status" />
              <asp:BoundField DataField="RESPONSE_MSG_BP" HeaderText="Resp Msg" />
              <asp:BoundField DataField="RESPONSE_STATUS_BP" HeaderText="Resp Status" />
              <asp:BoundField DataField="PAYER_MOBILE_NO" HeaderText="Payer No" />
              <asp:BoundField DataField="CHECK_STATUS" HeaderText="Check Status" />
              <asp:BoundField DataField="CANCLE_RESPONSE" HeaderText="Cancel Response" />
              
              <asp:TemplateField HeaderText="Actions">
                 <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkButtonDetails" Text="Details" OnClick="lnkButtonDetails_Click" ></asp:LinkButton> ||
                    <asp:LinkButton runat="server" ID="lnkButtonReverse" Text="Reverse" OnClick="lnkButtonReverse_Click" 
                        OnClientClick = " return confirm('Do you want to Reverse  ?'); this.disabled = true; this.value = 'Submit in progress...';" UseSubmitBehavior="false"></asp:LinkButton> 
                    
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
      
      <br/>
      
      <asp:DetailsView ID="dtvDetails" runat="server" AutoGenerateRows="False" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="80%"
                        Visible="False">
                        <PagerStyle CssClass="pgr" />
                        <Fields>
                            
                            <asp:BoundField DataField="UTILITY_TRAN_ID" HeaderText="Utility Transaction Id" SortExpression="UTILITY_TRAN_ID" />
                            <asp:BoundField DataField="LOCATION_ID" HeaderText="Location " SortExpression="LOCATION_ID" />
                            <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Bill Account" SortExpression="ACCOUNT_NUMBER" />
                            <asp:BoundField DataField="BILL_NUMBER" HeaderText="Bill Number" SortExpression="BILL_NUMBER" />
                            <asp:BoundField DataField="TOTAL_DPDC_AMOUNT" HeaderText="DPDC/DESCO Amount" SortExpression="TOTAL_DPDC_AMOUNT" />
                            <asp:BoundField DataField="VAT_AMOUNT" HeaderText="VAT" SortExpression="VAT_AMOUNT" />
                            <asp:BoundField DataField="BILL_MONTH" HeaderText="Bill Month" SortExpression="BILL_MONTH" />
                            <asp:BoundField DataField="BILL_YEAR" HeaderText="Bill Year" SortExpression="BILL_YEAR" />
                            <asp:BoundField DataField="CHEQUE_REMARKS" HeaderText="Check Remarks" SortExpression="CHEQUE_REMARKS" />
                            <asp:BoundField DataField="SERVICE" HeaderText="Service" SortExpression="SERVICE" />
                            <asp:BoundField DataField="REQUEST_ID" HeaderText="UBP Request Id" SortExpression="REQUEST_ID" />
                            <asp:BoundField DataField="OWNER_CODE" HeaderText="Owner Code" SortExpression="OWNER_CODE" />
                            <asp:BoundField DataField="SOURCE_ACC_NO" HeaderText="Source Account" SortExpression="SOURCE_ACC_NO" />
                            <asp:BoundField DataField="TRANSA_DATE" HeaderText="UBP Transaction Date" SortExpression="TRANSA_DATE" />
                            <asp:BoundField DataField="BILL_DUE_DATE" HeaderText="Bill Due Date" SortExpression="BILL_DUE_DATE" />
                            <asp:BoundField DataField="TOTAL_BILL_AMOUNT" HeaderText="Introducer Name" SortExpression="TOTAL_BILL_AMOUNT" />
                            <asp:BoundField DataField="NET_DPDC_AMOUNT" HeaderText="DPDC/DS Amount" SortExpression="NET_DPDC_AMOUNT" />
                            <asp:BoundField DataField="RESPONSE_STATUS" HeaderText="Response Status" SortExpression="RESPONSE_STATUS" />
                            <asp:BoundField DataField="RESPONSE_MSG" HeaderText="Response Message" SortExpression="RESPONSE_MSG" />
                            <asp:BoundField DataField="REVERSE_STATUS" HeaderText="Reverse Status" SortExpression="REVERSE_STATUS" />
                            <asp:BoundField DataField="REQUEST_LOG" HeaderText="Requset Log" SortExpression="REQUEST_LOG" />
                            <asp:BoundField DataField="RESPONSE_LOG" HeaderText="Response Log" SortExpression="RESPONSE_LOG" />
                            <asp:BoundField DataField="ST_CHARGE" HeaderText="ST Charge" SortExpression="ST_CHARGE" />
                            <asp:BoundField DataField="REQUEST_LOG_BP" HeaderText="BP Request Log" SortExpression="REQUEST_LOG_BP" />
                            <asp:BoundField DataField="RESPONSE_LOG_BP" HeaderText="BP Response Log" SortExpression="RESPONSE_LOG_BP" />
                            <asp:BoundField DataField="RESPONSE_MSG_BP" HeaderText="BP Response Message" SortExpression="RESPONSE_MSG_BP" />
                            <asp:BoundField DataField="RESPONSE_STATUS_BP" HeaderText="Response Status BP" SortExpression="RESPONSE_STATUS_BP" />
                            <asp:BoundField DataField="REQUEST_PARTY_TYPE" HeaderText="Request Channel" SortExpression="REQUEST_PARTY_TYPE" />
                            <asp:BoundField DataField="PAYER_MOBILE_NO" HeaderText="Payer Mobile Number" SortExpression="PAYER_MOBILE_NO" />
                            <asp:BoundField DataField="CHECK_STATUS_RESPONSE" HeaderText="Check Status Response" SortExpression="CHECK_STATUS_RESPONSE" />
                            <asp:BoundField DataField="CHECK_STATUS_REQ_LOG" HeaderText="Check Status Request Log" SortExpression="CHECK_STATUS_REQ_LOG" />
                            <asp:BoundField DataField="CHECH_STATUS_RES_LOG" HeaderText="Check Status Response Log" SortExpression="CHECH_STATUS_RES_LOG" />
                            <asp:BoundField DataField="CHECK_STATUS" HeaderText="Check Status" SortExpression="CHECK_STATUS" />
                            <asp:BoundField DataField="CANCLE_RESPONSE" HeaderText="Cancel Response " SortExpression="CANCLE_RESPONSE" />
                            <asp:BoundField DataField="CANCLE_REQ_LOG" HeaderText="Cancel Request Log" SortExpression="CANCLE_REQ_LOG" />
                            <asp:BoundField DataField="CANCLE_RES_LOG" HeaderText="Cancel Response Log" SortExpression="CANCLE_RES_LOG" />
                            
                            
                            
                            
                        </Fields>
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView>
   
   </contenttemplate>
   </asp:UpdatePanel>  
         
         
         
         
    
    
    </form>
</body>
</html>
