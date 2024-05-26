<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerritoryRegion.aspx.cs"
    Inherits="MANAGE_TM_TO_frmTerritoryRegion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                            <strong>Select Region for Area List</strong>
                        </td>
                        <td class="style4">
                            <asp:DropDownList runat="server" ID="drpRegion" DataTextField="REGION_NAME" DataValueField="REGION_ID"
                                OnSelectedIndexChanged="drpRegion_SelectedIndexChanged" AutoPostBack="True" />
                        </td>
                        <%--<td class="style5">
                          <strong>Area</strong>   
                         </td>
                         <td>
                          <asp:DropDownList runat="server" ID="drpArea" DataTextField="AREA_NAME" DataValueField="AREA_ID"/>   
                         </td>--%>
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
                <tr valign="top">
                    <td>
                        <fieldset style="width: 500px">
                            <legend><strong>Region Wise Area</strong></legend>
                            <asp:GridView ID="grvRegion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="GridViewClass" Width="500px" AllowPaging="True" DataKeyNames="REGION_ID" PageSize="10"
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
                            <fieldset style="width: 490px">
                                <legend><strong>Add/Modify Area</strong></legend>
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
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width: 450px">
                            <legend><strong>Add/Modify Region</strong></legend>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
    </div>
    </form>
</body>
</html>
