<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngClientAccount2.aspx.cs"
    Inherits="Forms_frmMngClientAccount2" Title="Manage Client Account" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scmMsgService" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btnAccountList" runat="server" OnClick="btnAccountList_Click" Text="ERS Account List"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" />
    <asp:Button ID="btnNewAccount" runat="server" OnClick="btnNewAccount_Click" Text="New ERS Account"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" Visible="false"  />
    <%--<asp:UpdatePanel ID="udpMngService" runat="server">
        <ContentTemplate>--%>
    <div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="background-color: royalblue; text-align: left;">
                            <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">:: Manage ERS Account&nbsp;
                                ::</span>
                        </div>
                        <div style="padding-left: 5px;">
                            <table cellpadding="0" cellspacing="4">
                                <tr>
                                    <td align="right">
                                        <span style="font-size: 12px; font-weight: bold;">Wallet Name: </span>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-size: 12px; font-weight: bold;">Wallet ID: </span>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAccCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-size: 12px; font-weight: bold;">Mobile Number: </span>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMSISDN" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center" style="font-size: 11px; font-weight: bold; color: Red">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:SqlDataSource ID="sdsClientList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT CL.CLINT_NAME || ' ['|| CL.CLINT_ID || ' ]' CLIENT_NAME, CL.CLINT_ID
FROM CLIENT_LIST CL WHERE CL.CLINT_ID NOT IN 
(SELECT AL.CLINT_ID FROM ACCOUNT_LIST AL)
ORDER BY TRIM(CL.CLINT_NAME)">
                        </asp:SqlDataSource>
                        <%--SELECT "CLINT_ID", "CLINT_NAME" FROM "CLIENT_LIST"  order By TRIM(CLINT_NAME) ASC--%>
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
                        <asp:SqlDataSource ID="sdsClientAccount2" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                            DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' 
                            InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN)'
                            
                            UpdateCommand='UPDATE ACCOUNT_LIST SET ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID ,ACCNT_RANK_ID = :ACCNT_RANK_ID WHERE (ACCNT_ID = :ACCNT_ID)'>
                            <%--CLINT_ID = :CLINT_ID, ACCNT_NO = :ACCNT_NO, ACCNT_MSISDN = :ACCNT_MSISDN--%>
                            <%--SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME, A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID AND rownum<2--%>
                            <DeleteParameters>
                                <asp:Parameter Name="ACCNT_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <%--<asp:Parameter Name="CLINT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_NO" Type="String" />--%>
                                <asp:Parameter Name="ACCNT_STATE" Type="String" />
                                <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                                <%--<asp:Parameter Name="ACCNT_MSISDN" Type="String" />--%>
                                <asp:Parameter Name="ACCNT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
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
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            
                            SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE'>
                        </asp:SqlDataSource>
                        
                        <asp:SqlDataSource ID="sdsAccntRank" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT RANK_TITEL,ACCNT_RANK_ID FROM ACCOUNT_RANK">
                        </asp:SqlDataSource>
                        
                        <asp:GridView ID="gdvSearch" Visible="False" runat="server" DataSourceID="sdsClientAccount2"
                            DataKeyNames="ACCNT_ID" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                            BorderColor="#E0E0E0" PageSize="7" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="gdvSearch_RowCancelingEdit" 
                            OnRowEditing="gdvSearch_RowEditing" OnRowUpdated="gdvSearch_RowUpdated" 
                            OnPageIndexChanging="gdvSearch_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ACCNT_ID" HeaderText="ACCNT_ID" SortExpression="ACCNT_ID"
                                    ReadOnly="True" Visible="False" />
                                <asp:TemplateField HeaderText="Service Package" SortExpression="SERVICE_PKG_ID">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEIPackage" runat="server" DataSourceID="sdsServicePackage"
                                            DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList Enabled="false" ID="ddlEIPackage" runat="server" DataSourceID="sdsServicePackage"
                                            DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wallet Name" SortExpression="CLINT_NAME">
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CLINT_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wallet ID" SortExpression="ACCNT_NO">
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ACCNT_NO") %>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ACCNT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                
                                <asp:TemplateField HeaderText="Mobile Number" SortExpression="ACCNT_MSISDN">
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ACCNT_MSISDN") %>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ACCNT_MSISDN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Account Rank" SortExpression="ACCNT_RANK_ID">
                                       <EditItemTemplate>
                                           <asp:DropDownList ID="ddlAccntrankID" runat="server" 
                                               DataSourceID="sdsAccntRank" DataTextField="RANK_TITEL" 
                                               DataValueField="ACCNT_RANK_ID" SelectedValue='<%# Bind("ACCNT_RANK_ID") %>'>
                                           </asp:DropDownList>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:DropDownList ID="ddlAccntRankName" runat="server" 
                                               DataSourceID="sdsAccntRank" DataTextField="RANK_TITEL" 
                                               DataValueField="ACCNT_RANK_ID" Enabled="False" 
                                               SelectedValue='<%# Bind("ACCNT_RANK_ID") %>'>
                                           </asp:DropDownList>
                                       </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="State" SortExpression="ACCNT_STATE" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                            <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                            <asp:ListItem Value="I">Idle</asp:ListItem>
                                            <asp:ListItem Value="L">Locked</asp:ListItem>
                                            <asp:ListItem Value="E">Expaired</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList Enabled="false" ID="DropDownList8" runat="server" SelectedValue='<%# Bind("ACCNT_STATE") %>'>
                                            <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                            <asp:ListItem Value="I">Idle</asp:ListItem>
                                            <asp:ListItem Value="L">Locked</asp:ListItem>
                                            <asp:ListItem Value="E">Expaired</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle ForeColor="White" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                     
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="View2"  runat="server" >
                <div style="background-color: royalblue; text-align: left;">
                    <strong><span style="color: white; font-size: 11px;">:: Add ERS Account ::</span></strong></div>
             
                &nbsp;<asp:Label ID="lblCheck" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <table>
                    <tr>
                        <td>
                            <span style="font-size: 12px; font-weight: bold;">Service Package:&nbsp; </span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList19" runat="server" DataSourceID="sdsServicePackage"
                                DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID">
                            </asp:DropDownList> <%--OnSelectedIndexChanged="DropDownList19_SelectedIndexChanged" AutoPostBack="True"--%> 
                            <%-- --%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span style="font-size: 12px; font-weight: bold;">Wallet Name: </span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList20" runat="server" AutoPostBack="True" DataSourceID="sdsClientList"
                                DataTextField="CLIENT_NAME" DataValueField="CLINT_ID" OnSelectedIndexChanged="DropDownList20_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span style="font-size: 12px; font-weight: bold;">Wallet ID: </span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="DropDownList19" EventName="SelectedIndexChanged" />--%>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList20" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span style="font-size: 12px; font-weight: bold;">&nbsp;Mobile Number: </span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox3" MaxLength="14" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="DropDownList19" EventName="SelectedIndexChanged" />--%>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList20" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span style="font-size: 12px; font-weight: bold;">Account Status: </span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList18" runat="server">
                                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="I">Idle</asp:ListItem>
                                <asp:ListItem Value="L">Locked</asp:ListItem>
                                <asp:ListItem Value="E">Expaired</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Btn_save" runat="server" OnClick="Btn_save_Click" Text="Save" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <%--<asp:Parameter Name="CLINT_ID" Type="String" />
                                <asp:Parameter Name="ACCNT_NO" Type="String" />--%>
    </form>
</body>
</html>
