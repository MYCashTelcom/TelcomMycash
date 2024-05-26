<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTerrotoryAccountKYC.aspx.cs" Inherits="MANAGE_TM_TO_frmTerrotoryAccountKYC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KYC Update</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .table
        {
            background-color: #fcfcfc;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            text-align: left;
            border-collapse: collapse;
            border-color: White;
        }
        .table td
        {
            padding: 2px;
            border: solid 1px #c1c1c1;
            color: #717171;
            font-size: 11px;
        }
        .div
        {
            margin: 5px 0 0 0;
        }
        .td
        {
            text-align: right;
            width: 125px;
        }
        .style1
        {
            width: 672px;
        }
        .Top_Panel
        {
            background-color: royalblue;
            height: 20px;
            font-weight: bold;
            color: White;
        }
        .View_Panel
        {
            background-color: powderblue;
            width: 817px;
        }
        .Inser_Panel
        {
            background-color: cornflowerblue;
            font-weight: bold;
            color: White;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:SqlDataSource ID="sdsBankAccount" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                DeleteCommand="DELETE FROM &quot;BANK_ACCOUNT&quot; WHERE &quot;BANK_ACCNT_ID&quot; = :original_BANK_ACCNT_ID"
                InsertCommand="INSERT INTO &quot;BANK_ACCOUNT&quot; (&quot;BANK_ACCNT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;BANK_BR_NAME&quot;, &quot;BANK_ACCNT_NO&quot;, &quot;BANK_NAME&quot;, &quot;REMARKS&quot;) VALUES (:BANK_ACCNT_ID, :CLIENT_ID, :BANK_BR_NAME, :BANK_ACCNT_NO, :BANK_NAME, :REMARKS)"
                OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                SelectCommand="SELECT * FROM &quot;BANK_ACCOUNT&quot;" UpdateCommand="UPDATE &quot;BANK_ACCOUNT&quot; SET  &quot;BANK_BR_NAME&quot; = :BANK_BR_NAME, &quot;BANK_ACCNT_NO&quot; = :BANK_ACCNT_NO, &quot;BANK_NAME&quot; = :BANK_NAME, &quot;REMARKS&quot; = :REMARKS WHERE &quot;BANK_ACCNT_ID&quot; = :original_BANK_ACCNT_ID">
                <DeleteParameters>
                    <asp:Parameter Name="original_BANK_ACCNT_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="BANK_BR_NAME" Type="String" />
                    <asp:Parameter Name="BANK_ACCNT_NO" Type="String" />
                    <asp:Parameter Name="BANK_NAME" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                    <asp:Parameter Name="original_BANK_ACCNT_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="BANK_ACCNT_ID" Type="String" />
                    <asp:Parameter Name="CLIENT_ID" Type="String" />
                    <asp:Parameter Name="BANK_BR_NAME" Type="String" />
                    <asp:Parameter Name="BANK_ACCNT_NO" Type="String" />
                    <asp:Parameter Name="BANK_NAME" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsNomineeInformation" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                DeleteCommand="DELETE FROM &quot;NOMINEE_INFO&quot; WHERE &quot;NOMNE_ID&quot; = :original_NOMNE_ID"
                InsertCommand="INSERT INTO &quot;NOMINEE_INFO&quot; (&quot;NOMNE_ID&quot;, &quot;CLIENT_ID&quot;, &quot;NOMNE_NAME&quot;, &quot;NOMNE_MOBILE&quot;, &quot;RELATION&quot;, &quot;PERCENTAGE&quot;, &quot;REMARKS&quot;) VALUES (:NOMNE_ID, :CLIENT_ID, :NOMNE_NAME, :NOMNE_MOBILE, :RELATION, :PERCENTAGE, :REMARKS)"
                OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                SelectCommand="SELECT * FROM &quot;NOMINEE_INFO&quot;" UpdateCommand="UPDATE &quot;NOMINEE_INFO&quot; SET &quot;NOMNE_NAME&quot; = :NOMNE_NAME, &quot;NOMNE_MOBILE&quot; = :NOMNE_MOBILE, &quot;RELATION&quot; = :RELATION, &quot;PERCENTAGE&quot; = :PERCENTAGE, &quot;REMARKS&quot; = :REMARKS WHERE &quot;NOMNE_ID&quot; = :original_NOMNE_ID">
                <DeleteParameters>
                    <asp:Parameter Name="original_NOMNE_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="NOMNE_NAME" Type="String" />
                    <asp:Parameter Name="NOMNE_MOBILE" Type="String" />
                    <asp:Parameter Name="RELATION" Type="String" />
                    <asp:Parameter Name="PERCENTAGE" Type="Decimal" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                    <asp:Parameter Name="original_NOMNE_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="NOMNE_ID" Type="String" />
                    <asp:Parameter Name="CLIENT_ID" Type="String" />
                    <asp:Parameter Name="NOMNE_NAME" Type="String" />
                    <asp:Parameter Name="NOMNE_MOBILE" Type="String" />
                    <asp:Parameter Name="RELATION" Type="String" />
                    <asp:Parameter Name="PERCENTAGE" Type="Decimal" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsIntroducerInformation" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                DeleteCommand="DELETE FROM &quot;INTRODUCER_INFO&quot; WHERE &quot;INTRODCR_ID&quot; = :original_INTRODCR_ID"
                InsertCommand="INSERT INTO &quot;INTRODUCER_INFO&quot; (&quot;INTRODCR_ID&quot;, &quot;CLIENT_ID&quot;, &quot;INTRODCR_NAME&quot;, &quot;INTRODCR_MOBILE&quot;, &quot;INTRODCR_ADDRESS&quot;, &quot;INTRODCR_OCCUPATION&quot;, &quot;REMARKS&quot;) VALUES (:INTRODCR_ID, :CLIENT_ID, :INTRODCR_NAME, :INTRODCR_MOBILE, :INTRODCR_ADDRESS, :INTRODCR_OCCUPATION, :REMARKS)"
                OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                SelectCommand="SELECT * FROM &quot;INTRODUCER_INFO&quot;" UpdateCommand="UPDATE &quot;INTRODUCER_INFO&quot; SET &quot;INTRODCR_NAME&quot; = :INTRODCR_NAME, &quot;INTRODCR_MOBILE&quot; = :INTRODCR_MOBILE, &quot;INTRODCR_ADDRESS&quot; = :INTRODCR_ADDRESS, &quot;INTRODCR_OCCUPATION&quot; = :INTRODCR_OCCUPATION, &quot;REMARKS&quot; = :REMARKS WHERE &quot;INTRODCR_ID&quot; = :original_INTRODCR_ID">
                <DeleteParameters>
                    <asp:Parameter Name="original_INTRODCR_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="INTRODCR_NAME" Type="String" />
                    <asp:Parameter Name="INTRODCR_MOBILE" Type="String" />
                    <asp:Parameter Name="INTRODCR_ADDRESS" Type="String" />
                    <asp:Parameter Name="INTRODCR_OCCUPATION" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                    <asp:Parameter Name="original_INTRODCR_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="INTRODCR_ID" Type="String" />
                    <asp:Parameter Name="CLIENT_ID" Type="String" />
                    <asp:Parameter Name="INTRODCR_NAME" Type="String" />
                    <asp:Parameter Name="INTRODCR_MOBILE" Type="String" />
                    <asp:Parameter Name="INTRODCR_ADDRESS" Type="String" />
                    <asp:Parameter Name="INTRODCR_OCCUPATION" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsThana" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
                <%--SELECT &quot;THANA_ID&quot;, &quot;THANA_NAME&quot;, &quot;DISTRICT_ID&quot; FROM &quot;MANAGE_THANA&quot;--%>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsDistrict" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="">
                <%--SELECT &quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot; FROM &quot;MANAGE_DISTRICT&quot;--%>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsThirdPartyAgntList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand=" SELECT * FROM THIRD_PARTY_AGENT_LIST">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsClientIdentification" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                DeleteCommand="DELETE FROM &quot;CLIENT_IDENTIFICATION&quot; WHERE &quot;CLINT_IDENT_ID&quot; = :original_CLINT_IDENT_ID"
                InsertCommand="INSERT INTO &quot;CLIENT_IDENTIFICATION&quot; (&quot;CLINT_IDENT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;CLINT_IDENT_NAME&quot;, &quot;REMARKS&quot;, &quot;IDNTIFCTION_ID&quot;) VALUES (:CLINT_IDENT_ID, :CLIENT_ID, :CLINT_IDENT_NAME, :REMARKS, :IDNTIFCTION_ID)"
                OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                SelectCommand="SELECT * FROM &quot;CLIENT_IDENTIFICATION&quot;" UpdateCommand="UPDATE &quot;CLIENT_IDENTIFICATION&quot; SET &quot;CLINT_IDENT_NAME&quot; = :CLINT_IDENT_NAME, &quot;REMARKS&quot; = :REMARKS, &quot;IDNTIFCTION_ID&quot; = :IDNTIFCTION_ID WHERE &quot;CLINT_IDENT_ID&quot; = :original_CLINT_IDENT_ID">
                <DeleteParameters>
                    <asp:Parameter Name="original_CLINT_IDENT_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CLINT_IDENT_NAME" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                    <asp:Parameter Name="IDNTIFCTION_ID" Type="String" />
                    <asp:Parameter Name="original_CLINT_IDENT_ID" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CLINT_IDENT_ID" Type="String" />
                    <asp:Parameter Name="CLIENT_ID" Type="String" />
                    <asp:Parameter Name="CLINT_IDENT_NAME" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />
                    <asp:Parameter Name="IDNTIFCTION_ID" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsClientIdentificationSetUp" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT * FROM &quot;IDENTIFICATION_SETUP&quot;">
            </asp:SqlDataSource>
            
            <asp:SqlDataSource ID="sdsTrRank" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                 ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                 SelectCommand="">
                 <%--SELECT &quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot; FROM &quot;MANAGE_DISTRICT&quot;--%>
             </asp:SqlDataSource>
            
            
            <asp:Panel ID="pnlTop" runat="server" CssClass="Top_Panel">
                <table width="100%">
                    <tr>
                        <td>
                            KYC Update
                        </td>
                        <td>
                        </td>
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
            <asp:Panel ID="pnlView" runat="server" CssClass="View_Panel">
                <table width="815px">
                    <tr>
                        <td class="style1">
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Search By Wallet ID:"></asp:Label>
                            <asp:TextBox ID="txtWalletID" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label3" runat="server" Text=" Search By Mobile Number:"></asp:Label>
                            <asp:TextBox ID="txtMobileNumber" runat="server" OnTextChanged="txtMobileNumber_TextChanged">
                            </asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                Width="60px" />
                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="New"
                                Width="50px" Visible="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlInsert" runat="server">
                <fieldset style="border-color: #FFFFFF; width: 815px; height: auto; margin-top: 10px 0px 0px 0px;">
                    <legend></legend>
                    <table width="815px">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtClientID" Visible="false" Enabled="false" runat="Server" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCashIn" runat="server" Text="Cash In"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblCashIn" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    Enabled="false">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <%--<asp:ListItem Value="N">No</asp:ListItem>--%>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblInComptKYC" runat="server" Text="Incomplete KYC"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblInComptKYC" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <%--<asp:ListItem Value="N">No</asp:ListItem>--%>
                                    <asp:ListItem Value="">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Form Serial No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFormSerialNo" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right">
                                Mobile Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtClientMobile" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvClientMobile" runat="server" ControlToValidate="txtClientMobile"
                                    ErrorMessage="Please enter mobile no" ForeColor="Red" ValidationGroup="Update"
                                    Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Name of the Client:
                            </td>
                            <td>
                                <asp:TextBox ID="txtClientName" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvClientName" runat="server" ControlToValidate="txtClientName"
                                    ErrorMessage="Please enter client name" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td align="left" colspan="2">
                                <table>
                                    <tr>
                                        <td style="width: 115px;">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblFatherHusband" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Selected="True">Father&#39;s Name</asp:ListItem>
                                                <asp:ListItem Value="1">Husband Name</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvFather" runat="server" ErrorMessage="Enter Father/Husband"
                                                ControlToValidate="rblFatherHusband" ValidationGroup="Update" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mother's Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMothersName" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvMotherName" runat="server" ControlToValidate="txtMothersName"
                                    ErrorMessage="Please enter Mother's name" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Father/Husband Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtFatherHusbandName" runat="server"></asp:TextBox>
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFatherHusbandName"
                                    ErrorMessage="Please enter Father's/Husband name" ValidationGroup="Update">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Date of Birth:
                            </td>
                            <td>
                                <cc1:GMDatePicker ID="dptDateOfBirth" runat="server" CalendarTheme="Silver" DateFormat="dd/MMM/yyyy "
                                    MinDate="1900-01-01" Style="position: relative;" TextBoxWidth="149" YearDropDownRange="200">
                                    <CalendarTitleStyle BackColor="#FFFFC0" Font-Size="X-Small" />
                                </cc1:GMDatePicker>
                            </td>
                            <td align="right">
                                Occupation:
                            </td>
                            <td>
                                <asp:TextBox ID="txtOccupation" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvOccupation" runat="server" ControlToValidate="txtOccupation"
                                    ErrorMessage="Please enter Occupation" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;District:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistrict" runat="server" Width="153px" DataSourceID="sdsDistrict"
                                    AutoPostBack="true" AppendDataBoundItems="true" DataTextField="DISTRICT_NAME"
                                    DataValueField="DISTRICT_ID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict"
                                    ErrorMessage="Please select a district" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                &nbsp;Thana:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlThana" runat="server" DataSourceID="sdsThana" AppendDataBoundItems="true"
                                    DataTextField="THANA_NAME" DataValueField="THANA_ID" Width="153px">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvThana" runat="server" ControlToValidate="ddlThana"
                                    ErrorMessage="Please select a thana" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Purpose of Transction:
                            </td>
                            <td colspan="1">
                                <asp:DropDownList ID="ddlPurOfTran" Width="153px" runat="server" AppendDataBoundItems="true"
                                    DataTextField="PUR_OF_TRAN" DataValueField="PUR_OF_TRAN" SelectedValue='<%# Eval("PUR_OF_TRAN") %>'>
                                    <asp:ListItem Value="Personal" Text="Personal"></asp:ListItem>
                                    <asp:ListItem Value="Business" Text="Business"></asp:ListItem>
                                    <asp:ListItem Value="Self" Text="Self"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblUISCAgent" runat="server" Text="Agent type:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUISCAgent" Width="153px" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="true" DataSourceID="sdsThirdPartyAgntList" DataTextField="AGENT_CATEGORY"
                                    DataValueField="THIRD_PARTY_AGENT_ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Work/Edu/Business:
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox runat="server" Width="501px" ID="txtWorkEduBusines"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Official Address:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtOffceAddress" Width="502px" TextMode="MultiLine" runat="Server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Present Address:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPreAddress" Width="502px" TextMode="MultiLine" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvPresentAddress" runat="server" ControlToValidate="txtPreAddress"
                                    ErrorMessage="Please enter present address" ValidationGroup="Update" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Permanent Adddress:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPerAddress" Width="502px" TextMode="MultiLine" runat="Server" />
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="rfvPermanentAddress" runat="server" ControlToValidate="txtPerAddress"
                                    ErrorMessage="Please enter permanent address" ValidationGroup="Update">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                         <td>
                          Territory Rank    
                         </td>   
                         <td>
                             <asp:DropDownList runat="server" ID="drpTrRank" 
                                 DataTextField="TERRITORY_RANK_NAME" DataValueField="TERRITORY_RANK_ID" 
                                 DataSourceID="sdsTrRank" Enabled="False"/>  
                         </td>
                        </tr>
                        
                        
                    </table>
                    
                    
                    
                    
                    
                    
                    
                    
                    <asp:Panel ID="pnlBankHeading" runat="server" CssClass="Inser_Panel">
                        <table class="div" style="height: 20px; width: 812px; cursor: pointer; margin: 2px;">
                            <tr>
                                <td>
                                    Bank Information
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imbBank" runat="server" ImageUrl="~/resources/images/expand.jpg"
                                        AlternateText="BankInfo" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                     
                    
                    
                    
                    
                    <asp:Panel ID="pnlBankDescription" runat="server" Style="overflow: hidden;">
                        <table width="815px">
                            <tr>
                                <td>
                                    <asp:GridView ID="grdBankAccount" runat="server" AllowPaging="True" PageSize="3"
                                        Width="812px" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsBankAccount"
                                        HeaderStyle-Font-Bold="false" DataKeyNames="BANK_ACCNT_ID" BorderColor="White"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnRowCancelingEdit="grdBankAccount_RowCancelingEdit" OnRowEditing="grdBankAccount_RowEditing"
                                        OnRowUpdated="grdBankAccount_RowUpdated" OnPageIndexChanged="grdBankAccount_PageIndexChanged"
                                        OnRowDeleted="grdBankAccount_RowDeleted" OnRowDeleting="grdBankAccount_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="BANK_ACCNT_ID" HeaderText="Bank Acc ID" ReadOnly="True"
                                                SortExpression="BANK_ACCNT_ID" Visible="false" />
                                            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" SortExpression="BANK_NAME" />
                                            <asp:BoundField DataField="BANK_BR_NAME" HeaderText="Bank Branch" SortExpression="BANK_BR_NAME" />
                                            <asp:BoundField DataField="BANK_ACCNT_NO" HeaderText="Bank Acc No" SortExpression="BANK_ACCNT_NO" />
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS"
                                                ItemStyle-Width="50px">
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:TemplateField ShowHeader="False">
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnBankUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                        Text="Update" />
                                                    &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                        Text="Cancel" />
                                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeBankUpdate" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                                                        OnClientCancel="cancelClick" TargetControlID="btnBankUpdate">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                        CancelControlID="btnBankUpdateCancel" OkControlID="btnBankUpdateOK" TargetControlID="btnBankUpdate"
                                                        PopupControlID="pnlPopUpBankUpdate">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                        Text="Edit" />
                                                    &nbsp;<asp:Button ID="btnDeleteBank" runat="server" CausesValidation="False" CommandName="Delete"
                                                        Text="Delete" />
                                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeBankDelete" runat="server" DisplayModalPopupID="ModalPopupExtender3"
                                                        OnClientCancel="cancelClick" TargetControlID="btnDeleteBank">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                        CancelControlID="btnBankDeleteCancel" OkControlID="btnBankDeleteOK" TargetControlID="btnDeleteBank"
                                                        PopupControlID="pnlPopUpBankDelete">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ControlStyle Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle Font-Bold="False" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                    <asp:Panel ID="pnlPopUpBankUpdate" runat="server" Style="display: none; width: 300px;
                                        height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                        border-style: solid; padding: 1px;">
                                        <div style="height: 95px;">
                                            <br />
                                            &nbsp;<br />
                                            &nbsp; Are you sure you want to update?
                                            <br />
                                            &nbsp;<br />
                                            &nbsp;
                                        </div>
                                        <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                            <br />
                                            &nbsp;
                                            <asp:Button ID="btnBankUpdateOK" runat="server" Text="  Yes  " />
                                            <asp:Button ID="btnBankUpdateCancel" runat="server" Text=" Cancel " />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPopUpBankDelete" runat="server" Style="display: none; width: 300px;
                                        height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                        border-style: solid; padding: 1px;">
                                        <div style="height: 95px;">
                                            <br />
                                            &nbsp;<br />
                                            &nbsp; Are you sure you want to delete?
                                            <br />
                                            &nbsp;<br />
                                            &nbsp;
                                        </div>
                                        <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                            <br />
                                            &nbsp;
                                            <asp:Button ID="btnBankDeleteOK" runat="server" Text="  Yes  " />
                                            <asp:Button ID="btnBankDeleteCancel" runat="server" Text=" Cancel " />
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 812px;" class="Inser_Panel">
                                        Add New&nbsp;Bank&nbsp;
                                    </div>
                                    <table>
                                        <tr>
                                            <td class="td">
                                                <asp:Label ID="Label1" runat="server" Text="Bank Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:Label ID="lblBankBranch" runat="server" Text="Branch Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountNo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:Label ID="lblBankRemarks" runat="server" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBankRemarks" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                        CollapseControlID="pnlBankHeading" ExpandControlID="pnlBankHeading" TargetControlID="pnlBankDescription"
                        Collapsed="true" AutoCollapse="false" AutoExpand="false" ExpandDirection="Vertical"
                        ImageControlID="imbBank" CollapsedImage="~/resources/images/expand.jpg" ExpandedImage="~/resources/images/collapse.jpg">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <asp:Panel ID="pnlIdenHeading" runat="server" CssClass="Inser_Panel">
                        <table class="div" style="height: 20px; width: 812px; cursor: pointer; margin: 2px;">
                            <tr>
                                <td>
                                    Identification Information
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imbIden" runat="server" ImageUrl="~/resources/images/expand.jpg"
                                        AlternateText="IdenInfo" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    
                    
                    
                    
                    <asp:Panel ID="pnlIdenContent" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdClientIdentification" runat="server" AllowPaging="True" PageSize="3"
                                            Width="812px" AllowSorting="True" DataSourceID="sdsClientIdentification" AutoGenerateColumns="False"
                                            DataKeyNames="CLINT_IDENT_ID" BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="grdClientIdentification_RowCancelingEdit"
                                            OnRowEditing="grdClientIdentification_RowEditing" OnRowUpdated="grdClientIdentification_RowUpdated"
                                            OnPageIndexChanged="grdClientIdentification_PageIndexChanged" OnRowDeleted="grdClientIdentification_RowDeleted"
                                            OnRowDeleting="grdClientIdentification_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="CLINT_IDENT_ID" SortExpression="CLINT_IDENT_ID" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CLINT_IDENT_ID") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CLINT_IDENT_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identification Name" SortExpression="IDNTIFCTION_ID">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="DropDownList6" DataTextField="IDNTIFCTION_NAME" DataValueField="IDNTIFCTION_ID"
                                                            runat="server" DataSourceID="sdsClientIdentificationSetUp" SelectedValue='<%# Bind("IDNTIFCTION_ID") %>'>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DropDownList5" runat="server" DataValueField="IDNTIFCTION_ID"
                                                            DataTextField="IDNTIFCTION_NAME" DataSourceID="sdsClientIdentificationSetUp"
                                                            SelectedValue='<%# Bind("IDNTIFCTION_ID") %>' Enabled="False">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CLINT_IDENT_NAME" HeaderText="ID" SortExpression="CLINT_IDENT_NAME" />
                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS" />
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnIdenUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            Text="Cancel" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeIdenUpdate" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                                                            OnClientCancel="cancelClick" TargetControlID="btnIdenUpdate">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnIdenUpdateCancel" OkControlID="btnIdenUpdateOK" TargetControlID="btnIdenUpdate"
                                                            PopupControlID="pnlPopUpIdenUpdate">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                            Text="Edit" />
                                                        &nbsp;<asp:Button ID="btnDeleteIden" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Delete" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeIdenDelete" runat="server" DisplayModalPopupID="ModalPopupExtender3"
                                                            OnClientCancel="cancelClick" TargetControlID="btnDeleteIden">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnIdenDeleteCancel" OkControlID="btnIdenDeleteOK" TargetControlID="btnDeleteIden"
                                                            PopupControlID="pnlPopUpIdenDelete">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                        <asp:Panel ID="pnlPopUpIdenUpdate" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to update?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnIdenUpdateOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnIdenUpdateCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPopUpIdenDelete" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to delete?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnIdenDeleteOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnIdenDeleteCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 812px;" class="Inser_Panel">
                                            Add New&nbsp;Identification&nbsp;
                                        </div>
                                        <table>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIdenName" runat="server" Text="Identification Name:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlIdenName" runat="server" Width="170px" AutoPostBack="true"
                                                        DataSourceID="sdsClientIdentificationSetUp" DataTextField="IDNTIFCTION_NAME"
                                                        DataValueField="IDNTIFCTION_ID">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIdenID" runat="server" Text="ID:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIdenID" runat="server" MaxLength="17"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIdenRemarks" runat="server" Text="Remarks:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIdenRemarks" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                        CollapseControlID="pnlIdenHeading" ExpandControlID="pnlIdenHeading" TargetControlID="pnlIdenContent"
                        Collapsed="True" AutoCollapse="false" AutoExpand="false" ExpandDirection="Vertical"
                        ImageControlID="imbIden" CollapsedImage="~/resources/images/expand.jpg" ExpandedImage="~/resources/images/collapse.jpg">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    <asp:Panel ID="pnlIntrodheading" runat="server" CssClass="Inser_Panel">
                        <table style="height: 20px; width: 812px; cursor: pointer; margin: 2px;">
                            <tr>
                                <td>
                                    Introducer Information
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imbIntrod" runat="server" ImageUrl="~/resources/images/expand.jpg"
                                        AlternateText="IdenInfo" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlIntrodDescription" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdIntroducerInfo" runat="server" AllowPaging="True" PageSize="3"
                                            AllowSorting="True" DataSourceID="sdsIntroducerInformation" AutoGenerateColumns="False"
                                            DataKeyNames="INTRODCR_ID" Width="812px" BorderColor="White" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="grdIntroducerInfo_RowCancelingEdit"
                                            OnRowEditing="grdIntroducerInfo_RowEditing" OnRowUpdated="grdIntroducerInfo_RowUpdated"
                                            OnPageIndexChanged="grdIntroducerInfo_PageIndexChanged" OnRowDeleted="grdIntroducerInfo_RowDeleted"
                                            OnRowDeleting="grdIntroducerInfo_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="INTRODCR_ID" HeaderText="INTRODCR_ID" ReadOnly="True"
                                                    SortExpression="INTRODCR_ID" Visible="False" />
                                                <asp:BoundField DataField="INTRODCR_NAME" HeaderText="Introducer Name" SortExpression="Introducer Name"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INTRODCR_MOBILE" HeaderText="Mobile" SortExpression="INTRODCR_MOBILE"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INTRODCR_ADDRESS" HeaderText="Address" SortExpression="INTRODCR_ADDRESS"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INTRODCR_OCCUPATION" ControlStyle-Width="100px" HeaderText="Occupation"
                                                    SortExpression="INTRODCR_OCCUPATION">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnIntrodUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            Text="Cancel" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeIntrodUpdate" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                                                            OnClientCancel="cancelClick" TargetControlID="btnIntrodUpdate">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnIntrodUpdateCancel" OkControlID="btnIntrodUpdateOK" TargetControlID="btnIntrodUpdate"
                                                            PopupControlID="pnlPopUpIntrodUpdate">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                            Text="Edit" />
                                                        &nbsp;<asp:Button ID="btnDeleteIntrod" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Delete" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeIntrodDelete" runat="server" DisplayModalPopupID="ModalPopupExtender3"
                                                            OnClientCancel="cancelClick" TargetControlID="btnDeleteIntrod">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnIntrodDeleteCancel" OkControlID="btnIntrodDeleteOK" TargetControlID="btnDeleteIntrod"
                                                            PopupControlID="pnlPopUpIntrodDelete">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                        <asp:Panel ID="pnlPopUpIntrodUpdate" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to update?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnIntrodUpdateOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnIntrodUpdateCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPopUpIntrodDelete" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to delete?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnIntrodDeleteOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnIntrodDeleteCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="Inser_Panel" style="width: 812px;">
                                            Add New&nbsp;Introducer&nbsp;
                                        </div>
                                        <table>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIntroducerName" runat="server" Text="Introducer Name:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntroducerName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIntroMobile" runat="server" Text="Mobile:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntroMobile" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIntroAddress" runat="server" Text="Address:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntroAddress" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIntroOccupation" runat="server" Text="Occupation:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntroOccupation" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblIntroRemarks" runat="server" Text="Remarks:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntroRemarks" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                        CollapseControlID="pnlIntrodheading" Collapsed="True" ExpandControlID="pnlIntrodheading"
                        TargetControlID="pnlIntrodDescription" AutoCollapse="false" AutoExpand="false"
                        ExpandDirection="Vertical" ImageControlID="imbIntrod" CollapsedImage="~/resources/images/expand.jpg"
                        ExpandedImage="~/resources/images/collapse.jpg">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <asp:Panel ID="pnlNominiheading" runat="server" CssClass="Inser_Panel">
                        <table class="div" style="height: 20px; width: 812px; cursor: pointer; margin: 2px;">
                            <span>
                                <tr>
                                    <td>
                                        Nominee Information
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imbNominee" runat="server" ImageUrl="~/resources/images/expand.jpg"
                                            AlternateText="IdenInfo" />
                                    </td>
                                </tr>
                            </span>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlNominiDescription" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="grdNomineeInfo" runat="server" AllowPaging="True" PageSize="3"
                                            AllowSorting="True" DataSourceID="sdsNomineeInformation" AutoGenerateColumns="False"
                                            DataKeyNames="NOMNE_ID" Width="812px" BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="grdNomineeInfo_RowCancelingEdit"
                                            OnRowEditing="grdNomineeInfo_RowEditing" OnRowUpdated="grdNomineeInfo_RowUpdated"
                                            OnPageIndexChanged="grdNomineeInfo_PageIndexChanged" OnRowDeleted="grdNomineeInfo_RowDeleted"
                                            OnRowDeleting="grdNomineeInfo_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="NOMNE_ID" HeaderText="NOMNE_ID" ReadOnly="True" SortExpression="NOMNE_ID"
                                                    Visible="False" />
                                                <asp:BoundField DataField="NOMNE_NAME" HeaderText="Nominee Name" SortExpression="Nominee Name"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NOMNE_MOBILE" HeaderText="Mobile" SortExpression="NOMNE_MOBILE"
                                                    ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RELATION" HeaderText="Relation" SortExpression="RELATION"
                                                    HeaderStyle-Width="80px">
                                                    <ControlStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PERCENTAGE" HeaderText="Percentage(%)" SortExpression="PERCENTAGE"
                                                    HeaderStyle-Width="90px">
                                                    <ControlStyle Width="90px" />
                                                    <HeaderStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS"
                                                    HeaderStyle-Width="60px">
                                                    <ControlStyle Width="60px" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnNominiUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            Text="Cancel" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeNominiUpdate" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                                                            OnClientCancel="cancelClick" TargetControlID="btnNominiUpdate">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnNominiUpdateCancel" OkControlID="btnNominiUpdateOK" TargetControlID="btnNominiUpdate"
                                                            PopupControlID="pnlPopUpNominiUpdate">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                            Text="Edit" />
                                                        &nbsp;<asp:Button ID="btnDeleteNomini" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Delete" />
                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeNominiDelete" runat="server" DisplayModalPopupID="ModalPopupExtender3"
                                                            OnClientCancel="cancelClick" TargetControlID="btnDeleteNomini">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnNominiDeleteCancel" OkControlID="btnNominiDeleteOK" TargetControlID="btnDeleteNomini"
                                                            PopupControlID="pnlPopUpNominiDelete">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                        <asp:Panel ID="pnlPopUpNominiUpdate" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to update?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnNominiUpdateOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnNominiUpdateCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPopUpNominiDelete" runat="server" Style="display: none; width: 300px;
                                            height: 165px; background-color: White; border-width: 1px; border-color: Silver;
                                            border-style: solid; padding: 1px;">
                                            <div style="height: 95px;">
                                                <br />
                                                &nbsp;<br />
                                                &nbsp; Are you sure you want to delete?
                                                <br />
                                                &nbsp;<br />
                                                &nbsp;
                                            </div>
                                            <div style="text-align: right; background-color: #C0C0C0; height: 70px;">
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnNominiDeleteOK" runat="server" Text="  Yes  " />
                                                <asp:Button ID="btnNominiDeleteCancel" runat="server" Text=" Cancel " />
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="height: 14px; width: 812px;" class="Inser_Panel">
                                            Add New&nbsp;Nominee&nbsp;
                                        </div>
                                        <table>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblNomneName" runat="server" Text="Nominee Name:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomineeName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblNomneMobile" runat="server" Text="Mobile:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomneMobile" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblNomneRelation" runat="server" Text="Relation:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomneRelation" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblNomnePercent" runat="server" Text="Percent(%):"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomnePrcent" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Label ID="lblNomneRemarks" runat="server" Text="Remarks:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomneRemarks" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                        TargetControlID="pnlNominiDescription" CollapseControlID="pnlNominiheading" ExpandControlID="pnlNominiheading"
                        Collapsed="True" AutoCollapse="false" AutoExpand="false" ExpandDirection="Vertical"
                        ImageControlID="imbNominee" CollapsedImage="~/resources/images/expand.jpg" ExpandedImage="~/resources/images/collapse.jpg">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <table width="714px">
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="txtUpdate" runat="server" Text="Save" OnClick="txtUpdate_Click" Width="69px"
                                    ValidationGroup="Update" />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                                    OnClientCancel="cancelClick" TargetControlID="txtUpdate">
                                </ajaxToolkit:ConfirmButtonExtender>
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ButtonCancel" OkControlID="ButtonOk" TargetControlID="txtUpdate"
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
                                        <asp:Button ID="ButtonOk" runat="server" Text="  Yes  " ValidationGroup="Update" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text=" Cancel " />
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
