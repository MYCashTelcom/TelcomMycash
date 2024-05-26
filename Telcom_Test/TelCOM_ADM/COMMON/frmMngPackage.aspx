<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="frmMngPackage.aspx.cs" Inherits="Forms_frmMngPackage" Title="Manage Packages" %>

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
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
      <contenttemplate>
        <table id="table1" width="100%">
         <tr><td valign="top">
          <div style="BACKGROUND-COLOR: royalblue">
            <strong><span style="COLOR: white; font-size: 11px;">Manage&nbsp;Packages</span></strong>
          </div>
      <div>
       <asp:SqlDataSource id="sdsSrvPackages" runat="server" 
              SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME, SERVICE_PKG_CHARGE_TYPE, SERVICE_PKG_SUBS_FEE, SERVICE_PKG_MONTH_FEE, SERVICE_PKG_STATUS, 
                             SERVICE_PKG_DAILY_TRN_LIMIT, SERVICE_PKG_MONTH_TRN_LIMIT,WEB_URL,WAP_URL,USSD_MENU 
                             FROM SERVICE_PACKAGE' 
              UpdateCommand='UPDATE SERVICE_PACKAGE SET SERVICE_PKG_NAME = :SERVICE_PKG_NAME, SERVICE_PKG_CHARGE_TYPE = :SERVICE_PKG_CHARGE_TYPE, 
                             SERVICE_PKG_SUBS_FEE = :SERVICE_PKG_SUBS_FEE, SERVICE_PKG_MONTH_FEE = :SERVICE_PKG_MONTH_FEE, SERVICE_PKG_STATUS = :SERVICE_PKG_STATUS,
                             SERVICE_PKG_DAILY_TRN_LIMIT = :SERVICE_PKG_DAILY_TRN_LIMIT, SERVICE_PKG_MONTH_TRN_LIMIT = :SERVICE_PKG_MONTH_TRN_LIMIT,WEB_URL=:WEB_URL,WAP_URL=:WAP_URL,USSD_MENU=:USSD_MENU 
                             WHERE (SERVICE_PKG_ID = :SERVICE_PKG_ID)' 
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              InsertCommand='INSERT INTO SERVICE_PACKAGE(SERVICE_PKG_ID, SERVICE_PKG_NAME, SERVICE_PKG_CHARGE_TYPE, SERVICE_PKG_SUBS_FEE, SERVICE_PKG_MONTH_FEE, SERVICE_PKG_STATUS, SERVICE_PKG_DAILY_TRN_LIMIT, SERVICE_PKG_MONTH_TRN_LIMIT,WEB_URL,WAP_URL,USSD_MENU) VALUES (:SERVICE_PKG_ID, :SERVICE_PKG_NAME, :SERVICE_PKG_CHARGE_TYPE, :SERVICE_PKG_SUBS_FEE, :SERVICE_PKG_MONTH_FEE, :SERVICE_PKG_STATUS, :SERVICE_PKG_DAILY_TRN_LIMIT, :SERVICE_PKG_MONTH_TRN_LIMIT, :WEB_URL,:WAP_URL,:USSD_MENU)' 
              ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
              DeleteCommand='DELETE FROM "SERVICE_PACKAGE" WHERE "SERVICE_PKG_ID" = :SERVICE_PKG_ID'>
              <DeleteParameters>
                     <asp:Parameter Type="String" Name="SERVICE_PKG_ID">   </asp:Parameter>
             </DeleteParameters>
             <UpdateParameters>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_NAME"></asp:Parameter>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_CHARGE_TYPE"></asp:Parameter>
                    <asp:Parameter Type="Decimal" Name="SERVICE_PKG_SUBS_FEE"></asp:Parameter>
                    <asp:Parameter Type="Decimal" Name="SERVICE_PKG_MONTH_FEE"></asp:Parameter>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_STATUS"></asp:Parameter>
                    <asp:Parameter Name="SERVICE_PKG_DAILY_TRN_LIMIT" />
                    <asp:Parameter Name="SERVICE_PKG_MONTH_TRN_LIMIT" />
                    
                    <asp:Parameter Type="String" Name="WEB_URL" />
                    <asp:Parameter Type="String" Name="WAP_URL" />
                    <asp:Parameter Type="String" Name="USSD_MENU" />
                    
                    <asp:Parameter Type="String" Name="SERVICE_PKG_ID"></asp:Parameter>
             </UpdateParameters>
             <InsertParameters>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_ID"></asp:Parameter>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_NAME"></asp:Parameter>
                    <asp:Parameter Type="String" Name="SERVICE_PKG_CHARGE_TYPE"></asp:Parameter>
                    <asp:Parameter Type="Decimal" Name="SERVICE_PKG_SUBS_FEE"></asp:Parameter>
                    <asp:Parameter Type="Decimal" Name="SERVICE_PKG_MONTH_FEE"></asp:Parameter>                    
                    <asp:Parameter Name="SERVICE_PKG_DAILY_TRN_LIMIT" />
                    <asp:Parameter Name="SERVICE_PKG_MONTH_TRN_LIMIT" />                    
                    <asp:Parameter Type="String" Name="WEB_URL" />
                    <asp:Parameter Type="String" Name="WAP_URL" />
                    <asp:Parameter Type="String" Name="USSD_MENU" />                    
                    <asp:Parameter Type="String" Name="SERVICE_PKG_STATUS"></asp:Parameter>
                    
            </InsertParameters>
       </asp:SqlDataSource> 
       <asp:GridView id="gdvPackages" runat="server" AllowSorting="True" 
                AllowPaging="True" DataKeyNames="SERVICE_PKG_ID" DataSourceID="sdsSrvPackages" 
                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" onrowdeleted="gdvPackages_RowDeleted" 
                onrowupdated="gdvPackages_RowUpdated">
           <Columns>
                <asp:BoundField ReadOnly="True" DataField="SERVICE_PKG_ID" Visible="False"
                      SortExpression="SERVICE_PKG_ID" HeaderText="SERVICE_PKG_ID">
                </asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_NAME" SortExpression="SERVICE_PKG_NAME" HeaderText="Package Name">
                      <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField SortExpression="SERVICE_PKG_CHARGE_TYPE" HeaderText="Charge Type">
                    <EditItemTemplate>
                        <asp:DropDownList id="DropDownList1" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_CHARGE_TYPE") %>'>
                            <asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                            <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                            <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                            <asp:ListItem Value="FRE">Free</asp:ListItem>
                        </asp:DropDownList> 
                    </EditItemTemplate>
                    <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                      <asp:DropDownList id="DropDownList2" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_CHARGE_TYPE") %>' Enabled="False">
                            <asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                            <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                            <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                            <asp:ListItem Value="FRE">Free</asp:ListItem>
                      </asp:DropDownList> 
                    </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField SortExpression="SERVICE_PKG_SUBS_FEE" HeaderText="Subscription Fee">
                   <EditItemTemplate>
                          <asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("SERVICE_PKG_SUBS_FEE") %>' Width="60px"></asp:TextBox>
                   </EditItemTemplate>

                    <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                   <ItemTemplate>
                      <asp:Label id="Label2" runat="server" Text='<%# Bind("SERVICE_PKG_SUBS_FEE") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField SortExpression="SERVICE_PKG_MONTH_FEE" HeaderText="Monthly Fee">
                 <EditItemTemplate>
                      <asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("SERVICE_PKG_MONTH_FEE") %>' Width="60px">
                      </asp:TextBox>
                 </EditItemTemplate>

                  <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                  <ItemTemplate>
                       <asp:Label id="Label3" runat="server" Text='<%# Bind("SERVICE_PKG_MONTH_FEE") %>'></asp:Label>
                  </ItemTemplate>
               </asp:TemplateField>
                <asp:BoundField DataField="SERVICE_PKG_DAILY_TRN_LIMIT" HeaderText="Daily Tran. Limit"
                    SortExpression="SERVICE_PKG_DAILY_TRN_LIMIT" />
                <asp:BoundField DataField="SERVICE_PKG_MONTH_TRN_LIMIT" HeaderText="Monthly Tran. Limit"
                    SortExpression="SERVICE_PKG_MONTH_TRN_LIMIT" />
                <asp:BoundField DataField="WEB_URL" HeaderText="WEB url" 
                    SortExpression="WEB_URL" />
                <asp:BoundField DataField="WAP_URL" HeaderText="WAP url" 
                    SortExpression="WAP_URL" />
                <asp:BoundField DataField="USSD_MENU" HeaderText="USSD Menu" 
                    SortExpression="USSD_MENU" />    
                <asp:TemplateField SortExpression="SERVICE_PKG_STATUS" HeaderText="Status">
                    <EditItemTemplate>
                        <asp:DropDownList id="DropDownList4" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_STATUS") %>'>
                        <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                            <asp:ListItem Value="L">Locked</asp:ListItem>
                            <asp:ListItem Value="I">Inactive</asp:ListItem>
                        </asp:DropDownList> 
                    </EditItemTemplate>

                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:DropDownList id="DropDownList3" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_STATUS") %>' Enabled="False"><asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="L">Locked</asp:ListItem>
                        <asp:ListItem Value="I">Inactive</asp:ListItem>
                        </asp:DropDownList> 
                   </ItemTemplate>
                </asp:TemplateField>
                
                <asp:CommandField ShowDeleteButton="True" EditText=" Edit " ShowEditButton="True"></asp:CommandField>
                </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
       </asp:GridView> 
    </div>
   </td>
  <td valign="top">
           <div style="BACKGROUND-COLOR: royalblue"><strong>
               <span style="COLOR: white; font-size: 11px;">New&nbsp;Package</span></strong>
           </div>
       <div>
       <asp:DetailsView id="DetailsView1" runat="server" Font-Size="11pt" 
                Font-Names="Times New Roman" DataKeyNames="SERVICE_PKG_ID" 
                DataSourceID="sdsSrvPackages" BorderColor="Silver" DefaultMode="Insert" 
                AutoGenerateRows="False" Height="50px" Width="125px" 
                oniteminserted="DetailsView1_ItemInserted"><Fields>
                <asp:BoundField ReadOnly="True" DataField="SERVICE_PKG_ID" Visible="False" SortExpression="SERVICE_PKG_ID" HeaderText="SERVICE_PKG_ID"></asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_NAME" SortExpression="SERVICE_PKG_NAME" HeaderText="Package Name">
                <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField SortExpression="SERVICE_PKG_CHARGE_TYPE" HeaderText="Charge Type"><EditItemTemplate>
                <asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SERVICE_PKG_CHARGE_TYPE") %>'></asp:TextBox> 
                </EditItemTemplate>
                <InsertItemTemplate>
                <asp:DropDownList id="DropDownList5" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_CHARGE_TYPE") %>'><asp:ListItem Value="PRP">Pre Paid</asp:ListItem>
                <asp:ListItem Value="POP">Post Paid</asp:ListItem>
                <asp:ListItem Value="HYB">Hybrid</asp:ListItem>
                <asp:ListItem Value="FRE">Free</asp:ListItem>
                </asp:DropDownList> 
                </InsertItemTemplate>
                <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                <ItemTemplate>
                <asp:Label id="Label1" runat="server" Text='<%# Bind("SERVICE_PKG_CHARGE_TYPE") %>'></asp:Label> 
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SERVICE_PKG_SUBS_FEE" SortExpression="SERVICE_PKG_SUBS_FEE" HeaderText="Subscription Fee">
                <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_MONTH_FEE" SortExpression="SERVICE_PKG_MONTH_FEE" HeaderText="Monthly Fee">
                <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_DAILY_TRN_LIMIT" HeaderText="Daily Tran. Limit"
                        SortExpression="SERVICE_PKG_DAILY_TRN_LIMIT">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_MONTH_TRN_LIMIT" HeaderText="Monthly Tran. Limit"
                        SortExpression="SERVICE_PKG_MONTH_TRN_LIMIT">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                
                <asp:BoundField DataField="WEB_URL" HeaderText="Web URL"
                        SortExpression="WEB_URL">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="WAP_URL" HeaderText="WAP URL"
                        SortExpression="WAP_URL">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="USSD_MENU" HeaderText="USSD Menu"
                        SortExpression="USSD_MENU">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                
                
                <asp:TemplateField SortExpression="SERVICE_PKG_STATUS" HeaderText="Status">
                  <EditItemTemplate>
                    <asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("SERVICE_PKG_STATUS") %>'></asp:TextBox> 
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList id="DropDownList6" runat="server" SelectedValue='<%# Bind("SERVICE_PKG_STATUS") %>'><asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="L">Locked</asp:ListItem>
                        <asp:ListItem Value="I">Inactive</asp:ListItem>
                        </asp:DropDownList> 
                    </InsertItemTemplate>

                    <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True">
                        </HeaderStyle>
                    <ItemTemplate>
                       <asp:Label id="Label2" runat="server" Text='<%# Bind("SERVICE_PKG_STATUS") %>'>
                       </asp:Label> 
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:CommandField InsertText="Add Package" ButtonType="Button" 
                        ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:CommandField>
                </Fields>
       </asp:DetailsView>
      </div>
    </TD>
    </TR>
 </TABLE>
 <table width="100%">
  <tr>
