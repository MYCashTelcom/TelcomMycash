<%@ Page Language="C#" AutoEventWireup="false" CodeFile="frmReplaceMobileNo.aspx.cs"
    Inherits="COMMON_frmReplaceMobileNo" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Replace Mobile No</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
    .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UDPanel" runat="server">
        <ContentTemplate>
        <asp:SqlDataSource ID="sdsAccountInfo" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT CL.CLINT_NAME,CL.CLINT_ADDRESS1,AL.ACCNT_NO,AL.ACCNT_MSISDN FROM ACCOUNT_LIST AL, CLIENT_LIST CL 
                            WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_NO=''">
        </asp:SqlDataSource>
        <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
        <table style="width:100%">
         <tr>
          <td>
            Manage Wallet Modify
          </td>
          <td></td>
          <td align="right">
            <asp:Label ID="lblWMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>
          </td>
          <td align="right">
            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
             <ProgressTemplate>
                <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
             </ProgressTemplate>
            </asp:UpdateProgress>
          </td>
         </tr>
        </table>
        </asp:Panel> 
        <asp:Panel ID="pnlView" runat="server" CssClass="View_Panel">
          <div id="divWalletSerch" style="padding-left: 5px;">                
                <span style="font-size: 12px; font-weight: normal;">Search by Wallet ID: </span>
                <asp:TextBox ID="TxtWalletID" runat="server"></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
          </div>
        </asp:Panel>
            <div id="divGrid" style="padding-left: 5px;">
                <asp:GridView ID="gdvAccountInfo" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAccountInfo"
                    BorderStyle="None"  CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                    Visible="False" Width="850">
                    <Columns>
                        <asp:BoundField DataField="CLINT_NAME" HeaderText="Wallet Name" SortExpression="CLINT_NAME" />
                        <asp:BoundField DataField="CLINT_ADDRESS1" HeaderText="Wallet Address" SortExpression="CLINT_ADDRESS1" />
                        <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" SortExpression="ACCNT_NO" />
                        <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile Number" SortExpression="ACCNT_MSISDN" />
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </div>
            <asp:UpdatePanel ID="pnlModify" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <div style="background-color: cornflowerblue; size: auto; text-align: left; height: 14px;">
                        <span style="font-size: 11px; font-weight: bold; color: #FFFFFF;">&nbsp;&nbsp;Modify wallet &nbsp;
                        </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <td colspan="3" align="center" style="font-size: 11px; font-weight: bold; color: White">
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="White" Font-Bold="true"></asp:Label>
                        </td>
                    </div>
                    <div id="divReplace" style="padding-left: 5px;">
                        <table cellpadding="0" cellspacing="4">
                            <tr>
                                <td align="right">
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size: 12px; font-weight: normal;">Old Wallet ID: </span>
                                </td>                                
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOldNumber" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size: 12px; font-weight: normal;">New Wallet ID: </span>
                                </td>
                                
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNewNumber" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnReplace" runat="server" Text="Replace" OnClick="btnReplace_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
