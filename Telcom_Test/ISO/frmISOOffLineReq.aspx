<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmISOOffLineReq.aspx.cs" Inherits="COM_frmISOOffLineReq" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ISO Offline Request</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
       <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        <style type="text/css" >        
        .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	font-size:12px;         	
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
             height:20px;
         	 color:White;
         }     
            .style5
            {
                width: 192px;
            }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
   <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<asp:UpdatePanel id="UpdatePanel1" runat="server" onprerender="UpdatePanel1_PreRender">--%>
    <asp:UpdatePanel id="UpdatePanel1" runat="server" >
     <ContentTemplate>      
        <asp:SqlDataSource ID="sdsOffline" runat="server" 
          ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
          ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
         SelectCommand="">
     </asp:SqlDataSource>
      <asp:SqlDataSource ID="sdsVendorType" runat="server" 
          ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
          ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
         SelectCommand="">
     </asp:SqlDataSource>
     <asp:Panel ID="Panel1" runat="server">
        <table width="850px" class="Top_Panel">
          <tr>
           <td style="width: 110px">
             <asp:Label ID="Label1" runat="server" Text=" From Date:" ForeColor="White"></asp:Label> &nbsp;
           </td>
           <td style="width: 160px">
             <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="130"   >
                <calendartitlestyle  ForeColor="Blue" />
              </cc1:GMDatePicker>
              &nbsp;  
           </td>
           <td style="width: 100px">
             <asp:Label ID="Label2" runat="server" Text="To Date:" ForeColor="White"></asp:Label> &nbsp; 
           </td>   
           <td class="style5">
             <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="130" ForeColor="Black" >
                <calendartitlestyle backcolor="#FFFFC0" ForeColor="Black"  Font-Size="X-Small" />
              </cc1:GMDatePicker>
              &nbsp;  
           </td>
           <td style="width: 60px">
             <asp:Label ID="Label3" runat="server" Text="Source" ForeColor="White"></asp:Label> &nbsp;  
           </td>    
           <td style="width: 100px">
             <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
              &nbsp;  
           </td>   
         </tr>
         <tr>
           <td style="width: 110px">
             <asp:Label ID="Label4" runat="server" Text=" MSS Account" ForeColor="White"></asp:Label> &nbsp;  
           </td>
           <td style="width: 160px">
             <asp:TextBox ID="txtSubscriberNo" runat="server" Width="112px"></asp:TextBox> &nbsp;  
           </td>  
           <td style="width: 100px">
             <asp:DropDownList ID="ddlVendorType" runat="server" DataSourceID="sdsVendorType" 
                      DataTextField="VENDOR_CODE" DataValueField="VENDOR_CODE" AppendDataBoundItems="true"
                       AutoPostBack="true">
              </asp:DropDownList>
              &nbsp;  
           </td> 
           <td class="style5">
             <asp:Button  ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
              &nbsp;
              
              <asp:Button ID="btnExport" runat="server" Text="Despatch" onclick="btnExport_Click" />
               <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                         DisplayModalPopupID="ModalPopupExtender2" onclientcancel="canceButtonlClick" 
                         TargetControlID="btnExport">
             </ajaxToolkit:ConfirmButtonExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                         BackgroundCssClass="modalBackground" CancelControlID="btnCancle" 
                         OkControlID="btnOK" TargetControlID="btnExport" PopupControlID="pnlPopUpExc">
             </ajaxToolkit:ModalPopupExtender>
             
             &nbsp;
             </td>   
           <td ></td>
           <td style="width: 100px">
             <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>   
           </td>  
              
             <asp:Panel ID="pnlPopUpExc" runat="server"  style=" display:none;width:300px; height:165px;
                  background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
                  <div style=" text-align:center; height:95px;color:Black;"><br />&nbsp;<br />&nbsp;
                    Are you sure you want to Despatch? <br />&nbsp;<br />&nbsp;
                  </div>
                  <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                        <br />&nbsp;
                        <asp:Button ID="btnOK" runat="server" Text="  Yes  " ValidationGroup="Update" />
                        <asp:Button ID="btnCancle" runat="server" Text=" Cancel " />
                  </div>
            </asp:Panel>
           
           
          </tr>
        </table> 
       </asp:Panel>
    <div>
        <asp:GridView ID="gdvOffline" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataKeyNames="CAS_DPS_ID" DataSourceID="sdsOffline"
            CssClass="mGrid" Width="100%" PagerStyle-CssClass="pgr" 
             AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
              pagesize="13" BorderColor="#E0E0E0" ShowFooter="True" 
            onpageindexchanged="gdvOffline_PageIndexChanged">
            <Columns>
                <asp:BoundField DataField="CAS_DPS_ID" HeaderText="DPS ID" ReadOnly="True" 
                        SortExpression="CAS_DPS_ID" />
                    <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" 
                        SortExpression="REQUEST_ID" />
                   <%-- <asp:BoundField DataField="CAS_ACC_ID" HeaderText="Account ID" 
                        SortExpression="CAS_ACC_ID" />
                    <asp:BoundField DataField="CAS_AC_PRE_BAL" HeaderText="Account Pre Balance" 
                        SortExpression="CAS_AC_PRE_BAL" />
                    <asp:BoundField DataField="CAS_AC_END_BAL" HeaderText="Account End Balance" 
                        SortExpression="CAS_AC_END_BAL" />--%>
                    <asp:BoundField DataField="CAS_TRAN_DATE" HeaderText="Transaction Date" 
                        SortExpression="CAS_TRAN_DATE" />
                    <asp:BoundField DataField="CAS_TRAN_AMT" HeaderText="Transaction Amount" 
                        SortExpression="CAS_TRAN_AMT" />
                    <asp:BoundField DataField="DPS_REF_CODE" HeaderText="MSS Account" 
                        SortExpression="DPS_REF_CODE" />
                    <asp:BoundField DataField="UPLOAD_STATUS" HeaderText="Upload Status" 
                        SortExpression="UPLOAD_STATUS" />
                    <asp:BoundField DataField="DPS_OWNER" HeaderText="DPS Owner" 
                        SortExpression="DPS_OWNER" />
                  
                   <%-- <asp:BoundField DataField="DEPOSIT_MONTH" HeaderText="DEPOSIT_MONTH" 
                        SortExpression="DEPOSIT_MONTH" />
                    <asp:BoundField DataField="DEPOSIT_TYPE" HeaderText="DEPOSIT_TYPE" 
                        SortExpression="DEPOSIT_TYPE" />--%>
                    <asp:BoundField DataField="CAS_ISO_REQ_STATUS" HeaderText="ISO Request Status" 
                        SortExpression="CAS_ISO_REQ_STATUS" />
                <asp:BoundField DataField="CAS_ISO_REQ_DESPATCH" 
                    HeaderText="ISO Request Despatch" SortExpression="CAS_ISO_REQ_DESPATCH" />
              <%--  <asp:BoundField DataField="ISO_RESPONSE_CODE" HeaderText="ISO Response Code" 
                        SortExpression="ISO_RESPONSE_CODE" /> --%>
            </Columns>
            <PagerStyle CssClass="pgr"></PagerStyle>
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        </asp:GridView>
    </div>
     </ContentTemplate>
      <Triggers>
     <%-- <asp:AsyncPostBackTrigger ControlID="btnExport" />--%>
        <asp:PostBackTrigger ControlID="btnExport" />
       </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
