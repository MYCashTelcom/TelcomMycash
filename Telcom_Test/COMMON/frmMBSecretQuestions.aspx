<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMBSecretQuestions.aspx.cs" Inherits="Forms_frmSecretQuestions" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Content Group</SPAN></STRONG></DIV>
<DIV><asp:SqlDataSource id="sdsSecretQuestion" runat="server" 
SelectCommand='SELECT ACCNT_SEC_QUES_SLNO, ACCNT_SEC_QUES_ID, ACCNT_SEC_QUES_DESC FROM ACCOUNT_SEC_QUESTION' 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>" DeleteCommand='DELETE FROM "ACCOUNT_SEC_QUESTION" WHERE "ACCNT_SEC_QUES_ID" = :ACCNT_SEC_QUES_ID' InsertCommand='INSERT INTO ACCOUNT_SEC_QUESTION(ACCNT_SEC_QUES_DESC, ACCNT_SEC_QUES_SLNO) VALUES (:ACCNT_SEC_QUES_DESC, :ACCNT_SEC_QUES_SLNO)' UpdateCommand='UPDATE ACCOUNT_SEC_QUESTION SET ACCNT_SEC_QUES_DESC = :ACCNT_SEC_QUES_DESC, ACCNT_SEC_QUES_SLNO = :ACCNT_SEC_QUES_SLNO WHERE (ACCNT_SEC_QUES_ID = :ACCNT_SEC_QUES_ID)'>
    <DeleteParameters>
        <asp:Parameter Name="ACCNT_SEC_QUES_ID" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="ACCNT_SEC_QUES_DESC" Type="String" />
        <asp:Parameter Name="ACCNT_SEC_QUES_SLNO" />
        <asp:Parameter Name="ACCNT_SEC_QUES_ID" Type="String" />
    </UpdateParameters>
    <InsertParameters>
       <asp:Parameter Name="ACCNT_SEC_QUES_DESC" Type="String" />
        <asp:Parameter Name="ACCNT_SEC_QUES_SLNO" />
    </InsertParameters>
</asp:SqlDataSource> 
    <asp:GridView id="GridView1" runat="server" DataSourceID="sdsSecretQuestion" DataKeyNames="ACCNT_SEC_QUES_ID" BorderColor="Silver" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="8"><Columns>
        <asp:BoundField DataField="ACCNT_SEC_QUES_SLNO" HeaderText="Question No" SortExpression="ACCNT_SEC_QUES_SLNO" />
        <asp:BoundField DataField="ACCNT_SEC_QUES_ID" HeaderText="ACCNT_SEC_QUES_ID" ReadOnly="True"
            SortExpression="ACCNT_SEC_QUES_ID" Visible="False" />
        <asp:TemplateField HeaderText="Question" SortExpression="ACCNT_SEC_QUES_DESC">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Height="40px" Text='<%# Bind("ACCNT_SEC_QUES_DESC") %>'
                    TextMode="MultiLine" Width="517px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Height="40px" Text='<%# Bind("ACCNT_SEC_QUES_DESC") %>'
                    TextMode="MultiLine" Width="517px"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add Content Group</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvServiceType" runat="server" DataSourceID="sdsSecretQuestion" DataKeyNames="ACCNT_SEC_QUES_ID" BorderColor="Silver" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px"><Fields>
    <asp:BoundField DataField="ACCNT_SEC_QUES_SLNO" HeaderText="Question No" SortExpression="ACCNT_SEC_QUES_SLNO">
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="Question" SortExpression="ACCNT_SEC_QUES_DESC">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACCNT_SEC_QUES_DESC") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Height="62px" Text='<%# Bind("ACCNT_SEC_QUES_DESC") %>'
                TextMode="MultiLine" Width="517px"></asp:TextBox>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACCNT_SEC_QUES_DESC") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
    </asp:TemplateField>
    <asp:CommandField ButtonType="Button" InsertText="Add Question" ShowInsertButton="True">
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

