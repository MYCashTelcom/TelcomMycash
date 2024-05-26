<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDpdcPosList.aspx.cs" Inherits="Forms_frmDpdcPosList" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>     
    <%--<link type="text/css" href="../css/style.css" media="screen" rel="stylesheet"  />
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />--%>
         <link type="text/css" rel="Stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            height: 164px;
        }
        .fontColor
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
        <div style="BACKGROUND-COLOR: royalblue;">
          <div><h4>DPDC_PREPAID_METER_POS_LIST </h4></div>
            <td align="right">
           <asp:Label ID="lblMsg" runat="server" ></asp:Label>  
         </td>  
        </div>
        <div>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsBankTrans" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">           
        </asp:SqlDataSource>
            <div>
                  <strong>
               <span class="fontColor:BLACK"> Agent Id </span>
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="130px"></asp:TextBox> 
                               <span ></span>&nbsp; &nbsp;
                      <span class="fontColor:BLACK"> Pos List</span>

                        <asp:TextBox ID="txtPosList" runat="server" Width="130px"></asp:TextBox> 
                       <asp:Button   ID="Button1" runat="server" OnClick="btnSubmit_Click" Text="Submit" style="height: 26px" />
                 </span>
             </strong>
            </div>
        <%--<asp:GridView ID="grvRequestList"  runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" DataSourceID="sdsBankTrans" Font-Size="11pt" ShowFooter="true"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                BorderStyle="None">
            <Columns>
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
        </asp:GridView>--%>
        &nbsp;<br />&nbsp; &nbsp;
        </div>
</contenttemplate>         
    </asp:UpdatePanel>
    </form>
</body>
</html>
