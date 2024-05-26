<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmManageDevice.aspx.cs" Inherits="COMMON_frmManageDevice" %>

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
            padding:10px;
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
                        <td>Manage Device</td>
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
                            <td>Wallet Id</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtWalletId" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchWallet" runat="server" Text="Search" OnClick="btnSearchWallet_Click"/>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvWalletDetails" runat="server" CssClass="mGrid" AutoGenerateColumns="false" OnRowDeleting="gdvWalletDetails_RowDeleting" DataKeyNames="DEVICE_LIST_ID" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Wallet Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWalletNumber" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terminal Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerminalName" runat="server" Text='<%# Bind("TERMINAL_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terminal Serial">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerminalSerial" runat="server" Text='<%# Bind("TERMINAL_SERIAL_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActiveStatus" runat="server" Text='<%# Bind("ACTIVE_STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivationDate" runat="server" Text='<%# Bind("ACTIVATION_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="edit" Text="Edit" Visible="false" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="delete" Text="Delete" OnClientClick="return confirm('Are you sure to Delete?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
