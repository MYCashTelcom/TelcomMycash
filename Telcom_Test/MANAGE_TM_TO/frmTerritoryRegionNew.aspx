<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerritoryRegionNew.aspx.cs" Inherits="MANAGE_TM_TO_frmTerritoryRegionNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    
    <head id="Head1" runat="server">
    <title>Manage Territory Region</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .Font_Color
        {
            color: White;
        }
        .Top_Panel
        {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            width: 100%;
            background-color: powderblue;
        }
        .GridViewClass
        {
            width: 100%;
            background-color: #fff;
            margin: 1px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            text-align: left;
        }
        .GridViewClass td
        {
            padding: 2px;
            border: solid 1px #c1c1c1;
            color: #717171;
            font-size: 11px;
        }
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
            width: 170px;
        }
    </style>
    
    <style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url( "../Images/InitialImage.jpg" ) no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url( "../Images/SelectedButton.jpg" ) no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url( "../Images/SelectedButton.jpg" ) no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
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
                            <asp:Label runat="server" ID="panelQ" Text="Manage Territory Region"></asp:Label>
                        </td>
                        <td class="style2">
                        </td>
                        <td class="style4">
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
            
            
            
            <table width="80%" align="left">
      <tr>
        <td>
          <asp:Button Text="Add/Modify Region" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="Region wise Area List" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <asp:Button Text="Add Area with Region " BorderStyle="None" ID="Tab3" 
                CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />
          <asp:Button Text="Modify Area with Region" BorderStyle="None" ID="Tab4" 
                CssClass="Initial" runat="server"
              OnClick="Tab4_Click" />
          <asp:MultiView ID="MainView" runat="server">
            
            
            <asp:View ID="View1" runat="server">
              <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">--%>
              <table>
                <tr>
                  <td>
                    <%--<h3>
                      <span>View 1 </span>
                    </h3>--%>
                    
                    <fieldset style="width: 450px">
                            <asp:GridView ID="grdRegionList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="GridViewClass" Width="450px" AllowPaging="True" DataKeyNames="REGION_ID"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                BorderStyle="None" OnPageIndexChanging="grdRegionList_PageIndexChanging" OnRowCancelingEdit="grdRegionList_RowCancelingEdit"
                                OnRowDeleting="grdRegionList_RowDeleting" OnRowEditing="grdRegionList_RowEditing"
                                OnRowUpdating="grdRegionList_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Region Id" Visible="False">
                                        <%--<EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REGION_ID") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("REGION_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("REGION_NAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("REGION_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle ForeColor="White" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                            <fieldset style="width: 445px">
                                <legend><strong>Add Region</strong></legend>
                                <table style="width: 440px">
                                    <tr>
                                        <td>
                                            <strong>Region Name</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtRegionName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnAdd" Text="Add Region" OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </fieldset>
                    
                    
                    
                  </td>
                </tr>
              </table>
            </asp:View>
            
            
            
            
            <asp:View ID="View2" runat="server">
              <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">--%>
              <table>
                <tr>
                  <td>
                    <%--<h3>
                      View 2
                    </h3>--%>
                    
                    <table style="width: 550px">
                      <tr>
                        <td>
                            <strong>Select Region </strong>
                        </td>  
                        <td>
                            <asp:DropDownList runat="server" ID="drpRegion" DataTextField="REGION_NAME" DataValueField="REGION_ID"
                                OnSelectedIndexChanged="drpRegion_SelectedIndexChanged" AutoPostBack="True" />
                        </td>  
                      </tr>
                      <tr>
                        <td></td>  
                        <td>
                            <asp:GridView ID="grvRegion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="GridViewClass" Width="400px" AllowPaging="True" DataKeyNames="REGION_ID" PageSize="10"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                BorderStyle="None" onpageindexchanging="grvRegion_PageIndexChanging" >
                                <Columns>
                                    <asp:BoundField DataField="REGION_ID" HeaderText="Region Id" Visible="False" />
                                    <asp:BoundField DataField="REGION_NAME" HeaderText="Region Name" />
                                    <asp:BoundField DataField="AREA_ID" HeaderText="Area Id" Visible="False" />
                                    <asp:BoundField DataField="AREA_NAME" HeaderText="Area Name" />
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle ForeColor="White" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                            
                        </td>  
                      </tr>  
                    </table>
                    
                  </td>
                </tr>
              </table>
            </asp:View>
            
            
            
            <asp:View ID="View3" runat="server">
              <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">--%>
              <table>
                <tr>
                  <td>
                    <%--<h3>
                      View 3
                    </h3>--%>
                        <fieldset style="width: 490px">
                                <legend></legend>
                                <table style="width: 490px">
                                    <tr>
                                        <td>
                                            <strong>Select Region</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpRegTagAdd" DataTextField="REGION_NAME" DataValueField="REGION_ID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Select Area</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpAreaTagAdd" DataTextField="AREA_NAME" DataValueField="AREA_ID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnTerrRegiAreaTagg" Text="Save" OnClick="btnTerrRegiAreaTagg_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                    
                    
                  </td>
                </tr>
              </table>
            </asp:View>
            
            
            
            
            <asp:View ID="View4" runat="server">
              <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">--%>
              <table>
                <tr>
                  <td>
                    <%--<h3>
                      View 4
                    </h3>--%>
                    
                    <fieldset style="width: 490px">
                                <legend></legend>
                                <table style="width: 490px">
                                    <tr>
                                        <td>
                                            <strong>Select Region</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpRegionM" DataTextField="REGION_NAME" DataValueField="REGION_ID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Select Area</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="drpAreaM" DataTextField="AREA_NAME" DataValueField="AREA_ID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnRegionAreaSave" Text="Save" OnClick="btnRegionAreaSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                    
                  </td>
                </tr>
              </table>
            </asp:View>
            
          </asp:MultiView>
        </td>
      </tr>
    </table>
            
            
            
            </ContentTemplate>
            </asp:UpdatePanel>
    
    
    
    </form>
</body>
</html>
