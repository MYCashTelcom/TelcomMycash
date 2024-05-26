<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmInsertFormSerial.aspx.cs" Inherits="COMMON_frmInsertFormSerial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PIN Information</title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />
     <style type="text/css">
       .Label
       {
       	text-align:center;
       	font-weight:bold;
       	font-size:12px;
       	display:inline;
       	}
      input[type="submit"]        
       	{
        height:30px;
        width:80px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
   <asp:ScriptManager id="SCManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UDPanel" runat="server">
        <contenttemplate>
        
            <asp:SqlDataSource ID="sdsAccntSerialDetail" runat="server" 
               ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"                 
              
                SelectCommand="SELECT &quot;ACCNT_SL_DETIL_ID&quot;, &quot;ACCNT_SL_MSTR_ID&quot;, &quot;SERIAL_NO&quot;, &quot;STATUS&quot;, &quot;CUSTOMER_MOBILE_NO&quot;, &quot;AGENT_MOBILE_NO&quot; FROM &quot;ACCOUNT_SERIAL_DETAIL&quot;" 
                DeleteCommand="DELETE FROM &quot;ACCOUNT_SERIAL_DETAIL&quot; WHERE &quot;ACCNT_SL_DETIL_ID&quot; = :ACCNT_SL_DETIL_ID" 
                InsertCommand="INSERT INTO &quot;ACCOUNT_SERIAL_DETAIL&quot; (&quot;ACCNT_SL_DETIL_ID&quot;, &quot;ACCNT_SL_MSTR_ID&quot;, &quot;SERIAL_NO&quot;, &quot;STATUS&quot;, &quot;CUSTOMER_MOBILE_NO&quot;, &quot;AGENT_MOBILE_NO&quot;) VALUES (:ACCNT_SL_DETIL_ID, :ACCNT_SL_MSTR_ID, :SERIAL_NO, :STATUS, :CUSTOMER_MOBILE_NO, :AGENT_MOBILE_NO)" 
                UpdateCommand="UPDATE &quot;ACCOUNT_SERIAL_DETAIL&quot; SET &quot;ACCNT_SL_MSTR_ID&quot; = :ACCNT_SL_MSTR_ID, &quot;SERIAL_NO&quot; = :SERIAL_NO, &quot;STATUS&quot; = :STATUS, &quot;CUSTOMER_MOBILE_NO&quot; = :CUSTOMER_MOBILE_NO, &quot;AGENT_MOBILE_NO&quot; = :AGENT_MOBILE_NO WHERE &quot;ACCNT_SL_DETIL_ID&quot; = :ACCNT_SL_DETIL_ID">
                <DeleteParameters>
                    <asp:Parameter Name="ACCNT_SL_DETIL_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ACCNT_SL_MSTR_ID" Type="String" />
                    <asp:Parameter Name="SERIAL_NO" Type="String" />
                    <asp:Parameter Name="STATUS" Type="String" />
                    <asp:Parameter Name="CUSTOMER_MOBILE_NO" Type="String" />
                    <asp:Parameter Name="AGENT_MOBILE_NO" Type="String" />
                    <asp:Parameter Name="ACCNT_SL_DETIL_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ACCNT_SL_DETIL_ID" Type="String" />
                    <asp:Parameter Name="ACCNT_SL_MSTR_ID" Type="String" />
                    <asp:Parameter Name="SERIAL_NO" Type="String" />
                    <asp:Parameter Name="STATUS" Type="String" />
                    <asp:Parameter Name="CUSTOMER_MOBILE_NO" Type="String" />
                    <asp:Parameter Name="AGENT_MOBILE_NO" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
             <asp:SqlDataSource ID="sdsShowData" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT ASD.AGENT_MOBILE_NO, CL.CLINT_NAME, ASD.CUSTOMER_MOBILE_NO, AL.ACCNT_NO, AR.RANK_TITEL, 
                ASD.SERIAL_NO, SP.SERVICE_PKG_NAME FROM CLIENT_LIST CL, ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_RANK AR, 
                SERVICE_PACKAGE SP WHERE CL.CLINT_ID = AL.CLINT_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO 
                AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND (ASD.SERIAL_NO = '')">
             </asp:SqlDataSource>
        <div style="BACKGROUND-COLOR: royalblue">
           
         <strong>
          <span style="COLOR: white">&nbsp;&nbsp;&nbsp;&nbsp;Manage Form Serial No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>&nbsp;
          </span>
         </strong>
        </div>
        <div>
           <asp:Label ID="Label1" runat="server" Text="Search By Form SL No"></asp:Label>
           <asp:TextBox ID="txtWallet" runat="server"></asp:TextBox>&nbsp; &nbsp; &nbsp;             
           <asp:Button ID="btnView" runat="server"  Text="  Search  "  onclick="btnView_Click" /> <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtWallet"
           ErrorMessage="Please insert form serial No.">
           </asp:RequiredFieldValidator>             
        </div>
            
        <fieldset id="flsFormSerialNo" runat="server" style="border-color: #FFFFFF; width:350px;height:auto;" visible="false">
         <legend>Insert Form Serial No</legend>
            <table>
                <tr>
                  <td>
                      <asp:Label ID="lblCustMobNo" runat="server" Text="Customer Mobile No"></asp:Label>
                  </td>
                  <td >
                      <asp:TextBox ID="txtCustMobNo" runat="server">+88</asp:TextBox>
                  </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAgntMobNo" runat="server" Text="Agent Mobile No"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtAgentMobileNo" runat="server">+88</asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <td>
                   </td>
                   <td>
                     <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                   </td>                    
                </tr>
            </table>
            <asp:DetailsView ID="dtvShowData" runat="server" Height="200px" Width="300px" 
                AutoGenerateRows="False" DataSourceID="sdsShowData" Visible="false"
                 CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                  <PagerStyle CssClass="pgr" />
                <Fields>
                    <asp:BoundField DataField="AGENT_MOBILE_NO" HeaderText="Agent Mobile No" 
                        SortExpression="AGENT_MOBILE_NO" />
                    <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                        SortExpression="CLINT_NAME" />
                    <asp:BoundField DataField="CUSTOMER_MOBILE_NO" HeaderText="Customer Mobile No" 
                        SortExpression="CUSTOMER_MOBILE_NO" />
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" 
                        SortExpression="ACCNT_NO" />
                    <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank Title" 
                        SortExpression="RANK_TITEL" />
                    <asp:BoundField DataField="SERIAL_NO" HeaderText="Serial No" 
                        SortExpression="SERIAL_NO" />
                    <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Package Name" 
                        SortExpression="SERVICE_PKG_NAME" />
                </Fields>
                 <AlternatingRowStyle CssClass="alt" />
            </asp:DetailsView>
          </fieldset>       
        </contenttemplate>
    </asp:UpdatePanel>
  </form>
</body>
</html>
