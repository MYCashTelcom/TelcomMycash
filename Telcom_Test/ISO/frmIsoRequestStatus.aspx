<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmIsoRequestStatus.aspx.cs" Inherits="ISO_frmIsoRequestStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Iso Request Status</title>
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
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
        .style2
        {
            width: 85px;
        }
        .style3
        {
            width: 122px;
        }
        .style4
        {
            width: 47px;
        }
        .style6
        {
            width: 65px;
        }
        .style7
        {
            width: 134px;
        }
        .style8
        {
            width: 110px;
        }
        .style9
        {
            width: 124px;
        }
        .style10
        {
            width: 54px;
        }
        .style11
        {
            width: 77px;
        }
    </style>
</head>


<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <asp:SqlDataSource runat="server" ID="sdsISOReq"
             ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
             DeleteCommand="DELETE FROM ISO_REQUEST WHERE (ISO_REQ_ID = :ISO_REQ_ID)"
             InsertCommand="INSERT INTO ISO_REQUEST(ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_PROCESS_STATUS, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, REQUEST_ID, ISO_REQUEST_INSERT_TIME) VALUES (:ISO_REQUEST_PARTY, :ISO_RECEIPENT_PARTY, :ISO_REQUEST_TIME, :ISO_REQUEST_CODE, :ISO_REQUEST_STATUS, :ISO_RESPONSE_CODE, :ISO_RESPONSE_TIME, :ISO_CLIENT_REQ_ID, :ISO_EXCEPTION, :ISO_OWNER_CODE, :ISO_REQUEST_TYPE, :ISO_REQ_PROCESS_STATUS, :ISO_REQ_AMOUNT, :HTTP_RESPONSE, :HTTP_RESPONSE_CODE, :REQUEST_ID, :ISO_REQUEST_INSERT_TIME)" 
             UpdateCommand="UPDATE ISO_REQUEST SET 
                                ISO_REQUEST_PARTY = :ISO_REQUEST_PARTY, 
                                ISO_RECEIPENT_PARTY = :ISO_RECEIPENT_PARTY, 
                                ISO_REQUEST_TIME = :ISO_REQUEST_TIME, 
                                ISO_REQUEST_CODE = :ISO_REQUEST_CODE, 
                                ISO_REQUEST_STATUS = :ISO_REQUEST_STATUS, 
                                ISO_RESPONSE_CODE = :ISO_RESPONSE_CODE, 
                                ISO_RESPONSE_TIME = :ISO_RESPONSE_TIME, 
                                ISO_CLIENT_REQ_ID = :ISO_CLIENT_REQ_ID, 
                                ISO_EXCEPTION = :ISO_EXCEPTION, 
                                ISO_OWNER_CODE = :ISO_OWNER_CODE, 
                                ISO_REQUEST_TYPE = :ISO_REQUEST_TYPE, 
                                ISO_REQ_PROCESS_STATUS = :ISO_REQ_PROCESS_STATUS, 
                                ISO_REQ_AMOUNT = :ISO_REQ_AMOUNT, 
                                HTTP_RESPONSE = :HTTP_RESPONSE, 
                                HTTP_RESPONSE_CODE = :HTTP_RESPONSE_CODE, 
                                REQUEST_ID = :REQUEST_ID, 
                                ISO_REQUEST_INSERT_TIME = :ISO_REQUEST_INSERT_TIME 
                                WHERE (ISO_REQ_ID = :ISO_REQ_ID)">
             
             <DeleteParameters>
                <asp:Parameter Name="ISO_REQ_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="ISO_REQUEST_PARTY" Type="String" />
                <asp:Parameter Name="ISO_RECEIPENT_PARTY" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_TIME" Type="DateTime" />
                <asp:Parameter Name="ISO_REQUEST_CODE" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_STATUS" Type="String" />
                <asp:Parameter Name="ISO_RESPONSE_CODE" Type="String" />
                <asp:Parameter Name="ISO_RESPONSE_TIME" Type="DateTime" />
                <asp:Parameter Name="ISO_CLIENT_REQ_ID" Type="String" />
                <asp:Parameter Name="ISO_EXCEPTION" Type="String" />
                <asp:Parameter Name="ISO_OWNER_CODE" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_TYPE" Type="String" />
                <asp:Parameter Name="ISO_REQ_PROCESS_STATUS" Type="String" />
                <asp:Parameter Name="ISO_REQ_AMOUNT" Type="Decimal" />
                <asp:Parameter Name="HTTP_RESPONSE" Type="String" />
                <asp:Parameter Name="HTTP_RESPONSE_CODE" Type="String" />
                <asp:Parameter Name="REQUEST_ID" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_INSERT_TIME" Type="DateTime" />
            </UpdateParameters>
            
            <InsertParameters>
                <asp:Parameter Name="ISO_REQUEST_PARTY" Type="String" />
                <asp:Parameter Name="ISO_RECEIPENT_PARTY" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_TIME" Type="DateTime" />
                <asp:Parameter Name="ISO_REQUEST_CODE" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_STATUS" Type="String" />
                <asp:Parameter Name="ISO_RESPONSE_CODE" Type="String" />
                <asp:Parameter Name="ISO_RESPONSE_TIME" Type="DateTime" />
                <asp:Parameter Name="ISO_CLIENT_REQ_ID" Type="String" />
                <asp:Parameter Name="ISO_EXCEPTION" Type="String" />
                <asp:Parameter Name="ISO_OWNER_CODE" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_TYPE" Type="String" />
                <asp:Parameter Name="ISO_REQ_PROCESS_STATUS" Type="String" />
                <asp:Parameter Name="ISO_REQ_AMOUNT" Type="Decimal" />
                <asp:Parameter Name="HTTP_RESPONSE" Type="String" />
                <asp:Parameter Name="HTTP_RESPONSE_CODE" Type="String" />
                <asp:Parameter Name="REQUEST_ID" Type="String" />
                <asp:Parameter Name="ISO_REQUEST_INSERT_TIME" Type="DateTime" />
            </InsertParameters>
            </asp:SqlDataSource>
            <%--SelectCommand="SELECT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_PROCESS_STATUS, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST ORDER BY ISO_REQUEST_TIME DESC"--%>
           
           <asp:SqlDataSource runat="server" ID="sdsISOReqOwnerCode"
             ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
             SelectCommand="SELECT DISTINCT ISO_OWNER_CODE FROM ISO_REQUEST ORDER BY ISO_OWNER_CODE" 
           ></asp:SqlDataSource>
           
           <asp:SqlDataSource runat="server" ID="sdsIsoReqStatus"
             ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
             SelectCommand="SELECT DISTINCT ISO_REQUEST_STATUS FROM ISO_REQUEST ORDER BY ISO_REQUEST_STATUS" 
           ></asp:SqlDataSource>
           <%--SELECT DISTINCT 
                            CASE 
                            WHEN ISO_REQUEST_STATUS = 'O' THEN 'Offline'
                            WHEN ISO_REQUEST_STATUS = 'F' THEN 'Failed'
                            WHEN ISO_REQUEST_STATUS = 'S' THEN 'Successful' 
                            END AS ISO_REQUEST_STATUS 
                            FROM ISO_REQUEST
                            ORDER BY ISO_REQUEST_STATUS--%>
           
           <asp:SqlDataSource ID="sdsISOR" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                     ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                     SelectCommand="">
           </asp:SqlDataSource> 
                 
           <asp:SqlDataSource runat="server" ID="sdsServiceProcessList"
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
            SelectCommand="SELECT DISTINCT IPC.ISO_PRO_CODE_ID, IPC.SERVICE_ID, IPC.PROCESSING_CODE, SL.SERVICE_TITLE 
                            FROM ISO_PROCESSING_CODE IPC, SERVICE_LIST SL
                            WHERE IPC.SERVICE_ID = SL.SERVICE_ID
                            ORDER BY SL.SERVICE_TITLE"
           ></asp:SqlDataSource>      
            
           <br/>
           <asp:Panel runat="server" ID="panel1" CssClass="Top_Panel" Height="55px" Width="1200px">
               <table style="width: 1200px ">
                   <tr>
                       <td class="style9" style="width: 120px">
                           <asp:Label runat="server" ID="lblTop1"><strong>ISO Request Status</strong></asp:Label>
                       </td>
                       <td class="style2" style="width: 40px">
                           <asp:Label ID="lblfdate" runat="server"><strong>From Date:</strong></asp:Label>
                       </td>
                       <td style="width: 170px" class="style11">
                           <cc1:GMDatePicker ID="dtpFromDate" runat="server" CalendarTheme="Silver" 
                               DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" 
                               Style="position: relative" TextBoxWidth="130">
                               <CalendarSelectedDayStyle BackColor="#FF0066" />
                               <CalendarTodayDayStyle BackColor="#996600" />
                               <CalendarDayHeaderStyle BackColor="Gray" />
                               <CalendarDayStyle BackColor="Gray" />
                           </cc1:GMDatePicker>
                       </td>
                       
                       <td class="style3" style="width: 100px">
                           <asp:Label ID="lblTdate" runat="server"><strong>To Date:</strong></asp:Label>
                       </td>
                       <td class="style8" style="width: 170px">
                           <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                               DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" 
                               Style="position: relative" TextBoxWidth="130">
                               <CalendarSelectedDayStyle BackColor="#FF0066" />
                               <CalendarTodayDayStyle BackColor="#996600" />
                               <CalendarDayHeaderStyle BackColor="Gray" />
                               <CalendarDayStyle BackColor="Gray" />
                               <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                           </cc1:GMDatePicker>
                       </td>
                       <td class="style6" style="width: 90px">
                           <asp:Label ID="lblServiceProcess" runat="server"><strong>Service Code:</strong></asp:Label>
                       </td>
                       <td class="style7" style="width: 280px">
                           <asp:CheckBox ID="chkBoxAllSrv" runat="server" AutoPostBack="True" 
                               oncheckedchanged="chkBoxAllSrv_CheckedChanged" Text="All" Checked="True" />
                               <asp:Label runat="server" ID="l" Text="  "></asp:Label>
                           <asp:DropDownList ID="ddlServiceListProcess" runat="server" 
                               DataSourceID="sdsServiceProcessList" DataTextField="SERVICE_TITLE" 
                               DataValueField="PROCESSING_CODE" AutoPostBack="True" 
                               onselectedindexchanged="ddlServiceListProcess_SelectedIndexChanged" />
                       </td>
                       <td class="style10" style="width: 50px">
                           
                       </td>
                       <td style="width: 150px">
                           &nbsp;</td>
                       
                   </tr>
                   
                   
                   
                   <tr>
                       
                       <td class="style9">
                       </td>
                       <td class="style2" style="width: 130px">
                           <asp:Label ID="lblReqSta" runat="server"><strong>ISO Request Status:</strong></asp:Label>
                       </td>
                       <td class="style11">
                           <asp:DropDownList ID="ddlReqStatus" runat="server" AppendDataBoundItems="True" 
                               AutoPostBack="True" onselectedindexchanged="ddlReqStatus_SelectedIndexChanged">
                               <asp:ListItem Text="All" Value="A"></asp:ListItem>
                               <asp:ListItem text="Successful" Value="S"></asp:ListItem>
                               <asp:ListItem text="Failed" Value="F"></asp:ListItem>
                               <%--<asp:listitem Value="O" text="Offline"></asp:listitem>--%>
                           </asp:DropDownList>
                       </td>
                       <td align="left" style="width: 70px">
                           <asp:Label ID="lblownercode" runat="server"><strong>Owner Code:</strong></asp:Label>
                       </td>
                       <td>
                           <asp:DropDownList ID="ddlOwnerCode" runat="server" 
                               DataSourceID="sdsISOReqOwnerCode" DataTextField="ISO_OWNER_CODE" 
                               DataValueField="ISO_OWNER_CODE" AutoPostBack="True" 
                               onselectedindexchanged="ddlOwnerCode_SelectedIndexChanged">
                           </asp:DropDownList>
                       </td>
                       <td class="style4">
                          <asp:Button runat="server" ID="btnShow" Text="Search" onclick="btnShow_Click"/>
                       </td>
                       <td>
                        <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" Text="Export Report" />   
                       </td>
                       
                       <td class="style10">
                           <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                            <ProgressTemplate>
                             <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                            </ProgressTemplate>
                           </asp:UpdateProgress>
                       </td>
                       <td class="style6">
                           <asp:Label runat="server" ID="lblMsg"></asp:Label>
                       </td>
                   </tr>
               </table>
           </asp:Panel>
           <br/>
           
           <asp:Panel runat="server" ID="panelTitle" CssClass="View_Panel" Visible="False" Width="1200px">
               <table class="View_Panel" style="color: #FFFFFF">
                   <tr>
                       <td>
                           <asp:Label runat="server" ID="lblGrid"><strong>ISO Request Status Details:</strong></asp:Label>
                       </td>
                   </tr>
               </table>
           </asp:Panel>
           <br/>
           <asp:Panel runat="server" ID="panelBody">
               <table>
                   <tr>
                       <td>
                           <asp:GridView ID="grdIsoRequest" runat="server" AllowPaging="True" 
                                PageSize="10"  Width="812px" AllowSorting="True" 
                                DataSourceID="sdsISOReq"  AutoGenerateColumns="False" DataKeyNames="ISO_REQ_ID" 
                                BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                               AlternatingRowStyle-CssClass="alt" Visible="False" 
                               onpageindexchanged="grdIsoRequest_PageIndexChanged" 
                               onpageindexchanging="grdIsoRequest_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ISO_REQ_ID" HeaderText="Iso Req Id" />
                                    <asp:BoundField DataField="ISO_REQUEST_PARTY" HeaderText="Iso Req Party" />
                                    <asp:BoundField DataField="ISO_RECEIPENT_PARTY" HeaderText="Iso Recp Party" />
                                    <asp:BoundField DataField="ISO_REQUEST_TIME" HeaderText="Req Time" />
                                    <asp:BoundField DataField="ISO_REQUEST_CODE" HeaderText="Req Code" />
                                    <asp:BoundField DataField="ISO_REQUEST_STATUS" HeaderText="Req Status" />
                                    <asp:BoundField DataField="ISO_RESPONSE_CODE" HeaderText="Response Code" />
                                    <asp:BoundField DataField="ISO_RESPONSE_TIME" HeaderText="Response Time" />
                                    <asp:BoundField DataField="ISO_CLIENT_REQ_ID" HeaderText="Client Req id" />
                                    <asp:BoundField DataField="ISO_EXCEPTION" HeaderText="Exception" />
                                    <asp:BoundField DataField="ISO_OWNER_CODE" HeaderText="ISO Owner Code" />
                                    <asp:BoundField DataField="ISO_REQUEST_TYPE" HeaderText="Req Type" />
                                    <asp:BoundField DataField="ISO_REQ_PROCESS_STATUS" HeaderText="Req Prc Status" />
                                    <asp:BoundField DataField="ISO_REQ_AMOUNT" HeaderText="Amount" />
                                    <asp:BoundField DataField="HTTP_RESPONSE" HeaderText="Http Response" />
                                    <asp:BoundField DataField="HTTP_RESPONSE_CODE" HeaderText="Http Resp Code" />
                                    <asp:BoundField DataField="REQUEST_ID" HeaderText="Request Id" />
                                    <asp:BoundField DataField="ISO_REQUEST_INSERT_TIME" HeaderText="Req Insert Time" />
                                </Columns>  
                                 <PagerStyle CssClass="pgr" />
                                 <AlternatingRowStyle CssClass="alt" />                                  
                           </asp:GridView>
                       </td>
                   </tr>
               </table>
           </asp:Panel>
            
        </ContentTemplate>
        <Triggers>
         <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
      </asp:UpdatePanel>
    </form>
</body>
</html>
