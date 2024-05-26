<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RESET_PASSWORD.aspx.cs" Inherits="RESET_PASSWORD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Password Change</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
  <style type="text/css">
        .clsBody {
            text-align: center;
        }
        .btnReset
        {
    color: black;
    width: 80px;
    height: 35px;
        }
    </style>
</head>
<body style="background-color: lightgrey; margin-top: -15px">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ToolkitScriptManager1" />
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>


                    <div>
                        <asp:Panel ID="pnlTop" runat="server" BackColor="royalblue">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="co"></div>
                                </div>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="height: 30px; font-size: 15px; text-align: center;">
                                        <strong><span style="color: white">Change LogIn Information Reset password</span></strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblSpMessage" runat="server" Font-Bold="True"
                                            Font-Italic="True" Font-Size="12px" ForeColor="Yellow"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <img alt="" src="../resources/images/loading.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <br />
                          <div class="container col-md-offset-4 col-md-8">
                        <asp:Panel ID="pnlExpired" runat="server" Visible="true" CssClass="clsBody">
                            <div style="border:1px solid black">
                            <span style="text-align: center"><b>Password Instructions:&nbsp; </b>Total Minimum of 8 characters and Atleast of One upper case Letter,one Lowercase 
                Letter, one Digit, one special character(@#$%&amp;*!=?)</span>&nbsp;
                        <br />
                        <br />

                           

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4 col-xs-4">
                                                <label for="exampleInputPassword1">Login Name</label></div>
                                            <div class="col-md-6 col-xs-6">
                                                <asp:TextBox CssClass="form-control" ID="txtLoginName" runat="server" Font-Size="12px"
                                                    TextMode="SingleLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="exampleInputPassword1">Enter old Password</label></div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server" Font-Size="12px"
                                                    TextMode="Password"></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator ID="reExp" runat="server" 
                        ControlToValidate="txtOldPassword" ErrorMessage="*" 
                        ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#*!?$%^&amp;+=]).*$" 
                        ValidationGroup="check">
                    </asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="exampleInputPassword1">New Password</label></div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNewpass" runat="server" Font-Size="12px" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  Display="Dynamic"
                                                    ControlToValidate="txtNewpass" ErrorMessage="Please Input Like Above Pattern"
                                                    ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#*!?$%^&amp;+=]).*$"
                                                    ValidationGroup="check">
                                                </asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="exampleInputPassword2">Confirm Password</label></div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" Font-Size="12px" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                             </div>
                                            <div class="col-md-4">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  Display="Dynamic"
                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="Please Input Like Above Pattern"
                                                    ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#*!?$%^&amp;+=]).*$"
                                                    ValidationGroup="check">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-md-4"></div>
                                            <div class="col-md-8" style="text-align:left;">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewpass" ErrorMessage="New password and confirm password does not match"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">&nbsp; </div>
                                            <div class="col-md-8" style="text-align:left;">
                                                <asp:Button ID="btnSave"  runat="server" CssClass="btnReset" Text="Save" OnClick="btnSave_Click1" ValidationGroup="check" BorderStyle="Solid"  BackColor="#009933" />
                                                <asp:Button ID="btnCancel" runat="server"  CssClass="btnReset" OnClick="btnCancel_Click" Text="Cancel" BorderStyle="Solid"  BackColor="#FF3300" />
                                                <asp:Button ID="btnLogin" runat="server" CssClass="btnReset" Visible="false" Text="Login" OnClick="btnLogin_Click" /></di>
                                            </div>
                                        </div>
                                    </div>
       </div>
                        </asp:Panel>

                          </div>
                </ContentTemplate>
            </asp:UpdatePanel>
      
    </form>
</body>
<script src="css/jquery-3.3.1.slim.min.js"></script>
<script src="css/popper.min.js"></script>
<link href="css/bootstrap.min.css" rel="stylesheet" />

</html>
