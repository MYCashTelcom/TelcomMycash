<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_System_Audit.aspx.cs" Inherits="System_SYS_System_Audit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Audit</title>
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
         .style1
         {
             width: 672px;
         }
         .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	
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
     <contenttemplate>
    
    <asp:SqlDataSource ID="sdsSystemAudit" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="">
       <%-- SELECT SA.*,SU.SYS_USR_LOGIN_NAME FROM CM_SYSTEM_AUDIT SA,CM_SYSTEM_USERS SU
                WHERE SA.SYS_USR_ID=SU.SYS_USR_ID ORDER BY CM_SYS_AUDIT_ID DESC--%>
    </asp:SqlDataSource>
     <asp:Panel ID="pnlView" runat="server" >
      <table class="Top_Panel" width="100%">
       <tr>
        <td>
          <span style="color:White;"> System Audit</span>
        </td>
        <td>
         <span style="color:White;"> Form Date:</span>
            <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="130">
                <calendartitlestyle  />
            </cc1:GMDatePicker>
        &nbsp;&nbsp;
        <span style="color:White;"> To Date</span>
           <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                TextBoxWidth="130" >
                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
           </cc1:GMDatePicker>
           &nbsp;
          <span style="color:White;"> Remarks&nbsp;</span>
            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
          &nbsp;  
         <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />        
        </td>
        <td>
           <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
          </asp:UpdateProgress>
        </td>
       </tr>       
      </table>
     </asp:Panel>
    
     <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
         AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CM_SYS_AUDIT_ID" 
         DataSourceID="sdsSystemAudit" BorderColor="#E0E0E0" CssClass="mGrid" 
         PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
         PageSize="20" onselectedindexchanged="GridView1_SelectedIndexChanged" 
             onselectedindexchanging="GridView1_SelectedIndexChanging" 
             onpageindexchanging="GridView1_PageIndexChanging">
         <Columns>
             <asp:BoundField DataField="CM_SYS_AUDIT_ID" HeaderText="Audit ID" 
                 ReadOnly="True" SortExpression="CM_SYS_AUDIT_ID" Visible="False" >
             </asp:BoundField>
             <asp:BoundField DataField="SYS_USR_ID" HeaderText="User ID" 
                 SortExpression="SYS_USR_ID" Visible="False" >
             </asp:BoundField>
             <asp:BoundField DataField="SYS_USR_LOGIN_NAME" HeaderText="User Name" 
                 SortExpression="SYS_USR_LOGIN_NAME" >
             </asp:BoundField>
             <asp:BoundField DataField="OPERATION_TIME" HeaderText="Operation Time" 
                 SortExpression="OPERATION_TIME" >
             </asp:BoundField>
             <asp:BoundField DataField="OPERATION_TYPE" HeaderText="Operation Type" 
                 SortExpression="OPERATION_TYPE" >
             </asp:BoundField>
             <asp:BoundField DataField="HOST" HeaderText="Host" 
                 SortExpression="HOST" >
             </asp:BoundField>
             <asp:BoundField DataField="MODULE" HeaderText="Module" 
                 SortExpression="MODULE"  />
             <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                 SortExpression="REMARKS"  />
         </Columns>
         <PagerStyle CssClass="pgr" />
         <AlternatingRowStyle CssClass="alt" />
     </asp:GridView>
    
      </contenttemplate>
   </asp:UpdatePanel>
    </form>
    </body>
</html>
