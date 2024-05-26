<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngThana.aspx.cs" Inherits="COMMON_frmMngThana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Thana</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:SqlDataSource ID="sdsThanaList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE (&quot;DISTRICT_ID&quot;=:DISTRICT_ID) ORDER BY THANA_NAME"
        UpdateCommand="UPDATE MANAGE_THANA SET THANA_NAME=:THANA_NAME WHERE (THANA_ID = :THANA_ID)"
        
        InsertCommand="INSERT INTO &quot;MANAGE_THANA&quot; (&quot;THANA_NAME&quot;, &quot;DISTRICT_ID&quot;) VALUES (:THANA_NAME, :DISTRICT_ID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlDistrictList" Name="DISTRICT_ID" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="THANA_NAME" />
            <asp:Parameter Name="THANA_ID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="THANA_NAME" />
            <asp:ControlParameter ControlID="ddlDistrictList" Name="DISTRICT_ID" PropertyName="SelectedValue" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDistrictList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT DISTRICT_ID,DISTRICT_NAME FROM MANAGE_DISTRICT">
    </asp:SqlDataSource>
    <div style="background-color: royalblue">
        <strong><span style="color: white">District List:</span><asp:DropDownList ID="ddlDistrictList"
            runat="server" AutoPostBack="True" DataSourceID="sdsDistrictList" DataTextField="DISTRICT_NAME"
            DataValueField="DISTRICT_ID">
        </asp:DropDownList>
        </strong>
    </div>
    <div>
        <asp:GridView ID="gdvThanaList" runat="server"
           AutoGenerateColumns="False" BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" DataKeyNames="THANA_ID" DataSourceID="sdsThanaList"
            Height="30px" AllowPaging="True" Width="500px" 
            onrowupdated="gdvThanaList_RowUpdated">
            <Columns>
                <asp:BoundField DataField="THANA_ID" HeaderText="THANA_ID" ReadOnly="True" SortExpression="THANA_ID"
                    Visible="False" />
                <asp:TemplateField HeaderText="Thana Name" SortExpression="THANA_NAME">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Height="22px" Text='<%# Bind("THANA_NAME") %>'
                            Width="327px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("THANA_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
    </div>
    <div style="background-color: royalblue">
        <strong><span style="color: white">Insert Thana</span></strong></div>
     <asp:DetailsView ID="dtvThana" runat="server" AutoGenerateRows="False" DataKeyNames="THANA_ID"
        DataSourceID="sdsThanaList" DefaultMode="Insert" Height="50px" Width="500px"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
        BorderStyle="None" oniteminserted="dtvThana_ItemInserted">
        <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:BoundField DataField="THANA_ID" HeaderText="THANA_ID" ReadOnly="True" SortExpression="THANA_ID"
                Visible="False" />
            <asp:TemplateField HeaderText="Thana Name" SortExpression="THANA_NAME">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("THANA_NAME") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="22px" Text='<%# Bind("THANA_NAME") %>'
                        Width="300px"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("THANA_NAME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowInsertButton="True" ButtonType="Button" />
        </Fields>
        <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
    </form>
</body>
</html>
