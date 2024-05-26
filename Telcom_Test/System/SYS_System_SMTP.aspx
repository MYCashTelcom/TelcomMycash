<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_System_SMTP.aspx.cs" Inherits="System_SYS_System_SMTP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System SMTP</title>
   <link type="text/css" rel="Stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
    <div style="background-color: royalblue">
        <strong><span style="color: white">Manage System SMTP</span></strong></div>
   
    <asp:SqlDataSource ID="sdsSysSMTP" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM &quot;SYSTEM_SMTP&quot;"
        InsertCommand="INSERT INTO SYSTEM_SMTP (SYS_SMTP_ID,SYS_SMTP_HOST,SYS_SMTP_PORT,SYS_SMTP_SENDER,SYS_SMTP_SPASS,SYS_MAIL_RECEIVER,SYS_SMTP_STATUS,SYS_SENDER_ADDRESS)
                       VALUES (:SYS_SMTP_ID,:SYS_SMTP_HOST,:SYS_SMTP_PORT,:SYS_SMTP_SENDER,:SYS_SMTP_SPASS,:SYS_MAIL_RECEIVER,:SYS_SMTP_STATUS,:SYS_SENDER_ADDRESS) "
        UpdateCommand="UPDATE SYSTEM_SMTP SET SYS_SMTP_ID=:SYS_SMTP_ID,SYS_SMTP_HOST=:SYS_SMTP_HOST,SYS_SMTP_PORT=:SYS_SMTP_PORT,SYS_SMTP_SENDER=:SYS_SMTP_SENDER,SYS_SMTP_SPASS=:SYS_SMTP_SPASS,SYS_MAIL_RECEIVER=:SYS_MAIL_RECEIVER,SYS_SMTP_STATUS=:SYS_SMTP_STATUS,SYS_SENDER_ADDRESS=:SYS_SENDER_ADDRESS WHERE SYS_SMTP_ID=:SYS_SMTP_ID">
        
        <UpdateParameters>
            <asp:Parameter Name="SYS_SMTP_ID" />
            <asp:Parameter Name="SYS_SMTP_HOST" />
            <asp:Parameter Name="SYS_SMTP_PORT" />
            <asp:Parameter Name="SYS_SMTP_SENDER" />
            <asp:Parameter Name="SYS_SMTP_SPASS" />
            <asp:Parameter Name="SYS_MAIL_RECEIVER" />
            <asp:Parameter Name="SYS_SMTP_STATUS" />
            <asp:Parameter Name="SYS_SENDER_ADDRESS" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SYS_SMTP_ID" />
            <asp:Parameter Name="SYS_SMTP_HOST" />
            <asp:Parameter Name="SYS_SMTP_PORT" />
            <asp:Parameter Name="SYS_SMTP_SENDER" />
            <asp:Parameter Name="SYS_SMTP_SPASS" />
            <asp:Parameter Name="SYS_MAIL_RECEIVER" />
            <asp:Parameter Name="SYS_SMTP_STATUS" />
            <asp:Parameter Name="SYS_SENDER_ADDRESS" />
        </InsertParameters>        
     </asp:SqlDataSource>   
    
    <asp:GridView ID="gdvSystemSMTPInfo" runat="server"  AutoGenerateColumns="False" 
           AllowPaging="True"   
        DataSourceID="sdsSysSMTP" DataKeyNames="SYS_SMTP_ID"  BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
           AlternatingRowStyle-CssClass="alt" >
        <Columns>
            <asp:BoundField DataField="SYS_SMTP_ID" HeaderText="ID" 
                SortExpression="SYS_SMTP_ID" Visible="False" />
            <asp:TemplateField HeaderText="Host" SortExpression="SYS_SMTP_HOST">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6"  Font-Size="8" Font-Names="Tahoma" runat="server" Text='<%# Bind("SYS_SMTP_HOST") %>' 
                        Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SYS_SMTP_HOST") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Port" SortExpression="SYS_SMTP_PORT">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" Font-Size="8" Font-Names="Tahoma" runat="server" Text='<%# Bind("SYS_SMTP_PORT") %>' 
                        Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SYS_SMTP_PORT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sender" SortExpression="SYS_SMTP_SENDER">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" Font-Size="8" Font-Names="Tahoma" runat="server" Text='<%# Bind("SYS_SMTP_SENDER") %>' 
                        Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SYS_SMTP_SENDER") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Spass" SortExpression="SYS_SMTP_SPASS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" Font-Size="8" Font-Names="Tahoma" runat="server" Text='<%# Bind("SYS_SMTP_SPASS") %>' 
                        Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("SYS_SMTP_SPASS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mail Receiver" 
                SortExpression="SYS_MAIL_RECEIVER">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" Font-Size="8" Font-Names="Tahoma" runat="server" 
                        Text='<%# Bind("SYS_MAIL_RECEIVER") %>' TextMode="MultiLine" Width="400px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SYS_MAIL_RECEIVER") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="SYS_SMTP_STATUS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" Font-Size="8" Font-Names="Tahoma" runat="server" Text='<%# Bind("SYS_SMTP_STATUS") %>' 
                        Width="50px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("SYS_SMTP_STATUS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sender Address" 
                SortExpression="SYS_SENDER_ADDRESS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" Font-Size="8" Font-Names="Tahoma" runat="server" 
                        Text='<%# Bind("SYS_SENDER_ADDRESS") %>' Width="140px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("SYS_SENDER_ADDRESS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="true" /> 
              
        </Columns>
        <PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    </asp:GridView>
      
  <div style="background-color: royalblue "><strong><span style="color: white;">System SMTP Creation</span></strong> </div>
         <asp:DetailsView ID="dtvSysSMTP" runat="server" 
            AutoGenerateRows="False" DataKeyNames="SYS_SMTP_ID" DataSourceID="sdsSysSMTP" DefaultMode="Insert" 
       GridLines="None" Height="50px" Width="254px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" BorderStyle="None">
         
             <PagerStyle CssClass="pgr" />
         
            <Fields>
                <asp:BoundField DataField="SYS_SMTP_ID" HeaderText="ID" 
                    SortExpression="SYS_SMTP_ID" Visible="False" />
                <asp:BoundField DataField="SYS_SMTP_HOST" HeaderText="Host" 
                    SortExpression="SYS_SMTP_HOST" />
                <asp:BoundField DataField="SYS_SMTP_PORT" HeaderText="Port" 
                    SortExpression="SYS_SMTP_PORT" />
                <asp:BoundField DataField="SYS_SMTP_SENDER" HeaderText="Sender" 
                    SortExpression="SYS_SMTP_SENDER" />
                <asp:BoundField DataField="SYS_SMTP_SPASS" HeaderText="Spass" 
                    SortExpression="SYS_SMTP_SPASS" />
                <asp:BoundField DataField="SYS_MAIL_RECEIVER" HeaderText="Mail Receiver" 
                    SortExpression="SYS_MAIL_RECEIVER" />
                <asp:BoundField DataField="SYS_SMTP_STATUS" HeaderText="Status" 
                    SortExpression="SYS_SMTP_STATUS" />
                <asp:BoundField DataField="SYS_SENDER_ADDRESS" HeaderText="Address" 
                    SortExpression="SYS_SENDER_ADDRESS" />
            
            <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            </Fields>
            <AlternatingRowStyle CssClass="alt" />
        </asp:DetailsView>   
        </ContentTemplate>   
         </asp:UpdatePanel>
    </form>
</body>
</html>
