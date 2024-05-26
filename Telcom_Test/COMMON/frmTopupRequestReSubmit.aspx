<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTopupRequestReSubmit.aspx.cs" Inherits="COMMON_frmTopupRequestReSubmit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI"  %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resubmit Topup Request</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
       <asp:PostBackTrigger ControlID="btnExport" /> 
     </Triggers>
     <ContentTemplate>
     <asp:SqlDataSource ID="sdsRequest" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         SelectCommand="SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,TRAN_AMOUNT,
                        REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,
                        SSL_CREATE_RECHAGE_STATUS,SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,SSL_INT_MESSAGE,SSL_FINAL_STATUS,
                        SSL_FINAL_MESSAGE,REVERSE_STATUS 
                        FROM TOPUP_TRANSACTION" 
         UpdateCommand="UPDATE &quot;TOPUP_TRANSACTION&quot; SET &quot;SUBSCRIBER_TYPE&quot; = :SUBSCRIBER_TYPE 
                        WHERE &quot;TOPUP_TRAN_ID&quot; = :TOPUP_TRAN_ID">
     </asp:SqlDataSource>
     <asp:Panel ID="pnlTop" runat="server" >
      <table class="Top_Panel" style="width:100%;">
       <tr>
        <td></td>
        <td></td>
        <td align="right">
          <asp:Label ID="lblMsg" runat="server"  ></asp:Label>
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
     <asp:Panel ID="Panel1" runat="server" CssClass="View_Panel">
        From Date
         <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
            TextBoxWidth="130">
            <calendartitlestyle  />
         </cc1:GMDatePicker>
       To Date
         <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
            TextBoxWidth="130" >
            <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
         </cc1:GMDatePicker>
      Source Wallet
        <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
         Subscriber Mobile No
        <asp:TextBox ID="txtSubscriberNo" runat="server" Width="112px"></asp:TextBox>
       <asp:Button  ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />
      </asp:Panel>       
      <asp:GridView ID="gdvRequest" runat="server" AllowPaging="True" 
              AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
              BorderColor="#E0E0E0" BorderStyle="None" CssClass="mGrid" 
              DataKeyNames="TOPUP_TRAN_ID" DataSourceID="sdsRequest" 
              onpageindexchanging="gdvRequest_PageIndexChanging" 
              onrowcancelingedit="gdvRequest_RowCancelingEdit" 
              onrowediting="gdvRequest_RowEditing" onrowupdating="gdvRequest_RowUpdating" OnSelectedIndexChanging="gdvRequest_SelectedIndexChanging"
              PagerStyle-CssClass="pgr" pagesize="13">
              <Columns>
                  <asp:BoundField DataField="TOPUP_TRAN_ID" HeaderText="Topup Transaction ID" 
                      ReadOnly="True" SortExpression="TOPUP_TRAN_ID" />
                  <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" ReadOnly="True" 
                      SortExpression="REQUEST_ID" />
                  <asp:BoundField DataField="TRAN_DATE" HeaderText="Transaction Date" 
                      ReadOnly="True" SortExpression="TRAN_DATE" />
                  <asp:BoundField DataField="SOURCE_ACCNT_NO" HeaderText="Source Wallet" 
                      ReadOnly="True" SortExpression="SOURCE_ACCNT_NO" />
                  <asp:BoundField DataField="SUBSCRIBER_MOBILE_NO" 
                      HeaderText="Subscriber Mobile No" ReadOnly="True" 
                      SortExpression="SUBSCRIBER_MOBILE_NO" />
                  <asp:TemplateField HeaderText="Subscriber Type" 
                      SortExpression="SUBSCRIBER_TYPE">
                      <EditItemTemplate>
                          <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" 
                              DataField="SUBSCRIBER_TYPE" SelectedValue='<%# Bind("SUBSCRIBER_TYPE") %>'>
                              <asp:ListItem Value="0">Prepaid</asp:ListItem>
                              <asp:ListItem Value="1">Postpaid</asp:ListItem>
                          </asp:DropDownList>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" 
                              DataField="SUBSCRIBER_TYPE" Enabled="false" 
                              SelectedValue='<%# Bind("SUBSCRIBER_TYPE") %>'>
                              <asp:ListItem Value="0">Prepaid</asp:ListItem>
                              <asp:ListItem Value="1">Postpaid</asp:ListItem>
                          </asp:DropDownList>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="TRAN_AMOUNT" HeaderText="Transaction Amount" 
                      ReadOnly="True" SortExpression="TRAN_AMOUNT" />
                  <asp:BoundField DataField="REQUEST_STATUS" HeaderText="REQUEST_STATUS" 
                      ReadOnly="True" SortExpression="REQUEST_STATUS" Visible="false" />
                  <asp:BoundField DataField="SUCCESSFUL_STATUS" HeaderText="SUCCESSFUL_STATUS" 
                      ReadOnly="True" SortExpression="SUCCESSFUL_STATUS" Visible="false" />
                  <asp:BoundField DataField="OPERATOR_CODE" HeaderText="OPERATOR_CODE" 
                      ReadOnly="True" SortExpression="OPERATOR_CODE" Visible="false" />
                  <asp:BoundField DataField="SSL_CREATE_RECHAGE_STATUS" 
                      HeaderText="SSL_CREATE_RECHAGE_STATUS" ReadOnly="True" 
                      SortExpression="SSL_CREATE_RECHAGE_STATUS" Visible="false" />
                  <asp:BoundField DataField="SSL_CREATE_MESSAGE" HeaderText="Create Message" 
                      ReadOnly="True" SortExpression="SSL_CREATE_MESSAGE" />
                  <asp:BoundField DataField="SSL_INT_RECHAGE_STATUS" 
                      HeaderText="SSL_INT_RECHAGE_STATUS" ReadOnly="True" 
                      SortExpression="SSL_INT_RECHAGE_STATUS" Visible="false" />
                  <asp:BoundField DataField="SSL_INT_MESSAGE" HeaderText="Initiate Message" 
                      ReadOnly="True" SortExpression="SSL_INT_MESSAGE" />
                  <asp:BoundField DataField="SSL_FINAL_STATUS" HeaderText="Final Code" 
                      ReadOnly="True" SortExpression="SSL_FINAL_STATUS" Visible="True" />
                  <asp:BoundField DataField="SSL_FINAL_MESSAGE" HeaderText="Final Message" 
                      ReadOnly="True" SortExpression="SSL_FINAL_MESSAGE" />
                  <asp:BoundField DataField="REVERSE_STATUS" HeaderText="REVERSE_STATUS" 
                      ReadOnly="True" SortExpression="REVERSE_STATUS" Visible="false" />
                   <asp:BoundField DataField="OWNER_CODE" HeaderText="Owner Code" 
                      ReadOnly="True" SortExpression="OWNER_CODE"  />    
                  <asp:TemplateField>
                      <ItemTemplate>
                          <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                              Text="Refresh" />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                  <asp:TemplateField>
                      <ItemTemplate>
                          <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                              Text="Resubmit" />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                      <ItemTemplate>
                          <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Reverse" 
                              Width="76px" />
                      </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
              <PagerStyle CssClass="pgr" />
              <AlternatingRowStyle CssClass="alt" />
          </asp:GridView>    
     </ContentTemplate>
	 <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />  
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
