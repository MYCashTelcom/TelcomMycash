<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountsReport.aspx.cs"
    Inherits="COMMON_frmAccountsReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>     
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wallet Record</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            height: 164px;
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
    </style>   
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:SqlDataSource ID="sdsRankTitle" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT RANK_TITEL FROM ACCOUNT_RANK where ACCNT_RANK_ID not in 
                (120519000000000001,120618000000000002,120618000000000001,120618000000000003,
                120618000000000004,120927000000000001,120930000000000001,121001000000000001,
                121107000000000001)ORDER BY ACCNT_RANK_ID ">
             </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsServicePackage" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsDistrict" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
                <%--SELECT &quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot; FROM &quot;MANAGE_DISTRICT&quot;--%>
                </asp:SqlDataSource>           
            <asp:SqlDataSource ID="sdsThana" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
                <%--SELECT &quot;THANA_ID&quot;, &quot;THANA_NAME&quot;, &quot;DISTRICT_ID&quot; FROM &quot;MANAGE_THANA&quot;--%>
            </asp:SqlDataSource>
           
            <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
             <table width="100%">
               <tr>
                <td>
                  Accounts Report
                </td>
                <td></td>
                <td align="right">
                  <asp:Label ID="lblMessage" runat="server" Text="" ></asp:Label>
                </td>
                <td align="right">
                  <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                     <ProgressTemplate>
                        <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                     </ProgressTemplate>
                  </asp:UpdateProgress>
                </td>
               </tr>
             </table>
           </asp:Panel>  
           <div>
            <table>
                <tr>
                 <td valign="top">
                  <fieldset style="border-color: #FFFFFF;width:500px; height:130px;">
                   <legend><strong style="color: #666666">&nbsp;&nbsp;Select Account</strong></legend>
                    <table>
                     <tr>
                      <td style="height: 30px;">                                   
                          <table>
                              <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbtnAllDateRange" runat="server" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1">Date Range</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                    <td style="font-size:10px;">
                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date :" ></asp:Label>                                   
                                     <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="110" Font-Size="X-Small">
                                       <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                                     </cc1:GMDatePicker>
                                   To Date :
                                    <strong>
                                        <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                            TextBoxWidth="110" Font-Size="X-Small">
                                            <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                        </cc1:GMDatePicker>
                                        </strong>
                                      </td>
                                     </tr>
                                    </table>               
                                </td>
                            </tr>
                            <tr>
                                <td >
                                <asp:RadioButtonList ID="rbtnSelectState" RepeatDirection="Horizontal" AutoPostBack="true"
                                    runat="server">
                                    <asp:ListItem Value="AC" Selected="True">All Wallet</asp:ListItem>
                                    <asp:ListItem Value="A">Active Wallet</asp:ListItem>
                                    <asp:ListItem Value="I">Idle Wallet</asp:ListItem>
                                    <asp:ListItem Value="U">UISC Agent</asp:ListItem>
                                    <asp:ListItem Value="T">TISC Agent</asp:ListItem>
                                </asp:RadioButtonList>
                                <br /> <br />
                                <asp:Button ID="btnViewReport" runat="server" Text="Show Report" OnClick="btnViewReport_Click" />
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                    <td valign="top">
                    <fieldset style="border-color: #FFFFFF; width: 524px; height:130px">
                     <legend><strong style="color: #666666">&nbsp;&nbsp;All Rank Report</strong></legend>
                        <table>
                            <tr>
                                <td style="height: 30px;">
                                    <asp:RadioButtonList ID="rbtnRankListItem" RepeatDirection="Horizontal" AutoPostBack="true"
                                        runat="server" >
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Date" >Date Range</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>    
                                    <asp:Label ID="Label1" runat="server" Text="From Date :" ></asp:Label>                                   
                                     <cc1:GMDatePicker ID="dptFDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="110" Font-Size="X-Small">
                                       <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                                     </cc1:GMDatePicker>
                                   To Date :
                                    <strong>
                                        <cc1:GMDatePicker ID="dptTDate" runat="server" CalendarTheme="Silver" 
                                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                            TextBoxWidth="110" Font-Size="X-Small">
                                            <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                        </cc1:GMDatePicker>
                                        </strong>
                                
                                    
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan="2">                                    
                                    <asp:RadioButtonList ID="rbtnSelectRank" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbtnSelectRank_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">MBL Branch</asp:ListItem>
                                        <asp:ListItem Value="1">MBL Distributor</asp:ListItem>
                                        <asp:ListItem Value="2">MBL DSE</asp:ListItem>
                                        <asp:ListItem Value="3">MBL Agent</asp:ListItem>
                                        <asp:ListItem Value="4">MBL Customer</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click" />
                                  
                                </td>
                            </tr>
                            
                        </table>
                          </fieldset>
                    </td>
                </tr>
                <tr>
                  <td> 
                   <fieldset style="border-color: #FFFFFF; width:500px; height:170px">
                     <legend><strong style="color: #666666">&nbsp;&nbsp;Area wise Report</strong></legend>
                        <table>
                              <tr>
                              <td>
                                 <asp:RadioButtonList ID="rblAreaWise" RepeatDirection="Horizontal" AutoPostBack="true"
                                        runat="server" >
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Date" >Date Range</asp:ListItem>
                                 </asp:RadioButtonList>
                              </td>
                              <td>
                                 <asp:Label ID="Label3" runat="server" Text="From Date :" ></asp:Label>                                   
                                     <cc1:GMDatePicker ID="dptAreaWiseFDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="110" Font-Size="X-Small">
                                       <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                                     </cc1:GMDatePicker>
                                   To Date :
                                    <strong>
                                        <cc1:GMDatePicker ID="dptAreaWiseTDate" runat="server" CalendarTheme="Silver" 
                                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                            TextBoxWidth="110" Font-Size="X-Small">
                                            <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                        </cc1:GMDatePicker>
                                  </strong>
                              </td>
                           </tr>
                            <tr>
                                <td style="height: 60px;width:100px;">
                                    <asp:RadioButtonList ID="rblAllArea" RepeatDirection="Vertical" AutoPostBack="true"
                                        runat="server" onselectedindexchanged="rblAllArea_SelectedIndexChanged" >
                                        <asp:ListItem Selected="True" Value="0">District Wise</asp:ListItem>                                        
                                        <asp:ListItem Value="1">ThanaWise</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="height: 60px;">
                                    Select District:
                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="185px"  DataSourceID="sdsDistrict" 
                                       AutoPostBack="true"  DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" 
                                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged" >
                                    </asp:DropDownList><br />                                    
                                    Select  Thana :
                                    <asp:DropDownList ID="ddlThana" runat="server" Width="185px" DataSourceID="sdsThana" 
                                        DataTextField="THANA_NAME" DataValueField="THANA_ID">
                                    </asp:DropDownList>                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">                                    
                                <asp:RadioButtonList ID="rblRankType" runat="server" 
                                RepeatDirection="Horizontal" >
                                    <asp:ListItem Value="0" Selected="True">MBL Branch</asp:ListItem>
                                    <asp:ListItem Value="1">MBL Distributor</asp:ListItem>
                                    <asp:ListItem Value="2">MBL DSE</asp:ListItem>
                                    <asp:ListItem Value="3">MBL Agent</asp:ListItem>
                                    <asp:ListItem Value="4">MBL Customer</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                <asp:Button ID="btnAreaWisePreView" runat="server" Text="Show Report" 
                                        onclick="btnAreaWisePreView_Click"/>
                                  
                                </td>
                            </tr>
                            
                        </table>
                   </fieldset>                         
                  </td>
                  <td>
                     <fieldset style="border-color: #FFFFFF; width: 524px; height:170px">
                          <legend> <strong style="color: #666666">&nbsp;&nbsp;Individual Rank Report</strong></legend>
                             <table>
                                 <tr>
                                    <td valign="top"><br />
                                        <asp:RadioButtonList ID="rblIndividualRank" RepeatDirection="Horizontal" AutoPostBack="true"
                                        runat="server" >
                                        <asp:ListItem Selected="True" Value="I">Individual</asp:ListItem>
                                        <asp:ListItem Value="D" >Date Range</asp:ListItem>
                                        <%--<asp:ListItem Value="Individual">Individual</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                    </td>
                                    <td style="height: 60px;">
                                        <asp:Label ID="Label2" runat="server" Text="From Date :" ></asp:Label>                                   
                                     <cc1:GMDatePicker ID="dtpFromIndiviDate" runat="server"  CalendarTheme="Silver" 
                                         DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative;" 
                                         TextBoxWidth="110" Font-Size="X-Small">
                                       <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                                     </cc1:GMDatePicker>
                                   To Date :
                                    <strong>
                                        <cc1:GMDatePicker ID="dtpToIndivDate" runat="server" CalendarTheme="Silver" 
                                            DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                            TextBoxWidth="110" Font-Size="X-Small">
                                            <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                        </cc1:GMDatePicker>
                                        </strong>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td colspan="2">&nbsp;
                                         <asp:DropDownList ID="ddlIndividualRank" runat="server" DataSourceID="sdsRankTitle"
                                              DataTextField="RANK_TITEL" DataValueField="RANK_TITEL">
                                         </asp:DropDownList>
                                    </td>                                    
                                 </tr>
                                 <tr>
                                    <td>&nbsp;
                                        <asp:Button ID="btnShowIndividualRankReport" runat="server" Text="Show Report" 
                                            onclick="btnShowIndividualRankReport_Click" />
                                    </td>
                                    <td>
                                    
                                    </td>
                                 </tr>
                             </table>
                     </fieldset>
                  </td>
                 </tr>
                 <tr>
                  <td>
                      <fieldset style="border-color: #FFFFFF; width: 500px; height:130px">
                          <legend> <strong style="color: #666666">&nbsp;&nbsp;Salary Statement Report</strong></legend>
                          <table>
                            <tr>
                              <td>
                               Select Package
                              </td>
                              <td>
                                  <asp:DropDownList ID="ddlSelectPackage" runat="server" 
                                      DataSourceID="sdsServicePackage" DataTextField="SERVICE_PKG_NAME" 
                                      DataValueField="SERVICE_PKG_ID">
                                  </asp:DropDownList>
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                  <asp:Button ID="btnTwoWalletBalance" runat="server" Text="Show Report" 
                                      onclick="btnTwoWalletBalance_Click" />
                              </td>
                            </tr>
                          </table>
                      </fieldset>    
                  </td>
                 </tr>
            </table>           
          </div>      
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
