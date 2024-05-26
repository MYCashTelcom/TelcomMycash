<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="frmMngService.aspx.cs" Inherits="Forms_frmMsgService" Title="Manage Services" %>

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
            <asp:SqlDataSource id="sdsService" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand="SELECT SERVICE_ID, SERVICE_ACCESS_CODE, SERVICE_SHORT_CODE, SERVICE_STATE, SERVICE_TITLE, SERVICE_CREATION_DATE, SERVICE_TYPE_ID, SERVICE_INTERNAL_CODE, SERVICE_REMARKS, SERVICE_OTP, SERVICE_SQA, SERVICE_REQF_MESSAGE, SERVICE_REPLY_CODE,OTP_IN_SAME_REQUEST,OTP_CUST_IN_REQUEST FROM SERVICE_LIST WHERE (SERVICE_TYPE_ID = :SERVICE_TYPE_ID) ORDER BY SERVICE_TYPE_ID" 
DeleteCommand="DELETE FROM SERVICE_LIST WHERE (SERVICE_ID = :SERVICE_ID)" 
InsertCommand="INSERT INTO SERVICE_LIST(SERVICE_ACCESS_CODE, SERVICE_SHORT_CODE, SERVICE_STATE, SERVICE_TITLE, SERVICE_TYPE_ID, SERVICE_INTERNAL_CODE, SERVICE_REMARKS, SERVICE_REQF_MESSAGE, SERVICE_REPLY_CODE) VALUES (:SERVICE_ACCESS_CODE, :SERVICE_SHORT_CODE, :SERVICE_STATE, :SERVICE_TITLE, :SERVICE_TYPE_ID, :SERVICE_INTERNAL_CODE, :SERVICE_REMARKS, :SERVICE_REQF_MESSAGE, :SERVICE_REPLY_CODE)" 
UpdateCommand="UPDATE SERVICE_LIST SET SERVICE_ACCESS_CODE = :SERVICE_ACCESS_CODE, SERVICE_SHORT_CODE = :SERVICE_SHORT_CODE, SERVICE_STATE = :SERVICE_STATE, SERVICE_TITLE = :SERVICE_TITLE, SERVICE_CREATION_DATE = :SERVICE_CREATION_DATE, SERVICE_TYPE_ID = :SERVICE_TYPE_ID, SERVICE_INTERNAL_CODE = :SERVICE_INTERNAL_CODE, SERVICE_REMARKS = :SERVICE_REMARKS, SERVICE_OTP = :SERVICE_OTP, SERVICE_SQA = :SERVICE_SQA, SERVICE_REQF_MESSAGE = :SERVICE_REQF_MESSAGE, SERVICE_REPLY_CODE = :SERVICE_REPLY_CODE,OTP_IN_SAME_REQUEST=:OTP_IN_SAME_REQUEST,OTP_CUST_IN_REQUEST=:OTP_CUST_IN_REQUEST WHERE (SERVICE_ID = :SERVICE_ID)">
<DeleteParameters>
    <asp:Parameter Name="SERVICE_ID">
    </asp:Parameter>
</DeleteParameters>
<SelectParameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="SERVICE_TYPE_ID" ControlID="ddlServiceType"></asp:ControlParameter>
</SelectParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="SERVICE_ACCESS_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_SHORT_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_STATE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_TITLE"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="SERVICE_CREATION_DATE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_TYPE_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_INTERNAL_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REMARKS" ></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_OTP" ></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_SQA"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REQF_MESSAGE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REPLY_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="OTP_IN_SAME_REQUEST"></asp:Parameter>
<asp:Parameter Type="String" Name="OTP_CUST_IN_REQUEST"></asp:Parameter>
<asp:Parameter Name="SERVICE_ID" Type="String"></asp:Parameter>
</UpdateParameters>
<InsertParameters>        
<asp:Parameter Type="String" Name="SERVICE_ACCESS_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_SHORT_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_STATE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_TYPE_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_INTERNAL_CODE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REMARKS"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REQF_MESSAGE"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_REPLY_CODE"></asp:Parameter>
</InsertParameters>
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
        onrowdeleted="gdvServiceList_RowDeleted" 
        onrowupdated="gdvServiceList_RowUpdated">
    <Columns>
