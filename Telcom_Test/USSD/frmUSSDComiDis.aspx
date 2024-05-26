<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUSSDComiDis.aspx.cs" Inherits="COMMON_frmUSSDComiDis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USSD Commission Disbursement</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:SqlDataSource ID="sdsComiDis" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT REQUEST_PARTY_NAME,ACCNT_NO,REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TEXT,SERVICE_TITLE,COLLECTED_COM_AMOUNT,CAS_TRAN_DATE,AIRTEL_COMMISSION,
                       COMMI_DIS_MASTER_ID FROM BDMIT_ERP_101.CAS_COMMISSION_DISBURSEMENT ORDER BY CAS_TRAN_DATE DESC">
    </asp:SqlDataSource>
    <div style="background-color: royalblue;width:auto;">
      <%--<span style="color: white">--%>       
        <table>
         <tr>
          <td>
              <asp:Label ID="Label1" runat="server" Font-Bold="true" Text=" USSD Commission Statement" style="color: white"></asp:Label>
         </td>
         <td style="color: white">
          <asp:RadioButtonList ID="rblType" runat="server"  Font-Bold="true"  RepeatDirection="Horizontal">
              <asp:ListItem Value="0">All</asp:ListItem>
              <asp:ListItem Value="1">Disbursed</asp:ListItem>
              <asp:ListItem Value="2">Payable</asp:ListItem>
          </asp:RadioButtonList>  
          </td>
          <td>     
              <asp:Label ID="lblWait0" runat="server" BackColor="Transparent" Font-Bold="True" Text="From Date" style="color: white" >
              </asp:Label>
              <cc1:GMDatePicker ID="txtFromDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                  CalendarOffsetY="25px" CalendarTheme="Silver" CalendarWidth="200px" CallbackEventReference=""
                  Culture="English (United States)" DateFormat="dd-MMM-yyyy" DateString=''
                   MaxDate="9999-12-31" MinDate="1900-01-01" NextMonthText="&gt;"
                  NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" Style="position: relative"
                  TextBoxWidth="90" ZIndex="1">
               <CalendarTitleStyle BackColor="#FFFFC0" />
              </cc1:GMDatePicker>
              <asp:Label ID="lblWait1" runat="server" BackColor="Transparent" Font-Bold="True"
                   Text="To Date" style="color: white" >
              </asp:Label>
              <cc1:GMDatePicker ID="txtToDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                  CalendarOffsetY="25px" CalendarTheme="Silver" CalendarWidth="200px" CallbackEventReference=""
                  Culture="English (United States)" DateFormat="dd-MMM-yyyy" DateString=''
                  EnableDropShadow="True" MaxDate="9999-12-31" MinDate="1900-01-01" NextMonthText="&gt;"
                  NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" Style="position: relative"
                  TextBoxWidth="90" ZIndex="1">
              <CalendarTitleStyle BackColor="#FFFFC0" />
              </cc1:GMDatePicker>
              <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
              <asp:Button ID="btnExport" runat="server" Text="Export" 
                  onclick="btnExport_Click" />
           </td>
          </tr>
         </table> 
      <%--  </span>--%>
       </div>        
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
           BorderStyle="None" BorderColor="#E0E0E0"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            DataSourceID="sdsComiDis" AllowPaging="True"  PageSize="25">
         <Columns>
            <asp:BoundField DataField="REQUEST_PARTY_NAME" HeaderText="Request Party Name" 
                SortExpression="REQUEST_PARTY_NAME" />
            <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet" 
                SortExpression="ACCNT_NO" />
            <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" 
                SortExpression="REQUEST_ID" />
            <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Request Party" 
                SortExpression="REQUEST_PARTY" />
            <asp:BoundField DataField="RECEIPENT_PARTY" HeaderText="Request Party Type" 
                SortExpression="RECEIPENT_PARTY" />
            <asp:BoundField DataField="REQUEST_TEXT" HeaderText="Request Text" 
                SortExpression="REQUEST_TEXT" />
            <asp:BoundField DataField="SERVICE_TITLE" HeaderText="Service Name" 
                SortExpression="SERVICE_TITLE" />
            <asp:BoundField DataField="COLLECTED_COM_AMOUNT" 
                HeaderText="Collected Commission Amount" 
                SortExpression="COLLECTED_COM_AMOUNT" Visible="false" />
            <asp:BoundField DataField="CAS_TRAN_DATE" HeaderText="Transaction date" 
                SortExpression="CAS_TRAN_DATE" />
            <asp:BoundField DataField="AIRTEL_COMMISSION" HeaderText="Airtel Commission" 
                SortExpression="AIRTEL_COMMISSION" />
            <asp:BoundField DataField="COMMI_DIS_MASTER_ID" HeaderText="Disburse ID" 
                SortExpression="COMMI_DIS_MASTER_ID" />
        </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
    </form>
</body>
</html>
