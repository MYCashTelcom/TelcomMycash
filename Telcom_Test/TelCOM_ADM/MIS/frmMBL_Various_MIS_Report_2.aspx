<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBL_Various_MIS_Report_2.aspx.cs" Inherits="MIS_frmMBL_Various_MIS_Report_2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>MIS Various Report 2</title>
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
        </style>
     
</head>

<body style="background-color: lightgrey;">
  <form id="form1" runat="server">
   <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True" AsyncPostBackTimeout="36000"></ajaxToolkit:ToolkitScriptManager>   
    <asp:SqlDataSource ID="sdsRank" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK 
            WHERE ACCNT_RANK_ID = '120519000000000002' OR 
            ACCNT_RANK_ID = '120519000000000003'OR 
            ACCNT_RANK_ID = '120519000000000004' OR 
            ACCNT_RANK_ID = '120519000000000005' OR 
            ACCNT_RANK_ID = '120519000000000006' OR
            ACCNT_RANK_ID = '131205000000000001' OR
            ACCNT_RANK_ID = '141105000000000001' OR
            ACCNT_RANK_ID = '130922000000000004' OR
            ACCNT_RANK_ID = '130922000000000002' OR
            ACCNT_RANK_ID = '130922000000000003' OR
            ACCNT_RANK_ID = '140410000000000004' OR 
            ACCNT_RANK_ID = '130914000000000001'
            ORDER BY RANK_TITEL ">
        </asp:SqlDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
       <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Width="1100px">
             <table style="width: 1100px" align="right" >
               <tr>
                 <td align="left">
                   <asp:Label runat="server" ID="panelQ" Text="Various MIS Report 2"></asp:Label> 
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
           
           <table >
            <tr>
             <td style="width: 420px">
               <fieldset style="width: 420px">
                <legend><strong>Distributor wise Agent Registration Report</strong></legend>
                 <table style="width: 420px">
                  <tr>
                   <td>
                     <asp:Label runat="server" ID="lblDisAgFromDate"><strong>From Date:</strong></asp:Label>  
                   </td>
                   <td>
                      <cc1:GMDatePicker ID="dtpDisAgFrDate" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="100">
                        <calendartitlestyle backcolor="#FFFFC0" />
                      </cc1:GMDatePicker>  
                   </td>
                   <td>
                     <asp:Label runat="server" ID="lblDisAgToDate"><strong>To Date</strong></asp:Label>  
                   </td>
                   <td>
                      <cc1:GMDatePicker ID="dtpDisAgToDate" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="100">
                        <calendartitlestyle backcolor="#FFFFC0" />
                      </cc1:GMDatePicker> 
                   </td>   
                  </tr>
                  <tr>
                   <td></td>
                   <td>
                    <asp:Button runat="server" ID="btnDisAgentReg" Text="Show Report" 
                           onclick="btnDisAgentReg_Click"/>      
                   </td>
                   <td></td>
                   <td></td>
                  </tr>   
                 </table>        
              </fieldset>   
             </td>
             <td style="width: 420px">
                <fieldset style="width: 420px">
                    <legend><strong>District Wise My Cash Sales Report</strong></legend>
                    <table style="width: 420px;">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblDWSFDate"><strong>From Date</strong></asp:Label>
                            </td>
                            <td>
                                <cc1:GMDatePicker ID="dtpDistrictFDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDistTD"><strong>To Date</strong></asp:Label>
                            </td>
                            <td>
                                <cc1:GMDatePicker ID="dtpDistrictToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                               <asp:Button runat="server" ID="btnDistrictWiseSales" Text="Show Report" 
                                    onclick="btnDistrictWiseSales_Click"/> 
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset> 
             </td>
            </tr>
            
            <tr>
              <td style="width: 420px" >
                 <fieldset style="width: 420px">
                    <legend><strong>Distributor Agent wise Transaction Performance Report</strong></legend>
                    <table style="width: 420px">
                        <tr>
                           <td>
                               <asp:Label runat="server" ID="lblDisAgentFromDate"><strong>From Date</strong></asp:Label>
                           </td> 
                           <td>
                               <cc1:GMDatePicker ID="dtpDisAgentFromDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                           </td>
                           <td>
                               <asp:Label runat="server" ID="lblDisAgentToDate"><strong>To Date</strong></asp:Label>
                           </td> 
                           <td>
                               <cc1:GMDatePicker ID="dtpDisAgentToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                           </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="btnDisAgentPerformCount" Text="Show Report" 
                                    onclick="btnDisAgentPerformCount_Click"/>
                            </td>
                            <td></td>
                            <td></td>
                            
                        </tr>
                    </table>
                     
                 </fieldset> 
              </td>
              
              <td style="width: 420px"> 
              <fieldset style="width: 420px">
                  <legend><strong>Business Collection Report1</strong></legend>
                  <table style="width: 420px">
                      <tr>
                          <td>
                              <asp:Label runat="server" ID="lblBus1FDa"><strong>From Date</strong></asp:Label>
                          </td>
                          <td>
                              <cc1:GMDatePicker ID="dtpBusRpt1FDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                          </td>
                          <td>
                              <asp:Label runat="server" ID="lblbus1TDa"><strong>To Date</strong></asp:Label>
                          </td>
                          <td>
                              <cc1:GMDatePicker ID="dtpBusRpt1ToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                          </td>
                      </tr>
                      <tr>
                          <td></td>
                          <td>
                              <asp:Button runat="server" ID="btnBusinessRpt1" Text="Show Report" 
                                  onclick="btnBusinessRpt1_Click"/>
                          </td>
                      </tr>
                  </table>
              </fieldset> 
             </td> 
            </tr>
            
            <tr>
              <td style="width: 420px">
                  <fieldset style="width: 420px">
                      <legend><strong>Business Collection Report 2</strong></legend>
                      <table style="width: 420px">
                          <tr>
                              <td>
                                  <asp:Label runat="server" ID="lblRpt2F"><strong>From Date</strong></asp:Label>
                              </td>
                              <td>
                                  <cc1:GMDatePicker ID="dtpBusCol2FDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                              </td>
                              <td>
                                  <asp:Label runat="server" ID="lblBusT"><strong>To Date</strong></asp:Label>
                              </td>
                              <td>
                                  <cc1:GMDatePicker ID="dtpBusCol2ToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                              </td>
                          </tr>
                          <tr>
                              <td></td>
                              <td>
                                  <asp:Button runat="server" ID="btnBussCollecRpt2" Text="Show Report" 
                                      onclick="btnBussCollecRpt2_Click"/>
                              </td>
                              <td></td>
                          </tr>
                          
                      </table>
                  </fieldset>
              </td>  
              <td style="width: 420px">
                  <fieldset style="width: 420px">
                      <legend><strong>Distributor Wise Customer Registration Count Report</strong></legend>
                      <table style="width: 420px">
                       <tr>
                          <td>
                             <asp:Label runat="server" ID="lblDisWsCusRF"><strong>From Date:</strong></asp:Label> 
                          </td> 
                          <td>
                             <cc1:GMDatePicker ID="dtpDWCustRegFDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker> 
                          </td>
                          <td>
                             <asp:Label runat="server" ID="lblDisWsCusRT"><strong>To Date:</strong></asp:Label>  
                          </td>
                          <td>
                             <cc1:GMDatePicker ID="dtpDWCustRegToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker> 
                          </td>
                       </tr> 
                       <tr>
                         <td></td>
                         <td>
                             <asp:Button runat="server" ID="btnDistWSCustRG" Text="Show Report" onclick="btnDistWSCustRG_Click"/>
                         </td>
                         <td></td>
                         <td></td>  
                       </tr>  
                      </table>
                  </fieldset>
              </td >
            </tr>
            <tr>
              <td style="width: 420px">
                  <fieldset style="width: 420px; height: 85px" >
                    <legend><strong>Customer Account Approve and KYC Update(MBL & MobiCash Agent)</strong></legend>
                    <table style="width: 420px">
                     <tr>
                       <td>
                        <asp:Label runat="server" ID="lblKycF"><strong>From Date</strong></asp:Label>   
                       </td>
                       <td>
                           <cc1:GMDatePicker ID="dtpKycFromDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                                </cc1:GMDatePicker>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblKycT"><strong>To Date</strong></asp:Label>
                       </td>
                       <td>
                           <cc1:GMDatePicker ID="dtpKycToDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                           </cc1:GMDatePicker>
                       </td>
                     </tr>
                     <tr>
                         <td></td>
                         <td>
                           <asp:Button runat="server" ID="btnKycUpdate" Text="Show Report" 
                                 onclick="btnKycUpdate_Click"/>  
                         </td>
                         <td></td>
                         <td></td>
                     </tr>   
                    </table>
                  </fieldset>
              </td>
              <td style="width: 420px">
               <fieldset style="width: 420px">
                 <legend><strong>Rank Wise Member Count</strong></legend>  
                 <table style="width: 420px">
                     <tr>
                         <td>
                             <asp:RadioButtonList ID="rbtOption" runat="server" Font-Bold="True" 
                                 RepeatDirection="Horizontal" AutoPostBack="True">
                                 <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                 <asp:ListItem Value="1">Operator</asp:ListItem>
                                 <asp:ListItem Value="2">Date Range</asp:ListItem>
                                 <asp:ListItem Value="3">Both</asp:ListItem>
                             </asp:RadioButtonList>
                         </td>
                         <td>
                             <asp:Label ID="lblRank" runat="server"><strong>Rank</strong></asp:Label>
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlSourceRank" runat="server"  
                                 DataSourceID="sdsRank" DataTextField="RANK_TITEL" 
                                 DataValueField="ACCNT_RANK_ID" />
                         </td>
                     </tr>
                 </table>
                 <table style="width: 420px">
                    <tr>
                        <td>
                          <asp:Label runat="server" ID="lblFDate"><strong>From Date</strong></asp:Label>  
                        </td>
                        <td>
                          <cc1:GMDatePicker ID="dtpRankFDate" runat="server" CalendarTheme="Silver" Enabled="False"
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                           </cc1:GMDatePicker>  
                        </td>
                        <td>
                          <asp:Label runat="server" ID="lblToDate"><strong>To Date</strong></asp:Label>  
                        </td>
                        <td>
                          <cc1:GMDatePicker ID="dtpRankToDate" runat="server" CalendarTheme="Silver" Enabled="False"
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                           </cc1:GMDatePicker>  
                        </td> 
                     </tr>
                     <tr>
                         <td>
                          <asp:Label runat="server" ID="lblOpr"><strong>Select Operator</strong></asp:Label>  
                         </td>
                         <td>
                          <asp:DropDownList runat="server" ID="ddlOperator" Enabled="False">   
                              <asp:ListItem Value="017">GP</asp:ListItem>
                              <asp:ListItem Value="018">Robi</asp:ListItem>
                              <asp:ListItem Value="019">BL</asp:ListItem>
                              <asp:ListItem Value="016">AirTel</asp:ListItem>
                              <asp:ListItem Value="015">TeleTalk</asp:ListItem>
                              <asp:ListItem Value="011">CityCell</asp:ListItem>
                             </asp:DropDownList>
                             <asp:Button ID="btnRankOpr" runat="server" onclick="btnRankOpr_Click" 
                                 Text="Count" />
                         </td>
                         <td>
                           <asp:Button runat="server" ID="btnRankWiseList" Text="List Show" 
                                 onclick="btnRankWiseList_Click"/>    
                         </td>
                         <td>
                           <asp:Label runat="server" ID="lblResult" Font-Bold="True" ForeColor="#FF3300" 
                                 Font-Size="Medium"></asp:Label>
                         </td>
                     </tr> 
                 </table>
               </fieldset>   
              </td>  
            </tr>
            <tr>
             <td style="width: 420px">
              <fieldset style="width: 420px">
               <legend><strong>D2D Agent Performance Report(Based on Verify Date)</strong></legend>   
               <table style="width: 420px">
                <tr>
                 <td>
                   <asp:Label runat="server" ID="lblDF"><strong>From Date</strong></asp:Label>  
                 </td>
                 <td>
                    <cc1:GMDatePicker ID="dtpD2DFDate" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="100">
                                    <calendartitlestyle backcolor="#FFFFC0" />
                           </cc1:GMDatePicker> 
                 </td>
                 <td>
                  <asp:Label runat="server" ID="lblDTo"><strong>To Date</strong></asp:Label>   
                 </td>
                 <td>
                   <cc1:GMDatePicker ID="dtpD2DToDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100">
                       <calendartitlestyle backcolor="#FFFFC0" />
                    </cc1:GMDatePicker>   
                 </td>   
                </tr>
                <tr>
                 <td></td>
                 <td>
                  <asp:Button runat="server" ID="btnD2dRpt" Text="Show Report" 
                         onclick="btnD2dRpt_Click"/>   
                 </td>
                 <td></td>
                 <td></td>   
                </tr>   
               </table>
              </fieldset>   
             </td>
             <td>
               <fieldset style="width: 420px">
                <legend><strong>Daily Interim TopUp Report</strong></legend>   
                 <table style="width: 420px">
                  <tr>
                    <td>
                     <asp:Label runat="server" ID="lblInterim"><strong>Date</strong></asp:Label>      
                    </td>
                    <td>
                     <cc1:GMDatePicker ID="dtpInterimDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="False">
                       <calendartitlestyle backcolor="#FFFFC0" />
                    </cc1:GMDatePicker>   
                    </td>
                  </tr>
                  <tr>
                    <td></td>
                    <td>
                     <asp:Button runat="server" ID="btnInterim" Text="Show Report" onclick="btnInterim_Click"/>   
                    </td>  
                  </tr>   
                 </table>  
               </fieldset> 
             </td>   
            </tr>
            <tr>
             <td>
              <fieldset style="width: 420px">
               <legend><strong>Daily Distribution Channel Balance</strong></legend>   
               <table style="width: 420px">
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblDisDate"><strong>Date</strong></asp:Label>   
                 </td>
                 <td>
                   <cc1:GMDatePicker ID="dtpDistribution" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                   </cc1:GMDatePicker>   
                 </td>   
                </tr>
                <tr>
                 <td></td>   
                 <td>
                  <asp:Button runat="server" ID="btnDisChaBalance" Text="Show Report" 
                         onclick="btnDisChaBalance_Click" />   
                 </td>   
                </tr>   
               </table>
              </fieldset>   
             </td>
             <td>
              <fieldset style="width: 420px">
               <legend><strong>Daily Distributor Lifting & Refund Report</strong></legend>
               <table style="width: 420px">
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblLiftFD"><strong>From Date:</strong></asp:Label>   
                 </td>
                 <td>
                  <cc1:GMDatePicker ID="dtpDisLiftFD" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>    
                 </td>
                 <td>
                  <asp:Label runat="server" ID="lblLiftTD"><strong>To Date:</strong></asp:Label>   
                 </td>
                 <td>
                  <cc1:GMDatePicker ID="dtpDisLiftToD" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>    
                 </td>   
                </tr>
                <tr>
                 <td></td>  
                 <td>
                     <asp:Button runat="server" ID="btnDisLiftNRefndOld" Text="Show Report Old" 
                         onclick="btnDisLiftNRefndOld_Click"/>&nbsp;</td> 
                 <td></td>
                 <td>
                     <asp:Button ID="btnDisLiftNRfnd" runat="server" onclick="btnDisLiftNRfnd_Click" 
                         Text="Show Report New" />
                    </td>
                </tr>   
               </table>   
                  
              </fieldset>   
             </td>   
            </tr>
            <tr>
             <td>
              <fieldset style="width: 420px">
               <legend><strong>Transaction Rule Checker Report </strong></legend>
               <table style="width: 420px">
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblRCFD"><strong>From Date</strong></asp:Label>   
                 </td>
                 <td>
                  <cc1:GMDatePicker ID="dtpRuleFromDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>    
                 </td>
                 <td>
                  <asp:Label runat="server" ID="lblRCTD"><strong>To Date</strong></asp:Label>   
                 </td>
                 <td>
                  <cc1:GMDatePicker ID="dtpRuleToDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>    
                 </td>  
                </tr>
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblRCFAmt"><strong>From Amt</strong></asp:Label>      
                 </td>
                 <td>
                  <asp:TextBox runat="server" ID="txtRCFRAmt"></asp:TextBox>   
                 </td>
                 <td>
                  <asp:Label runat="server" ID="lblRCToAmt"><strong>To Amt</strong></asp:Label>   
                 </td>
                 <td>
                  <asp:TextBox runat="server" ID="txtRCTOAmt"></asp:TextBox>
                 </td>
                </tr>
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblRCService"><strong>Service</strong></asp:Label>   
                 </td>
                 <td>
                  <asp:DropDownList runat="server" ID="ddlRCServiceList">   
                      <asp:ListItem Value="0">Select Service</asp:ListItem>
                      <asp:ListItem Value="1">CN</asp:ListItem>
                      <asp:ListItem Value="2">CCT, SW</asp:ListItem>
                      <asp:ListItem Value="3">FT</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td></td>
                 <td>
                  <asp:Button runat="server" ID="btnRuleCheck" Text="Show Report" onclick="btnRuleCheck_Click"/>    
                  <asp:Button runat="server" ID="btnRuleCheckClear" Text="Clear" 
                         onclick="btnRuleCheckClear_Click"/>
                 </td>   
                </tr>   
               </table>   
              </fieldset>   
             </td>
             <td>
              <fieldset style="width: 420px">
               <legend><strong>D2D Agent Performance Report(Based on Registration Date)</strong></legend>
               <table style="width: 420px">
                <tr>
                 <td>
                  <asp:Label runat="server" ID="lblD2DF"><strong>From Date</strong></asp:Label>   
                 </td>
                 <td>
                   <cc1:GMDatePicker ID="dtpD2DRegFDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>  
                 </td>
                 <td>
                  <asp:Label runat="server" ID="lblD2DRTo"><strong>To Date</strong></asp:Label>   
                 </td>
                 <td>
                  <cc1:GMDatePicker ID="dtpD2DRegToDate" runat="server" CalendarTheme="Silver" 
                     DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                     TextBoxWidth="100" Enabled="True">
                     <calendartitlestyle backcolor="#FFFFC0" />
                  </cc1:GMDatePicker>   
                 </td>   
                </tr>
                <tr>
                 <td></td>
                 <td>
                  <asp:Button runat="server" ID="btnD2DRegRpt" Text="Show Report" onclick="btnD2DRegRpt_Click"/>   
                 </td>
                 <td></td>
                 <td>
                     &nbsp;</td>   
                </tr>   
               </table>   
              </fieldset>   
             </td>   
            </tr>
            
           </table>
           
      </ContentTemplate>
      <Triggers>
       <asp:PostBackTrigger ControlID="btnDisAgentReg" />
       <asp:PostBackTrigger ControlID="btnDistrictWiseSales" />
       <asp:PostBackTrigger ControlID="btnDisAgentPerformCount" />
       <asp:PostBackTrigger ControlID="btnBusinessRpt1" />
       <asp:PostBackTrigger ControlID="btnBussCollecRpt2" />
       <asp:PostBackTrigger ControlID="btnDistWSCustRG" />
       <asp:PostBackTrigger ControlID="btnKycUpdate" />
       <asp:PostBackTrigger ControlID="btnD2dRpt" />
       <asp:PostBackTrigger ControlID="btnInterim" />
       <asp:PostBackTrigger ControlID="btnDisChaBalance" />       
       <asp:PostBackTrigger ControlID="btnDisLiftNRefndOld" />
       <asp:PostBackTrigger ControlID="btnDisLiftNRfnd" />
       <asp:PostBackTrigger ControlID="btnRankWiseList" />
       <asp:PostBackTrigger ControlID="btnRuleCheck" />
       <asp:PostBackTrigger ControlID="btnD2DRegRpt" />
      </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
