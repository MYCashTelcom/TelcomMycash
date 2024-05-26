<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQuerySubmitStatusMPW.aspx.cs" Inherits="TEXT_frmQuerySubmitStatusMPW" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Query Request Status Mannual Paywell</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <%--<META HTTP-EQUIV="Refresh" CONTENT="10"> --%>  
    <style type="text/css">
       .font_Color
       {
       	color:White;
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
    </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
     <Triggers>
       <asp:PostBackTrigger ControlID="btnExport" /> 
     </Triggers>
    <contenttemplate>
    <div style="BACKGROUND-COLOR: royalblue"><strong><span class="font_Color" >Service 
          Keyword</span>
    <asp:TextBox ID="txtServiceCode" runat="server" Width="56px"></asp:TextBox>
    <span class="font_Color" >&nbsp;(i.e. FT For Fund Transfer)&nbsp;From Date</span>
     <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
            TextBoxWidth="130">
            <calendartitlestyle  />
        </cc1:GMDatePicker>
    
   <span class="font_Color" > To Date</span>
     <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
    <span class="font_Color"> Mobile Number</span>
    <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />
      </strong>
      
      </div>
      
      <div style="BACKGROUND-COLOR: royalblue" align="right" >
        <strong>
           <asp:Label ID="lblMsg" runat="server" ForeColor="White" Text=""></asp:Label>
         </strong>
        </div>
      
      <div>
    
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
    
    <asp:SqlDataSource ID="sdsRequestList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT ROWNUM,SR.REQUEST_ID,SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME, DECODE(SR.REQUEST_STAE, 'P', 'In Que', 'Processed') AS REQ_STATE, SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1, LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1) AS REQ_TEXT, SR.SERVICE_COST, RSP.RESPONSE_TIME,RSP.RESPONSE_ID ,DECODE(RSP.RESPONSE_STAE, 'P', 'In Que', NULL, 'Waitting', 'Replied') AS RSP_STATE,RSP.RESPONSE_MESSAGE FROM SERVICE_REQUEST SR LEFT OUTER JOIN SERVICE_RESPONSE RSP ON SR.REQUEST_ID = RSP.REQUEST_ID">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sdsBankTrans" runat="server" 
           ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">           
    </asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sdsRequestID" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
    </asp:SqlDataSource>        
        
        
        
        
        <asp:GridView ID="grvRequestList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" CssClass="mGrid" 
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
              BorderStyle="None" onsorting="grvRequestList_Sorting" 
              onrowdatabound="grvRequestList_RowDataBound" 
              onselectedindexchanged="grvRequestList_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" ReadOnly="True" SortExpression="REQUEST_ID"/>
                <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Request Party" SortExpression="REQUEST_PARTY" />
                <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Receipent Party" SortExpression="RECEIPENT_PARTY" />
                <asp:BoundField DataField="REQUEST_TIME" HeaderText="Request Time" SortExpression="REQUEST_TIME" />
                <asp:BoundField DataField="REQ_STATE" HeaderText="Request State" SortExpression="REQ_STATE" />
                <asp:BoundField DataField="REQ_TEXT" HeaderText="Request Message" SortExpression="REQ_TEXT" />
                <asp:BoundField DataField="RESPONSE_ID" HeaderText="Reponse ID" SortExpression="RESPONSE_ID" />
                <asp:BoundField DataField="RSP_STATE" HeaderText="Response State" SortExpression="RSP_STATE" />
                <asp:BoundField DataField="RESPONSE_TIME" HeaderText="Response Time" SortExpression="RESPONSE_TIME" />
                <asp:BoundField DataField="RESPONSE_MESSAGE" HeaderText="Response Message" SortExpression="RESPONSE_MESSAGE" />
                <%--<asp:BoundField DataField="CCT_OWNER_CODE" HeaderText="CCT Agent" SortExpression="CCT_OWNER_CODE" />--%>
                
                
                 <asp:CommandField SelectText="Details" ShowSelectButton="true" ButtonType="Button" Visible="true" />
                </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        &nbsp;</DIV>
    </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
