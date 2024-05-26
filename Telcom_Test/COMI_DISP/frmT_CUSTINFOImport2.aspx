<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmT_CUSTINFOImport2.aspx.cs"
    Inherits="COMI_DISP_frmT_CUSTINFOImport2" %>
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
            var iFrame = document.getElementById('Iframe1');
            iFrame.Src = "frmShowMessage.aspx";    
        }

//        function HideProgressBar()
//        {
//            var panelProg2 = document.getElementById('divImage');
//            panelProg2.style.display = 'none';
//        }
    </script>

</head>
<body style="background-color: lightgrey;">
<iframe id="Iframe1" src="blank1.htm" height="50"  width="300" style="border-style: none"></iframe>
    <form id="form1" runat="server">
    <div>
    <%--<div style="BACKGROUND-COLOR: royalblue">
    
    <table>
    
    <tr>
    <td>     
        <asp:Label ID="lblMessage" runat="server" Text="" 
        ForeColor="MediumBlue"></asp:Label><br />
        <asp:Button ID="btnImportSub" runat="server" Text="Import Data" Width="183px" 
            onclick="btnImportSub_Click" />
        </td>
    
    </tr>
    
    
    </table>--%>
    <%--<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <%--<script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            var panelProg = $get('divImage');
            panelProg.style.display = '';
            // reset label text
            var lbl = $get('<%= this.lblMessage.ClientID %>');
            lbl.innerHTML = '';

            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = true;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divImage and hide it again
            var panelProg = $get('divImage');
            panelProg.style.display = 'none';

            // Enable button that caused a postback
            $get(sender._postBackSettings.sourceElement.id).disabled = false;
        }
         </script>--%>
    <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>--%>
    
    <div style="background-color: royalblue;">
        <strong><span style="color: white">New Account Create and Update</span></strong></div>
    <%--<iframe id="Iframe1" src="" height="200"  width="300" style="border-style: none"></iframe>--%>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Auto Import and Update Account Information"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnImportSub" runat="server" Text="Create New Account" Width="183px"
                
                    OnClick="btnImportSub_Click" OnClientClick="ShowProgressBar()" Font-Names="Times New Roman" />
                    <%--OnClick="btnImportSub_Click" iFrame.Src = "frmShowMessage.aspx" Font-Names="Times New Roman" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="MediumBlue"></asp:Label><br />
                <%--<div id="divImage" style="display: none">
                    Processing...
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/pleasewait.gif" />
                </div>
                <asp:Literal ID="ltlHide" runat="server"></asp:Literal>--%>
            </td>
        </tr>
    </table>
    </div>
    <%--</contenttemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnImportSub" EventName="Click" />        
    </Triggers>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