<asp:BoundField DataField="SERVICE_ID" Visible="False" SortExpression="SERVICE_ID" HeaderText="Service ID">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SERVICE_TYPE_ID" HeaderText="Service Type"><EditItemTemplate>
            <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsServiceType"
                DataTextField="SERVICE_TYPE_NAME" DataValueField="SERVICE_TYPE_ID" SelectedValue='<%# Bind("SERVICE_TYPE_ID") %>'>
            </asp:DropDownList>
        
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsServiceType"
                DataTextField="SERVICE_TYPE_NAME" DataValueField="SERVICE_TYPE_ID" Enabled="False" SelectedValue='<%# Bind("SERVICE_TYPE_ID") %>'>
            </asp:DropDownList>
        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SERVICE_TITLE" SortExpression="SERVICE_TITLE" HeaderText="Service Name">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_ACCESS_CODE" SortExpression="SERVICE_ACCESS_CODE" HeaderText="Access Code">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_SHORT_CODE" SortExpression="SERVICE_STATE" 
            HeaderText="Service Short Code">
<HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
        <asp:BoundField DataField="SERVICE_REPLY_CODE" HeaderText="Reply Short Code" 
            SortExpression="SERVICE_REPLY_CODE" />
<asp:BoundField DataField="SERVICE_INTERNAL_CODE" SortExpression="SERVICE_INTERNAL_CODE" HeaderText="Internal Code">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
        <asp:BoundField DataField="SERVICE_REQF_MESSAGE" HeaderText="Forward Message" 
            SortExpression="SERVICE_REQF_MESSAGE" />
<asp:TemplateField SortExpression="SERVICE_REMARKS" HeaderText="Remarks"><EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Height="49px" Text='<%# Bind("SERVICE_REMARKS") %>'
                TextMode="MultiLine" Width="213px"></asp:TextBox>
        
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SERVICE_REMARKS") %>'></asp:Label>
        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SERVICE_CREATION_DATE" SortExpression="SERVICE_CREATION_DATE" HeaderText="Creation Date">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField SortExpression="SERVICE_STATE" HeaderText="State"><EditItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("SERVICE_STATE") %>'>
<asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
    <asp:ListItem Value="L">Locked</asp:ListItem>
    <asp:ListItem Value="I">Inactive</asp:ListItem>
    </asp:DropDownList> 
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("SERVICE_STATE") %>' Enabled="False"><asp:ListItem Value="A">Active</asp:ListItem>
<asp:ListItem Value="L">Locked</asp:ListItem>
<asp:ListItem Value="I">Inactive</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField SortExpression="SERVICE_OTP" HeaderText="OTP">
<ItemTemplate>
<asp:DropDownList id="DropDownList7" runat="server" SelectedValue='<%# Bind("SERVICE_OTP") %>' Enabled="False">
<asp:ListItem Value="N">No</asp:ListItem>
<asp:ListItem Value="Y">Yes</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>
    <EditItemTemplate>
<asp:DropDownList id="DropDownList6" runat="server" SelectedValue='<%# Bind("SERVICE_OTP") %>'>
<asp:ListItem Selected="True" Value="N">No</asp:ListItem>
    <asp:ListItem Value="Y">Yes</asp:ListItem>  
    </asp:DropDownList> 
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:TemplateField>
        <asp:TemplateField HeaderText="Initiator OTP in  Request" 
            SortExpression="OTP_IN_SAME_REQUEST">
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList10" runat="server" 
                    DataTextField="OTP_IN_SAME_REQUEST" DataValueField="OTP_IN_SAME_REQUEST" 
                    SelectedValue='<%# Bind("OTP_IN_SAME_REQUEST") %>' Enabled="False">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList11" runat="server" 
                    DataTextField="OTP_IN_SAME_REQUEST" DataValueField="OTP_IN_SAME_REQUEST" 
                    SelectedValue='<%# Bind("OTP_IN_SAME_REQUEST") %>'>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer OTP in Request" 
            SortExpression="OTP_CUST_IN_REQUEST">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList13" runat="server" 
                    SelectedValue='<%# Bind("OTP_CUST_IN_REQUEST") %>'>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList12" runat="server" Enabled="False" 
                    SelectedValue='<%# Bind("OTP_CUST_IN_REQUEST") %>'>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
