<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmWellcome.aspx.cs" Inherits="Forms_frmWellcome" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NITL Telco 1.0.1</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
    <div style="background-color: skyblue; text-align: left; background-image: url(../Images/Header.JPG); vertical-align: text-bottom;">
        <strong>
            <br />
            <span style="font-size: 24pt; color: white;"><span style="font-size: 28pt; font-family: Constantia;">&nbsp;<asp:Image ID="Image1" runat="server" Height="25px" ImageUrl="~/Images/spinner3-bluey.gif" />NITL Telco <span style="font-size: 32pt">1.0.1</span></span><br />
            </span>
            <br />
        </strong>
    </div>
    <div style="background-color: whitesmoke;">
        <table border="1" cellspacing="0" width="100%" style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; table-layout: auto; border-collapse: collapse; height: 440px; font-size: 12pt;" frame="box">
            <tr>
                <td align=left valign=top style="width: 202px; height: 440px;">
                    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <Nodes>
                            <asp:TreeNode Text="NITL Telco Services" Value="nitlvp">
                                <asp:TreeNode Text="System" Value="sys" Expanded="False">
                                    <asp:TreeNode Text="System User Group"
                                        Value="sysug"></asp:TreeNode>
                                    <asp:TreeNode Text="System Users" Value="sysu"></asp:TreeNode>
                                    <asp:TreeNode Text="Change Pass" Value="chgpass"></asp:TreeNode>
                                    <asp:TreeNode Text="System Audit" Value="syslq"></asp:TreeNode>
                                    <asp:TreeNode Text="System Alarm" Value="sysErrLog"></asp:TreeNode>
                                    <asp:TreeNode Text="Background Process" Value="backgp"></asp:TreeNode>
                                    <asp:TreeNode Text="Online SQL Window" Value="olSQL_Window"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Service &amp; Package" Value="pkgm" Expanded="False">
                                    <asp:TreeNode Text="Manage Service Type"
                                        Value="mngSrvType"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Services" Value="mngService"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Service Rate"
                                        Value="mngSrvRate"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Packages" Value="mngPackage"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Secret Questions" Value="mngSecQus"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Clients &amp; Account" Value="acm" Expanded="False">
                                    <asp:TreeNode Text="Manage Client Register" Value="acchld"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Client Account" Value="acqr"></asp:TreeNode>
                                    <asp:TreeNode Text="Account Activation" Value="acActive"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Account Refill" Value="acrf"></asp:TreeNode>
                                    <asp:TreeNode Text="Query Account History" Value="qryAccHistory"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Bulk Account" Value="mngBulkAcc"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage PIN" Value="mngPIN"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Group Account" Value="msgm" Expanded="False">
                                    <asp:TreeNode Text="Manage Group " Value="mngAccntGroup"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Group Member" Value="mngAccntGroupMember"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Expanded="False" Text="Manage Messages" Value="Manage Gateway Message">
                                    <asp:TreeNode Text="Manage Quiz" Value="mng_quiz"></asp:TreeNode>
                                    <asp:TreeNode Text="Broadcast Message" Value="sndBrdMessage"></asp:TreeNode>
                                    <asp:TreeNode Text="Group Message" Value="sbtGrpMsg"></asp:TreeNode>
                                    <asp:TreeNode Text="Query Submission Status" Value="qrymsg"></asp:TreeNode>
                                    <asp:TreeNode Text="Query Invitation Status" Value="qryInvStatus"></asp:TreeNode>
                                    <asp:TreeNode Text="Query Quiz Result" Value="qryQuzResult"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Content" Value="Manage Content" Expanded="False">
                                    <asp:TreeNode Text="Content Type" Value="cnttype"></asp:TreeNode>
                                    <asp:TreeNode Text="Content Group" Value="cntgrp"></asp:TreeNode>
                                    <asp:TreeNode Text="Content List" Value="cntlist"></asp:TreeNode>
                                    <asp:TreeNode Text="Content Text Edit" Value="cnttxtedit"></asp:TreeNode>
                                    <asp:TreeNode Text="Content File Upload" Value="cntfilupld"></asp:TreeNode>
                                    <asp:TreeNode Text="Content Used History" Value="cntusdhis"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Voucher" Value="vchrm" Expanded="False">
                                    <asp:TreeNode Text="Voucher Denomination"
                                        Value="vouDenomination"></asp:TreeNode>
                                    <asp:TreeNode Text="Voucher Generation" Value="vouGeneration"></asp:TreeNode>
                                    <asp:TreeNode Text="Voucher Activation" Value="vouActivation"></asp:TreeNode>
                                    <asp:TreeNode Text="Voucher State Query" Value="vouState"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Mobile Banking" Value="mngMB" Expanded="False">
                                    <asp:TreeNode Text="Manage Banks" Value="mngBankList"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Client Bank Account" Value="mngCBAccount"></asp:TreeNode>
                                    <asp:TreeNode Text="Bank Transaction History" Value="bnkTranHis"></asp:TreeNode>
                                    <asp:TreeNode Text="Bank Settlement" Value="bnkSettlement"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Mobile cash" Value="Manage Mobile cash" Expanded="False">
                                    <asp:TreeNode Text="Mcash Setup" Value="Mcash Setup"></asp:TreeNode>
                                    <asp:TreeNode Text="MCash Dealer" Value="MCash Dealer"></asp:TreeNode>
                                    <asp:TreeNode Text="MCash Consumer" Value="MCash Consumer"></asp:TreeNode>
                                    <asp:TreeNode Text="MCash Transaction Inquery" Value="MCash Transaction Inquery"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Mobile Zone" Value="mmzone" Expanded="False">
                                    <asp:TreeNode Text="Coverage Area" Value="zoarea"></asp:TreeNode>
                                    <asp:TreeNode Text="Cell Sites" Value="clsts"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Zone" Value="mngzone"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Zone Cell" Value="mngzonecell"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage CB Message"
                                        Value="mngcbm"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Zone Packages" Value="mngznpkg"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage APSNG" Value="mngapsng" Expanded="False">
                                    <asp:TreeNode Text="Manage Promotion Services" Value="mngPromoSrv" Expanded="False">
                                        <asp:TreeNode Text="Manage IN Packages"
                                            Value="mngIN_Pkg"></asp:TreeNode>
                                        <asp:TreeNode Text="Manage Rent Services" Value="mngRentSrv">
                                            <asp:TreeNode Text="Package Change Rule" Value="pkgChngRule"></asp:TreeNode>
                                            <asp:TreeNode Text="Package Change Bonus" Value="pkgChngBonus"></asp:TreeNode>
                                            <asp:TreeNode Text="Package Change Message" Value="pkgChngMsg"></asp:TreeNode>
                                            <asp:TreeNode Text="Package Change History" Value="pkgChngHistory"></asp:TreeNode>
                                        </asp:TreeNode>
                                        <asp:TreeNode Text="Manage Consumption Services" Value="mngConsumSrv"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Manage EVC" Value="mngEvc" Expanded="False">
                                        <asp:TreeNode Text="Manage Dealer Area" Value="mngDlrArea"></asp:TreeNode>
                                        <asp:TreeNode Text="Manage Dealer Hierchy" Value="mngDlrHrchy"></asp:TreeNode>
                                        <asp:TreeNode Text="Manage Dealaer Transactions" Value="mngDlrTrnsac"></asp:TreeNode>
                                        <asp:TreeNode Text="Dealer Transactio History" Value="dlrTranHstry"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Mange Postpaid Bills" Value="mngPostBills"></asp:TreeNode>
                                    <asp:TreeNode Text="Manage Dash Board" Value="mngDashBoard"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Manage Reports" Value="mngReport">
                                    <asp:TreeNode Text="Report list" Value="rptList"></asp:TreeNode>
                                </asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                    </asp:TreeView>
                </td>
                <td align=left valign=top style="height: 550px; background-color: lightgrey;" >
                    <IFRAME id="Iframe1" runat="server" scrolling="auto" width="100%" height="100%" frameborder="0" src="frnWellcomeScreen.aspx"></IFRAME></td>
            </tr>
        
        </table>
    </div>
    <div style="background-color: lightsteelblue; text-align: center;">
        <span style="font-size: 10pt">© 2007-2009 Nodisha Infotech Ltd. All rights reserved.</span></div>
        </contenttemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
