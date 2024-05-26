<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTst.aspx.cs"
    Inherits="COMMON_frmTst" %>

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
                            <asp:Label runat="server" ID="panelQ" Text="Manage Name"></asp:Label>
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
                        <asp:Button Text="Add/Modify Name" BorderStyle="None" ID="Tab1" CssClass="Initial"
                            runat="server" OnClick="Tab1_Click" />
                        <asp:MultiView ID="MainView" runat="server">
                            
                            <asp:View ID="View1" runat="server">
                              
                                <table>
                                    <tr>
                                        <td>
                                            <h3>
                                              
                      <br/>
                                                <fieldset style="width: 450px">
                                                    <legend></legend>
                                                    <asp:GridView ID="grvEduIns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CssClass="GridViewClass" Width="500px" AllowPaging="True" DataKeyNames="TEST_EDU_INS_PK_ID"
                                                        PageSize="5" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                                        BorderStyle="None" OnPageIndexChanging="grvEduIns_PageIndexChanging" OnRowCancelingEdit="grvEduIns_RowCancelingEdit"
                                                        OnRowEditing="grvEduIns_RowEditing"
                                                        OnRowUpdating="grvEduIns_RowUpdating">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="TEST_EDU_INS_PK_ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TEST_EDU_INS_PK_ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                              
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TEST_EDU_INS_NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TEST_EDU_INS_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TEST_EDU_INS_NAME") %>'></asp:TextBox>
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
                                                        <legend><strong>Add Name</strong></legend>
                                                        <table style="width: 440px">
                                                            <tr>
                                                                <td>
                                                                    <legend><strong>TEST_EDU_INS_NAME</strong></legend>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnAddName" Text="Add Name" OnClick="btnAddName_Click" />
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
                            
                           
                            
                        </asp:MultiView>
                    </td
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
