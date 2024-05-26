<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCOMI_RFR_SETUP_IMPORT.aspx.cs"
    Inherits="frmCOMI_RFR_SETUP_IMPORT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data Import</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowProgressBar() 
        {            
            var panelProg1 = document.getElementById('divImage');
            panelProg1.style.display = '';         
        }

        function HideProgressBar()
        {
            var panelProg2 = document.getElementById('divImage');
            panelProg2.style.display = 'none';
        }
    </script>

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div>
   
    
    <div style="background-color: royalblue;">
        <strong><span style="color: white">Import new referance setupID</span></strong></div>    
    <table>
    <%--    <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Auto Import and Update Account Information"></asp:Label>
            </td>
        </tr>--%>
        <br />
        <tr>
            <td>
                <asp:Button ID="btnImportSub" runat="server" Text="Import" Width="183px"
                    OnClick="btnImportSub_Click" OnClientClick="ShowProgressBar()" Font-Names="Times New Roman" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="MediumBlue"></asp:Label><br />
                <div id="divImage" style="display: none">
                    Processing...
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/pleasewait.gif" />
                </div>
                <asp:Literal ID="ltlHide" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </div>    
    </form>
</body>
</html>
