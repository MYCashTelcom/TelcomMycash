<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmComImpoMaster.aspx.cs"
    Inherits="COMI_DISP_frmComImpoMaster" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

   <script language="javascript" type="text/javascript">
        function showWait() {
//            if ($get('FileUpload1').value.length > 0) {
//                $get('UpdateProgress1').style.display = 'block';
//            }
        }
    </script>
    
    
  <%--
    <script language="javascript" type="text/javascript">
        function ShowProgressBar() {
            var iFrame = document.getElementById('Iframe1');
            iFrame.src = "frmShowMessage.aspx";
        }
    </script> --%> 

    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            var panelProg = $get('divImage');
            panelProg.style.display = '';
            // reset label text
            var lbl = $get('<%= this.lblMessage.ClientID %>');
            lbl.innerHTML = '';

            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = true;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divImage and hide it again
            var panelProg = $get('divImage');
            panelProg.style.display = 'none';

            // Enable button that caused a postback
            $get(sender._postBackSettings.sourceElement.id).disabled = false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnExpClinetList" />
        </Triggers>
        <ContentTemplate>
            <asp:Button ID="btnExpClinetList" runat="server" OnClick="btnExpClinetList_Click"
                Text="Export Client List" BackColor="LightSteelBlue" BorderColor="LightSlateGray"
                BorderStyle="Solid" Font-Bold="False" ForeColor="Black" Visible="False" />
            <asp:Button ID="btnExpAccList" runat="server" Text="Export Account List" BackColor="LightSteelBlue"
                BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False" ForeColor="Black"
                OnClick="btnExpAccList_Click" Visible="False" /><div style="background-color: royalblue;">
                    <strong><span style="color: white">Commission File List</span></strong></div>
            <div>
                <asp:SqlDataSource ID="sdsBulkAccountFile" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand='SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME,LOADED_SUMMARY,LOADED_TO_DB
FROM APSNG101.COMMISSION_MASTER C ORDER BY C.FILE_UPLOAD_TIME DESC' UpdateCommand="UPDATE COMMISSION_MASTER SET LOADED_TO_DB = :LOADED_TO_DB WHERE (COMI_MASTER_ID = :COMI_MASTER_ID)">
                    <UpdateParameters>
                        <asp:Parameter Name="LOADED_TO_DB" />
                        <asp:Parameter Name="COMI_MASTER_ID" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsPendingBFile" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME
FROM APSNG101.COMMISSION_MASTER C WHERE LOADED_TO_DB='N' ORDER BY C.FILE_UPLOAD_TIME DESC"></asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsLoadedFile" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT 
C.COMI_MASTER_ID, C.COMI_MASTER_NAME, C.COMI_STRAT_DATE, 
   C.COMI_END_DATE, C.COMI_FILE_NAME, C.BROAD_CAST_COUNT,C.FILE_UPLOAD_TIME
