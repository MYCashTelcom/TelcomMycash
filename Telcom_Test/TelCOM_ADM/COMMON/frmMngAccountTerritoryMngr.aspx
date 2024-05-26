<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngAccountTerritoryMngr.aspx.cs" Inherits="COMMON_frmMngAccountTerritoryMngr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
        
            <asp:SqlDataSource ID="sdsClientList" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                 SelectCommand="SELECT CL.CLINT_NAME || ' ['|| CL.CLINT_ID || ' ]' CLIENT_NAME, CL.CLINT_ID
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
                            InsertCommand='INSERT INTO ACCOUNT_LIST(CLINT_ID, ACCNT_NO, ACCNT_STATE, SERVICE_PKG_ID, ACCNT_MSISDN,STATE_NOTE) VALUES (:CLINT_ID, :ACCNT_NO, :ACCNT_STATE, :SERVICE_PKG_ID, :ACCNT_MSISDN,:STATE_NOTE)'
                            UpdateCommand='UPDATE ACCOUNT_LIST SET ACCNT_STATE = :ACCNT_STATE, SERVICE_PKG_ID = :SERVICE_PKG_ID ,ACCNT_RANK_ID = :ACCNT_RANK_ID,STATE_NOTE=:STATE_NOTE  WHERE (ACCNT_ID = :ACCNT_ID)'>
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
                            SelectCommand="SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_ID IN ('1205190002','1205190003')">
                            <%--SelectCommand='SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE'--%>
                            <%--SELECT SERVICE_PKG_ID, SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_ID IN ('1205190002','1205190003')--%>
            </asp:SqlDataSource>
                        
            <asp:SqlDataSource ID="sdsAccntRank" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                     SelectCommand="SELECT RANK_TITEL,ACCNT_RANK_ID FROM ACCOUNT_RANK WHERE ACCNT_RANK_ID IN ('120519000000000005','120519000000000006','130914000000000001')">
                        <%--SelectCommand="SELECT RANK_TITEL,ACCNT_RANK_ID FROM ACCOUNT_RANK WHERE SHORT_CODE IN ('NA','D','SD','SA','A','C','MCPAY') ORDER BY ACCNT_RANK_ID"--%>
            </asp:SqlDataSource>
            
            <asp:SqlDataSource runat="server" ID="sdsUserGroup"
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                    SelectCommand="SELECT SYS_USR_GRP_ID, SYS_USR_GRP_TITLE FROM APSNG101.CM_SYSTEM_USER_GROUP ">
                    <%--SelectCommand="SELECT SYS_USR_GRP_ID, SYS_USR_GRP_TITLE FROM APSNG101.CM_SYSTEM_USER_GROUP WHERE SYS_USR_GRP_ID = '14061601001001'"--%>
            </asp:SqlDataSource>
            <div>
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
            </div>
    <div>
        <asp:Panel ID="pnlView" runat="server">
           <table class="View_Panel" cellpadding="0" cellspacing="4">
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
    </div>
    <div>
        <asp:GridView ID="gdvSearch" Visible="False" runat="server" DataSourceID="sdsClientAccount2"
                    DataKeyNames="ACCNT_ID" AutoGenerateColumns="False" 
            AllowPaging="True" AllowSorting="True"
                    BorderColor="#E0E0E0" PageSize="7" CssClass="mGrid" PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="gdvSearch_RowCancelingEdit" 
                    OnRowEditing="gdvSearch_RowEditing" OnRowUpdated="gdvSearch_RowUpdated" 
                    OnPageIndexChanging="gdvSearch_PageIndexChanging" 
                    onrowupdating="gdvSearch_RowUpdating" 
            onselectedindexchanged="gdvSearch_SelectedIndexChanged" 
            onselectedindexchanging="gdvSearch_SelectedIndexChanging">
                    <Columns>
                        <%--<asp:BoundField DataField="ACCNT_ID" HeaderText="ACCNT_ID" SortExpression="ACCNT_ID"
                            ReadOnly="True" Visible="False" />--%>
                            
                       <asp:TemplateField HeaderText="ACCNT_ID" SortExpression="ACCNT_ID" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ACCNT_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ACCNT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>     
                        <asp:TemplateField HeaderText="Service Package" SortExpression="SERVICE_PKG_ID">
                            <%--<EditItemTemplate>
                                <asp:DropDownList ID="ddlEIPackage" runat="server" DataSourceID="sdsServicePackage"
                                    DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Bind("SERVICE_PKG_ID") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:DropDownList  ID="ddlEIPackage" runat="server" DataSourceID="sdsServicePackage" Enabled="False"
                                    DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID" SelectedValue='<%# Eval("SERVICE_PKG_ID") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Wallet Name" SortExpression="CLINT_NAME">
                            <%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("CLINT_NAME") %>'></asp:Label>
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
                                   <asp:DropDownList ID="ddlAccntrankID" runat="server" DataSourceID="sdsAccntRank" DataTextField="RANK_TITEL" 
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
                        <asp:TemplateField HeaderText="State" SortExpression="ACCNT_STATE" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
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
                        <asp:TemplateField HeaderText="State Note" SortExpression="STATE_NOTE">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATE_NOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("STATE_NOTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                 <ajaxToolkit:ConfirmButtonExtender ID="cbeNominiUpdate" runat="server" 
                                         DisplayModalPopupID="ModalPopupExtender2" onclientcancel="cancelClick" 
                                         TargetControlID="btnUpdate">
                                 </ajaxToolkit:ConfirmButtonExtender>  
                                 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                         BackgroundCssClass="modalBackground" CancelControlID="btnUpdateCancel" 
                                         OkControlID="btnUpdateOK" TargetControlID="btnUpdate" 
                                         PopupControlID="pnlUpdate">
                                 </ajaxToolkit:ModalPopupExtender>    
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                    CommandName="Edit" Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle ForeColor="White" />
                    <AlternatingRowStyle CssClass="alt" />
                 </asp:GridView>
                 <asp:Panel ID="pnlUpdate" runat="server"  style=" display:none;width:300px; height:165px; 
                     background-color:White; border-width:1px; border-color:Silver; border-style:solid; padding:1px;">
                  <div style="height:95px;"><br />&nbsp;<br />&nbsp;
                  Are you sure you want to update?
                     <br />&nbsp;<br />&nbsp;
                  </div>
                  <div style="text-align:right; background-color: #C0C0C0;height:70px;">
                        <br />&nbsp;
                        <asp:Button ID="btnUpdateOK" runat="server" Text="  Yes  " />
                        <asp:Button ID="btnUpdateCancel" runat="server" Text=" Cancel " />
                  </div>
                </asp:Panel>
    </div>
    <div>
        <asp:Panel runat="server" ID="panelBtn" BackColor="silver">
           <table>
               <tr>
                   <td>
                       <asp:Label runat="server" ID="lblToAgent"><strong>Change Customer to Agent:</strong></asp:Label>
                   </td>
                   <td>
                       <asp:Button runat="server" ID="btnConvertToAgent" Text="Change Customer to Agent" 
                           onclick="btnConvertToAgent_Click"/>
                   </td>
               </tr>
           </table> 
        </asp:Panel>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
