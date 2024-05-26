<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngThanaInfo.aspx.cs" Inherits="Forms_Default" Title="Manage Thana" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Thana</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
       <div style="BACKGROUND-COLOR: royalblue"><strong><span style="COLOR: white">
       Manage Thana </span></strong>
       </div>
     <div>
         <asp:SqlDataSource ID="sdsDistrict" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT &quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot; FROM &quot;MANAGE_DISTRICT&quot;"></asp:SqlDataSource>
         <asp:SqlDataSource ID="sdsThana" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             DeleteCommand="DELETE FROM &quot;MANAGE_THANA&quot; WHERE &quot;THANA_ID&quot; = :THANA_ID" 
             InsertCommand="INSERT INTO &quot;MANAGE_THANA&quot; (&quot;THANA_ID&quot;, &quot;THANA_NAME&quot;, &quot;DISTRICT_ID&quot;) VALUES (:THANA_ID, :THANA_NAME, :DISTRICT_ID)" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT * FROM &quot;MANAGE_THANA&quot;" 
             UpdateCommand="UPDATE &quot;MANAGE_THANA&quot; SET &quot;THANA_NAME&quot; = :THANA_NAME, &quot;DISTRICT_ID&quot; = :DISTRICT_ID WHERE &quot;THANA_ID&quot; = :THANA_ID">
             <DeleteParameters>
                 <asp:Parameter Name="THANA_ID" Type="String" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="THANA_NAME" Type="String" />
                 <asp:Parameter Name="DISTRICT_ID" Type="String" />
                 <asp:Parameter Name="THANA_ID" Type="String" />
             </UpdateParameters>
             <InsertParameters>
                 <asp:Parameter Name="THANA_ID" Type="String" />
                 <asp:Parameter Name="THANA_NAME" Type="String" />
                 <asp:Parameter Name="DISTRICT_ID" Type="String" />
             </InsertParameters>
         </asp:SqlDataSource>
         <asp:SqlDataSource ID="sdsMngDist" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             DeleteCommand="DELETE FROM &quot;MANAGE_DISTRICT&quot; WHERE &quot;DISTRICT_ID&quot; = :DISTRICT_ID" 
             InsertCommand="INSERT INTO &quot;MANAGE_DISTRICT&quot; (&quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot;, &quot;DISTRICT_CODE&quot;, &quot;POSTAL_CODE&quot;) VALUES (:DISTRICT_ID, :DISTRICT_NAME, :DISTRICT_CODE, :POSTAL_CODE)" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT * FROM &quot;MANAGE_DISTRICT&quot;" 
             UpdateCommand="UPDATE &quot;MANAGE_DISTRICT&quot; SET &quot;DISTRICT_NAME&quot; = :DISTRICT_NAME, &quot;DISTRICT_CODE&quot; = :DISTRICT_CODE, &quot;POSTAL_CODE&quot; = :POSTAL_CODE WHERE &quot;DISTRICT_ID&quot; = :DISTRICT_ID">
             <DeleteParameters>
                 <asp:Parameter Name="DISTRICT_ID" Type="String" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="DISTRICT_NAME" Type="String" />
                 <asp:Parameter Name="DISTRICT_CODE" Type="String" />
                 <asp:Parameter Name="POSTAL_CODE" Type="String" />
                 <asp:Parameter Name="DISTRICT_ID" Type="String" />
             </UpdateParameters>
             <InsertParameters>
                 <asp:Parameter Name="DISTRICT_ID" Type="String" />
                 <asp:Parameter Name="DISTRICT_NAME" Type="String" />
                 <asp:Parameter Name="DISTRICT_CODE" Type="String" />
                 <asp:Parameter Name="POSTAL_CODE" Type="String" />
             </InsertParameters>
         </asp:SqlDataSource>
         </div>
         <div>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" PageSize="15" 
                 DataKeyNames="THANA_ID" DataSourceID="sdsThana"  BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                Width="800px"                    AlternatingRowStyle-CssClass="alt" >
                 <Columns>
                     <asp:BoundField DataField="THANA_ID" HeaderText="THANA_ID" ReadOnly="True" 
                         SortExpression="THANA_ID" Visible="False" />
                     <asp:TemplateField HeaderText="District Name" SortExpression="DISTRICT_ID" >
                         <EditItemTemplate>
                             <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsDistrict" 
                                 DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" SelectedValue='<%# Bind("DISTRICT_ID") %>'>
                             </asp:DropDownList>
                         </EditItemTemplate>
                         <ItemTemplate >
                             <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsDistrict" Enabled="false"  
                                 DataTextField="DISTRICT_NAME"  DataValueField="DISTRICT_ID" SelectedValue='<%# Bind("DISTRICT_ID") %>'>
                             </asp:DropDownList>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="THANA_NAME" HeaderText="Thana Name" 
                         SortExpression="THANA_NAME" />
                    <asp:CommandField ShowEditButton="true" ButtonType="Button" />     
                 </Columns>
                 <PagerStyle CssClass="pgr" />
                                       <HeaderStyle Font-Bold="False" />
                                       <AlternatingRowStyle CssClass="alt" />
             </asp:GridView>
         
         </div>
         <div style="BACKGROUND-COLOR: royalblue">
            <strong>
             <span style="COLOR: white">Add New&nbsp;Thana&nbsp;</span>
            </strong>
         </div>
            <div>
                <asp:DetailsView ID="DetailsView1" runat="server" Height="60px"  BorderColor="Silver"
                    AutoGenerateRows="False" DataKeyNames="THANA_ID" DataSourceID="sdsThana" DefaultMode="Insert"
                    CssClass="mGrid" PagerStyle-CssClass="pgr"  
                                 AlternatingRowStyle-CssClass="alt" GridLines="None" BorderStyle="None"
            Font-Names="Times New Roman" Font-Size="11pt"  Width="240px">
                    <PagerStyle CssClass="pgr" />
                    <Fields>
                        
                        <asp:BoundField DataField="THANA_ID" HeaderText="THANA_ID" 
                            SortExpression="THANA_ID" ReadOnly="True" Visible="false" />
                         <asp:TemplateField HeaderText="District Name" SortExpression="DISTRICT_ID">
                             <EditItemTemplate>
                                 <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsMngDist" 
                                     DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" SelectedValue='<%# Bind("DISTRICT_ID") %>'>
                                 </asp:DropDownList>
                             </EditItemTemplate>
                             <InsertItemTemplate>
                                 <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsMngDist" 
                                     DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" SelectedValue='<%# Bind("DISTRICT_ID") %>'>
                                 </asp:DropDownList>
                             </InsertItemTemplate>
                             <ItemTemplate>
                                 <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsMngDist" 
                                     DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" SelectedValue='<%# Bind("DISTRICT_ID") %>'>
                                 </asp:DropDownList>
                             </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="THANA_NAME" HeaderText="Thana Name" 
                            SortExpression="THANA_NAME" />
                            <asp:CommandField ShowInsertButton="true" ButtonType="Button" />
                    </Fields>
                    <AlternatingRowStyle CssClass="alt" />
                </asp:DetailsView>
      </div>
   </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>