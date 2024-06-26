<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQueryBankTransactionNew.aspx.cs" Inherits="Forms_frmQueryBankTransaction" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Transaction History</title>     
    <link type="text/css" href="../css/style.css" media="screen" rel="stylesheet"  />
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
      <style type="text/css">
      .fontColor
      {
      	color:White;
      }
      #UpdateProgress1 
      {
        background-color:transparent;
        color:White;               
        top: 300px;
        left:512px; 
        position:fixed;
      }             
     #UpdateProgress1 img {
        vertical-align:middle;
        margin:2px;
      }
      </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
        <div style="BACKGROUND-COLOR: royalblue">
            <strong>
               <span class="fontColor">Query Bank Transaction</span>&nbsp;
                      <asp:DropDownList ID="ddlAccountList" runat="server" DataSourceID="sdsAccountList"  DataTextField="ACCNT_NO" 
                                     DataValueField="ACCNT_ID" OnSelectedIndexChanged="ddlAccountList_SelectedIndexChanged" Visible="False">
                       </asp:DropDownList>&nbsp;<span class="fontColor"> From Date</span>
                         <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="135" >
                                      <%-- <calendartitlestyle backcolor="#FFFFC0"  />--%>
                                     </cc1:GMDatePicker>
                          &nbsp;
                        <span class="fontColor"> To Date</span>
                        <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="135" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
                      &nbsp;
                     <span class="fontColor">  Wallet ID</span>
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="130px"></asp:TextBox>
                       <asp:Button   ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                       <asp:Button ID="btnExport" runat="server" Text="Export" 
                onclick="btnExport_Click" />
                 
             </strong>
        </div>
        <div>
    <asp:SqlDataSource id="sdsAccountList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'>
    </asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsBankTrans" runat="server" 
           ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">           
        </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsSelectedBankTransaction" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
             </asp:SqlDataSource>
            
            <asp:SqlDataSource ID="sdsRequestID" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
            </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList"  runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" DataSourceID="sdsBankTrans" Font-Size="11pt" ShowFooter="True"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                AllowPaging="True" onpageindexchanging="grvRequestList_PageIndexChanging" PageSize="15"
                BorderStyle="None" GridLines="None" 
                ondatabinding="grvRequestList_DataBinding" 
                ondatabound="grvRequestList_DataBound">
            <Columns>
                <asp:BoundField DataField="REQUEST_ID" HeaderText="Transaction Number" 
                    SortExpression="REQUEST_ID" />
                <asp:BoundField DataField="BANK_TRAN_DESC" HeaderText="Description" SortExpression="BANK_TRAN_DESC" />
                <asp:BoundField DataField="BANK_ACCOUNT_NO" HeaderText="Account No" SortExpression="BANK_ACCOUNT_NO" Visible="false" />
                <asp:BoundField DataField="AC_NAME" HeaderText="Account Name" SortExpression="AC_NAME" />
                <asp:BoundField DataField="CLINT_BANK_ACC_NO" HeaderText="ACC No" SortExpression="CLINT_BANK_ACC_NO" />
                <asp:BoundField DataField="BANK_INTERNAL_CODE" HeaderText="Bank Code" SortExpression="BANK_INTERNAL_CODE" />
                <asp:BoundField DataField="BANK_TRAN_DATE" HeaderText="Date" SortExpression="BANK_TRAN_DATE" />
                <asp:BoundField DataField="DEBIT" HeaderText="Debit" SortExpression="DEBIT" />
                <asp:BoundField DataField="CREDIT" HeaderText="Credit" SortExpression="CREDIT" />
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
            <asp:Label ID="lblRequestID" runat="server" Text="Select a Transaction Number" 
                Font-Bold="True" Visible="False"></asp:Label>        
            <asp:DropDownList ID="ddlReQuestID" runat="server" DataSourceID="sdsRequestID"  DataTextField="REQUEST_ID"
                 DataValueField="REQUEST_ID" AutoPostBack="true" 
                onselectedindexchanged="ddlReQuestID_SelectedIndexChanged" Visible="False">
            </asp:DropDownList>
            <asp:Button ID="btnView" runat="server" onclick="btnView_Click" Text="View" 
                Visible="False" />
                
            <asp:GridView ID="grvIndividualRequestList" runat="server" Font-Size="11pt" CssClass="mGrid"
         PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                 DataSourceID="sdsSelectedBankTransaction" BorderStyle="None" ShowFooter="true">
                <Columns>
                <asp:BoundField DataField="REQUEST_ID" HeaderText="Transaction Number" 
                        SortExpression="REQUEST_ID" />
                <asp:BoundField DataField="BANK_TRAN_DESC" HeaderText="Description" SortExpression="BANK_TRAN_DESC" />
                <asp:BoundField DataField="BANK_ACCOUNT_NO" HeaderText="Account No" SortExpression="BANK_ACCOUNT_NO" Visible="false" />
                <asp:BoundField DataField="AC_NAME" HeaderText="Account Name" SortExpression="AC_NAME" />
                <asp:BoundField DataField="CLINT_BANK_ACC_NO" HeaderText="ACC No" SortExpression="CLINT_BANK_ACC_NO" />
                <asp:BoundField DataField="BANK_INTERNAL_CODE" HeaderText="Bank Code" SortExpression="BANK_INTERNAL_CODE" />
                <asp:BoundField DataField="BANK_TRAN_DATE" HeaderText="Date" SortExpression="BANK_TRAN_DATE" />
                <asp:BoundField DataField="DEBIT" HeaderText="Debit" SortExpression="DEBIT" />
                <asp:BoundField DataField="CREDIT" HeaderText="Credit" SortExpression="CREDIT" />
                </Columns>
                <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
              <ProgressTemplate>
              <img  alt="loading" src="../Images/ajax-loader.gif"/>
              </ProgressTemplate>
           </asp:UpdateProgress>
        </div>
       </contenttemplate>
       <Triggers>
         <asp:PostBackTrigger ControlID="btnExport" />
       </Triggers>          
    </asp:UpdatePanel>
    </form>
</body>
</html>
