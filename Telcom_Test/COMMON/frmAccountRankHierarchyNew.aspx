<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountRankHierarchyNew.aspx.cs" Inherits="COMMON_frmAccountRankHierarchyNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Hierarchy</title>
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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
    <asp:SqlDataSource ID="sdsParentInfo" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsWalletInfo"  runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"             
            
            SelectCommand=" SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL,CLIENT_NAME(AH.HIERARCHY_ACCNT_ID) CLIENT_RANK_NAME,AH.HIERARCHY_ACCNT_ID,AH.UPDATED_BY,AH.MERCHANT_COMMISSION FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH  WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=''">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sdsParent" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                        SelectCommand="">
                    </asp:SqlDataSource>
        
        <asp:Panel ID="Panel1" runat="server" >
           <table width="100%" class="Top_Panel">
              <tr>
                <td>         
                  Manage Hierarchy 
                </td>
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
              <td valign="top">
                 <%--<fieldset style="border-color: #FFFFFF; height:auto;">--%>
                 <%-- <legend>Account Hierarchy Info</legend>--%>
                   <table>
                   <tr>
                       <td>
                           Wallet ID 
                       </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtWalletAccountNo" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" Width="62px"/>
                         </td>
                    </tr>
                    <tr>
                       <td colspan="4">
                          <asp:DetailsView ID="dtvAccInfo" runat="server" AutoGenerateRows="False" DataSourceID="sdsWalletInfo"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                               BorderStyle="None" Visible="False">   
                           <PagerStyle CssClass="pgr" />                   
                              <Fields>
                                  <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile No" 
                                      SortExpression="ACCNT_MSISDN" />
                                  <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                                      SortExpression="CLINT_NAME" />
                                  <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                                      SortExpression="SERVICE_PKG_NAME" />
                                  <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank" 
                                      SortExpression="RANK_TITEL" />
                                  <asp:BoundField DataField="CLIENT_RANK_NAME" HeaderText="Hierarchy" 
                                      SortExpression="CLIENT_RANK_NAME" />
                                  <asp:BoundField DataField="UPDATED_BY" HeaderText="Updated By" 
                                      SortExpression="UPDATED_BY" />
                                  <asp:BoundField DataField="MERCHANT_COMMISSION" HeaderText="Merchant Commission" 
                                      SortExpression="MERCHANT_COMMISSION" />
                              </Fields>
                          <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>
                       </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                          <asp:Label ID="lblParent" runat="server" Visible="false" Text="Parent Mobile No"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWalletID" runat="server" Text="Wallet ID" Visible="False"></asp:Label>
                        </td>
                        <td>                         
                            <asp:TextBox ID="txtWalletSearch" runat="server" Visible="False" Width="106px"></asp:TextBox>
                            <asp:Button ID="btnViewSelectedValue" runat="server" Text="Search" 
                                onclick="btnViewSelectedValue_Click" Visible="False" />
                        </td>                    
                        <td align="right">
                                <asp:DropDownList ID="ddlShowHierarchyList" runat="server"  AppendDataBoundItems="True"
                                    DataSourceID="sdsParent" DataTextField="ACCNT_NAME" 
                                    DataValueField="ACCNT_MSISDN" Visible="False">
                                </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" 
                                Visible="False" Width="62px" />
                        </td>
                    </tr>  
                       <tr>
                           <td>
                               <asp:Label ID="lblParentCommssion" runat="server" Text="Merchant Commission" Visible="False"></asp:Label>
                           </td>
                           <td colspan="3">
                               <asp:TextBox ID="txtParentCommssion" runat="server" Visible="False" Width="106px"></asp:TextBox>
                           </td>
                       </tr>                  
                    <tr>
                     <td colspan="4">
                         <asp:DetailsView ID="dtvParentInfo" runat="server"   
                             DataSourceID="sdsParentInfo"  CssClass="mGrid" PagerStyle-CssClass="pgr" 
                             AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
                             AutoGenerateRows="False" Visible="False" >
                             <PagerStyle CssClass="pgr" /> 
                             <Fields>
                                 <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile No" 
                                     SortExpression="ACCNT_MSISDN" />
                                 <asp:BoundField DataField="CLINT_NAME" HeaderText="Name" 
                                     SortExpression="CLINT_NAME" />
                                 <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                                     SortExpression="SERVICE_PKG_NAME" />
                                 <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank" 
                                     SortExpression="RANK_TITEL" />
                             </Fields>
                             <AlternatingRowStyle CssClass="alt" />
                         </asp:DetailsView>
                     </td>
                    </tr>
                    <tr>                  
                      <td colspan="4" align="center">
                          <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                              Visible="False" Width="62px"  />            
                      
                          <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                              Text="Update" Visible="False" />
                      </td>
                    </tr>
                    </table>
                <%-- </fieldset> --%>  
              </td>
            </tr>
        </table>    
           
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
