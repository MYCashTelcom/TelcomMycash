<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPartexBranchInfo.aspx.cs" Inherits="COMMON_frmPartexBranchInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Branch List</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Top_Panel
        {
            background-color: royalblue;
            height: 25px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            background-color: powderblue;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
        .style1
        {
            width: 166px;
        }
        </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form2" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
                <table width="100%">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Text="Add Branch"></asp:Label>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                <ProgressTemplate>
                                    <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlInsert" runat="server">
                <table>

                     <tr>
                        <td>
                           Office Code
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchCode" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width:100px;">
                            Office Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchName" runat="server" Width="225px" Height="22px" 
                                 AutoPostBack="True"></asp:TextBox>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                                            Text=""></asp:Label>
                        </td>
                    </tr>
                     
                    <tr>
                        <td>
                            Office Address
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>
                    
                    

                    <tr>
                        <td style="width:100px;">
                            Incharge Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmployeeName" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width:100px;">
                            Incharge Mobile
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmployeeMobile" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>

                    <%--<tr>
                        <td style="width:100px;">
                            Lead Retail Sales Mobile
                        </td>
                        <td>
                            <asp:TextBox ID="txtLeadsMobile" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>
                    

                     <tr>
                        <td style="width:100px;">
                            Business Controller's Mobile
                        </td>
                        <td>
                            <asp:TextBox ID="txtConMobile" runat="server" Width="225px" Height="22px"></asp:TextBox>
                        </td>
                    </tr>--%>

                    <tr>
                        <td style="width:100px;">
                            Branch Type
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlBrType" runat="server"> 
                            <asp:ListItem>FURNITURE</asp:ListItem>  
                            <asp:ListItem>CABLES</asp:ListItem>
                        </asp:DropDownList> 
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                                OnClick="btnSave_Click" Height="26px" />
                        </td>    
                    </tr>
                </table>
            </asp:Panel>
            <br />
            
            <asp:Panel ID="Panel4" runat="server" CssClass="Top_Panel">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Branch List"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlSearch" runat="server">
                <table>
                    <tr>
                        <td>Search By Branch Name : </td>
                        <td><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                        <td><asp:Button ID="btnSearch" runat="server" Text="Search" 
                                onclick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlGridView" runat="server">
                <asp:GridView ID="gdvBranchList" runat="server" AutoGenerateColumns="False" DataKeyNames="PARTEX_BRANCH_INFO_ID"
                    CssClass="mGrid" PageSize="10" AllowPaging="true" Width="80%" 
                    EmptyDataText="No data found....." 
                    onpageindexchanging="gdvBranchList_PageIndexChanging" 
                    onrowcancelingedit="gdvBranchList_RowCancelingEdit" 
                    onrowdeleting="gdvBranchList_RowDeleting" 
                    onrowediting="gdvBranchList_RowEditing" 
                    onselectedindexchanging="gdvBranchList_SelectedIndexChanging" 
                    onrowupdating="gdvBranchList_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Branch Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchId" Visible="false" runat="server" Text='<%#Bind("PARTEX_BRANCH_INFO_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Office Name" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" runat="server" Text='<%#Bind("BR_NAME") %>'></asp:Label>
                            </ItemTemplate>
                              <EditItemTemplate>
                                <asp:TextBox ID="gvtxtBranchName" runat="server" Text='<%#Bind("BR_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Branch Type" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchType" runat="server" Text='<%#Bind("CMP_BRANCH_TYPE_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Office Address" ControlStyle-Width="200">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchAddress" runat="server" Text='<%#Bind("BR_ADDRESS") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                                <asp:TextBox ID="gvtxtBranchAddress" runat="server" Text='<%#Bind("BR_ADDRESS") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Office Code" ControlStyle-Width="75">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchCode" runat="server" Text='<%#Bind("BR_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            
                              <%--<EditItemTemplate>
                                <asp:TextBox ID="gvtxtBranchCode" runat="server" Text='<%#Bind("BR_CODE") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                        </asp:TemplateField>
                        
                         

                        <asp:TemplateField HeaderText="Incharge Name" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Bind("INCHARGE_NAME") %>'></asp:Label>
                            </ItemTemplate>
                              <EditItemTemplate>
                                <asp:TextBox ID="gvtxtEmployeeName" runat="server" Text='<%#Bind("INCHARGE_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Incharge Mobile No" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeMobile" runat="server" Text='<%#Bind("INCHARGE_MOBILE") %>'></asp:Label>
                            </ItemTemplate>
                              <EditItemTemplate>
                                <asp:TextBox ID="gvtxtEmployeeMobile" runat="server" Text='<%#Bind("INCHARGE_MOBILE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                       <%--  <asp:TemplateField HeaderText="Designation" ControlStyle-Width="150" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Bind("DESIGNATION") %>'></asp:Label>
                            </ItemTemplate>
                              <EditItemTemplate>
                                <asp:TextBox ID="gvtxtDesignation" runat="server" Text='<%#Bind("DESIGNATION") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Wallet ID" ControlStyle-Width="150" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblWallet" Visible="true" runat="server" Text='<%#Bind("WALLET_ID") %>'></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Lead's Mobile No" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblLeadsMobile" runat="server" Text='<%#Bind("LEAD_RETAIL_SALES_MOBILE") %>'></asp:Label>
                            </ItemTemplate>
                              <%--<EditItemTemplate>
                                <asp:TextBox ID="gvtxtlblLeadsMobile" runat="server" Text='<%#Bind("LEAD_RETAIL_SALES_MOBILE") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Controller's Mobile No" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblConMobile" runat="server" Text='<%#Bind("BUSINESS_CONTROLLER_MOBILE") %>'></asp:Label>
                            </ItemTemplate>
                              <%--<EditItemTemplate>
                                <asp:TextBox ID="gvtxtlblConMobile" runat="server" Text='<%#Bind("BUSINESS_CONTROLLER_MOBILE") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" ControlStyle-Width="75">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("BR_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                              <EditItemTemplate>
                                <asp:TextBox ID="txtStatus" runat="server" Text='<%#Bind("BR_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Branch Type" ControlStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchType" runat="server" Text='<%#Bind("BR_TYPE") %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="" ControlStyle-Width="100">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="update" Text="Update"></asp:LinkButton> <br /> 
                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>    
                    </Columns>
                    <HeaderStyle BackColor="Gray" ForeColor="White" />
                    <EditRowStyle BorderStyle="Outset" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