FROM APSNG101.COMMISSION_MASTER C WHERE LOADED_TO_DB='Y' ORDER BY C.FILE_UPLOAD_TIME DESC"></asp:SqlDataSource>
                <asp:GridView ID="gdvBulkAccountFile" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="COMI_MASTER_ID" DataSourceID="sdsBulkAccountFile" CssClass="mGrid"
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                    AllowPaging="True" PageSize="5" 
                    onrowupdated="gdvBulkAccountFile_RowUpdated">
                    <Columns>
                        <asp:BoundField DataField="COMI_MASTER_ID" HeaderText="COMI_MASTER_ID" ReadOnly="True"
                            SortExpression="COMI_MASTER_ID" Visible="False" />
                        <asp:BoundField DataField="COMI_MASTER_NAME" HeaderText="Commission Title" SortExpression="COMI_MASTER_NAME"
                            ReadOnly="True" />
                        <asp:BoundField DataField="COMI_FILE_NAME" HeaderText="File Name" SortExpression="COMI_FILE_NAME"
                            ReadOnly="True" />
                        <asp:BoundField DataField="FILE_UPLOAD_TIME" HeaderText="Upload Time" SortExpression="FILE_UPLOAD_TIME"
                            ReadOnly="True" />
                        <asp:BoundField DataField="COMI_STRAT_DATE" HeaderText="Cycle Start Date" SortExpression="COMI_STRAT_DATE"
                            Visible="False" ReadOnly="True" />
                        <asp:BoundField DataField="COMI_END_DATE" HeaderText="Cycle End Date" SortExpression="COMI_END_DATE"
                            ReadOnly="True" />
                        <asp:TemplateField HeaderText="Loaded To Database" SortExpression="LOADED_TO_DB">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("LOADED_TO_DB") %>'>
                                    <asp:ListItem>Y</asp:ListItem>
                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" Enabled="False" SelectedValue='<%# Bind("LOADED_TO_DB") %>'>
                                    <asp:ListItem>Y</asp:ListItem>
                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOADED_SUMMARY" HeaderText="Loading Summary" SortExpression="LOADED_SUMMARY"
                            ReadOnly="True" />
                        <asp:BoundField DataField="BROAD_CAST_COUNT" HeaderText="Broadcast Count" SortExpression="BROAD_CAST_COUNT"
                            ReadOnly="True" />
                        <asp:CommandField InsertVisible="False" ShowEditButton="True" />
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="MediumBlue"></asp:Label>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" style="display: none">
                    Processing...
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/pleasewait.gif" />
                </div>
                <br />
                <table>
                    <tr>
                        <td style="background-color: royalblue" colspan="2">
                            <strong><span style="color: #ffffff; font-size: 11px;">Commission Cycle&nbsp; </span>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWait0" runat="server" BackColor="Transparent" Font-Bold="True"
                                Font-Size="12px" ForeColor="Black" Text="From Date" Width="70px"></asp:Label>&nbsp;&nbsp;
                            <cc1:GMDatePicker ID="txtFromDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                                CalendarOffsetY="25px" CalendarTheme="Green" CalendarWidth="200px" CallbackEventReference=""
                                Culture="English (United States)" DateFormat="dd-MMM-yyyy" DateString=''
                                EnableDropShadow="True" MaxDate="9999-12-31" MinDate="1900-01-01" NextMonthText="&gt;"
                                NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" Style="position: relative"
                                TextBoxWidth="90" ZIndex="1">
                                <CalendarTitleStyle BackColor="#FFFFC0" />
                            </cc1:GMDatePicker>
                            <%--<cc1:GMDatePicker ID="txtFromDate" runat="server" DateFormat="dd-MMM-yyyy" MinDate="1980-01-04">
                            </cc1:GMDatePicker>--%>
                        </td>
                        <%--<asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>--%>
                    </tr>
                    <tr>    
                        <td colspan="2">
                            <asp:Label ID="lblWait1" runat="server" BackColor="Transparent" Font-Bold="True"
                                Font-Size="12px" ForeColor="Black" Text="To Date" Width="70px"></asp:Label>&nbsp;&nbsp;
                            <cc1:GMDatePicker ID="txtToDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                                CalendarOffsetY="25px" CalendarTheme="Green" CalendarWidth="200px" CallbackEventReference=""
                                Culture="English (United States)" DateFormat="dd-MMM-yyyy" DateString=''
                                EnableDropShadow="True" MaxDate="9999-12-31" MinDate="1900-01-01" NextMonthText="&gt;"
                                NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" Style="position: relative"
                                TextBoxWidth="90" ZIndex="1">
                                <CalendarTitleStyle BackColor="#FFFFC0" />
                            </cc1:GMDatePicker>
                            <%--<cc1:GMDatePicker ID="txtToDate" runat="server" DateFormat="dd-MMM-yyyy" MinDate="1980-01-04">
                            </cc1:GMDatePicker>--%>
                        </td>
                        <%--<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" BackColor="Transparent" Font-Bold="True" Font-Size="12px"
                                ForeColor="Black" Text="Process Date" Width="79px" Height="16px"></asp:Label>
                            <cc1:GMDatePicker ID="txtProcessDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                                CalendarOffsetY="25px" CalendarTheme="Green" CalendarWidth="200px" CallbackEventReference=""
                                Culture="English (United States)" DateFormat="dd-MMM-yyyy" DateString='' EnableDropShadow="True" MaxDate="9999-12-31" MinDate="1900-01-01" NextMonthText="&gt;"
                                NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" Style="position: relative"
                                TextBoxWidth="90" ZIndex="1">
                                <CalendarTitleStyle BackColor="#FFFFC0" />
                            </cc1:GMDatePicker>
                            <%--<cc1:GMDatePicker ID="txtProcessDate" runat="server" DateFormat="dd-MMM-yyyy" MinDate="1980-01-04">
                            </cc1:GMDatePicker>--%>
                        </td>
                        <%--<asp:TextBox ID="txtProcessDate" runat="server"></asp:TextBox>--%>
                        <%--<td>
                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" OnClientClick="javascript:showWait()"
                                Text="Create Cycle" />
                        </td>--%>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" OnClientClick="javascript:showWait()"
                                Text="Create Cycle" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: royalblue" colspan="2">
                            <strong><span style="color: #ffffff; font-size: 11px;">Commission Import</span></strong>
                        </td>
                        <%--<td style="background-color: royalblue">
            &nbsp;</td>--%>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left" class="style1">
                            <asp:Label ID="lbltypeoption" runat="server" Text="Commission Type" BackColor="Transparent"
                                Font-Bold="True" Font-Size="10pt" ForeColor="Black" Height="16px"></asp:Label>
                            <asp:RadioButtonList ID="rdblbcrOption" runat="server" Height="16px" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rdblbcrOption_SelectedIndexChanged"
                                RepeatLayout="Flow" Font-Size="10pt">
                                <%-- <asp:ListItem>Compliance</asp:ListItem>
           <asp:ListItem>Refill /Usage</asp:ListItem>--%>
                                <asp:ListItem Value="CO" Selected="True">Compliance</asp:ListItem>
                                <asp:ListItem Value="RU">Refill / Usage </asp:ListItem>
                                <asp:ListItem Value="BO">Both </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <%--<tr>
          <td>
                          <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                              Text="Process Date From"></asp:Label>
                          &nbsp;<asp:TextBox ID="txtProcessFrom" runat="server"></asp:TextBox>
                      </td>
                      <td>
                          <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" 
                              Text="To Date"></asp:Label>
                          &nbsp;<asp:TextBox ID="txtProcessTo" runat="server"></asp:TextBox>
                      </td>
                  </tr>  --%>
                    <tr>
                        <td colspan="2">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label6" runat="server" BackColor="Transparent" Font-Bold="True" Font-Size="12px"
                                ForeColor="Black" Text="Setup ID" Width="55px"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtSetupID" runat="server" Width="155px"></asp:TextBox>
                        </td>
                        <caption>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlPendingFile" runat="server" DataSourceID="sdsPendingBFile"
                                        DataTextField="COMI_MASTER_NAME" DataValueField="COMI_MASTER_ID">
                                    </asp:DropDownList>
                                    <%--<asp:Button ID="btnCreateSub" runat="server" OnClick="btnCreateSub_Click" OnClientClick="javascript:ShowProgressBar()"--%>
                                    <asp:Button ID="btnCreateSub" runat="server" OnClick="btnCreateSub_Click" OnClientClick="javascript:showWait()"
                                        
                                        Text="Import Comission" />
                                </td>
                            </tr>
                            
                         
                         <tr>
                         <td colspan="2">
                         &nbsp;&nbsp;
                         </td>
                         
                         </tr>   
                            
                            
                            
                           <%-- <tr>
                                <td colspan="2">
                                    <iframe id="Iframe1" runat="server" frameborder="0" src="Blank.aspx" height="30"
                            width="300" scrolling="no" style="border-style: none"></iframe>
                                </td>
                            </tr>--%>
                            
                            
                            
                            
                            
                            
                            
                            <tr>
                                <td colspan="2" style="background-color: royalblue">
                                    <strong><span style="color: #ffffff; font-size: 11px;">Commission Broadcast&nbsp; </span>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" Text="Channel Type"></asp:Label>
                                    &nbsp;<asp:DropDownList ID="ddlChannelType" runat="server">
                                        <asp:ListItem Value="ALL">All</asp:ListItem>
                                        <asp:ListItem Value="DIS">Distributor</asp:ListItem>
                                        <asp:ListItem>RSP</asp:ListItem>
                                        <asp:ListItem Value="RTL">Retailer</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" Text="Amount Limit"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtAmountLimit" runat="server">50000</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlLoadedFile" runat="server" DataSourceID="sdsLoadedFile"
                                        DataTextField="COMI_MASTER_NAME" DataValueField="COMI_MASTER_ID">
                                    </asp:DropDownList>
                                    &nbsp;<asp:Button ID="btnBroadcast" runat="server" OnClick="btnBroadcast_Click" Text="Broadcast" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    &nbsp;
                                </td>
                            </tr>
                        </caption>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
