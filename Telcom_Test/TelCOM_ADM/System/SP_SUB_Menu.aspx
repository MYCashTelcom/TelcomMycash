<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SP_SUB_Menu.aspx.cs" Inherits="System_SP_SUB_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>System Menu</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
      <asp:SqlDataSource ID="sdsSPSubMenu" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "SERVICE_PACKAGE_SUB_MENU" WHERE "SP_MENU_ID" = :SP_MENU_ID'
        InsertCommand='INSERT INTO "SERVICE_PACKAGE_SUB_MENU" ("SP_MENU_ID", "SP_MENU_TITLE", "SP_MENU_CODE", "SP_MENU_PARENT", "SP_DISPLAY_ORDER", "SERVICE_ID", "SERVICE_PKG_ID", "SP_MENU_PARENT_CODE","PARAMETER_ONE","PARAMETER_TWO","PARAMETER_THREE","PARAMETER_FOUR","PARAMETER_FIVE","SP_MENU_TYPE","PARAMETER_SIX", "PARAMETER_SEVEN", "PARAMETER_EIGHT", "CONFIRMATIONS_PARAMETER", "TYPE_PARAMETER") 
                        VALUES (:SP_MENU_ID, :SP_MENU_TITLE, :SP_MENU_CODE, :SP_MENU_PARENT, :SP_DISPLAY_ORDER, :SERVICE_ID, :SERVICE_PKG_ID, :SP_MENU_PARENT_CODE,:PARAMETER_ONE,:PARAMETER_TWO,:PARAMETER_THREE,:PARAMETER_FOUR,:PARAMETER_FIVE,:SP_MENU_TYPE,:PARAMETER_SIX, :PARAMETER_SEVEN, :PARAMETER_EIGHT, :CONFIRMATIONS_PARAMETER, :TYPE_PARAMETER )'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM SERVICE_PACKAGE_SUB_MENU WHERE (SERVICE_PKG_ID=:SERVICE_PKG_ID)  ORDER BY SERVICE_PKG_ID,SP_DISPLAY_ORDER ASC"
        UpdateCommand='UPDATE "SERVICE_PACKAGE_SUB_MENU" SET "SP_MENU_TITLE" = :SP_MENU_TITLE, "SP_MENU_PARENT" = :SP_MENU_PARENT, 
            "SP_MENU_CODE" = :SP_MENU_CODE, "SP_DISPLAY_ORDER" = :SP_DISPLAY_ORDER, "SERVICE_ID" = :SERVICE_ID, 
            "SP_MENU_PARENT_CODE" = :SP_MENU_PARENT_CODE ,"PARAMETER_ONE"=:PARAMETER_ONE,"PARAMETER_TWO"=:PARAMETER_TWO,
            "PARAMETER_THREE"=:PARAMETER_THREE,"PARAMETER_FOUR"=:PARAMETER_FOUR,"PARAMETER_FIVE"=:PARAMETER_FIVE,"SP_MENU_TYPE"=:SP_MENU_TYPE,
            "PARAMETER_SIX"=:PARAMETER_SIX, "PARAMETER_SEVEN" =: PARAMETER_SEVEN, "PARAMETER_EIGHT" =: PARAMETER_EIGHT, 
            "CONFIRMATIONS_PARAMETER"=:CONFIRMATIONS_PARAMETER, "TYPE_PARAMETER"=:TYPE_PARAMETER WHERE "SP_MENU_ID" = :SP_MENU_ID'>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlServicePackage" Type="String"  Name="SERVICE_PKG_ID" PropertyName="SelectedValue" />
        </SelectParameters>        
        <DeleteParameters>
            <asp:Parameter Name="SP_MENU_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="SP_MENU_TITLE" Type="String" />
            <asp:Parameter Name="SP_MENU_CODE" Type="String" />
            <asp:Parameter Name="SP_MENU_PARENT" Type="String" />
            <asp:Parameter Name="SP_DISPLAY_ORDER" Type="String" />
            <asp:Parameter Name="SERVICE_ID" Type="String" />
            <%--<asp:Parameter Name="SERVICE_PKG_ID" Type="String" />--%>
            <asp:Parameter Name="SP_MENU_PARENT_CODE" Type="String" />
            <asp:Parameter Name="PARAMETER_ONE" Type="String" />
            <asp:Parameter Name="PARAMETER_TWO" Type="String" />
            <asp:Parameter Name="PARAMETER_THREE" Type="String" />
            <asp:Parameter Name="PARAMETER_FOUR" Type="String" />
            <asp:Parameter Name="PARAMETER_FIVE" Type="String" />            
            <asp:Parameter Name="SP_MENU_TYPE" Type="String" />
            <asp:Parameter Name="PARAMETER_SIX" Type="String" />
            <asp:Parameter Name="PARAMETER_SEVEN" Type="String" />
            <asp:Parameter Name="PARAMETER_EIGHT" Type="String" />  
            <asp:Parameter Name="CONFIRMATIONS_PARAMETER" Type="String" />            
            <asp:Parameter Name="TYPE_PARAMETER" Type="String" /> 
                      
            <asp:Parameter Name="SP_MENU_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SP_MENU_ID" Type="String" />
            <asp:Parameter Name="SP_MENU_TITLE" Type="String" />
            <asp:Parameter Name="SP_MENU_CODE" Type="String" />
            <asp:Parameter Name="SP_MENU_PARENT" Type="String" />
            <asp:Parameter Name="SP_DISPLAY_ORDER" Type="String" />
            <asp:Parameter Name="SERVICE_ID" Type="String" />
            <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
            <asp:Parameter Name="SP_MENU_PARENT_CODE" Type="String" />
            <asp:Parameter Name="PARAMETER_ONE" Type="String" />
            <asp:Parameter Name="PARAMETER_TWO" Type="String" />
            <asp:Parameter Name="PARAMETER_THREE" Type="String" />
            <asp:Parameter Name="PARAMETER_FOUR" Type="String" />
            <asp:Parameter Name="PARAMETER_FIVE" Type="String" />
            <asp:Parameter Name="PARAMETER_SIX" Type="String" />
            <asp:Parameter Name="PARAMETER_SEVEN" Type="String" />
            <asp:Parameter Name="PARAMETER_EIGHT" Type="String" />
            <asp:Parameter Name="CONFIRMATIONS_PARAMETER" Type="String" />
            <asp:Parameter Name="TYPE_PARAMETER" Type="String" />
            
            <asp:Parameter Name="SP_MENU_TYPE" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBranch" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand='SELECT "CMP_BRANCH_ID", "CMP_BRANCH_NAME" FROM "CM_CMP_BRANCH"'>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsParentMenu" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT  SP_DISPLAY_ORDER||' '||SP_MENU_TITLE SP_MENU_TITLE,SP_MENU_ID  FROM SERVICE_PACKAGE_SUB_MENU WHERE (SERVICE_PKG_ID=:SERVICE_PKG_ID) ORDER BY SP_DISPLAY_ORDER">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlServicePackage" Type="String"  Name="SERVICE_PKG_ID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsServiceList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST WHERE SERVICE_STATE='A' ORDER BY SERVICE_TITLE ASC">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsServPkg" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_STATUS='A' ORDER BY SERVICE_PKG_NAME ASC ">
    </asp:SqlDataSource>
    <table>
    <tr>
        <td valign="top">
        <div style="BACKGROUND-COLOR: royalblue">
        <strong>
        <span style="COLOR: white">Service Package 
            <asp:DropDownList ID="ddlServicePackage" runat="server"  AutoPostBack="true"
                DataSourceID="sdsServPkg" DataTextField="SERVICE_PKG_NAME" 
                DataValueField="SERVICE_PKG_ID" >
            </asp:DropDownList>
        </span></strong></div>
        <div>
          <asp:GridView ID="grvSystemMenu" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="SP_MENU_ID" DataSourceID="sdsSPSubMenu" BorderColor="#E0E0E0" 
            AllowPaging="True" PageSize="15" CssClass="mGrid" PagerStyle-CssClass="pgr"  ForeColor="White"
            AlternatingRowStyle-CssClass="alt" onrowdeleted="grvSystemMenu_RowDeleted" 
            onrowupdated="grvSystemMenu_RowUpdated" >
            <Columns>
                 <asp:BoundField DataField="SP_MENU_ID" HeaderText="Menu ID" ReadOnly="True"
                    SortExpression="SP_MENU_ID" Visible="False" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="SP_DISPLAY_ORDER" HeaderText="Display Order" 
                    SortExpression="SP_DISPLAY_ORDER"  >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField> 
                <asp:BoundField DataField="SP_MENU_CODE" HeaderText="USSD Code" 
                    SortExpression="SP_MENU_CODE" />            
                <asp:BoundField DataField="SP_MENU_PARENT_CODE" HeaderText="Parent Code" 
                    SortExpression="SP_MENU_PARENT_CODE" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>                
                <asp:TemplateField HeaderText="Parent Menu" SortExpression="SP_MENU_PARENT">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsParentMenu" DataTextField="SP_MENU_TITLE" AppendDataBoundItems="true"
                            DataValueField="SP_MENU_ID" SelectedValue='<%# Bind("SP_MENU_PARENT") %>' Style="position: relative">
                            <asp:ListItem Value="" Text=""> </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true" DataSourceID="sdsSPSubMenu" DataTextField="SP_MENU_TITLE"
                            DataValueField="SP_MENU_ID" Enabled="False" SelectedValue='<%# Bind("SP_MENU_PARENT") %>' 
                            Style="position: relative">
                            <asp:ListItem Value="" Text=""> </asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Service List" SortExpression="SERVICE_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE"
                            DataValueField="SERVICE_ID" SelectedValue='<%# Bind("SERVICE_ID") %>' Style="position: relative">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE"
                            DataValueField="SERVICE_ID" Enabled="False" SelectedValue='<%# Bind("SERVICE_ID") %>'
                            Style="position: relative">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>                
                <asp:BoundField DataField="SP_MENU_TITLE" HeaderText="Menu Title" SortExpression="SP_MENU_TITLE" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>            
                <asp:TemplateField HeaderText="Menu Type" SortExpression="SP_MENU_TYPE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList6" runat="server" SelectedValue='<%# Bind("SP_MENU_TYPE") %>'
                            Style="position: relative">                                                       
                            <asp:ListItem Value="GROUP">Group</asp:ListItem>
                            <asp:ListItem Value="SRVS">Service</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList5" runat="server" Enabled="False" SelectedValue='<%# Bind("SP_MENU_TYPE") %>'
                            Style="position: relative">                            
                            <asp:ListItem Value="GROUP">Group</asp:ListItem>
                            <asp:ListItem Value="SRVS">Service</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                 <asp:BoundField DataField="PARAMETER_ONE" HeaderText="Parameter One" 
                    SortExpression="PARAMETER_ONE" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField> 
                 <asp:BoundField DataField="PARAMETER_TWO" HeaderText="Parameter Two" 
                    SortExpression="PARAMETER_TWO" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField> 
                  <asp:BoundField DataField="PARAMETER_THREE" HeaderText="Parameter Three" 
                    SortExpression="PARAMETER_THREE" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField> 
                  <asp:BoundField DataField="PARAMETER_FOUR" HeaderText="Parameter Four" 
                    SortExpression="PARAMETER_FOUR" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>     
                <asp:BoundField DataField="PARAMETER_FIVE" HeaderText="Parameter Five" 
                    SortExpression="PARAMETER_FIVE" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="PARAMETER_SIX" HeaderText="Parameter Six" 
                    SortExpression="PARAMETER_SIX" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                 
                 
                 <asp:BoundField DataField="PARAMETER_SEVEN" HeaderText="Parameter Seven" 
                    SortExpression="PARAMETER_SEVEN" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="PARAMETER_EIGHT" HeaderText="Parameter Eight" 
                    SortExpression="PARAMETER_EIGHT" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>            
                 
                 <asp:BoundField DataField="CONFIRMATIONS_PARAMETER" HeaderText="Conformation Parameter" 
                    SortExpression="CONFIRMATIONS_PARAMETER" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField> 
                
                <asp:BoundField DataField="TYPE_PARAMETER" HeaderText="Type Parameter" SortExpression="TYPE_PARAMETER"/>
                     
                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />--%>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" HeaderText="Actions" />
            </Columns>
            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" NextPageText="Next"
                PageButtonCount="7" PreviousPageText="Previous" />
            <FooterStyle BackColor="#FFC0C0" />
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
         </asp:GridView>
        </div>
        </td>
       </tr>
       <tr>
         <td>
             <div style="BACKGROUND-COLOR: royalblue"><strong>
                <span style="COLOR: white">New Service Package Menu</span></strong>
            </div>
         <asp:DetailsView ID="dtvSystemMenu" runat="server" Height="50px"  
            AutoGenerateRows="False" BorderColor="#E0E0E0" DataKeyNames="SP_MENU_ID" 
            DataSourceID="sdsSPSubMenu" DefaultMode="Insert" 
            oniteminserted="dtvSystemMenu_ItemInserted" 
            oniteminserting="dtvSystemMenu_ItemInserting">
            <Fields>
                <asp:TemplateField HeaderText="SP_MENU_ID" SortExpression="SP_MENU_ID" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SP_MENU_ID") %>'></asp:Label>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SP_MENU_ID") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SP_MENU_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="SP_DISPLAY_ORDER" HeaderText="Display Order" 
                 SortExpression="SP_DISPLAY_ORDER" >
                 <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SP_MENU_CODE" HeaderText="USSD Code" 
                    SortExpression="SP_MENU_CODE">
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField>                
                <asp:TemplateField HeaderText="Parent Menu" SortExpression="SP_MENU_PARENT">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList9" runat="server" DataSourceID="sdsParentMenu" AppendDataBoundItems="true"
                            DataTextField="SP_MENU_TITLE" DataValueField="SP_MENU_ID" SelectedValue='<%# Bind("SP_MENU_PARENT") %>'
                            Style="position: relative; left: 0px;">
                            <asp:ListItem Value="" Text=""> </asp:ListItem>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>                
                 <asp:TemplateField HeaderText="Service List" SortExpression="SERVICE_ID">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList112" runat="server" DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE"
                            DataValueField="SERVICE_ID" SelectedValue='<%# Bind("SERVICE_ID") %>' Style="position: relative">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="SP_MENU_TITLE" HeaderText="Menu Title" SortExpression="SP_MENU_TITLE" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField> 
                 <asp:TemplateField HeaderText="Menu Type" SortExpression="SP_MENU_TYPE">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList10" runat="server" SelectedValue='<%# Bind("SP_MENU_TYPE") %>'
                            Style="position: relative">                            
                           <asp:ListItem Value="GROUP">Group</asp:ListItem>
                            <asp:ListItem Value="SRVS">Service</asp:ListItem>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:TemplateField>               
                <asp:BoundField DataField="PARAMETER_ONE" HeaderText="Parameter One" 
                    SortExpression="PARAMETER_ONE" >
                     <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField> 
                 <asp:BoundField DataField="PARAMETER_TWO" HeaderText="Parameter Two" 
                    SortExpression="PARAMETER_TWO" >
                     <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField> 
                  <asp:BoundField DataField="PARAMETER_THREE" HeaderText="Parameter Three" 
                    SortExpression="PARAMETER_THREE" >
                     <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField> 
                  <asp:BoundField DataField="PARAMETER_FOUR" HeaderText="Parameter Four" 
                    SortExpression="PARAMETER_FOUR" >
                     <HeaderStyle HorizontalAlign="Right" />
                </asp:BoundField> 
                 <asp:BoundField DataField="PARAMETER_FIVE" HeaderText="Parameter Five" 
                    SortExpression="PARAMETER_FIVE" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>   
                <asp:BoundField DataField="PARAMETER_SIX" HeaderText="Parameter Six" 
                    SortExpression="PARAMETER_SIX" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                
                
                 
                 <asp:BoundField DataField="PARAMETER_SEVEN" HeaderText="Parameter Seven" 
                    SortExpression="PARAMETER_SEVEN">
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    </asp:BoundField>
                    
                 <asp:BoundField DataField="PARAMETER_EIGHT" HeaderText="Parameter Eight" 
                    SortExpression="PARAMETER_EIGHT">
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    </asp:BoundField>
                    
                    
                    
                 
                 <asp:BoundField DataField="CONFIRMATIONS_PARAMETER" HeaderText="Conformation Parameter" 
                    SortExpression="CONFIRMATIONS_PARAMETER" >
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:CommandField ShowInsertButton="True" ButtonType="Button" >
                    <FooterStyle BackColor="#8080FF" BorderColor="Blue" BorderStyle="Solid" ForeColor="#FFC0C0" />
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                </asp:CommandField>
            </Fields>           
         </asp:DetailsView>
         </td>
       </tr>
    </table>
    </contenttemplate>
   </asp:UpdatePanel>
  </form>
</body>
</html>
