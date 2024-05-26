<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMZoneZoneCellSites.aspx.cs" Inherits="Forms_frmMZoneZoneCellSites" Title="Manage Zone Cell" %>
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
<TABLE cellSpacing=0 cellPadding=0 border=0 frame=void><TBODY><TR><TD colSpan=3><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Mobile Zone <asp:DropDownList id="DropDownList4" runat="server" DataTextField="MZONE_PKG_NAME" DataValueField="MZONE_PKG_ID" DataSourceID="sdsMZonePackage" AutoPostBack="True"></asp:DropDownList> <asp:SqlDataSource id="sdsMZonePackage" runat="server" SelectCommand='SELECT "MZONE_PKG_ID", "MZONE_PKG_NAME", "MZONE_PKG_SUBS_FEE", "MZONE_PKG_MONTH_FEE", "MZONE_PKG_DFEE_BOM" FROM "MZONE_PACKAGE"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" UpdateCommand='UPDATE "MZONE_PACKAGE" SET "MZONE_PKG_NAME" = :MZONE_PKG_NAME, "MZONE_PKG_SUBS_FEE" = :MZONE_PKG_SUBS_FEE, "MZONE_PKG_MONTH_FEE" = :MZONE_PKG_MONTH_FEE, "MZONE_PKG_DFEE_BOM" = :MZONE_PKG_DFEE_BOM WHERE "MZONE_PKG_ID" = :MZONE_PKG_ID' InsertCommand='INSERT INTO "MZONE_PACKAGE" ("MZONE_PKG_ID", "MZONE_PKG_NAME", "MZONE_PKG_SUBS_FEE", "MZONE_PKG_MONTH_FEE", "MZONE_PKG_DFEE_BOM") VALUES (:MZONE_PKG_ID, :MZONE_PKG_NAME, :MZONE_PKG_SUBS_FEE, :MZONE_PKG_MONTH_FEE, :MZONE_PKG_DFEE_BOM)' DeleteCommand='DELETE FROM "MZONE_PACKAGE" WHERE "MZONE_PKG_ID" = :MZONE_PKG_ID'><DeleteParameters>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        </DeleteParameters>        
        <UpdateParameters>
        <asp:Parameter Name="MZONE_PKG_NAME" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_SUBS_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_MONTH_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_DFEE_BOM" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        </UpdateParameters>
        <InsertParameters>
        <asp:Parameter Name="MZONE_PKG_ID" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_NAME" Type="String"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_SUBS_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_MONTH_FEE" Type="Decimal"></asp:Parameter>
        <asp:Parameter Name="MZONE_PKG_DFEE_BOM" Type="String"></asp:Parameter>
        </InsertParameters>
        </asp:SqlDataSource> <asp:SqlDataSource id="sdsMZoneCellSites" runat="server" SelectCommand='SELECT "MZONE_GROUP_ID", "MZONE_ID", "MZONE_CELL_ID" FROM "MZONE_GROUP" WHERE ("MZONE_ID" = :MZONE_ID)' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList4" Name="MZONE_ID" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource> <asp:SqlDataSource id="sdsMZoneNICellSites" runat="server" SelectCommand='SELECT "MZONE_CELL_ID", "MZONE_CELL_TITLE", "MZONE_CELL_CELLID" FROM "MZONE_CELL_LIST"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
        </asp:SqlDataSource> </SPAN></STRONG></DIV></TD></TR><TR style="BORDER-RIGHT: gainsboro thin solid; BORDER-TOP: gainsboro thin solid; BORDER-LEFT: gainsboro thin solid; BORDER-BOTTOM: gainsboro thin solid; BACKGROUND-COLOR: gainsboro"><TD noWrap colSpan=2><SPAN style="COLOR: blue"><SPAN style="TEXT-DECORATION: underline"><STRONG>Added Cell Sites</STRONG> &nbsp;</SPAN> &nbsp;</SPAN> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</TD><TD noWrap><STRONG><SPAN><SPAN style="COLOR: navy"><SPAN style="COLOR: blue; TEXT-DECORATION: underline">Left Cell Sites</SPAN> </SPAN>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </SPAN></STRONG></TD></TR><TR><TD vAlign=top><DIV><asp:CheckBoxList id="CheckBoxList2" runat="server" DataTextField="MZONE_CELL_ID" DataValueField="MZONE_CELL_ID" DataSourceID="sdsMZoneCellSites"></asp:CheckBoxList> <BR />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </DIV></TD><TD style="WIDTH: 50px"><asp:Button id="btnAddCellSites" runat="server" Text="  <<  "></asp:Button><BR /><asp:Button id="btnRemoveCellsites" runat="server" Text="  >>  "></asp:Button></TD><TD vAlign=top><DIV><STRONG><SPAN style="COLOR: white"></SPAN></STRONG><asp:CheckBoxList id="CheckBoxList1" runat="server" DataTextField="MZONE_CELL_TITLE" DataValueField="MZONE_CELL_ID" DataSourceID="sdsMZoneNICellSites"></asp:CheckBoxList>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </DIV></TD></TR><TR><TD colSpan=3><DIV style="BACKGROUND-COLOR: royalblue">&nbsp; &nbsp; &nbsp;&nbsp; </DIV></TD></TR></TBODY></TABLE>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
