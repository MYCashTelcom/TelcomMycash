<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGPISOReconciliation.aspx.cs" Inherits="ISO_frmGPISOReconciliation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GP ISO Reconciliation</title>
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
        </style>
</head>

<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <%--<asp:SqlDataSource ID="sdsStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME,
            ISO_REQUEST_CODE, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_REQUEST_STATUS,
            ISO_CLIENT_REQ_ID, ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_PROCESS_STATUS,
            ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, REQUEST_ID, ISO_REQUEST_INSERT_TIME,
            ISO_REQUEST_LOG, ISO_RESPONSE_LOG, HTTP_REQUEST_TIME, HTTP_RESPONSE_TIME,
            ISO_ROLLBACK_REF_ID, ISO_CHECK_STATUS_REF_ID FROM ISO_REQUEST ">
      </asp:SqlDataSource>--%>
      <%--WHERE ISO_CLIENT_REQ_ID=ISO_CLIENT_REQ_ID--%>
      <asp:SqlDataSource ID="sdsStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT R.ISO_CLIENT_REQ_ID,R.CS_ID,R.ISO_RESPONSE_CODE,R.DIFF ,R.CS_RES_CODE,R.CS_DIFF ,
            RB.ISO_ROLLBACK_REF_ID RollbackID,RB.ISO_RESPONSE_CODE RB_RES_CODE,R.REQ_TIME,R.REQ_TIME_CS,
            DATEDIFFINSEC(TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'),
            TO_DATE(SUBSTR(RB.ISO_RESPONSE_LOG,1,INSTR(RB.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS RB_DIFF,
            TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') AS REQ_TIME_RB ,
            R.ISO_REQUEST_LOG,R.ISO_RESPONSE_LOG,R.CS_REQUEST_LOG,R.CS_RESPONSE_LOG,RB.ISO_REQUEST_LOG RB_REQ_LOG,
            RB.ISO_RESPONSE_LOG RB_RES_LOG FROM (SELECT  M.ISO_CLIENT_REQ_ID,CS.ISO_CHECK_STATUS_REF_ID CS_ID,
            M.ISO_RESPONSE_CODE,M.DIFF ,CS.ISO_RESPONSE_CODE CS_RES_CODE, 
            DATEDIFFINSEC(TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'),
            TO_DATE(SUBSTR(CS.ISO_RESPONSE_LOG,1,INSTR(CS.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS CS_DIFF ,M.REQ_TIME,
            TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') as REQ_TIME_CS,
            M.ISO_REQUEST_LOG,M.ISO_RESPONSE_LOG, CS.ISO_REQUEST_LOG CS_REQUEST_LOG,CS.ISO_RESPONSE_LOG CS_RESPONSE_LOG
            FROM (SELECT ISO_CLIENT_REQ_ID,ISO_RESPONSE_CODE,DATEDIFFINSEC(TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'),
            TO_DATE(SUBSTR(ISO_RESPONSE_LOG,1,INSTR(ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS DIFF,TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')) ,'MM/DD/YYYY HH:MI:SS AM')as  REQ_TIME ,ISO_REQUEST_LOG,ISO_RESPONSE_LOG
            FROM ISO_REQUEST WHERE ISO_CLIENT_REQ_ID  IN('ISO_CLIENT_REQ_ID')) M,ISO_REQUEST CS WHERE M.ISO_CLIENT_REQ_ID=CS.ISO_CHECK_STATUS_REF_ID(+) ) R ,
            ISO_REQUEST RB WHERE  R.ISO_CLIENT_REQ_ID=RB.ISO_ROLLBACK_REF_ID(+) ">
      </asp:SqlDataSource>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Height="25px" >
                 <table style="width: 100%" align="left"  >
                     <tr align="left" valign="top">
                         <td align="left" style="width: 120px">
                             <asp:Label runat="server" ID="lblTitle"><strong>ISO Reconciliation</strong></asp:Label>
                         </td>
                         <td align="left">
                           <asp:Label runat="server" ID="lblMsg"></asp:Label>
                       </td>
                         <td align="left" style="width: 50px">
                           <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                            <ProgressTemplate>
                             <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;</ProgressTemplate>
                           </asp:UpdateProgress>
                       </td>
                     </tr>
                 </table>
             </asp:Panel>
             <fieldset style="width: 400px">
                 <legend><strong>ISO Reconciliation</strong></legend>
                  <table style="width: 400px" align="left"  >
                     <tr>
                       <td style="width: 90px">
                         <asp:Label runat="server" ID="lblSample"><strong>Sample Download</strong></asp:Label>  
                       </td>
                       <td>
                         <asp:Button runat="server" ID="btnSample" Text="Download" 
                               onclick="btnSample_Click"/>  
                       </td>  
                     </tr>
                     
                     <tr align="left" valign="top">
                         
                         <td style="width: 90px" >
                             <asp:Label runat="server" ID="lblBankList"><strong>ISO Client Request Id</strong></asp:Label>
                         </td>
                         <td align="left" class="style3" >
                             <asp:TextBox ID="txtReqID" runat="server" TextMode="MultiLine" Height="55px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                       <td style="width: 90px"></td>
                       <td>
                         <asp:Button runat="server" ID="btnShow" Text="Show" onclick="btnShow_Click"/>  
                           <asp:Button runat="server" ID="btnReport" Text="Show Report" 
                                 onclick="btnReport_Click"/>  
                       </td>  
                     </tr>
                 </table>
             </fieldset>
             
             <asp:GridView ID="gdvStatus" runat="server" AutoGenerateColumns="False" runat="server" 
                    DataKeyNames="ISO_CLIENT_REQ_ID" DataSourceID="sdsStatus"
                    CssClass="mGrid" Width="100%" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
                     AllowPaging="True"  pagesize="15" BorderColor="#E0E0E0" ShowFooter="True" >             
                <Columns>
                    <asp:BoundField DataField="ISO_CLIENT_REQ_ID" HeaderText="ISO Client Request Id" ReadOnly="True" 
                        SortExpression="ISO_CLIENT_REQ_ID" />
                    <asp:BoundField DataField="ISO_RESPONSE_CODE" HeaderText="ISO Response Code" ReadOnly="True" 
                        SortExpression="ISO_RESPONSE_CODE" />
                    <asp:BoundField DataField="REQ_TIME" HeaderText="Request Time" ReadOnly="True" 
                        SortExpression="REQ_TIME" />
                    <asp:BoundField DataField="DIFF" HeaderText="Time Difference(In Second)" ReadOnly="True" 
                        SortExpression="DIFF" />
                    <asp:BoundField DataField="CS_ID" HeaderText="Check Status Id" ReadOnly="True" 
                        SortExpression="CS_ID" />
                    <asp:BoundField DataField="REQ_TIME_CS" HeaderText="Check Status Request Time " ReadOnly="True" 
                        SortExpression="REQ_TIME_CS" />
                    <asp:BoundField DataField="CS_RES_CODE" HeaderText="Check Status Response Code" ReadOnly="True" 
                        SortExpression="CS_RES_CODE" />
                    <asp:BoundField DataField="CS_DIFF" HeaderText="Check Status Time Difference" ReadOnly="True" 
                        SortExpression="CS_DIFF" />
                    <asp:BoundField DataField="ROLLBACKID" HeaderText="Roll Back Id" ReadOnly="True" 
                        SortExpression="ROLLBACKID" />
                    <asp:BoundField DataField="REQ_TIME_RB" HeaderText="Roll Back Request Time" ReadOnly="True" 
                        SortExpression="REQ_TIME_RB" />
                    <asp:BoundField DataField="RB_RES_CODE" HeaderText="Roll Back Response Code" ReadOnly="True" 
                        SortExpression="RB_RES_CODE" />
                    <asp:BoundField DataField="RB_DIFF" HeaderText="Roll Back Time Difference(In Second)" ReadOnly="True" 
                        SortExpression="RB_DIFF" />
                    <asp:BoundField DataField="ISO_REQUEST_LOG" HeaderText="ISO Request Log" ReadOnly="True" 
                        SortExpression="ISO_REQUEST_LOG" />
                    <asp:BoundField DataField="ISO_RESPONSE_LOG" HeaderText="ISO Response Log" ReadOnly="True" 
                        SortExpression="ISO_RESPONSE_LOG" />
                    <asp:BoundField DataField="CS_REQUEST_LOG" HeaderText="Check Status Request Log" ReadOnly="True" 
                        SortExpression="CS_REQUEST_LOG" />
                    <asp:BoundField DataField="CS_RESPONSE_LOG" HeaderText="Check Status Response Log" ReadOnly="True" 
                        SortExpression="CS_RESPONSE_LOG" />
                    <asp:BoundField DataField="RB_REQ_LOG" HeaderText="Roll Back Request Log" ReadOnly="True" 
                        SortExpression="RB_REQ_LOG" />
                    <asp:BoundField DataField="RB_RES_LOG" HeaderText="Roll Back Response Log" ReadOnly="True" 
                        SortExpression="RB_RES_LOG" />
                </Columns>
               <PagerStyle CssClass="pgr"></PagerStyle>
              <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
       <asp:PostBackTrigger ControlID="btnSample" /> 
       <asp:PostBackTrigger ControlID="btnReport" />  
     </Triggers>
      </asp:UpdatePanel>
    </form>    
 </body>
</html>
