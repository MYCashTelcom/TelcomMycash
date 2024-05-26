<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngAgentPerformanceReport.aspx.cs" Inherits="MIS_frmMngAgentPerformanceReport" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Performance Report</title>
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
         	background-color:powderblue;
         	width:auto;  
         	color:Black;       	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
         input[type="submit"]
         {
         	height:25px;
         	width:80px;
         	}
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
     </ajaxToolkit:ToolkitScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>--%>
       <asp:SqlDataSource ID="sdsAgntPerReport" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="SELECT AGNT_PERF_RPT_ID, REPORT_NAME, RPT_CREATE_DATE, CREATED_BY FROM AGNT_PERF_RPT ORDER BY RPT_CREATE_DATE DESC">
        </asp:SqlDataSource>
          <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
              <table width="100%">
               <tr>
                <td>
                   Manage Agent Performance Report
                </td>
                <td>                  
                </td>
                <td>
                  <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>
                  <%-- <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                     <ProgressTemplate>
                        <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                     </ProgressTemplate>
                  </asp:UpdateProgress>--%>
                </td>
               </tr>
             </table>               
           </asp:Panel> 
           <asp:Panel ID="Panel2" runat="server">
            <table>
             <tr>
              <td>
                  <asp:Label ID="Label1" runat="server" Text="Agent Performance Report"></asp:Label>
              </td>
              <td>
                  <asp:Button ID="btnAgntPrfRpt" runat="server" Text=" View " onclick="btnAgntPrfRpt_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
              </td>
             </tr>
            </table>
           </asp:Panel>  
           <asp:Panel ID="Panel3" runat="server">
        <div>
            <asp:GridView ID="gdvReportDownload" runat="server" AllowPaging="true" 
                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" 
                BorderColor="#E0E0E0" CssClass="mGrid" DataKeyNames="AGNT_PERF_RPT_ID" 
                DataSourceID="sdsAgntPerReport" 
                PagerStyle-CssClass="pgr" PageSize="15" Width="900px" >                
                <Columns>
                    <asp:BoundField DataField="AGNT_PERF_RPT_ID" HeaderText="Report ID" 
                        SortExpression="AGNT_PERF_RPT_ID" />
                    <asp:BoundField DataField="REPORT_NAME" HeaderText="Report Name" 
                        SortExpression="REPORT_NAME" />
                    <asp:BoundField DataField="RPT_CREATE_DATE" HeaderText="Create Date" 
                        SortExpression="RPT_CREATE_DATE" />
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                         <a onclick="window.open('EXPORT_AGENT_PER_REPORT/<%# DataBinder.Eval(Container,"DataItem.REPORT_NAME") %>','', 'status=1, toolbar=0,menubar=0,resizable=1,width=950,height=550,scrollbars=1')" href="javascript:void(0)" style="color: #000000; text-decoration: none"><strong>DownLoad</strong></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                <PagerStyle CssClass="pgr" />
                <HeaderStyle ForeColor="White" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
           </div>
          <div>
        </div>        
        </asp:Panel>         
       <%--</ContentTemplate>
     </asp:UpdatePanel>--%>
    </form>
</body>
</html>
