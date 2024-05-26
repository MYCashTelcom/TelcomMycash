<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_Edit_System_Info.aspx.cs"
    Inherits="System_SYS_Edit_System_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Salary</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:SqlDataSource ID="sdsSystemInfo" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            DeleteCommand="DELETE FROM &quot;CM_SYSTEM_INFO&quot; WHERE &quot;SYSINFO_ID&quot; = :SYSINFO_ID"
            InsertCommand="INSERT INTO &quot;CM_SYSTEM_INFO&quot; (&quot;SYSINFO_ID&quot;, &quot;SOLN_NAME_BFR_LOGIN&quot;, &quot;SOLN_NAME_AFTR_LOGIN&quot;, &quot;COPYRIGHT_INFO&quot;, &quot;WELCOME_MSG&quot;) VALUES (:SYSINFO_ID, :SOLN_NAME_BFR_LOGIN, :SOLN_NAME_AFTR_LOGIN, :COPYRIGHT_INFO, :WELCOME_MSG)"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT * FROM &quot;CM_SYSTEM_INFO&quot;"
            UpdateCommand="UPDATE &quot;CM_SYSTEM_INFO&quot; SET &quot;SOLN_NAME_BFR_LOGIN&quot; = :SOLN_NAME_BFR_LOGIN, &quot;SOLN_NAME_AFTR_LOGIN&quot; = :SOLN_NAME_AFTR_LOGIN, &quot;COPYRIGHT_INFO&quot; = :COPYRIGHT_INFO, &quot;WELCOME_MSG&quot; = :WELCOME_MSG,&quot;TITLE_BFR_LOGIN&quot;=:TITLE_BFR_LOGIN,&quot;TITLE_AFTR_LOGIN&quot;=:TITLE_AFTR_LOGIN WHERE &quot;SYSINFO_ID&quot; = :SYSINFO_ID">
            <DeleteParameters>
                <asp:Parameter Name="SYSINFO_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="SOLN_NAME_BFR_LOGIN" Type="String" />
                <asp:Parameter Name="SOLN_NAME_AFTR_LOGIN" Type="String" />
                <asp:Parameter Name="COPYRIGHT_INFO" Type="String" />
                <asp:Parameter Name="WELCOME_MSG" Type="String" />
                <asp:Parameter Name="SYSINFO_ID" Type="String" />
                <asp:Parameter Name="TITLE_BFR_LOGIN" Type="String" />
                <asp:Parameter Name="TITLE_AFTR_LOGIN" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="SYSINFO_ID" Type="String" />
                <asp:Parameter Name="SOLN_NAME_BFR_LOGIN" Type="String" />
                <asp:Parameter Name="SOLN_NAME_AFTR_LOGIN" Type="String" />
                <asp:Parameter Name="COPYRIGHT_INFO" Type="String" />
                <asp:Parameter Name="WELCOME_MSG" Type="String" />
                <asp:Parameter Name="TITLE_BFR_LOGIN" Type="String" />
                <asp:Parameter Name="TITLE_AFTR_LOGIN" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<strong><span style="color: white">
                    <asp:SqlDataSource ID="sdsCMPBranch" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT &quot;CMP_BRANCH_ID&quot;, &quot;CMP_BRANCH_NAME&quot; FROM &quot;CM_CMP_BRANCH&quot;">
                    </asp:SqlDataSource>
                </span></strong>--%>
                <%--<div style="background-color: royalblue;">
                    <strong><span style="color: white">&nbsp;
                        <asp:DropDownList ID="ddlEmpBranch" runat="server" Font-Size="11px" AutoPostBack="True"
                            DataSourceID="sdsCMPBranch" DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID"
                            Visible="False">
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                    </span></strong>
                </div>--%>
                <asp:GridView ID="gdvSystemInfo" runat="server" AllowPaging="True" 
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                    CssClass="mGrid" DataKeyNames="SYSINFO_ID" DataSourceID="sdsSystemInfo" PagerStyle-CssClass="pgr"
                    PageSize="7">
                    <Columns>
                        <asp:BoundField DataField="SYSINFO_ID" HeaderText="System Info ID" ReadOnly="True"
                            SortExpression="SYSINFO_ID" Visible="false" />
                        <asp:BoundField DataField="SOLN_NAME_BFR_LOGIN" HeaderText="Name Before Login" SortExpression="SOLN_NAME_BFR_LOGIN" />
                        <asp:BoundField DataField="COPYRIGHT_INFO" HeaderText="Copyright Info" SortExpression="COPYRIGHT_INFO" />
                        <asp:BoundField DataField="SOLN_NAME_AFTR_LOGIN" HeaderText="Name After Login" SortExpression="SOLN_NAME_AFTR_LOGIN" />
                        <asp:BoundField DataField="WELCOME_MSG" HeaderText="Welcome Message" SortExpression="WELCOME_MSG" />
                         <asp:BoundField DataField="TITLE_BFR_LOGIN" HeaderText="Page Title before login " SortExpression="TITLE_BFR_LOGIN" />
                        <asp:BoundField DataField="TITLE_AFTR_LOGIN" HeaderText="Page Title after login" SortExpression="TITLE_AFTR_LOGIN" />
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle ForeColor="White" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
