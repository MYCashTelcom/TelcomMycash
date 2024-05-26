<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmManageAccntStatus.aspx.cs" Inherits="Forms_frmManageAccntStatus" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reset Status</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UDPanel" runat="server">
            <ContentTemplate>
                <div style="background-color: royalblue; padding: 5px;">
                    <strong>
                        <span style="color: white;">&nbsp;&nbsp; Manage Account Status&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </span></strong>
                </div>
                <div>

                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <span style="color: black; font-size:12px; font-weight:bolder;">Search By Wallet Id :</span> 
                                <asp:TextBox ID="txtWalletNumber" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearchByWallet" runat="server" Text="Search" OnClick="btnSearchByWallet_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="center">
                                <asp:GridView ID="gdvSearch" runat="server" AllowPaging="True" AllowSorting="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    BorderColor="#E0E0E0" CssClass="mGrid" DataKeyNames="ACCNT_NO" EmptyDataText="No Data Found ...."
                                    OnPageIndexChanging="gdvSearch_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" PageSize="15">
                                    <Columns>
                                        <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" SortExpression="ACCNT_NO" />
                                        <%-- <asp:TemplateField HeaderText="Wallet ID" SortExpression="ACCNT_NO"><%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:TextBox>
                            </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ACCNT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Wallet Name" SortExpression="CLINT_NAME"><%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("CLINT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address" SortExpression="CLINT_ADDRESS1"><%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ACCNT_MSISDN") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("CLINT_ADDRESS1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ACCNT_STATE" HeaderText="Account State" SortExpression="ACCNT_STATE" />
                                        <asp:BoundField DataField="ACCNT_LOCKED_TIME" HeaderText="Account Locked Time" SortExpression="ACCNT_LOCKED_TIME" />

                                        <asp:TemplateField ShowHeader="False">

                                            <ItemTemplate>
                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" CausesValidation="False" Text="Reset Pin" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle ForeColor="White" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="center"></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
