<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSystemStatus.aspx.cs" Inherits="Forms_frmQuerySubmitStatus" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>System Status</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />     
    <style type="text/css">
       .font_Color
       {
       	color:White;
       	}
    </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<div style="BACKGROUND-COLOR: royalblue"><strong>
    <span class="font_Color" ></span>
     &nbsp;<span class="font_Color" >&nbsp;&nbsp;From Date</SPAN>
     <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="140">
                                <calendartitlestyle  />
                            </cc1:GMDatePicker>
    
    <span class="font_Color"> To Date</span>
     <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="140" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
    
   
     <asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
      </strong>
      </div>
      <div>     
        <asp:SqlDataSource ID="sdsSystemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
              SelectCommand="SELECT * FROM &quot;SYSTEM_STATUS&quot;"  >
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataKeyNames="SYS_STATE_ID" DataSourceID="sdsSystemStatus" CssClass="mGrid" 
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
              BorderStyle="None">
            <Columns>
                <asp:BoundField DataField="SYS_STATE_ID" HeaderText="SYS_STATE_ID" ReadOnly="True" 
                    SortExpression="SYS_STATE_ID" Visible="false" />
                <asp:BoundField DataField="SYS_STATE_TIME" HeaderText="Sys State Time" 
                    SortExpression="SYS_STATE_TIME" />
                <asp:BoundField DataField="SYS_RESPONSE_PENDING" HeaderText="Response Pending" 
                    SortExpression="SYS_RESPONSE_PENDING" />
                <asp:BoundField DataField="SYS_HEALTH_REMARKS" HeaderText="Health Remarks" 
                    SortExpression="SYS_HEALTH_REMARKS" />
                <asp:BoundField DataField="LAST_RESPONSE_TIME" HeaderText="Last Response Time" 
                    SortExpression="LAST_RESPONSE_TIME" />
                <asp:BoundField DataField="LAST_REQUEST_TIME" HeaderText="Last Requist Time" 
                    SortExpression="LAST_REQUEST_TIME" />
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
        &nbsp;<br />&nbsp; &nbsp;</div>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
