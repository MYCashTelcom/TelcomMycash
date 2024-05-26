<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountType.aspx.cs" Inherits="COMMON_frmAccountType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Type</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div style="background-color: royalblue; text-align: left;">
                            <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">Account Type&nbsp;
                                </span>
                        </div>
    <asp:SqlDataSource ID="sdsClientList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        SelectCommand="SELECT AL.CLINT_ID,CL.CLINT_NAME,AL.ACCNT_NO,AL.ACCNT_TYPE_ID FROM CLIENT_LIST CL,ACCOUNT_LIST AL
WHERE CL.CLINT_ID=AL.CLINT_ID" 
        
        
        UpdateCommand="UPDATE ACCOUNT_LIST SET ACCNT_TYPE_ID= :ACCNT_TYPE_ID WHERE ACCNT_NO =:ACCNT_NO" InsertCommand="INSERT INTO ACCOUNT_LIST (ACCNT_TYPE_ID)
VALUES (:ACCNT_TYPE_ID) WHERE ACCNT_NO=:ACCNT_NO">
        <UpdateParameters>
            <asp:Parameter Name="ACCNT_NO" />
            <asp:Parameter Name="ACCNT_TYPE_ID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ACCNT_TYPE_ID" />
            <asp:Parameter Name="ACCNT_NO" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsClientType" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT ACCNT_TYPE_NAME,ACCNT_TYPE_ID FROM ACCOUNT_TYPE">
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataSourceID="sdsClientList" DataKeyNames="ACCNT_NO">
        <Columns>
        
          <%--
        <asp:GridView ID="gdvSysUsr" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="SYS_USR_ID" DataSourceID="sdsSysUsr"
                        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>--%>
        
        
        
            <asp:BoundField DataField="CLINT_NAME" HeaderText="Account Name" 
                ReadOnly="True" SortExpression="CLINT_NAME" />
            <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet Account" ReadOnly="True" 
                SortExpression="ACCNT_NO" />
            <asp:TemplateField HeaderText="Account Type" 
                SortExpression="CLIENT_TYPE_SHORT_CODE">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAccntType" runat="server" DataSourceID="sdsClientType" 
                        DataTextField="ACCNT_TYPE_NAME" DataValueField="ACCNT_TYPE_ID" 
                        SelectedValue='<%# Bind("ACCNT_TYPE_ID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="sdsClientType" DataTextField="ACCNT_TYPE_NAME" 
                        DataValueField="ACCNT_TYPE_ID" Enabled="False" 
                        SelectedValue='<%# Bind("ACCNT_TYPE_ID") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
        </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    </asp:GridView>
    </form>
</body>
</html>
