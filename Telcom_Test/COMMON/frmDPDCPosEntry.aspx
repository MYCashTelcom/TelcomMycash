<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDPDCPosEntry.aspx.cs" Inherits="frmDPDCPosEntry"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">    
    .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	font-size:12px;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	width:817px;         	
         	}	
         .Inser_Panel	
         {
         	 width:100%;
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         	 font-size:12px;
         }
        .style1
        {
            width: 161px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
        
       
      
        <asp:SqlDataSource id="sdsDPDCPos" runat="server" 
            DeleteCommand="DELETE FROM DPDC_PREPAID_METER_POS_LIST WHERE POS_LIST_ID = :POS_LIST_ID"
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            
            
            InsertCommand="INSERT INTO DPDC_PREPAID_METER_POS_LIST (POS_LIST_ID, AAMRA_USER_ID, DPDC_POS_ID, IS_ACTIVE, USER_TYPE)
             VALUES (:POS_LIST_ID,:AAMRA_USER_ID, :DPDC_POS_ID, :IS_ACTIVE, 'Aamra')" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 

            UpdateCommand="UPDATE DPDC_PREPAID_METER_POS_LIST SET AAMRA_USER_ID = :AAMRA_USER_ID, DPDC_POS_ID = :DPDC_POS_ID, IS_ACTIVE = :IS_ACTIVE WHERE (POS_LIST_ID = :POS_LIST_ID)"
            

            SelectCommand="SELECT POS_LIST_ID,AAMRA_USER_ID,DPDC_POS_ID,IS_ACTIVE FROM DPDC_PREPAID_METER_POS_LIST WHERE USER_TYPE='Aamra' ORDER BY POS_LIST_ID  DESC ">
       <%-- <SelectParameters>
            <asp:ControlParameter ControlID="ddlBranch" Name="CMP_BRANCH_ID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>--%>
        <DeleteParameters>
            <asp:Parameter Name="POS_LIST_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="AAMRA_USER_ID" Type="String" />
            <asp:Parameter Name="DPDC_POS_ID" Type="String" />
            <asp:Parameter Name="IS_ACTIVE" Type="String" />
            <asp:Parameter Name="POS_LIST_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="POS_LIST_ID" Type="String" />
            <asp:Parameter Name="AAMRA_USER_ID" Type="String" />
            <asp:Parameter Name="DPDC_POS_ID" Type="String" />
            <asp:Parameter Name="IS_ACTIVE" Type="String" />
        </InsertParameters>
        </asp:SqlDataSource>

   <asp:Panel ID="pnlTop" runat="server">
     <table width="100%" class="Top_Panel">
      <tr>
        <td align="left" class="style1">         
          DPDC POS 
        </td>
        <td align="left" width="40%">
       
        </td>
       
       
        <td align="left">
          <asp:Label ID="lblMsg" runat="server" ></asp:Label>
        </td>
        <td align="left">
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
    <%--<asp:Panel ID="Panel1" runat="server" >--%>
        
        <asp:GridView ID="gdvCashAccType" runat="server" Width="900px" 
            AllowPaging="True"  PageSize="12" 
                AutoGenerateColumns="False" DataSourceID="sdsDPDCPos"
                BorderColor="#E0E0E0" GridLines="None"
             CssClass="mGrid" PagerStyle-CssClass="pgr" 
           AlternatingRowStyle-CssClass="alt" Font-Size="12px"             
            DataKeyNames="POS_LIST_ID" onrowupdating="gdvCashAccType_RowUpdating">

            <Columns>                
                <asp:BoundField DataField="POS_LIST_ID" HeaderText="POS_LIST_ID" 
                    ReadOnly="True" SortExpression="POS_LIST_ID" Visible="false" />
                <asp:BoundField DataField="AAMRA_USER_ID" HeaderText="AMRA User ID" 
                    SortExpression="AAMRA_USER_ID" />
                <asp:BoundField DataField="DPDC_POS_ID" HeaderText="DPDC Pos ID" 
                    SortExpression="DPDC_POS_ID" />
                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Active Status" 
                    SortExpression="IS_ACTIVE" />

                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                       <ajaxToolkit:ConfirmButtonExtender ID="cbeUpdate" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                             TargetControlID="btnUpdate">
                         </ajaxToolkit:ConfirmButtonExtender>  
                         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnUpdateCancel" 
                             OkControlID="btnUpdateOK" TargetControlID="btnUpdate" 
                             PopupControlID="pnlPopUpUpdate">
                         </ajaxToolkit:ModalPopupExtender>      
                            
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
      </asp:GridView>
      <asp:Panel ID="pnlPopUpUpdate" runat="server"  style=" display:none;width:300px; height:165px;
                  background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
              <div style="text-align:center; height:95px;"><br />&nbsp;<br />&nbsp;
              Are you sure you want to update?
                 <br />&nbsp;<br />&nbsp;
              </div>
              <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                    <br />&nbsp;
                    <asp:Button ID="btnUpdateOK" runat="server" Text="  Yes  " />
                    <asp:Button ID="btnUpdateCancel" runat="server" Text=" Cancel " />
              </div>
        </asp:Panel>
    <%--</asp:Panel>--%>
    </div>
    <asp:Panel ID="Panel2" runat="server">
     <table class="Inser_Panel">
      <tr>
       <td>
           Add New Pos
       </td>
      </tr>
     </table>               
       <asp:DetailsView id="dlvPOS" runat="server" DataSourceID="sdsDPDCPos" 
                BorderColor="Silver" Height="150px" Width="550px" AutoGenerateRows="False" 
                DefaultMode="Insert" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" Font-Size="12px"  BorderStyle="None" 
                oniteminserted="dlvPOS_ItemInserted" DataKeyNames="POS_LIST_ID">
           <PagerStyle CssClass="pgr" />
           <Fields>
               <asp:BoundField DataField="POS_LIST_ID" HeaderText="POS_LIST_ID" 
                   ReadOnly="True" SortExpression="POS_LIST_ID" Visible="false" />
               <asp:BoundField DataField="AAMRA_USER_ID" HeaderText="AMRA User ID" 
                   SortExpression="AAMRA_USER_ID" />
               <asp:BoundField DataField="DPDC_POS_ID" HeaderText="DPDC Pos ID" 
                   SortExpression="DPDC_POS_ID" />
               <asp:BoundField DataField="IS_ACTIVE" HeaderText="Active Status" 
                   SortExpression="IS_ACTIVE" />
              
                  
                      
               <asp:TemplateField ShowHeader="False">
                   <InsertItemTemplate>
                       <asp:Button ID="btnInsert" runat="server" CausesValidation="True" 
                           CommandName="Insert" Text="Insert" />
                       &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                           CommandName="Cancel" Text="Cancel" />
                        <ajaxToolkit:ConfirmButtonExtender ID="cbeInsert" runat="server" 
                             DisplayModalPopupID="ModalPopupExtender3" onclientcancel="cancelClick" 
                             TargetControlID="btnInsert" >
                         </ajaxToolkit:ConfirmButtonExtender>
                          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
                             BackgroundCssClass="modalBackground" CancelControlID="btnCancel" 
                             OkControlID="btnOK" TargetControlID="btnInsert" 
                             PopupControlID="pnlPopUpInsert">
                         </ajaxToolkit:ModalPopupExtender>    
                   </InsertItemTemplate>
                   <ItemTemplate>
                       <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                           CommandName="New" Text="New" />
                   </ItemTemplate>
               </asp:TemplateField>
           </Fields>
           <AlternatingRowStyle CssClass="alt" />
          </asp:DetailsView> 



      <%--  <asp:DetailsView ID="dtvPos" runat="server" AutoGenerateRows="False" 
        DataKeyNames="POS_LIST_ID" DataSourceID="sdsDPDCPos" DefaultMode="Insert" 
        GridLines="None" Height="50px" Width="350px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
            oniteminserted="dtvPos_ItemInserted">
            <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:BoundField DataField="POS_LIST_ID" HeaderText="POS_LIST_ID" 
                ReadOnly="True" SortExpression="POS_LIST_ID" Visible="False" />
            <asp:BoundField DataField="AAMRA_USER_ID" HeaderText="Wallet ID" 
                SortExpression="AAMRA_USER_ID" >
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>       
                  
             <asp:BoundField DataField="DPDC_POS_ID" HeaderText="POS ID" 
                SortExpression="DPDC_POS_ID" >
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Fields>
            <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>--%>



         <br />
          <asp:Panel ID="pnlPopUpInsert" runat="server"  style=" display:none;width:300px; height:165px;
                 background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
              <div style="text-align:center; height:95px;"><br />&nbsp;<br />&nbsp;
               Are you sure you want to save?
                 <br />&nbsp;<br />&nbsp;
              </div>
              <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                <br />&nbsp;
                <asp:Button ID="btnOK" runat="server" Text="  Yes  " />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " />
              </div>
           </asp:Panel>       
        </asp:Panel>             
       </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

