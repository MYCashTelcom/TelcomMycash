﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerritoryArea.aspx.cs" Inherits="MANAGE_TM_TO_frmTerritoryArea" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Territory Area</title>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel">
                <table style="width: 100%" align="right">
                    <tr>
                        <td align="left" class="style1">
                            <asp:Label runat="server" ID="panelQ" Text="Manage Territory Area"></asp:Label>
                        </td>
                        <td class="style2">
                            <strong>Select Area for Thana List</strong>
                        </td>
                        <td class="style4">
                            <asp:DropDownList runat="server" ID="drpArea" DataTextField="AREA_NAME" DataValueField="AREA_ID"
                                AutoPostBack="True" 
                                onselectedindexchanged="drpArea_SelectedIndexChanged" />
                        </td>
                        
                        <td align="right">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 50px;">
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DynamicLayout="True">
                                <ProgressTemplate>
                                    <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            
            <table>
             <tr>
              <td valign="top">
               
               <fieldset style="width: 450px">
                <legend><strong>Area Wise Thana</strong></legend>
                
                <asp:GridView ID="grvArea" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="GridViewClass" Width="450px" AllowPaging="True" DataKeyNames="THANA_ID" PageSize="15"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                BorderStyle="None" onpageindexchanging="grvArea_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="AREA_ID" HeaderText="Area Id" Visible="False" />
                                    <asp:BoundField DataField="AREA_NAME" HeaderText="Area Name" />
                                    <asp:BoundField DataField="THANA_ID" HeaderText="Thana Id" Visible="False" />
                                    <asp:BoundField DataField="THANA_NAME" HeaderText="Thana Name" />
                                </Columns>
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle ForeColor="White" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
                            
                <fieldset style="width: 440px">
                 <legend><strong>Add/Modify Thana</strong></legend>
                 <table style="width: 440px">
                  <tr>
                   <td>
                    <strong>Select Area</strong>   
                   </td>
                   <td>
                    <asp:DropDownList runat="server" ID="drpAreaM" DataValueField="AREA_ID" DataTextField="AREA_NAME"/>   
                   </td>   
                  </tr>
                  <tr>
                   <td>
                    <strong>Select Thana</strong>
                   </td>
                   <td>
                    <asp:DropDownList runat="server" ID="drpThanaM" DataValueField="THANA_ID" DataTextField="THANA_NAME"/>   
                   </td>   
                  </tr>
                  <tr>
                   <td></td>   
                   <td>
                    <asp:Button runat="server" ID="btnAreaThanaSave" Text="Save" onclick="btnAreaThanaSave_Click"/>   
                   </td>   
                  </tr>
                     
                 </table>
                </fieldset>
               </fieldset>
              </td>
              
              
              
              
              <td valign="top">
               <fieldset style="width: 450px">
                <legend><strong>Add/Modify Area</strong></legend>
                
                <asp:GridView ID="grvEduIns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="GridViewClass" Width="500px" AllowPaging="True" 
                       DataKeyNames="AREA_ID" PageSize="5"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                BorderStyle="None" 
                       onpageindexchanging="grvEduIns_PageIndexChanging" 
                       onrowcancelingedit="grvEduIns_RowCancelingEdit" 
                       onrowdeleting="grvEduIns_RowDeleting" onrowediting="grvEduIns_RowEditing" 
                       onrowupdating="grvEduIns_RowUpdating" >
                                
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Area Id" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("AREA_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--<EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AREA_ID") %>'></asp:TextBox>
                                        </EditItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("AREA_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AREA_NAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                
                                
                                <PagerStyle CssClass="pgr" />
                    <HeaderStyle ForeColor="White" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
                
                
                
                
                
                <fieldset style="width: 440px">
                 <legend><strong>Add Area</strong></legend> 
                 <table style="width: 440px">
                  <tr>
                   <td>
                    <legend><strong>Area Name</strong></legend>   
                   </td> 
                   <td>
                    <asp:TextBox runat="server" ID="txtAreaName"></asp:TextBox>   
                   </td>  
                  </tr>
                  <tr>
                   <td></td>
                   <td>
                       <asp:Button runat="server" ID="btnAddArea" Text="Add Area" 
                           onclick="btnAddArea_Click"/>   
                   </td>   
                  </tr>   
                 </table>  
                </fieldset>
                   
               </fieldset>   
                  
                  
                  
              </td>   
             </tr>   
            </table>
            
            
            
            </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>