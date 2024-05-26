<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAdminAccountReset.aspx.cs" Inherits="COMMON_frmAdminAccountReset" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reset PIN</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UDPanel" runat="server">
            <ContentTemplate>
                <div style="background-color: royalblue">
                    <table>
                        <tr>
                            <td>
                                <strong><span style="color: white; font-size:12px;">Reset System User password</span></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAdmin" runat="server" OnClick="btnAdmin_Click" Text="Search" />
                            </td>
                            <td style="color: white; font-size:12px;">
                                <strong><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></strong>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding:10px">
                    <asp:Panel ID="pnlUserDetail" runat="server" Width="300">
                        <asp:DetailsView ID="dtvClient" runat="server" AutoGenerateRows="False" Visible="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" BorderStyle="None">
                            <PagerStyle CssClass="pgr" />
                            <Fields>
                                <asp:BoundField DataField="SYS_USR_DNAME" HeaderText="User Name"
                                    SortExpression="SYS_USR_DNAME" />
                                <asp:BoundField DataField="LOCKED_STATUS" HeaderText="User Status"
                                    SortExpression="LOCKED_STATUS" />
                                <asp:BoundField DataField="PASSWORD_EXPIRED_DATE" HeaderText="Password Expired Date"
                                    SortExpression="PASSWORD_EXPIRED_DATE" />
                                <asp:BoundField DataField="CLICK_FAILURE" HeaderText="Try Count"
                                    SortExpression="CLICK_FAILURE" />
                            </Fields>
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:DetailsView>
                    </asp:Panel>
                    <asp:Button ID="btnResetAdminPIN" runat="server" Visible="false" OnClick="btnResetAdminPIN_Click" Text="  Reset Admin PIN " />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
