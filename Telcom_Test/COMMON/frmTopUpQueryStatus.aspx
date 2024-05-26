<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTopUpQueryStatus.aspx.cs" Inherits="COMMON_frmTopUpQueryStatus" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Topup Status</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:SqlDataSource ID="sdsRequest" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         
         
         SelectCommand="
SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,
SSL_CREATE_MESSAGE,SSL_INT_MESSAGE,SSL_FINAL_MESSAGE,REVERSE_STATUS,SSL_VRG_UNIQUE_ID,REVERSE_NOTE FROM TOPUP_TRANSACTION"></asp:SqlDataSource>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
      <Triggers>
     <asp:PostBackTrigger ControlID="btnExport" /> 
    </Triggers>
    <ContentTemplate> 
    
      <div style="BACKGROUND-COLOR: royalblue"><strong>
   
    <span class="Font_Color" style="COLOR: white">From Date</span>
               <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="130">
                                <calendartitlestyle  />
                            </cc1:GMDatePicker>
   <span class="Font_Color" style="COLOR: white"> To Date</span>
    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
    <span class="Font_Color" style="COLOR: white">Source Wallet</span>
    <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
     <span class="Font_Color" style="COLOR: white">Subscriber Mobile No</span>
    <asp:TextBox ID="txtSubscriberNo" runat="server" Width="112px"></asp:TextBox>
     <span class="Font_Color" style="COLOR: white"> Owner Code</span>
    <asp:TextBox ID="txtOwnerCode" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />
        </strong>
         <td colspan="3" align="center" style="font-size: 11px; font-weight: bold; color: White;">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>
             </td>
        </div>
      
     <div>
    
         <asp:GridView ID="gdvStatus" runat="server" AutoGenerateColumns="False" 
             DataKeyNames="TOPUP_TRAN_ID" DataSourceID="sdsRequest"  CssClass="mGrid" 
             Width="100%" PagerStyle-CssClass="pgr" 
             AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
             AllowPaging="True"  pagesize="13" 
             onpageindexchanging="gdvStatus_PageIndexChanging">
             <Columns>
                 <asp:BoundField DataField="TOPUP_TRAN_ID" HeaderText="Topup Transaction ID" 
                     ReadOnly="True" SortExpression="TOPUP_TRAN_ID" />
                 <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" 
                     SortExpression="REQUEST_ID" />
                 <asp:BoundField DataField="TRAN_DATE" HeaderText="Transaction Date" 
                     SortExpression="TRAN_DATE" />
                 <asp:BoundField DataField="SOURCE_ACCNT_NO" HeaderText="Source Wallet" 
                     SortExpression="SOURCE_ACCNT_NO" />
                 <asp:BoundField DataField="SUBSCRIBER_MOBILE_NO" HeaderText="Subscriber Mobile No" 
                     SortExpression="SUBSCRIBER_MOBILE_NO" />
                     
                      <asp:TemplateField HeaderText="Subscriber Type" SortExpression="SUBSCRIBER_TYPE" >
                 <%--  <EditItemTemplate>
                       <asp:DropDownList ID="DropDownList1" runat="server"  AppendDataBoundItems="true"  DataField="SUBSCRIBER_TYPE"  SelectedValue='<%# Bind("SUBSCRIBER_TYPE") %>'>
                         <asp:ListItem Value="0" >Prepaid</asp:ListItem>
                         <asp:ListItem Value="1">Postpaid</asp:ListItem>
                       </asp:DropDownList>
                  </EditItemTemplate>--%>
                  <ItemTemplate>
                      <asp:DropDownList ID="DropDownList2" runat="server"  Enabled="false" AppendDataBoundItems="true"  DataField="SUBSCRIBER_TYPE"  SelectedValue='<%# Bind("SUBSCRIBER_TYPE") %>'>
                         <asp:ListItem Value="0" >Prepaid</asp:ListItem>
                         <asp:ListItem Value="1">Postpaid</asp:ListItem>
                       </asp:DropDownList>
                  </ItemTemplate>
                 </asp:TemplateField>
                 <%--<asp:BoundField DataField="SUBSCRIBER_TYPE" 
                     HeaderText="Subscriber Type" SortExpression="SUBSCRIBER_TYPE" />--%>
                 <asp:BoundField DataField="TRAN_AMOUNT" HeaderText="Transaction Amount" 
                     SortExpression="TRAN_AMOUNT" />
                 <asp:BoundField DataField="REQUEST_STATUS" HeaderText="REQUEST_STATUS" 
                     SortExpression="REQUEST_STATUS" Visible="false"/>
                 <asp:BoundField DataField="SUCCESSFUL_STATUS" HeaderText="SUCCESSFUL_STATUS" 
                     SortExpression="SUCCESSFUL_STATUS" Visible="false" />
                 <asp:BoundField DataField="OPERATOR_CODE" HeaderText="OPERATOR_CODE" 
                     SortExpression="OPERATOR_CODE"  Visible="false"/>
                 <asp:BoundField DataField="SSL_VRG_UNIQUE_ID" HeaderText="GU ID" 
                     SortExpression="SSL_VRG_UNIQUE_ID"  />    
                 <asp:BoundField DataField="SSL_CREATE_MESSAGE" HeaderText="Create Message" 
                     SortExpression="SSL_CREATE_MESSAGE" />
                 <asp:BoundField DataField="SSL_INT_MESSAGE" HeaderText="Initiate Message" 
                     SortExpression="SSL_INT_MESSAGE" />
                 <asp:BoundField DataField="SSL_FINAL_MESSAGE" HeaderText="Final Message" 
                     SortExpression="SSL_FINAL_MESSAGE" />
                     
                 <asp:BoundField DataField="REVERSE_STATUS" HeaderText="REVERSE_STATUS" 
                     SortExpression="REVERSE_STATUS" Visible="true" />
                     <asp:BoundField DataField="REVERSE_NOTE" HeaderText="Reverse Note" 
                     SortExpression="REVERSE_NOTE"  /> 
                     <asp:BoundField DataField="OWNER_CODE" HeaderText="Owner Code" 
                     SortExpression="OWNER_CODE"  /> 
             </Columns>
             <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
         </asp:GridView>    
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
    </form>
</body>
</html>
