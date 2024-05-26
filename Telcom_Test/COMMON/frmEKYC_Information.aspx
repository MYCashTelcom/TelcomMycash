<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEKYC_Information.aspx.cs" Inherits="COMMON_frmEKYC_Information" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI"  %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resubmit Topup Request</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />

    <style type="text/css">
        .inline {
            display: inline-block;
            border: 1px solid #c1c1c1;
            margin: 10px;
        }

        .font_Color {
            color: White;
        }

        .mGridModify {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            text-align: left;
        }

            .mGridModify td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                font-size: 11px;
            }

            .mGridModify th {
                padding: 4px 2px;
                color: #fff;
                background: url(grd_head1.png) activecaption repeat-x 50% top;
                border-left: solid 0px #525252;
                font-size: 11px;
            }
            /*.mGrid .alt { background: #fcfcfc url(grd_alt.png) repeat-x top; }*/
            .mGridModify .pgr {
                background-color: activecaption;
                background-attachment: scroll;
                background-repeat: repeat-x;
                background-position: 50% top;
            }

                .mGridModify .pgr table {
                    margin: 5px 0;
                }

                .mGridModify .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                    font-size: 11px;
                }

                .mGridModify .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGridModify .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
        /*tr:hover {
  background-color: #ffa;
}
tr.alt:hover {
  background-color: #ffa!important;
}*/
        .Visible_Flase {
            display: none;
        }

        .Top_Panel {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            font-size: 12px;
            color: White;
        }

        .View_Panel {
            background-color: powderblue;
        }

        .Inser_Panel {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }

        #dtpRegFDate_showCalendar, #dtpRegTDate_showCalendar {
            color: black;
        }
    </style>

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <asp:SqlDataSource ID="sdsRequest" runat="server"
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                    SelectCommand="SELECT SERIAL_NO, DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME,DKI.CLINT_FATHER_NAME,DKI.CLINT_MOTHER_NAME,CLIENT_PRE_ADDRESS,REGISTRATION_DATE,
                        DKI.AGENT_ACCNT_NO, (SELECT CLINT_NAME FROM ACCOUNT_LIST ALS , CLIENT_LIST CLS 
                        WHERE ALS.ACCNT_NO = DKI.AGENT_ACCNT_NO AND ALS.CLINT_ID = CLS.CLINT_ID ) AGENT_NAME ,DKI.IS_UPDATE, DKI.IS_PROCESSING,CL.CLINT_NAME AS CN,CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID, REMARKS
                        FROM DIGITAL_KYC_INFO DKI, ACCOUNT_LIST AL, CLIENT_LIST CL
                        WHERE DKI.AGENT_ACCNT_NO = AL.ACCNT_NO AND CL.CLINT_ID = AL.CLINT_ID AND IS_UPDATE = 'N' ORDER BY SERIAL_NO;"
                    UpdateCommand=""><%--UPDATE &quot;TOPUP_TRANSACTION&quot; SET &quot;SUBSCRIBER_TYPE&quot; = :SUBSCRIBER_TYPE 
                        WHERE &quot;TOPUP_TRAN_ID&quot; = :TOPUP_TRAN_ID--%>
                </asp:SqlDataSource>
                <asp:Panel ID="pnlTop" runat="server">
                    <table class="Top_Panel">
                        <tr>
                            <td>NID:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNIDSearch"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" ID="btnSearchNID" Text="Search" OnClick="btnSearchMobile_Click" /></td>
                            <td>Mobile:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMobileSearch"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" ID="btnSearchMobile" Text="Search" OnClick="btnSearchMobile_Click" /></td>
                            <td>Category:<asp:DropDownList runat="server" ID="ddlSearchOption">
                                <asp:ListItem Value="all">All</asp:ListItem>
                                <asp:ListItem Value="nonverified" >Pending</asp:ListItem>
                                <asp:ListItem Value="verified"  Selected="True">Verified</asp:ListItem>
                                <asp:ListItem Value="cancel">Cancel</asp:ListItem>
                                <asp:ListItem Value="onproc">On Process</asp:ListItem>
                            </asp:DropDownList>
                            </td>

                            <td>From:<cc1:GMDatePicker ID="dtpRegFDate" runat="server" CalendarTheme="Silver"
                                DateFormat="dd-MMM-yyyy" DateString=''
                                MaxDate="9999-12-31" MinDate="" NextMonthText="&gt;" NoneButtonText="None" ShowNoneButton="True"
                                Style="position: relative" TextBoxWidth="80" ZIndex="1">
                                <CalendarTitleStyle BackColor="#FFFFC0" Font-Size="X-Small" />
                            </cc1:GMDatePicker>
                            </td>
                            <td>To:<cc1:GMDatePicker ID="dtpRegTDate" runat="server" CalendarTheme="Silver"
                                DateFormat="dd-MMM-yyyy" DateString='' EnableDropShadow="True"
                                MaxDate="9999-12-31" MinDate="" NextMonthText="&gt;" NoneButtonText="None" ShowNoneButton="True"
                                ShowTodayButton="True" Style="position: relative" TextBoxWidth="80" ZIndex="1">
                                <CalendarTitleStyle BackColor="#FFFFC0" Font-Size="X-Small" />
                            </cc1:GMDatePicker>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnExport" Text="Download" OnClick="btnExport_Click" /></td>

                            <td align="right">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--<asp:Panel ID="Panel1" runat="server" CssClass="View_Panel">
      Source Wallet
        <asp:TextBox ID="txtRequestParty" runat="server" Width="112px"></asp:TextBox>
         Subscriber Mobile No
        <asp:TextBox ID="txtSubscriberNo" runat="server" Width="112px"></asp:TextBox>
         Operator Type
        <asp:TextBox ID="txtOperatorType" runat="server" Width="50px"></asp:TextBox>
       <asp:Button  ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />
      </asp:Panel>   --%>
                <%--CssClass="mGrid"--%>
                <asp:GridView ID="gdvRequest" runat="server" AllowPaging="True"
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                    BorderColor="#E0E0E0" BorderStyle="None" CssClass="mGridModify"
                    DataKeyNames="DIGITAL_KYC_ID" DataSourceID="sdsRequest"
                    OnRowDataBound="gdvRequest_RowDataBound"                    
                    OnPageIndexChanging="gdvRequest_PageIndexChanging"                   
                    PagerStyle-CssClass="pgr" PageSize="13" Width="1000px">
                    <Columns>
                        <asp:BoundField DataField="SERIAL_NO" HeaderText="Serial No" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="SERIAL_NO" />
                                               
                        <asp:BoundField DataField="DIGITAL_KYC_ID" HeaderText="Trace ID"
                            ReadOnly="True" SortExpression="DIGITAL_KYC_ID" Visible="false" />
                        <asp:BoundField DataField="CLINT_MOBILE" HeaderText="Customer Mobile/ Wallet ID" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="CLINT_MOBILE" />
                        <asp:BoundField DataField="CLINT_NAME" HeaderText="Customer Name" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="CLINT_NAME" />
                        <asp:BoundField DataField="CLINT_FATHER_NAME"
                            HeaderText="Customer Father’s Name" ReadOnly="True"
                            SortExpression="CLINT_FATHER_NAME" Visible="false" />
                        <asp:BoundField DataField="CLINT_MOTHER_NAME"
                            HeaderText="Customer Mother’s Name" ReadOnly="True"
                            SortExpression="CLINT_MOTHER_NAME" Visible="false" />
                        <asp:BoundField DataField="CLIENT_PRE_ADDRESS"
                            HeaderText="Customer Address" ReadOnly="True"
                            SortExpression="CLIENT_PRE_ADDRESS" Visible="false" />
                        <asp:BoundField DataField="REGISTRATION_DATE"
                            HeaderText="Registration Date And Time" ReadOnly="True"
                            SortExpression="REGISTRATION_DATE" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AGENT_ACCNT_NO" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Agent Number" ReadOnly="True"
                            SortExpression="AGENT_ACCNT_NO" />
                        <asp:BoundField DataField="AGENT_NAME" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Agent Name" ReadOnly="True"
                            SortExpression="AGENT_NAME" Visible="true" />
                        <asp:BoundField DataField="CN"
                            HeaderText="User Name" ReadOnly="True"
                            SortExpression="CN" Visible="false" />
                        <asp:BoundField DataField="CLINT_ADDRESS1"
                            HeaderText="Agent address" ReadOnly="True"
                            SortExpression="CLINT_ADDRESS1" Visible="false" />
                        <asp:TemplateField HeaderText="ID Type"
                            SortExpression="IDENTITY_TYPE" Visible="false">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true"
                                    DataField="IDENTITY_TYPE" SelectedValue='<%# Bind("IDENTITY_TYPE") %>'>
                                    <asp:ListItem Value="0">Prepaid</asp:ListItem>
                                    <asp:ListItem Value="1">Postpaid</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CLINT_NATIONAL_ID" HeaderText="National ID" ItemStyle-Width="85px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="CLINT_NATIONAL_ID" />

                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Refresh" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" Visible="false" />
                        <asp:TemplateField ItemStyle-Width="75">
                            <ItemTemplate>
                                <asp:Button ID="btnShowReport" runat="server" CssClass="PapeReport" OnClick="btnShowReport_Click" Text="Show Report" /><%--OnClientClick="return confirm('Do you want to success this topup ? ');"--%>
                                <asp:Button ID="btnVerifiedPDF" runat="server" OnClick="btnVerifiedPDF_Click" Text="Report" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="REMARKS"
                            HeaderText="Remarks" ReadOnly="True" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center"
                            SortExpression="REMARKS" />
                        <asp:BoundField DataField="IS_UPDATE" ItemStyle-CssClass="Visible_Flase" HeaderStyle-CssClass="Visible_Flase" />
                        <asp:BoundField DataField="IS_PROCESSING" ItemStyle-CssClass="Visible_Flase" HeaderStyle-CssClass="Visible_Flase" />
                        <asp:BoundField DataField="VERIFIED_BY"
                            HeaderText="Verified By" ReadOnly="True" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center" Visible="false"
                            SortExpression="VERIFIED_BY" />
                        <%--<asp:TemplateField>
                      <ItemTemplate>
                          <asp:Button ID="btnVerify" runat="server" onclick="btnVerify_Click" OnClientClick="return confirm('Are you sure all information are right? ');" Text="Verify" Width="76px" />
                      </ItemTemplate>
                  </asp:TemplateField>--%>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerStyle CssClass="pgr" />

                    <AlternatingRowStyle CssClass="alt" />

                </asp:GridView>
                <div class="inline">
                    <asp:Label runat="server" ID="lblCountMsg"></asp:Label></div>
                <div class="inline">Page:<asp:TextBox runat="server" ID="txtPageSearch" Width="40"></asp:TextBox><asp:Button runat="server" ID="btnPageGo" Text="GO" OnClick="btnPageGo_Click" /></div>
            </ContentTemplate>
            <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />  
        </Triggers>--%>
        </asp:UpdatePanel>
    </form>
</body>
</html>
