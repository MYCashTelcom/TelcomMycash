<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_PHOTO_UPLOAD.aspx.cs"
    Inherits="CRM_CRM_PHOTO_UPLOAD" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Photo Signature Upload</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            height: 30px;
            width: 1px;
        }
        .style2
        {
            width: 1px;
        }
        .Top_Panel
        {
            background-color: royalblue;
            height: 28px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            background-color: powderblue;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
        .style3
        {
            width: 300px;
        }
        .style4
        {
            width: 179px;
        }
        .style5
        {
            width: 323px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:SqlDataSource ID="sdsClientInfo" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT * FROM  ACCOUNT_LIST  AL,CLIENT_LIST CL WHERE  AL.CLINT_ID=CL.CLINT_ID "
        UpdateCommand='UPDATE "ACCOUNT_LIST" SET "CLINT_ID" = :CLINT_ID, "ACCNT_NO" = :ACCNT_NO, "ACCNT_PIN" = :ACCNT_PIN, "ACCNT_PIN_POLICY" = :ACCNT_PIN_POLICY, "ACCNT_CHARGE_TYPE" = :ACCNT_CHARGE_TYPE, "ACCNT_STATE" = :ACCNT_STATE, "ACCNT_BALANCE" = :ACCNT_BALANCE, "ACCNT_EXPIRY_DATE" = :ACCNT_EXPIRY_DATE, "ACCNT_BONUS_ON_NET" = :ACCNT_BONUS_ON_NET, "ACCNT_BONUS_OFF_NET" = :ACCNT_BONUS_OFF_NET, "ACCNT_BONUS_INT" = :ACCNT_BONUS_INT, "SERVICE_PKG_ID" = :SERVICE_PKG_ID, "ACCNT_MSISDN" = :ACCNT_MSISDN, "ACCNT_PIN2" = :ACCNT_PIN2, "LANGUAGE_ID" = :LANGUAGE_ID, "RETAILER_NAME" = :RETAILER_NAME, "DISTRIBUTORCODE" = :DISTRIBUTORCODE, "DISTRIBUTORNAME" = :DISTRIBUTORNAME, "DISTRIBUTORERSNUMBER" = :DISTRIBUTORERSNUMBER, "DSRCODE" = :DSRCODE, "DSRNAME" = :DSRNAME, "RSPCODE" = :RSPCODE, "RSPNAME" = :RSPNAME, "RSPEASYLOADNUMBER" = :RSPEASYLOADNUMBER, "ACCNT_RANK_ID" = :ACCNT_RANK_ID, "ACCNT_TYPE_ID" = :ACCNT_TYPE_ID WHERE "ACCNT_ID" = :ACCNT_ID'
        DeleteCommand='DELETE FROM "ACCOUNT_LIST" WHERE "ACCNT_ID" = :ACCNT_ID' InsertCommand='INSERT INTO "ACCOUNT_LIST" ("CLINT_ID", "ACCNT_ID", "ACCNT_NO", "ACCNT_PIN", "ACCNT_PIN_POLICY", "ACCNT_CHARGE_TYPE", "ACCNT_STATE", "ACCNT_BALANCE", "ACCNT_EXPIRY_DATE", "ACCNT_BONUS_ON_NET", "ACCNT_BONUS_OFF_NET", "ACCNT_BONUS_INT", "SERVICE_PKG_ID", "ACCNT_MSISDN", "ACCNT_PIN2", "LANGUAGE_ID", "RETAILER_NAME", "DISTRIBUTORCODE", "DISTRIBUTORNAME", "DISTRIBUTORERSNUMBER", "DSRCODE", "DSRNAME", "RSPCODE", "RSPNAME", "RSPEASYLOADNUMBER", "ACCNT_RANK_ID", "ACCNT_TYPE_ID") VALUES (:CLINT_ID, :ACCNT_ID, :ACCNT_NO, :ACCNT_PIN, :ACCNT_PIN_POLICY, :ACCNT_CHARGE_TYPE, :ACCNT_STATE, :ACCNT_BALANCE, :ACCNT_EXPIRY_DATE, :ACCNT_BONUS_ON_NET, :ACCNT_BONUS_OFF_NET, :ACCNT_BONUS_INT, :SERVICE_PKG_ID, :ACCNT_MSISDN, :ACCNT_PIN2, :LANGUAGE_ID, :RETAILER_NAME, :DISTRIBUTORCODE, :DISTRIBUTORNAME, :DISTRIBUTORERSNUMBER, :DSRCODE, :DSRNAME, :RSPCODE, :RSPNAME, :RSPEASYLOADNUMBER, :ACCNT_RANK_ID, :ACCNT_TYPE_ID)'>
        <UpdateParameters>
            <asp:Parameter Name="CLINT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_NO" Type="String" />
            <asp:Parameter Name="ACCNT_PIN" Type="String" />
            <asp:Parameter Name="ACCNT_PIN_POLICY" Type="String" />
            <asp:Parameter Name="ACCNT_CHARGE_TYPE" Type="String" />
            <asp:Parameter Name="ACCNT_STATE" Type="String" />
            <asp:Parameter Name="ACCNT_BALANCE" Type="Decimal" />
            <asp:Parameter Name="ACCNT_EXPIRY_DATE" Type="DateTime" />
            <asp:Parameter Name="ACCNT_BONUS_ON_NET" Type="Decimal" />
            <asp:Parameter Name="ACCNT_BONUS_OFF_NET" Type="Decimal" />
            <asp:Parameter Name="ACCNT_BONUS_INT" Type="Decimal" />
            <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
            <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
            <asp:Parameter Name="ACCNT_PIN2" Type="String" />
            <asp:Parameter Name="LANGUAGE_ID" Type="String" />
            <asp:Parameter Name="RETAILER_NAME" Type="String" />
            <asp:Parameter Name="DISTRIBUTORCODE" Type="String" />
            <asp:Parameter Name="DISTRIBUTORNAME" Type="String" />
            <asp:Parameter Name="DISTRIBUTORERSNUMBER" Type="String" />
            <asp:Parameter Name="DSRCODE" Type="String" />
            <asp:Parameter Name="DSRNAME" Type="String" />
            <asp:Parameter Name="RSPCODE" Type="String" />
            <asp:Parameter Name="RSPNAME" Type="String" />
            <asp:Parameter Name="RSPEASYLOADNUMBER" Type="String" />
            <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
            <asp:Parameter Name="ACCNT_TYPE_ID" Type="String" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="ACCNT_ID" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CLINT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_ID" Type="String" />
            <asp:Parameter Name="ACCNT_NO" Type="String" />
            <asp:Parameter Name="ACCNT_PIN" Type="String" />
            <asp:Parameter Name="ACCNT_PIN_POLICY" Type="String" />
            <asp:Parameter Name="ACCNT_CHARGE_TYPE" Type="String" />
            <asp:Parameter Name="ACCNT_STATE" Type="String" />
            <asp:Parameter Name="ACCNT_BALANCE" Type="Decimal" />
            <asp:Parameter Name="ACCNT_EXPIRY_DATE" Type="DateTime" />
            <asp:Parameter Name="ACCNT_BONUS_ON_NET" Type="Decimal" />
            <asp:Parameter Name="ACCNT_BONUS_OFF_NET" Type="Decimal" />
            <asp:Parameter Name="ACCNT_BONUS_INT" Type="Decimal" />
            <asp:Parameter Name="SERVICE_PKG_ID" Type="String" />
            <asp:Parameter Name="ACCNT_MSISDN" Type="String" />
            <asp:Parameter Name="ACCNT_PIN2" Type="String" />
            <asp:Parameter Name="LANGUAGE_ID" Type="String" />
            <asp:Parameter Name="RETAILER_NAME" Type="String" />
            <asp:Parameter Name="DISTRIBUTORCODE" Type="String" />
            <asp:Parameter Name="DISTRIBUTORNAME" Type="String" />
            <asp:Parameter Name="DISTRIBUTORERSNUMBER" Type="String" />
            <asp:Parameter Name="DSRCODE" Type="String" />
            <asp:Parameter Name="DSRNAME" Type="String" />
            <asp:Parameter Name="RSPCODE" Type="String" />
            <asp:Parameter Name="RSPNAME" Type="String" />
            <asp:Parameter Name="RSPEASYLOADNUMBER" Type="String" />
            <asp:Parameter Name="ACCNT_RANK_ID" Type="String" />
            <asp:Parameter Name="ACCNT_TYPE_ID" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
        <table width="100%">
            <tr>
                <td class="style3">
                    Manage Photo and Signature
                </td>
                <td class="style5">
                </td>
                <td class="style4">
                </td>
                <td align="left">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td align="left">
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                        <ProgressTemplate>
                            <img alt="Loading" src="../resources/images/loading.gif" />&nbsp;&nbsp;
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlView" runat="server" CssClass="View_Panel">
        &nbsp; Search By Wallet ID
        <asp:TextBox ID="txtAccountNo" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        &nbsp;
        <%-- Client List&nbsp;
           <asp:DropDownList ID="ddlClient_list" runat="server" AutoPostBack="True" 
                DataSourceID="sdsClientInfo" DataTextField="ACCNT_MSISDN" DataValueField="CLINT_ID"
                OnSelectedIndexChanged="ddlClient_list_SelectedIndexChanged" Width="200px">
           </asp:DropDownList>    
            <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click"></asp:Button>--%>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <table width="100%">
            <tr>
                <td valign="top">
                    <table border="0">
                        <tr>
                            <td style="width: 3px; height: 30px;" align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Client Picture " ForeColor="Black"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Client Signature:" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label runat="server" ID="llbCustKyc" Font-Bold="True" Text="Customer KYC Form "
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3px; height: 30px;" align="right">
                                &nbsp;
                            </td>
                            <td style="width: 2px; height: 30px;" align="right">
                                <asp:Image ID="imgClientPic" runat="server" BackColor="#E0E0E0" BorderColor="Gray"
                                    Height="154px" Width="217px" />
                            </td>
                            <td style="width: 82px; height: 30px;" align="left">
                                <asp:Image ID="imgClientSig" runat="server" BackColor="#E0E0E0" BorderColor="Gray"
                                    Height="154px" Width="217px" />
                            </td>
                            <td class="style1">
                                <asp:Image ID="imgCustomerKyc" runat="server" BackColor="#E0E0E0" BorderColor="Gray"
                                    Height="154px" Width="217px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3px; height: 30px" align="right">
                                &nbsp;
                            </td>
                            <td style="width: 2px; height: 30px">
                                <asp:FileUpload ID="fluClientPic" runat="server" />
                            </td>
                            <td style="width: 82px; height: 30px" align="left">
                                <asp:FileUpload ID="fluClientSig" runat="server" />
                            </td>
                            <td style="width: 82px; height: 30px" align="left">
                                <asp:FileUpload ID="fluCustomerKyc" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="   Save   " />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                                    OnClientCancel="cancelClick" TargetControlID="btnSave">
                                </ajaxToolkit:ConfirmButtonExtender>
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ButtonCancel" OkControlID="ButtonOk" TargetControlID="btnSave"
                                    PopupControlID="pnlPopUp">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlPopUp" runat="server" Style="display: none; width: 300px; height: 165px;
                                    background-color: White; border-width: 1px; border-color: Silver; border-style: solid;
                                    padding: 1px;">
                                    <div style="height: 95px;">
                                        <br />
                                        &nbsp;<br />
                                        &nbsp; Are you sure you want to save?
                                        <br />
                                        &nbsp;<br />
                                        &nbsp;
                                    </div>
                                    <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                        <br />
                                        &nbsp;
                                        <asp:Button ID="ButtonOk" runat="server" Text="  Yes  " />
                                        <asp:Button ID="ButtonCancel" runat="server" Text=" Cancel " />
                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btnChangePic" runat="server" Text="Update" OnClick="btnChangePic_Click" />
                            </td>
                            <td colspan="2" align="center">
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                                    OnClientCancel="cancelClick" TargetControlID="btnChangePic">
                                </ajaxToolkit:ConfirmButtonExtender>
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnCancel" OkControlID="btnOk" TargetControlID="btnChangePic"
                                    PopupControlID="pnlPopUpUpdate">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlPopUpUpdate" runat="server" Style="display: none; width: 300px;
                                    height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                    border-style: solid; padding: 1px;">
                                    <div style="height: 95px;">
                                        <br />
                                        &nbsp;<br />
                                        &nbsp; Are you sure you want to save?
                                        <br />
                                        &nbsp;<br />
                                        &nbsp;
                                    </div>
                                    <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                        <br />
                                        &nbsp;
                                        <asp:Button ID="btnOk" runat="server" Text="  Yes  " />
                                        <asp:Button ID="btnCancel" runat="server" Text=" Cancel " />
                                    </div>
                                </asp:Panel>
                                &nbsp;
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--</ContentTemplate>
     </asp:UpdatePanel>--%>
    </form>
</body>
</html>
