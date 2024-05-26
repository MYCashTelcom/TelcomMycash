<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngClintBankAccount.aspx.cs"
    Inherits="Forms_frmMngClintBankAcc" Title="Manage Client bank Account" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
           
                <asp:SqlDataSource ID="sdsBankList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                    SelectCommand='SELECT * FROM "BANK_LIST" '>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsCliBankAcc" runat="server" DeleteCommand='DELETE FROM "CLIENT_BANK_ACCOUNT" WHERE "CLINT_BANK_ACC_ID" = :CLINT_BANK_ACC_ID'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" InsertCommand='INSERT INTO "CLIENT_BANK_ACCOUNT" ("ACCNT_ID","CLINT_BANK_ACC_ID", "CLINT_BANK_ACC_NO", "CLINT_BANK_ACC_LOGIN", "CLINT_BANK_ACC_PASS", "BANK_ID") VALUES (:ACCNT_ID,:CLINT_BANK_ACC_ID, :CLINT_BANK_ACC_NO, :CLINT_BANK_ACC_LOGIN, :CLINT_BANK_ACC_PASS, :BANK_ID)'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" UpdateCommand='UPDATE "CLIENT_BANK_ACCOUNT" SET "ACCNT_ID"= :ACCNT_ID,"CLINT_BANK_ACC_NO" = :CLINT_BANK_ACC_NO, "CLINT_BANK_ACC_LOGIN" = :CLINT_BANK_ACC_LOGIN, "CLINT_BANK_ACC_PASS" = :CLINT_BANK_ACC_PASS, "BANK_ID" = :BANK_ID WHERE "CLINT_BANK_ACC_ID" = :CLINT_BANK_ACC_ID'
                    SelectCommand=''>
                    <%--SELECT * FROM "CLIENT_BANK_ACCOUNT" ORDER BY CLINT_BANK_ACC_ID DESC--%>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_ID"></asp:Parameter>
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Type="String" Name="ACCNT_ID"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_NO"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_LOGIN"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_PASS"></asp:Parameter>
                        <asp:Parameter Type="String" Name="BANK_ID"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_ID"></asp:Parameter>
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Type="String" Name="ACCNT_ID"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_ID"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_NO"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_LOGIN"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_BANK_ACC_PASS"></asp:Parameter>
                        <asp:Parameter Type="String" Name="BANK_ID"></asp:Parameter>
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsClientAccount" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT AL.ACCNT_ID, CL.CLINT_NAME || ' (' || AL.ACCNT_MSISDN || ')' AS ACCNT_NO 
                            FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID">
             </asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsCMobile" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                    
                    SelectCommand="SELECT concat(substr(AL.ACCNT_MSISDN,4,14),'1') as CLINT_MOBILE  
FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND (&quot;CLINT_ID&quot; = :CLINT_ID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlClAccount" Name="CLINT_ID" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
               <div style="background-color: royalblue">
                    <strong><span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">Client Bank Account Search</span></strong>&nbsp;<asp:Label 
                    ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <table cellpadding="0" cellspacing="4">                                
                                <tr>
                                    <td align="right">
                                        <span style="font-size: 11px;">Wallet ID: </span>
                                    </td>                                    
                                    <td align="left">
                                        <asp:TextBox ID="txtWalletID" runat="server"></asp:TextBox>
                                    </td>                                
                                    <td align="right">
                                        <span style="font-size: 11px;">Mobile Number: </span>
                                    </td>                                    
                                    <td align="left">
                                        <asp:TextBox ID="txtMSISDN" runat="server"></asp:TextBox>
                                    </td>                                
                                    <td  align="right">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                            onclick="btnSearch_Click"/>
                                    </td>
                                </tr>                               
                            </table>
                </div>
                 <div>
                  <asp:GridView ID="GridView1" runat="server" AllowSorting="True"  Width="900px"
                         AutoGenerateColumns="False" DataKeyNames="CLINT_BANK_ACC_ID"
                      DataSourceID="sdsCliBankAcc"   AllowPaging="True" BorderStyle="None" 
                         BorderColor="#E0E0E0"  CssClass="mGrid" PagerStyle-CssClass="pgr"
                                   AlternatingRowStyle-CssClass="alt" 
                         onrowcancelingedit="GridView1_RowCancelingEdit" 
                          onrowupdated="GridView1_RowUpdated" onrowediting="GridView1_RowEditing" 
                         onrowdeleted="GridView1_RowDeleted">
                         
                    <Columns>
                        <asp:BoundField ReadOnly="True" DataField="CLINT_BANK_ACC_ID" Visible="False" SortExpression="CLINT_BANK_ACC_ID"
                            HeaderText="CLINT_BANK_ACC_ID"></asp:BoundField>
                        <asp:TemplateField SortExpression="ACCNT_ID" HeaderText="Client Account" ItemStyle-Width="100px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsClientAccount"
                                    SelectedValue='<%# Bind("ACCNT_ID") %>' DataTextField="ACCNT_NO" DataValueField="ACCNT_ID">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsClientAccount"
                                    SelectedValue='<%# Bind("ACCNT_ID") %>' DataTextField="ACCNT_NO" DataValueField="ACCNT_ID"
                                    Enabled="False">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="BANK_ID" HeaderText="Bank" ItemStyle-Width="150px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList20" runat="server" DataSourceID="sdsBankList" SelectedValue='<%# Bind("BANK_ID") %>'
                                    DataTextField="BANK_NAME" DataValueField="BANK_ID">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList10" runat="server" DataSourceID="sdsBankList" SelectedValue='<%# Bind("BANK_ID") %>'
                                    DataTextField="BANK_NAME" DataValueField="BANK_ID" Enabled="False">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CLINT_BANK_ACC_NO" SortExpression="CLINT_BANK_ACC_NO"
                            HeaderText="Account Synonym">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CLINT_BANK_ACC_LOGIN" SortExpression="CLINT_BANK_ACC_LOGIN"
                            HeaderText="Bank A/C No">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField SortExpression="CLINT_BANK_ACC_PASS" HeaderText="Account Pass">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Width="104px" Text='<%# Bind("CLINT_BANK_ACC_PASS") %>'
                                    TextMode="Password" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowDeleteButton="True">
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                                     <AlternatingRowStyle CssClass="alt" /> 
                </asp:GridView>
                &nbsp;
                </div>
            <div style="background-color: royalblue">
                <strong><span style="color: white">Add New&nbsp;Client Bank Account&nbsp;</span></strong></div>
            <div>
                <table style="font-size:12px;">
                <tr>
                  <td align="right"><asp:Label ID="Label4" runat="server" Text="Client Account"></asp:Label>  
                  </td>
                  <td>
                    <asp:DropDownList ID="ddlClAccount" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsClientAccount" DataTextField="ACCNT_NO" 
                        DataValueField="ACCNT_ID" 
                        onselectedindexchanged="ddlClAccount_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                  <td align="right"><asp:Label ID="Label5" runat="server" Text="Bank"></asp:Label> 
                   </td>
                   <td>
                    <asp:DropDownList ID="dlBank" runat="server" DataSourceID="sdsBankList" 
                        DataTextField="BANK_NAME" DataValueField="BANK_ID">
                    </asp:DropDownList>
                    </td></tr>
                 <tr><td align="right"><asp:Label ID="Label6" runat="server" Text="Account Synonym"></asp:Label>  </td><td>
                     <asp:TextBox ID="txtAcc_Syn" runat="server" Height="22px"></asp:TextBox>
                     </td></tr>
                  <tr><td align="right"><asp:Label ID="Label7" runat="server" Text="Account A/C No"></asp:Label>  </td><td>
                      <asp:TextBox ID="txtAccNo" runat="server"></asp:TextBox></td></tr>
                    <tr><td align="right"><asp:Label ID="Label8" runat="server" Text="Account Password"></asp:Label>  </td><td>
                        <asp:TextBox ID="txtPawd" runat="server" TextMode="Password"></asp:TextBox></td></tr>
                     <tr><td>
                         <asp:Button ID="btnAddClient" runat="server" onclick="btnAddClient_Click" 
                             Text="Add Client Account" Width="150px" />
                         </td><td>
                             <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                 Text="Refresh" />
                         </td></tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
