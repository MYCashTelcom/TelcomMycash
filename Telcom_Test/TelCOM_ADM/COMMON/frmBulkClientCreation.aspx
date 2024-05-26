<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBulkClientCreation.aspx.cs" Inherits="Forms_frmBulkClinetCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">    
   
    <title>Bulk Upload</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            width: 214px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
  </ajaxToolkit:ToolkitScriptManager>
  <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <Triggers>
          <asp:PostBackTrigger ControlID="btnUpload" /> 
          <asp:PostBackTrigger ControlID="btnSampleData" /> 
          <asp:PostBackTrigger ControlID="btnCreateSub" /> 
     </Triggers>     
    <contenttemplate>  
       <div style="background-color: royalblue;">
        <table width="100%">
         <tr>
          <td class="style1">
          <strong><span style="color: white;font-size:14px;"> Bulk Account Registration</span></strong>
          </td>
          <td>
             &nbsp;
           <asp:Button ID="btnSampleData" runat="server" Text="Sample Data" onclick="btnSampleData_Click" />
          </td>
          <td></td>
          <td>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="White" Font-Size="10"></asp:Label>
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
       </div>
    <div>
    <asp:SqlDataSource ID="sdsBulkAccountFile" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand='SELECT BULK_ACCNT_CRE_ID, BULK_ACCNT_FILE_UPTIME, BULK_ACCNT_FILE, BULK_ACCNT_CRE_TIME, BULK_ACCNT_CRE_STATUS, BULK_ACCNT_RESULT, UPLOAD_SYS_USR_ID, UPLOAD_SYSTEM_IP, UPLOAD_SYSTEM_NAME, EXCUTE_SYS_USR_ID, EXECUTE_SYSTEM_IP, EXECUTE_SYSTEM_NAME,BULK_ACCNT_REGISTERED FROM BULK_ACCOUNT_CREATION ORDER BY BULK_ACCNT_FILE_UPTIME DESC'>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPendingBFile" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT BULK_ACCNT_CRE_ID, BULK_ACCNT_FILE, BULK_ACCNT_CRE_STATUS FROM BULK_ACCOUNT_CREATION WHERE (BULK_ACCNT_CRE_STATUS = 'P')">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsExecutedFile" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT BULK_ACCNT_CRE_ID, BULK_ACCNT_FILE, BULK_ACCNT_CRE_STATUS FROM BULK_ACCOUNT_CREATION WHERE (BULK_ACCNT_CRE_STATUS = 'E')">
    </asp:SqlDataSource>
    <asp:GridView ID="gdvBulkAccountFile" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        BorderColor="Silver" DataKeyNames="BULK_ACCNT_CRE_ID" DataSourceID="sdsBulkAccountFile"
        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="BULK_ACCNT_FILE" HeaderText="File Name" SortExpression="BULK_ACCNT_FILE" />
            <asp:TemplateField HeaderText="Status" SortExpression="BULK_ACCNT_CRE_STATUS">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("BULK_ACCNT_CRE_STATUS") %>'>
                        <asp:ListItem Selected="True" Value="P">Pending</asp:ListItem>
                        <asp:ListItem Value="E">Executed</asp:ListItem>
                        <asp:ListItem Value="F">Failed</asp:ListItem>
                        <asp:ListItem Value="R">Registered</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled="False" SelectedValue='<%# Bind("BULK_ACCNT_CRE_STATUS") %>'>
                        <asp:ListItem Selected="True" Value="P">Pending</asp:ListItem>
                        <asp:ListItem Value="E">Executed</asp:ListItem>
                        <asp:ListItem Value="F">Failed</asp:ListItem>
                        <asp:ListItem Value="R">Registered</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BULK_ACCNT_FILE_UPTIME" HeaderText="Upload time" SortExpression="BULK_ACCNT_FILE_UPTIME" />
            <asp:BoundField DataField="UPLOAD_SYS_USR_ID" HeaderText="Uloaded By" SortExpression="UPLOAD_SYS_USR_ID" />
            <asp:BoundField DataField="UPLOAD_SYSTEM_NAME" HeaderText="Uloaded From" SortExpression="UPLOAD_SYSTEM_NAME" />
            <asp:BoundField DataField="BULK_ACCNT_CRE_TIME" HeaderText="Execute Time" SortExpression="BULK_ACCNT_CRE_TIME" />
            <asp:BoundField DataField="EXCUTE_SYS_USR_ID" HeaderText="Execute By" SortExpression="EXCUTE_SYS_USR_ID" />
            <asp:BoundField DataField="EXECUTE_SYSTEM_NAME" HeaderText="Executed From" SortExpression="EXECUTE_SYSTEM_NAME" />
            <asp:BoundField DataField="BULK_ACCNT_RESULT" HeaderText="Submit Result" SortExpression="BULK_ACCNT_RESULT" />
            <asp:BoundField DataField="BULK_ACCNT_REGISTERED" HeaderText="Result" SortExpression="BULK_ACCNT_REGISTERED" />
        </Columns>
    </asp:GridView>
    <table>   
      <tr>
        <td style="background-color: royalblue" colspan="2">
            <strong><span style="color: #ffffff; font-size: 11px;">Bulk File Upload&nbsp; </span></strong>
        </td>
      </tr>
      <tr>
       <td colspan="2" style="text-align: right">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="501px" Height="24px" />
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
              DisplayModalPopupID="ModalPopupExtender1" onclientcancel="cancelClick" 
              TargetControlID="btnUpload">
             </ajaxToolkit:ConfirmButtonExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                   BackgroundCssClass="modalBackground" CancelControlID="ButtonCancel" 
                   OkControlID="ButtonOk" TargetControlID="btnUpload" PopupControlID="pnlPopUp">
             </ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="pnlPopUp" runat="server"  style=" display:none; width:300px;height:165px;background-color:White;border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
               <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                   Are you sure you want to Upload? <br />&nbsp;<br />&nbsp;
               </div>               
               <div style="text-align:right; background-color:#C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="ButtonOk" runat="server" Text="  OK  " />
                    <asp:Button ID="ButtonCancel" runat="server" Text=" Cancel " />
                    <br />&nbsp;
               </div>     
             </asp:Panel>        
       </td>
      </tr>
      <tr>            
       <td style="background-color: royalblue">
            <strong><span style="color: #ffffff; font-size: 11px;">Bulk Account Execute</span></strong>
       </td>
       <td style="background-color: royalblue">
            <strong><span style="color: #ffffff; font-size: 11px;">Bulk Registration</span></strong>
       </td>
    </tr> 
    <tr> 
        <td>
            <asp:DropDownList ID="ddlPendingFile" runat="server" DataSourceID="sdsPendingBFile" DataTextField="BULK_ACCNT_FILE" 
                DataValueField="BULK_ACCNT_CRE_ID">
            </asp:DropDownList>
            <asp:Button ID="btnCreateSub" runat="server" Text="Execute" 
                OnClick="btnCreateSub_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
              DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
              TargetControlID="btnCreateSub">
             </ajaxToolkit:ConfirmButtonExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                   BackgroundCssClass="modalBackground" CancelControlID="ButtonCancelUpdate" 
                   OkControlID="ButtonOkUpdate" TargetControlID="btnCreateSub" PopupControlID="pnlPopUpUpdate">
             </ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="pnlPopUpUpdate" runat="server"  style=" display:none; width:300px;height:165px;background-color:White;border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
               <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                   Are you sure you want to Execute? <br />&nbsp;<br />&nbsp;
               </div>               
               <div style="text-align:right; background-color:#C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="ButtonOkUpdate" runat="server" Text="  OK  " />
                    <asp:Button ID="ButtonCancelUpdate" runat="server" Text=" Cancel " />
                    <br />&nbsp;
               </div>     
             </asp:Panel>    
            <asp:Button ID="Button2" runat="server" Text="Update" OnClick="btnExecuted_Click" Visible="false" />
        </td> 
        <td>
            <asp:DropDownList ID="ddlPendingAccount" runat="server" DataSourceID="sdsExecutedFile" DataTextField="BULK_ACCNT_FILE" 
            DataValueField="BULK_ACCNT_CRE_ID">
            </asp:DropDownList>
            <asp:Button ID="btnCreateAcc" runat="server" Text="Registration" OnClick="btnExecuted_Click" />
            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
              DisplayModalPopupID="ModalPopupExtender3" onclientcancel="cancelClick" 
              TargetControlID="btnCreateAcc">
             </ajaxToolkit:ConfirmButtonExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
                   BackgroundCssClass="modalBackground" CancelControlID="ButtonCancelReg" 
                   OkControlID="ButtonOkReg" TargetControlID="btnCreateAcc" PopupControlID="pnlPopUpReg">
             </ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="pnlPopUpReg" runat="server"  style=" display:none; width:300px;height:165px;background-color:White;border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
               <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                   Are you sure you want to Register? <br />&nbsp;<br />&nbsp;
               </div>               
               <div style="text-align:right; background-color:#C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="ButtonOkReg" runat="server" Text="  OK  " />
                    <asp:Button ID="ButtonCancelReg" runat="server" Text=" Cancel " />
                    <br />&nbsp;
               </div>     
             </asp:Panel>
        </td>
        </tr>
     </table>
    </div>
    </contenttemplate>
    </asp:UpdatePanel>    
   </form>
</body>
</html>
