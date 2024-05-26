<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmMngCilentList.aspx.cs" Inherits="Forms_frmMngCilentList" Title="Manage Client Register" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Button ID="btnClinetList" runat="server" OnClick="btnClinetList_Click" 
        Text="Channel List" BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False" ForeColor="Black" />
        <asp:Button
            ID="btnNewClient" runat="server" OnClick="btnNewClient_Click" 
        Text="New Channel" BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False" ForeColor="Black" />
        <asp:Button
            ID="btnExport" runat="server" OnClick="btnExport_Click" 
        Text="   Export   " BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False" ForeColor="Black" Visible="False" />
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<div>
    <asp:SqlDataSource id="sdsClientList" runat="server" 
        SelectCommand='SELECT * FROM "CLIENT_LIST"' 
        UpdateCommand='UPDATE CLIENT_LIST SET CLINT_NAME = :CLINT_NAME, CLINT_PASS = :CLINT_PASS, CLINT_ADDRESS1 = :CLINT_ADDRESS1, CLINT_ADDRESS2 = :CLINT_ADDRESS2, CLINT_TOWN = :CLINT_TOWN, CLINT_POSTCODE = :CLINT_POSTCODE, CLINT_CITY = :CLINT_CITY, CLINT_CONTACT_F_NAME = :CLINT_CONTACT_F_NAME, CLINT_CONTACT_M_NAME = :CLINT_CONTACT_M_NAME, CLINT_CONTACT_L_NAME = :CLINT_CONTACT_L_NAME, CLINT_JOB_TITLE = :CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL = :CLINT_CONTACT_EMAIL, CLINT_LAND_LINE = :CLINT_LAND_LINE, CLINT_MOBILE = :CLINT_MOBILE, CLINT_FAX = :CLINT_FAX, CREATION_DATE = :CREATION_DATE, CLINT_M_NAME = :CLINT_M_NAME, CLINT_L_NAME = :CLINT_L_NAME, CLINT_CONTACT_TITLE = :CLINT_CONTACT_TITLE, CLINT_N_ID = :CLINT_N_ID, CLINT_PASSPORT_NO = :CLINT_PASSPORT_NO, CLI_ZONE_ID = :CLI_ZONE_ID, CLINET_RSP_CODE = :CLINET_RSP_CODE, CLIENT_RSP_NAME = :CLIENT_RSP_NAME, DISTRIBUTOR_NAME = :DISTRIBUTOR_NAME, DISTRIBUTOR_CODE = :DISTRIBUTOR_CODE, DISTRIBUTOR_ZONE_ID = :DISTRIBUTOR_ZONE_ID, OWNER_NAME = :OWNER_NAME, OWNER_MOBILE = :OWNER_MOBILE, OWNER_NID = :OWNER_NID WHERE (CLINT_ID = :CLINT_ID)' 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        InsertCommand='INSERT INTO CLIENT_LIST(CLINT_ID, CLINT_NAME, CLINT_PASS, CLINT_ADDRESS1, CLINT_ADDRESS2, CLINT_TOWN, CLINT_POSTCODE, CLINT_CITY, CLINT_CONTACT_F_NAME, CLINT_CONTACT_M_NAME, CLINT_CONTACT_L_NAME, CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL, CLINT_LAND_LINE, CLINT_MOBILE, CLINT_FAX, CREATION_DATE, CLINT_M_NAME, CLINT_L_NAME, CLINT_CONTACT_TITLE, CLINT_N_ID, CLINT_PASSPORT_NO, CLI_ZONE_ID, CLINET_RSP_CODE, CLIENT_RSP_NAME, DISTRIBUTOR_NAME, DISTRIBUTOR_CODE, DISTRIBUTOR_ZONE_ID, OWNER_NAME, OWNER_MOBILE, OWNER_NID) VALUES (:CLINT_ID, :CLINT_NAME, :CLINT_PASS, :CLINT_ADDRESS1, :CLINT_ADDRESS2, :CLINT_TOWN, :CLINT_POSTCODE, :CLINT_CITY, :CLINT_CONTACT_F_NAME, :CLINT_CONTACT_M_NAME, :CLINT_CONTACT_L_NAME, :CLINT_JOB_TITLE, :CLINT_CONTACT_EMAIL, :CLINT_LAND_LINE, :CLINT_MOBILE, :CLINT_FAX, :CREATION_DATE, :CLINT_M_NAME, :CLINT_L_NAME, :CLINT_CONTACT_TITLE, :CLINT_N_ID, :CLINT_PASSPORT_NO, :CLI_ZONE_ID, :CLINET_RSP_CODE, :CLIENT_RSP_NAME, :DISTRIBUTOR_NAME, :DISTRIBUTOR_CODE, :DISTRIBUTOR_ZONE_ID, :OWNER_NAME, :OWNER_MOBILE, :OWNER_NID)' 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        DeleteCommand='DELETE FROM "CLIENT_LIST" WHERE "CLINT_ID" = :CLINT_ID'><DeleteParameters>
