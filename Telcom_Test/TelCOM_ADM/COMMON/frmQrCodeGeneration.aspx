<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQrCodeGeneration.aspx.cs" Inherits="COMMON_frmQrCodeGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QrCode Generation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

    <style type="text/css">
        .Top_Panel {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            font-size: 12px;
            color: White;
        }

        .View_Panel {
            background-color: powderblue;
            height: 20px;
            width: 100%;
            font-size: 12px;
            font-weight: bold;
            color: White;
        }

        .Inser_Panel {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
            width: 100%;
            font-size: 12px;
            font-weight: bold;
        }

        .style1 {
            width: 200px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:SqlDataSource ID="sdsBranch" runat="server"
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                    SelectCommand="SELECT * FROM CM_CMP_BRANCH"></asp:SqlDataSource>
                <asp:Panel ID="pnlTop" runat="server">
                    <table style="width: 100%" class="Top_Panel">
                        <tr>
                            <td class="style1">QrCode Generation</td>
                            <td style="text-align: left;">&nbsp;&nbsp;
             <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                                    DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="pnlView" runat="server">
                    <table style="font-size: 12px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblWalletNumber" runat="server" Text="Wallet Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWalletNumber" runat="server"
                                    Placeholder="Please insert wallet number with comma (,) separator" Width="561px" Columns="250" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Insert Wallet Number"
                                    ControlToValidate="txtWalletNumber"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnGenerateQr" runat="server" Text="Generate QR Code" OnClick="btnGenerateQr_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:GridView ID="gdvQrCodeDetail" runat="server" CssClass="mGrid"></asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
