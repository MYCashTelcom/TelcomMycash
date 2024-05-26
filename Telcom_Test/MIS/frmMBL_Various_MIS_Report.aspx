<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBL_Various_MIS_Report.aspx.cs" Inherits="MIS_frmMBL_Various_MIS_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>MIS Various Report</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<style type="text/css">        
        .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         }
         .View_Panel
         {         	
         	 width:550px;       	
         }	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }     
        .style7
        {
            width: 65px;
        }
        .style8
        {
            width: 610px;
        }
        .style9
        {
            width: 532px;
        }
        .style10
        {
            width: 168px;
        }
        .style11
        {
            width: 144px;
        }
        .style12
        {
            width: 83px;
        }
        .style13
        {
            height: 18px;
            width: 65px;
        }
        .style14
        {
            width: 159px;
        }
    </style>
     
</head>
<body style="background-color: lightgrey;">
  <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True" AsyncPostBackTimeout="36000"></ajaxToolkit:ToolkitScriptManager>
     <%--<asp:scriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000"> </asp:scriptManager>--%>
     <%--<asp:scriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000"> </asp:scriptManager>--%>
     
     <asp:SqlDataSource ID="sdsRank" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK 
            WHERE ACCNT_RANK_ID = '120519000000000002' OR 
            ACCNT_RANK_ID = '120519000000000003' OR 
            ACCNT_RANK_ID = '120519000000000004' OR 
            ACCNT_RANK_ID = '120519000000000005' OR 
            ACCNT_RANK_ID = '120519000000000006' OR
            ACCNT_RANK_ID = '170422000000000002' OR
            ACCNT_RANK_ID = '170422000000000003' OR
	    ACCNT_RANK_ID = '161215000000000004' OR
            ACCNT_RANK_ID = '180128000000000008' OR
            ACCNT_RANK_ID = '130914000000000001' OR
            ACCNT_RANK_ID = '170422000000000004'                        
            ORDER BY RANK_TITEL ">
        </asp:SqlDataSource>
     <asp:SqlDataSource ID="sdsServices" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT SERVICE_ACCESS_CODE, SERVICE_ACCESS_CODE||' ('|| SERVICE_TITLE|| ') '  TITLE 
            FROM SERVICE_LIST WHERE SERVICE_STATE = 'A' AND SERVICE_ACCESS_CODE IN('CN','CCT','FT','FM', 'SW', 'QT', 'BD', 'RG', 'MTP' ) ORDER BY TITLE ">            
      </asp:SqlDataSource>
      
     
     
     
     <asp:SqlDataSource ID="sdsAccountList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT COA.ACC_INT_ID,ACC_ID || ' - ' || ACC_NAME AS ACC_NAME FROM BDMIT_ERP_101.GL_CHART_OF_ACC COA WHERE (COA.IS_ACC = 'Y') AND (COA.CLOSE_ACC <> 'Y') AND COA.ACC_INT_ID &#13;&#10;NOT IN (SELECT ACC_INT_ID FROM BDMIT_ERP_101.GL_OPENING_BAL WHERE CMP_BRANCH_ID='100310002' AND GL_ACC_YEAR_ID='10031500001001') ORDER BY ACC_ID">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCommpanyBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM BDMIT_ERP_101.CM_CMP_BRANCH WHERE (CMP_BRANCH_ID = '110102004')">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsOpenBal" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT OB.GL_OPEBAL_ID, OB.ACC_INT_ID, COA.ACC_ID || ' -> ' || COA.ACC_NAME AS ACC_NAME, OB.OPENING_BAL, OB.GL_ACC_YEAR_ID, OB.CURRENT_BAL, OB.CMP_BRANCH_ID FROM BDMIT_ERP_101.GL_OPENING_BAL OB, BDMIT_ERP_101.GL_CHART_OF_ACC COA WHERE OB.ACC_INT_ID = COA.ACC_INT_ID ORDER BY COA.ACC_ID">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAccYear" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT GL_ACC_YEAR_ID, GL_ACC_YEAR_TITLE FROM BDMIT_ERP_101.GL_ACCOUNTING_YEAR ORDER BY GL_ACC_YEAR_TITLE DESC">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsRemainAcc" runat="server"></asp:SqlDataSource>
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
             <table style="width: 100%" align="right" >
               <tr>
                 <td align="left">
                   <asp:Label runat="server" ID="panelQ" Text="Various MIS Report"></asp:Label> 
                 </td>
                 <td align="right">
                   <asp:Label ID="lblMsg" runat="server" ></asp:Label>  
                 </td>  
                 <td align="right" style="width: 50px;">
                   <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
                     <ProgressTemplate>
                       <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;
                     </ProgressTemplate>
                    </asp:UpdateProgress> 
                 </td>  
                </tr>
             </table> 
           </asp:Panel>  
          
        <table style="width: 100%">
         <tr>
          <td class="style9">
            <fieldset style="width: 600px;">
             <legend style="color: black"><strong>Daily Transaction Report </strong></legend>
              <table style="width: 100%;" align="right" >
               <tr>
                <td class="style12">&nbsp;</td> 
                <td>
                 <asp:Label ID="Label9" runat="server"><strong>Choose Rank:</strong></asp:Label>
                 <asp:CheckBox ID="checkBoxRank" runat="server" AutoPostBack="True" Font-Bold="True" Text="Disable" />
                </td>
               </tr>
                      
               <tr>
                <td class="style12">
                 <asp:Label runat="server" ID="lblService"><strong>Service List:</strong></asp:Label> 
                </td> 
                <td class="style10">
                 <asp:DropDownList runat="server" ID="ddlServiceCode" DataSourceID="sdsServices" DataTextField="TITLE" DataValueField="SERVICE_ACCESS_CODE" AutoPostBack="True"/>
                </td>
               </tr>
               
               <tr>
                <td class="style12">
                 <asp:Label runat="server" ID="lblSrcRank"><strong>Source Rank:</strong></asp:Label> 
                </td>  
                <td class="style10">
                 <asp:DropDownList runat="server" ID="ddlSourceRank" DataSourceID="sdsRank" 
                  DataValueField="ACCNT_RANK_ID" DataTextField="RANK_TITEL" AutoPostBack="True"/>
                </td>
                <td class="style11">
                 <asp:Label runat="server" ID="lblDestRank"><strong>Destination Rank:</strong></asp:Label> 
                </td>
                <td>
                 <asp:DropDownList runat="server" ID="ddlDestinationRank" DataSourceID="sdsRank" 
                  DataValueField="ACCNT_RANK_ID" DataTextField="RANK_TITEL" AutoPostBack="True"/> 
                </td>
               </tr> 
               
               <tr>
                <td class="style12">
                 <asp:Label runat="server" ID="lblFD"><strong>From Date:</strong></asp:Label> 
                </td> 
                <td class="style10">
                 <cc1:GMDatePicker ID="dtpFDate" runat="server" CalendarTheme="Silver" 
                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                  TextBoxWidth="100">
                  <calendartitlestyle backcolor="#FFFFC0" />
                 </cc1:GMDatePicker> 
                </td> 
                <td class="style11">
                 <asp:Label runat="server" ID="lblTD"><strong>To Date:</strong></asp:Label> 
                </td>
                <td>
                 <cc1:GMDatePicker ID="dtpTDate" runat="server" CalendarTheme="Silver" 
                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                  TextBoxWidth="100">
                  <calendartitlestyle backcolor="#FFFFC0" />
                 </cc1:GMDatePicker> 
                </td>
               </tr>
                     
               <tr>
                <td class="style12"></td> 
                <td class="style10"></td> 
                <td class="style11">
                 <asp:Button ID="btnReport" runat="server" onclick="btnReport_Click" Text="Show Report" Width="110px" />
                </td>
                <td>&nbsp;</td>  
               </tr>   
              </table>
            </fieldset> 
          </td> 
               
          <td class="style8">
           <fieldset style="width: 400px">
            <legend><strong style="color: black">Daily DPS Account Opening Report</strong></legend>
             <table style="width: 100%">
              <tr>
               <td class="style7">
                <asp:Label ID="Label2" runat="server" Text="From Date:" Font-Bold="True"></asp:Label>
               </td>
               <td class="style14">
                <cc1:GMDatePicker ID="dtpDate" runat="server"  CalendarTheme="Silver" Enabled="True"
                 DateFormat="dd-MMM-yyyy " MinDate="1900-01-01" Style="position: relative" 
                 TextBoxWidth="90"><calendartitlestyle  />
               </cc1:GMDatePicker>
               </td>
               <td class="style4">
                <asp:Label ID="lblTDate" runat="server" Text="To Date:" Font-Bold="True"></asp:Label>              
               </td>
               <td>
                <cc1:GMDatePicker ID="dtpToAODate" runat="server"  CalendarTheme="Silver" 
                 DateFormat="dd-MMM-yyyy " MinDate="1900-01-01" Style="position: relative" 
                 TextBoxWidth="90"><calendartitlestyle  />
                </cc1:GMDatePicker>
               </td>
              </tr>
              <tr>
               <td class="style7"></td>
               <td class="style14">
                <asp:Button ID="btnAccOpenReport" runat="server"  Text="Show Report" Width="99px" onclick="btnAccOpenReport_Click" />
               </td>
              </tr>
              <tr>
               <td class="style13">&nbsp;</td>
              </tr>
              <tr>
               <td class="style13">&nbsp;</td>
              </tr>
              <tr>
               <td class="style13">&nbsp;</td>
              </tr>
             </table>
           </fieldset>
          </td> 
         </tr>
              
         <tr>
          <td class="style8">
           <fieldset style="width: 600px;">
            <legend><strong style="color: black">Customer KYC Pending Status Report</strong></legend>
            <table>
             <tr>
              <td>
               <asp:Label ID="Label3" runat="server" Text="From Date :" Font-Bold="True"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpKYCFDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
              <td>
               <asp:Label ID="Label4" runat="server" Text="To Date :" Font-Bold="True" ></asp:Label> 
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpKYCToDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0" />
               </cc1:GMDatePicker>
              </td>
             </tr>
             
             <tr>
              <td></td> 
              <td></td>
              <td>&nbsp;</td>
              <td>
               <asp:Button ID="btnKYCRPT" runat="server" onclick="btnKYCRPT_Click" Text="Show Report" />
              </td>
             </tr>
            </table>
           </fieldset> 
          </td>
          
          
          <td class="style8">
           <fieldset style="width: 400px; height: 60px;">
            <legend><strong>Pending/Idle Customer Status Report</strong></legend>
            <table>
             <tr>
              <td>
               <asp:Label ID="Label5" runat="server" ><strong>From Date:</strong></asp:Label>
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpCusFDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
              <td>
               <asp:Label ID="Label6" runat="server" ><strong>To Date:</strong></asp:Label>
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpCusTDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
             </tr>
             
             <tr>
              <td>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               &nbsp;&nbsp;
              </td>
              <td>
               <asp:Button ID="btnIdleCust" runat="server" Text="Show Report" onclick="btnIdleCust_Click" />
              </td>
             </tr>
            </table>
           </fieldset>
          </td>
         </tr>
         
         <tr>
          <td>
           <fieldset style="width: 600px;">
            <legend><strong>Daily Interim MIS Report</strong></legend>
            <table>
             <tr class="style8">
              <td>
               <asp:Label runat="server" ID="lblInterim"><strong>Date:</strong></asp:Label>
                  (<asp:Label runat="server" ID="lblBr" Text="Branch" Enabled="False"></asp:Label>
                  <asp:DropDownList ID="ddlCmpBranchList" runat="server" 
                      DataSourceID="sdsCommpanyBranch" DataTextField="CMP_BRANCH_NAME" 
                      DataValueField="CMP_BRANCH_ID" Enabled="False">
                  </asp:DropDownList>
                  
                  <asp:Label runat="server" ID="lblType" Text="Type"></asp:Label>
                  <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                      Enabled="False">
                        <asp:ListItem Value="AL">Asset/Liabilities Statement</asp:ListItem>
                  </asp:DropDownList>
                  
                  <asp:Label runat="server" ID="lblYear" Text="Year"></asp:Label>
                  <asp:DropDownList ID="ddlAccYear" runat="server" DataSourceID="sdsAccYear"
                        DataTextField="GL_ACC_YEAR_TITLE" DataValueField="GL_ACC_YEAR_ID" 
                      Enabled="False"> </asp:DropDownList>
                  
                  
                  <asp:Label runat="server" ID="lblAsOffDate" Text="As Off Date"></asp:Label>
                  <cc1:GMDatePicker ID="dtpAsOff" runat="server" DateFormat="dd-MMM-yyyy" Visible="true"
                      Enabled="false"   CalendarTheme="Blue"> </cc1:GMDatePicker>
                  <asp:CheckBox ID="chkAllAccount" runat="server" Text="Include All Account" 
                      Enabled="False" />
                  )
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpInterim" runat="server"  CalendarTheme="Silver" Enabled="False"
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
             </tr>
             
             <tr>
              <td>
                  <asp:HiddenField ID="HDFDistributorEndBal" runat="server" />
                  <asp:HiddenField ID="HDFAgentEndBal" runat="server" />
                  <asp:HiddenField ID="HiddenField3" runat="server" />
                  <asp:HiddenField ID="HiddenField4" runat="server" />
                  <asp:HiddenField ID="hdfSalDisbAmt" runat="server" />
                  
              </td>
              <td>
               <asp:Button runat="server" ID="btnInterimMISReport" Text="Show Report" onclick="btnInterimMISReport_Click" />
              </td>
             </tr>
            </table>
           </fieldset>
          </td>
             
          <td class="style8" >
           <fieldset style="width: 400px;">
            <legend><strong>Daily Agent Balance</strong></legend>
            <table>
             <tr>
              <td>
               <asp:Label runat="server" ID="Label7"><strong>From Date:</strong></asp:Label>
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpAgentBalanceFDate" runat="server"  CalendarTheme="Silver" Enabled="False"
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
              <td>
               <asp:Label runat="server" ID="Label8"><strong>To Date:</strong></asp:Label>
              </td>
              <td>
               <cc1:GMDatePicker ID="dtpAgentBalanceToDate" runat="server"  CalendarTheme="Silver" Enabled="False"
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
             </tr>
             
             <tr>
              <td></td>
              <td>
               <asp:Button runat="server" ID="btnAgentBalance" Text="Show Report" onclick="btnAgentBalance_Click"/>
              </td>
              <td></td>
              <td></td>
             </tr>
            </table>
           </fieldset>
          </td>
         </tr>
         
         <tr class="style8">
             <td>
                 <fieldset style="width: 600px;">
                     <legend><strong>Distributor wise Agent List Till Today</strong></legend>
                     <table>
                         <tr >
                             <td>
                                <asp:Label runat="server" ID="lblag"><strong>Date:</strong></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                             </td>
                             <td>
                                 <cc1:GMDatePicker ID="dtpDisAgent" runat="server"  CalendarTheme="Silver" Enabled="False"
                                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                  TextBoxWidth="110" >
                                  <calendartitlestyle backcolor="#FFFFC0"   />
                                </cc1:GMDatePicker>
                             </td>
                         </tr>
                         <tr>
                             <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             &nbsp;</td>
                             <td>
                                 <asp:Button runat="server" ID="btnDisAgent" Text="Show Report" 
                                     onclick="btnDisAgent_Click"></asp:Button>
                             </td>
                         </tr>
                     </table>
                 </fieldset>
             </td>
             <td class="style8">
                 <fieldset style="width: 400px;">
                     <legend><strong>Distributor's Performance Report</strong></legend>
                     <table style="width: 400px;">
                         <tr>
                             <td>
                                 <asp:Label runat="server" ID="lblDisPer"><strong>Date:</strong></asp:Label>
                             </td>
                             <td>
                                  <cc1:GMDatePicker ID="dtpDisPerformance" runat="server"  CalendarTheme="Silver" Enabled="False"
                                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                  TextBoxWidth="110" >
                                  <calendartitlestyle backcolor="#FFFFC0"   />
                                </cc1:GMDatePicker>
                             </td>
                         </tr>
                         <tr>
                             <td></td>
                             <td>
                                 <asp:Button runat="server" ID="btnDisPerformance" Text="Show Report" 
                                     onclick="btnDisPerformance_Click"/>
                             </td>
                             <td></td>
                             <td></td>
                         </tr>
                     </table>
                 </fieldset>
             </td>
         </tr>
              <tr>
                  <td class="style8">
                    <fieldset style="width: 600px;">
                        <legend><strong>Agent's Performance Report</strong></legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAgentPer"><strong>Date:</strong></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                  <cc1:GMDatePicker ID="dtpAgentPerformance" runat="server"  CalendarTheme="Silver" Enabled="False"
                                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                  TextBoxWidth="110" >
                                  <calendartitlestyle backcolor="#FFFFC0"   />
                                </cc1:GMDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" ID="btnAgentPerformanceRpt" Text="Show Report" 
                                        onclick="btnAgentPerformanceRpt_Click"/>
                                </td>
                            </tr>
                        </table>
                    </fieldset>  
                  </td>
                  <td>
                      <fieldset style="width: 400px;">
                          <legend><strong>Agent Commission Reoprt as per Customer Transaction(1st Month)</strong></legend>
                          <table>
                              <tr>
                                  <td>
                                      <asp:Label runat="server" ID="lblAgentComm"><strong>Date:</strong></asp:Label>
                                  </td>
                                  <td>
                                      <cc1:GMDatePicker ID="dtpAgentCommFstMonth" runat="server"  CalendarTheme="Silver" Enabled="False"
                                  DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                  TextBoxWidth="110" >
                                  <calendartitlestyle backcolor="#FFFFC0"   />
                                </cc1:GMDatePicker>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      
                                  </td>
                                  <td>
                                      <asp:Button runat="server" ID="btnAgentCommFM" Text="Show Report" 
                                          onclick="btnAgentCommFM_Click"/>
                                  </td>
                              </tr>
                          </table>
                      </fieldset>
                  </td>
              </tr>
              <tr class="style8">
                  <td>
                      <fieldset style="width: 600px;">
                          <legend><strong>Agent Commission Reoprt as per Customer Transaction(2nd Month)</strong></legend>
                          <table>
                              <tr>
                                  <td>
                                      <asp:Label runat="server" ID="lblAgentComm2nd"><strong>Date:</strong></asp:Label>
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  </td>
                                  <td>
                                      <cc1:GMDatePicker ID="dtpAgentComm2ndMonth" runat="server"  CalendarTheme="Silver" Enabled="False"
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                                  </td>
                              </tr>
                              <tr>
                                  <td></td>
                                  <td>
                                      <asp:Button runat="server" ID="btnAgentConn2ndMonth" Text="Show Report" onclick="btnAgentConn2ndMonth_Click"/>
                                  </td>
                              </tr>
                          </table>
                      </fieldset>
                  </td>
                  <td>
                      <fieldset style="width: 400px;">
                          <legend><strong>Daily Agent Aquisition Report</strong></legend>
                          <table style="width: 400px;">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAgentAqui"><strong>From Date:</strong></asp:Label>
                                </td>
                                <td>
                                    <cc1:GMDatePicker ID="dtpAgentAquiFromDate" runat="server"  CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="Label1"><strong>To Date:</strong></asp:Label>
                                </td>
                                <td>
                                    <cc1:GMDatePicker ID="dtpAgentAquiToDate" runat="server"  CalendarTheme="Silver" 
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" ID="btnAgentAquisiotionRpt" Text="Show Report" 
                                        onclick="btnAgentAquisiotionRpt_Click" Enabled="true"/>
                                </td>
                            </tr>  
                          </table>
                      </fieldset>
                  </td>
              </tr>
              
              <tr>
                  <td class="style8">
                      <fieldset style="width: 600px;">
                          <legend><strong>Customer A/C Approve and KYC Update Details Report</strong></legend>
                          <table style="width: 600px">
                              <tr>
                                  <td>
                                      <asp:Label runat="server" ID="lblCustnKycF"><strong>From Date:</strong></asp:Label>
                                  </td>
                                  <td>
                                      <cc1:GMDatePicker ID="dtpCustNKycFDate" runat="server"  CalendarTheme="Silver" Enabled="True"
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                                  </td>
                                  <td>
                                      <asp:Label runat="server" ID="lblCustnKycT"><strong>To Date:</strong> </asp:Label>
                                  </td>
                                  <td>
                                      <cc1:GMDatePicker ID="dtpCustNKycToDate" runat="server"  CalendarTheme="Silver" Enabled="True"
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                                  </td>
                              </tr>
                              <tr>
                                  <td></td>
                                  <td>
                                      <asp:Button runat="server" ID="btnCustAprvNKyc" Text="Show Report" 
                                          onclick="btnCustAprvNKyc_Click"/>
                                  </td>
                              </tr>
                          </table>
                      </fieldset>
                  </td>
                  <td>
                      <fieldset style="width: 400px;">
                          <legend><strong>Customer A/C Approve and KYC Update Summary Report</strong></legend>
                          <table style="width: 400px">
                              <tr>
                                  <td>
                                  <asp:Label runat="server" ID="lblDateSummary"><strong>Date:</strong></asp:Label>
                              </td>
                              <td>
                                  <cc1:GMDatePicker ID="GMDatePicker1" runat="server"  CalendarTheme="Silver" Enabled="False"
                                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                                        TextBoxWidth="110" >
                                        <calendartitlestyle backcolor="#FFFFC0"   />
                                      </cc1:GMDatePicker>
                              </td>
                              </tr>
                              <tr>
                                  <td></td>
                                  <td>
                                      <asp:Button runat="server" ID="btnCustAprvNKycSummary" Text="Show Report" 
                                          onclick="btnCustAprvNKycSummary_Click"/>
                                  </td>
                              </tr>
                              
                          </table>
                      </fieldset>
                  </td>
              </tr>
             <tr>

             <td>
           <fieldset style="width: 600px;">
            <legend><strong>Daily Interim MIS Report By Date</strong></legend>
            <table>
             <tr class="style8">
              <td>
               <asp:Label runat="server" ID="Label10"><strong>Date:</strong></asp:Label>
                  (<asp:Label runat="server" ID="Label11" Text="Branch" Enabled="False"></asp:Label>
                  <asp:DropDownList ID="DropDownList1" runat="server" 
                      DataSourceID="sdsCommpanyBranch" DataTextField="CMP_BRANCH_NAME" 
                      DataValueField="CMP_BRANCH_ID" Enabled="False">
                  </asp:DropDownList>
                  
                  <asp:Label runat="server" ID="Label12" Text="Type"></asp:Label>
                  <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                      Enabled="False">
                        <asp:ListItem Value="AL">Asset/Liabilities Statement</asp:ListItem>
                  </asp:DropDownList>
                  
                  <asp:Label runat="server" ID="Label13" Text="Year"></asp:Label>
                  <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsAccYear"
                        DataTextField="GL_ACC_YEAR_TITLE" DataValueField="GL_ACC_YEAR_ID" 
                      Enabled="False"> </asp:DropDownList>
                  
                  
                  <asp:Label runat="server" ID="Label14" Text="As Off Date"></asp:Label>
                  <cc1:GMDatePicker ID="GMDatePicker2" runat="server" DateFormat="dd-MMM-yyyy" Visible="true"
                      Enabled="false"   CalendarTheme="Blue"> </cc1:GMDatePicker>
                  <asp:CheckBox ID="CheckBox1" runat="server" Text="Include All Account" 
                      Enabled="False" />
                  )
              </td>
              <td>
               <cc1:GMDatePicker ID="vDateP" runat="server"  CalendarTheme="Silver" Enabled="True"
                DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative;" 
                TextBoxWidth="110" >
                <calendartitlestyle backcolor="#FFFFC0"   />
               </cc1:GMDatePicker>
              </td>
             </tr>
             
             <tr>
             
              <td>
               <asp:Button runat="server" ID="btnInterimMISReportWD" Text="Show Report" onclick="btnInterimMISReportWD_Click" />
              </td>
             </tr>
            </table>
           </fieldset>
          </td>

         </tr>
        </table> 
     </ContentTemplate>
      <Triggers>
      <%-- <asp:AsyncPostBackTrigger ControlID="btnReport" EventName="Click" />--%>
         <%--<asp:PostBackTrigger ControlID="btnReport" />
         <asp:PostBackTrigger ControlID="btnAccOpenReport"/>
         <asp:PostBackTrigger ControlID="btnView"/> 
         <asp:PostBackTrigger ControlID="btnBankDepo"/>
         <asp:PostBackTrigger ControlID="btnKYCRPT"/>
         <asp:PostBackTrigger ControlID="btnIdleCust"/>
         <asp:PostBackTrigger ControlID="btnTopUp"/>
         <asp:PostBackTrigger ControlID="btnAgentBalance"/>
         <asp:PostBackTrigger ControlID="btnInterimMISReport"/>--%>
         <%--<asp:AsyncPostBackTrigger ControlID="btnReport" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnAccOpenReport" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnBankDepo" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnKYCRPT" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnIdleCust" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnTopUp" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnAgentBalance" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnInterimMISReport" EventName="Click" />--%>
         
         <asp:PostBackTrigger ControlID="btnReport" />
         <asp:PostBackTrigger ControlID="btnAccOpenReport"/>
         <asp:PostBackTrigger ControlID="btnKYCRPT"/>
         <asp:PostBackTrigger ControlID="btnIdleCust"/>
         <asp:PostBackTrigger ControlID="btnAgentBalance"/>
         <asp:PostBackTrigger ControlID="btnInterimMISReport"/>
           <asp:PostBackTrigger ControlID="btnInterimMISReportWD"/>
         <asp:PostBackTrigger ControlID="btnDisAgent"/>
         <asp:PostBackTrigger ControlID="btnDisPerformance"/>
         <asp:PostBackTrigger ControlID="btnAgentPerformanceRpt"/>
         <asp:PostBackTrigger ControlID="btnAgentCommFM"/>
         <asp:PostBackTrigger ControlID="btnAgentConn2ndMonth"/>
         <asp:PostBackTrigger ControlID="btnAgentAquisiotionRpt"/>
         <asp:PostBackTrigger ControlID="btnCustAprvNKyc"/>
         <asp:PostBackTrigger ControlID="btnCustAprvNKycSummary"/>
         
      </Triggers>
     
    </asp:UpdatePanel>
    
    
  </form>
</body>
</html>
