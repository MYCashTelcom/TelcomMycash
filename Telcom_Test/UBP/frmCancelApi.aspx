<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCancelApi.aspx.cs" Inherits="UBP_frmCancelApi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>	UBP Cancel Api List Reverse</title>
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
    
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <asp:Panel ID="Panel1" runat="server" >
           <table width="100%" class="Top_Panel">
              <tr>
                <td>
                     <strong>UBP Cancel Api List Reverse </strong> 
                </td>
                <td>
                    <strong>From Date</strong>
                </td>
                <td>
                    <cc1:GMDatePicker ID="dtpFrDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                </td>
                
                <td>
                    <strong>To Date</strong>
                </td>
                <td>
                    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="100">
                                <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSearch" Text="Search List" 
                        onclick="btnSearch_Click"/>
                </td>
                
                
                <td align="left">
                  <asp:Label ID="lblMessage" runat="server"  Text=""></asp:Label>
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
         
         
         <asp:GridView ID="grvCancelApiList" runat="server" AllowSorting="True" 
          AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Data Found"
            AllowPaging="True" PageSize="15"
          BorderStyle="None" CssClass="GridViewClass" DataKeyNames="UTILITY_TRAN_ID"
          HeaderStyle-ForeColor="White" PagerStyle-CssClass="pgr" Width="100%" 
              onpageindexchanging="grvCancelApiList_PageIndexChanging">
          <Columns>
              
              
              <asp:TemplateField HeaderText="Trx Id" >
                  <%--<EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:TextBox>
                  </EditItemTemplate>--%>
                  <ItemTemplate>
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("UTILITY_TRAN_ID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Account No" />
              <asp:BoundField DataField="BILL_NUMBER" HeaderText="Bill No" />
              <asp:BoundField DataField="BILL_MONTH" HeaderText="Month" />
              <asp:BoundField DataField="BILL_YEAR" HeaderText="Year" />
              <asp:BoundField DataField="TOTAL_BILL_AMOUNT" HeaderText="Total Amount" />
              <asp:BoundField DataField="SERVICE" HeaderText="Service" />
              <asp:BoundField DataField="REQUEST_ID" HeaderText="Request Id" />
              <asp:BoundField DataField="OWNER_CODE" HeaderText="Owner Code" />
              <asp:BoundField DataField="SOURCE_ACC_NO" HeaderText="Source No" />
              <asp:BoundField DataField="TRANSA_DATE" HeaderText="Trx Date" />
              <asp:BoundField DataField="REVERSE_STATUS" HeaderText="Reverse Status" />
              <asp:BoundField DataField="RESPONSE_MSG_BP" HeaderText="Resp Msg" />
              <asp:BoundField DataField="RESPONSE_STATUS_BP" HeaderText="Resp Status" />
              <asp:BoundField DataField="PAYER_MOBILE_NO" HeaderText="Payer No" />
              <asp:BoundField DataField="CHECK_STATUS" HeaderText="Check Status" />
              <asp:BoundField DataField="CANCLE_RESPONSE" HeaderText="Cancel Response" />
              
              
              <asp:TemplateField HeaderText="Actions">
                 <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkButtonReverse" Text="Reverse" OnClick="lnkButtonReverse_Click" ></asp:LinkButton> 
                    
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
         
         
         
         
         
         </ContentTemplate>
         </asp:UpdatePanel>
    
    
    
    </form>
</body>
</html>
