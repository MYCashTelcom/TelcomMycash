<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngSrvNotification.aspx.cs" Inherits="COMMON_frmMngSrvNotification" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="scmMsgService" runat="server">
    </asp:ScriptManager>
    <div style="background-color: royalblue;">
        <strong><span style="color: white">Manage Services&nbsp; for Service Type
            <asp:DropDownList ID="ddlServiceType" runat="server" DataSourceID="sdsServiceType"
                DataTextField="SERVICE_TYPE_NAME" DataValueField="SERVICE_TYPE_ID" AutoPostBack="True">
            </asp:DropDownList>
            &nbsp;
        <asp:Button ID="btnPrintPreview" runat="server" onclick="btnPrintPreview_Click" 
            Text="Print Preview" />
            <asp:SqlDataSource id="sdsService" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE, SERVICE_ACCESS_CODE, NOTIFI_FOR_APARTY, NOTIFI_FOR_BPARTY, NOTIFI_FOR_CPARTY, NOTIFI_FOR_DUPLICATE, NOTIFI_FOR_EXPIRY FROM SERVICE_LIST WHERE (SERVICE_TYPE_ID = :SERVICE_TYPE_ID) ORDER BY SERVICE_TYPE_ID" 
DeleteCommand="DELETE FROM SERVICE_LIST WHERE (SERVICE_ID = :SERVICE_ID)" 

            
            
            UpdateCommand="UPDATE SERVICE_LIST SET NOTIFI_FOR_APARTY = :NOTIFI_FOR_APARTY, NOTIFI_FOR_BPARTY = :NOTIFI_FOR_BPARTY, NOTIFI_FOR_CPARTY = :NOTIFI_FOR_CPARTY, NOTIFI_FOR_DUPLICATE = :NOTIFI_FOR_DUPLICATE, NOTIFI_FOR_EXPIRY = :NOTIFI_FOR_EXPIRY WHERE (SERVICE_ID = :SERVICE_ID)">
<DeleteParameters>
    <asp:Parameter Name="SERVICE_ID">
    </asp:Parameter>
</DeleteParameters>
<SelectParameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="SERVICE_TYPE_ID" ControlID="ddlServiceType"></asp:ControlParameter>
</SelectParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="NOTIFI_FOR_APARTY"></asp:Parameter>
<asp:Parameter Type="String" Name="NOTIFI_FOR_BPARTY"></asp:Parameter>
<asp:Parameter Type="String" Name="NOTIFI_FOR_CPARTY"></asp:Parameter>
<asp:Parameter Type="String" Name="NOTIFI_FOR_DUPLICATE"></asp:Parameter>
<asp:Parameter Type="String" Name="NOTIFI_FOR_EXPIRY"></asp:Parameter>
<asp:Parameter Name="SERVICE_ID" Type="String"></asp:Parameter>
</UpdateParameters>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsServiceType" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "SERVICE_TYPE_ID", "SERVICE_TYPE_NAME" FROM "SERVICE_TYPE"'></asp:SqlDataSource> </span></strong>
    </div>
    <asp:UpdatePanel id="udpMngService" runat="server">
        <contenttemplate>
<DIV><asp:GridView id="gdvServiceList" runat="server" DataSourceID="sdsService" 
        AutoGenerateColumns="False" AllowSorting="True" 
    DataKeyNames="SERVICE_ID" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" 
        onrowupdated="gdvServiceList_RowUpdated">
    <Columns>
<asp:BoundField DataField="SERVICE_TITLE" SortExpression="SERVICE_TITLE" 
            HeaderText="Service Name" ReadOnly="True">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_ACCESS_CODE" SortExpression="SERVICE_ACCESS_CODE" 
            HeaderText="Access Code" ReadOnly="True">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
        <asp:TemplateField HeaderText="A Party Notification" 
            SortExpression="NOTIFI_FOR_APARTY">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Height="65px" MaxLength="140" 
                    Text='<%# Bind("NOTIFI_FOR_APARTY") %>' TextMode="MultiLine" Width="227px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NOTIFI_FOR_APARTY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="B Barty Notification" 
            SortExpression="NOTIFI_FOR_BPARTY">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Height="65px" MaxLength="140" 
                    Text='<%# Bind("NOTIFI_FOR_BPARTY") %>' TextMode="MultiLine" Width="229px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("NOTIFI_FOR_BPARTY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="C Party Notification" 
            SortExpression="NOTIFI_FOR_CPARTY">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Height="65px" MaxLength="140" 
                    Text='<%# Bind("NOTIFI_FOR_CPARTY") %>' TextMode="MultiLine" Width="225px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("NOTIFI_FOR_CPARTY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Duplicate Request Notification" 
            SortExpression="NOTIFI_FOR_DUPLICATE">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox4" runat="server" Height="65px" MaxLength="140" 
                    Text='<%# Bind("NOTIFI_FOR_DUPLICATE") %>' TextMode="MultiLine" Width="226px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" 
                    Text='<%# Bind("NOTIFI_FOR_DUPLICATE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Expiry Notification" 
            SortExpression="NOTIFI_FOR_EXPIRY">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox5" runat="server" Height="65px" MaxLength="140" 
                    Text='<%# Bind("NOTIFI_FOR_EXPIRY") %>' TextMode="MultiLine" Width="226px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("NOTIFI_FOR_EXPIRY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
<asp:CommandField DeleteText="" ShowEditButton="True"></asp:CommandField>
</Columns>
    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView> </DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