<asp:TemplateField SortExpression="SERVICE_SQA" HeaderText="SQA"><EditItemTemplate>
<asp:DropDownList id="DropDownList8" runat="server" SelectedValue='<%# Bind("SERVICE_SQA") %>'>
<asp:ListItem Selected="True" Value="N">No</asp:ListItem>
    <asp:ListItem Value="Y">Yes</asp:ListItem>  
    </asp:DropDownList> 
</EditItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList9" runat="server" SelectedValue='<%# Bind("SERVICE_SQA") %>' Enabled="False">
<asp:ListItem Value="N">No</asp:ListItem>
<asp:ListItem Value="Y">Yes</asp:ListItem>
</asp:DropDownList> 
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField DeleteText="" ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
</Columns>
    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView> </DIV>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New Service </SPAN></STRONG></DIV>
<DIV>
<asp:DetailsView id="dtvNewService" runat="server" DataSourceID="sdsService" 
    BorderColor="Silver" AutoGenerateRows="False" 
        DefaultMode="Insert" oniteminserting="dtvNewService_ItemInserting" 
        oniteminserted="dtvNewService_ItemInserted">
    <FieldHeaderStyle Font-Size="X-Small" />
    <Fields>
<asp:BoundField DataField="SERVICE_TITLE" SortExpression="SERVICE_TITLE" HeaderText="Service Name">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_ACCESS_CODE" SortExpression="SERVICE_ACCESS_CODE" HeaderText="Access Code">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_SHORT_CODE" SortExpression="SERVICE_SHORT_CODE" 
            HeaderText="Service Short Code">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
        <asp:BoundField DataField="SERVICE_REPLY_CODE" HeaderText="Reply Short Code" 
            SortExpression="SERVICE_REPLY_CODE">
            <HeaderStyle HorizontalAlign="Right" />
        </asp:BoundField>
<asp:BoundField DataField="SERVICE_INTERNAL_CODE" SortExpression="SERVICE_INTERNAL_CODE" HeaderText="Internal Code">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVICE_REQF_MESSAGE" SortExpression="SERVICE_REQF_MESSAGE" 
            HeaderText="Forward Message" >
    <HeaderStyle HorizontalAlign="Right" />
</asp:BoundField>
        <asp:TemplateField HeaderText="Remarks" SortExpression="SERVICE_REMARKS">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SERVICE_REMARKS") %>'></asp:TextBox>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Height="68px" 
                    Text='<%# Bind("SERVICE_REMARKS") %>' TextMode="MultiLine" Width="231px"></asp:TextBox>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SERVICE_REMARKS") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
        </asp:TemplateField>
<asp:TemplateField SortExpression="SERVICE_STATE" HeaderText="State"><EditItemTemplate>
    <asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SERVICE_STATE") %>'></asp:TextBox>    
</EditItemTemplate>
<InsertItemTemplate>
    <asp:DropDownList id="DropDownList1" runat="server" SelectedValue='<%# Bind("SERVICE_STATE") %>'><asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
    <asp:ListItem Value="L">Locked</asp:ListItem>
    <asp:ListItem Value="I">Inactive</asp:ListItem>
    </asp:DropDownList>
    
</InsertItemTemplate>

<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
<ItemTemplate>
    <asp:Label id="Label1" runat="server" Text='<%# Bind("SERVICE_STATE") %>'></asp:Label>
    
</ItemTemplate>
</asp:TemplateField>

<asp:CommandField InsertText=" Insert " ButtonType="Button" ShowInsertButton="True">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView> </DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
