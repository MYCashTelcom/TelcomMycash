<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankSerFeeWave.aspx.cs" Inherits="COMMON_frmMngBankSerFeeWave" Title="Manage Channel Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
       
     </style>
</head>
<body style="background-color: lightgrey;"> 
    <form id="form1" runat="server">
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
         <asp:SqlDataSource id="sdsBankSerFeeWave" runat="server" 
            DeleteCommand='DELETE FROM "BANK_SERVICE_FEE_WAVE" WHERE "BNK_SRV_FEE_WAVE_ID" = :BNK_SRV_FEE_WAVE_ID' 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            InsertCommand='INSERT INTO "BANK_SERVICE_FEE_WAVE" ("BNK_SRV_FEE_WAVE_ID", "ACCNT_RANK_ID_SOURCE", "ACCNT_RANK_ID_DEST", "WAVE_AMOUNT", "SERVICE_ID", "CHANNEL_TYPE_ID","TRAN_ALLOWED","CMP_BRANCH_ID","BANK_CODE","HIERARCHY_ALLOWED") VALUES (:BNK_SRV_FEE_WAVE_ID, :ACCNT_RANK_ID_SOURCE, :ACCNT_RANK_ID_DEST, :WAVE_AMOUNT, :SERVICE_ID, :CHANNEL_TYPE_ID,:TRAN_ALLOWED,:CMP_BRANCH_ID,:BANK_CODE,:HIERARCHY_ALLOWED)' 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            
            UpdateCommand='UPDATE "BANK_SERVICE_FEE_WAVE" SET  "WAVE_AMOUNT" = :WAVE_AMOUNT, "CHANNEL_TYPE_ID" = :CHANNEL_TYPE_ID,"TRAN_ALLOWED"=:TRAN_ALLOWED,"HIERARCHY_ALLOWED"=:HIERARCHY_ALLOWED  WHERE "BNK_SRV_FEE_WAVE_ID" = :BNK_SRV_FEE_WAVE_ID' 
            SelectCommand='SELECT * FROM BANK_SERVICE_FEE_WAVE WHERE (CMP_BRANCH_ID=:CMP_BRANCH_ID AND BANK_CODE=:BANK_CODE AND SERVICE_ID=:SERVICE_ID AND ACCNT_RANK_ID_SOURCE=:ACCNT_RANK_ID_SOURCE AND ACCNT_RANK_ID_DEST=:ACCNT_RANK_ID_DEST)  ORDER BY CHANNEL_TYPE_ID'>
          
           <SelectParameters>
                    <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" Type="String" />  
                    <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" PropertyName="SelectedValue" />                         
                    <asp:ControlParameter ControlID="ddlSourceRank" Name="ACCNT_RANK_ID_SOURCE" PropertyName="SelectedValue" />                         
                    <asp:ControlParameter ControlID="ddlDestinationRank" Name="ACCNT_RANK_ID_DEST" PropertyName="SelectedValue" />                         
          </SelectParameters>
          <DeleteParameters>
            <asp:Parameter Name="BNK_SRV_FEE_WAVE_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <%--<asp:Parameter Name="ACCNT_RANK_ID_SOURCE" Type="String" />
            <asp:Parameter Name="ACCNT_RANK_ID_DEST" Type="String" />--%>
            <asp:Parameter Name="WAVE_AMOUNT" Type="String" />
           
            <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />
            <asp:Parameter Name="TRAN_ALLOWED" Type="String" />
            <asp:Parameter Name="HIERARCHY_ALLOWED" Type="String" /> 
            <asp:Parameter Name="BNK_SRV_FEE_WAVE_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="BNK_SRV_FEE_WAVE_ID" Type="String" />
           <%-- <asp:Parameter Name="ACCNT_RANK_ID_SOURCE" Type="String" />
            <asp:Parameter Name="ACCNT_RANK_ID_DEST" Type="String" />--%>
            <asp:Parameter Name="WAVE_AMOUNT" Type="String" />
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlSourceRank" Name="ACCNT_RANK_ID_SOURCE" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlDestinationRank" Name="ACCNT_RANK_ID_DEST" PropertyName="SelectedValue" />           
            <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />
            <asp:Parameter Name="TRAN_ALLOWED" Type="String" />
            <asp:Parameter Name="HIERARCHY_ALLOWED" Type="String" /> 
            
        </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsBranch" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   
                SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH ORDER BY CMP_BRANCH_NAME">
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="sdsBankList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   
                SelectCommand="SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="sdsAccntRank" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            
            SelectCommand="SELECT ACCNT_RANK_ID,RANK_TITEL FROM ACCOUNT_RANK WHERE STATUS='A' ORDER BY RANK_TITEL"></asp:SqlDataSource>

        <asp:SqlDataSource ID="sdstransAllowed" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT &quot;TRAN_ALLOWED&quot; FROM &quot;BANK_SERVICE_FEE_WAVE&quot;">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="sdsServiceList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE "></asp:SqlDataSource>

        <asp:SqlDataSource ID="sdsChannelType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE_NAME"></asp:SqlDataSource>

   
   
   <asp:Panel ID="pnlTop" runat="server">
     <table width="100%" class="Top_Panel">
      <tr>
        <td align="left" class="style1">         
          Manage Bank Service Fee Waive
        </td>
        <td align="left">
            Branch&nbsp;
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID">
            </asp:DropDownList>
        </td>
        <td align="left">
            &nbsp;
            Bank
            &nbsp;
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBankList" runat="server" DataSourceID="sdsBankList"    AutoPostBack="true"
                DataTextField="BANK_NAME" DataValueField="BANK_INTERNAL_CODE">
            </asp:DropDownList>
        </td>
        <td align="left">
            &nbsp;
             Service List&nbsp;
        </td>
        <td align="left">
            <asp:DropDownList id="ddlService" runat="server" DataSourceID="sdsServiceList"
                 DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" __designer:wfdid="w13"
                  AutoPostBack="True">
            </asp:DropDownList>
           &nbsp; 
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
      <tr>
          <td>
          </td>
          <td align="left">
              <asp:Label runat="server" ID="lblSrcrank" Text="Source Rank"></asp:Label>
              &nbsp;
          </td>
          <td align="left">
              <asp:DropDownList ID="ddlSourceRank" runat="server" AutoPostBack="True" 
               DataSourceID="sdsAccntRank" DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID">
              </asp:DropDownList>
          </td>
          <td align="left">
              &nbsp;
              <asp:Label runat="server" ID="lblDesrank" Text="Destination Rank"></asp:Label>  
              &nbsp;
          </td>
          <td align="left">
              <asp:DropDownList runat="server" ID="ddlDestinationRank" DataSourceID="sdsAccntRank" AutoPostBack="True"
              DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"/>
          </td>
          
      </tr>
    <%--  <tr>
      <td></td>
       <td align="left" colspan="2">
       <table>
       <tr>
       <td>
        <asp:RadioButtonList ID="rblSelectType" runat="server"  
                  RepeatDirection="Horizontal" Height="35px" 
                  Width="214px">
                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem> 
                    <asp:ListItem Value="1" >Bankwise</asp:ListItem>
                   <asp:ListItem Value="2" >Servicewise</asp:ListItem>
               
              </asp:RadioButtonList>
       </td>
       <td>
                <asp:Button ID="btnExport" runat="server"   Text="Export" 
                  onclick="btnExport_Click" Width="56px" />
                </td>
                
       </tr>
       </table>
             
               
          </td>
           
          
      </tr>--%>
     </table>
    </asp:Panel> 
    <%--<asp:Panel ID="Panel1" runat="server" >--%>
         <div>
        <asp:GridView ID="gdvBankSerFeeWave" runat="server" Width="1050px"   BorderStyle="None"
                AutoGenerateColumns="False" DataKeyNames="BNK_SRV_FEE_WAVE_ID" DataSourceID="sdsBankSerFeeWave"
                BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                 AlternatingRowStyle-CssClass="alt" 
            onrowupdated="gdvBankSerFeeWave_RowUpdated" 
            onrowupdating="gdvBankSerFeeWave_RowUpdating" 
            onselectedindexchanged="gdvBankSerFeeWave_SelectedIndexChanged" 
                 AllowPaging="True" >
            <Columns>
                
                <asp:BoundField DataField="BNK_SRV_FEE_WAVE_ID"  Visible="false" HeaderText="BNK_SRV_FEE_WAVE_ID" ReadOnly="True" 
                    SortExpression="BNK_SRV_FEE_WAVE_ID" />
                
                <%--<asp:TemplateField HeaderText="Source Rank" 
                    SortExpression="ACCNT_RANK_ID_SOURCE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlAccRankSource" runat="server" DataSourceID="sdsAccntRank" 
                         AppendDataBoundItems="true" Enabled="True"
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"
                            SelectedValue='<%# Eval("ACCNT_RANK_ID_SOURCE") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="sdsAccntRank"  AppendDataBoundItems="True"
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"  Enabled="False"
                            SelectedValue='<%# Eval("ACCNT_RANK_ID_SOURCE") %>'>
                                <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
                
                <%--<asp:TemplateField HeaderText="Destination Rank" 
                    SortExpression="ACCNT_RANK_ID_DEST">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlAccRankDest" runat="server" DataSourceID="sdsAccntRank"  
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" AppendDataBoundItems="true"
                            SelectedValue='<%# Eval("ACCNT_RANK_ID_DEST") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsAccntRank"   Enabled="false"
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"  AppendDataBoundItems="true"
                            SelectedValue='<%# Eval("ACCNT_RANK_ID_DEST") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
                
                
                <asp:BoundField DataField="WAVE_AMOUNT" 
                    HeaderText="Waive Amount" SortExpression="WAVE_AMOUNT" />
                <%--<asp:TemplateField HeaderText="Service List" SortExpression="SERVICE_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlServiceList" runat="server"  AppendDataBoundItems="true" 
                            DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                            DataValueField="SERVICE_ID"  SelectedValue='<%# Eval("SERVICE_ID") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList5" runat="server"  AppendDataBoundItems="true"
                            DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE"  Enabled="false" 
                            DataValueField="SERVICE_ID" SelectedValue='<%# Eval("SERVICE_ID") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Channel Name" 
                    SortExpression="CHANNEL_TYPE_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlChannelName" runat="server"  AppendDataBoundItems="true"
                            DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE"  
                            DataValueField="CHANNEL_TYPE_ID" SelectedValue='<%# Eval("CHANNEL_TYPE_ID") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList7" runat="server"  AppendDataBoundItems="true"
                            DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE"  Enabled="false"
                            DataValueField="CHANNEL_TYPE_ID" SelectedValue='<%# Eval("CHANNEL_TYPE_ID") %>'>
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Transaction Allowed" ItemStyle-Width="50px" SortExpression="TRAN_ALLOWED">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList8" runat="server" DataTextField="TRAN_ALLOWED"
                                 DataValueField="TRAN_ALLOWED" SelectedValue='<%# Bind("TRAN_ALLOWED") %>'>
                                     <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl" runat="server"  DataTextField="TRAN_ALLOWED"
                                 DataValueField="TRAN_ALLOWED" SelectedValue='<%# Bind("TRAN_ALLOWED") %>' Enabled="false" >
                                
                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>                                   
                                </asp:DropDownList>
                            </ItemTemplate>
                           
                            <ItemStyle Width="50px" />
                           
                        </asp:TemplateField>     
                        
                       <asp:TemplateField HeaderText="Hierarchy Allowed" SortExpression="HIERARCHY_ALLOWED" 
                        >
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlHierarchyLevel" runat="server" SelectedValue='<%# Bind("HIERARCHY_ALLOWED") %>'>
                                <asp:ListItem Value="S">Own Hierarchy</asp:ListItem>
                                <asp:ListItem Value="A">All Hierarchy</asp:ListItem>                                
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList Enabled="false" ID="DropDownList8" runat="server" SelectedValue='<%# Bind("HIERARCHY_ALLOWED") %>'>
                                <asp:ListItem Value="S">Own Hierarchy</asp:ListItem>
                                <asp:ListItem Value="A">All Hierarchy</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField ShowHeader="False">
                             <EditItemTemplate>
                                 <asp:Button ID="btnBankUpdate" runat="server" CausesValidation="True" 
                                     CommandName="Update" Text="Update" />
                                 &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                     CommandName="Cancel" Text="Cancel" />
                                 <ajaxToolkit:ConfirmButtonExtender ID="cbeBankUpdate" runat="server" 
                                     DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                                     TargetControlID="btnBankUpdate">
                                 </ajaxToolkit:ConfirmButtonExtender>  
                                 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                     BackgroundCssClass="modalBackground" CancelControlID="btnBankUpdateCancel" 
                                     OkControlID="btnBankUpdateOK" TargetControlID="btnBankUpdate" 
                                     PopupControlID="pnlPopUpBankUpdate">
                                 </ajaxToolkit:ModalPopupExtender>   
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                     CommandName="Edit" Text="Edit" />
                                 &nbsp;<asp:Button ID="btnDeleteBank" runat="server" CausesValidation="False" 
                                     CommandName="Delete" Text="Delete" />
                                 <ajaxToolkit:ConfirmButtonExtender ID="cbeBankDelete" runat="server" 
                                     DisplayModalPopupID="ModalPopupExtender3" onclientcancel="cancelClick" 
                                     TargetControlID="btnDeleteBank" >
                                 </ajaxToolkit:ConfirmButtonExtender>
                                  <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
                                     BackgroundCssClass="modalBackground" CancelControlID="btnBankDeleteCancel" 
                                     OkControlID="btnBankDeleteOK" TargetControlID="btnDeleteBank" 
                                     PopupControlID="pnlPopUpBankDelete">
                                 </ajaxToolkit:ModalPopupExtender>
                                     
                             </ItemTemplate>
                             <ControlStyle Width="60px" />
                        </asp:TemplateField>
                
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
      </asp:GridView>
        
        <asp:Panel ID="pnlPopUpBankUpdate" runat="server"  style=" display:none;width:300px; height:165px;
                          background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
                      <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                      Are you sure you want to update?
                         <br />&nbsp;<br />&nbsp;
                      </div>
                      <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                            <br />&nbsp;
                            <asp:Button ID="btnBankUpdateOK" runat="server" Text="  Yes  " />
                            <asp:Button ID="btnBankUpdateCancel" runat="server" Text=" Cancel " />
                      </div>
                </asp:Panel>
                <asp:Panel ID="pnlPopUpBankDelete" runat="server"  style=" display:none;width:300px; height:165px;
                         background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
                      <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                       Are you sure you want to delete?
                         <br />&nbsp;<br />&nbsp;
                      </div>
                      <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                        <br />&nbsp;
                        <asp:Button ID="btnBankDeleteOK" runat="server" Text="  Yes  " />
                        <asp:Button ID="btnBankDeleteCancel" runat="server" Text=" Cancel " />
                      </div>
               </asp:Panel> 
        </div> 
        <asp:Panel ID="Panel2" runat="server">
         <table class="Inser_Panel">
          <tr>
           <td>
               Add New Waive
           </td>
          </tr>
         </table>               
             <asp:DetailsView id="dtvBankSerFee" runat="server" DataSourceID="sdsBankSerFeeWave" 
                BorderColor="Silver" Height="200px" Width="450px" AutoGenerateRows="False" 
                DefaultMode="Insert" DataKeyNames="BNK_SRV_FEE_WAVE_ID"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                BorderStyle="None" oniteminserted="dtvBankSerFee_ItemInserted" oniteminserting="dtvBankSerFee_ItemInserting">
           <PagerStyle CssClass="pgr" />
           <Fields>
              <asp:BoundField DataField="BNK_SRV_FEE_WAVE_ID"  Visible="false"
                    HeaderText="BNK_SRV_FEE_WAVE_ID" ReadOnly="True" 
                    SortExpression="BNK_SRV_FEE_WAVE_ID" />
                    
               <%--<asp:TemplateField HeaderText="Source Rank" 
                    SortExpression="ACCNT_RANK_ID_SOURCE">                   
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlSuorceRank" runat="server" DataSourceID="sdsAccntRank" 
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"
                             SelectedValue='<%# Bind("ACCNT_RANK_ID_SOURCE") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsAccntRank" 
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"
                             SelectedValue='<%# Bind("ACCNT_RANK_ID_SOURCE") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
                
              <%-- <asp:TemplateField HeaderText="Destination Rank" 
                    SortExpression="ACCNT_RANK_ID_DEST">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlDestRank" runat="server" DataSourceID="sdsAccntRank" 
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"
                             SelectedValue='<%# Bind("ACCNT_RANK_ID_DEST") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsAccntRank" 
                            DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID"
                             SelectedValue='<%# Bind("ACCNT_RANK_ID_DEST") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
                
                <asp:BoundField DataField="WAVE_AMOUNT" 
                    HeaderText="Waive Amount" SortExpression="WAVE_AMOUNT" />
                <%--<asp:TemplateField HeaderText="Service List" SortExpression="SERVICE_ID">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlSerList" runat="server" 
                            DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                            DataValueField="SERVICE_ID"  SelectedValue='<%# Bind("SERVICE_ID") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList5" runat="server" 
                            DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                            DataValueField="SERVICE_ID" SelectedValue='<%# Bind("SERVICE_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Channel Name" 
                    SortExpression="CHANNEL_TYPE_ID">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlChnlName" runat="server" 
                            DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                            DataValueField="CHANNEL_TYPE_ID" SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList7" runat="server" 
                            DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                            DataValueField="CHANNEL_TYPE_ID" 
                            SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Transaction Allowed" SortExpression="TRAN_ALLOWED">
                   <ItemTemplate>
                      <%-- <asp:DropDownList ID="DropDownList8" runat="server" 
                           DataSourceID="sdsBankSerFeeWave">
                       </asp:DropDownList>--%>
                       <asp:DropDownList ID="ddltranAllowed" runat="server">                       
                         <asp:ListItem Value="Y">Yes</asp:ListItem>
                         <asp:ListItem Value="N">No</asp:ListItem>                      
                       </asp:DropDownList>                       
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Hierarchy Allowed" 
                   SortExpression="HIERARCHY_ALLOWED">
                   <ItemTemplate>                      
                       <asp:DropDownList ID="ddlHierarchy" runat="server">                       
                         <asp:ListItem Value="S">Own Hierarchy</asp:ListItem>
                         <asp:ListItem Value="A">All Hierarchy</asp:ListItem>                       
                       </asp:DropDownList>                       
                   </ItemTemplate>
                   <InsertItemTemplate>
                     <asp:DropDownList ID="ddlHierarchy" runat="server">                       
                         <asp:ListItem Value="S">Own Hierarchy</asp:ListItem>
                         <asp:ListItem Value="A">All Hierarchy</asp:ListItem>                      
                       </asp:DropDownList>
                   </InsertItemTemplate>
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
          <br />
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
     <%--  <Triggers>
         <asp:PostBackTrigger ControlID="btnExport" />
      </Triggers>--%>
    </asp:UpdatePanel>
    </form>
</body>
</html>
