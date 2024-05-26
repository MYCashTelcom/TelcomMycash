<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngChannelType.aspx.cs" Inherits="COMMON_frmMngChannelType" Title="Manage Channel Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
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
         	width:817px;         	
         	}	
         .Inser_Panel	
         {
         	 width:100%;
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         	 font-size:12px;
         }
        .style1
        {
            width: 161px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>

         <asp:SqlDataSource ID="sdsBranch" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH">
        </asp:SqlDataSource>
        
         <asp:SqlDataSource ID="sdsBankList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   SelectCommand="SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST ORDER BY BANK_NAME">
        </asp:SqlDataSource>
        
         
        <asp:SqlDataSource id="sdsChannelType" runat="server" 
            DeleteCommand='DELETE FROM "CHANNEL_TYPE" WHERE "CHANNEL_TYPE_ID" = :CHANNEL_TYPE_ID' 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            
            InsertCommand='INSERT INTO "CHANNEL_TYPE" ("CHANNEL_TYPE_ID", "CHANNEL_TYPE_NAME","CMP_BRANCH_ID", "CHANNEL_TYPE","BANK_CODE", "CHANNEL_ACCOUNT_ID") VALUES (:CHANNEL_TYPE_ID, :CHANNEL_TYPE_NAME,:CMP_BRANCH_ID, :CHANNEL_TYPE,:BANK_CODE, :CHANNEL_ACCOUNT_ID)' 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            
            UpdateCommand='UPDATE "CHANNEL_TYPE" SET "CHANNEL_TYPE_NAME" = :CHANNEL_TYPE_NAME, "CHANNEL_TYPE" = :CHANNEL_TYPE,"BANK_CODE"=:BANK_CODE, "CHANNEL_ACCOUNT_ID" = :CHANNEL_ACCOUNT_ID WHERE "CHANNEL_TYPE_ID" = :CHANNEL_TYPE_ID' 
            
             SelectCommand='SELECT * FROM CHANNEL_TYPE WHERE (CMP_BRANCH_ID=:CMP_BRANCH_ID) ORDER BY CHANNEL_TYPE_NAME'>
        
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        
        <DeleteParameters>
           <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />
        </DeleteParameters>
       
        <UpdateParameters>
            <asp:Parameter Name="CHANNEL_TYPE_NAME" Type="String" />
            <asp:Parameter Name="CHANNEL_TYPE" Type="String" />
             <asp:Parameter Name="BANK_CODE" Type="String" />
            <asp:Parameter Name="CHANNEL_ACCOUNT_ID" Type="String" />
            <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />
            <asp:Parameter Name="CHANNEL_TYPE_NAME" Type="String" />
            <asp:Parameter Name="BANK_CODE" Type="String" />
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" />
            <asp:Parameter Name="CHANNEL_TYPE" Type="String" />
            <asp:Parameter Name="CHANNEL_ACCOUNT_ID" Type="String" />
        </InsertParameters>
        </asp:SqlDataSource>

   <asp:Panel ID="pnlTop" runat="server">
     <table width="100%" class="Top_Panel">
      <tr>
        <td align="left" class="style1">         
          Manage Channel Type 
        </td>
        <td align="left">
          Branch
            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" 
                onselectedindexchanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td align="left">
          <asp:Label ID="lblMsg" runat="server" ></asp:Label>
        </td>
        <td align="left">
          <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
        </td>
      </tr>
     </table>
    </asp:Panel> 
    <asp:Panel ID="Panel1" runat="server" >
        
        <asp:GridView ID="gdvChannelType" runat="server" Width="650px" AllowPaging="True"  GridLines="None" BorderStyle="None"
                AutoGenerateColumns="False" DataKeyNames="CHANNEL_TYPE_ID" DataSourceID="sdsChannelType"
                BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                 AlternatingRowStyle-CssClass="alt" 
            onrowupdated="gdvChannelType_RowUpdated" 
            onrowupdating="gdvChannelType_RowUpdating" >
          <Columns>             
             
            <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID" ReadOnly="True"
                SortExpression="CHANNEL_TYPE_ID" Visible="false" />
            <asp:BoundField DataField="CHANNEL_TYPE_NAME" HeaderText="Channel Type Name" 
                  SortExpression="CHANNEL_TYPE_NAME">
            </asp:BoundField>
            <asp:BoundField DataField="CHANNEL_TYPE" HeaderText="Channel Type" 
                  SortExpression="CHANNEL_TYPE">
            </asp:BoundField>
            <asp:BoundField DataField="CHANNEL_ACCOUNT_ID" HeaderText="Commission Account ID" 
                  SortExpression="CHANNEL_ACCOUNT_ID" />
           <asp:TemplateField HeaderText="Bank List" SortExpression="BANK_CODE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBank" runat="server"   AppendDataBoundItems="true"
                            DataSourceID="sdsBankList" DataTextField="BANK_NAME" 
                            DataValueField="BANK_INTERNAL_CODE" SelectedValue='<%# Eval("BANK_CODE") %>'>
                          <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server"  AppendDataBoundItems="true"
                            DataSourceID="sdsBankList" DataTextField="BANK_NAME"  Enabled="false"
                            DataValueField="BANK_INTERNAL_CODE" SelectedValue='<%# Eval("BANK_CODE") %>'>
                          <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
            </asp:TemplateField>
                      
            <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                       <ajaxToolkit:ConfirmButtonExtender ID="cbeUpdate" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                             TargetControlID="btnUpdate">
                         </ajaxToolkit:ConfirmButtonExtender>  
                         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnUpdateCancel" 
                             OkControlID="btnUpdateOK" TargetControlID="btnUpdate" 
                             PopupControlID="pnlPopUpUpdate">
                         </ajaxToolkit:ModalPopupExtender>      
                            
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>    
        </Columns>        
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
      </asp:GridView>
      <asp:Panel ID="pnlPopUpUpdate" runat="server"  style=" display:none;width:300px; height:165px;
                  background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
              <div style="text-align:center; height:95px;"><br />&nbsp;<br />&nbsp;
              Are you sure you want to update?
                 <br />&nbsp;<br />&nbsp;
              </div>
              <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="btnUpdateOK" runat="server" Text="  Yes  " />
                    <asp:Button ID="btnUpdateCancel" runat="server" Text=" Cancel " />
              </div>
        </asp:Panel>
    </asp:Panel>
    
    <asp:Panel ID="Panel2" runat="server">
     <table class="Inser_Panel">
      <tr>
       <td>
           Add New Channel
       </td>
      </tr>
     </table>               
       <asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsChannelType" 
                BorderColor="Silver" Height="50px" Width="350px" AutoGenerateRows="False" 
                DefaultMode="Insert" DataKeyNames="CHANNEL_TYPE_ID"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        BorderStyle="None" 
            oniteminserted="dlvServiceType_ItemInserted">
           <PagerStyle CssClass="pgr" />
          <Fields>               
                <asp:BoundField DataField="CHANNEL_TYPE_NAME" 
                    SortExpression="CHANNEL_TYPE_NAME" HeaderText="Channel Name">
                </asp:BoundField>
                <asp:BoundField DataField="CHANNEL_TYPE" SortExpression="CHANNEL_TYPE" 
                    HeaderText="Channel Type">
                </asp:BoundField>
                <asp:BoundField DataField="CHANNEL_ACCOUNT_ID" HeaderText="Commission Account ID" 
                    SortExpression="CHANNEL_ACCOUNT_ID" />
                <asp:TemplateField HeaderText="Bank List" SortExpression="BANK_CODE">
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BANK_CODE") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <InsertItemTemplate>
                       <asp:DropDownList ID="ddlBankList" runat="server" 
                           DataSourceID="sdsBankList" DataTextField="BANK_NAME" 
                           DataValueField="BANK_INTERNAL_CODE"
                           SelectedValue='<%# Bind("BANK_CODE") %>'>
                       </asp:DropDownList>
                   </InsertItemTemplate>
                   <ItemTemplate>
                       <asp:DropDownList ID="DropDownList1" runat="server" 
                           DataSourceID="sdsBankList" DataTextField="BANK_NAME" 
                           DataValueField="BANK_INTERNAL_CODE"
                            SelectedValue='<%# Bind("BANK_CODE") %>'>
                       </asp:DropDownList>
                   </ItemTemplate>
               </asp:TemplateField>    
                <asp:TemplateField ShowHeader="False">
                    <InsertItemTemplate>
                        <asp:Button ID="btnInsert" runat="server" CausesValidation="True" 
                            CommandName="Insert" Text="Insert" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                     <ajaxToolkit:ConfirmButtonExtender ID="cbeInsert" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender3" onclientcancel="cancelClick" 
                             TargetControlID="btnInsert" >
                         </ajaxToolkit:ConfirmButtonExtender>
                          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnCancel" 
                             OkControlID="btnOK" TargetControlID="btnInsert" 
                             PopupControlID="pnlPopUpInsert">
                         </ajaxToolkit:ModalPopupExtender>         
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                            CommandName="New" Text="New" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
           <AlternatingRowStyle CssClass="alt" />
          </asp:DetailsView> 
          <asp:Panel ID="pnlPopUpInsert" runat="server"  style=" display:none;width:300px; height:165px;
                 background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
              <div style="text-align:center; height:95px;"><br />&nbsp;<br />&nbsp;
               Are you sure you want to save?
                 <br />&nbsp;<br />&nbsp;
              </div>
              <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                <br />&nbsp;
                <asp:Button ID="btnOK" runat="server" Text="  Yes  " />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " />
              </div>
           </asp:Panel>       
        </asp:Panel>             
       </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
