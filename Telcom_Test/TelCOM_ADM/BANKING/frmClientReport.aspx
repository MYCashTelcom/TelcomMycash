<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmClientReport.aspx.cs" Inherits="frmClientReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div style="background-color: royalblue; text-align: left;">
                            <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">:: KYC Report&nbsp;::</span>
    </div>
    <div>   
    <table>
    <tr>
    <td>
                                        <span style="font-size: 12px; font-weight: bold;">
        Client Name:</span></td>
      <td>
          <asp:DropDownList ID="DropDownList1" runat="server" 
              DataSourceID="sdsClientList" DataTextField="CLINT_NAME" 
              DataValueField="CLINT_ID">
          </asp:DropDownList>
        </td>
   </tr>
   <tr>
   <td>
       <asp:SqlDataSource ID="sdsClientList" runat="server" 
           ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
           ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
           SelectCommand="SELECT CL.CLINT_NAME,CL.CLINT_ID FROM CLIENT_LIST CL ORDER BY TRIM(CL.CLINT_NAME)">
       </asp:SqlDataSource>
       </td>
       <td>
           <asp:Button ID="btnPrint" runat="server" Text="Print" 
               onclick="btnPrint_Click" />
       </td>
   
   </tr>
   
    </table>
    </div>
  
    </form>
</body>
</html>
