<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerritoryAccounts.aspx.cs" Inherits="MANAGE_TM_TO_frmTerritoryAccountsAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Pay Type</title>
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
    </style> 
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:SqlDataSource id="sdsRank" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT TERRITORY_RANK_ID, TERRITORY_RANK_NAME FROM MANAGE_TERRITORY_RANK WHERE TERRITORY_RANK_STATUS = 'A'">
    </asp:SqlDataSource> 
    
  <asp:UpdatePanel id="UpdatePanel1" runat="server">
   <contenttemplate>   
    <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
     <table style="width: 100%" align="right" >
       <tr>
         <td align="left">
           <asp:Label runat="server" ID="panelQ" Text="Define Territory Manager/Officer Accounts"></asp:Label> 
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
    
    <fieldset style="width: 500px">
      <legend><strong>Search by Account No</strong></legend>  
      <table style="width: 500px">
       <tr>
        <td><strong>Account No</strong></td>   
        <td>
         <asp:TextBox runat="server" ID="txtSearchAccount"></asp:TextBox>   
         <asp:Button runat="server" ID="btnSearch" Text="Search" onclick="btnSearch_Click"/>
        </td>   
       </tr>
       <tr>
        <td colspan="2">
          <asp:GridView ID="grvTerr" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                CssClass="GridViewClass" Width="600px" AllowPaging="True" DataKeyNames="ACCNT_NO"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="10"
                HeaderStyle-ForeColor="White" BorderStyle="None" 
                onrowcancelingedit="grvTerr_RowCancelingEdit" onrowediting="grvTerr_RowEditing" 
                onrowupdating="grvTerr_RowUpdating" >
            <Columns>
                <asp:TemplateField HeaderText="Account No">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Account Name">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Territory Rank" SortExpression="TERRITORY_RANK_ID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsRank" DataTextField="TERRITORY_RANK_NAME"
                            DataValueField="TERRITORY_RANK_ID" Enabled="True" SelectedValue='<%# Bind("TERRITORY_RANK_ID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsRank" DataTextField="TERRITORY_RANK_NAME"
                            DataValueField="TERRITORY_RANK_ID" Enabled="False" SelectedValue='<%# Bind("TERRITORY_RANK_ID") %>'>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:CommandField ShowEditButton="True" />
                
            </Columns>
            <PagerStyle CssClass="pgr" />
            <HeaderStyle ForeColor="White" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>  
        </td>   
       </tr>   
      </table>
      
    </fieldset>
    
    <br/>
    
    <fieldset style="width: 500px">
      <legend><strong>Define Territory Manager/Officer Accounts</strong></legend>  
      
      <table style="width: 500px">
        <tr>
         <td>
          <strong>Account No. </strong>  
         </td>   
         <td>
          <asp:TextBox runat="server" ID="txtAccount"></asp:TextBox>   
         </td>   
        </tr>
        <tr>
         <td>
          <strong>Territory Rank   </strong>
         </td>   
         <td>
          <asp:DropDownList runat="server" ID="drpTrRank" DataTextField="TERRITORY_RANK_NAME" DataValueField="TERRITORY_RANK_ID"/>   
         </td>   
        </tr>
        <tr>
         <td></td>   
         <td>
          <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" OnClientClick="javascript:return confirm('Are you sure, you want to Save?');"/>   
         </td>   
        </tr>  
      </table>
      
      
    </fieldset>
    
    
    
    
    </contenttemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
