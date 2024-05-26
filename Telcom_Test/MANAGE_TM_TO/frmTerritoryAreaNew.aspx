<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerritoryAreaNew.aspx.cs"
    Inherits="MANAGE_TM_TO_frmTerritoryAreaNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Territory Area</title>
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
            width: 173px;
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
        .style3
        {
            width: 84px;
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
            <table width="80%" align="center">
                <tr>
                    <td>
                        <asp:Button Text="Add/Modify Area" BorderStyle="None" ID="Tab1" CssClass="Initial"
                            runat="server" OnClick="Tab1_Click" />
                        <asp:Button Text=" Area wise Thana List" BorderStyle="None" ID="Tab2" CssClass="Initial"
                            runat="server" OnClick="Tab2_Click" />
                        <asp:Button Text="Add Thana with Territory Area" BorderStyle="None" ID="Tab3" CssClass="Initial"
                            runat="server" OnClick="Tab3_Click" />
                        <asp:Button Text="Modify Thana with Territory Area" BorderStyle="None" ID="Tab4"
                            CssClass="Initial" runat="server" OnClick="Tab4_Click" />
                        <asp:MultiView ID="MainView" runat="server">
                            
                            <asp:View ID="View1" runat="server">
                                <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">--%>
                                <table>
                                    <tr>
                                        <td>
                                            <h3>
                                                <%--<span>View 1 </span>
                      <br/>--%>
                                                <fieldset style="width: 450px">
                                                    <legend></legend>
                                                    <asp:GridView ID="grvEduIns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CssClass="GridViewClass" Width="500px" AllowPaging="True" DataKeyNames="AREA_ID"
                                                        PageSize="5" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                                        BorderStyle="None" OnPageIndexChanging="grvEduIns_PageIndexChanging" OnRowCancelingEdit="grvEduIns_RowCancelingEdit"
                                                        OnRowDeleting="grvEduIns_RowDeleting" OnRowEditing="grvEduIns_RowEditing"
                                                        OnRowUpdating="grvEduIns_RowUpdating">
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
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnAddArea" Text="Add Area" OnClick="btnAddArea_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </fieldset>
                                            </h3>
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
                                            <fieldset style="width: 550px">
                                                <table style="width: 550px">
                                                    <tr>
                                                        <td class="style3" style="width: 100px">
                                                            <strong>Select Area </strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="drpArea" DataTextField="AREA_NAME" DataValueField="AREA_ID"
                                                                AutoPostBack="True" OnSelectedIndexChanged="drpArea_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" style="width: 100px">
                                                        </td>
                                                        <td>
                                                            <asp:GridView ID="grvArea" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                CssClass="GridViewClass" Width="400px" AllowPaging="True" DataKeyNames="THANA_ID"
                                                                PageSize="15" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                                                BorderStyle="None" OnPageIndexChanging="grvArea_PageIndexChanging">
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
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
                                            <fieldset style="width: 550px">
                                                <legend></legend>
                                                <table style="width: 550px">
                                                    <tr>
                                                        <td>
                                                            <strong>Select Area</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="drpAddTerrArea" DataValueField="AREA_ID" DataTextField="AREA_NAME" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Select Thana</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="drpAddThanawt" DataValueField="THANA_ID" DataTextField="THANA_NAME" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnAddThwtArea" Text="Save" OnClick="btnAddThwtArea_Click" />
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
                                            <fieldset style="width: 440px">
                                                <legend></legend>
                                                <table style="width: 440px">
                                                    <tr>
                                                        <td>
                                                            <strong>Select Area</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="drpAreaM" DataValueField="AREA_ID" DataTextField="AREA_NAME" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Select Thana</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="drpThanaM" DataValueField="THANA_ID" DataTextField="THANA_NAME" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnAreaThanaSave" Text="Save" OnClick="btnAreaThanaSave_Click" />
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
