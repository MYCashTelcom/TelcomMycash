

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmABStoMYCashReport.aspx.cs" Inherits="COMMON_frmABStoMYCashReport" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agent Current Account Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
       
        <div width="100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtpFrom" />
                    <asp:AsyncPostBackTrigger ControlID="dtpTo" />
                    <asp:PostBackTrigger ControlID="btnShow" />
                    
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
                <ContentTemplate>

                    
                            
                            <div style="background-color: powderblue">
                                <strong><span style="color: white"></span></strong>
                                <table style="background-color: royalblue">
                                    <tr>
                                        
                                       <td>
                                        <asp:Label ID="Label5" runat="server" Text="Date From"></asp:Label></td>
                                    <td>
                                        <cc1:GMDatePicker ID="dtpFrom" runat="server" DateFormat="MM/dd/yyyy hh:mm:ss tt" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="150">
                                        </cc1:GMDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblto" runat="server" Style="position: relative;"
                                            Text="To"></asp:Label></td>

                                    <td>
                                        <cc1:GMDatePicker ID="dtpTo" runat="server" DateFormat="MM/dd/yyyy hh:mm:ss tt" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="150">
                                        </cc1:GMDatePicker>
                                    </td>
                                        <td>
                                            <asp:Button ID="btnShow" runat="server" Font-Size="10pt" OnClick="btnShow_Click" Style="position: relative;" Text="Show" Width="87px" />
                                        </td>
                                        
                                        <td>
                                            <asp:Button ID="btnExport" runat="server" Font-Size="10pt" OnClick="btnExport_Click" Style="position: relative;" Text="Export" Width="87px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            </td>
                        </tr>
                   
                    </table>
                    </div>
                    <asp:GridView ID="gdvAccountDetail" runat="server" CssClass="mGrid" ShowHeaderWhenEmpty="true" EmptyDataText="No data found......" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="MYCash Wallet No">
                                <ItemTemplate>
                                    <asp:Label ID="lblAgentNo" runat="server" Text='<%# Bind("MYCASH_WALLET") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ABS Account No">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnintName" runat="server" Text='<%# Bind("ABS_ACCOUNT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccnNmbr" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client NID No">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrfx" runat="server" Text='<%# Bind("CLIENT_NID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Creation Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFchqNmbr" runat="server" Text='<%# Bind("CREATION_DATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ABS Registered By Agent">
                                <ItemTemplate>
                                    <asp:Label ID="lblFchqNmbr" runat="server" Text='<%# Bind("ABS_AGENT_ACCOUNT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tag Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblLchqNmbr" runat="server" Text='<%# Bind("IS_TAGGED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                

            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>



