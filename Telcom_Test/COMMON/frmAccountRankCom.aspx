<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountRankCom.aspx.cs" Inherits="COMMON_frmAccountRankCom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Rank Commission</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
         .table
         {
         	background-color:#fcfcfc ;
         	margin: 5px 0 10px 0;
         	border: solid 1px #525252;
            text-align: left;
            border-collapse:collapse;
            border-color:White;
         	}
        .table td{ padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
         .div
         {
         	margin:5px 0 0 0;
         	}	
         .td
         {
         	text-align:right;
         	width:125px;
         	}	
         .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
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
        .style2
        {
            width: 92px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <asp:SqlDataSource ID="sdsRankCom" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand="
INSERT INTO &quot;ACCOUNT_RANK_COMMISSION&quot; (&quot;ACCNT_RANK_COMI_ID&quot;, &quot;ACCNT_RANK_ID&quot;, &quot;SERVICE_ID&quot;, &quot;ACCNT_NO&quot;, &quot;COMMISSION_AMT&quot;, &quot;REMARKS&quot;,&quot;MAX_VALUE_PER_DAY&quot;, &quot;MAX_VALUE_PER_MONTH&quot;, &quot;MAX_TRAN_PER_DAY&quot;, &quot;MAX_TRAN_PER_MONTH&quot;,&quot;TRANSACTION_DIRECTION&quot;,&quot;MAX_CR_VALUE_PER_DAY&quot;,&quot;MAX_CR_VALUE_PER_MONTH&quot;, &quot;CMP_BRANCH_ID&quot; , &quot;BANK_CODE&quot;, &quot;MAX_TRAN_PER_DAY_RCV&quot; , &quot;MAX_TRAN_PER_MONTH_RCV&quot;) 
VALUES (:ACCNT_RANK_COMI_ID, :ACCNT_RANK_ID, :SERVICE_ID, :ACCNT_NO, :COMMISSION_AMT, :REMARKS, :MAX_VALUE_PER_DAY, :MAX_VALUE_PER_MONTH, :MAX_TRAN_PER_DAY, :MAX_TRAN_PER_MONTH, :TRANSACTION_DIRECTION, :MAX_CR_VALUE_PER_DAY, :MAX_CR_VALUE_PER_MONTH, :CMP_BRANCH_ID, :BANK_CODE,:MAX_TRAN_PER_DAY_RCV,:MAX_TRAN_PER_MONTH_RCV )" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        
        
        
        
        
        SelectCommand="SELECT * FROM ACCOUNT_RANK_COMMISSION WHERE (SERVICE_ID = :SERVICE_ID AND CMP_BRANCH_ID=:CMP_BRANCH_ID AND BANK_CODE=:BANK_CODE) ORDER BY ACCNT_RANK_ID" 
        UpdateCommand="
 UPDATE ACCOUNT_RANK_COMMISSION SET
ACCNT_RANK_ID=:ACCNT_RANK_ID,
COMMISSION_AMT=:COMMISSION_AMT,
MAX_VALUE_PER_DAY=:MAX_VALUE_PER_DAY,
MAX_VALUE_PER_MONTH=:MAX_VALUE_PER_MONTH,
MAX_TRAN_PER_DAY=:MAX_TRAN_PER_DAY,
MAX_TRAN_PER_MONTH=:MAX_TRAN_PER_MONTH,
REMARKS=:REMARKS,TRANSACTION_DIRECTION=:TRANSACTION_DIRECTION
,MAX_CR_VALUE_PER_DAY=:MAX_CR_VALUE_PER_DAY
,MAX_CR_VALUE_PER_MONTH=:MAX_CR_VALUE_PER_MONTH
,MAX_TRAN_PER_DAY_RCV=:MAX_TRAN_PER_DAY_RCV
,MAX_TRAN_PER_MONTH_RCV=:MAX_TRAN_PER_MONTH_RCV
WHERE (ACCNT_RANK_COMI_ID = :ACCNT_RANK_COMI_ID)" 
        
        
        
        DeleteCommand="DELETE FROM &quot;ACCOUNT_RANK_COMMISSION&quot; WHERE &quot;ACCNT_RANK_COMI_ID&quot; = :ACCNT_RANK_COMI_ID">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" 
                        PropertyName="SelectedValue" Type="String" />  
            <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" 
                        PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="ddlServiceList" Name="SERVICE_ID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="ACCNT_RANK_COMI_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ACCNT_RANK_ID" />
            <asp:Parameter Name="COMMISSION_AMT" />
            <asp:Parameter Name="MAX_VALUE_PER_DAY" />
            <asp:Parameter Name="MAX_VALUE_PER_MONTH" />
            <asp:Parameter Name="MAX_TRAN_PER_DAY" />
            <asp:Parameter Name="MAX_TRAN_PER_MONTH" />
            <asp:Parameter Name="REMARKS" />
            <asp:Parameter Name="TRANSACTION_DIRECTION" />
            <asp:Parameter Name="MAX_CR_VALUE_PER_DAY" />
            <asp:Parameter Name="MAX_CR_VALUE_PER_MONTH" />
            <asp:Parameter Name="MAX_TRAN_PER_DAY_RCV" />
            <asp:Parameter Name="MAX_TRAN_PER_MONTH_RCV" />
            <asp:Parameter Name="ACCNT_RANK_COMI_ID" />
            
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ACCNT_RANK_COMI_ID" Type="String" />
            <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
            <asp:Parameter Name="SERVICE_ID" Type="String" />
            <asp:Parameter Name="ACCNT_NO" Type="String" />
            <asp:Parameter Name="COMMISSION_AMT" Type="String" />
            <asp:Parameter Name="REMARKS" Type="String" />
            <asp:Parameter Name="MAX_VALUE_PER_DAY" />
            <asp:Parameter Name="MAX_VALUE_PER_MONTH" />
            <asp:Parameter Name="MAX_TRAN_PER_DAY" />
            <asp:Parameter Name="MAX_TRAN_PER_MONTH" />
            <asp:Parameter Name="TRANSACTION_DIRECTION" />
            <asp:Parameter Name="MAX_CR_VALUE_PER_DAY" />
            <asp:Parameter Name="MAX_CR_VALUE_PER_MONTH" />
            <asp:Parameter Name="MAX_TRAN_PER_DAY_RCV" />
            <asp:Parameter Name="MAX_TRAN_PER_MONTH_RCV" />
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsRankName" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        
        SelectCommand="SELECT RANK_TITEL,ACCNT_RANK_ID FROM ACCOUNT_RANK WHERE ACCNT_RANK_ID&lt;&gt;'120519000000000001'ORDER BY ACCNT_RANK_ID">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBranch" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH">
        </asp:SqlDataSource>
        
         <asp:SqlDataSource ID="sdsBankList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   SelectCommand="SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME">
        </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sdsServiceList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE ">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sdsClientList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT CL.CLINT_NAME||' ['||AL.ACCNT_NO||']' CLIENT_NAME,AL.ACCNT_NO FROM ACCOUNT_LIST AL, CLIENT_LIST CL 
WHERE AL.CLINT_ID=CL.CLINT_ID ORDER BY CLINT_NAME">
</asp:SqlDataSource>

<asp:Panel ID="pnlTop" runat="server"  CssClass="Top_Panel" Height="70px" >
     <table width="100%" style="height: 37px">
      <tr>
        <td> &nbsp;&nbsp; 
           Branch &nbsp;
            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID">
            </asp:DropDownList>
             &nbsp; &nbsp; 
           Bank  List    
            <asp:DropDownList ID="ddlBankList" runat="server" DataSourceID="sdsBankList"    AutoPostBack="true"
                DataTextField="BANK_NAME" DataValueField="BANK_INTERNAL_CODE">
            </asp:DropDownList>
       &nbsp; &nbsp; 
        Service List:
             <asp:DropDownList 
            ID="ddlServiceList" runat="server" AutoPostBack="True" DataSourceID="sdsServiceList" 
            DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID">
        </asp:DropDownList>
         </td>
        <td align="left">
        </td>
        <td></td>
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
  <%--  <tr>
    
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
           <td></td>
           <td></td>
           <td></td>
    </tr>--%>
     </table>
    </asp:Panel>  
    
    <asp:GridView ID="gdvComList" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" DataKeyNames="ACCNT_RANK_COMI_ID" 
        DataSourceID="sdsRankCom" onrowdeleted="gdvComList_RowDeleted" 
        onrowupdated="gdvComList_RowUpdated">
        <Columns>
            <asp:BoundField DataField="ACCNT_RANK_COMI_ID" HeaderText="ACCNT_RANK_COMI_ID" ItemStyle-Width="50px" 
                ReadOnly="True" SortExpression="ACCNT_RANK_COMI_ID" Visible="False" >
                <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Account Rank" SortExpression="ACCNT_RANK_ID" ItemStyle-Width="50px">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAccntRank2" runat="server" DataSourceID="sdsRankName" 
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" 
                        SelectedValue='<%# Bind("ACCNT_RANK_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAccntRank" runat="server" DataSourceID="sdsRankName" 
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" Enabled="False" 
                        SelectedValue='<%# Bind("ACCNT_RANK_ID") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Service Name" SortExpression="SERVICE_ID"  ItemStyle-Width="50px"
                Visible="False">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                        DataValueField="SERVICE_ID" SelectedValue='<%# Bind("SERVICE_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                        DataValueField="SERVICE_ID" Enabled="False" 
                        SelectedValue='<%# Bind("SERVICE_ID") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="COMMISSION_AMT" HeaderText="Reward" 
                SortExpression="COMMISSION_AMT" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="50px" />               
                <ControlStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField  DataField="MAX_VALUE_PER_DAY" 
                HeaderText="Maximum Debit Amount PerDay" SortExpression="MAX_VALUE_PER_DAY" > 
                <ItemStyle HorizontalAlign="Left" Width="100px" />               
                <ControlStyle Width="100px" />              
                </asp:BoundField>
            <asp:BoundField DataField="MAX_VALUE_PER_MONTH" 
                HeaderText="Maximum Debit Amount PerMonth" SortExpression="MAX_VALUE_PER_MONTH" >
                <ItemStyle HorizontalAlign="Left" Width="110px" />               
                <ControlStyle Width="110px" />
                </asp:BoundField>
            
            <asp:BoundField DataField="MAX_CR_VALUE_PER_DAY" 
                HeaderText="Maximum Credit Amount PerDay" SortExpression="MAX_CR_VALUE_PER_DAY">
                 <ItemStyle HorizontalAlign="Left" Width="100px" />               
                <ControlStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="MAX_CR_VALUE_PER_MONTH" 
                HeaderText="Maximum Credit Amount PerMonth" SortExpression="MAX_CR_VALUE_PER_MONTH">
                <ItemStyle HorizontalAlign="Left" Width="100px" />               
                <ControlStyle Width="100px" />
            </asp:BoundField>
                
            <asp:BoundField DataField="MAX_TRAN_PER_DAY" 
                HeaderText="Maximum Transaction PerDay" SortExpression="MAX_TRAN_PER_DAY" >
                <ItemStyle HorizontalAlign="Left" Width="110px" />               
                <ControlStyle Width="110px" />
                </asp:BoundField>
            <asp:BoundField DataField="MAX_TRAN_PER_MONTH" 
                HeaderText="Maximum Transaction PerMonth" SortExpression="MAX_TRAN_PER_MONTH" >
               <ItemStyle HorizontalAlign="Left" Width="130px" />               
                <ControlStyle Width="130px" />
                </asp:BoundField> 
                
            <asp:BoundField DataField="MAX_TRAN_PER_DAY_RCV" 
                HeaderText="Maximum Transaction RCV PerDay" SortExpression="MAX_TRAN_PER_DAY_RCV" >
                <ItemStyle HorizontalAlign="Left" Width="110px" />               
                <ControlStyle Width="110px" />
                </asp:BoundField>
            <asp:BoundField DataField="MAX_TRAN_PER_MONTH_RCV" 
                HeaderText="Maximum Transaction RCV PerMonth" SortExpression="MAX_TRAN_PER_MONTH_RCV" >
               <ItemStyle HorizontalAlign="Left" Width="130px" />               
                <ControlStyle Width="130px" />
             </asp:BoundField> 
                
                 <asp:TemplateField HeaderText="Transaction Direction" ItemStyle-Width="120px" SortExpression="TRANSACTION_DIRECTION">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList8" runat="server" DataTextField="TRANSACTION_DIRECTION" AppendDataBoundItems="true"
                                 DataValueField="TRANSACTION_DIRECTION" SelectedValue='<%# Bind("TRANSACTION_DIRECTION") %>'>
                                   <asp:ListItem Value=""></asp:ListItem>
                                      <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="UPO" Text="Upper Level"></asp:ListItem>
                                     <asp:ListItem Value="DLO" Text="Down Level"></asp:ListItem>
                                      <asp:ListItem Value="SLO" Text="Same Level"></asp:ListItem>
                                      <asp:ListItem Value="SAU" Text="Same and Up"></asp:ListItem>
                                      <asp:ListItem Value="SAD" Text="Same and Down"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList10" runat="server" DataTextField="TRANSACTION_DIRECTION" AppendDataBoundItems="true" 
                                 DataValueField="TRANSACTION_DIRECTION" SelectedValue='<%# Bind("TRANSACTION_DIRECTION") %>' Enabled="false" >
                                 <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="UPO" Text="Upper Level"></asp:ListItem>
                                     <asp:ListItem Value="DLO" Text="Down Level"></asp:ListItem>
                                      <asp:ListItem Value="SLO" Text="Same Level"></asp:ListItem>
                                      <asp:ListItem Value="SAU" Text="Same and Up"></asp:ListItem>
                                      <asp:ListItem Value="SAD" Text="Same and Down"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                
                
            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                SortExpression="REMARKS" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="60px" />
                <ControlStyle Width="60px" />
            </asp:BoundField>
            <%--<asp:CommandField ButtonType="Button" ShowEditButton="True" 
                ShowDeleteButton="True" />--%>
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
                             
                        </asp:TemplateField>    
        </Columns>
        <PagerStyle CssClass="pgr" />
        <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
     <asp:Panel ID="pnlPopUpBankUpdate" runat="server"  style=" display:none;width:300px; height:165px;
                          background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
                      <div style=" text-align:center; height:95px;"><br />&nbsp;<br />&nbsp;
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
                      <div style=" text-align:center;height:95px;"><br />&nbsp;<br />&nbsp;
                       Are you sure you want to delete?
                         <br />&nbsp;<br />&nbsp;
                      </div>
                      <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                        <br />&nbsp;
                        <asp:Button ID="btnBankDeleteOK" runat="server" Text="  Yes  " />
                        <asp:Button ID="btnBankDeleteCancel" runat="server" Text=" Cancel " />
                      </div>
               </asp:Panel> 
               
    <div class="Inser_Panel" > <strong><span style="color: white">Add New Rank Commission</span></strong></div> 
    
    <asp:DetailsView ID="dtvAccntRankCom" runat="server" AutoGenerateRows="False" 
        DataKeyNames="ACCNT_RANK_COMI_ID" DataSourceID="sdsRankCom" 
        DefaultMode="Insert" GridLines="None" Height="50px" Width="325px" 
        CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
        oniteminserting="dtvAccntRankCom_ItemInserting" 
        oniteminserted="dtvAccntRankCom_ItemInserted" >
        <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:BoundField DataField="ACCNT_RANK_COMI_ID" HeaderText="ACCNT_RANK_COMI_ID" 
                ReadOnly="True" SortExpression="ACCNT_RANK_COMI_ID" Visible="False" />
            <asp:TemplateField HeaderText="Account Rank" SortExpression="ACCNT_RANK_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_RANK_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="ddlAccntRank" runat="server" DataSourceID="sdsRankName" 
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" 
                        SelectedValue='<%# Bind("ACCNT_RANK_ID") %>'>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACCNT_RANK_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Service" SortExpression="SERVICE_ID" 
                Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="ddlServiceID" runat="server" 
                        DataSourceID="sdsServiceList" DataTextField="SERVICE_TITLE" 
                        DataValueField="SERVICE_ID" SelectedValue='<%# Bind("SERVICE_ID") %>'>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:BoundField DataField="COMMISSION_AMT" HeaderText="Reward" 
                SortExpression="COMMISSION_AMT">
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            
            <asp:BoundField DataField="MAX_VALUE_PER_DAY" 
                HeaderText="Maximum Debit Amount PerDay" SortExpression="MAX_VALUE_PER_DAY">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MAX_VALUE_PER_MONTH" 
                HeaderText="Maximum Debit Amount PerMonth" SortExpression="MAX_VALUE_PER_MONTH">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
            <asp:BoundField DataField="MAX_CR_VALUE_PER_DAY" 
                HeaderText="Maximum Credit Amount PerDay" SortExpression="MAX_CR_VALUE_PER_DAY">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MAX_CR_VALUE_PER_MONTH" 
                HeaderText="Maximum Credit Amount PerMonth" SortExpression="MAX_CR_VALUE_PER_MONTH">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
            <asp:BoundField DataField="MAX_TRAN_PER_DAY" 
                HeaderText="Maximum Transaction PerDay" SortExpression="MAX_TRAN_PER_DAY">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MAX_TRAN_PER_MONTH" 
                HeaderText="Maximum Transaction PerMonth" SortExpression="MAX_TRAN_PER_MONTH">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
             <asp:BoundField DataField="MAX_TRAN_PER_DAY_RCV" 
                HeaderText="Maximum Transaction RCV PerDay" SortExpression="MAX_TRAN_PER_DAY_RCV">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MAX_TRAN_PER_MONTH_RCV" 
                HeaderText="Maximum Transaction RCV PerMonth" SortExpression="MAX_TRAN_PER_MONTH_RCV">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
             <asp:TemplateField  HeaderText="Transaction Direction">
                            <EditItemTemplate>
                               <asp:DropDownList ID="DropDownList111" runat="server" DataTextField="TRANSACTION_DIRECTION" DataValueField="TRANSACTION_DIRECTION"
                                 SelectedValue='<%# Bind("TRANSACTION_DIRECTION") %>'>
                                    <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="UPO" Text="Upper Level"></asp:ListItem>
                                     <asp:ListItem Value="DLO" Text="Down Level"></asp:ListItem>
                                      <asp:ListItem Value="SLO" Text="Same Level"></asp:ListItem>
                                      <asp:ListItem Value="SAU" Text="Same and Up"></asp:ListItem>
                                      <asp:ListItem Value="SAD" Text="Same and Down"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="TRANSACTION_DIRECTION" DataValueField="TRANSACTION_DIRECTION"
                             SelectedValue='<%# Bind("TRANSACTION_DIRECTION") %>'  > 
                                   <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="UPO" Text="Upper Level"></asp:ListItem>
                                     <asp:ListItem Value="DLO" Text="Down Level"></asp:ListItem>
                                      <asp:ListItem Value="SLO" Text="Same Level"></asp:ListItem>
                                      <asp:ListItem Value="SAU" Text="Same and Up"></asp:ListItem>
                                      <asp:ListItem Value="SAD" Text="Same and Down"></asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList17" runat="server" DataTextField="TRANSACTION_DIRECTION" DataValueField="TRANSACTION_DIRECTION"
                                SelectedValue='<%# Bind("TRANSACTION_DIRECTION") %>' Enabled="false" >
                            <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="UPO" Text="Upper Level"></asp:ListItem>
                                     <asp:ListItem Value="DLO" Text="Down Level"></asp:ListItem>
                                      <asp:ListItem Value="SLO" Text="Same Level"></asp:ListItem>
                                      <asp:ListItem Value="SAU" Text="Same and Up"></asp:ListItem>
                                      <asp:ListItem Value="SAD" Text="Same and Down"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
            
            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                SortExpression="REMARKS">
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            
            <%--<asp:CommandField ButtonType="Button" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>--%>
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
    </ContentTemplate>
   <%--  <Triggers>
         <asp:PostBackTrigger ControlID="btnExport" />
      </Triggers>--%>
    </asp:UpdatePanel>
    </form>
</body>
</html>
