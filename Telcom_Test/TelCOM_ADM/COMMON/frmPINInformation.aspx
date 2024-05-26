<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPINInformation.aspx.cs" Inherits="COMMON_frmPINInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PIN Information</title>
     <link type="text/css" rel="Stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
   <asp:ScriptManager id="SCManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UDPanel" runat="server">
        <contenttemplate>
        <div style="BACKGROUND-COLOR: royalblue"><strong><span style="COLOR: white">&nbsp;Search By Wallet ID&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWallet" runat="server"></asp:TextBox>
            &nbsp; &nbsp; &nbsp;<asp:SqlDataSource ID="sdsClientList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
              SelectCommand=" SELECT C.CLINT_NAME,A.ACCNT_NO,A.ACCNT_PIN,A.ACCNT_MSISDN,SP.SERVICE_PKG_NAME, AR.RANK_TITEL, C.CLINT_FATHER_NAME, C.CLINT_MOTHER_NAME ,
 C.CLIENT_DOB,C.OCCUPATION,C.WORK_EDU_BUSINESS,C.PUR_OF_TRAN, C.CLIENT_OFFIC_ADDRESS, C.CLINT_ADDRESS1,C.CLINT_ADDRESS2, BA.BANK_BR_NAME,BA.BANK_ACCNT_NO,
 IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,INF.INTRODCR_NAME,INF.INTRODCR_MOBILE,INF.INTRODCR_ADDRESS,INF.INTRODCR_OCCUPATION, NI.NOMNE_NAME,NI.NOMNE_MOBILE,
 NI.RELATION,NI.PERCENTAGE FROM CLIENT_LIST C,ACCOUNT_LIST A, ACCOUNT_RANK AR,SERVICE_PACKAGE SP,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,
 IDENTIFICATION_SETUP IDS,INTRODUCER_INFO INF  ,NOMINEE_INFO NI WHERE A.CLINT_ID=C.CLINT_ID AND A.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND
  SP.SERVICE_PKG_ID=A.SERVICE_PKG_ID AND C.CLINT_ID=BA.CLIENT_ID(+) AND C.CLINT_ID=CI.CLIENT_ID(+)  AND IDS.IDNTIFCTION_ID(+)=CI.IDNTIFCTION_ID  AND C.CLINT_ID=INF.CLIENT_ID(+) AND
   C.CLINT_ID=NI.CLIENT_ID(+) AND A.ACCNT_NO=' '">
            </asp:SqlDataSource>
          <asp:Button ID="btnView" runat="server"  Text="  View  " 
                onclick="btnView_Click" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
&nbsp;
        </span></strong></div>
        <div>
        
            <table>
                <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align="center">
                    <asp:DetailsView ID="dtvClient" runat="server" AutoGenerateRows="False" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" DataSourceID="sdsClientList" 
                        >
                        <PagerStyle CssClass="pgr" />
                        <Fields>
                            <asp:BoundField DataField="CLINT_NAME" HeaderText="Wallet Holder Name" 
                                SortExpression="CLINT_NAME" />
                            <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" 
                                SortExpression="ACCNT_NO" />
                            <asp:BoundField DataField="ACCNT_PIN" HeaderText="PIN No" 
                                SortExpression="ACCNT_PIN" />
                            <asp:BoundField DataField="ACCNT_MSISDN" HeaderText="Mobile Number" 
                                SortExpression="ACCNT_MSISDN" />
                            <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                                SortExpression="SERVICE_PKG_NAME" />
                            <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank" 
                                SortExpression="RANK_TITEL" />
                            <asp:BoundField DataField="CLINT_FATHER_NAME" HeaderText="Father's Name" 
                                SortExpression="CLINT_FATHER_NAME" />
                            <asp:BoundField DataField="CLINT_MOTHER_NAME" HeaderText="Mother's Name" 
                                SortExpression="CLINT_MOTHER_NAME" />
                            <asp:BoundField DataField="CLIENT_DOB" HeaderText="Date Of Birth" 
                                SortExpression="CLIENT_DOB" />
                            <asp:BoundField DataField="OCCUPATION" HeaderText="Occupation" 
                                SortExpression="OCCUPATION" />
                            <asp:BoundField DataField="WORK_EDU_BUSINESS" HeaderText="Work/EDU/Business" 
                                SortExpression="WORK_EDU_BUSINESS" />
                            <asp:BoundField DataField="PUR_OF_TRAN" HeaderText="Purpose Of Transaction" 
                                SortExpression="PUR_OF_TRAN" />
                            <asp:BoundField DataField="CLIENT_OFFIC_ADDRESS" HeaderText="Office Address" 
                                SortExpression="CLIENT_OFFIC_ADDRESS" />
                            <asp:BoundField DataField="CLINT_ADDRESS1" HeaderText="Present Address" 
                                SortExpression="CLINT_ADDRESS1" />
                            <asp:BoundField DataField="CLINT_ADDRESS2" HeaderText="Permanent Address" 
                                SortExpression="CLINT_ADDRESS2" />
                            <asp:BoundField DataField="BANK_BR_NAME" HeaderText="Bank Branch Name" 
                                SortExpression="BANK_BR_NAME" />
                            <asp:BoundField DataField="BANK_ACCNT_NO" HeaderText="Bank Acc No" 
                                SortExpression="BANK_ACCNT_NO" />
                            <asp:BoundField DataField="IDNTIFCTION_NAME" HeaderText="Identification Name" 
                                SortExpression="IDNTIFCTION_NAME" />
                            <asp:BoundField DataField="CLINT_IDENT_NAME" HeaderText="Identification ID" 
                                SortExpression="CLINT_IDENT_NAME" />
                            <asp:BoundField DataField="INTRODCR_NAME" HeaderText="Introducer Name" 
                                SortExpression="INTRODCR_NAME" />
                            <asp:BoundField DataField="INTRODCR_MOBILE" HeaderText="Introducer Mobile" 
                                SortExpression="INTRODCR_MOBILE" />
                            <asp:BoundField DataField="INTRODCR_ADDRESS" HeaderText="Introducer Address" 
                                SortExpression="INTRODCR_ADDRESS" />
                            <asp:BoundField DataField="INTRODCR_OCCUPATION" HeaderText="Introducer Occupation" 
                                SortExpression="INTRODCR_OCCUPATION" />
                            <asp:BoundField DataField="NOMNE_NAME" HeaderText="Nominee Name" 
                                SortExpression="NOMNE_NAME" />
                            <asp:BoundField DataField="NOMNE_MOBILE" HeaderText="Nominee Mobile" 
                                SortExpression="NOMNE_MOBILE" />
                            <asp:BoundField DataField="RELATION" HeaderText="Relation" 
                                SortExpression="RELATION" />
                            <asp:BoundField DataField="PERCENTAGE" HeaderText="Percentage(%)" 
                                SortExpression="PERCENTAGE" />
                        </Fields>
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView>
                    </td></tr>
                <tr>
                    <td></td><td align="center">&nbsp;</td>
                </tr>
            </table>
         </DIV>   
        </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
