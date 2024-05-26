<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Delete_Comi_Data.aspx.cs" Inherits="COMI_DISP_frm_Delete_Comi_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Commission Data</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;"><form id="form1" runat="server"> 

 <div style="background-color: royalblue">
                <strong><span style="color: white">Delete Commission Data &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </span></strong>
            </div>
            
            <br />
    <table>
    <tr><td >
        <asp:Label ID="lblSetupID" runat="server" Text="Setup ID"></asp:Label>
        </td><td >
            <asp:TextBox ID="txtSetupID" runat="server" Width="95px"></asp:TextBox>
        </td></tr>
    <tr>
    <td></td>
    <td>
        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="82px" onclick="btnDelete_Click" style="text-align: center" />
        </td></tr>
        <tr><td></td><td><asp:Label ID="lblMessage1" runat="server"></asp:Label></td></tr>
    </table>
  
  
    
      </form>
  
    
    </body>
</html>
