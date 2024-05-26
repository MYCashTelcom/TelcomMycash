<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngSMSResend.aspx.cs" Inherits="Forms_frmQuerySubmitStatus" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Manage SMS Resend</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <%--<META HTTP-EQUIV="Refresh" CONTENT="10"> --%>  
    <style type="text/css">
       .font_Color
       {
       	color:White;
       	}
    </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
    <div style="BACKGROUND-COLOR: royalblue">
      <table style="font-size:12px;">
         <tr>
            <td>
                 <strong><span class="font_Color" >Service 
                  Keyword</SPAN>
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
                 <asp:Button  ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
           </strong>
            <span class="font_Color" >
                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
            </span>
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
           
        
      </div>
      <div>
        <asp:SqlDataSource id="sdsAccountList" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'>
        </asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsRequestList" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT ROWNUM,SR.REQUEST_ID,SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME, DECODE(SR.REQUEST_STAE, 'P', 'In Que', 'Processed') AS REQ_STATE, SUBSTR(SR.REQUEST_TEXT, INSTR(SR.REQUEST_TEXT, '*', 2) + 1, LENGTH(SR.REQUEST_TEXT) - INSTR(SR.REQUEST_TEXT, '*', 2) - 1) AS REQ_TEXT, SR.SERVICE_COST, RSP.RESPONSE_TIME,RSP.RESPONSE_ID ,DECODE(RSP.RESPONSE_STAE, 'P', 'In Que', NULL, 'Waiting', 'Replied') AS RSP_STATE,RSP.RESPONSE_MESSAGE FROM SERVICE_REQUEST SR LEFT OUTER JOIN SERVICE_RESPONSE RSP ON SR.REQUEST_ID = RSP.REQUEST_ID">
        </asp:SqlDataSource>
        
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" CssClass="mGrid" 
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
              BorderStyle="None" onselectedindexchanged="grvRequestList_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" ReadOnly="True" 
                    SortExpression="REQUEST_ID" />
                <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Request Party" SortExpression="REQUEST_PARTY" />
                <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Receipent Party" SortExpression="RECEIPENT_PARTY" />
                <asp:BoundField DataField="REQUEST_TIME" HeaderText="Request Time" SortExpression="REQUEST_TIME" />
                <asp:BoundField DataField="REQ_STATE" HeaderText="Request State" SortExpression="REQ_STATE" />
                <asp:BoundField DataField="REQ_TEXT" HeaderText="Request Message" 
                    SortExpression="REQ_TEXT" />
                <asp:BoundField DataField="RESPONSE_ID" HeaderText="Reponse ID" 
                    SortExpression="RESPONSE_ID" />
                <asp:BoundField DataField="RSP_STATE" HeaderText="Response State" SortExpression="RSP_STATE" />
                <asp:BoundField DataField="RESPONSE_TIME" HeaderText="Response Time" SortExpression="RESPONSE_TIME" />
                <asp:BoundField DataField="RESPONSE_MESSAGE" HeaderText="Response Message" 
                    SortExpression="RESPONSE_MESSAGE" />
                <asp:CommandField  ShowSelectButton="true" SelectText="Resend" ButtonType="Button" />    
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        </div>
     </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
