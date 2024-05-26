<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQuerySubmitStatusM.aspx.cs" Inherits="Forms_frmQuerySubmitStatus"%>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Query Request Status Mannual</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>   
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
     <Triggers>
       <asp:PostBackTrigger ControlID="btnExport" /> 
     </Triggers>
    <contenttemplate>
    <div>
        <asp:SqlDataSource ID="sdsRequestList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">
        </asp:SqlDataSource>            
    </div>
    <div style="BACKGROUND-COLOR: royalblue">
     <table>
       <tr>
        <td>
          <strong><span class="font_Color" >Service Keyword</span>
            <asp:TextBox ID="txtServiceCode" runat="server" Width="56px"></asp:TextBox>
            <span class="font_Color" >&nbsp;From Date</span>
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
            <span class="font_Color"> Mobile No</span>
            <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
            <span class="font_Color"> Request ID</span>
              <asp:TextBox ID="txtRequestID" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
            <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />
        </strong>
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
      <div style="BACKGROUND-COLOR: royalblue" align="right" >
        <strong>
           <asp:Label ID="lblMsg" runat="server" ForeColor="White" Text=""></asp:Label>
         </strong>
        </div>      
      <div>        
        <asp:GridView ID="grvRequestList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="REQUEST_ID" DataSourceID="sdsRequestList" CssClass="mGrid" 
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
            BorderStyle="None" onsorting="grvRequestList_Sorting"               
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
                <asp:CommandField SelectText="Details" ShowSelectButton="true" ButtonType="Button" Visible="true" />
                </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        &nbsp;</div>
      </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
