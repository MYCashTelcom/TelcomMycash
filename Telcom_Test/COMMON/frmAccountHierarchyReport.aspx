<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountHierarchyReport.aspx.cs" Inherits="COMMON_frmAccountHierarchyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Hierarchy Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
      #UpdateProgress1 
      {
        background-color:transparent;
        color:White;               
        top: 300px;
        left:512px; 
        position:fixed;
      }             
     #UpdateProgress1 img {
        vertical-align:middle;
        margin:2px;
      }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:SqlDataSource ID="sdsParentReportInfo" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand="">
        </asp:SqlDataSource>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       <div>
         <table>
           <tr>
             <td valign="top">
               <fieldset style="border-color: #FFFFFF; height:47px;">
                    <legend>Account Hierarchy Report</legend>
                      <table>
                       <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Account Hierarchy Type"></asp:Label>                          
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectHierarchyType" runat="server" 
                                AutoPostBack="true" 
                                onselectedindexchanged="ddlSelectHierarchyType_SelectedIndexChanged" 
                                Height="23px">                                
                                <asp:ListItem Value="B">Bounded Hierarchy</asp:ListItem>
                                <asp:ListItem Value="N">Un Bounded Hierarchy</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      
                           <td align="center">
                               <asp:Button ID="btnReport" runat="server" Text="View" Width="62px" 
                                   onclick="btnReport_Click" />                        
                           </td>
                       </tr>
                      </table>   
                     
                  </fieldset> 
              </td> 
              <td valign="top">
                 <fieldset  style="border-color: #FFFFFF; height:auto;">
                      <legend>Account Parent Report</legend>
                       <table>
                              <tr>
                                 <td>
                                    Search By Wallet ID
                                 </td>
                                 <td>
                                     <asp:TextBox ID="txtAccountNo" runat="server" Height="22px"></asp:TextBox>
                                 </td>
                                 <td>
                                     <asp:Button ID="btnParentInfoSearch" runat="server" Text="Search" 
                                         onclick="btnParentInfoSearch_Click" Width="62px" />                               
                                 </td>
                              </tr>
                              <tr>
                                   <td colspan="3">
                                       <asp:DetailsView ID="dtvParentReportInfo" runat="server" 
                                           AutoGenerateRows="False"  DataSourceID="sdsParentReportInfo" 
                                           CssClass="mGrid"   PagerStyle-CssClass="pgr" 
                                           AlternatingRowStyle-CssClass="alt" BorderStyle="None" Visible="False">
                                             <PagerStyle CssClass="pgr" />                   
                                               <Fields>
                                                   <asp:TemplateField HeaderText="Mobile No" SortExpression="ACCNT_MSISDN">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_MSISDN") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <InsertItemTemplate>
                                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_MSISDN") %>'></asp:TextBox>
                                                       </InsertItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblAccMobile" runat="server" Text='<%# Bind("ACCNT_MSISDN") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Name" SortExpression="CLINT_NAME">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <InsertItemTemplate>
                                                           <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                                                       </InsertItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblClientName" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Service Package" 
                                                       SortExpression="SERVICE_PKG_NAME">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox3" runat="server" 
                                                               Text='<%# Bind("SERVICE_PKG_NAME") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <InsertItemTemplate>
                                                           <asp:TextBox ID="TextBox3" runat="server" 
                                                               Text='<%# Bind("SERVICE_PKG_NAME") %>'></asp:TextBox>
                                                       </InsertItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblServicePackage" runat="server" Text='<%# Bind("SERVICE_PKG_NAME") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Account Rank" SortExpression="RANK_TITEL">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("RANK_TITEL") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <InsertItemTemplate>
                                                           <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("RANK_TITEL") %>'></asp:TextBox>
                                                       </InsertItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblRankTitle" runat="server" Text='<%# Bind("RANK_TITEL") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Fields>
                                               <AlternatingRowStyle CssClass="alt" /> 
                                       </asp:DetailsView>
                                   </td>
                              </tr>
                              <tr>
                                 <td colspan="3" align="center">
                                     <asp:Button ID="btnReportView" runat="server" Text="View" 
                                         onclick="btnReportView_Click" Visible="False" Width="62px" /> 
                                 </td>
                              </tr>
                       </table>                      
                    </fieldset> 
             </tr>
           </table>   
        
        <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
           <ProgressTemplate>
           <img  alt="loading" src="../Images/ajax-loader.gif"/>
           </ProgressTemplate>
        </asp:UpdateProgress>     
       </div>    
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
