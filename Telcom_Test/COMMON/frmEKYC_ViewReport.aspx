<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEKYC_ViewReport.aspx.cs" Inherits="COMMON_frmEKYC_ViewReport" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI"  %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resubmit Topup Request</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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

        input[type='text'], select {
            width: 225px;
        }

        table, tr, td {
            padding: 2px;
        }
        .disable_class{
            cursor: default; background-color: rgb(235, 235, 228); border: 2px solid rgb(235, 235, 228); color: rgb(84,84,84);
        }
    </style>
	
	<script type="text/javascript">
	    function UserDeleteConfirmation() {
	        return confirm("Are you sure you want to delete this user?");
	    }
    </script>

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlTop" runat="server">
                    <table class="Top_Panel" style="width: 100%;">
                        <tr>
                            <td>Verified Digital KYC</td>
                            <td></td>
                            <td align="right">
                                <asp:Button ID="btnReturn" runat="server" Text="  Return to EKYC Varified"  OnClick="btnReturn_Click" Visible="true" />

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
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblMsg" runat="server" Style="color: red;"></asp:Label></td>
                                </tr>
                                <tr>
                                    
                                    <td><asp:Label runat="server" ID="lblSerialNumber"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Customer Name</td>
                                    <td>
                                        <asp:TextBox ID="txtClientID" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtCName" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>
                                    <td>Mobile Number</td>
                                    <td>
                                        <asp:TextBox ID="txtCMobileNo" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Father's Name</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblFatherHusband" runat="server" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F" Selected="True">Father&#39;s Name</asp:ListItem>
                                            <asp:ListItem Value="H">Husband Name</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="txtCFNameOrHusband" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>
                                    <td>Customer Mother's Name</td>
                                    <td>
                                        <asp:TextBox ID="txtCMName" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td>Present Address</td>
                                    <td>
                                        <asp:TextBox TextMode="multiline" Columns="26" Rows="4" ID="txtCPreAddress" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)"></asp:TextBox></td>
                                    <td>Permanent Address</td>
                                    <td>
                                        <asp:TextBox TextMode="multiline" Columns="26" Rows="4" ID="txtCPermaAddress" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Present District</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDistrictPre" runat="server" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlDistrictPre_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Permanent District</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDistrictPer" runat="server" AutoPostBack="true"                                             OnSelectedIndexChanged="ddlDistrictPer_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<span style="color: Red;">*</span>--%>
                                        <asp:RequiredFieldValidator ID="rfvDistrict" runat="server"
                                            ControlToValidate="ddlDistrictPer" ErrorMessage="Please select a district"
                                            ValidationGroup="Update" Display="Dynamic">
                                        </asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td>Present Thana</td>
                                    <td>
                                        <asp:DropDownList ID="ddlThanaPre" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>Permanent Thana</td>
                                    <td>
                                        <asp:DropDownList ID="ddlThanaPer" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvThanaPer" runat="server"
                                            ControlToValidate="ddlThanaPer" ErrorMessage="Please select a thana"
                                            ValidationGroup="Update" Display="Dynamic">
                                        </asp:RequiredFieldValidator></td>
                                </tr>

                                <tr>
                                    <td>Urban/Rural</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblUrbanOrRural" runat="server" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="U" Selected="True">Urban</asp:ListItem>
                                            <asp:ListItem Value="R">Rural</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td>Gender</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoCGender" runat="server" Enabled="true" AutoPostBack="true" RepeatDirection="Horizontal" ReadOnly="false">
                                            <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                            <asp:ListItem Value="O">Others</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Identity Type</td>
                                    <td>
                                        <asp:DropDownList ID="ddlIdenType" runat="server" AutoPostBack="true" Enabled="false" 
                                            Style="cursor: default; background-color: rgb(235, 235, 228); border: 2px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true">
                                        </asp:DropDownList>
                                    </td>

                                    <td>National ID No</td>
                                    <td>
                                        <asp:TextBox ID="txtCNationalId" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Purpose of Transaction</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPurOfTran" runat="server" AppendDataBoundItems="true" Enabled="false"
                                            DataTextField="PUR_OF_TRAN"
                                            DataValueField="PUR_OF_TRAN" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true">
                                            <asp:ListItem Value="P" Text="Personal"></asp:ListItem>
                                            <asp:ListItem Value="B" Text="Business"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="Self"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Date of Birth</td>
                                    <td>
                                        <asp:TextBox ID="txtCDOB" runat="server" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nominee Name</td>
                                    <td>
                                        <asp:TextBox ID="txtNomineeName" runat="server"></asp:TextBox></td>
                                    <td>Nominee Address</td>
                                    <td>
                                        <asp:TextBox ID="txtNomineeAddress" runat="server" TextMode="multiline" Columns="26" Rows="2" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Relationship</td>
                                    <td>
                                        <asp:DropDownList ID="ddlRelationship" runat="server" AutoPostBack="true"
                                             Style="cursor: default; background-color: rgb(235, 235, 228); border: 2px solid rgb(235, 235, 228); color: rgb(84,84,84)">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtRelationshipId" runat="server"></asp:TextBox>--%></td>
                                    <td>Nominee Percentage</td>
                                    <td>
                                        <asp:TextBox ID="txtNomineePercentage" runat="server" Style="cursor: default; background-color: rgb(235, 235, 228); border: 1px solid rgb(235, 235, 228); color: rgb(84,84,84)" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Occuption</td>
                                    <td>
                                        <asp:DropDownList ID="ddlOccupation" runat="server" AutoPostBack="true" Enabled="false"
                                             Style="cursor: default; background-color: rgb(235, 235, 228); border: 2px solid rgb(235, 235, 228); color: rgb(84,84,84)">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Verification Code</td>
                                    <td>
                                        <asp:TextBox ID="txtVerificationCode" runat="server" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Customer Unique Key</td>
                                    <td>
                                        <asp:TextBox ID="txtUniqueKey" runat="server" Enabled="false"></asp:TextBox></td>
                                    <td>Registration Date</td>
                                    <td>
                                        <asp:TextBox ID="txtRegistrationDate" runat="server" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>

                                    <td>Agent Account No.</td>
                                    <td>
                                        <asp:TextBox ID="txtAgentAccNo" runat="server" Enabled="false"></asp:TextBox></td>

                                    <td>Remarks</td>
                                    <td <%--style="display:none"--%>>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="multiline" Columns="26" Rows="2"></asp:TextBox >
                                        <asp:Label ID="lblRemarks" runat="server" ForeColor="Red"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvRemarks" runat="server"
                                            ControlToValidate="txtRemarks" ErrorMessage="Please write something"
                                            ValidationGroup="UpdateCancel" Display="Dynamic"  Visible="true">
                                        </asp:RequiredFieldValidator></td>
                                    <%--<td colspan="2"></td>--%>
                                </tr>

                                 <tr>

                                    <td>Agent Name</td>
                                    <td>
                                        <asp:TextBox ID="txtAgentName" runat="server" Enabled="false"></asp:TextBox></td>



                                   
                                    </td>
                                    <%--<td colspan="2"></td>--%>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="ButtonCancel" runat="server" Text=" Cancel " ValidationnGroup="UpdateCancel" OnClick="ButtonCancel_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="  Verify  " ValidationGroup="Update" OnClick="btnUpdate_Click" Visible="false" />
                                        <asp:Button ID="ButtonReport" runat="server" Text=" Report " OnClick="ButtonReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <table style="border: 1px solid gray">
                                            <tr>
                                                <td align="center"><strong>Customer NID Front</strong></td>
                                                <td align="center"><strong>Customer NID Back</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="ImgNIDFront" runat="server" Width="250" Height="150" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="ImgNIDBack" runat="server" Width="250" Height="150" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center"><strong>Customer Picture</strong></td>
                                                <td align="center"><strong>Customer Signature</strong></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Image ID="ImgCustomer" runat="server" Width="150" Height="150" />
                                                </td>
                                                <td align="center">
                                                    <asp:Image ID="ImgSignature" runat="server" Width="150" Height="50" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>


            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
