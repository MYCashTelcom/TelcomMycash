<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDistributor_Commi_Statement.aspx.cs" Inherits="MIS_frmDistributor_Commi_Statement" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distributor Commission Statement</title>
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
   <%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>--%>
       <asp:SqlDataSource ID="sdsRegReport" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT REG_RPT_ID, REPORT_NAME, RPT_CREATE_DATE, CREATED_BY, CMP_BRANCH_ID, REG_FROM_DATE, REG_TO_DATE, REPORT_TYPE FROM REG_REPORT_INFO WHERE REPORT_TYPE='DistCommStatemet'  ORDER BY RPT_CREATE_DATE DESC">
        </asp:SqlDataSource>
          <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
           <table width="100%">
            <tr>
             <td>
                Distributor Commission Statement
             </td>
             <td></td>
             <td></td>
             <td align="right">
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
             </td>
             <td align="right">
             <%--<asp:UpdateProgress ID="UpdateProgress3" runat="server">
                 <ProgressTemplate>
                    <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                 </ProgressTemplate>
              </asp:UpdateProgress>--%>
             </td>
            </tr>
           </table>
        </asp:Panel>
        <asp:Panel ID="pnlReport" runat="server">
          <fieldset id="fldSet1" style="width:500px;border-color: #FFFFFF;">
         <legend>
         <strong style="color: #666666">
          Distributor Commission Statement Report </strong> </legend>
          <table style="width:500px;">
            <tr>
             <td>
                 <asp:Label ID="Label2" runat="server" Text="Date Range:" ></asp:Label>              
             </td>
                <td>
                    <asp:Label ID="Label5" runat="server"  Text="From Date: "></asp:Label>
                    &nbsp;&nbsp;
                    <cc1:GMDatePicker ID="dtpFromDate" runat="server"  CalendarTheme="Silver" 
                       DateFormat="dd-MMM-yyyy " MinDate="1900-01-01" Style="position: relative" 
                       TextBoxWidth="90">
                    <calendartitlestyle  />
                   </cc1:GMDatePicker>
                    <asp:Label ID="Label6" runat="server" Text="To Date:"></asp:Label>
                    &nbsp;&nbsp;                    
                    <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                        DateFormat="dd-MMM-yyyy" MinDate="1900-01-01" Style="position: relative" 
                        TextBoxWidth="90"  >
                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                    </cc1:GMDatePicker >
               </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                                 
                    <asp:Button ID="btnReport" runat="server"  Text="Generate Report" onclick="btnReport_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                </td>
            </tr>
          </table>
         </fieldset></asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
          <%--  <asp:GridView ID="gdvReportDownload" runat="server" AllowPaging="true" 
                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" 
                BorderColor="#E0E0E0" CssClass="mGrid" DataKeyNames="REG_RPT_ID" 
                DataSourceID="sdsRegReport" 
                PagerStyle-CssClass="pgr" PageSize="15" Width="900px" >                
                <Columns>
                    <asp:BoundField DataField="REG_RPT_ID" HeaderText="Report ID" 
                        SortExpression="REG_RPT_ID" />
                    <asp:BoundField DataField="REPORT_NAME" HeaderText="Report Name" 
                        SortExpression="REPORT_NAME" />
                    <asp:BoundField DataField="RPT_CREATE_DATE" HeaderText="Create Date" 
                        SortExpression="RPT_CREATE_DATE" />
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                         <a onclick="window.open('EXPORT_REGISTRATION_RPT/<%# DataBinder.Eval(Container,"DataItem.REPORT_NAME") %>','', 'status=1, toolbar=0,menubar=0,resizable=1,width=950,height=550,scrollbars=1')" href="javascript:void(0)" style="color: #000000; text-decoration: none"><strong>DownLoad</strong></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                <PagerStyle CssClass="pgr" />
                <HeaderStyle ForeColor="White" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>   --%>
            
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
        </asp:Panel>
     <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
