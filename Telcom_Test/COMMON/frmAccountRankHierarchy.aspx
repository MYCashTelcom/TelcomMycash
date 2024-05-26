<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountRankHierarchy.aspx.cs" Inherits="COMMON_frmAccountRankHierarchy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Hierarchy</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:SqlDataSource ID="sdsParentInfo" runat="server" 
         ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand=""></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsWalletInfo"  runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            
            
            SelectCommand=" SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL,CLIENT_NAME(AH.HIERARCHY_ACCNT_ID) CLIENT_RANK_NAME,AH.HIERARCHY_ACCNT_ID,AH.UPDATED_BY FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH  WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=''">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sdsParent" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                        SelectCommand="">
                    </asp:SqlDataSource>
            <div>
                <div style="background-color: royalblue">
                            <strong><span style="color: white">Manage Hierarchy&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMessage" runat="server"  Text=""></asp:Label></span></strong>
                            
                </div>
        <table>
            <tr>
              <td valign="top">
                 <%--<fieldset style="border-color: #FFFFFF; height:auto;">--%>
                 <%-- <legend>Account Hierarchy Info</legend>--%>
                   <table>
                   <tr>
                        <td colspan="3">
                               Wallet ID 
                            <asp:TextBox ID="txtWalletAccountNo" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" Width="62px"/>
                         </td>
                    </tr>
                    <tr>
                       <td colspan="3">
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
                              </Fields>
                          <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>
                       </td>
                    </tr>
                    <tr>
                      <td colspan="3">
                          <asp:Label ID="lblParent" runat="server" Visible="false" Text="Parent Mobile No"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWalletID" runat="server" Text="Wallet ID" Visible="False"></asp:Label>
                          
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
                     <td colspan="3">
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
                      <td colspan="3" align="center">
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
        </div>    
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
