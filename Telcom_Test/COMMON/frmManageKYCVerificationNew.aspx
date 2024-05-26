<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmManageKYCVerificationNew.aspx.cs" Inherits="COMMON_frmPINInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> KYC Verification </title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />
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
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" >
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
      <contenttemplate>
        <asp:SqlDataSource ID="sdsClientList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
              SelectCommand="
 SELECT CM.SERIAL_NO, MD.DISTRICT_NAME,MT.THANA_NAME,C.CLINT_NAME,C.HUSBAND_NAME,A.ACCNT_NO,A.ACCNT_PIN,A.ACCNT_MSISDN,SP.SERVICE_PKG_NAME, AR.RANK_TITEL, C.CLINT_FATHER_NAME, C.CLINT_MOTHER_NAME ,
 C.CLIENT_DOB,C.OCCUPATION,C.WORK_EDU_BUSINESS,C.PUR_OF_TRAN, C.CLIENT_OFFIC_ADDRESS, C.CLINT_ADDRESS1,C.CLINT_ADDRESS2, BA.BANK_BR_NAME,BA.BANK_ACCNT_NO,
 IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,INF.INTRODCR_NAME,INF.INTRODCR_MOBILE,INF.INTRODCR_ADDRESS,INF.INTRODCR_OCCUPATION, NI.NOMNE_NAME,NI.NOMNE_MOBILE,
 NI.RELATION,NI.PERCENTAGE 
  FROM CLIENT_LIST C,ACCOUNT_LIST A, ACCOUNT_RANK AR,SERVICE_PACKAGE SP,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,
 IDENTIFICATION_SETUP IDS,INTRODUCER_INFO INF  ,NOMINEE_INFO NI,MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_SERIAL_DETAIL CM
 WHERE A.CLINT_ID=C.CLINT_ID AND A.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND
 SP.SERVICE_PKG_ID=A.SERVICE_PKG_ID AND C.CLINT_ID=BA.CLIENT_ID(+) AND C.CLINT_ID=CI.CLIENT_ID(+) 
 AND IDS.IDNTIFCTION_ID(+)=CI.IDNTIFCTION_ID  AND C.CLINT_ID=INF.CLIENT_ID(+) AND
 C.CLINT_ID=NI.CLIENT_ID(+) AND A.ACCNT_NO=''  AND C.THANA_ID=MT.THANA_ID(+) AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID AND C.CLINT_MOBILE=CM.CUSTOMER_MOBILE_NO(+)
 ">
  </asp:SqlDataSource>
   <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
    <table width="100%">
     <tr>
      <td>
        Manage KYC Verification &nbsp;&nbsp;&nbsp;&nbsp;
      </td>
      <td></td>
      <td align="right">
       <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
      </td>
      <td>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
           <ProgressTemplate>
             <img alt="Loading" src="../resources/images/loading.gif" />                    
           </ProgressTemplate>
         </asp:UpdateProgress>
      </td>
     </tr>
    </table>   
       
   </asp:Panel>  
  <asp:Panel ID="Panel2" runat="server" CssClass="View_Panel" >
   <table>
    <tr>
        <td align="right" style="width:150px;">
           Search By Wallet ID
        </td>
        <td>
          <asp:TextBox ID="txtAccountNo" runat="server"></asp:TextBox>
           <asp:Button ID="btnView" runat="server"  Text="  View  " onclick="btnView_Click" />
        </td>
        <td>
        </td>
    </tr>
  </table>
   </asp:Panel> 
     <asp:Panel ID="Panel3" runat="server">
          
  
     <table>
      <tr>
       <td style="width:150px;">
       </td>
       <td align="center">
        <asp:DetailsView ID="dtvClient" runat="server" AutoGenerateRows="False" 
            CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" DataSourceID="sdsClientList" Visible="False" >
            <PagerStyle CssClass="pgr" />
            <Fields>
                <asp:TemplateField HeaderText="Form Serial No" SortExpression="SERIAL_NO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFormSLNo" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CLINT_NAME" HeaderText="Wallet Holder Name" 
                    SortExpression="CLINT_NAME" />
                <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" 
                    SortExpression="ACCNT_NO" />
                <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile Number" 
                    SortExpression="ACCNT_MSISDN" />
                <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                    SortExpression="SERVICE_PKG_NAME" />
                <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank" 
                    SortExpression="RANK_TITEL" />
                <asp:BoundField DataField="CLINT_FATHER_NAME" HeaderText="Father's Name" 
                    SortExpression="CLINT_FATHER_NAME" />
                <asp:BoundField DataField="CLINT_MOTHER_NAME" HeaderText="Mother's Name" 
                    SortExpression="CLINT_MOTHER_NAME" />
                 <asp:BoundField DataField="HUSBAND_NAME" HeaderText="Husband Name" 
                    SortExpression="HUSBAND_NAME" />   
                <asp:BoundField DataField="CLIENT_DOB" HeaderText="Date Of Birth" 
                    SortExpression="CLIENT_DOB" />
                <asp:BoundField DataField="OCCUPATION" HeaderText="Occupation" 
                    SortExpression="OCCUPATION" />
                <asp:BoundField DataField="DISTRICT_NAME" HeaderText="District" 
                    SortExpression="DISTRICT_NAME" />
                <asp:BoundField DataField="THANA_NAME" HeaderText="Thana" 
                    SortExpression="THANA_NAME" />        
                <asp:BoundField DataField="WORK_EDU_BUSINESS" HeaderText="Work/EDU/Business" 
                    SortExpression="WORK_EDU_BUSINESS" />
                <asp:BoundField DataField="PUR_OF_TRAN" HeaderText="Purpose Of Transaction" 
                    SortExpression="PUR_OF_TRAN" />
                <asp:BoundField DataField="CLIENT_OFFIC_ADDRESS" HeaderText="Office Address" 
                    SortExpression="CLIENT_OFFIC_ADDRESS" />
                <asp:BoundField DataField="CLINT_ADDRESS1" HeaderText="Present Address" 
                    SortExpression="CLINT_ADDRESS1" />
                <asp:BoundField DataField="CLINT_ADDRESS2" HeaderText="Permanent Address" 
                    SortExpression="CLINT_ADDRESS2" />
                <asp:BoundField DataField="BANK_BR_NAME" HeaderText="Bank Branch Name" 
                    SortExpression="BANK_BR_NAME" />
                <asp:BoundField DataField="BANK_ACCNT_NO" HeaderText="Bank Acc No" 
                    SortExpression="BANK_ACCNT_NO" />
                <asp:BoundField DataField="IDNTIFCTION_NAME" HeaderText="Identification Name" 
                    SortExpression="IDNTIFCTION_NAME" />
                <asp:BoundField DataField="CLINT_IDENT_NAME" HeaderText="Identification ID" 
                    SortExpression="CLINT_IDENT_NAME" />
                <asp:BoundField DataField="INTRODCR_NAME" HeaderText="Introducer Name" 
                    SortExpression="INTRODCR_NAME" />
                <asp:BoundField DataField="INTRODCR_MOBILE" HeaderText="Introducer Mobile" 
                    SortExpression="INTRODCR_MOBILE" />
                <asp:BoundField DataField="INTRODCR_ADDRESS" HeaderText="Introducer Address" 
                    SortExpression="INTRODCR_ADDRESS" />
                <asp:BoundField DataField="INTRODCR_OCCUPATION" HeaderText="Introducer Occupation" 
                    SortExpression="INTRODCR_OCCUPATION" />
                <asp:BoundField DataField="NOMNE_NAME" HeaderText="Nominee Name" 
                    SortExpression="NOMNE_NAME" />
                <asp:BoundField DataField="NOMNE_MOBILE" HeaderText="Nominee Mobile" 
                    SortExpression="NOMNE_MOBILE" />
                <asp:BoundField DataField="RELATION" HeaderText="Relation" 
                    SortExpression="RELATION" />
                <asp:BoundField DataField="PERCENTAGE" HeaderText="Percentage(%)" 
                    SortExpression="PERCENTAGE" />
            </Fields>
            <AlternatingRowStyle CssClass="alt" />
        </asp:DetailsView>
       </td>
      </tr>
      <tr>
        <td>
          </td>
            <td align="center">
            <asp:Button ID="btnVerify" runat="server" onclick="btnVerify_Click" 
                Text="Verify" Visible="false" />
             <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
              DisplayModalPopupID="ModalPopupExtender1" onclientcancel="cancelClick" 
              TargetControlID="btnVerify">
             </ajaxToolkit:ConfirmButtonExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                   BackgroundCssClass="modalBackground" CancelControlID="ButtonCancel" 
                   OkControlID="ButtonOk" TargetControlID="btnVerify" PopupControlID="pnlPopUp">
             </ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="pnlPopUp" runat="server"  style=" display:none; width:300px;height:165px;background-color:White;border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
               <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                   Are you sure you want to verify? <br />&nbsp;<br />&nbsp;
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
        </table>
     </asp:Panel>          
     </contenttemplate>
    </asp:UpdatePanel>
   </form>
</body>
</html>
