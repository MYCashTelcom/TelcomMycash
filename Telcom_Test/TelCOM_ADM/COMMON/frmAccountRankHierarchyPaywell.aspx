<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountRankHierarchyPaywell.aspx.cs" Inherits="COMMON_frmAccountRankHierarchyPaywell" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Hierarchy(Paywell)</title>
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
         	color:White;
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
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <asp:Panel ID="Panel1" runat="server" >
           <table width="100%" class="Top_Panel">
              <tr>
                <td> <strong>Define Hierarchy(Paywell) </strong> </td>
                <td>
                </td>
                <td>
                </td>
                <td align="left">
                  <asp:Label ID="lblMessage" runat="server"  Text=""></asp:Label>
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
         
         
         <table>
         <tr>
          <td>
           <strong>Account No</strong>   
          </td>
          <td>
           <asp:TextBox runat="server" ID="txtSearchAccount"></asp:TextBox> 
           <asp:Button runat="server" ID="btnSearch" Text="Search " 
                  onclick="btnSearch_Click"/>  
          </td>   
         </tr>
         <tr>
          <td></td>   
          <td >
            <asp:DetailsView ID="dtvSearchAccInfo" runat="server" AutoGenerateRows="False" DataKeyNames="ACCNT_ID"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                               BorderStyle="None" Visible="False" Width="500px">   
                           <PagerStyle CssClass="pgr" /> 
                           
                           <Fields>
                               <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" />
                               <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile No" />
                               <asp:BoundField DataField="CLINT_NAME" HeaderText="Account Name" />
                               <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank" />
                               <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" />
                               <asp:BoundField DataField="HIERARCHY_NAME_ADDRESS" 
                                   HeaderText="Hierarchy Account Information" />
                               <asp:BoundField DataField="UPBY_INFO" HeaderText="Last Updated By" />
                           </Fields>
                           
                           <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>  
          </td>   
         </tr> 
         
         <tr>
          <td>
           <strong>
            <asp:Label runat="server" ID="lblParent" Visible="False">Hierarchy Account</asp:Label>   
           </strong>   
          </td>
          <td>
           <asp:TextBox runat="server" ID="txtParentAccount" Visible="False"></asp:TextBox>
           <asp:Button runat="server" ID="btnParentSearch" Text="Search " 
                  onclick="btnParentSearch_Click" Visible="False"/>
              
          </td>   
         </tr>
         <tr>
          <td></td>   
          <td >
            <asp:DropDownList runat="server" ID="drpParent" DataValueField="ACCNT_NO" DataTextField="UPPER_HIERARCHY_INFO" Visible="False"/>
           <asp:Button runat="server" ID="btnParentInfo" Text="View Information"  
                  onclick="btnParentInfo_Click" Visible="False"/>  
          </td>   
         </tr>
         <tr>
          <td>
           <asp:HiddenField runat="server" ID="hdfRankId"/>
           <asp:HiddenField runat="server" ID="hdfTerritoryRankId"/>   
          </td>
          <td>
            <asp:DetailsView ID="dtvParentInfo" runat="server" AutoGenerateRows="False" DataKeyNames="ACCNT_ID"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                               BorderStyle="None" Visible="False" >   
                           <PagerStyle CssClass="pgr" /> 
                           
                           <Fields>
                               <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" />
                               <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile No" />
                               <asp:BoundField DataField="CLINT_NAME" HeaderText="Account Name" />
                               <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank" />
                               <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" />
                               <asp:BoundField DataField="HIERARCHY_NAME_ADDRESS" HeaderText="Hierarchy Account Information" />
                               <asp:BoundField DataField="UPBY_INFO" HeaderText="Last Updated By" />
                           </Fields>
                           
                           <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>  
              
              
          </td>   
         </tr> 
         <tr>
          <td></td>
          <td>
           <asp:Button runat="server" ID="btnHieraychySave" Text="Save Hierarchy" 
                  onclick="btnHieraychySave_Click" Visible="False"/>   
          </td>   
         </tr>
         
         </table>
         
         </ContentTemplate>
         </asp:UpdatePanel>
    
    
    </form>
</body>
</html>