<asp:Parameter Type="String" Name="CLINT_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="CLINT_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_PASS"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_ADDRESS1"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_ADDRESS2"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_TOWN"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_POSTCODE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CITY"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_F_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_M_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_L_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_JOB_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_EMAIL"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_LAND_LINE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_MOBILE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_FAX"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="CREATION_DATE"></asp:Parameter>
    <asp:Parameter Name="CLINT_M_NAME" />
    <asp:Parameter Name="CLINT_L_NAME" />
    <asp:Parameter Name="CLINT_CONTACT_TITLE" />
    <asp:Parameter Name="CLINT_N_ID" />
    <asp:Parameter Name="CLINT_PASSPORT_NO" />
<asp:Parameter Name="CLI_ZONE_ID"></asp:Parameter>
    <asp:Parameter Name="CLINET_RSP_CODE" />
    <asp:Parameter Name="CLIENT_RSP_NAME" />
    <asp:Parameter Name="DISTRIBUTOR_NAME" />
    <asp:Parameter Name="DISTRIBUTOR_CODE" />
    <asp:Parameter Name="DISTRIBUTOR_ZONE_ID" />
    <asp:Parameter Name="OWNER_NAME" />
    <asp:Parameter Name="OWNER_MOBILE" />
    <asp:Parameter Name="OWNER_NID" />
    <asp:Parameter Name="CLINT_ID" Type="String" />
</UpdateParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="CLINT_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_PASS"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_ADDRESS1"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_ADDRESS2"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_TOWN"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_POSTCODE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CITY"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_F_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_M_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_L_NAME"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_JOB_TITLE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_CONTACT_EMAIL"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_LAND_LINE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_MOBILE"></asp:Parameter>
<asp:Parameter Type="String" Name="CLINT_FAX"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="CREATION_DATE"></asp:Parameter>
    <asp:Parameter Name="CLINT_M_NAME" />
    <asp:Parameter Name="CLINT_L_NAME" />
    <asp:Parameter Name="CLINT_CONTACT_TITLE" />
    <asp:Parameter Name="CLINT_N_ID" />
    <asp:Parameter Name="CLINT_PASSPORT_NO" />
    <asp:Parameter Name="CLI_ZONE_ID" />
    <asp:Parameter Name="CLINET_RSP_CODE" />
    <asp:Parameter Name="CLIENT_RSP_NAME" />
    <asp:Parameter Name="DISTRIBUTOR_NAME" />
    <asp:Parameter Name="DISTRIBUTOR_CODE" />
    <asp:Parameter Name="DISTRIBUTOR_ZONE_ID" />
    <asp:Parameter Name="OWNER_NAME" />
    <asp:Parameter Name="OWNER_MOBILE" />
    <asp:Parameter Name="OWNER_NID" />
</InsertParameters>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsCountryList" runat="server" SelectCommand='SELECT "COUNTRY_ID", "COUNTRY_NMAE" FROM "COUNTRY_LIST"' ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsClientZone" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM CLIENT_ZONE C WHERE C.CLI_ZONE_TYPE='ARE'">
    </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsDistriZone" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM CLIENT_ZONE C WHERE C.CLI_ZONE_TYPE='ZON'">
    </asp:SqlDataSource>
        </div>
    <div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
    <div style="BACKGROUND-COLOR: royalblue; text-align: right;">
        <strong><span style="COLOR: white; font-size: 11px;">:: Manage Client List ::</span></strong></div>    
        <div id="div1" style="overflow:scroll;height:430px;">
        <asp:GridView id="GridView1" runat="server" DataSourceID="sdsClientList" 
                    DataKeyNames="CLINT_ID" BorderColor="White" AutoGenerateColumns="False" 
                    AllowSorting="True" PageSize="5"
    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                onrowdeleted="GridView1_RowDeleted" onrowupdated="GridView1_RowUpdated"><Columns>
