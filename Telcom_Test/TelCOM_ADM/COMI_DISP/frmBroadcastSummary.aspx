<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBroadcastSummary.aspx.cs" Inherits="COMI_DISP_frmBroadcastSummary" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">    
    <script language="javascript" type="text/javascript">
    function showWait()
    {
        if ($get('FileUpload1').value.length > 0)
        {
            $get('UpdateProgress1').style.display = 'block';
        }
    }
    </script>
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    </head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
        
    <asp:SqlDataSource ID="sdsBroadCastList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME,LOADED_SUMMARY,LOADED_TO_DB
FROM APSNG101.COMMISSION_MASTER C ORDER BY C.FILE_UPLOAD_TIME DESC'></asp:SqlDataSource>
        
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    
    <contenttemplate>
        <div style="background-color: royalblue;">
        <strong><span style="color: white">Commission List
            <asp:DropDownList ID="ddlLoadedFile" runat="server" 
                DataSourceID="sdsBroadCastList" DataTextField="COMI_MASTER_NAME" 
                DataValueField="COMI_MASTER_ID">
            </asp:DropDownList>
            &nbsp;
            <asp:Button ID="btnBroadcast" runat="server" onclick="btnBroadcast_Click" 
                Text="Print Summary" />
            </span></strong></div>
<DIV>
    <asp:SqlDataSource ID="sdsBCSummary" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT * FROM COM_BROADCAST_SUMMARY WHERE COMMISSION_ID=:COMI_MASTER_ID
">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlLoadedFile" Name="COMI_MASTER_ID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    
            <asp:GridView ID="gdvBulkAccountFile" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="sdsBCSummary" CssClass="mGrid" 
        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="CLINT_NAME" HeaderText="Channel Name" 
                        SortExpression="CLINT_NAME" />
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Channel Code" 
                        SortExpression="ACCNT_NO" />
                    <asp:BoundField DataField="REQUEST_PARTY" HeaderText="Easy Load Number" 
                        SortExpression="REQUEST_PARTY" />
                    <asp:BoundField DataField="CIMISSION_MONTH" HeaderText="Commission Month" 
                        SortExpression="CIMISSION_MONTH" />
                    <asp:BoundField DataField="TOTAL_BRADCASTED" HeaderText="Broadcast Times" 
                        SortExpression="TOTAL_BRADCASTED" />
                    <asp:BoundField DataField="ACKNOW_STATUS" HeaderText="Acknowledgement Status" 
                        SortExpression="ACKNOW_STATUS" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
    </DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

