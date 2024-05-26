<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngBankServiceFeeReport.aspx.cs" Inherits="BANKING_frmMngBankServiceFeeReport" %>

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
        .style6
        {
            width: 28px;
        }
        .style7
        {
            width: 30px;
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
            
          <asp:SqlDataSource ID="sdsBranch" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH">
        </asp:SqlDataSource>
        
         <asp:SqlDataSource ID="sdsBankList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                   ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                   
             SelectCommand="SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS = 'A' ORDER BY BANK_NAME">
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
                InsertCommand="INSERT INTO &quot;BANK_SERVICE_FEE&quot; (&quot;SERVICE_ID&quot;, &quot;BANK_SRV_FEE_ID&quot;, &quot;BANK_SRV_FEE_TITLE&quot;, &quot;START_AMOUNT&quot;, &quot;MAX_AMOUNT&quot;, &quot;BANK_SRV_FEE_AMOUNT&quot;, &quot;VAT_TAX&quot;, &quot;SERVICE_PKG_ID&quot;, &quot;ACCNT_RANK_ID&quot;, &quot;TAX_PAID_BY&quot;, &quot;FEE_INCLUDE_VAT_TAX&quot;, &quot;FEES_PAID_BY_BANK&quot;, &quot;FEES_PAID_BY_CUSTOMER&quot;, &quot;AIT&quot;, &quot;AGENT_COMM_AMOUNT&quot;, &quot;BANK_COMM_AMOUNT&quot;, &quot;FEES_PAID_BY&quot;, &quot;POOL_ADJUSTMENT&quot;, &quot;VENDOR_COMMISSION&quot;, &quot;THIRD_PARTY_COMMISSION&quot;, &quot;CHANNEL_COMMISSION&quot;, &quot;CHANNEL_TYPE_ID&quot;, &quot;FEES_PAID_BY_AGENT&quot;, &quot;MIN_FEE&quot;, &quot;CMP_BRANCH_ID&quot;, &quot;BANK_CODE&quot;) VALUES (:SERVICE_ID, :BANK_SRV_FEE_ID, :BANK_SRV_FEE_TITLE, :START_AMOUNT, :MAX_AMOUNT, :BANK_SRV_FEE_AMOUNT, :VAT_TAX, :SERVICE_PKG_ID, :ACCNT_RANK_ID, :TAX_PAID_BY, :FEE_INCLUDE_VAT_TAX, :FEES_PAID_BY_BANK, :FEES_PAID_BY_CUSTOMER, :AIT, :AGENT_COMM_AMOUNT, :BANK_COMM_AMOUNT, :FEES_PAID_BY, :POOL_ADJUSTMENT, :VENDOR_COMMISSION, :THIRD_PARTY_COMMISSION, :CHANNEL_COMMISSION, :CHANNEL_TYPE_ID,:FEES_PAID_BY_AGENT, :MIN_FEE,:CMP_BRANCH_ID,:BANK_CODE)" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT * FROM BANK_SERVICE_FEE WHERE (SERVICE_ID=:SERVICE_ID) AND (ACCNT_RANK_ID=:ACCNT_RANK_ID) AND (CHANNEL_TYPE_ID=:CHANNEL_TYPE_ID) AND (CMP_BRANCH_ID=:CMP_BRANCH_ID) AND (BANK_CODE=:BANK_CODE)" 
                
                UpdateCommand="UPDATE &quot;BANK_SERVICE_FEE&quot; SET &quot;SERVICE_ID&quot; = :SERVICE_ID, &quot;BANK_SRV_FEE_TITLE&quot; = :BANK_SRV_FEE_TITLE, &quot;START_AMOUNT&quot; = :START_AMOUNT, &quot;MAX_AMOUNT&quot; = :MAX_AMOUNT, &quot;BANK_SRV_FEE_AMOUNT&quot; = :BANK_SRV_FEE_AMOUNT, &quot;VAT_TAX&quot; = :VAT_TAX, &quot;SERVICE_PKG_ID&quot; = :SERVICE_PKG_ID, &quot;ACCNT_RANK_ID&quot; = :ACCNT_RANK_ID, &quot;TAX_PAID_BY&quot; = :TAX_PAID_BY, &quot;FEE_INCLUDE_VAT_TAX&quot; = :FEE_INCLUDE_VAT_TAX, &quot;FEES_PAID_BY_BANK&quot; = :FEES_PAID_BY_BANK, &quot;FEES_PAID_BY_CUSTOMER&quot; = :FEES_PAID_BY_CUSTOMER,&quot;FEES_PAID_BY_AGENT&quot; =:FEES_PAID_BY_AGENT,&quot;AIT&quot; = :AIT, &quot;AGENT_COMM_AMOUNT&quot; = :AGENT_COMM_AMOUNT, &quot;BANK_COMM_AMOUNT&quot; = :BANK_COMM_AMOUNT, &quot;FEES_PAID_BY&quot; = :FEES_PAID_BY, &quot;POOL_ADJUSTMENT&quot; = :POOL_ADJUSTMENT, &quot;VENDOR_COMMISSION&quot; = :VENDOR_COMMISSION, &quot;THIRD_PARTY_COMMISSION&quot; = :THIRD_PARTY_COMMISSION, &quot;CHANNEL_COMMISSION&quot; = :CHANNEL_COMMISSION, &quot;MIN_FEE&quot; = :MIN_FEE WHERE &quot;BANK_SRV_FEE_ID&quot; = :BANK_SRV_FEE_ID">
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
                   <%-- <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />--%>
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
                   <%-- <asp:Parameter Name="CHANNEL_TYPE_ID" Type="String" />--%>
                    <asp:ControlParameter ControlID="ddlChannelName" Name="CHANNEL_TYPE_ID" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlBankList" Name="BANK_CODE" PropertyName="SelectedValue" />
                    
                </InsertParameters>
            </asp:SqlDataSource>
           
     <asp:Panel ID="pnlTop" runat="server">
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
     <asp:panel ID="pnlView" runat="server">
     <fieldset style="border-color: #FFFFFF; width:400px; height:96px">
            <legend><strong style="color: #666666">  Manage Service Rate Report</strong></legend>
       <table>
        <tr>
        <td class="style6"></td>
         <td>
              Select Report type
         </td>
        </tr>
        <tr>
          <td class="style6">
             
          </td>
          <td>
              <asp:RadioButtonList ID="rblSelectType" runat="server"  RepeatDirection="Horizontal" Height="30px" 
                  Width="320px">
                 <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                 <asp:ListItem Value="1" >Bankwise</asp:ListItem>
                 <asp:ListItem Value="2" >Servicewise</asp:ListItem>
                 <asp:ListItem Value="3" >Rankwise</asp:ListItem>
                 <asp:ListItem Value="4" >Channelwise</asp:ListItem>
              </asp:RadioButtonList>
          </td>
        </tr>
        <tr>
          <td class="style6"></td>
          <td>
              <asp:Button ID="btnView" runat="server" Text="View" Width="65px" 
                  onclick="btnView_Click" />
          </td>
        </tr> 
       </table>
        </fieldset></asp:panel>
      <asp:panel ID="Panel1" runat="server">
       <fieldset style="border-color: #FFFFFF; width:400px; height:96px">
            <legend><strong style="color: #666666">Manage Bank Fee Wave and Restriction</strong></legend>
            
             <table>
        <tr>
        <td class="style6">
         </td>
         <td>
              Select Report type Bank Fee Wave
         </td>
         
        </tr>
        <tr>
          <td class="style6">
             
          </td>
          <td>
              <asp:RadioButtonList ID="rblSelectBankFeeWave" runat="server" RepeatDirection="Horizontal" Height="30px" 
                  Width="170px">
                 <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                 <asp:ListItem Value="1" >Bankwise</asp:ListItem>
                 <asp:ListItem Value="2" >Servicewise</asp:ListItem>
                 <%--<asp:ListItem Value="3" >Rankwise</asp:ListItem>
                 <asp:ListItem Value="4" >Channelwise</asp:ListItem>--%>
              </asp:RadioButtonList>
          </td>
        </tr>
        <tr>
          <td class="style6"></td>
          <td>
              <asp:Button ID="btnExport" runat="server"   Text="Export" 
                  onclick="btnExport_Click" Width="56px" />
          </td>
        </tr> 
       </table>
            </fieldset></asp:panel>
       
        <asp:panel ID="Panel2" runat="server">
       <fieldset style="border-color: #FFFFFF; width:400px; height:96px">
            <legend><strong style="color: #666666">Transaction Limit and Commission</strong></legend>
            
             <table>
        <tr>
        <td class="style7"></td>
         <td>
              Select Report type Transaction Limit and Commission
         </td>
         
        </tr>
        <tr>
          <td class="style7">
             
          </td>
          <td>
              <asp:RadioButtonList ID="rblTransactionLimitCommisssion" runat="server" RepeatDirection="Horizontal" Height="30px" 
                  Width="170px">
                 <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                 <asp:ListItem Value="1" >Bankwise</asp:ListItem>
                 <asp:ListItem Value="2" >Servicewise</asp:ListItem>
                 <%--<asp:ListItem Value="3" >Rankwise</asp:ListItem>
                 <asp:ListItem Value="4" >Channelwise</asp:ListItem>--%>
              </asp:RadioButtonList>
          </td>
        </tr>
        <tr>
          <td class="style7"></td>
          <td>
              <asp:Button ID="btnExportTransLimitComm" runat="server"   Text="Export" 
                  Width="56px" onclick="btnExportTransLimitComm_Click1" />
          </td>
        </tr> 
       </table>
            </fieldset></asp:panel>
     </ContentTemplate>
      <Triggers>
         <asp:PostBackTrigger ControlID="btnView" />
          <asp:PostBackTrigger ControlID="btnExport" />
           <asp:PostBackTrigger ControlID="btnExportTransLimitComm" />
      </Triggers> 
    </asp:UpdatePanel>
    </form>
</body>

</html>
