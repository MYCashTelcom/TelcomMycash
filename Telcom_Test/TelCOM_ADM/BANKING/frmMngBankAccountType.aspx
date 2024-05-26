<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankAccountType.aspx.cs" Inherits="BANKING_frmMngBankAccountType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Bank Account Type</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <div style="background-color: royalblue"> <strong><span style="color: white">Manage Bank Account Type</span></strong></div>
     <asp:SqlDataSource ID="sdsBankAccountTypeList" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         
         
         
         SelectCommand="SELECT BANK_ACC_TYPE_ID,BANK_ACC_TYPE_TITLE,BANK_ACC_TYPE_CODE FROM BANK_ACCOUNT_TYPE ORDER BY  BANK_ACC_TYPE_ID" InsertCommand="INSERT INTO BANK_ACCOUNT_TYPE (BANK_ACC_TYPE_TITLE,BANK_ACC_TYPE_CODE)
VALUES (:BANK_ACC_TYPE_TITLE,:BANK_ACC_TYPE_CODE) " 
         
         
         UpdateCommand="UPDATE BANK_ACCOUNT_TYPE SET BANK_ACC_TYPE_TITLE=:BANK_ACC_TYPE_TITLE,BANK_ACC_TYPE_CODE=:BANK_ACC_TYPE_CODE WHERE BANK_ACC_TYPE_ID=:BANK_ACC_TYPE_ID">
         <UpdateParameters>
             <asp:Parameter Name="BANK_ACC_TYPE_TITLE" />
             <asp:Parameter Name="BANK_ACC_TYPE_CODE" />
             <asp:Parameter Name="BANK_ACC_TYPE_ID" />
         </UpdateParameters>
         <InsertParameters>
             <asp:Parameter Name="BANK_ACC_TYPE_TITLE" />
             <asp:Parameter Name="BANK_ACC_TYPE_CODE" />
         </InsertParameters>
     </asp:SqlDataSource>
     <asp:GridView ID="gdvBankAccountTypeList" runat="server" 
         AutoGenerateColumns="False" BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" DataKeyNames="BANK_ACC_TYPE_ID" 
         DataSourceID="sdsBankAccountTypeList">
         <Columns>
             <asp:BoundField DataField="BANK_ACC_TYPE_ID" HeaderText="BANK_ACC_TYPE_ID" 
                 ReadOnly="True" SortExpression="BANK_ACC_TYPE_ID" Visible="False" />
             <asp:BoundField DataField="BANK_ACC_TYPE_TITLE" 
                 HeaderText="Account Title" SortExpression="BANK_ACC_TYPE_TITLE" >
             <HeaderStyle HorizontalAlign="Left" />
             </asp:BoundField>
             <asp:BoundField DataField="BANK_ACC_TYPE_CODE" HeaderText="Account Type Code" 
                 SortExpression="BANK_ACC_TYPE_CODE" >
             <HeaderStyle HorizontalAlign="Left" />
             </asp:BoundField>
             <asp:CommandField ButtonType="Button" ShowEditButton="True" />
         </Columns>
         <PagerStyle CssClass="pgr" />
         <AlternatingRowStyle CssClass="alt" />
     </asp:GridView>
     
     
     <div style="background-color: royalblue"> <strong><span style="color: white">Type Creation</span></strong></div>
     
     <asp:DetailsView ID="dtvTypeCreation" runat="server" 
         DataSourceID="sdsBankAccountTypeList" DefaultMode="Insert" Height="50px" 
         Width="259px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
         AutoGenerateRows="False">
         <PagerStyle CssClass="pgr" />
         <Fields>
             <asp:BoundField DataField="BANK_ACC_TYPE_TITLE" HeaderText="Account Title" 
                 SortExpression="BANK_ACC_TYPE_TITLE" />
             <asp:BoundField DataField="BANK_ACC_TYPE_CODE" HeaderText="Account Type Code" 
                 SortExpression="BANK_ACC_TYPE_CODE" />
             <asp:CommandField ShowInsertButton="True" ButtonType="Button" >
             <ItemStyle HorizontalAlign="Center" />
             </asp:CommandField>
         </Fields>
         <AlternatingRowStyle CssClass="alt" />
     </asp:DetailsView>
     
     </form>
     
    </body>
</html>
