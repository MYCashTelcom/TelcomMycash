<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankServiceFee.aspx.cs" Inherits="BANKING_frmMngBankServiceFee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bank Service Fee</title>
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
    <form id="form2" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>    
       
         <asp:SqlDataSource ID="sdsService" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand="SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE">
         </asp:SqlDataSource>
          <asp:SqlDataSource ID="sdsAgntOperatorCommi" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand="SELECT AGENT_OPARETOR_COMI_ID,COMMISSION_NAME||' '||AGENT_OPERATOR_COMI COMMISSION_NAME  FROM  AGENT_OPARETOR_COMMISSION WHERE (SERVICE_ID=:SERVICE_ID)">
            <SelectParameters>
                    <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" 
                        PropertyName="SelectedValue" Type="String" /> 
            </SelectParameters>
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
            
            <asp:SqlDataSource ID="sdsChannelType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE">
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
            <asp:SqlDataSource ID="sdsServiceFee" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                DeleteCommand="DELETE FROM &quot;BANK_SERVICE_FEE&quot; WHERE &quot;BANK_SRV_FEE_ID&quot; = :BANK_SRV_FEE_ID" 
                InsertCommand="INSERT INTO &quot;BANK_SERVICE_FEE&quot; (&quot;SERVICE_ID&quot;, &quot;BANK_SRV_FEE_ID&quot;, &quot;BANK_SRV_FEE_TITLE&quot;, &quot;START_AMOUNT&quot;, &quot;MAX_AMOUNT&quot;, &quot;BANK_SRV_FEE_AMOUNT&quot;, &quot;VAT_TAX&quot;, &quot;SERVICE_PKG_ID&quot;, &quot;ACCNT_RANK_ID&quot;, &quot;TAX_PAID_BY&quot;, &quot;FEE_INCLUDE_VAT_TAX&quot;, &quot;FEES_PAID_BY_BANK&quot;, &quot;FEES_PAID_BY_CUSTOMER&quot;, &quot;AIT&quot;, &quot;AGENT_COMM_AMOUNT&quot;, &quot;BANK_COMM_AMOUNT&quot;, &quot;FEES_PAID_BY&quot;, &quot;POOL_ADJUSTMENT&quot;, &quot;VENDOR_COMMISSION&quot;, &quot;THIRD_PARTY_COMMISSION&quot;, &quot;CHANNEL_COMMISSION&quot;, &quot;CHANNEL_TYPE_ID&quot;, &quot;FEES_PAID_BY_AGENT&quot;, &quot;MIN_FEE&quot;, &quot;CMP_BRANCH_ID&quot;, &quot;BANK_CODE&quot;, &quot;AGENT_OPERATOR_COMI&quot;, &quot;AGENT_OPARETOR_COMI_ID&quot;) VALUES (:SERVICE_ID, :BANK_SRV_FEE_ID, :BANK_SRV_FEE_TITLE, :START_AMOUNT, :MAX_AMOUNT, :BANK_SRV_FEE_AMOUNT, :VAT_TAX, :SERVICE_PKG_ID, :ACCNT_RANK_ID, :TAX_PAID_BY, :FEE_INCLUDE_VAT_TAX, :FEES_PAID_BY_BANK, :FEES_PAID_BY_CUSTOMER, :AIT, :AGENT_COMM_AMOUNT, :BANK_COMM_AMOUNT, :FEES_PAID_BY, :POOL_ADJUSTMENT, :VENDOR_COMMISSION, :THIRD_PARTY_COMMISSION, :CHANNEL_COMMISSION, :CHANNEL_TYPE_ID,:FEES_PAID_BY_AGENT, :MIN_FEE,:CMP_BRANCH_ID,:BANK_CODE,:AGENT_OPERATOR_COMI,:AGENT_OPARETOR_COMI_ID)" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT * FROM BANK_SERVICE_FEE WHERE (SERVICE_ID=:SERVICE_ID) AND (ACCNT_RANK_ID=:ACCNT_RANK_ID) AND (CHANNEL_TYPE_ID=:CHANNEL_TYPE_ID) AND (CMP_BRANCH_ID=:CMP_BRANCH_ID) AND (BANK_CODE=:BANK_CODE)" 
                
                UpdateCommand="UPDATE &quot;BANK_SERVICE_FEE&quot; SET &quot;SERVICE_ID&quot; = :SERVICE_ID, &quot;BANK_SRV_FEE_TITLE&quot; = :BANK_SRV_FEE_TITLE, &quot;START_AMOUNT&quot; = :START_AMOUNT, &quot;MAX_AMOUNT&quot; = :MAX_AMOUNT, &quot;BANK_SRV_FEE_AMOUNT&quot; = :BANK_SRV_FEE_AMOUNT, &quot;VAT_TAX&quot; = :VAT_TAX, &quot;SERVICE_PKG_ID&quot; = :SERVICE_PKG_ID, &quot;ACCNT_RANK_ID&quot; = :ACCNT_RANK_ID, &quot;TAX_PAID_BY&quot; = :TAX_PAID_BY, &quot;FEE_INCLUDE_VAT_TAX&quot; = :FEE_INCLUDE_VAT_TAX, &quot;FEES_PAID_BY_BANK&quot; = :FEES_PAID_BY_BANK, &quot;FEES_PAID_BY_CUSTOMER&quot; = :FEES_PAID_BY_CUSTOMER,&quot;FEES_PAID_BY_AGENT&quot; =:FEES_PAID_BY_AGENT,&quot;AIT&quot; = :AIT, &quot;AGENT_COMM_AMOUNT&quot; = :AGENT_COMM_AMOUNT, &quot;BANK_COMM_AMOUNT&quot; = :BANK_COMM_AMOUNT, &quot;FEES_PAID_BY&quot; = :FEES_PAID_BY, &quot;POOL_ADJUSTMENT&quot; = :POOL_ADJUSTMENT, &quot;VENDOR_COMMISSION&quot; = :VENDOR_COMMISSION, &quot;THIRD_PARTY_COMMISSION&quot; = :THIRD_PARTY_COMMISSION, &quot;CHANNEL_COMMISSION&quot; = :CHANNEL_COMMISSION, &quot;MIN_FEE&quot; = :MIN_FEE,&quot;AGENT_OPERATOR_COMI&quot; = :AGENT_OPERATOR_COMI,&quot;AGENT_OPARETOR_COMI_ID&quot; = :AGENT_OPARETOR_COMI_ID  WHERE &quot;BANK_SRV_FEE_ID&quot; = :BANK_SRV_FEE_ID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlService" Name="SERVICE_ID" 
                        PropertyName="SelectedValue" Type="String" />  
                     <asp:ControlParameter ControlID="ddlAccountRank" Name="ACCNT_RANK_ID" 
                        PropertyName="SelectedValue" Type="String" />
                     <asp:ControlParameter ControlID="ddlChannelName" Name="CHANNEL_TYPE_ID" 
                        PropertyName="SelectedValue" Type="String" />   
                     <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" 
                        PropertyName="SelectedValue" Type="String" />  
                     <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" 
                        PropertyName="SelectedValue" Type="String" />                          
                </SelectParameters>                
                <DeleteParameters>
                    <asp:Parameter Name="BANK_SRV_FEE_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SERVICE_ID" Type="String" />
                    <asp:Parameter Name="BANK_SRV_FEE_TITLE" Type="String" />
                    <asp:Parameter Name="START_AMOUNT" Type="Decimal" />
                    <asp:Parameter Name="MAX_AMOUNT" Type="Decimal" />
                    <asp:Parameter Name="BANK_SRV_FEE_AMOUNT" Type="String" />
                    <asp:Parameter Name="MIN_FEE" Type="String" />
                    <asp:Parameter Name="VAT_TAX" Type="String" />
                    <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                    <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
                    <asp:Parameter Name="TAX_PAID_BY" Type="String" />
                    <asp:Parameter Name="FEE_INCLUDE_VAT_TAX" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_BANK" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_CUSTOMER" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_AGENT" Type="String" />
                    <asp:Parameter Name="AIT" Type="String" />
                    <asp:Parameter Name="AGENT_COMM_AMOUNT" Type="String" />
                    <asp:Parameter Name="BANK_COMM_AMOUNT" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY" Type="String" />
                    <asp:Parameter Name="POOL_ADJUSTMENT" Type="String" />
                    <asp:Parameter Name="VENDOR_COMMISSION" Type="String" />
                    <asp:Parameter Name="THIRD_PARTY_COMMISSION" Type="String" />
                    <asp:Parameter Name="CHANNEL_COMMISSION" Type="String" />
                    <asp:Parameter Name="AGENT_OPERATOR_COMI" Type="String" />
                    <asp:Parameter Name="AGENT_OPARETOR_COMI_ID" Type="String" />
                    <asp:Parameter Name="BANK_SRV_FEE_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="SERVICE_ID" Type="String" />
                    <asp:Parameter Name="BANK_SRV_FEE_ID" Type="String" />
                    <asp:Parameter Name="BANK_SRV_FEE_TITLE" Type="String" />
                    <asp:Parameter Name="START_AMOUNT" Type="Decimal" />
                    <asp:Parameter Name="MAX_AMOUNT" Type="Decimal" />
                    <asp:Parameter Name="BANK_SRV_FEE_AMOUNT" Type="String" />
                    <asp:Parameter Name="MIN_FEE" Type="String" />
                    <asp:Parameter Name="VAT_TAX" Type="String" />
                    <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                    <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
                    <asp:Parameter Name="TAX_PAID_BY" Type="String" />
                    <asp:Parameter Name="FEE_INCLUDE_VAT_TAX" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_BANK" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_CUSTOMER" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY_AGENT" Type="String" />
                    <asp:Parameter Name="AIT" Type="String" />
                    <asp:Parameter Name="AGENT_COMM_AMOUNT" Type="String" />
                    <asp:Parameter Name="BANK_COMM_AMOUNT" Type="String" />
                    <asp:Parameter Name="FEES_PAID_BY" Type="String" />
                    <asp:Parameter Name="POOL_ADJUSTMENT" Type="String" />
                    <asp:Parameter Name="VENDOR_COMMISSION" Type="String" />
                    <asp:Parameter Name="THIRD_PARTY_COMMISSION" Type="String" />
                    <asp:Parameter Name="CHANNEL_COMMISSION" Type="String" />
                    <asp:Parameter Name="AGENT_OPERATOR_COMI" Type="String" />
                    <asp:Parameter Name="AGENT_OPARETOR_COMI_ID" Type="String" />
                   <%-- <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />--%>
                    <asp:ControlParameter ControlID="ddlChannelName" Name="CHANNEL_TYPE_ID" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" />
                    
                </InsertParameters>
            </asp:SqlDataSource>
           
     <asp:Panel ID="pnlTop" runat="server" Width="100%">
       <table width="100%" class="Top_Panel">
        <tr>
         <td>
            Manage Service Rate
         </td>
         <td align="left">
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Branch 
            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch" AutoPostBack="true"
                DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID">
            </asp:DropDownList>
         </td>
         <td align="left">
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Bank  List    
            <asp:DropDownList ID="ddlBankList" runat="server" DataSourceID="sdsBankList"    AutoPostBack="true"
                DataTextField="BANK_NAME" DataValueField="BANK_INTERNAL_CODE">
            </asp:DropDownList>
         </td>
         <td align="left">
            Service List
            <asp:DropDownList id="ddlService" runat="server" DataSourceID="sdsService"
                 DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" __designer:wfdid="w13"
                  AutoPostBack="True">
            </asp:DropDownList>
         </td>
         <td>
          <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
         </td>
        </tr>
        <tr>
         <td></td>
         <td align="left">
            <asp:Label ID="lblRank" runat="server" Text="Account Rank"></asp:Label>
           
            <asp:DropDownList ID="ddlAccountRank" runat="server"  
                           AppendDataBoundItems="true" AutoPostBack="true"
                            DataSourceID="sdsAccountRank" DataTextField="RANK_TITEL" 
                            DataValueField="ACCNT_RANK_ID" >
                         <asp:ListItem Value="" Text="" ></asp:ListItem>
            </asp:DropDownList>
         </td>
         <td align="left">
             <asp:Label ID="lblChannelName" runat="server" Text="Channel Name"></asp:Label>           
            <asp:DropDownList ID="ddlChannelName" runat="server" AppendDataBoundItems="true" 
                    DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE"  AutoPostBack="true"
                    DataValueField="CHANNEL_TYPE_ID">
                     <asp:ListItem Value="" Text="" ></asp:ListItem>
            </asp:DropDownList>
         </td>
        </tr>
       </table>
     </asp:Panel>           
        <div>        
            <asp:GridView ID="gdvServiceFee" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BANK_SRV_FEE_ID" 
                DataSourceID="sdsServiceFee" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt"  
                onrowupdating="gdvServiceFee_RowUpdating" BorderStyle="None" 
                onrowdeleted="gdvServiceFee_RowDeleted" onrowupdated="gdvServiceFee_RowUpdated">
                <Columns>
                    <asp:BoundField DataField="SERVICE_ID" HeaderText="Service" 
                        SortExpression="SERVICE_ID" Visible="False" />
                    <asp:BoundField DataField="BANK_SRV_FEE_ID" HeaderText="BANK_SRV_FEE_ID" 
                        ReadOnly="True" SortExpression="BANK_SRV_FEE_ID" Visible="False" />
                   
                    <asp:BoundField DataField="BANK_SRV_FEE_TITLE" HeaderText="Fee Name" 
                        SortExpression="BANK_SRV_FEE_TITLE" >
                        <ItemStyle Width="80px" />
                        <ControlStyle Width="80px"/>
                     </asp:BoundField >
                    <asp:BoundField DataField="START_AMOUNT" HeaderText="Start Amount" 
                        SortExpression="START_AMOUNT" >
                         <ItemStyle Width="50px" />
                        <ControlStyle Width="50px"/>
                      </asp:BoundField >   
                    <asp:BoundField DataField="MAX_AMOUNT" HeaderText="Max Amount" 
                        SortExpression="MAX_AMOUNT" >
                     <ItemStyle Width="50px" />
                        <ControlStyle Width="50px"/>
                      </asp:BoundField >   
                    <asp:BoundField DataField="BANK_SRV_FEE_AMOUNT" 
                        HeaderText="Fee" SortExpression="BANK_SRV_FEE_AMOUNT">
                      <ItemStyle Width="40px" />
                        <ControlStyle Width="40px"/>
                      </asp:BoundField >   
                      <asp:BoundField DataField="MIN_FEE" 
                        HeaderText="Minimum Fee" SortExpression="MIN_FEE">
                      <ItemStyle Width="40px" />
                        <ControlStyle Width="40px"/>
                      </asp:BoundField >          
                    <asp:BoundField DataField="VAT_TAX" HeaderText="Vat &amp; Tax" 
                        SortExpression="VAT_TAX" >
                     <ItemStyle Width="40px" />
                        <ControlStyle Width="40px"/>
                      </asp:BoundField >   
                     <asp:BoundField DataField="AIT" HeaderText="AIT (%)" 
                        SortExpression="AIT" >
                      <ItemStyle Width="40px" />
                        <ControlStyle Width="40px"/>
                      </asp:BoundField >
                        
                        <asp:BoundField DataField="FEES_PAID_BY_BANK" HeaderText="Fees Paid By Bank (%)" 
                            SortExpression="FEES_PAID_BY_BANK" >
                            <ItemStyle Width="50px" />
                            <ControlStyle Width="50px"/>
                         </asp:BoundField >
                        
                        <asp:BoundField DataField="FEES_PAID_BY_CUSTOMER" HeaderText="Fees Paid By Initiator (%)" 
                        SortExpression="FEES_PAID_BY_CUSTOMER" >
                          <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                        <asp:BoundField DataField="FEES_PAID_BY_AGENT" HeaderText="Fees Paid By Receipent(%)" 
                        SortExpression="FEES_PAID_BY_AGENT" >
                          <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                        <asp:BoundField DataField="BANK_COMM_AMOUNT" HeaderText="Bank Commission (%)" 
                        SortExpression="BANK_COMM_AMOUNT" >
                        <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                        <asp:BoundField DataField="AGENT_COMM_AMOUNT" HeaderText="Agent Commission (%)" 
                        SortExpression="AGENT_COMM_AMOUNT" >
                        <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                         <asp:BoundField DataField="POOL_ADJUSTMENT" HeaderText="Pool Adjustment (%)" 
                        SortExpression="POOL_ADJUSTMENT" >
                        <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                          <asp:BoundField DataField="VENDOR_COMMISSION" HeaderText="Vendor Commission (%)" 
                        SortExpression="VENDOR_COMMISSION" >
                        <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                         <asp:BoundField DataField="THIRD_PARTY_COMMISSION" HeaderText="Third Party Commission (%)" 
                        SortExpression="THIRD_PARTY_COMMISSION" >
                        <ItemStyle Width="70px" />
                            <ControlStyle Width="70px"/>
                        </asp:BoundField >
                        
                         <asp:BoundField DataField="CHANNEL_COMMISSION" HeaderText="Channel Commission (%)" 
                            SortExpression="CHANNEL_COMMISSION" >
                            <ItemStyle Width="70px" />
                                <ControlStyle Width="70px"/>
                         </asp:BoundField >
                          <asp:BoundField DataField="AGENT_OPERATOR_COMI" HeaderText="Common Operator Commission (%)" 
                            SortExpression="AGENT_OPERATOR_COMI" >
                            <ItemStyle Width="70px" />
                                <ControlStyle Width="70px"/>
                         </asp:BoundField >
                         <asp:TemplateField HeaderText="Agent Operator Commission" SortExpression="AGENT_OPARETOR_COMI_ID">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAgntOpertrComID91" runat="server" DataSourceID="sdsAgntOperatorCommi" 
                                        AppendDataBoundItems="true" DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"
                                        SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList187" runat="server" 
                                        DataSourceID="sdsAgntOperatorCommi"  AppendDataBoundItems="True"
                                        DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"  Enabled="False"
                                        SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            <ControlStyle Width="120px" />
                            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Tax Paid By" ItemStyle-Width="60px" SortExpression="TAX_PAID_BY">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList8" runat="server" DataTextField="TAX_PAID_BY" AppendDataBoundItems="true"
                                 DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>'>
                                   <asp:ListItem Value=""></asp:ListItem>
                                   <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                   <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList10" runat="server" DataTextField="TAX_PAID_BY" AppendDataBoundItems="true" 
                                 DataValueField="TAX_PAID_BY" SelectedValue='<%# Bind("TAX_PAID_BY") %>' Enabled="false" >
                                 <asp:ListItem Value=""></asp:ListItem>
                                 <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                 <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                            <ControlStyle Width="80px" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Fees Paid By" ItemStyle-Width="60px" SortExpression="FEES_PAID_BY">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlFeesPaidby1" runat="server" DataTextField="FEES_PAID_BY" AppendDataBoundItems="true"
                                 DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>'>
                                   <asp:ListItem Value=""></asp:ListItem>
                                   <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                   <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                   <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem> 
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlFeesPaidby2" runat="server" DataTextField="FEES_PAID_BY" AppendDataBoundItems="true" 
                                 DataValueField="FEES_PAID_BY" SelectedValue='<%# Bind("FEES_PAID_BY") %>' Enabled="false" >
                                 <asp:ListItem Value=""></asp:ListItem>
                                 <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                 <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                 <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                            <ControlStyle Width="80px" />
                        </asp:TemplateField>
                                             
                        <asp:TemplateField HeaderText="Fees Include Vat/Tax" ItemStyle-Width="50px" SortExpression="FEE_INCLUDE_VAT_TAX">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList11" runat="server" DataTextField="FEE_INCLUDE_VAT_TAX"
                                 DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'>
                                   <asp:ListItem Value=""></asp:ListItem>
                                   <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList13" runat="server"  DataTextField="FEE_INCLUDE_VAT_TAX"
                                 DataValueField="FEE_INCLUDE_VAT_TAX" SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>' Enabled="false" >
                                 <asp:ListItem Value=""></asp:ListItem> 
                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>                                   
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                            <ControlStyle Width="50px" />
                        </asp:TemplateField>      
                      <%--<asp:TemplateField HeaderText="Channel Type" SortExpression="CHANNEL_TYPE_ID">
                          <EditItemTemplate>
                              <asp:DropDownList ID="DropDownList117" runat="server"  AppendDataBoundItems="true"
                                  DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                                  DataValueField="CHANNEL_TYPE_ID"
                                  SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                                 <asp:ListItem Value=""></asp:ListItem>     
                              </asp:DropDownList>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:DropDownList ID="DropDownList116" runat="server"  AppendDataBoundItems="true"
                                  DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                                  DataValueField="CHANNEL_TYPE_ID" Enabled="false"
                                  SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                                  <asp:ListItem Value=""></asp:ListItem> 
                              </asp:DropDownList>
                          </ItemTemplate>
                          <ItemStyle Width="100px" />
                          <ControlStyle Width="100px" />
                    </asp:TemplateField> --%>                   
                    <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                    
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
        <asp:Panel ID="pnlInsert" runat="server">
            <div class="Inser_Panel">
             <strong>
              <span style="COLOR: white"> 
               Add New Service Fee
              </span>
             </strong>
            </div>
            <div>
                <asp:DetailsView ID="dtvServiceFee" runat="server" AutoGenerateRows="False" 
                    DataKeyNames="BANK_SRV_FEE_ID" DataSourceID="sdsServiceFee" 
                    DefaultMode="Insert" Height="70px" Width="280px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" 
                    oniteminserting="dtvServiceFee_ItemInserting" BorderStyle="None" 
                    oniteminserted="dtvServiceFee_ItemInserted" CaptionAlign="Right">
                    <PagerStyle CssClass="pgr" />
                    <Fields>                        
                        <asp:BoundField DataField="BANK_SRV_FEE_TITLE" HeaderText="Fee Name" 
                            SortExpression="BANK_SRV_FEE_TITLE" >
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="START_AMOUNT" HeaderText="Start Amount" 
                            SortExpression="START_AMOUNT"><HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MAX_AMOUNT" HeaderText="Max Amount" 
                            SortExpression="MAX_AMOUNT" ><HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BANK_SRV_FEE_AMOUNT" 
                            HeaderText="Fee" SortExpression="BANK_SRV_FEE_AMOUNT">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="MIN_FEE" 
                            HeaderText="Minimum Fee" SortExpression="MIN_FEE">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="VAT_TAX" HeaderText="VAT & TAX" 
                            SortExpression="VAT_TAX">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AIT" HeaderText="AIT(%)" 
                            SortExpression="AIT">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                                            
                        <asp:BoundField DataField="FEES_PAID_BY_BANK" HeaderText="Fees Paid By Bank (%)" 
                            SortExpression="FEES_PAID_BY_BANK">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FEES_PAID_BY_CUSTOMER" HeaderText="Fess Paid By Initiator(%)" 
                            SortExpression="FEES_PAID_BY_CUSTOMER">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="FEES_PAID_BY_AGENT" HeaderText="Fees Paid By Receipent(%)" 
                        SortExpression="FEES_PAID_BY_AGENT" >
                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField >    
                                               
                       <asp:BoundField DataField="BANK_COMM_AMOUNT" HeaderText="Bank Commission (%)" 
                            SortExpression="BANK_COMM_AMOUNT">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                       
                         <asp:BoundField DataField="AGENT_COMM_AMOUNT" HeaderText="Agent Commission(%)" 
                            SortExpression="AGENT_COMM_AMOUNT">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
             
                          <asp:BoundField DataField="POOL_ADJUSTMENT" HeaderText="Pool Adjustment (%)" 
                            SortExpression="POOL_ADJUSTMENT">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
               
                        <asp:BoundField DataField="VENDOR_COMMISSION" HeaderText="Vendor Commission(%)" 
                            SortExpression="VENDOR_COMMISSION">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>  
                        
                        <asp:BoundField DataField="THIRD_PARTY_COMMISSION" HeaderText="Third Party Commission(%)" 
                            SortExpression="THIRD_PARTY_COMMISSION">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="CHANNEL_COMMISSION" HeaderText="Channel Commission(%)" 
                            SortExpression="CHANNEL_COMMISSION">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                         <asp:BoundField DataField="AGENT_OPERATOR_COMI" HeaderText="Common Operator Commission(%)" 
                            SortExpression="AGENT_OPERATOR_COMI">
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>   
                        
                        <asp:TemplateField HeaderText="Agent Operator Commission" SortExpression="AGENT_OPARETOR_COMI_ID">                   
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlAgntOpCommissionID" runat="server" DataSourceID="sdsAgntOperatorCommi" 
                                    DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"
                                     SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList17" runat="server" DataSourceID="sdsAgntOperatorCommi" 
                                    DataTextField="COMMISSION_NAME" DataValueField="AGENT_OPARETOR_COMI_ID"
                                     SelectedValue='<%# Bind("AGENT_OPARETOR_COMI_ID") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                                                                        
                        <asp:TemplateField  HeaderText="Tax Paid By">
                            <EditItemTemplate>
                               <asp:DropDownList ID="DropDownList111" runat="server" DataTextField="TAX_PAID_BY" DataValueField="TAX_PAID_BY"
                                 SelectedValue='<%# Bind("TAX_PAID_BY") %>'>
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="TAX_PAID_BY" DataValueField="TAX_PAID_BY"
                             SelectedValue='<%# Bind("TAX_PAID_BY") %>'  > 
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList17" runat="server" DataTextField="TAX_PAID_BY" DataValueField="TAX_PAID_BY"
                                SelectedValue='<%# Bind("TAX_PAID_BY") %>' Enabled="false" >
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField  HeaderText="Fees Paid By">
                            <EditItemTemplate>
                               <asp:DropDownList ID="DropDownList113" runat="server" DataTextField="FEES_PAID_BY" DataValueField="FEES_PAID_BY"
                                 SelectedValue='<%# Bind("FEES_PAID_BY") %>'>
                                  <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="DropDownList114" runat="server" DataTextField="FEES_PAID_BY" DataValueField="FEES_PAID_BY"
                             SelectedValue='<%# Bind("FEES_PAID_BY") %>'  > 
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList115" runat="server" DataTextField="FEES_PAID_BY" DataValueField="FEES_PAID_BY"
                                SelectedValue='<%# Bind("FEES_PAID_BY") %>' Enabled="false" >
                                    <asp:ListItem Value="BANK" Text="Bank"></asp:ListItem>
                                    <asp:ListItem Value="CUST" Text="Customer"></asp:ListItem>
                                    <asp:ListItem Value="AGNT" Text="Agent"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Fees Include Vat/Tax">
                            <EditItemTemplate>
                                 <asp:DropDownList ID="DropDownList5" runat="server"  DataTextField="FEE_INCLUDE_VAT_TAX" DataValueField="FEE_INCLUDE_VAT_TAX"  SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'>
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem> 
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>                                  
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="DropDownList4" runat="server"  DataTextField="FEE_INCLUDE_VAT_TAX" DataValueField="FEE_INCLUDE_VAT_TAX"  SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>' >
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>                                  
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList6" runat="server"  DataTextField="FEE_INCLUDE_VAT_TAX" DataValueField="FEE_INCLUDE_VAT_TAX"  SelectedValue='<%# Bind("FEE_INCLUDE_VAT_TAX") %>'>
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="Yes"></asp:ListItem>                                  
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="ChannelType" SortExpression="CHANNEL_TYPE_ID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddl9" runat="server"  AppendDataBoundItems="True"
                                    DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                                    DataValueField="CHANNEL_TYPE_ID" 
                                     SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>' >
                                     <asp:ListItem Value="" Text=""></asp:ListItem>
                                </asp:DropDownList>  
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlChannelType" runat="server" 
                                    DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                                    DataValueField="CHANNEL_TYPE_ID" 
                                    SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>'>
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl8" runat="server"  AppendDataBoundItems="True"
                                    DataSourceID="sdsChannelType" DataTextField="CHANNEL_TYPE" 
                                    DataValueField="CHANNEL_TYPE_ID" 
                                     SelectedValue='<%# Bind("CHANNEL_TYPE_ID") %>' Enabled="False">
                                     <asp:ListItem Value="" Text=""></asp:ListItem>
                                </asp:DropDownList>  
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>--%>
                        <%--<asp:CommandField ShowInsertButton="True" ButtonType="Button" >
                            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
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
            </div>
        </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
