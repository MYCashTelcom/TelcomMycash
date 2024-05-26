<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAPSNG_PKG_ChangeBonus.aspx.cs" Inherits="Forms_frmPackageChangeBonus" Title="Package Change Bonus" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
<asp:ScriptManager id="ScriptManager1" runat="server"> 
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Package&nbsp;Change Bonus</SPAN></STRONG></DIV><DIV><asp:SqlDataSource id="sdsIN_Pkg_Chng_rule" runat="server" SelectCommand='SELECT "APSNG_IN_PCRL_ID", "APSNG_IN_PCRL_ONVBNS", "APSNG_IN_PCRL_OFVBNS", "APSNG_IN_PCRL_INTVBNS", "APSNG_IN_PCRL_ONSBNS", "APSNG_IN_PCRL_OFSBNS", "APSNG_IN_PCRL_INSBNS", "APSNG_IN_PCRL_MMSBNS", "APSNG_IN_PCRL_DATABNS", "APSNG_IN_PCRL_MONYBNS", "APSNG_IN_PCRL_MSG", "APSNG_IN_PCRL_DESC", "APSNG_IN_PCRL_CODE" FROM "APSNG_IN_PKG_CRL"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" UpdateCommand='UPDATE "APSNG_IN_PKG_CRL" SET "APSNG_IN_PCRL_ONVBNS" = :APSNG_IN_PCRL_ONVBNS, "APSNG_IN_PCRL_OFVBNS" = :APSNG_IN_PCRL_OFVBNS, "APSNG_IN_PCRL_INTVBNS" = :APSNG_IN_PCRL_INTVBNS, "APSNG_IN_PCRL_ONSBNS" = :APSNG_IN_PCRL_ONSBNS, "APSNG_IN_PCRL_OFSBNS" = :APSNG_IN_PCRL_OFSBNS, "APSNG_IN_PCRL_INSBNS" = :APSNG_IN_PCRL_INSBNS, "APSNG_IN_PCRL_MMSBNS" = :APSNG_IN_PCRL_MMSBNS, "APSNG_IN_PCRL_DATABNS" = :APSNG_IN_PCRL_DATABNS, "APSNG_IN_PCRL_MONYBNS" = :APSNG_IN_PCRL_MONYBNS, "APSNG_IN_PCRL_MSG" = :APSNG_IN_PCRL_MSG WHERE "APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID' InsertCommand='INSERT INTO "APSNG_IN_PKG_CRL" ("APSNG_IN_PCRL_ID", "APSNG_IN_PCRL_ONVBNS", "APSNG_IN_PCRL_OFVBNS", "APSNG_IN_PCRL_INTVBNS", "APSNG_IN_PCRL_ONSBNS", "APSNG_IN_PCRL_OFSBNS", "APSNG_IN_PCRL_INSBNS", "APSNG_IN_PCRL_MMSBNS", "APSNG_IN_PCRL_DATABNS", "APSNG_IN_PCRL_MONYBNS", "APSNG_IN_PCRL_MSG", "APSNG_IN_PCRL_DESC", "APSNG_IN_PCRL_CODE") VALUES (:APSNG_IN_PCRL_ID, :APSNG_IN_PCRL_ONVBNS, :APSNG_IN_PCRL_OFVBNS, :APSNG_IN_PCRL_INTVBNS, :APSNG_IN_PCRL_ONSBNS, :APSNG_IN_PCRL_OFSBNS, :APSNG_IN_PCRL_INSBNS, :APSNG_IN_PCRL_MMSBNS, :APSNG_IN_PCRL_DATABNS, :APSNG_IN_PCRL_MONYBNS, :APSNG_IN_PCRL_MSG, :APSNG_IN_PCRL_DESC, :APSNG_IN_PCRL_CODE)' DeleteCommand='DELETE FROM "APSNG_IN_PKG_CRL" WHERE "APSNG_IN_PCRL_ID" = :APSNG_IN_PCRL_ID'><DeleteParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ONVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OFVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_INTVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ONSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OFSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_INSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MMSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_DATABNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MONYBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>
<asp:Parameter Name="APSNG_IN_PCRL_ID" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ONVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OFVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_INTVBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_ONSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_OFSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_INSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MMSBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_DATABNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MONYBNS" Type="Decimal"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_MSG" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_DESC" Type="String"></asp:Parameter>
<asp:Parameter Name="APSNG_IN_PCRL_CODE" Type="String"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:GridView id="gdvSysUsrGroup" runat="server" Font-Size="11pt" BorderColor="#E0E0E0" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="APSNG_IN_PCRL_ID" DataSourceID="sdsIN_Pkg_Chng_rule"><Columns>
<asp:BoundField DataField="APSNG_IN_PCRL_ID" HeaderText="APSNG_IN_PCRL_ID" ReadOnly="True" SortExpression="APSNG_IN_PCRL_ID" Visible="False"></asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_DESC" HeaderText="Rule Description" ReadOnly="True" SortExpression="APSNG_IN_PCRL_DESC">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_CODE" HeaderText="Rule Code" ReadOnly="True" SortExpression="APSNG_IN_PCRL_CODE">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_MONYBNS" HeaderText="Money" SortExpression="APSNG_IN_PCRL_MONYBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_ONVBNS" HeaderText="On-Net Voice" SortExpression="APSNG_IN_PCRL_ONVBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_OFVBNS" HeaderText="Off-Net Voice" SortExpression="APSNG_IN_PCRL_OFVBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_INTVBNS" HeaderText="International Voice" SortExpression="APSNG_IN_PCRL_INTVBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_ONSBNS" HeaderText="On-Net SMS" SortExpression="APSNG_IN_PCRL_ONSBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_OFSBNS" HeaderText="Off-Net SMS" SortExpression="APSNG_IN_PCRL_OFSBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_INSBNS" HeaderText="Internatioal SMS" SortExpression="APSNG_IN_PCRL_INSBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_MMSBNS" HeaderText="MMS" SortExpression="APSNG_IN_PCRL_MMSBNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APSNG_IN_PCRL_DATABNS" HeaderText="Data" SortExpression="APSNG_IN_PCRL_DATABNS">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:CommandField ShowEditButton="True" ButtonType="Button" EditText=" Edit "></asp:CommandField>
</Columns>
</asp:GridView>&nbsp;&nbsp; </DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