<asp:BoundField ReadOnly="True" DataField="CLINT_ID" SortExpression="CLINT_ID" HeaderText="CID"></asp:BoundField>
<asp:BoundField DataField="CLINT_NAME" SortExpression="CLINT_NAME" HeaderText="POS Name">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
    <asp:BoundField DataField="CLINT_M_NAME" HeaderText="ClM Name" 
                SortExpression="CLINT_M_NAME" Visible="False" />
    <asp:BoundField DataField="CLINT_L_NAME" HeaderText="ClL Name" 
                SortExpression="CLINT_L_NAME" Visible="False" />
    <asp:BoundField DataField="CLINT_N_ID" HeaderText="POS Code" 
                SortExpression="CLINT_N_ID" />
    <asp:BoundField DataField="CLINT_PASSPORT_NO" HeaderText="Passport No" 
                SortExpression="CLINT_PASSPORT_NO" Visible="False" />
<asp:BoundField DataField="CLINT_PASS" Visible="False" SortExpression="CLINT_PASS" HeaderText="CLINT_PASS"></asp:BoundField>
<asp:BoundField DataField="CLINT_ADDRESS1" SortExpression="CLINT_ADDRESS1" HeaderText="Address1">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_ADDRESS2" SortExpression="CLINT_ADDRESS2" HeaderText="Address2"></asp:BoundField>
<asp:BoundField DataField="CLINT_TOWN" Visible="False" SortExpression="CLINT_TOWN" HeaderText="Town">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_POSTCODE" SortExpression="CLINT_POSTCODE" 
                HeaderText="Post Code"></asp:BoundField>
