<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCommiUpload.aspx.cs" Inherits="Forms_frmCommiUpload" %>
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
        
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <Triggers>
          <asp:PostBackTrigger ControlID="btnUpload" /> 
          <asp:PostBackTrigger ControlID="btnExpClinetList" />
     </Triggers>
     
    <contenttemplate>
        <asp:Button ID="btnExpClinetList" runat="server" 
            OnClick="btnExpClinetList_Click" Text="Export Client List" 
            BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" 
            Font-Bold="False" ForeColor="Black" Visible="False" />
        <asp:Button ID="btnExpAccList" runat="server" Text="Export Account List" 
            BackColor="LightSteelBlue" BorderColor="LightSlateGray" BorderStyle="Solid" 
            Font-Bold="False" ForeColor="Black" OnClick="btnExpAccList_Click" 
            Visible="False" /><div style="background-color: royalblue;">
        <strong><span style="color: white">Commission File List</span></strong></div>
<DIV>
    <asp:SqlDataSource ID="sdsBulkAccountFile" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME,LOADED_SUMMARY,LOADED_TO_DB
FROM APSNG101.COMMISSION_MASTER C ORDER BY C.FILE_UPLOAD_TIME DESC'></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPendingBFile" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME
FROM APSNG101.COMMISSION_MASTER C WHERE LOADED_TO_DB='N' ORDER BY C.FILE_UPLOAD_TIME DESC">
    </asp:SqlDataSource>
    
            <asp:SqlDataSource ID="sdsLoadedFile" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME
FROM APSNG101.COMMISSION_MASTER C WHERE LOADED_TO_DB='Y' ORDER BY C.FILE_UPLOAD_TIME DESC">
    </asp:SqlDataSource>
    
            <asp:GridView ID="gdvBulkAccountFile" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="COMI_MASTER_ID" 
        DataSourceID="sdsBulkAccountFile" CssClass="mGrid" 
        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="COMI_MASTER_ID" HeaderText="COMI_MASTER_ID" 
                        ReadOnly="True" SortExpression="COMI_MASTER_ID" Visible="False" />
                    <asp:BoundField DataField="COMI_MASTER_NAME" HeaderText="Commission Title" 
                        SortExpression="COMI_MASTER_NAME" />
                    <asp:BoundField DataField="COMI_FILE_NAME" HeaderText="File Name" 
                        SortExpression="COMI_FILE_NAME" />
                    <asp:BoundField DataField="FILE_UPLOAD_TIME" HeaderText="Upload Time" 
                        SortExpression="FILE_UPLOAD_TIME" />
                    <asp:BoundField DataField="COMI_STRAT_DATE" HeaderText="Cycle Start Date" 
                        SortExpression="COMI_STRAT_DATE" Visible="False" />
                    <asp:BoundField DataField="COMI_END_DATE" HeaderText="Cycle End Date" 
                        SortExpression="COMI_END_DATE" />
                    <asp:BoundField DataField="LOADED_TO_DB" HeaderText="Loaded To Database" 
                        SortExpression="LOADED_TO_DB" />
                    <asp:BoundField DataField="LOADED_SUMMARY" HeaderText="Loading Summary" 
                        SortExpression="LOADED_SUMMARY" />
                    <asp:BoundField DataField="BROAD_CAST_COUNT" HeaderText="Broadcast Count" 
                        SortExpression="BROAD_CAST_COUNT" />
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
    </asp:GridView>
    <br />
    
            <asp:Label ID="lblMessage" runat="server" Text="Label" 
        ForeColor="MediumBlue"></asp:Label><asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Label ID="lblWait" runat="server" BackColor="Transparent" 
                        Font-Bold="True" ForeColor="Black" 
                        Text="Please wait ........." Width="119px"></asp:Label>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/pleasewait.gif" />
            </ProgressTemplate>
            </asp:UpdateProgress>
    <br />
    <table> 
    <tr>
        <td style="background-color: royalblue" colspan="2">
            <strong><span style="color: #ffffff; font-size: 11px;">Commission Cycle&nbsp; </span></strong></td>
    </tr>  
    <tr>
        <td>
            <asp:Label ID="lblWait0" runat="server" BackColor="Transparent" 
                Font-Bold="True" Font-Size="12px" ForeColor="Black" Text="From Date" 
                Width="70px"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblWait1" runat="server" BackColor="Transparent" 
                Font-Bold="True" Font-Size="12px" ForeColor="Black" Text="To Date" Width="55px"></asp:Label>
            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="background-color: royalblue" colspan="2">
            <strong><span style="color: #ffffff; font-size: 11px;">File Upload&nbsp; </span></strong></td>
    </tr>
    <tr><td colspan="2" style="text-align: right">
    <asp:FileUpload ID="FileUpload1" runat="server" Width="501px" Height="24px" />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" OnClientClick="javascript:showWait()" Text="Upload" /></td>
        </tr>
        <tr>            
        <td style="background-color: royalblue">
            <strong><span style="color: #ffffff; font-size: 11px;">Bulk Clinet</span></strong></td>
        <td style="background-color: royalblue">
            &nbsp;</td>
    </tr> 
    <tr> 
        <td>
            <asp:DropDownList ID="ddlPendingFile" runat="server" 
                DataSourceID="sdsPendingBFile" DataTextField="COMI_MASTER_NAME" 
                DataValueField="COMI_MASTER_ID">
            </asp:DropDownList>
            <asp:Button ID="btnCreateSub" runat="server" Text="Load To DB" 
                OnClick="btnCreateSub_Click" />
            <asp:Button ID="Button2" runat="server" Text="Update" 
                OnClick="btnExecuted_Click" Visible="False" />
            </td> 
        <td>
            <asp:DropDownList ID="ddlLoadedFile" runat="server" 
                DataSourceID="sdsLoadedFile" DataTextField="COMI_MASTER_NAME" 
                DataValueField="COMI_MASTER_ID">
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnBroadcast" runat="server" onclick="btnBroadcast_Click" 
                Text="Broadcast" />
        </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
    </DIV>
</contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
