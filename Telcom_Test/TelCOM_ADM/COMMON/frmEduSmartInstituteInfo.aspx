<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEduSmartInstituteInfo.aspx.cs"
    Inherits="COMMON_frmEduSmartInstituteInfo" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edu Smart Institute Info</title>
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
            background: url(grd_head1.png) activecaption repeat-x 50% top;
            border-left: solid 0px #525252;
            font-size: 11px;
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
                        <td align="left">
                            <asp:Label runat="server" ID="panelQ" Text="Institute and Payment Information"></asp:Label>
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
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnWithPurposeFees" runat="server" ForeColor="Black" Text="Add Institute" Width="120px"
                            CausesValidation="False" class="tabbutton" OnClick="btnWithPurposeFees_Click" />

                        <asp:Button ID="btnWithoutPurposeFees" runat="server" ForeColor="Black" Text="Add Amount" Width="120px" 
							CausesValidation="False" class="tabbutton" OnClick="btnWithoutPurposeFees_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="grvEduIns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="GridViewClass" AllowPaging="True" DataKeyNames="EDU_INS_PK_ID"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                    BorderStyle="None" OnPageIndexChanging="grvEduIns_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="EDU_INS_PK_ID" HeaderText="Pk_Id" Visible="False" />
                                        <asp:BoundField DataField="EDU_INS_NAME" HeaderText="Institute Name" />
                                        <asp:BoundField DataField="EDU_INST_REF_ID" HeaderText="Ref Id" />
                                        <asp:BoundField DataField="ACCOUNT_NO" HeaderText="Account No" />
                                        <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                        <asp:BoundField DataField="OWNER_CODE" HeaderText="Owner Code" />
                                        <asp:BoundField DataField="BDMIT_INTERNAL_INS_CODE" HeaderText="BDMIT Code" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle ForeColor="White" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                                <fieldset style="width: 450px; padding: 10px;">
                                    <legend><strong>Add Institute Information</strong></legend>
                                    <table style="width: 450px;">
                                        <tr>
                                            <td>
                                                <strong>Institute Name</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtName" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Institute Reference Id</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtId"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtId"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Institute Merchant Account No</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtWallet"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtWallet"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Owner Code</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOwnerCode" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOwnerCode"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                <strong>BDMIT Internal Code</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBdMitInternalCode" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtBdMitInternalCode"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                       <asp:Label ID="lblCodeLength" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnAddInstitute" Text="  Save  " 
                                                    onclick="btnAddInstitute_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                
                                <asp:GridView ID="gvInstituteAmount" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="GridViewClass" AllowPaging="True" DataKeyNames="EDU_INS_AMT_ID"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="White"
                                    BorderStyle="None" 
                                    OnPageIndexChanging="gvInstituteAmount_OnPageIndexChanging" 
                                    onrowcancelingedit="gvInstituteAmount_RowCancelingEdit" 
                                    onrowediting="gvInstituteAmount_RowEditing" 
                                    onrowupdating="gvInstituteAmount_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pk_Id" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPkId" runat="server" Text='<% #BIND("EDU_INS_AMT_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Institute Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstituteName" runat="server" Text='<% #BIND("EDU_INS_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPurposeName" runat="server" Text='<% #BIND("PURPOSE_CODE_NAME") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurposeName" runat="server" Text='<% #BIND("PURPOSE_CODE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPurposeCode" runat="server" Text='<% #BIND("PURPOSE_CODE") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurposeCode" runat="server" Text='<% #BIND("PURPOSE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" Text='<% #BIND("DESCRIPTION") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<% #BIND("DESCRIPTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Text='<% #BIND("AMOUNT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<% #BIND("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                                                <%--<asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CausesValidation="true"
                                                OnClientClick="return confirm('Are you sure to Delete?')" />--%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CausesValidation="false" OnClientClick="return confirm('Are you sure to Update?')"/>
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle ForeColor="White" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                                
                                <fieldset style="width: 450px; padding: 10px;">
                                    <legend><strong>Add Purpose Code with Amount</strong></legend>
                                    <table style="width: 450px;">
                                        
                                        <tr>
                                            <td>
                                                <strong>Institute Name</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInstituteName" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Purpose Name</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPurposeName" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPurposeCode"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Purpose Code</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPurposeCode" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPurposeCode"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Description</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtDescription"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Amount</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPurposeAmount" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPurposeAmount"
                                                    runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button runat="server" ID="btnSaveAmount" Text="  Save  " 
                                                    onclick="btnSaveAmount_Click"/>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </fieldset>
                                
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
