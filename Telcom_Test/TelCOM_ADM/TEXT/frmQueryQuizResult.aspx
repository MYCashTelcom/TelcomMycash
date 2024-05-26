<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQueryQuizResult.aspx.cs" Inherits="Forms_frmQueryQuizResult" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<DIV style="BACKGROUND-COLOR: royalblue"><STRONG><SPAN style="COLOR: white">Query Quiz Result: &nbsp;
    <asp:DropDownList ID="ddlQuizListList" runat="server" DataSourceID="sdsQuizList"
        DataTextField="QUIZ_NAME" DataValueField="QUIZ_ID" AutoPostBack="True" OnSelectedIndexChanged="ddlQuizListList_SelectedIndexChanged">
    </asp:DropDownList>&nbsp; </SPAN></STRONG></DIV><DIV>
    <asp:SqlDataSource id="sdsQuizList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT QUIZ_ID, QUIZ_TITLE || ' (' || QUIZ_CODE || ')' AS QUIZ_NAME,QUIZ_TEXT FROM QUIZ_LIST"></asp:SqlDataSource> 
        <asp:SqlDataSource ID="sdsQuizResult" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT QL.QUIZ_ID,QL.QUIZ_TITLE,QL.QUIZ_CODE,QA.REQUEST_PARTY,QA.REQUEST_TIME,QA.ANSWER SEND_ANSWER,QL.QUIZ_ANS1 CORRECT_ANSWER,DECODE(QL.QUIZ_ANS1,QA.ANSWER,'Correct','Wrong') RESULT FROM ALL_QUIZ_ANSWER QA, QUIZ_LIST QL WHERE SUBSTR(QL.QUIZ_ID,5,8)=SUBSTR(QA.QUIZID,1,8) AND QL.QUIZ_CODE=SUBSTR(QA.QUIZID,9,4) AND QL.QUIZ_ID=:QUIZ_ID">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlQuizListList" Name="QUIZ_ID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="grvRequestList" runat="server"  AutoGenerateColumns="False"
            DataKeyNames="QUIZ_ID" DataSourceID="sdsQuizResult" Font-Size="11pt">
            <Columns>
                <asp:BoundField DataField="QUIZ_TITLE" HeaderText="Name" SortExpression="QUIZ_TITLE" />
                <asp:BoundField DataField="QUIZ_CODE" HeaderText="Quiz Code" SortExpression="QUIZ_CODE" />
                <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Participent" SortExpression="REQUEST_PARTY" />
                <asp:BoundField DataField="REQUEST_TIME" HeaderText="Answered Time" SortExpression="REQUEST_TIME" />
                <asp:BoundField DataField="SEND_ANSWER" HeaderText="Send Answered" SortExpression="SEND_ANSWER" />
                <asp:BoundField DataField="CORRECT_ANSWER" HeaderText="Correct Answer" SortExpression="CORRECT_ANSWER" />
                <asp:BoundField DataField="RESULT" HeaderText="Result" SortExpression="RESULT" />
            </Columns>
        </asp:GridView>
        &nbsp;<BR />&nbsp; &nbsp;</DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
