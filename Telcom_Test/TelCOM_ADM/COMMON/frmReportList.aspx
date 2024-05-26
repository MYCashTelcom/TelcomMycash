<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReportList.aspx.cs" Inherits="Forms_frmReportList" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id=SCManager runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id=UDPanel runat="server">
        <contenttemplate>
        <DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">&nbsp;Report List&nbsp; &nbsp;<asp:SqlDataSource ID="sdsReportList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "REPORT_LIST"' DeleteCommand='DELETE FROM "REPORT_LIST" WHERE "REPORT_ID" = :REPORT_ID' InsertCommand='INSERT INTO "REPORT_LIST" ("REPORT_ID", "REPORT_NAME", "REPORT_QUERY", "REPORT_PARAMETER1", "REPORT_PARAMETER2", "REPORT_PARAMETER3", "REPORT_PARAMETER4", "REPORT_FILE_RPT") VALUES (:REPORT_ID, :REPORT_NAME, :REPORT_QUERY, :REPORT_PARAMETER1, :REPORT_PARAMETER2, :REPORT_PARAMETER3, :REPORT_PARAMETER4, :REPORT_FILE_RPT)' UpdateCommand='UPDATE "REPORT_LIST" SET "REPORT_NAME" = :REPORT_NAME, "REPORT_QUERY" = :REPORT_QUERY, "REPORT_PARAMETER1" = :REPORT_PARAMETER1, "REPORT_PARAMETER2" = :REPORT_PARAMETER2, "REPORT_PARAMETER3" = :REPORT_PARAMETER3, "REPORT_PARAMETER4" = :REPORT_PARAMETER4, "REPORT_FILE_RPT" = :REPORT_FILE_RPT WHERE "REPORT_ID" = :REPORT_ID'>
            <DeleteParameters>
                <asp:Parameter Name="REPORT_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="REPORT_NAME" Type="String" />
                <asp:Parameter Name="REPORT_QUERY" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER1" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER2" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER3" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER4" Type="String" />
                <asp:Parameter Name="REPORT_FILE_RPT" Type="String" />
                <asp:Parameter Name="REPORT_ID" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="REPORT_ID" Type="String" />
                <asp:Parameter Name="REPORT_NAME" Type="String" />
                <asp:Parameter Name="REPORT_QUERY" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER1" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER2" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER3" Type="String" />
                <asp:Parameter Name="REPORT_PARAMETER4" Type="String" />
                <asp:Parameter Name="REPORT_FILE_RPT" Type="String" />
            </InsertParameters>
            </asp:SqlDataSource>
        </SPAN></STRONG></DIV><DIV>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BorderColor="Silver" DataKeyNames="REPORT_ID" DataSourceID="sdsReportList" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="REPORT_ID" HeaderText="REPORT_ID" ReadOnly="True" SortExpression="REPORT_ID"
                        Visible="False" />
                    <asp:BoundField DataField="REPORT_NAME" HeaderText="Report Name" SortExpression="REPORT_NAME" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" SortExpression="DESCRIPTION" />
                    <asp:BoundField DataField="REPORT_FILE_RPT" HeaderText="Report File" SortExpression="REPORT_FILE_RPT" >
                        <ControlStyle Width="0px" />
                        <ItemStyle Width="0px" />
                    </asp:BoundField>
                    <asp:CommandField ButtonType="Button" SelectText="View" ShowSelectButton="True" />
                    <asp:BoundField DataField="REPORT_QUERY" HeaderText="REPORT_QUERY" SortExpression="REPORT_QUERY"
                        Visible="False" />
                    <asp:BoundField DataField="REPORT_PARAMETER1" HeaderText="REPORT_PARAMETER1" SortExpression="REPORT_PARAMETER1"
                        Visible="False" />
                    <asp:BoundField DataField="REPORT_PARAMETER2" HeaderText="REPORT_PARAMETER2" SortExpression="REPORT_PARAMETER2"
                        Visible="False" />
                    <asp:BoundField DataField="REPORT_PARAMETER3" HeaderText="REPORT_PARAMETER3" SortExpression="REPORT_PARAMETER3"
                        Visible="False" />
                    <asp:BoundField DataField="REPORT_PARAMETER4" HeaderText="REPORT_PARAMETER4" SortExpression="REPORT_PARAMETER4"
                        Visible="False" />
                </Columns>
            </asp:GridView>
            &nbsp;<BR /><BR />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
