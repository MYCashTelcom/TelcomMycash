<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBroadcastMSG.aspx.cs" Inherits="Forms_frmSubmitMSG" Title="NITL VAS Portal" %>

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
        <DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">&nbsp;Send Broadcast Message: &nbsp;
    Account
    <asp:DropDownList ID="ddlAccountList" runat="server" AutoPostBack="True" DataSourceID="sdsAccountList"
        DataTextField="ACCNT_NO" DataValueField="ACCNT_ID">
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp;<asp:SqlDataSource ID="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT ACCNT_ID, ACCNT_NO || ' [' || ACCNT_MSISDN||']' AS ACCNT_NO FROM ACCOUNT_LIST">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsQuizList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT QUIZ_ID, QUIZ_TITLE || ' (' || QUIZ_CODE || ')' AS QUIZ_NAME,QUIZ_TEXT FROM QUIZ_LIST WHERE (ACCNT_ID = :ACCNT_ID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlAccountList" Name="ACCNT_ID" PropertyName="SelectedValue"
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </SPAN></STRONG></DIV><DIV>
<TABLE style="border-bottom: darkgray thin solid; border-left: darkgray thin solid; border-top: darkgray thin solid; border-right: darkgray thin solid;" border="1" cellpadding="0" cellspacing="0">
<TBODY>
<tr><td style="text-align: right">
    <strong>Message Sender</strong></td><td>
    <asp:TextBox ID="txtMessageSender" runat="server"></asp:TextBox></td></tr>
<TR><TD style="WIDTH: 31px; text-align: right;" nowarp><STRONG style="text-align: right">Receipent Numbers</STRONG></TD><TD style="WIDTH: 212px"><asp:TextBox id="txtMSISDN" runat="server" TextMode="MultiLine" Width="527px" Height="89px"></asp:TextBox><br />
    <span style="font-size: 10pt"><strong><span style="color: #0000cc">MSISDN1</span></strong>;<strong><span
        style="color: #0000cc">MSISDN2</span></strong>;<strong><span style="color: #0000cc">MSISDN3</span></strong>;<strong><span
            style="color: #0000cc">MSISDN4</span></strong></span></TD></TR>
            <tr><td style="text-align: right">
                <strong>Message Purpose</strong></td><td>
                <asp:DropDownList ID="ddlMessagePurpose" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMessagePurpose_SelectedIndexChanged">
                    <asp:ListItem Value="BDC">Broadcast</asp:ListItem>
                    <asp:ListItem Value="INV">Invitation</asp:ListItem>
                    <asp:ListItem Value="QUZ">Quiz</asp:ListItem>
                </asp:DropDownList>
                    <asp:DropDownList ID="ddlQuizList" runat="server" DataSourceID="sdsQuizList" DataTextField="QUIZ_NAME" DataValueField="QUIZ_ID" AutoPostBack="True" OnSelectedIndexChanged="ddlQuizList_SelectedIndexChanged" Visible="False">
                    </asp:DropDownList></td></tr>
            <TR><TD style="WIDTH: 31px; text-align: right;" nowarp><STRONG style="text-align: right">Message</STRONG></TD><TD style="WIDTH: 212px"><asp:TextBox id="txtMessage" runat="server" TextMode="MultiLine" Width="527px" Height="89px"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 26px" align=center colSpan=2><asp:Button id="btnSend" onclick="btnSend_Click" runat="server" Text="Submit"></asp:Button></TD></TR></TBODY></TABLE><BR /><BR /><BR /><BR /><BR />
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

