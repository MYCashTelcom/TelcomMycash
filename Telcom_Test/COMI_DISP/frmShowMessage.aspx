<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmShowMessage.aspx.cs" Inherits="COMI_DISP_frmShowMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div>
        <%--<asp:Label ID="lblMessage" runat="server" ForeColor="MediumBlue"></asp:Label><br />--%>
        <div id="divImage"> <%--style="display: none"--%>
            Processing...
            <asp:Image ID="img1" runat="server" ImageUrl="~/Images/pleasewait.gif" />
        </div>
    </div>
    </form>
</body>
</html>
