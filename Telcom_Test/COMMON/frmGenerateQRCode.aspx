<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGenerateQRCode.aspx.cs" Inherits="COMMON_frmGenerateQRCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }

        .View_Panel {
            padding: 10px;
        }

        .Inser_Panel {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div>
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%" class="Top_Panel">
                    <tr>
                        <td>Print QRCode</td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                <ProgressTemplate>
                                    <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="View_Panel">
                        <table>
                            <tr>
                                <td>Account Rank</td>
                                <td>
                                    <%--<asp:TextBox ID="txtWalletNumber" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlAccountRank" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="plBarCode1" runat="server" />
                                </td>
                                <td>
                                    <asp:PlaceHolder ID="plBarCode2" runat="server" />
                                </td>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodeName1" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCodeName2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnpl1Download" runat="server" Text="Download" Visible="false" OnClick="btnpl1Download_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnpl2Download" runat="server" Text="Download" Visible="false" OnClick="btnpl2Download_Click" />
                                </td>
                            </tr>
                                <%--<td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnUpload" runat="server" BackColor="#009933" Font-Bold="True"
                                        ForeColor="White" Height="30px" OnClick="btnUpload_Click" Text="Upload"
                                        Width="100px" />
                                    <asp:Button ID="btnSave" runat="server" BackColor="#009900" Font-Bold="True"
                                        ForeColor="White" Height="30px" OnClick="btnSave_Click" Text="Save"
                                        Width="100px" />
                                </td>
                                <td class="style1">
                                    <asp:Image ID="Image1" runat="server" Height="293px" Width="584px" />
                                </td>--%>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnpl1Download" />
                    <asp:PostBackTrigger ControlID="btnpl2Download" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
