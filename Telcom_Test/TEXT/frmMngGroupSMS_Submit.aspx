<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngGroupSMS_Submit.aspx.cs" Inherits="Forms_frmMngGroupSMS_Submit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Send Group Message: &nbsp;Account
    <asp:DropDownList ID="ddlAccountList" runat="server" AutoPostBack="True" DataSourceID="sdsAccountList"
        DataTextField="ACCNT_NO" DataValueField="ACCNT_ID">
    </asp:DropDownList>
    &nbsp; &nbsp; Group
    <asp:DropDownList ID="ddlGroupList" runat="server" AutoPostBack="True" DataSourceID="sdsGroupList"
        DataTextField="ACCNT_GROUP_NAME" DataValueField="ACCNT_GROUP_ID">
    </asp:DropDownList></SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "ACCNT_ID", "ACCNT_NO" FROM "ACCOUNT_LIST"'></asp:SqlDataSource> <asp:SqlDataSource id="sdsGroupList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "ACCOUNT_GROUP_LIST" WHERE ("ACCNT_ID" = :ACCNT_ID)'>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlAccountList" Name="ACCNT_ID" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsGroupSMS" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "GROUP_SMS_REQUEST" WHERE "GROUP_SMS_REQ_ID" = :GROUP_SMS_REQ_ID'
        InsertCommand='INSERT INTO "GROUP_SMS_REQUEST" ("GROUP_SMS_REQ_ID", "ACCNT_ID", "ACCNT_GROUP_ID", "GROUP_SMS_TEXT", "GROUP_SMS_RESP", "GROUP_SMS_REF_NO","GROUP_MESSAGE_TYPE") VALUES (:GROUP_SMS_REQ_ID, :ACCNT_ID, :ACCNT_GROUP_ID, :GROUP_SMS_TEXT, :GROUP_SMS_RESP, :GROUP_SMS_REF_NO,:GROUP_MESSAGE_TYPE)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "GROUP_SMS_REQUEST" WHERE ("ACCNT_GROUP_ID" = :ACCNT_GROUP_ID)'
        UpdateCommand='UPDATE "GROUP_SMS_REQUEST" SET "GROUP_SMS_REQ_TIME" = :GROUP_SMS_REQ_TIME, "ACCNT_ID" = :ACCNT_ID, "ACCNT_GROUP_ID" = :ACCNT_GROUP_ID, "GROUP_SMS_TEXT" = :GROUP_SMS_TEXT, "GROUP_SMS_RESP" = :GROUP_SMS_RESP, "GROUP_SMS_REF_NO" = :GROUP_SMS_REF_NO WHERE "GROUP_SMS_REQ_ID" = :GROUP_SMS_REQ_ID'>
        <DeleteParameters>
            <asp:Parameter Name="GROUP_SMS_REQ_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="GROUP_SMS_REQ_TIME" Type="DateTime" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_GROUP_ID" Type="String" />
            <asp:Parameter Name="GROUP_SMS_TEXT" Type="String" />
            <asp:Parameter Name="GROUP_SMS_RESP" Type="String" />
            <asp:Parameter Name="GROUP_SMS_REF_NO" Type="String" />
            <asp:Parameter Name="GROUP_SMS_REQ_ID" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlGroupList" Name="ACCNT_GROUP_ID" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="GROUP_SMS_REQ_ID" Type="String" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_GROUP_ID" Type="String" />
            <asp:Parameter Name="GROUP_SMS_TEXT" Type="String" />
            <asp:Parameter Name="GROUP_SMS_RESP" Type="String" />
            <asp:Parameter Name="GROUP_SMS_REF_NO" Type="String" />
            <asp:Parameter Name="GROUP_MESSAGE_TYPE" Type="String" />            
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:GridView id="GridView1" runat="server"  AutoGenerateColumns="False" BorderColor="White" DataKeyNames="GROUP_SMS_REQ_ID" DataSourceID="sdsGroupSMS" Font-Size="11pt" AllowPaging="True"><Columns>
    <asp:BoundField DataField="GROUP_SMS_REQ_ID" HeaderText="GROUP_SMS_REQ_ID" ReadOnly="True"
        SortExpression="GROUP_SMS_REQ_ID" Visible="False" />
    <asp:BoundField DataField="GROUP_SMS_REF_NO" HeaderText="Request No" SortExpression="GROUP_SMS_REF_NO" />
    <asp:BoundField DataField="GROUP_SMS_REQ_TIME" HeaderText="Request Time" SortExpression="GROUP_SMS_REQ_TIME" />
    <asp:BoundField DataField="GROUP_SMS_TEXT" HeaderText="Message" SortExpression="GROUP_SMS_TEXT" />
    <asp:BoundField DataField="GROUP_SMS_RESP" HeaderText="System Response" SortExpression="GROUP_SMS_RESP" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Submit New&nbsp;Message&nbsp;</SPAN></STRONG></DIV><DIV>
<asp:DetailsView id="dlvServiceType" runat="server" BorderColor="Silver" DataKeyNames="GROUP_SMS_REQ_ID" DataSourceID="sdsGroupSMS" Height="50px" Width="125px" AutoGenerateRows="False" DefaultMode="Insert" Font-Names="Times New Roman" Font-Size="11pt" OnItemInserting="dlvServiceType_ItemInserting" OnItemUpdating="dlvServiceType_ItemUpdating"><Fields>
    <asp:TemplateField HeaderText="Message Type" SortExpression="GROUP_MESSAGE_TYPE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("GROUP_MESSAGE_TYPE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:DropDownList ID="ddlMessagePurpose" runat="server" SelectedValue='<%# Bind("GROUP_MESSAGE_TYPE") %>'>
                <asp:ListItem Value="BDC">Broadcast</asp:ListItem>
                <asp:ListItem Value="INV">Invitation</asp:ListItem>
            </asp:DropDownList>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("GROUP_MESSAGE_TYPE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Message" SortExpression="GROUP_SMS_TEXT">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GROUP_SMS_TEXT") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Height="88px" MaxLength="160" Text='<%# Bind("GROUP_SMS_TEXT") %>'
                TextMode="MultiLine" Width="379px"></asp:TextBox>
        </InsertItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("GROUP_SMS_TEXT") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="GROUP_SMS_REF_NO" HeaderText="GROUP_SMS_REF_NO" SortExpression="GROUP_SMS_REF_NO"
        Visible="False" />
    <asp:CommandField ButtonType="Button" InsertText="Submit" ShowInsertButton="True">
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView>
    &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
