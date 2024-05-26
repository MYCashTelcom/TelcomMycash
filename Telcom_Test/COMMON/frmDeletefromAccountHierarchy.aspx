<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDeletefromAccountHierarchy.aspx.cs"
    Inherits="COMMON_frmReplaceMobileNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Account Hierarchy</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scmMsgService" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UDPanel" runat="server">
        <ContentTemplate>
            <div style="background-color: royalblue; text-align: left;">
                <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">&nbsp;&nbsp;Manage Delete Account Hierarchy &nbsp;
                </span>&nbsp;&nbsp;               
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>                
            </div>
            <div >                
                <span style="font-size: 12px; font-weight: normal;">Delete by Wallet ID: </span>
                <asp:TextBox ID="txtWalletID" runat="server"></asp:TextBox>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                    onclick="btnDelete_Click"  />
            </div>           
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
