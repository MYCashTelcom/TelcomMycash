<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTax_Rate.aspx.cs" Inherits="COMI_DISP_frmTax_Rate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tax Rate</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <div style="background-color: royalblue">
                <strong><span style="color: white">Tax Rate for Bangla Commission Statement &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </span></strong>
            </div>
            <br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         DataSourceID="sdsTaxList" CssClass="mGrid" BorderStyle="None">
         <Columns>
             <asp:BoundField DataField="TAX_RATE" HeaderText="Tax Rate" 
                 SortExpression="TAX_RATE" >
             <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
             <asp:BoundField DataField="TAX_YEAR" HeaderText="TAX_YEAR" 
                 SortExpression="TAX_YEAR" Visible="False" />
             <asp:BoundField DataField="TAX_DETAILS" HeaderText="Details" 
                 SortExpression="TAX_DETAILS" />
             <asp:CommandField ButtonType="Button" ShowEditButton="True" />
         </Columns>
     </asp:GridView>
     <asp:SqlDataSource ID="sdsTaxList" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         SelectCommand="SELECT TAX_RATE,TAX_YEAR,TAX_DETAILS FROM COM_TAX_RATE" 
         
         UpdateCommand="UPDATE COM_TAX_RATE SET TAX_RATE=:TAX_RATE,TAX_YEAR=:TAX_YEAR,TAX_DETAILS=:TAX_DETAILS">
         <UpdateParameters>
             <asp:Parameter Name="TAX_RATE" />
             <asp:Parameter Name="TAX_YEAR" />
             <asp:Parameter Name="TAX_DETAILS" />
         </UpdateParameters>
     </asp:SqlDataSource>
    </form>
</body>
</html>
