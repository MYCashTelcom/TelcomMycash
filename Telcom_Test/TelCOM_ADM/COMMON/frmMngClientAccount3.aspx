<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngClientAccount3.aspx.cs"
    Inherits="Forms_frmMngClientAccount3" Title="Manage Client Account" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />    
    <style type="text/css">
        .style1
        {
            width: 332px;
        }
        .style2
        {
            width: 112px;
        }
        .style3
        {
            width: 125px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scmMsgService" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btnAccountList" runat="server" OnClick="btnAccountList_Click" Text="Channel Account List"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" />
    <asp:Button ID="btnNewAccount" runat="server" OnClick="btnNewAccount_Click" Text="New Channel Account"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" />
    <asp:UpdatePanel ID="udpMngService" runat="server">
        <ContentTemplate>
            <div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div style="background-color: royalblue; text-align: right;">
                            <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">:: Manage Channel Account List&nbsp;
                                ::</span>
                        </div>
                        <div style="padding-top: 5px;"> <%--border: 1px solid #4F4F4F;--%>
                            <div style="margin-left: 15px; padding-left: 15px; padding-top: 10px; padding-right: 15px;
                                padding-bottom: 15px; width: 952px;">
                                <table cellpadding="0" cellspacing="4">
                                    <%--<tr>
                                        <td colspan="9" align="center" 
                                            style="font-size: 11px; font-weight: bold; color: Red" class="style1">
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" class="style3">
                                            <span style="font-size: 14px; font-weight: bold;">Channel Name: </span>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="style2">
                                            <span style="font-size: 14px; font-weight: bold;">Channel Code: </span>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAccCode" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                        </td>
                                        <td align="left" 
                                            style="font-size: 11px; font-weight: bold; color: Red; padding-left:5px;" 
                                            class="style1">
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                    <td>
                                        <span style="font-size: 12px; font-weight: bold;">Channel Code: </span>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                    <%--<tr>
                                    <td colspan="3" align="right">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>--%>                                    
                                </table>
                            </div>
                        </div>
                        <asp:SqlDataSource ID="sdsClientList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT CLINT_ID, CLINT_NAME FROM CLIENT_LIST ORDER BY NLSSORT(TRIM(CLINT_NAME), 'NLS_SORT=generic_m')">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsClientAccount" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT * FROM "ACCOUNT_LIST"'
                            DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN)'
                            UpdateCommand='UPDATE ACCOUNT_LIST SET CLINT_ID = :CLINT_ID, ACCNT_NO = :ACCNT_NO, ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID, ACCNT_MSISDN = :ACCNT_MSISDN WHERE (ACCNT_ID = :ACCNT_ID)'>
                            <DeleteParameters>
                                <asp:Parameter Name="ACCNT_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CLINT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_NO" Type="String" />
                                <asp:Parameter Name="ACCNT_STATE" Type="String" />
                                <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
                                <asp:Parameter Name="ACCNT_ID" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="CLINT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_NO" Type="String" />
                                <asp:Parameter Name="ACCNT_STATE" Type="String" />
                                <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsServicePackage" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE'>
                        </asp:SqlDataSource>
                        <%--runat="server" visible="false" id="dvFrmView"  border: 1px solid #4F4F4F;--%>
                        <div style="margin-left: 15px; padding-right: 15px; padding-top: 15px; width: 460px;
                            padding-bottom: 15px;">
                            <asp:FormView ID="FormView1" runat="server" DataKeyNames="ACCNT_ID" DataSourceID="SqlDataSource1"
                                DefaultMode="Edit" Width="492px" AllowPaging="true" 
                                RowStyle-VerticalAlign="Top" onitemupdated="FormView1_ItemUpdated">
                                <PagerTemplate>
                                    <div style="width: 450px; padding-left: 15px; padding-bottom: 10px; vertical-align: top;">
                                        <span style="font-size: 12px;">Page:
                                            <%#FormView1.PageIndex+1%></span>&nbsp;
                                        <asp:LinkButton ID="btnPrev" runat="server" CommandArgument="Prev" CommandName="Page"
                                            Font-Bold="True" Font-Size="12px">Previous</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                            Font-Size="12px" Font-Bold="True">Next</asp:LinkButton>
                                    </div>
                                </PagerTemplate>
                                <ItemTemplate>
                                    <table border="0" style="font-size: 14px; padding-left: 15px;" width="480px">
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Service Package: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsServicePackage"
                                                    DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" Enabled="False"
                                                    SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Channel Name: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="sdsClientList"
                                                    DataTextField="CLINT_NAME" DataValueField="CLINT_ID" Enabled="False" SelectedValue='<%# Bind("CLINT_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Channel Code: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <%# Eval("ACCNT_NO") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">MSISDN: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <%# Eval("ACCNT_MSISDN")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Account State: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList13" runat="server" Enabled="False" SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                                    <asp:ListItem Value="I">Idle</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                    <asp:ListItem Value="L">Locked</asp:ListItem>
                                                    <asp:ListItem Value="E">Expired</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" Font-Bold="True" Font-Size="14px"></asp:LinkButton>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <PagerSettings Position="Top" />
                                <RowStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <EditItemTemplate>
                                    <table border="0" style="font-size: 14px; padding-left: 15px;" width="480px">
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Service Package: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList Enabled="false" ID="DropDownList6" runat="server" DataSourceID="sdsServicePackage"
                                                    DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Channel Name: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList8" Enabled="false" runat="server" DataSourceID="sdsClientList"
                                                    DataTextField="CLINT_NAME" DataValueField="CLINT_ID" SelectedValue='<%# Bind("CLINT_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Channel Code: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccNo" Text='<%# Bind("ACCNT_NO") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">MSISDN: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccMSISDN" Text='<%# Bind("ACCNT_MSISDN") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Account State: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList14" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                                    <asp:ListItem Value="I">Idle</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                    <asp:ListItem Value="L">Locked</asp:ListItem>
                                                    <asp:ListItem Value="E">Expired</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="UpdateButton" Text="Update" CausesValidation="true" CommandName="Update"
                                                    runat="server" Font-Bold="True" Font-Size="12px" />&nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;<asp:LinkButton ID="CancelUpdateButton" CausesValidation="false" Text="Cancel"
                                                    CommandName="Cancel" runat="server" Font-Bold="True" Font-Size="12px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <PagerStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:FormView>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE ACCOUNT_LIST SET CLINT_ID = :CLINT_ID, ACCNT_NO = :ACCNT_NO, ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID, ACCNT_MSISDN = :ACCNT_MSISDN WHERE (ACCNT_ID = :ACCNT_ID)'>
                            <UpdateParameters>
                                <asp:Parameter Name="CLINT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_NO" Type="String" />
                                <asp:Parameter Name="ACCNT_STATE" Type="String" />
                                <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
                                <asp:Parameter Name="ACCNT_ID" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div style="background-color: royalblue; text-align: right;">
                            <strong><span style="color: white; font-size: 11px;">:: Add New Channel Account ::</span></strong></div>
                        <div style="margin-left: 15px; padding-right: 15px; padding-top: 15px; width: 460px;
                            padding-bottom: 15px; margin-top: 15px;">  <%--border: 1px solid #4F4F4F;--%>
                            <asp:DetailsView ID="DetailsView1" runat="server" DataKeyNames="ACCNT_ID" Font-Size="14px"
                                BorderColor="#E0E0E0" DataSourceID="sdsClientAccount"
                                DefaultMode="Insert" AutoGenerateRows="False" Width="492px" 
                                GridLines="None" Height="173px" oniteminserted="DetailsView1_ItemInserted">
                                <Fields>
                                    <asp:BoundField ReadOnly="True" DataField="ACCNT_ID" Visible="False" SortExpression="ACCNT_ID"
                                        HeaderText="ACCNT_ID">
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField SortExpression="SERVICE_PKG_ID" HeaderText="Service Package: &nbsp;&nbsp;">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:TextBox></EditItemTemplate>
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="DropDownList16" runat="server" DataSourceID="sdsServicePackage"
                                                DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                            </asp:DropDownList>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("SERVICE_PKG_ID") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="CLINT_ID" HeaderText="Channel Name: &nbsp;&nbsp;">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:TextBox></EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="DropDownList17" runat="server" DataSourceID="sdsClientList"
                                                DataTextField="CLINT_NAME" DataValueField="CLINT_ID" SelectedValue='<%# Bind("CLINT_ID") %>'>
                                            </asp:DropDownList>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("CLINT_ID") %>'></asp:Label></ItemTemplate>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Channel Code: &nbsp;&nbsp;" SortExpression="ACCNT_NO">
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ACCNT_MSISDN" SortExpression="ACCNT_MSISDN" HeaderText="MSISDN: &nbsp;&nbsp;">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField SortExpression="ACCNT_STATE" HeaderText="Account State: &nbsp;&nbsp;">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("ACCNT_STATE") %>'></asp:TextBox></EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="DropDownList15" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                                <asp:ListItem Value="I">Idle</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="L">Locked</asp:ListItem>
                                                <asp:ListItem Value="E">Expired</asp:ListItem>
                                            </asp:DropDownList>
                                        </InsertItemTemplate>
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right" Font-Bold="True"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("ACCNT_STATE") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField InsertText="Add Account" ButtonType="Button" ShowInsertButton="True">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:CommandField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;
    </form>
</body>
</html>
