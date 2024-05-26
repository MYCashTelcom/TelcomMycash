<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmISO_Query_status.aspx.cs" Inherits="COM_frmISO_Query_status"%>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ISO  Query Status</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
      
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    
     <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:SqlDataSource ID="sdsStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION WHERE DPS_OWNER='MBL'"></asp:SqlDataSource>
     <asp:UpdatePanel id="UpdatePanel1" runat="server">
     <ContentTemplate> 
     
     <%--SELECT * FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION WHERE DPS_OWNER='MBL'--%>
   
    <div style="background-color: royalblue">
   <span class="Font_Color" style="COLOR: white"> &nbsp;</span>
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
    <span class="Font_Color" style="COLOR: white"> Source </span>
    <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
     <span class="Font_Color" style="COLOR: white"> Destination</span>
    <asp:TextBox ID="txtSubscriberNo" runat="server" Width="112px"></asp:TextBox>
    <asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        </strong>
         <td colspan="3" align="center" style="font-size: 11px; font-weight: bold; color: White;">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>
             </td>
        </div>
        <div>
            <asp:GridView ID="gdvstatus" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="CAS_DPS_ID" DataSourceID="sdsStatus"
               CssClass="mGrid" Width="100%" PagerStyle-CssClass="pgr" 
             AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
             AllowPaging="True"  pagesize="15" BorderColor="#E0E0E0" ShowFooter="True" 
                onpageindexchanged="gdvstatus_PageIndexChanged" >             
                <Columns>
                    <asp:BoundField DataField="CAS_DPS_ID" HeaderText="DPS ID" ReadOnly="True" 
                        SortExpression="CAS_DPS_ID" />
                    <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" 
                        SortExpression="REQUEST_ID" />
                    <%--<asp:BoundField DataField="CAS_ACC_ID" HeaderText="Account ID" 
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
                    <asp:BoundField DataField="CAS_ISO_REQ_DESPATCH" HeaderText="Request Dispatch Status" 
                     SortExpression="CAS_ISO_REQ_DESPATCH" />
                    <asp:BoundField DataField="CAS_ISO_DESPATHCH_DATE" HeaderText="Request Dispatch Date" 
                        SortExpression="CAS_ISO_DESPATHCH_DATE" />
                    <asp:BoundField DataField="PRE_CAS_DPS_ID" HeaderText="Pre Dispatch ID" 
                        SortExpression="PRE_CAS_DPS_ID" />
                    <asp:BoundField DataField="CAS_DPS_RESUBMIT" HeaderText="Request Resubmit" 
                        SortExpression="CAS_DPS_RESUBMIT" />
                    <asp:BoundField DataField="ISO_RESPONSE_CODE" HeaderText="ISO Response Code" 
                        SortExpression="ISO_RESPONSE_CODE" />    
                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            </asp:GridView>
        </div>
         </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
