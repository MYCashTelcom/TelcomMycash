<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKYCReport.aspx.cs" Inherits="COMMON_frmKYCReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KYC Report</title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:SqlDataSource ID="sdsAllInfo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="">
     </asp:SqlDataSource>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            <div style="BACKGROUND-COLOR: royalblue">
               <strong><span style="COLOR: white">&nbsp;Wallet ID&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWalletID" runat="server"></asp:TextBox>
                   <asp:Button ID="btnReport" runat="server"  Text="Report" 
                    onclick="btnReport_Click"/>
                 <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
               </span></strong>
            </div>
         </ContentTemplate>
       </asp:UpdatePanel>
    </form>
</body>
</html>
