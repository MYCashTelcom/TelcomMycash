<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmManageKPIParameters.aspx.cs" Inherits="MANAGE_TM_TO_frmManageKPIParameters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Define KPI Parameters</title>
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
        .style1
        {
            width: 157px;
        }
        .style2
        {
            width: 173px;
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
           <asp:Label runat="server" ID="panelQ" Text="Define KPI Parameters"></asp:Label> 
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
     <legend><strong>KPI Parameters for Utility Area</strong></legend>   
    
        <asp:GridView ID="grvKpi" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                CssClass="GridViewClass" Width="500px" AllowPaging="True" DataKeyNames="MANAGE_KPI_PARAMETERS_ID"
                  onrowediting="grdKPI_RowEditing" 
                  onrowcancelingedit="grdKPI_RowCancelingEdit" 
                  onrowupdating="grdKPI_RowUpdating"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                HeaderStyle-ForeColor="White" BorderStyle="None" >
                
                <Columns>
                   <%-- <asp:BoundField  DataField="MANAGE_KPI_PARAMETERS_ID" HeaderText="KPI Id" Visible="False"/>--%>
                     <asp:TemplateField HeaderText="Kpi Id" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelId" runat="server" 
                                        Text='<%# Bind("MANAGE_KPI_PARAMETERS_ID") %>'></asp:Label>
                                </ItemTemplate>                           
                            </asp:TemplateField>


                  <%--  <asp:BoundField DataField="PARAMETER_NAME" HeaderText="KPI Parameter Name" />--%>
                     <asp:TemplateField HeaderText="Parameter Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PARAMETER_NAME") %>'></asp:Label>
                                </ItemTemplate>                             
                            </asp:TemplateField>

                   <%-- <asp:BoundField  DataField="BENCHMARK" HeaderText="Benchmark" />--%>

                     <asp:TemplateField HeaderText="Benchmark">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("BENCHMARK") %>'></asp:Label>
                                </ItemTemplate> 
                           <EditItemTemplate>
                                    <asp:TextBox ID="TextBox" Width="60px" runat="server" Text='<%# Bind("BENCHMARK") %>' ></asp:TextBox>
                                </EditItemTemplate>                            
                            </asp:TemplateField>

                      <asp:CommandField ShowEditButton="True" HeaderText="Action"/>
                </Columns>


                
                <PagerStyle CssClass="pgr" />
            <HeaderStyle ForeColor="White" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView> 
    
    </fieldset>

         <fieldset style="width: 500px">
     <legend><strong>KPI Parameters for Non-Utility Area</strong></legend>   
    
        <asp:GridView ID="grvKpiNu" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                CssClass="GridViewClass" Width="500px" AllowPaging="True" DataKeyNames="MANAGE_KPI_PARAMETERS_ID"
                  onrowediting="grdKPI_RowEditing2" 
                  onrowcancelingedit="grdKPI_RowCancelingEdit2" 
                  onrowupdating="grdKPI_RowUpdating2"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                HeaderStyle-ForeColor="White" BorderStyle="None" >
                
                <Columns>
                   <%-- <asp:BoundField  DataField="MANAGE_KPI_PARAMETERS_ID" HeaderText="KPI Id" Visible="False"/>--%>
                     <asp:TemplateField HeaderText="Kpi Id" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelId1" runat="server" 
                                        Text='<%# Bind("MANAGE_KPI_PARAMETERS_ID") %>'></asp:Label>
                                </ItemTemplate>                           
                            </asp:TemplateField>


                  <%--  <asp:BoundField DataField="PARAMETER_NAME" HeaderText="KPI Parameter Name" />--%>
                     <asp:TemplateField HeaderText="Parameter Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("PARAMETER_NAME") %>'></asp:Label>
                                </ItemTemplate>                             
                            </asp:TemplateField>

                   <%-- <asp:BoundField  DataField="BENCHMARK" HeaderText="Benchmark" />--%>

                     <asp:TemplateField HeaderText="Benchmark">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("BENCHMARK") %>'></asp:Label>
                                </ItemTemplate> 
                           <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" Width="60px" runat="server" Text='<%# Bind("BENCHMARK") %>' ></asp:TextBox>
                                </EditItemTemplate>                            
                            </asp:TemplateField>

                      <asp:CommandField ShowEditButton="True" HeaderText="Action"/>
                </Columns>


                
                <PagerStyle CssClass="pgr" />
            <HeaderStyle ForeColor="White" />
            <AlternatingRowStyle CssClass="alt" />
        </asp:GridView> 
    
    </fieldset>
    
         
    
    
    </contenttemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
