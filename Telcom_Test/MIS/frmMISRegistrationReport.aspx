<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMISRegistrationReport.aspx.cs" Inherits="MIS_frmMISRegistrationReport" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Report</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >        
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
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <asp:SqlDataSource ID="sdsRegReport" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT * FROM REG_REPORT_INFO WHERE REPORT_TYPE='RegReport'">
        </asp:SqlDataSource>
    
        <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
          <table width="100%">
            <tr>
             <td>
                MIS Registration Report
             </td>
             <td></td>
             <td align="right">
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
             </td>
             <td align="right">
             <%-- <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                 <ProgressTemplate>
                    <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                 </ProgressTemplate>
              </asp:UpdateProgress>--%>
             </td>
            </tr>
           </table>
        </asp:Panel>
        <asp:Panel ID="pnlView" runat="server" >
          <fieldset style="border-color: #FFFFFF; width:550px; height:150px;float:left;">
            <legend>
             <strong style="color: #666666">Registration Report</strong></legend>
              <table>
               <tr>
                <td>
                  <asp:RadioButtonList ID="rblAllDateRange" runat="server" 
                      RepeatDirection="Horizontal" >
                      <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                      <asp:ListItem Value="1">Date Range</asp:ListItem>
                  </asp:RadioButtonList>
                </td>
                <td>
                   <asp:Label ID="lblFromDate" runat="server" Text="From Date" ></asp:Label>
                   <cc1:GMDatePicker ID="dptFrom" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="100">
                      <calendartitlestyle backcolor="#FFFFC0" />
                   </cc1:GMDatePicker>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblTo" runat="server" Text="To Date " ></asp:Label>
                   <cc1:GMDatePicker ID="dtpTo" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="100">
                     <calendartitlestyle backcolor="#FFFFC0" />
                   </cc1:GMDatePicker>
                </td>
               </tr>
               <tr>
                <td>
                 &nbsp;&nbsp;
                   By Wallet
                 </td>
                 <td>  
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtWallet" runat="server"></asp:TextBox>
                </td>
               </tr>
               <tr>
                <td>
                
                </td>
                <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnView" runat="server" Text=" View " Width="83px" 
                        onclick="btnView_Click" />
                </td>
               </tr>
              </table>
         </fieldset>
         <fieldset id="fldSet1" style="width:500px;height:150px;border-color: #FFFFFF;">
         <legend>
         <strong style="color: #666666">
         Hierarchywise Registration Report </strong> </legend>
          <table style="width:500px;">
            <tr>
             <td>
                 <asp:Label ID="Label2" runat="server" Text="Date Range:" ></asp:Label>              
             </td>
                <td>
                    <asp:Label ID="Label5" runat="server"  Text="From Date: "></asp:Label>
                    &nbsp;&nbsp;
                    <cc1:GMDatePicker ID="dtpFDate" runat="server"  CalendarTheme="Silver" 
                       DateFormat="dd-MMM-yyyy " MinDate="1900-01-01" Style="position: relative" 
                       TextBoxWidth="90">
                    <calendartitlestyle  />
                   </cc1:GMDatePicker>
                    <asp:Label ID="Label6" runat="server" Text="To Date:"></asp:Label>
                    &nbsp;&nbsp;                    
                    <cc1:GMDatePicker ID="dtpTDate" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="90" >
                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                    </cc1:GMDatePicker>
               </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                 <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                    <asp:Button ID="btnReport" runat="server"  Text="Generate" 
                        onclick="btnReport_Click" Width="67px" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                </td>
            </tr>
          </table>
         </fieldset></asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
        <div>
        </div>
        <div>
            <asp:GridView ID="gdvReportDownload" runat="server" AutoGenerateColumns="false" AllowPaging="true" Width="900px"
                DataSourceID="sdsRegReport" DataKeyNames="REG_RPT_ID"
                BorderColor="#E0E0E0" PageSize="15" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt">
                <Columns>
                <asp:BoundField DataField="REG_RPT_ID" HeaderText="Report ID"  SortExpression="REG_RPT_ID" />
                 <asp:BoundField DataField="REPORT_NAME" HeaderText="Report Name"  SortExpression="REPORT_NAME" />
                 <asp:BoundField DataField="RPT_CREATE_DATE" HeaderText="Create Date" SortExpression="RPT_CREATE_DATE" />
                 <asp:TemplateField HeaderText="Download" >
                     <ItemTemplate>                       
                         <asp:Button ID="btnDownload" runat="server" Text="Download" 
                             onclick="btnDownload_Click" />                       
                     </ItemTemplate>
                 </asp:TemplateField>
                </Columns>
               <PagerStyle CssClass="pgr" />
                <HeaderStyle ForeColor="White" />
                <AlternatingRowStyle CssClass="alt" />
          </asp:GridView>
        </div>        
        </asp:Panel>
    <%-- </ContentTemplate>  
     <Triggers>
       <asp:PostBackTrigger ControlID="btnReport" />
       
       </Triggers>   
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
