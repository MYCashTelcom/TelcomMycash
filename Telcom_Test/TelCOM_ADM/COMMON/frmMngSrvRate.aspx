<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="frmMngSrvRate.aspx.cs" Inherits="Forms_frmMngPackage" Title="Manage Package" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="scm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">
    <asp:SqlDataSource id="sdsServiceRate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        InsertCommand="INSERT INTO SERVICE_RATE(SERVICE_ID, SERVICE_RATE_TITLE, RATE_CHARGE_TYPE, RATE_ON_NET, RATE_OFF_NET, RATE_INTERNATIONAL, DISCOUNT_ON_NET, DISCOUNT_OFF_NET, DISCOUNT_INTERNATIONAL, BALANCE_TO_DISCOUNT, DISCOUNT_ON_BALANCE, REWARD) VALUES (:SERVICE_ID, :SERVICE_RATE_TITLE, :RATE_CHARGE_TYPE, :RATE_ON_NET, :RATE_OFF_NET, :RATE_INTERNATIONAL, :DISCOUNT_ON_NET, :DISCOUNT_OFF_NET, :DISCOUNT_INTERNATIONAL, :BALANCE_TO_DISCOUNT, :DISCOUNT_ON_BALANCE, :REWARD)" 
        DeleteCommand="DELETE FROM SERVICE_RATE WHERE (SERVICE_RATE_ID = :SERVICE_RATE_ID)" UpdateCommand="UPDATE SERVICE_RATE SET SERVICE_ID = :SERVICE_ID, SERVICE_RATE_TITLE = :SERVICE_RATE_TITLE, RATE_CHARGE_TYPE = :RATE_CHARGE_TYPE, RATE_ON_NET = :RATE_ON_NET, RATE_OFF_NET = :RATE_OFF_NET, RATE_INTERNATIONAL = :RATE_INTERNATIONAL, DISCOUNT_ON_NET = :DISCOUNT_ON_NET, DISCOUNT_OFF_NET = :DISCOUNT_OFF_NET, DISCOUNT_INTERNATIONAL = :DISCOUNT_INTERNATIONAL, BALANCE_TO_DISCOUNT = :BALANCE_TO_DISCOUNT, DISCOUNT_ON_BALANCE = :DISCOUNT_ON_BALANCE, REWARD = :REWARD WHERE (SERVICE_RATE_ID = :SERVICE_RATE_ID)" 

        SelectCommand="SELECT SERVICE_ID, SERVICE_RATE_ID, SERVICE_RATE_TITLE, RATE_CHARGE_TYPE, RATE_ON_NET, RATE_OFF_NET, RATE_INTERNATIONAL, DISCOUNT_ON_NET, DISCOUNT_OFF_NET, DISCOUNT_INTERNATIONAL, BALANCE_TO_DISCOUNT, DISCOUNT_ON_BALANCE, REWARD FROM SERVICE_RATE WHERE (SERVICE_ID = :SERVICE_ID)"><DeleteParameters>
    <asp:Parameter Name="SERVICE_RATE_ID"></asp:Parameter>
    </DeleteParameters>
    <SelectParameters>
    <asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="SERVICE_ID" ControlID="ddlService"></asp:ControlParameter>
    </SelectParameters>
    <UpdateParameters>
    <asp:Parameter Type="String" Name="SERVICE_ID"></asp:Parameter>
    <asp:Parameter Type="String" Name="SERVICE_RATE_TITLE"></asp:Parameter>
    <asp:Parameter Type="String" Name="RATE_CHARGE_TYPE"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_ON_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_OFF_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_INTERNATIONAL"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_ON_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_OFF_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_INTERNATIONAL"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="BALANCE_TO_DISCOUNT"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_ON_BALANCE"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="REWARD"></asp:Parameter>
    <asp:Parameter Type="String" Name="SERVICE_RATE_ID"></asp:Parameter>
    </UpdateParameters>
    <InsertParameters>
    <asp:Parameter Type="String" Name="SERVICE_ID"></asp:Parameter>
    <asp:Parameter Type="String" Name="SERVICE_RATE_TITLE"></asp:Parameter>
    <asp:Parameter Type="String" Name="RATE_CHARGE_TYPE"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_ON_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_OFF_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="RATE_INTERNATIONAL"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_ON_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_OFF_NET"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_INTERNATIONAL"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="BALANCE_TO_DISCOUNT"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="DISCOUNT_ON_BALANCE"></asp:Parameter>
    <asp:Parameter Type="Decimal" Name="REWARD"></asp:Parameter>
    </InsertParameters>
    </asp:SqlDataSource> <asp:SqlDataSource id="sdsService" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT "SERVICE_ID", "SERVICE_TITLE" FROM "SERVICE_LIST"'>
    </asp:SqlDataSource>Manage Service Rate of Service <asp:DropDownList id="ddlService" runat="server" DataSourceID="sdsService" DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" __designer:wfdid="w13" AutoPostBack="True"></asp:DropDownList></SPAN></STRONG></DIV>
    <DIV> <asp:GridView id="grvPackage" runat="server" AutoGenerateColumns="False" 
            DataSourceID="sdsServiceRate" DataKeyNames="SERVICE_RATE_ID" AllowPaging="True" AllowSorting="True"
    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            onrowdeleted="grvPackage_RowDeleted" onrowupdated="grvPackage_RowUpdated">
    <Columns>
        <asp:TemplateField SortExpression="SERVICE_ID" HeaderText="Service"><EditItemTemplate>
    <asp:DropDownList id="DropDownList3" runat="server" DataSourceID="sdsService" SelectedValue='<%# Bind("SERVICE_ID") %>' DataValueField="SERVICE_ID" DataTextField="SERVICE_TITLE"></asp:DropDownList> 
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:DropDownList id="DropDownList2" runat="server" DataSourceID="sdsService" SelectedValue='<%# Bind("SERVICE_ID") %>' DataValueField="SERVICE_ID" DataTextField="SERVICE_TITLE" Enabled="False" Font-Bold="False"></asp:DropDownList> 
    </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField ReadOnly="True" DataField="SERVICE_RATE_ID" Visible="False" SortExpression="SERVICE_RATE_ID" HeaderText="Package ID">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="SERVICE_RATE_TITLE" SortExpression="SERVICE_RATE_TITLE" HeaderText="Rate Name">
    <ItemStyle Wrap="False"></ItemStyle>

    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" VerticalAlign="Bottom"></HeaderStyle>
    </asp:BoundField>
    <asp:TemplateField SortExpression="RATE_CHARGE_TYPE" HeaderText="Rate Type"><EditItemTemplate>
    <asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("RATE_CHARGE_TYPE") %>' __designer:wfdid="w9">
                    <asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                    <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                    <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                    <asp:ListItem Value="FRE">Free</asp:ListItem>
                </asp:DropDownList> 
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:DropDownList id="DropDownList1" runat="server" SelectedValue='<%# Bind("RATE_CHARGE_TYPE") %>' __designer:wfdid="w38" Enabled="False"><asp:ListItem Value="0">Pre Paid</asp:ListItem>
                    <asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                    <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                    <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                    <asp:ListItem Value="FRE">Free</asp:ListItem>
    </asp:DropDownList> 
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField SortExpression="RATE_ON_NET" HeaderText="On-Net Rate"><EditItemTemplate>
    <asp:TextBox id="TextBox1" runat="server" Width="50px" __designer:wfdid="w12" Text='<%# Bind("RATE_ON_NET") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label1" runat="server" __designer:wfdid="w11" Text='<%# Bind("RATE_ON_NET") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="RATE_OFF_NET" HeaderText="Off-Net Rate"><EditItemTemplate>
    <asp:TextBox id="TextBox2" runat="server" Width="50px" __designer:wfdid="w14" Text='<%# Bind("RATE_OFF_NET") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label2" runat="server" __designer:wfdid="w13" Text='<%# Bind("RATE_OFF_NET") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="RATE_INTERNATIONAL" HeaderText="Int-Net Rate"><EditItemTemplate>
    <asp:TextBox id="TextBox3" runat="server" Width="50px" __designer:wfdid="w17" Text='<%# Bind("RATE_INTERNATIONAL") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label3" runat="server" __designer:wfdid="w16" Text='<%# Bind("RATE_INTERNATIONAL") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
 
    <asp:TemplateField SortExpression="DISCOUNT_ON_NET" HeaderText="Discount On On-Net (%)"><EditItemTemplate>
    <asp:TextBox id="TextBox6" runat="server" Width="50px" __designer:wfdid="w23" Text='<%# Bind("DISCOUNT_ON_NET") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label6" runat="server" __designer:wfdid="w22" Text='<%# Bind("DISCOUNT_ON_NET") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="DISCOUNT_OFF_NET" HeaderText="Discount On Off-Net (%)"><EditItemTemplate>
    <asp:TextBox id="TextBox7" runat="server" Width="50px" __designer:wfdid="w25" Text='<%# Bind("DISCOUNT_OFF_NET") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label7" runat="server" __designer:wfdid="w24" Text='<%# Bind("DISCOUNT_OFF_NET") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="DISCOUNT_INTERNATIONAL" HeaderText="Discount On Int-Net (%)"><EditItemTemplate>
    <asp:TextBox id="TextBox8" runat="server" Width="50px" __designer:wfdid="w27" Text='<%# Bind("DISCOUNT_INTERNATIONAL") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label8" runat="server" __designer:wfdid="w26" Text='<%# Bind("DISCOUNT_INTERNATIONAL") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="DISCOUNT_ON_BALANCE" 
            HeaderText="Discount On Balance(%)"><EditItemTemplate>
    <asp:TextBox id="TextBox10" runat="server" Width="50px" __designer:wfdid="w31" Text='<%# Bind("DISCOUNT_ON_BALANCE") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label10" runat="server" __designer:wfdid="w30" Text='<%# Bind("DISCOUNT_ON_BALANCE") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField SortExpression="BALANCE_TO_DISCOUNT" 
            HeaderText="Min Balance to Get Discount"><EditItemTemplate>
    <asp:TextBox id="TextBox9" runat="server" Width="50px" __designer:wfdid="w29" Text='<%# Bind("BALANCE_TO_DISCOUNT") %>'></asp:TextBox>
    </EditItemTemplate>

    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom"></HeaderStyle>
    <ItemTemplate>
    <asp:Label id="Label9" runat="server" __designer:wfdid="w28" Text='<%# Bind("BALANCE_TO_DISCOUNT") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
        <asp:BoundField DataField="REWARD" HeaderText="Reward" 
            SortExpression="REWARD" />
    <asp:CommandField EditText=" Edit " ShowEditButton="True"></asp:CommandField>
    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
    </Columns>
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
    </asp:GridView></DIV>
    <DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">New Service Rate</SPAN></STRONG></DIV>
    <DIV><asp:DetailsView id="DetailsView1" runat="server" BorderColor="Silver" 
            DataSourceID="sdsServiceRate" DataKeyNames="SERVICE_RATE_ID" Height="50px" 
            Width="125px" AutoGenerateRows="False" DefaultMode="Insert" 
            oniteminserting="DetailsView1_ItemInserting" 
            oniteminserted="DetailsView1_ItemInserted">
        <FieldHeaderStyle Font-Size="X-Small" />
        <Fields>
    <asp:BoundField DataField="SERVICE_RATE_TITLE" SortExpression="SERVICE_RATE_TITLE" HeaderText="Rate Name">
    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:TemplateField SortExpression="RATE_CHARGE_TYPE" HeaderText="Charge Type"><EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("RATE_CHARGE_TYPE") %>'></asp:TextBox>
            
    </EditItemTemplate>
    <InsertItemTemplate>
                <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("RATE_CHARGE_TYPE") %>'>
                    <asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                    <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                    <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                    <asp:ListItem Value="FRE">Free</asp:ListItem>
                </asp:DropDownList>
            
    </InsertItemTemplate>

    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("RATE_CHARGE_TYPE") %>'></asp:Label>
            
    </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="RATE_ON_NET" SortExpression="RATE_ON_NET" HeaderText="On-Net Rate">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="RATE_OFF_NET" SortExpression="RATE_OFF_NET" HeaderText="Off-Net Rate">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="RATE_INTERNATIONAL" SortExpression="RATE_INTERNATIONAL" HeaderText="Int-Net Rate">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="DISCOUNT_ON_NET" SortExpression="DISCOUNT_ON_NET" HeaderText="Discount on On-Net (%)">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="DISCOUNT_OFF_NET" SortExpression="DISCOUNT_OFF_NET" HeaderText="Discount on Off-Net (%)">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="DISCOUNT_INTERNATIONAL" SortExpression="DISCOUNT_INTERNATIONAL" HeaderText="Discount on Int-Net (%)">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="DISCOUNT_ON_BALANCE" SortExpression="DISCOUNT_ON_BALANCE" 
                HeaderText="Discount on Balance(%)">
        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
    <asp:BoundField DataField="BALANCE_TO_DISCOUNT" SortExpression="BALANCE_TO_DISCOUNT" 
                HeaderText="Min Balance to Get Discount">
    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
    </asp:BoundField>
            <asp:BoundField DataField="REWARD" HeaderText="Reward" SortExpression="REWARD">
            <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
            </asp:BoundField>
    <asp:CommandField InsertText=" Insert " ButtonType="Button" ShowInsertButton="True">
    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

    <ItemStyle HorizontalAlign="Center"></ItemStyle>
    </asp:CommandField>
    </Fields>
    </asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>