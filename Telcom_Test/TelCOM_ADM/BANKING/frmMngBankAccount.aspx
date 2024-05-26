<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankAccount.aspx.cs"
    Inherits="COMMON_frmMngBankAccount" Title="Manage Client Account" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mng Admin Wallet</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
       .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
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
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
                        
        <asp:SqlDataSource ID="sdsClientBankAccnt" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            
            DeleteCommand='DELETE FROM "CLIENT_BANK_ACCOUNT" WHERE "CLINT_BANK_ACC_ID" = :CLINT_BANK_ACC_ID' 
            InsertCommand='INSERT INTO "CLIENT_BANK_ACCOUNT" ("CLINT_BANK_ACC_ID", "CLINT_BANK_ACC_NO", "CLINT_BANK_ACC_LOGIN", "CLINT_BANK_ACC_PASS", "BANK_ID", "ACCNT_ID", "CLINT_BANK_ACC_STATE") VALUES (:CLINT_BANK_ACC_ID, :CLINT_BANK_ACC_NO, :CLINT_BANK_ACC_LOGIN, :CLINT_BANK_ACC_PASS, :BANK_ID, :ACCNT_ID, :CLINT_BANK_ACC_STATE)'
            UpdateCommand='UPDATE CLIENT_BANK_ACCOUNT  SET  CLINT_BANK_ACC_LOGIN = :CLINT_BANK_ACC_LOGIN  WHERE (CLINT_BANK_ACC_ID = :CLINT_BANK_ACC_ID)'>
            <DeleteParameters>
                <asp:Parameter Name="CLINT_BANK_ACC_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
               
                <asp:Parameter Name="CLINT_BANK_ACC_LOGIN" Type="String" />                
                <asp:Parameter Name="CLINT_BANK_ACC_ID" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="CLINT_BANK_ACC_ID" Type="String" />
                <asp:Parameter Name="CLINT_BANK_ACC_NO" Type="String" />
                <asp:Parameter Name="CLINT_BANK_ACC_LOGIN" Type="String" />
                <asp:Parameter Name="CLINT_BANK_ACC_PASS" Type="String" />
                <asp:Parameter Name="BANK_ID" Type="String" />
                <asp:Parameter Name="ACCNT_ID" Type="String" />
                <asp:Parameter Name="CLINT_BANK_ACC_STATE" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        
        <asp:Panel ID="Panel1" runat="server">
         <table width="100%" class="Top_Panel">
          <tr>
           <td>
             Manage Admin Account
           </td>
           <td></td>
           <td></td>
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
        <asp:Panel ID="Panel2" runat="server">
         <table cellpadding="0" cellspacing="4" class="View_Panel">
          
          <tr>
            <td align="right">
                <span style="font-size: 12px; font-weight: bold;">Search by Wallet ID: </span>
            </td>
            <td>
            </td>
            <td align="left">
                <asp:TextBox ID="txtWalletNo" runat="server"></asp:TextBox>
            </td>
           </tr>           
           <tr>
            <td colspan="3" align="right">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
           </tr>                                
        </table>        
       </asp:Panel>     
        <asp:GridView ID="gdvSearch" runat="server" AllowPaging="True"  Width="850px"
            AllowSorting="True" AlternatingRowStyle-CssClass="alt"  BorderStyle="None" GridLines="None"
            AutoGenerateColumns="False" BorderColor="#E0E0E0" CssClass="mGrid" 
            DataKeyNames="CLINT_BANK_ACC_ID" DataSourceID="sdsClientBankAccnt" 
            OnPageIndexChanging="gdvSearch_PageIndexChanging" 
            OnRowCancelingEdit="gdvSearch_RowCancelingEdit" 
            OnRowEditing="gdvSearch_RowEditing" OnRowUpdated="gdvSearch_RowUpdated" 
             PagerStyle-CssClass="pgr" PageSize="7" 
            Visible="False" onrowupdating="gdvSearch_RowUpdating">
            <Columns>                                
                <asp:BoundField DataField="CLINT_BANK_ACC_ID" HeaderText="CLINT_BANK_ACC_ID" 
                    SortExpression="CLINT_BANK_ACC_ID" ReadOnly="true" Visible="false"  />
                <asp:TemplateField HeaderText="Client Name" SortExpression="CLINT_NAME">
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("CLINT_NAME") %>' Enabled="false"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CLINT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wallet ID" SortExpression="CLINT_BANK_ACC_NO">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" 
                            Text='<%# Bind("CLINT_BANK_ACC_NO") %>' Enabled="false"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CLINT_BANK_ACC_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GL/FiS Account" 
                    SortExpression="CLINT_BANK_ACC_LOGIN">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" 
                            Text='<%# Bind("CLINT_BANK_ACC_LOGIN") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("CLINT_BANK_ACC_LOGIN") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" />
                     <ajaxToolkit:ConfirmButtonExtender ID="cbeNominiUpdate" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                             TargetControlID="btnUpdate">
                     </ajaxToolkit:ConfirmButtonExtender>  
                     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnUpdateCancel" 
                             OkControlID="btnUpdateOK" TargetControlID="btnUpdate" 
                             PopupControlID="pnlUpdate">
                     </ajaxToolkit:ModalPopupExtender>    
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pgr" />
            <HeaderStyle ForeColor="White" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>
                
         <asp:Panel ID="pnlUpdate" runat="server"  style=" display:none;width:300px; height:165px; 
             background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
          <div style="height:95px;"><br />&nbsp;<br />&nbsp;
          Are you sure you want to update?
             <br />&nbsp;<br />&nbsp;
          </div>
          <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                <br />&nbsp;
                <asp:Button ID="btnUpdateOK" runat="server" Text="  Yes  " />
                <asp:Button ID="btnUpdateCancel" runat="server" Text=" Cancel " />
          </div>
       </asp:Panel>           
    </ContentTemplate>
  </asp:UpdatePanel>
  </form>
</body>
</html>