<asp:BoundField DataField="CLINT_CITY" SortExpression="CLINT_CITY" HeaderText="City">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
            <asp:TemplateField HeaderText="Area" SortExpression="CLI_ZONE_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList14" runat="server" 
                        DataSourceID="sdsClientZone" DataTextField="CLI_ZONE_TITLE" 
                        DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("CLI_ZONE_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList13" runat="server" 
                        DataSourceID="sdsClientZone" DataTextField="CLI_ZONE_TITLE" 
                        DataValueField="CLI_ZONE_ID"  
                        Enabled="False">
                    </asp:DropDownList><%--SelectedValue='<%# Bind("CLI_ZONE_ID") %>'--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CLINET_RSP_CODE" HeaderText="RSP Code" 
                SortExpression="CLINET_RSP_CODE" />
            <asp:BoundField DataField="CLIENT_RSP_NAME" HeaderText="RSP Name" 
                SortExpression="CLIENT_RSP_NAME" />
            <asp:BoundField DataField="DISTRIBUTOR_CODE" HeaderText="Distributor Code" 
                SortExpression="DISTRIBUTOR_CODE" />
            <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Distributor Name" 
                SortExpression="DISTRIBUTOR_NAME" />
            <asp:TemplateField HeaderText="Zone" SortExpression="DISTRIBUTOR_ZONE_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList23" runat="server" 
                        DataSourceID="sdsDistriZone" DataTextField="CLI_ZONE_TITLE" 
                        DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList24" runat="server" 
                        DataSourceID="sdsDistriZone" DataTextField="CLI_ZONE_TITLE" 
                        DataValueField="CLI_ZONE_ID" Enabled="False"  
                        ><%--SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'--%>
                    </asp:DropDownList>
                </ItemTemplate>
                
            
            </asp:TemplateField>
            <asp:BoundField DataField="OWNER_NAME" HeaderText="Owner Name" 
                SortExpression="OWNER_NAME" />
            <asp:BoundField DataField="OWNER_MOBILE" HeaderText="Owner Mobile" 
                SortExpression="OWNER_MOBILE" />
            <asp:BoundField DataField="OWNER_NID" HeaderText="Natinal ID" 
                SortExpression="OWNER_NID" />
<asp:BoundField DataField="CLINT_CONTACT_F_NAME" SortExpression="CLINT_CONTACT_F_NAME" 
                HeaderText="Contact Person">
                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CLINT_CONTACT_M_NAME" HeaderText="CM Name" 
                SortExpression="CLINT_CONTACT_M_NAME" Visible="False" />
<asp:BoundField DataField="CLINT_CONTACT_L_NAME" SortExpression="CLINT_CONTACT_L_NAME" 
                HeaderText="CL Name" Visible="False"></asp:BoundField>
<asp:BoundField DataField="CLINT_JOB_TITLE" Visible="False" SortExpression="CLINT_JOB_TITLE" HeaderText="CLINT_JOB_TITLE"></asp:BoundField>
<asp:BoundField DataField="CLINT_CONTACT_EMAIL" SortExpression="CLINT_CONTACT_EMAIL" HeaderText="Email">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_LAND_LINE" SortExpression="CLINT_LAND_LINE" 
                HeaderText="Land Line" Visible="False">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_MOBILE" SortExpression="CLINT_MOBILE" HeaderText="Mobile">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_FAX" SortExpression="CLINT_FAX" HeaderText="Fax" 
                Visible="False">
<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CREATION_DATE" Visible="False" SortExpression="CREATION_DATE" HeaderText="CREATION_DATE"></asp:BoundField>
<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
</Columns>
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
</asp:GridView> 
        </div>
        </asp:View>
            <asp:View ID="View2" runat="server">
            <div style="BACKGROUND-COLOR: royalblue; text-align: right;">
                <strong><span style="COLOR: white; font-size: 11px;">&nbsp;:: Add New&nbsp;Client&nbsp;::</span></strong></div>
            <asp:DetailsView id="dtvNewClient" runat="server" DataSourceID="sdsClientList" 
                    DataKeyNames="CLINT_ID" BorderColor="#E0E0E0" DefaultMode="Insert" 
                    AutoGenerateRows="False" Width="125px" Height="50px" 
                    oniteminserted="dtvNewClient_ItemInserted">
                <FieldHeaderStyle Font-Size="X-Small" />
            <Fields>
<asp:BoundField ReadOnly="True" DataField="CLINT_ID" Visible="False" SortExpression="CLINT_ID" HeaderText="CLINT_ID"></asp:BoundField>
    <asp:TemplateField HeaderText="POS Name" SortExpression="CLINT_NAME">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                </EditItemTemplate>
                
        <InsertItemTemplate>
            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
            &#160;**
                </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label5" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:Label>
                </ItemTemplate>
        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
        <ItemStyle Wrap="False" />
    </asp:TemplateField>
                <asp:TemplateField HeaderText="POS Code" SortExpression="CLINT_N_ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("CLINT_N_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("CLINT_N_ID") %>'></asp:TextBox>
                        &#160;**
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("CLINT_N_ID") %>'></asp:Label>
                    </ItemTemplate>
                
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Client Middle Name" 
                    SortExpression="CLINT_M_NAME" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:TextBox>
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:Label>
                    </ItemTemplate>
                
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
    <asp:BoundField DataField="CLINT_L_NAME" HeaderText="Client Last Name" 
                    SortExpression="CLINT_L_NAME" Visible="False"></asp:BoundField>
    <asp:BoundField DataField="CLINT_PASSPORT_NO" HeaderText="Passport No" 
                    SortExpression="CLINT_PASSPORT_NO" Visible="False">
        <HeaderStyle HorizontalAlign="Right" />
    </asp:BoundField>
<asp:TemplateField SortExpression="CLINT_PASS" Visible="False" HeaderText="CLINT_PASS">
    <EditItemTemplate>

<asp:TextBox runat="server" Text='<%# Bind("CLINT_GENDER") %>' id="TextBox4"></asp:TextBox>
                </EditItemTemplate>
<InsertItemTemplate>

    <asp:DropDownList ID="DropDownList21" runat="server" 
        SelectedValue='<%# Bind("CLINT_GENDER") %>'>
        <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
        <asp:ListItem Value="F">Female</asp:ListItem>
        <asp:ListItem Value="O">Other</asp:ListItem>
    </asp:DropDownList>
                </InsertItemTemplate>
<ItemTemplate>

    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CLINT_GENDER") %>'></asp:TextBox>
                </ItemTemplate>
    </asp:TemplateField>
                <asp:TemplateField HeaderText="Address1" SortExpression="CLINT_ADDRESS1">
                <EditItemTemplate>
                        <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:TextBox>
                    </EditItemTemplate>                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox37" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:TextBox>
                    </InsertItemTemplate>                
                    <ItemTemplate>
                        <asp:Label ID="Labe39" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:Label>
                    </ItemTemplate>                
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address2" SortExpression="CLINT_ADDRESS2">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:TextBox>
                    </EditItemTemplate>                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:TextBox>
                    </InsertItemTemplate>                
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:Label>
                    </ItemTemplate>
                
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CLINT_TOWN" SortExpression="CLINT_TOWN" 
                    Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:TextBox>
                    </EditItemTemplate>
                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:TextBox>
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:Label>
                    </ItemTemplate>
                
                
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:TextBox>
                    </EditItemTemplate>
                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:TextBox>
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:Label>
                    </ItemTemplate>
                
                </asp:TemplateField>
<asp:BoundField DataField="CLINT_POSTCODE" SortExpression="CLINT_POSTCODE" 
                    HeaderText="Post Code">
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField>
<asp:TemplateField SortExpression="CLINT_CITY" HeaderText="City">
    <EditItemTemplate>

<asp:TextBox runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>' id="TextBox10"></asp:TextBox>
                </EditItemTemplate>
<InsertItemTemplate>

<asp:TextBox runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>' id="TextBox7"></asp:TextBox>
                </InsertItemTemplate>

    <ItemTemplate>
        <asp:Label ID="Label9" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:Label>
                </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_CITY") %>'></asp:TextBox>
                </EditItemTemplate>
    <InsertItemTemplate>
        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_CITY") %>'></asp:TextBox>
                </InsertItemTemplate>
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                    <ItemTemplate>
<asp:Label runat="server" Text='<%# Bind("CLINT_CITY") %>' id="Label2"></asp:Label>

                </ItemTemplate>
                </asp:TemplateField>
<asp:TemplateField SortExpression="CLI_ZONE_ID" HeaderText="Area">
                    <EditItemTemplate>

<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("CLI_ZONE_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                    <InsertItemTemplate>

<asp:DropDownList id="DropDownList16" runat="server" DataSourceID="sdsClientZone" 
                            SelectedValue='<%# Bind("CLI_ZONE_ID") %>' DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID"></asp:DropDownList>
                </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList15" runat="server" 
                            DataSourceID="sdsClientZone" DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("CLI_ZONE_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RSP Code" SortExpression="CLINET_RSP_CODE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" 
                            Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RSP Name" SortExpression="CLIENT_RSP_NAME">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox13" runat="server" 
                            Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" 
                            Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Distributor Code" 
                    SortExpression="DISTRIBUTOR_CODE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:TextBox>
                        &#160;**
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Distributor Name" 
                    SortExpression="DISTRIBUTOR_NAME">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zone" 
                    SortExpression="DISTRIBUTOR_ZONE_ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList22" runat="server" 
                            DataSourceID="sdsDistriZone" DataTextField="CLI_ZONE_TITLE" 
                            DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" 
                            Text='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Owner Name" SortExpression="OWNER_NAME">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox17" runat="server" 
                            Text='<%# Bind("OWNER_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("OWNER_NAME") %>'></asp:TextBox>
                        &#160;**
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" 
                            Text='<%# Bind("OWNER_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Owner Mobile" SortExpression="OWNER_MOBILE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Owner National ID" SortExpression="OWNER_NID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Person" 
                    SortExpression="CLINT_CONTACT_F_NAME">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" 
                            Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" 
                            Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" 
                            Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="CLINT_CONTACT_M_NAME" 
                    HeaderText="Contact Middle Name" SortExpression="CLINT_CONTACT_M_NAME" 
                    Visible="False">
                
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
    <asp:BoundField DataField="CLINT_CONTACT_L_NAME" HeaderText="Contact Last Name" 
                    SortExpression="CLINT_CONTACT_L_NAME" Visible="False">
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CLINT_JOB_TITLE" HeaderText="CLINT_JOB_TITLE" 
                    SortExpression="CLINT_JOB_TITLE" Visible="False" />
<asp:BoundField DataField="CLINT_CONTACT_EMAIL" SortExpression="CLINT_CONTACT_EMAIL" HeaderText="Contact Email">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLINT_LAND_LINE" SortExpression="CLINT_LAND_LINE" 
                    HeaderText="Land Line" Visible="False">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
                <asp:TemplateField HeaderText="Mobile" SortExpression="CLINT_MOBILE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:TextBox>
                    </EditItemTemplate>
                
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:TextBox>                        
                    </InsertItemTemplate>
                
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:Label>
                    </ItemTemplate>
                
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
<asp:BoundField DataField="CLINT_FAX" SortExpression="CLINT_FAX" HeaderText="Fax" 
                    Visible="False">
<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CREATION_DATE" Visible="False" SortExpression="CREATION_DATE" HeaderText="CREATION_DATE"></asp:BoundField>
<asp:CommandField InsertText="Add Client" ButtonType="Button" ShowInsertButton="True">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
</Fields>
</asp:DetailsView>
            </asp:View>
        </asp:MultiView>&nbsp;</div>
</contenttemplate>
    </asp:UpdatePanel>
        &nbsp;&nbsp;
    </form>
</body>
</html>