<td valign="top">
  <div style="BACKGROUND-COLOR: royalblue"><strong>
        <SPAN style="COLOR: white; font-size: 11px;">Package: 
        <asp:DropDownList id="ddlPackage" runat="server" DataSourceID="sdsSrvPackages" 
               AutoPostBack="True" DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID">
        </asp:DropDownList>
        </span></STRONG>
        </DIV>
        <div>
        <asp:SqlDataSource id="sdsSrvPkgDetails"
         runat="server" 
        SelectCommand='SELECT "SERVICE_PKG_DTL_ID", "SERVICE_PKG_ID", "SERVICE_RATE_ID" FROM "SERVICE_PACKAGE_DETAIL" WHERE ("SERVICE_PKG_ID" = :SERVICE_PKG_ID)' 
        UpdateCommand='UPDATE "SERVICE_PACKAGE_DETAIL" SET "SERVICE_RATE_ID" = :SERVICE_RATE_ID WHERE "SERVICE_PKG_DTL_ID" = :SERVICE_PKG_DTL_ID'
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         InsertCommand='INSERT INTO "SERVICE_PACKAGE_DETAIL" ("SERVICE_PKG_DTL_ID", "SERVICE_PKG_ID", "SERVICE_RATE_ID") VALUES (:SERVICE_PKG_DTL_ID, :SERVICE_PKG_ID, :SERVICE_RATE_ID)' ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "SERVICE_PACKAGE_DETAIL" WHERE "SERVICE_PKG_DTL_ID" = :SERVICE_PKG_DTL_ID'>
<DeleteParameters>
<asp:Parameter Type="String" Name="SERVICE_PKG_DTL_ID"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="String" Name="SERVICE_RATE_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_PKG_DTL_ID"></asp:Parameter>
</UpdateParameters>
<SelectParameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="String" Name="SERVICE_PKG_ID" ControlID="ddlPackage"></asp:ControlParameter>
</SelectParameters>
<InsertParameters>
<asp:Parameter Type="String" Name="SERVICE_PKG_DTL_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_PKG_ID"></asp:Parameter>
<asp:Parameter Type="String" Name="SERVICE_RATE_ID"></asp:Parameter>
</InsertParameters>
</asp:SqlDataSource> <asp:SqlDataSource id="sdsSrvRate" runat="server" SelectCommand="SELECT SR.SERVICE_RATE_ID,SR.SERVICE_RATE_TITLE||' ('||SL.SERVICE_TITLE||')' RATE_DESC FROM SERVICE_RATE SR, SERVICE_LIST SL WHERE SR.SERVICE_ID=SL.SERVICE_ID" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
</asp:SqlDataSource> 
<asp:GridView id="gdvPackageDetail" runat="server" AllowSorting="True" 
        DataKeyNames="SERVICE_PKG_DTL_ID" DataSourceID="sdsSrvPkgDetails" AutoGenerateColumns="False"
CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        onrowdeleted="gdvPackageDetail_RowDeleted" 
        onrowupdated="gdvPackageDetail_RowUpdated"><Columns>
<asp:BoundField ReadOnly="True" DataField="SERVICE_PKG_DTL_ID" Visible="False" SortExpression="SERVICE_PKG_DTL_ID" HeaderText="SERVICE_PKG_DTL_ID"></asp:BoundField>
<asp:BoundField DataField="SERVICE_PKG_ID" Visible="False" SortExpression="SERVICE_PKG_ID" HeaderText="SERVICE_PKG_ID"></asp:BoundField>
<asp:TemplateField SortExpression="SERVICE_RATE_ID" HeaderText="Service Name"><EditItemTemplate>
<asp:DropDownList id="DropDownList10" runat="server" DataSourceID="sdsSrvRate" DataValueField="SERVICE_RATE_ID" DataTextField="RATE_DESC" SelectedValue='<%# Bind("SERVICE_RATE_ID") %>'></asp:DropDownList> 
</EditItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:DropDownList id="DropDownList9" runat="server" DataSourceID="sdsSrvRate" DataValueField="SERVICE_RATE_ID" DataTextField="RATE_DESC" SelectedValue='<%# Bind("SERVICE_RATE_ID") %>' Enabled="False"></asp:DropDownList> 
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField ShowDeleteButton="True" EditText=" Edit " ShowEditButton="True"></asp:CommandField>
</Columns>
    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />
     </asp:GridView>
    </DIV>
    <div style="BACKGROUND-COLOR: royalblue"><strong>
            <SPAN style="COLOR: white; font-size: 11px;">Add Service</SPAN></STRONG>
     </div>
    <div>
       <asp:DetailsView id="stvAddService" runat="server" 
        DataKeyNames="SERVICE_PKG_DTL_ID" DataSourceID="sdsSrvPkgDetails" 
        BorderColor="Silver" DefaultMode="Insert" AutoGenerateRows="False" 
        Height="50px" Width="125px" OnItemInserting="stvAddService_ItemInserting" 
        oniteminserted="stvAddService_ItemInserted">
        <FieldHeaderStyle Font-Size="X-Small" />
          <Fields>
                <asp:BoundField ReadOnly="True" DataField="SERVICE_PKG_DTL_ID" Visible="False" SortExpression="SERVICE_PKG_DTL_ID" HeaderText="SERVICE_PKG_DTL_ID"></asp:BoundField>
                <asp:BoundField DataField="SERVICE_PKG_ID" Visible="False" SortExpression="SERVICE_PKG_ID" HeaderText="SERVICE_PKG_ID"></asp:BoundField>
                <asp:TemplateField SortExpression="SERVICE_RATE_ID" HeaderText="Service">
                    <EditItemTemplate>
                           <asp:DropDownList id="DropDownList8" runat="server" DataSourceID="sdsSrvRate" 
                               SelectedValue='<%# Bind("SERVICE_RATE_ID") %>' DataTextField="RATE_DESC" 
                               DataValueField="SERVICE_RATE_ID">
                           </asp:DropDownList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                         <asp:DropDownList id="DropDownList7" runat="server" DataSourceID="sdsSrvRate" 
                             SelectedValue='<%# Bind("SERVICE_RATE_ID") %>' DataTextField="RATE_DESC"
                              DataValueField="SERVICE_RATE_ID">
                         </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                    <asp:Label id="Label4" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField InsertText="Add Service" ButtonType="Button" 
                      ShowInsertButton="True">
                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:CommandField>
             </Fields>
        </asp:DetailsView>
       </div>
       </td>
       </tr>
    </table>
   </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>