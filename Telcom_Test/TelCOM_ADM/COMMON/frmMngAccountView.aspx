<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngAccountView.aspx.cs"
    Inherits="Forms_frmMngAccountView" Title="Manage Client Account" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
     <style type="text/css">
       .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         }
         .View_Panel
         {
         	background-color:powderblue;
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
   <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    
    <asp:Button ID="btnAccountList" runat="server" OnClick="btnAccountList_Click" Text="ERS Account List"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" Visible="False" />
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
         <asp:SqlDataSource ID="sdsClientList" runat="server"
              ConnectionString="<%$ ConnectionStrings:oracleConString %>"
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              SelectCommand="SELECT CL.CLINT_NAME || ' ['|| CL.CLINT_ID || ' ]' CLIENT_NAME, CL.CLINT_ID
                 FROM CLIENT_LIST CL WHERE CL.CLINT_ID NOT IN (SELECT AL.CLINT_ID FROM ACCOUNT_LIST AL)
                        ORDER BY TRIM(CL.CLINT_NAME)">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsClientAccount" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand='SELECT * FROM "ACCOUNT_LIST"'
            DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' 
            
            InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN)'
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
            InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN,STATE_NOTE) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN,:STATE_NOTE)'
            
            UpdateCommand='UPDATE ACCOUNT_LIST SET ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID ,ACCNT_RANK_ID = :ACCNT_RANK_ID,STATE_NOTE=:STATE_NOTE WHERE (ACCNT_ID = :ACCNT_ID)'>
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
                <asp:Parameter Name="STATE_NOTE" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="CLINT_ID" Type="String" />
                <asp:Parameter Name="ACCNT_NO" Type="String" />
                <asp:Parameter Name="ACCNT_STATE" Type="String" />
                <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
                <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
                <asp:Parameter Name="STATE_NOTE" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsServicePackage" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"                             
            SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE'>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsAccntRank" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"                             
            
               SelectCommand="SELECT RANK_TITEL,ACCNT_RANK_ID FROM ACCOUNT_RANK WHERE SHORT_CODE IN ('NA','D','SD','SA','A','C','MCPAY') ORDER BY ACCNT_RANK_ID">
        </asp:SqlDataSource>
        <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
           <table style="color:White; font-weight:bold; font-size:12px;" width="100%">
            <tr>
             <td >
                Manage Account
             </td>
             <td>
             </td>
             <td>
               <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
             </td>
             <td>
              <asp:UpdateProgress ID="UpdateProgress3" runat="server">
               <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
               </ProgressTemplate>
              </asp:UpdateProgress>
             </td>
            </tr>
           </table> 
        </asp:Panel> 
        <asp:Panel ID="Panel1" runat="server">
          <table cellpadding="0" cellspacing="4" class="View_Panel">
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
          </table>
        </asp:Panel>    
        <asp:GridView ID="gdvSearch" Visible="False" runat="server" DataSourceID="sdsClientAccount2"
            DataKeyNames="ACCNT_ID" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
            BorderColor="#E0E0E0" PageSize="7" CssClass="mGrid" PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="gdvSearch_RowCancelingEdit" 
            OnRowEditing="gdvSearch_RowEditing" OnRowUpdated="gdvSearch_RowUpdated" 
            OnPageIndexChanging="gdvSearch_PageIndexChanging" 
            onrowupdating="gdvSearch_RowUpdating">
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
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CLINT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wallet ID" SortExpression="ACCNT_NO">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ACCNT_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile Number" SortExpression="ACCNT_MSISDN">
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
              <asp:BoundField SortExpression="STATE_NOTE"  HeaderText="State Note" DataField="STATE_NOTE" />  
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
