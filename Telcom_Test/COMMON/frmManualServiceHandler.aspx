<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmManualServiceHandler.aspx.cs" Inherits="COMMON_frmManualServiceHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manual Service handler</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <div style="background-color: royalblue; text-align: left;">
           <strong><span style="color: white; font-size: 11px;"> Manage Service Handler
              &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
           </span>
           </strong>
          </div>
           <fieldset style="border-color: #FFFFFF;width:550px; height:180px;margin:5 5 5 5 px">
            <legend><strong style="color: #666666">&nbsp;&nbsp;Manual Service Handler</strong></legend>
           <table >
             <tr> 
             <td valign="middle" style="width:210px;">
           
                <asp:Label ID="lblSender" runat="server" Text="Sender"></asp:Label><br />
                <asp:TextBox ID="txtSender" runat="server"></asp:TextBox><br />
                <asp:Label ID="lblReceipentParty" runat="server" Text="Receipent"></asp:Label><br />
                <asp:TextBox ID="txtReceipentParty" runat="server"></asp:TextBox>
           
            </td>
            <td valign="top" style="width:300px;">
                <asp:Label ID="lblMsg" runat="server" Text="Message" ></asp:Label><br />
                <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Height="100px" 
                    Width="300px" ></asp:TextBox>
                
           
            </td>
            </tr>
            <tr>
            <td colspan="2" align="center" style="margin-top:20px;">
                <asp:Button ID="btnAddRequest" runat="server" Text="Add Request" 
                    onclick="btnAddRequest_Click"  />
           
            </td></tr></table>
           </fieldset>
        </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
