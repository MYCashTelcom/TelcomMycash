<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSQL_Terminal.aspx.cs" Inherits="Forms_frmSQL_Terminal" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id=SCManager runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id=UDPanel runat="server">
        <contenttemplate>
        <DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">&nbsp;Online SQL Terminal &nbsp; &nbsp;<asp:SqlDataSource
                ID="sdsSQL" runat="server"></asp:SqlDataSource>
        </SPAN></STRONG></DIV><DIV>
<TABLE style="border-bottom: darkgray thin solid; border-left: darkgray thin solid; border-top: darkgray thin solid; border-right: darkgray thin solid;" border="1" cellpadding="0" cellspacing="0">
<TBODY>
<tr><td style="text-align: right"><strong>SQL</strong></td>
    <td><asp:TextBox ID="txtSQL" runat="server" Height="130px" TextMode="MultiLine" Width="803px"></asp:TextBox></td>
    <TD style="HEIGHT: 26px" align=center><asp:Button id="btnExecute" onclick="btnExecute_Click" runat="server" Text="Execute"></asp:Button></TD>
</tr>
</Table>
<table>
<TR><td style="width: 204px">
    <asp:GridView ID="gdvOutput" runat="server" AllowPaging="True"  CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</td></TR></TBODY></TABLE>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

