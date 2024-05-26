<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPINInfoNid.aspx.cs" Inherits="COMMON_frmPINInfoNid" %>

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
        <div style="BACKGROUND-COLOR: royalblue"><strong><span style="COLOR: white">&nbsp;Search By NID ID&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWallet" runat="server"></asp:TextBox>
            &nbsp; &nbsp; &nbsp;<%--<asp:SqlDataSource ID="sdsClientList" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                
              SelectCommand=" SELECT CL.CLINT_NAME, AL.ACCNT_NO,AL.ACCNT_MSISDN, AR.RANK_TITEL, SP.SERVICE_PKG_NAME, CLINT_ADDRESS1,CLINT_ADDRESS2,CLIENT_DOB FROM CLIENT_IDENTIFICATION CI , CLIENT_LIST CL, ACCOUNT_LIST AL, ACCOUNT_RANK AR ,SERVICE_PACKAGE SP WHERE CI.CLIENT_ID=CL.CLINT_ID AND CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID= SP.SERVICE_PKG_ID AND CI.CLINT_IDENT_NAME=' '">
            </asp:SqlDataSource>--%><asp:Button ID="btnView" runat="server"  Text="  View  " 
                onclick="btnView_Click" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
&nbsp;
        </span></strong></div>
        <div>
        
            <table>
                <tr><td align="center">
                 <%--   <asp:DetailsView ID="dtvClient" runat="server" AutoGenerateRows="False" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" DataSourceID="sdsClientList" 
                        >
                        <PagerStyle CssClass="pgr" />
                        <Fields>
                          
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
                    </asp:DetailsView>--%>



                     <asp:GridView ID="dtvClient" runat="server" AllowPaging="True"
                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                    BorderColor="#E0E0E0" BorderStyle="None" CssClass="mGrid"
                    DataKeyNames="ACCNT_NO" 
                     OnRowDataBound="dtvClient_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataText="No data found....."  
                   
                    PagerStyle-CssClass="pgr" PageSize="13" Width="1000px">
                <Columns>
                       
                      
                        <asp:BoundField DataField="CLINT_NAME" HeaderText="Wallet Holder Name" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="CLINT_NAME" />

                        <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet ID" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"
                            ReadOnly="True" SortExpression="ACCNT_NO" />  
                                       
                        <asp:BoundField DataField="ACCNT_MSISDN"
                            HeaderText="Mobile Number" ReadOnly="True"
                            SortExpression="ACCNT_MSISDN" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" />

                        <asp:BoundField DataField="SERVICE_PKG_NAME" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Service Package" ReadOnly="True"
                            SortExpression="SERVICE_PKG_NAME" />

                        <asp:BoundField DataField="RANK_TITEL" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"     HeaderText="Rank" ReadOnly="True"
                            SortExpression="RANK_TITEL" Visible="true" />                    
                     
                        <asp:BoundField DataField="CLINT_ADDRESS1"
                            HeaderText="Present Addrss" ReadOnly="True"  HeaderStyle-HorizontalAlign="Center"
                            SortExpression="CLINT_ADDRESS1" />

                      <asp:BoundField DataField="CLINT_ADDRESS2"
                            HeaderText="Permanent Address" ReadOnly="True"  HeaderStyle-HorizontalAlign="Center"
                            SortExpression="CLINT_ADDRESS2" />

                      <asp:BoundField DataField="CLIENT_DOB"
                            HeaderText="Date of Barth" ReadOnly="True"  HeaderStyle-HorizontalAlign="Center"
                            SortExpression="CLIENT_DOB" />
                       
                    </Columns> 
                        
                
            </asp:GridView>


                    </td><td align="center">&nbsp;</td>
                </tr>
            </table>
         </DIV>   
        </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
