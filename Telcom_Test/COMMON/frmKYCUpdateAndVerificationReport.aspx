<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKYCUpdateAndVerificationReport.aspx.cs" Inherits="COMMON_frmKYCUpdateAndVerificationReport" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KYC Update and Verification Report</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
    
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="background-color: royalblue">
             <strong><span style="color: white">KYC Update Report &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
             </span></strong>
           </div>
           <asp:SqlDataSource ID="sdsSelectUser" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"                 
                SelectCommand="SELECT ACCNT_ID,SYS_USR_LOGIN_NAME FROM CM_SYSTEM_USERS WHERE SYS_USR_GRP_ID 
                               IN('12052901001001','12050401001001','12082901001001','14081401001001','14080301001001') ORDER BY UPPER(SYS_USR_LOGIN_NAME)">
           </asp:SqlDataSource>
           <div>
            <table>
             <tr>
              <td valign="top" >
               <fieldset style="border-color: #FFFFFF;width:450px; height:110px;">
                <legend><strong style="color: #666666">&nbsp;&nbsp;KYC update report for all user</strong></legend>
                <table>
                 <tr>
                  <td style="height: 30px;">                                   
                   <table>
                    <tr>
                     <td>
                      <asp:RadioButtonList ID="rbtnAllKYC" runat="server" 
                          RepeatDirection="Horizontal">
                          <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                          <asp:ListItem Value="1">Date Range</asp:ListItem>
                      </asp:RadioButtonList>
                     </td>
                     <td style="font-size:10px;" class="style2">
                      <asp:Label ID="lblFromDate" runat="server" Text="From Date :" Font-Size="12px" ></asp:Label>                                   
                      <cc1:GMDatePicker ID="dptFromDateAllKYC" runat="server"  CalendarTheme="Silver" 
                          DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                          TextBoxWidth="80" Font-Size="X-Small">
                          <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                      </cc1:GMDatePicker>
                      <asp:Label ID="Label4" runat="server" Text="To Date :" Font-Size="12px"></asp:Label>                       
                      <strong>
                       <cc1:GMDatePicker ID="dtpToDateAllKYC" runat="server" CalendarTheme="Silver" 
                           DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                           TextBoxWidth="80" Font-Size="X-Small">
                           <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                       </cc1:GMDatePicker>
                      </strong>
                     </td>
                    </tr>
                   </table>               
                  </td>
                 </tr>
                 <tr>
                   <td></td>
                 </tr>
                 <tr>
                    <td >
                    <br />
                 <asp:Button ID="btnKYCAll" runat="server" Text="Show Report" 
                       onclick="btnKYCAll_Click"  />
                  </td>
                  </tr>
               </table>
               </fieldset>
                   </td>
                   <td valign="top">
                   <fieldset style="border-color: #FFFFFF; width: 450px; height:110px">
                    <legend><strong style="color: #666666">&nbsp;&nbsp;KYC update report for individual user</strong></legend>
                     <table>
                        <tr>
                        <td align="right">
                            User List
                        </td>
                        <td>                                 
                         <asp:DropDownList ID="ddlIndividualKYCUpdate" runat="server" DataSourceID="sdsSelectUser" AutoPostBack="true"
                                DataTextField="SYS_USR_LOGIN_NAME" DataValueField="ACCNT_ID">
                         </asp:DropDownList>
                        </td>
                       </tr>
                       <tr>
                        <td style="height: 30px;">
                         <asp:RadioButtonList ID="rbtnIndividualKYC" RepeatDirection="Horizontal" AutoPostBack="true" runat="server" >
                             <asp:ListItem Selected="True" Value="0">User</asp:ListItem>
                             <asp:ListItem Value="1" >Date Range</asp:ListItem>
                         </asp:RadioButtonList>
                        </td>
                        <td>    
                         <asp:Label ID="Label1" runat="server" Text="From Date :"  Font-Size="12px"></asp:Label>                                   
                         <cc1:GMDatePicker ID="dptFromDateIndividualKYC" runat="server"  CalendarTheme="Silver" 
                             DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                             TextBoxWidth="80" Font-Size="X-Small">
                           <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                         </cc1:GMDatePicker>
                            <asp:Label ID="Label5" runat="server" Text="To Date :"  Font-Size="12px"></asp:Label>
                        <strong>
                         <cc1:GMDatePicker ID="dptToIndividualKYC" runat="server" CalendarTheme="Silver" 
                                    DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                                    TextBoxWidth="80" Font-Size="X-Small">
                                    <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                         </cc1:GMDatePicker>
                         </strong>  
                        </td>
                       </tr> 
                       <tr>
                        <td>
                          <asp:Button ID="btnKYCIndividual" runat="server" Text="Show Report" onclick="btnKYCIndividual_Click"  /> 
                        </td>
                        <td>                                                        
                        </td>
                       </tr>
                      </table>
                     </fieldset>
                    </td>
                </tr>
                <tr>
                  <td colspan="2">
                   <div style="background-color: royalblue">
                    <strong>
                      <span style="color: white">KYC Verification Report 
              
                     </span>
                   </strong>
                  </div>
                  </td>
                </tr>
                <tr>
                 <td class="style1"> 
                  <fieldset style="border-color: #FFFFFF; width:450px; height:130px">
                   <legend><strong style="color: #666666">&nbsp;&nbsp;KYC verification report for all user</strong></legend>
                    <table>
                     <tr>
                      <td style="height: 60px;width:100px;">
                        <asp:RadioButtonList ID="rblAllVerification" RepeatDirection="Horizontal" AutoPostBack="true"
                             runat="server"  >
                             <asp:ListItem Selected="True" Value="0">All</asp:ListItem>                                        
                             <asp:ListItem Value="1">DateRange</asp:ListItem>
                        </asp:RadioButtonList>
                      </td>
                      <td style="height: 60px;">
                       <asp:Label ID="Label3" runat="server" Text="From Date :" Font-Size="12px" ></asp:Label>                                   
                         <cc1:GMDatePicker ID="dptFromDateAllKYCVerification" runat="server"  CalendarTheme="Silver" 
                           DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                           TextBoxWidth="80" Font-Size="X-Small">
                         <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                        </cc1:GMDatePicker>
                        <asp:Label ID="Label6" runat="server" Text=" To Date :"></asp:Label>
                      
                       <strong>
                        <cc1:GMDatePicker ID="dptToDateAllKYCVerification" runat="server" CalendarTheme="Silver" 
                            DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                            TextBoxWidth="80" Font-Size="X-Small">
                           <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                        </cc1:GMDatePicker>
                       </strong>  
                      </td>
                     </tr>
                     <tr>
                   <td></td>
                 </tr>
                     <tr>
                      <td colspan="2">
                       <asp:Button ID="btnKYCVerificationAll" runat="server" Text="Show Report" 
                            onclick="btnKYCVerificationAll_Click" />
                      </td>
                     </tr>
                    </table>
                   </fieldset>                         
                  </td>
                  <td>
                   <fieldset style="border-color: #FFFFFF; width: 450px; height:130px">
                    <legend> <strong style="color: #666666">&nbsp;&nbsp;KYC verification report for individual user</strong></legend>
                     <table>
                       <tr>
                       <td align="right">
                          User List:
                       </td>
                       <td>
                        <asp:DropDownList ID="ddlIndividualVerification" runat="server" DataSourceID="sdsSelectUser" AutoPostBack="true"
                            DataTextField="SYS_USR_LOGIN_NAME" DataValueField="ACCNT_ID">
                        </asp:DropDownList>
                       </td>                                    
                      </tr>
                      <tr>
                       <td valign="top"><br />
                        <asp:RadioButtonList ID="rblIndividualVerification" RepeatDirection="Horizontal" AutoPostBack="true"
                             runat="server" >
                             <asp:ListItem Selected="True" Value="0">User</asp:ListItem>
                             <asp:ListItem Value="1" >Date Range</asp:ListItem>
                         </asp:RadioButtonList>
                       </td>
                       <td style="height: 60px;">
                        <asp:Label ID="Label2" runat="server" Text="From Date :" Font-Size="12px" ></asp:Label>                                   
                        <cc1:GMDatePicker ID="dtpFromIndiviDate" runat="server"  CalendarTheme="Silver" 
                            DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                            TextBoxWidth="80" Font-Size="X-Small">
                            <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                        </cc1:GMDatePicker>
                        <asp:Label ID="Label7" runat="server" Text=" To Date :"></asp:Label>  
                         
                          <strong>
                        <cc1:GMDatePicker ID="dtpToIndivDate" runat="server" CalendarTheme="Silver" 
                            DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                            TextBoxWidth="80" Font-Size="X-Small">
                         <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                        </cc1:GMDatePicker>
                        </strong>
                       </td>
                      </tr>
                     
                      <tr>
                       <td> 
                          <asp:Button ID="btnKYCVericIndi" runat="server" Text="Show Report" 
                                     onclick="btnKYCVericIndi_Click" />                                       
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
                 <div style="background-color: royalblue">
                    <strong>
                      <span style="color: white">KYC Update Report for Micare
                    
                     </span>
                   </strong>
                  </div>
                 <fieldset style="border-color: #FFFFFF; width:450px; height:130px">
                 <legend><strong style="color: #666666">&nbsp;&nbsp;KYC update report for micare</strong></legend>
                 
                   <table>
                 <tr>
                  <td style="height: 30px;">                                   
                   <table>
                    <tr>
                     <td>
                      <asp:RadioButtonList ID="rblMicareKYCUpdate" runat="server" 
                          RepeatDirection="Horizontal">
                          <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                          <asp:ListItem Value="1">Date Range</asp:ListItem>
                      </asp:RadioButtonList>
                     </td>
                     <td style="font-size:10px;" class="style2">
                      <asp:Label ID="Label8" runat="server" Text="From Date :" Font-Size="12px" ></asp:Label>                                   
                      <cc1:GMDatePicker ID="dtpMicareFromDate" runat="server"  CalendarTheme="Silver" 
                          DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                          TextBoxWidth="80" Font-Size="X-Small">
                          <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
                      </cc1:GMDatePicker>
                      <asp:Label ID="Label9" runat="server" Text="To Date :" Font-Size="12px"></asp:Label>                       
                      <strong>
                       <cc1:GMDatePicker ID="dtpMicateToDate" runat="server" CalendarTheme="Silver" 
                           DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                           TextBoxWidth="80" Font-Size="X-Small">
                           <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                       </cc1:GMDatePicker>
                      </strong>
                     </td>
                    </tr>
                   </table>               
                  </td>
                 </tr>
                 <tr>
                   <td></td>
                 </tr>
                 <tr>
                    <td >
                    <br />
                 <asp:Button ID="btnShowMicareReport" runat="server" Text="Show Report" onclick="btnShowMicareReport_Click" 
                        />
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
