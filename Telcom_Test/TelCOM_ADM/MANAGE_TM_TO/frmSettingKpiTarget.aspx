<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSettingKpiTarget.aspx.cs" Inherits="MANAGE_TM_TO_frmSettingKpiTarget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign KPI Target </title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
       .Font_Color
       {
       	color:White;
       	}
       	.Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         }
         .View_Panel
         {         	
         	 width:100%;
         	 background-color:powderblue;       	
         }
       	
       	.GridViewClass { width: 100%; background-color: #fff; margin: 1px 0 10px 0; 
        border: solid 1px #525252; border-collapse:collapse;
            text-align: left;
        }
            .GridViewClass td { padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
            .GridViewClass th
        {
	        padding: 4px 2px;
	        color: #fff;
	        background: url(../COMMON/grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
        .style3
        {
            width: 302px;
        }
        .style5
        {
            width: 209px;
        }
        .style6
        {
            width: 211px;
        }
    </style>
    
    
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
   <contenttemplate>   
    <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
     <table style="width: 100%" align="right" >
       <tr>
         <td align="left">
           <asp:Label runat="server" ID="panelQ" Text="Assign KPI to TO"></asp:Label> 
         </td>
         <td align="right">
           <asp:Label ID="lblMsg" runat="server" ></asp:Label>  
         </td>  
         <td align="right" style="width: 50px;">
           <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="True">
             <ProgressTemplate>
               <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;
             </ProgressTemplate>
            </asp:UpdateProgress> 
         </td>  
        </tr>
     </table> 
    </asp:Panel>
    
    <table style="width: 100%">
     <tr>
      <td align="left" valign="top" class="style6">
       <strong>Account No</strong>   
      </td> 
      <td align="left" valign="top" class="style3">
       <asp:TextBox runat="server" ID="txtSearchTOAcc"></asp:TextBox>
       <asp:Button runat="server" ID="btnTOInfo" Text="View" 
              onclick="btnTOInfo_Click"/>   
      </td>
      <td>
          
      </td>  
     </tr>
     <tr>
      <td colspan="3">
        <asp:DetailsView ID="dtvSearchAccInfo" runat="server" AutoGenerateRows="False" DataKeyNames="ACCNT_ID"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="450px"
                               BorderStyle="None" Visible="True" >   
                           <PagerStyle CssClass="pgr" /> 
                           
                           <Fields>
                               <asp:BoundField DataField="ACCNT_ID" HeaderText="Account Id" Visible="False"/>
                               <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" />
                               <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile No" />
                               <asp:BoundField DataField="CLINT_NAME" HeaderText="Account Name" />
                               <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank" />
                               <asp:BoundField DataField="TERRITORY_RANK_NAME" HeaderText="Territory Rank" />
                               <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" />
                               <asp:BoundField DataField="HIERARCHY_NAME_ADDRESS" HeaderText="Hierarchy Account Information" />
                               <asp:BoundField DataField="UPBY_INFO" HeaderText="Last Updated By" />
                           </Fields>
                           
                           <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>  
      </td>   
         
     </tr>
     
     
        
    </table>
    
    <fieldset style="width: 430px">
     <legend><strong>Assign Target </strong></legend>
     <table style="width: 430px">
          <tr>
        <td>
            <strong> Select KPI Area</strong>
        </td>  
        <td>
          <asp:DropDownList runat="server" ID="DrpArea" AutoPostBack = "true" OnSelectedIndexChanged="Utarea_SelectedIndexChanged">  
              <asp:ListItem Selected="True" Value="0">Utility Area</asp:ListItem>
              <asp:ListItem>Non-Utility Area</asp:ListItem>
             
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>
            <strong> Select Year</strong>
        </td>  
        <td>
          <asp:DropDownList runat="server" ID="drpYear">  
              <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
              <asp:ListItem>2021</asp:ListItem>
              <asp:ListItem>2022</asp:ListItem>
              <asp:ListItem>2023</asp:ListItem>
              <asp:ListItem>2024</asp:ListItem>
              <asp:ListItem>2025</asp:ListItem>
              <asp:ListItem>2026</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
            <tr>
        <td>
            <strong> Select Year</strong>
        </td>  
        <td>
          <asp:DropDownList runat="server" ID="DrpMonth">  
              <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
              <asp:ListItem>Jan</asp:ListItem>
              <asp:ListItem>Feb</asp:ListItem>
              <asp:ListItem>Mar</asp:ListItem>
              <asp:ListItem>Apr</asp:ListItem>
              <asp:ListItem>May</asp:ListItem>
              <asp:ListItem>Jun</asp:ListItem>
              <asp:ListItem>Jul</asp:ListItem>
              <asp:ListItem>Aug</asp:ListItem>
              <asp:ListItem>Sep</asp:ListItem>
              <asp:ListItem>Oct</asp:ListItem>
              <asp:ListItem>Nov</asp:ListItem>
              <asp:ListItem>Dec</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      
      <tr>
       <td class="style5"><strong>Customer Registration</strong></td>   
       <td>
        <asp:TextBox runat="server" ID="txtCustAcqTar"></asp:TextBox>   
       </td>
      </tr>   
         <caption>
             &nbsp;</td>
             </tr>
             <tr>
                 <td class="style5"><strong>Transaction </strong></td>
                 <td>
                     <asp:TextBox ID="txtTrxAmtTar" runat="server"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td class="style5"><strong>Active Agent No</strong></td>
                 <td>
                     <asp:TextBox ID="txtActiveAgentNoTar" runat="server"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td class="style5"><strong>Corporate Collection </strong></td>
                 <td>
                     <asp:TextBox ID="txtLiftRfdTar" runat="server"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td><strong>Lifting Amount</strong> </td>
                 <td>
                     <asp:TextBox ID="txtLiftingTarget" runat="server"></asp:TextBox>
                 </td>
             </tr>
               <tr>
                 <td><strong>Active Agent transaction Amount</strong> </td>
                 <td>
                     <asp:TextBox ID="txtActvAgntTrxtAmt" runat="server"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td><strong>Utility Bill Payment </strong></td>
                 <td>
                     <asp:TextBox ID="txtUtilityBillPay" runat="server"   ></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td><strong>Remarks</strong> </td>
                 <td>
                     <asp:TextBox ID="txtRemarks" runat="server" style="resize:none" TextMode="MultiLine"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td></td>
                 <td>
                     <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" OnClientClick="javascript:return confirm('Are you sure you want to Save Target?'); " Text="Save " ValidationGroup="s" />
                 </td>
             </tr>
         </caption>
      
     </table>   
    </fieldset>
    
    </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
