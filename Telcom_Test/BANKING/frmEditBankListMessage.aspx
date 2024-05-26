<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEditBankListMessage.aspx.cs" Inherits="BANKING_frmEditBankListMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Message</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">       
        .Top_Panel
             {
         	background-color: royalblue;          	
         	height:24px;
         	font-weight:bold;
         	font-size:12px;
         	color:White;         	
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	        	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
        .style1
        {
            width: 164px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:SqlDataSource ID="sdsBranch" runat="server"
           ConnectionString="<%$ ConnectionStrings:oracleConString %>"
           ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
           SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH ORDER BY CMP_BRANCH_NAME ASC">
         </asp:SqlDataSource>
         <asp:SqlDataSource ID="sdsBankList" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             DeleteCommand="DELETE FROM &quot;BANK_LIST&quot; WHERE &quot;BANK_ID&quot; = :BANK_ID" 
             InsertCommand="INSERT INTO &quot;BANK_LIST&quot; (&quot;BANK_ID&quot;, &quot;BANK_NAME&quot;, &quot;BANK_ADDRESS&quot;, &quot;BANK_INTERNAL_CODE&quot;, &quot;BANK_SWIFT_CODE&quot;, &quot;BANK_CB_CODE&quot;, &quot;BANK_SATLE_AC&quot;, &quot;BANK_IS_SATLMNT_BANK&quot;, &quot;BANK_STATUS&quot;, &quot;BANK_BIN&quot;, &quot;BANK_ACC_WELCOME_MSG&quot;, &quot;BANK_ACC_ACTIVE_MSG&quot;, &quot;BANK_ACC_DEACTIVE_MSG&quot;, &quot;BANK_ACC_RESET_PIN_MEG&quot;, &quot;CMP_BRANCH_ID&quot;, &quot;BANK_SIGNATURE&quot;, &quot;BANK_DPS_OPENING_SMS&quot;, &quot;BANK_DPS_ADJUSTMENT_SMS&quot;, &quot;BANK_DPS_OVER_DUE_SMS&quot;, &quot;BANK_DPS_INST_ALERT_SMS&quot;, &quot;BANK_DPS_CLOSE_SMS&quot;, &quot;BANK_DPS_AGENT_COMI_SMS&quot;, &quot;BANK_DPS_TEMPCOLSE_SMS&quot;, &quot;BANK_TOPUP_FAILED_SMS&quot;, &quot;BANK_TOPUP_SUCCESS_SMS&quot;) VALUES (:BANK_ID, :BANK_NAME, :BANK_ADDRESS, :BANK_INTERNAL_CODE, :BANK_SWIFT_CODE, :BANK_CB_CODE, :BANK_SATLE_AC, :BANK_IS_SATLMNT_BANK, :BANK_STATUS, :BANK_BIN, :BANK_ACC_WELCOME_MSG, :BANK_ACC_ACTIVE_MSG, :BANK_ACC_DEACTIVE_MSG, :BANK_ACC_RESET_PIN_MEG, :CMP_BRANCH_ID, :BANK_SIGNATURE, :BANK_DPS_OPENING_SMS, :BANK_DPS_ADJUSTMENT_SMS, :BANK_DPS_OVER_DUE_SMS, :BANK_DPS_INST_ALERT_SMS, :BANK_DPS_CLOSE_SMS, :BANK_DPS_AGENT_COMI_SMS, :BANK_DPS_TEMPCOLSE_SMS, :BANK_TOPUP_FAILED_SMS, :BANK_TOPUP_SUCCESS_SMS)" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT * FROM BANK_LIST WHERE BANK_STATUS='A' AND CMP_BRANCH_ID=:CMP_BRANCH_ID" 
             
             UpdateCommand="UPDATE &quot;BANK_LIST&quot; SET &quot;BANK_NAME&quot; = :BANK_NAME, &quot;BANK_INTERNAL_CODE&quot; = :BANK_INTERNAL_CODE, &quot;BANK_BIN&quot; = :BANK_BIN, &quot;BANK_ACC_WELCOME_MSG&quot; = :BANK_ACC_WELCOME_MSG, &quot;BANK_ACC_ACTIVE_MSG&quot; = :BANK_ACC_ACTIVE_MSG, &quot;BANK_ACC_DEACTIVE_MSG&quot; = :BANK_ACC_DEACTIVE_MSG, &quot;BANK_ACC_RESET_PIN_MEG&quot; = :BANK_ACC_RESET_PIN_MEG, &quot;BANK_SIGNATURE&quot; = :BANK_SIGNATURE, &quot;BANK_DPS_OPENING_SMS&quot; = :BANK_DPS_OPENING_SMS, &quot;BANK_DPS_ADJUSTMENT_SMS&quot; = :BANK_DPS_ADJUSTMENT_SMS, &quot;BANK_DPS_OVER_DUE_SMS&quot; = :BANK_DPS_OVER_DUE_SMS, &quot;BANK_DPS_INST_ALERT_SMS&quot; = :BANK_DPS_INST_ALERT_SMS, &quot;BANK_DPS_CLOSE_SMS&quot; = :BANK_DPS_CLOSE_SMS, &quot;BANK_DPS_AGENT_COMI_SMS&quot; = :BANK_DPS_AGENT_COMI_SMS, &quot;BANK_DPS_TEMPCOLSE_SMS&quot; = :BANK_DPS_TEMPCOLSE_SMS, &quot;BANK_TOPUP_FAILED_SMS&quot; = :BANK_TOPUP_FAILED_SMS, &quot;BANK_TOPUP_SUCCESS_SMS&quot; = :BANK_TOPUP_SUCCESS_SMS WHERE &quot;BANK_ID&quot; = :BANK_ID">
             <SelectParameters>
                <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" 
                        PropertyName="SelectedValue" Type="String" /> 
             </SelectParameters>
             <DeleteParameters>
                 <asp:Parameter Name="BANK_ID" Type="String" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="BANK_NAME" Type="String" />                
                 
                 <asp:Parameter Name="BANK_INTERNAL_CODE" Type="String" />
                
                 <asp:Parameter Name="BANK_BIN" Type="String" />
                 <asp:Parameter Name="BANK_ACC_WELCOME_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_ACTIVE_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_DEACTIVE_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_RESET_PIN_MEG" Type="String" />
                 
                 <asp:Parameter Name="BANK_SIGNATURE" Type="String" />
                 <asp:Parameter Name="BANK_DPS_OPENING_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_ADJUSTMENT_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_OVER_DUE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_INST_ALERT_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_CLOSE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_AGENT_COMI_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_TEMPCOLSE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_TOPUP_FAILED_SMS" Type="String" />
                 <asp:Parameter Name="BANK_TOPUP_SUCCESS_SMS" Type="String" />
                 <asp:Parameter Name="BANK_ID" Type="String" />
             </UpdateParameters>
             <InsertParameters>
                 <asp:Parameter Name="BANK_ID" Type="String" />
                 <asp:Parameter Name="BANK_NAME" Type="String" />
                 <asp:Parameter Name="BANK_ADDRESS" Type="String" />
                 <asp:Parameter Name="BANK_INTERNAL_CODE" Type="String" />
                 <asp:Parameter Name="BANK_SWIFT_CODE" Type="String" />
                 <asp:Parameter Name="BANK_CB_CODE" Type="String" />
                 <asp:Parameter Name="BANK_SATLE_AC" Type="String" />
                 <asp:Parameter Name="BANK_IS_SATLMNT_BANK" Type="String" />
                 <asp:Parameter Name="BANK_STATUS" Type="String" />
                 <asp:Parameter Name="BANK_BIN" Type="String" />
                 <asp:Parameter Name="BANK_ACC_WELCOME_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_ACTIVE_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_DEACTIVE_MSG" Type="String" />
                 <asp:Parameter Name="BANK_ACC_RESET_PIN_MEG" Type="String" />
                 <asp:Parameter Name="CMP_BRANCH_ID" Type="String" />
                 <asp:Parameter Name="BANK_SIGNATURE" Type="String" />
                 <asp:Parameter Name="BANK_DPS_OPENING_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_ADJUSTMENT_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_OVER_DUE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_INST_ALERT_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_CLOSE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_AGENT_COMI_SMS" Type="String" />
                 <asp:Parameter Name="BANK_DPS_TEMPCOLSE_SMS" Type="String" />
                 <asp:Parameter Name="BANK_TOPUP_FAILED_SMS" Type="String" />
                 <asp:Parameter Name="BANK_TOPUP_SUCCESS_SMS" Type="String" />
             </InsertParameters>
         </asp:SqlDataSource>
        <asp:Panel ID="pnlTop" runat="server" >
       <table class="Top_Panel" width="100%">
        <tr>
         <td class="style1">
            Manage Bank Message
         </td>
         <td align="left">
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Branch 
            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID">
            </asp:DropDownList>
         </td>
         <td>
         </td>
         <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
         </td>
         <td>
           <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
         </td>
         </tr>
         </table>
         </asp:Panel>
         <asp:Panel ID="Panel1" runat="server">
             <asp:GridView ID="gdvBankInfo" runat="server" DataKeyNames="BANK_ID" DataSourceID="sdsBankList" 
             AllowPaging="True"  AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" onrowediting="gdvBankInfo_RowEditing">
                 <Columns>
                     
                     <asp:BoundField DataField="BANK_ID" HeaderText="BANK_ID" ReadOnly="True" 
                         SortExpression="BANK_ID" Visible="false" />
                     <asp:TemplateField HeaderText="Bank Name" SortExpression="BANK_NAME">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("BANK_NAME") %>' Enabled="false"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label14" runat="server" Text='<%# Bind("BANK_NAME") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bank Internal Code" 
                         SortExpression="BANK_INTERNAL_CODE">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox15" runat="server" 
                                 Text='<%# Bind("BANK_INTERNAL_CODE") %>' Enabled="false"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label15" runat="server" Text='<%# Bind("BANK_INTERNAL_CODE") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bank Bin" SortExpression="BANK_BIN">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("BANK_BIN") %>' Enabled="false"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label16" runat="server" Text='<%# Bind("BANK_BIN") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Welcome Msg" SortExpression="BANK_ACC_WELCOME_MSG">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox12" runat="server" 
                                 Text='<%# Bind("BANK_ACC_WELCOME_MSG") %>' Height="120px"  TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label12" runat="server" 
                                 Text='<%# Bind("BANK_ACC_WELCOME_MSG") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Active Msg" SortExpression="BANK_ACC_ACTIVE_MSG">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox13" runat="server" 
                                 Text='<%# Bind("BANK_ACC_ACTIVE_MSG") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label13" runat="server" 
                                 Text='<%# Bind("BANK_ACC_ACTIVE_MSG") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DeActive Msg" 
                         SortExpression="BANK_ACC_DEACTIVE_MSG">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox11" runat="server" 
                                 Text='<%# Bind("BANK_ACC_DEACTIVE_MSG") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label11" runat="server" 
                                 Text='<%# Bind("BANK_ACC_DEACTIVE_MSG") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Reset PIN Msg" 
                         SortExpression="BANK_ACC_RESET_PIN_MEG">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox10" runat="server" 
                                 Text='<%# Bind("BANK_ACC_RESET_PIN_MEG") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label10" runat="server" 
                                 Text='<%# Bind("BANK_ACC_RESET_PIN_MEG") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="BANK_SIGNATURE" HeaderText="Signature" 
                         SortExpression="BANK_SIGNATURE"   />
                    
                     <asp:TemplateField HeaderText="DPS Opening SMS" 
                         SortExpression="BANK_DPS_OPENING_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox9" runat="server" 
                                 Text='<%# Bind("BANK_DPS_OPENING_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label9" runat="server" 
                                 Text='<%# Bind("BANK_DPS_OPENING_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DPS Adjustment SMS" 
                         SortExpression="BANK_DPS_ADJUSTMENT_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox8" runat="server" 
                                 Text='<%# Bind("BANK_DPS_ADJUSTMENT_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label8" runat="server" 
                                 Text='<%# Bind("BANK_DPS_ADJUSTMENT_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Over Due SMS" 
                         SortExpression="BANK_DPS_OVER_DUE_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox7" runat="server" 
                                 Text='<%# Bind("BANK_DPS_OVER_DUE_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label7" runat="server" 
                                 Text='<%# Bind("BANK_DPS_OVER_DUE_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DPS Inst Alert SMS" 
                         SortExpression="BANK_DPS_INST_ALERT_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox6" runat="server" 
                                 Text='<%# Bind("BANK_DPS_INST_ALERT_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label6" runat="server" 
                                 Text='<%# Bind("BANK_DPS_INST_ALERT_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DPS Close SMS" 
                         SortExpression="BANK_DPS_CLOSE_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox5" runat="server" 
                                 Text='<%# Bind("BANK_DPS_CLOSE_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label5" runat="server" Text='<%# Bind("BANK_DPS_CLOSE_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DPS Agent Commi SMS" 
                         SortExpression="BANK_DPS_AGENT_COMI_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox4" runat="server" 
                                 Text='<%# Bind("BANK_DPS_AGENT_COMI_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label4" runat="server" 
                                 Text='<%# Bind("BANK_DPS_AGENT_COMI_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="DPS TempClose SMS" 
                         SortExpression="BANK_DPS_TEMPCOLSE_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox3" runat="server" 
                                 Text='<%# Bind("BANK_DPS_TEMPCOLSE_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label3" runat="server" 
                                 Text='<%# Bind("BANK_DPS_TEMPCOLSE_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Topup Failed SMS" 
                         SortExpression="BANK_TOPUP_FAILED_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox2" runat="server" 
                                 Text='<%# Bind("BANK_TOPUP_FAILED_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label2" runat="server" 
                                 Text='<%# Bind("BANK_TOPUP_FAILED_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Topup Success SMS" 
                         SortExpression="BANK_TOPUP_SUCCESS_SMS">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" 
                                 Text='<%# Bind("BANK_TOPUP_SUCCESS_SMS") %>' Height="120px" TextMode="MultiLine"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label1" runat="server" 
                                 Text='<%# Bind("BANK_TOPUP_SUCCESS_SMS") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:CommandField ShowEditButton="True" ButtonType="Button" />    
                 </Columns>
                 <PagerStyle CssClass="pgr" />
                 <AlternatingRowStyle CssClass="alt" />
             </asp:GridView>
         </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
