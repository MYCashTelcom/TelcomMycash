<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAgentOperatorCommission.aspx.cs" Inherits="BANKING_frmAgentOperatorCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="header1" runat="server">
    <title>Agent Operator Commission</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
         	width:817px;         	
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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="updatePanel1" runat="server">
    <ContentTemplate>
            <asp:SqlDataSource ID="sdsService" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST WHERE SERVICE_STATE='A' ORDER BY SERVICE_TITLE">
         </asp:SqlDataSource>
            
            <asp:SqlDataSource ID="sdsBranch" runat="server"
               ConnectionString="<%$ ConnectionStrings:oracleConString %>"
               ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
               SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH">
          </asp:SqlDataSource>
          
          <%--  <asp:SqlDataSource ID="sdsServicePackage" runat="server" 
          ConnectionString="<%$ ConnectionStrings:oracleConString %>"
          ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
          SelectCommand="SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_STATUS='A'"
          ></asp:SqlDataSource> --%>       
        
            <asp:SqlDataSource ID="sdsBankList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"                    
             SelectCommand="SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME">
        </asp:SqlDataSource>  
            
            <asp:SqlDataSource ID="sdsAccountRank" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                DeleteCommand="DELETE FROM &quot;ACCOUNT_RANK&quot; WHERE &quot;ACCNT_RANK_ID&quot; = :ACCNT_RANK_ID" 
                InsertCommand="INSERT INTO &quot;ACCOUNT_RANK&quot; (&quot;ACCNT_RANK_ID&quot;, &quot;RANK_TITEL&quot;) VALUES (:ACCNT_RANK_ID, :RANK_TITEL)" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE STATUS='A' " 
                
                UpdateCommand="UPDATE &quot;ACCOUNT_RANK&quot; SET &quot;RANK_TITEL&quot; = :RANK_TITEL WHERE &quot;ACCNT_RANK_ID&quot; = :ACCNT_RANK_ID">
                <DeleteParameters>
                    <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="RANK_TITEL" Type="String" />
                    <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>      
                    <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />                                  
                    <asp:Parameter Name="RANK_TITEL" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            
            
            <asp:SqlDataSource ID="sdsAgentOprCommission" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                DeleteCommand="DELETE FROM AGENT_OPARETOR_COMMISSION WHERE (AGENT_OPARETOR_COMI_ID = :AGENT_OPARETOR_COMI_ID)" 
                InsertCommand="INSERT INTO AGENT_OPARETOR_COMMISSION(ACCNT_RANK_ID,SERVICE_ID, AGENT_OPERATOR_COMI, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_PREFIX,COMMISSION_NAME) VALUES ( :ACCNT_RANK_ID,:SERVICE_ID, :AGENT_OPERATOR_COMI, :CMP_BRANCH_ID, :BANK_CODE, :AGENT_OPERATOR_PREFIX,:COMMISSION_NAME )" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT  * FROM AGENT_OPARETOR_COMMISSION WHERE (CMP_BRANCH_ID = :CMP_BRANCH_ID AND ACCNT_RANK_ID = :ACCNT_RANK_ID AND SERVICE_ID = :SERVICE_ID AND  BANK_CODE = :BANK_CODE   )" 
                UpdateCommand="UPDATE AGENT_OPARETOR_COMMISSION SET AGENT_OPERATOR_COMI = :AGENT_OPERATOR_COMI, AGENT_OPERATOR_PREFIX= :AGENT_OPERATOR_PREFIX,COMMISSION_NAME=:COMMISSION_NAME WHERE (AGENT_OPARETOR_COMI_ID = :AGENT_OPARETOR_COMI_ID)">
                           
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlAccountRank" Name="ACCNT_RANK_ID" PropertyName="SelectedValue" Type="String" />  
                     <%--<asp:ControlParameter ControlID="ddlServicePackage" Name="SERVICE_PKG_ID" PropertyName="SelectedValue" Type="String" />--%>
                     <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" PropertyName="SelectedValue" Type="String" />
                     <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" Type="String" />  
                     <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" Type="String" />                          
                </SelectParameters>                                            
                <DeleteParameters>
                    <asp:Parameter Name="AGENT_OPARETOR_COMI_ID" Type="String" />
                </DeleteParameters>   
                <UpdateParameters>
                    <asp:Parameter Name="AGENT_OPERATOR_COMI" Type="String" />
                    <asp:Parameter Name="AGENT_OPERATOR_PREFIX" Type="String" />    
                    <asp:Parameter Name="COMMISSION_NAME" Type="String" />               
                </UpdateParameters>                             
                <InsertParameters>
                    <asp:ControlParameter ControlID="ddlAccountRank" Name="ACCNT_RANK_ID" PropertyName="SelectedValue" Type="String" />  
                     <%--<asp:ControlParameter ControlID="ddlServicePackage" Name="SERVICE_PKG_ID" PropertyName="SelectedValue" Type="String" />--%>
                     <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" PropertyName="SelectedValue" Type="String" /> 
                     <asp:Parameter Name="AGENT_OPERATOR_COMI" Type="String" />
                     <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" Type="String" />  
                     <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" Type="String" /> 
                     <asp:Parameter Name="AGENT_OPERATOR_PREFIX" Type="String" />
                     <asp:Parameter Name="COMMISSION_NAME" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
             <div>            
                <asp:Panel ID="pnlTop" runat="server" Width="100%">
                    <table style="width: 100%;" class="Top_Panel">
                        <tr>
                            <td>
                                <asp:Label ID="lblTitile" runat="server" Text="Agent Operator Commission:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                                    DataSourceID="sdsBranch" DataTextField="CMP_BRANCH_NAME" 
                                    DataValueField="CMP_BRANCH_ID">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblBranchList" runat="server" Text="Bank  List"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBankList" runat="server" AutoPostBack="true" 
                                    DataSourceID="sdsBankList" DataTextField="BANK_NAME" 
                                    DataValueField="BANK_INTERNAL_CODE">
                                </asp:DropDownList>
                            </td>
                            <td>
                               <asp:Label ID="lblSerList" runat="server" Text="Service List"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlService" runat="server" AutoPostBack="True" 
                                    DataSourceID="sdsService" DataTextField="SERVICE_TITLE" 
                                    DataValueField="SERVICE_ID">
                                </asp:DropDownList>
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
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblRank" runat="server" Text="Agent Rank"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAccountRank" runat="server" 
                                    AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="sdsAccountRank" 
                                    DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID">
                                </asp:DropDownList>
                            </td>
                            <%--<td>
                                <asp:Label ID="lblServicePackage" runat="server" Text="Service Package"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlServicePackage" runat="server" 
                                    AppendDataBoundItems="true" AutoPostBack="true" 
                                    DataSourceID="sdsServicePackage" DataTextField="SERVICE_PKG_NAME" 
                                    DataValueField="SERVICE_PKG_ID">
                                </asp:DropDownList>
                            </td>--%>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                               </td>
                           
                        </tr>
                    </table>
             </asp:Panel>     
             </div>
             <div>
                 <asp:GridView ID="gdvOprCom" runat="server" AllowPaging="True" 
                     CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                     DataKeyNames="AGENT_OPARETOR_COMI_ID" PageSize="15" AllowSorting="True" AutoGenerateColumns="False" 
                     DataSourceID="sdsAgentOprCommission" onrowdeleted="gdvOprCom_RowDeleted" 
                     onrowupdated="gdvOprCom_RowUpdated" onrowupdating="gdvOprCom_RowUpdating" 
                     BorderColor="#E0E0E0" BorderStyle="None" onselectedindexchanged="gdvOprCom_SelectedIndexChanged" Width="700px">                    
                     <Columns>
                         
                      <asp:BoundField DataField="AGENT_OPARETOR_COMI_ID" HeaderText="AGENT_OPARETOR_COMI_ID" SortExpression="AGENT_OPARETOR_COMI_ID" Visible="false" /> 
                      <asp:BoundField DataField="COMMISSION_NAME" HeaderText="Commission Name" SortExpression="COMMISSION_NAME"  /> 
                      <asp:BoundField DataField="AGENT_OPERATOR_COMI" HeaderText="Agent Operator Comm" SortExpression="AGENT_OPERATOR_COMI"  /> 
                         
                      <asp:TemplateField HeaderText="Operator Prefix" SortExpression="AGENT_OPERATOR_PREFIX" >
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlOpr" runat="server" DataTextField="AGENT_OPERATOR_PREFIX" Enabled="true"
                            DataValueField="AGENT_OPERATOR_PREFIX"  SelectedValue='<%# Bind("AGENT_OPERATOR_PREFIX") %>'>
                                <asp:ListItem Value="011">011</asp:ListItem>
                                <asp:ListItem Value="015">015</asp:ListItem>
                                <asp:ListItem Value="016">016</asp:ListItem>
                                <asp:ListItem Value="017">017</asp:ListItem>
                                <asp:ListItem Value="018">018</asp:ListItem>
                                <asp:ListItem Value="019">019</asp:ListItem>                                
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList8" runat="server" DataTextField="AGENT_OPERATOR_PREFIX" Enabled="false"
                            DataValueField="AGENT_OPERATOR_PREFIX"  SelectedValue='<%# Bind("AGENT_OPERATOR_PREFIX") %>'>
                                <asp:ListItem Value="011">011</asp:ListItem>
                                <asp:ListItem Value="015">015</asp:ListItem>
                                <asp:ListItem Value="016">016</asp:ListItem>
                                <asp:ListItem Value="017">017</asp:ListItem>
                                <asp:ListItem Value="018">018</asp:ListItem>
                                <asp:ListItem Value="019">019</asp:ListItem> 
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
                         
                       <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />--%>
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
             <div>
             <asp:Panel ID="panelBody" runat="server" CssClass="Inser_Panel" Width="100%" >
             <table>
             <tr>
                <td>
                    <asp:Label ID="lblAddCommission" runat="server" Text="Add New Commission:"></asp:Label>
                </td>
             </tr>
             </table>
             </asp:Panel>
             </div>
     
            <div>
            <asp:DetailsView ID="dtvOprtComm" runat="server" AutoGenerateRows="False" 
                DataKeyNames="AGENT_OPARETOR_COMI_ID" DataSourceID="sdsAgentOprCommission" 
                 Height="50px" Width="125px" DefaultMode="Insert" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" oniteminserted="dtvOprtComm_ItemInserted" oniteminserting="dtvOprtComm_ItemInserting" >
                 <PagerStyle CssClass="pgr" />
                 <Fields>
                 <asp:BoundField DataField="COMMISSION_NAME" HeaderText="Commission Name" SortExpression="COMMISSION_NAME"  /> 
                <asp:BoundField DataField="AGENT_OPERATOR_COMI" HeaderText="Agent Operator Commission(%)" SortExpression="AGENT_OPERATOR_COMI">
                    <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Operator Prefix"  SortExpression="AGENT_OPERATOR_PREFIX" >
                   <ItemTemplate>                      
                       <asp:DropDownList ID="ddlPrefix" runat="server" DataTextField="AGENT_OPERATOR_PREFIX" DataValueField="AGENT_OPERATOR_PREFIX"  SelectedValue='<%# Bind("AGENT_OPERATOR_PREFIX") %>' >                       
                        <asp:ListItem Value="011">011</asp:ListItem>
                        <asp:ListItem Value="015">015</asp:ListItem>
                        <asp:ListItem Value="016">016</asp:ListItem>
                        <asp:ListItem Value="017">017</asp:ListItem>
                        <asp:ListItem Value="018">018</asp:ListItem>
                        <asp:ListItem Value="019">019</asp:ListItem>                       
                       </asp:DropDownList>                       
                   </ItemTemplate>
                   <InsertItemTemplate>
                     <asp:DropDownList ID="ddlPrefix1" runat="server" DataTextField="AGENT_OPERATOR_PREFIX" DataValueField="AGENT_OPERATOR_PREFIX"  SelectedValue='<%# Bind("AGENT_OPERATOR_PREFIX") %>' >                       
                        <asp:ListItem Value="011">011</asp:ListItem>
                        <asp:ListItem Value="015">015</asp:ListItem>
                        <asp:ListItem Value="016">016</asp:ListItem>
                        <asp:ListItem Value="017">017</asp:ListItem>
                        <asp:ListItem Value="018">018</asp:ListItem>
                        <asp:ListItem Value="019">019</asp:ListItem>                     
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
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
   </form>
</body>
</html>
