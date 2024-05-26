<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQuizList.aspx.cs" Inherits="Forms_frmQuizListt" %>
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
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Quiz List</SPAN></STRONG>&nbsp;
    <asp:DropDownList ID="ddlAccountId" runat="server" DataSourceID="sdsAccountList"
        DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" AutoPostBack="True">
    </asp:DropDownList></DIV><DIV><asp:SqlDataSource id="sdsAccountList" runat="server" SelectCommand="SELECT ACCNT_ID, ACCNT_NO || ' [' || ACCNT_MSISDN||']' AS ACCNT_NO FROM ACCOUNT_LIST" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
</asp:SqlDataSource> 
    <asp:SqlDataSource ID="sdsQuizList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        DeleteCommand='DELETE FROM "QUIZ_LIST" WHERE "QUIZ_ID" = :QUIZ_ID' InsertCommand='INSERT INTO QUIZ_LIST(QUIZ_ID, QUIZ_CODE, QUIZ_TITLE, QUIZ_TEXT, QUIZ_ANS1, QUIZ_ANS2, QUIZ_ANS3, QUIZ_ANS4, QUIZ_VALID_DATE, ACCNT_ID, QUIZ_START_DATE) VALUES (:QUIZ_ID, :QUIZ_CODE, :QUIZ_TITLE, :QUIZ_TEXT, :QUIZ_ANS1, :QUIZ_ANS2, :QUIZ_ANS3, :QUIZ_ANS4, :QUIZ_VALID_DATE, :ACCNT_ID,:QUIZ_START_DATE)'
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT QUIZ_ID, QUIZ_CODE, QUIZ_TITLE, QUIZ_TEXT, QUIZ_ANS1, QUIZ_ANS2, QUIZ_ANS3, QUIZ_ANS4, QUIZ_VALID_DATE, ACCNT_ID, QUIZ_START_DATE FROM QUIZ_LIST WHERE (ACCNT_ID = :ACCNT_ID)'
        UpdateCommand='UPDATE QUIZ_LIST SET QUIZ_CODE = :QUIZ_CODE, QUIZ_TITLE = :QUIZ_TITLE, QUIZ_TEXT = :QUIZ_TEXT, QUIZ_ANS1 = :QUIZ_ANS1, QUIZ_ANS2 = :QUIZ_ANS2, QUIZ_ANS3 = :QUIZ_ANS3, QUIZ_ANS4 = :QUIZ_ANS4, QUIZ_VALID_DATE = :QUIZ_VALID_DATE, ACCNT_ID = :ACCNT_ID, QUIZ_START_DATE = :QUIZ_START_DATE WHERE (QUIZ_ID = :QUIZ_ID)'>
        <DeleteParameters>
            <asp:Parameter Name="QUIZ_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="QUIZ_CODE" Type="String" />
            <asp:Parameter Name="QUIZ_TITLE" Type="String" />
            <asp:Parameter Name="QUIZ_TEXT" Type="String" />
            <asp:Parameter Name="QUIZ_ANS1" Type="String" />
            <asp:Parameter Name="QUIZ_ANS2" Type="String" />
            <asp:Parameter Name="QUIZ_ANS3" Type="String" />
            <asp:Parameter Name="QUIZ_ANS4" Type="String" />
            <asp:Parameter Name="QUIZ_VALID_DATE" Type="DateTime" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="QUIZ_START_DATE" Type="DateTime" />
            <asp:Parameter Name="QUIZ_ID" Type="String" />
            
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="QUIZ_ID" Type="String" />
            <asp:Parameter Name="QUIZ_CODE" Type="String" />
            <asp:Parameter Name="QUIZ_TITLE" Type="String" />
            <asp:Parameter Name="QUIZ_TEXT" Type="String" />
            <asp:Parameter Name="QUIZ_ANS1" Type="String" />
            <asp:Parameter Name="QUIZ_ANS2" Type="String" />
            <asp:Parameter Name="QUIZ_ANS3" Type="String" />
            <asp:Parameter Name="QUIZ_ANS4" Type="String" />
            <asp:Parameter Name="QUIZ_VALID_DATE" Type="DateTime" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="QUIZ_START_DATE" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlAccountId" Name="ACCNT_ID" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView id="GridView1" runat="server" DataSourceID="sdsQuizList" DataKeyNames="QUIZ_ID" BorderColor="White" AutoGenerateColumns="False"  Font-Size="11pt" AllowPaging="True" PageSize="5">
    <Columns>
    <asp:BoundField DataField="QUIZ_ID" HeaderText="QUIZ_ID" ReadOnly="True" SortExpression="QUIZ_ID"
        Visible="False" />
    <asp:TemplateField HeaderText="Account" SortExpression="ACCNT_ID">
        <EditItemTemplate>
            <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsAccountList"
                DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" SelectedValue='<%# Bind("ACCNT_ID") %>'>
            </asp:DropDownList>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsAccountList"
                DataTextField="ACCNT_NO" DataValueField="ACCNT_ID" Enabled="False" SelectedValue='<%# Bind("ACCNT_ID") %>'>
            </asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="QUIZ_TITLE" HeaderText="Quiz Name" SortExpression="QUIZ_TITLE" />
    <asp:BoundField DataField="QUIZ_CODE" HeaderText="Quiz Code" SortExpression="QUIZ_CODE" />
        <asp:TemplateField HeaderText="Quiz" SortExpression="QUIZ_TEXT">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Height="103px" Text='<%# Bind("QUIZ_TEXT") %>'
                    TextMode="MultiLine" Width="347px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Height="76px" Text='<%# Bind("QUIZ_TEXT") %>'
                    TextMode="MultiLine" Width="348px"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="QUIZ_START_DATE" HeaderText="Start Date" SortExpression="QUIZ_START_DATE" />
    <asp:BoundField DataField="QUIZ_VALID_DATE" HeaderText="Expiry Date" SortExpression="QUIZ_VALID_DATE" />
    <asp:BoundField DataField="QUIZ_ANS1" HeaderText="Answer1" SortExpression="QUIZ_ANS1" />
    <asp:BoundField DataField="QUIZ_ANS2" HeaderText="Answer2" SortExpression="QUIZ_ANS2" />
    <asp:BoundField DataField="QUIZ_ANS3" HeaderText="Answer3" SortExpression="QUIZ_ANS3" />
    <asp:BoundField DataField="QUIZ_ANS4" HeaderText="Answer4" SortExpression="QUIZ_ANS4" />
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
</Columns>
</asp:GridView> <BR />&nbsp;</DIV><DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Add New&nbsp;Quiz&nbsp;</SPAN></STRONG></DIV><DIV><asp:DetailsView id="dlvQuiz" runat="server" DataSourceID="sdsQuizList" DataKeyNames="QUIZ_ID" BorderColor="Gainsboro" Font-Size="11pt" Font-Names="Times New Roman" DefaultMode="Insert" AutoGenerateRows="False" Width="125px" Height="50px" OnItemInserting="dlvServiceType_ItemInserting" OnItemCreated="dlvQuiz_ItemCreated"><Fields>
    <asp:BoundField DataField="QUIZ_ID" HeaderText="QUIZ_ID" ReadOnly="True" SortExpression="QUIZ_ID" Visible="False" />
    <asp:BoundField DataField="QUIZ_TITLE" HeaderText="Quiz Name" SortExpression="QUIZ_TITLE" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />        
    </asp:BoundField>
    <asp:BoundField DataField="QUIZ_CODE" HeaderText="Quiz Code" SortExpression="QUIZ_CODE" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False"/>
    </asp:BoundField>
    <asp:TemplateField HeaderText="Quiz" SortExpression="QUIZ_TEXT">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("QUIZ_TEXT") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Height="103px" Text='<%# Bind("QUIZ_TEXT") %>'
                TextMode="MultiLine" Width="347px"></asp:TextBox>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("QUIZ_TEXT") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Start Date" SortExpression="QUIZ_START_DATE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("QUIZ_START_DATE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("QUIZ_START_DATE") %>'></asp:TextBox>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text='<%# Bind("QUIZ_START_DATE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Expiry Date" SortExpression="QUIZ_VALID_DATE">
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("QUIZ_VALID_DATE") %>'></asp:TextBox>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:TextBox ID="txtDV_ValidDate" runat="server" Text='<%# Bind("QUIZ_VALID_DATE") %>'></asp:TextBox>
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("QUIZ_VALID_DATE") %>'></asp:Label>
        </ItemTemplate>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False" />
    </asp:TemplateField>
    <asp:BoundField DataField="QUIZ_ANS1" HeaderText="Answer1" SortExpression="QUIZ_ANS1" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False"/>
    </asp:BoundField>
    <asp:BoundField DataField="QUIZ_ANS2" HeaderText="Answer2" SortExpression="QUIZ_ANS2" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False"/>
    </asp:BoundField>
    <asp:BoundField DataField="QUIZ_ANS3" HeaderText="Answer3" SortExpression="QUIZ_ANS3" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False"/>
    </asp:BoundField>
    <asp:BoundField DataField="QUIZ_ANS4" HeaderText="Answer4" SortExpression="QUIZ_ANS4" >
        <HeaderStyle Font-Bold="True" HorizontalAlign="Right" Wrap="False"/>
    </asp:BoundField>
    <asp:BoundField DataField="ACCNT_ID" HeaderText="ACCNT_ID" SortExpression="ACCNT_ID" Visible="False" />
    <asp:CommandField ButtonType="Button" InsertText="Add Quiz" ShowInsertButton="True">
        <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
</Fields>
</asp:DetailsView></DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>