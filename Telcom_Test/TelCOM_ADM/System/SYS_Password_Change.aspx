<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_Password_Change.aspx.cs" Inherits="System_SYS_Password_Change" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Password Change</title>
 
</head>
<body style="background-color: lightgrey;">
    
  <form id="form1" runat="server">
    <div>
         
         <div>
           <asp:ScriptManager ID="ScriptManager1" runat="server">
           </asp:ScriptManager>
          </div>
          <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <div>
                            <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            
                            SelectCommand='SELECT "CMP_BRANCH_ID", "CMP_BRANCH_NAME" FROM "CM_CMP_BRANCH"'>
                            </asp:SqlDataSource>
                        </div>
                        <div>
                        
                        <div style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Change LogIn Information  </SPAN></STRONG>
                        &nbsp;
                            <asp:Label ID="lblSpMessage" runat="server" Font-Bold="True" Font-Italic="True" 
                                Font-Size="Small" ForeColor="#CC0000"></asp:Label>
                           </div>
                        
        <table>
           
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="Login Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLogInName" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Text="Old Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="Enter New Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Rewrite New Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRepass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                </td>
                <td align="left">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        onclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
                    </div>
                    
                
                   
        </ContentTemplate>
        </asp:UpdatePanel>
     </div>
    </div>
   </form>
</body>
</html>
