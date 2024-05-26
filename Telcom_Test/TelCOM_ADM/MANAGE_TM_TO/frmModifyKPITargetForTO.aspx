<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmModifyKPITargetForTO.aspx.cs" Inherits="MANAGE_TM_TO_frmModifyKPITargetForTO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify KPI Target </title>
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
            width: 125px;
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
           <asp:Label runat="server" ID="panelQ" Text="Modify KPI Target "></asp:Label> 
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
    
    <fieldset >
     <legend><strong></strong></legend>
     <table>
      <tr>
       <td class="style3">
         <strong> Account No</strong>  
       </td>   
       <td>
           <asp:TextBox runat="server" ID="txtToAccNo"></asp:TextBox>
       </td>
      </tr>   
      <tr>
          <td class="style3">
              <strong>Select Year </strong>
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
            <asp:Button runat="server" ID="btnView" Text="View " onclick="btnView_Click"/>
          </td>
      </tr>
      <tr>
       <td colspan="2">
         <asp:GridView ID="grvKpi" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                CssClass="GridViewClass" PageSize="12" DataKeyNames="KPI_TARGET_ID" Width="100%"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                HeaderStyle-ForeColor="White" BorderStyle="None" Visible="False" 
               onrowcancelingedit="grvKpi_RowCancelingEdit" onrowediting="grvKpi_RowEditing" 
               onrowupdating="grvKpi_RowUpdating">
                
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="False">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("KPI_TARGET_ID") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("KPI_TARGET_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acc Id" Visible="False">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TO_ACCNT_ID") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("TO_ACCNT_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account No">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Month">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("TARGET_MONTH") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("TARGET_MONTH") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year">
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("TARGET_YEAR") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("TARGET_YEAR") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Registration">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" 
                                Text='<%# Bind("CUST_ACQU_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("CUST_ACQU_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="DPS Account Acquisition">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" 
                                Text='<%# Bind("DPS_ACC_ACQU_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("DPS_ACC_ACQU_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Transaction Amount">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("TRX_AMT_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("TRX_AMT_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active Agent">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("ACTIVE_AGENTNO_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" 
                                Text='<%# Bind("ACTIVE_AGENTNO_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="Corporate Collection">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" 
                                Text='<%# Bind("CORP_COLLECTION_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" 
                                Text='<%# Bind("CORP_COLLECTION_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="Lifting ">
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" 
                                Text='<%# Bind("LIFTING_AMOUNT_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LIFTING_AMOUNT_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Agent's Trx Amount">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" 
                                Text='<%# Bind("ACTIVE_AGENT_TRXAMT_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" 
                                Text='<%# Bind("ACTIVE_AGENT_TRXAMT_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Utility Bill Payment">
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" 
                                Text='<%# Bind("UTILITY_AMOUNT_TARGET") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxUtility" runat="server" Text='<%# Bind("LIFTING_AMOUNT_TARGET") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="Remarks">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" HeaderText="Action" />
                </Columns>
                
                <PagerStyle CssClass="pgr" />
            <HeaderStyle ForeColor="White" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView>   
       </td>   
      </tr>
     </table>   
    </fieldset>
    
    </contenttemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
