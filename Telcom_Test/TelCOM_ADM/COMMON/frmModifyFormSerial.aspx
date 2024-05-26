<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmModifyFormSerial.aspx.cs" Inherits="COMMON_frmModifyFormSerial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify Form Serial No</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scmMsgService" runat="server">
    </asp:ScriptManager>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         SelectCommand="SELECT CL.CLINT_NAME,AL.ACCNT_NO,ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,ASD.SERIAL_NO
 FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE ASD.CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN AND AL.CLINT_ID=CL.CLINT_ID
 AND SERIAL_NO=''" 
         
         
         UpdateCommand="UPDATE &quot;ACCOUNT_SERIAL_DETAIL&quot; SET &quot;SERIAL_NO&quot; = :SERIAL_NO WHERE &quot;CUSTOMER_MOBILE_NO&quot; = :CUSTOMER_MOBILE_NO">
         <UpdateParameters>
             <asp:Parameter Name="SERIAL_NO" />
             <asp:Parameter Name="CUSTOMER_MOBILE_NO" />
         </UpdateParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="sdsAvailable" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
         SelectCommand="SELECT SERIAL_NO,STATUS,CUSTOMER_MOBILE_NO,AGENT_MOBILE_NO FROM  ACCOUNT_SERIAL_DETAIL WHERE  SERIAL_NO=''"></asp:SqlDataSource>
    <asp:UpdatePanel ID="UDPanel" runat="server">
        <ContentTemplate>
            <div style="background-color: royalblue; text-align: left;">
                <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">&nbsp;&nbsp;Manage Form Serial&nbsp;
                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <td colspan="3" align="center" style="font-size: 11px; font-weight: bold; color: White;">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>
                </td>
            </div>
            <div id="divWalletSerch" style="padding-left: 5px;">                
                <span style="font-size: 12px; font-weight: normal;">Form Serial No: </span>
                <asp:TextBox ID="txtserailNo" runat="server"></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
            </div>
        
            <div id="divGrid" style="padding-left: 5px;">
             <asp:GridView ID="gdvSearch" runat="server" AutoGenerateColumns="False" 
                    Width="850px" Visible="False" BorderStyle="None"  GridLines="None"
         DataKeyNames="CUSTOMER_MOBILE_NO" DataSourceID="SqlDataSource1" CssClass="mGrid" 
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
         <Columns>
             <asp:TemplateField HeaderText="Form Serial No" SortExpression="SERIAL_NO">
                 <EditItemTemplate>
                     <asp:TextBox ID="TxtbSerialNo" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:TextBox>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblOldSerialNumber" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS" />
             <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                 SortExpression="CLINT_NAME" ReadOnly="True" />
             <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" 
                 SortExpression="ACCNT_NO" ReadOnly="True" />
             <asp:TemplateField HeaderText="Customer Mobile No" 
                 SortExpression="CUSTOMER_MOBILE_NO"  >
                 <EditItemTemplate>
                     <asp:TextBox ID="TxtbCustomerMobile" runat="server"  ReadOnly="true"
                         Text='<%# Bind("CUSTOMER_MOBILE_NO") %>'></asp:TextBox>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblCustMob" runat="server"  Text='<%# Bind("CUSTOMER_MOBILE_NO") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="AGENT_MOBILE_NO" HeaderText="Agent Mobile No" 
                 SortExpression="AGENT_MOBILE_NO" ReadOnly="True" />
            </Columns>
             <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
          </asp:GridView>
            </div>
            <div id="div1" style="padding-left: 5px;" runat="server" visible="false">
            
             <div style="background-color: royalblue; text-align: left;">
                <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">&nbsp;&nbsp;Search Available Serial No&nbsp;
                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                
                 <asp:Label ID="lblAvlbl" runat="server" Text="" ForeColor="White" Font-Bold="true">
                 </asp:Label>               
            </div>             
                <table cellpadding="0" cellspacing="4">
                    <tr>
                        <td align="right">
                            <span style="font-size: 12px; font-weight: normal;">New Serial No: </span>
                        </td>                        
                        <td align="left">
                            <asp:TextBox ID="txtbAvailabl" runat="server"></asp:TextBox>
                        </td>                        
                        <td align="right">
                            <asp:Button ID="BtnSearchAvlb" runat="server" Text="Search Available Number" 
                                onclick="BtnSearchAvlb_Click"  />
                        </td>
                         <td align="right">
                            <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                 onclick="BtnUpdate_Click" Visible="False" />
                        </td>
                    </tr>
                </table>                        
                     
                <asp:GridView ID="gdvAvailable" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                    DataSourceID="sdsAvailable"  CssClass="mGrid"  Width="850px" GridLines="None"
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
                    <Columns>
                        <asp:TemplateField HeaderText="Form Serial No" SortExpression="SERIAL_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNewSerialNumber" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STATUS" HeaderText="Status" 
                            SortExpression="STATUS" />
                        <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                 SortExpression="CLINT_NAME" ReadOnly="True" />
             <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" 
                 SortExpression="ACCNT_NO" ReadOnly="True" />    
                        <asp:BoundField DataField="CUSTOMER_MOBILE_NO" HeaderText="Customer Mobile No" 
                            SortExpression="CUSTOMER_MOBILE_NO" />
                        <asp:BoundField DataField="AGENT_MOBILE_NO" HeaderText="Agent Mobile No" 
                            SortExpression="AGENT_MOBILE_NO"/>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            
            </div>            
            </ContentTemplate>
            </asp:UpdatePanel>
    
    </form>
</body>
</html>
