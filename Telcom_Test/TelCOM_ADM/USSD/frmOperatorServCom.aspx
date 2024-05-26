<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOperatorServCom.aspx.cs" Inherits="COMMON_frmOperatorServCom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Operator Service Commission</title>
   <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div>      
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>             
            <asp:SqlDataSource ID="sdsChannelType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT * FROM &quot;CHANNEL_TYPE&quot;">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsServList" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT &quot;SERVICE_ID&quot;, &quot;SERVICE_TITLE&quot; FROM &quot;SERVICE_LIST&quot;">
            </asp:SqlDataSource>         
            <asp:SqlDataSource ID="sdsOprtServCom" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                DeleteCommand="DELETE FROM &quot;OPARETOR_SERVICE_COMMISSION&quot; WHERE &quot;OPARETOR_SERVICE_COMI_ID&quot; = :OPARETOR_SERVICE_COMI_ID" 
                InsertCommand="INSERT INTO &quot;OPARETOR_SERVICE_COMMISSION&quot; (&quot;OPARETOR_SERVICE_COMI_ID&quot;, &quot;OPARETOR_NAME&quot;, &quot;SERVICE_ID&quot;, &quot;COMMISSION_RATE&quot;, &quot;OPARETOR_CODE&quot;,&quot;CHANNEL_TYPE_ID&quot;, &quot;NO_OF_START_TRANSACTION&quot;, &quot;NO_OF_END_TRANSACTION&quot;) VALUES (:OPARETOR_SERVICE_COMI_ID, :OPARETOR_NAME, :SERVICE_ID, :COMMISSION_RATE, :OPARETOR_CODE,:CHANNEL_TYPE_ID, :NO_OF_START_TRANSACTION, :NO_OF_END_TRANSACTION)" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT * FROM &quot;OPARETOR_SERVICE_COMMISSION&quot; WHERE (&quot;SERVICE_ID&quot; = :SERVICE_ID)" 
                UpdateCommand="UPDATE &quot;OPARETOR_SERVICE_COMMISSION&quot; SET &quot;OPARETOR_NAME&quot; = :OPARETOR_NAME, &quot;COMMISSION_RATE&quot; = :COMMISSION_RATE, &quot;OPARETOR_CODE&quot; = :OPARETOR_CODE, &quot;CHANNEL_TYPE_ID&quot; = :CHANNEL_TYPE_ID, &quot;NO_OF_START_TRANSACTION&quot; = :NO_OF_START_TRANSACTION, &quot;NO_OF_END_TRANSACTION&quot; = :NO_OF_END_TRANSACTION WHERE &quot;OPARETOR_SERVICE_COMI_ID&quot; = :OPARETOR_SERVICE_COMI_ID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlServiceList" Name="SERVICE_ID" 
                        PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="OPARETOR_SERVICE_COMI_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters> 
                    <asp:Parameter Name="OPARETOR_NAME" Type="String" />
                    <asp:Parameter Name="COMMISSION_RATE" Type="String" />
                    <asp:Parameter Name="OPARETOR_CODE" Type="String" />
                    <asp:Parameter Name="CHANNEL_TYPE_ID" Type="Decimal" />
                    <asp:Parameter Name="NO_OF_START_TRANSACTION" Type="Decimal" />
                    <asp:Parameter Name="NO_OF_END_TRANSACTION" Type="Decimal" />
                    <asp:Parameter Name="OPARETOR_SERVICE_COMI_ID" Type="String" />
                   
                </UpdateParameters>
                <InsertParameters>                  
                    <asp:Parameter Name="OPARETOR_SERVICE_COMI_ID" Type="String" />                    
                    <asp:Parameter Name="OPARETOR_NAME" Type="String" />
                    <asp:ControlParameter ControlID="ddlServiceList" Name="SERVICE_ID" PropertyName="SelectedValue" />                   
                    <asp:Parameter Name="COMMISSION_RATE" Type="String" />                   
                    <asp:Parameter Name="OPARETOR_CODE" Type="String" />                    
                    <asp:Parameter Name="CHANNEL_TYPE_ID" Type="Decimal" />
                    <asp:Parameter Name="NO_OF_START_TRANSACTION" Type="Decimal" />
                    <asp:Parameter Name="NO_OF_END_TRANSACTION" Type="Decimal" />
                </InsertParameters>
            </asp:SqlDataSource>
         <div style="background-color: royalblue">
          <strong>
            <span style="color: white">
             Service List &nbsp;
               <asp:DropDownList ID="ddlServiceList" runat="server" DataSourceID="sdsServList" 
               AutoPostBack="true" DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" >
               </asp:DropDownList>
          </span>              
          </strong>
         </div>
            <asp:GridView ID="gdvOperatorServComi" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" DataKeyNames="OPARETOR_SERVICE_COMI_ID" 
                DataSourceID="sdsOprtServCom"   GridLines="None"
                CssClass="mGrid" PagerStyle-CssClass="pgr" BorderStyle="None"
                               AlternatingRowStyle-CssClass="alt" 
                onrowupdating="gdvOperatorServComi_RowUpdating" >
                <Columns>                    
                    <asp:BoundField DataField="OPARETOR_SERVICE_COMI_ID" 
                        HeaderText="OPARETOR_SERVICE_COMI_ID" ReadOnly="True" 
                        SortExpression="OPARETOR_SERVICE_COMI_ID"  Visible="false"/>
                    <asp:TemplateField HeaderText="Operator Name" SortExpression="OPARETOR_NAME">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlOpertrName" runat="server" AutoPostBack="True" 
                                DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE_NAME" 
                                DataValueField="CHANNEL_TYPE_ID" 
                                SelectedValue='<%# Eval("CHANNEL_TYPE_ID") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlChannel" runat="server"  Enabled="False" 
                            AutoPostBack="True" DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE_NAME" 
                                DataValueField="CHANNEL_TYPE_ID" 
                                SelectedValue='<%# Eval("CHANNEL_TYPE_ID") %>'>
                                <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="COMMISSION_RATE" HeaderText="Commission Rate" 
                        SortExpression="COMMISSION_RATE"   />                                           
                    <asp:BoundField DataField="NO_OF_START_TRANSACTION" HeaderText="No of Start Tran" 
                        SortExpression="NO_OF_START_TRANSACTION"   />
                    <asp:BoundField DataField="NO_OF_END_TRANSACTION" HeaderText="No of End Tran" 
                        SortExpression="NO_OF_END_TRANSACTION" />
                     <asp:CommandField ShowEditButton="True"  ButtonType="Button" />   
                </Columns>
                <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            <div style="background-color: royalblue">
              <strong>
               <span style="color: white">
                  Add Service Commission
               </span>
              </strong>
            </div>
            <asp:DetailsView ID="dtvOperatorServCom" runat="server" Height="170px" 
                        Width="320px" AutoGenerateRows="False" DataKeyNames="OPARETOR_SERVICE_COMI_ID" 
                        DataSourceID="sdsOprtServCom" DefaultMode="Insert" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        BorderStyle="None" oniteminserting="dtvOperatorServCom_ItemInserting" >
                <PagerStyle CssClass="pgr" />
                 <Fields>
                    <asp:BoundField DataField="OPARETOR_SERVICE_COMI_ID" 
                        HeaderText="OPARETOR_SERVICE_COMI_ID" ReadOnly="True" 
                        SortExpression="OPARETOR_SERVICE_COMI_ID" Visible="false" />                    
                    <asp:TemplateField HeaderText="Operator Name" SortExpression="SERVICE_ID">
                        <InsertItemTemplate>
                           <asp:DropDownList ID="ddlChannelName" runat="server"  
                                 DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE_NAME"  
                                 DataValueField="CHANNEL_TYPE_ID" 
                                SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                              </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlChannelNameID" runat="server"  
                                 DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE_NAME"  
                                 DataValueField="CHANNEL_TYPE_ID" SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                            </asp:DropDownList>                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COMMISSION_RATE" HeaderText="Commission Rate" 
                        SortExpression="COMMISSION_RATE" />                    
                    <asp:BoundField DataField="NO_OF_START_TRANSACTION" HeaderText="No of Start Tran" 
                        SortExpression="NO_OF_START_TRANSACTION"   />
                    <asp:BoundField DataField="NO_OF_END_TRANSACTION" HeaderText="No of End Tran" 
                        SortExpression="NO_OF_END_TRANSACTION"   />    
                    <asp:CommandField ShowInsertButton="true"  ButtonType="Button" />    
                </Fields>
                 <AlternatingRowStyle CssClass="alt" />
            </asp:DetailsView>
        </ContentTemplate>
       </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
