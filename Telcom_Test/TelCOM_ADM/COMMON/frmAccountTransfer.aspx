<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountTransfer.aspx.cs" Inherits="COMMON_frmAccountTransfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hierarchy Transfer</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
     <script  type="text/javascript" language="javascript">
        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
            spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                  elm[i].id != theBox.id) {
                //elm[i].click();

                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }
    </script>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">        
     </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
          <asp:SqlDataSource ID="sdsOldWalletID" runat="server" 
              ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              SelectCommand="SELECT CL.CLINT_NAME, AL.ACCNT_NO, AR.RANK_TITEL, SP.SERVICE_PKG_NAME FROM ACCOUNT_LIST AL,
                             CLIENT_LIST CL, ACCOUNT_RANK AR, SERVICE_PACKAGE SP WHERE AL.CLINT_ID = CL.CLINT_ID AND 
                             AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND AL.ACCNT_NO=''">
          </asp:SqlDataSource>
          <asp:SqlDataSource ID="sdsNewWalletID" runat="server" 
              ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              SelectCommand="SELECT CL.CLINT_NAME, AL.ACCNT_NO, AR.RANK_TITEL, SP.SERVICE_PKG_NAME FROM ACCOUNT_LIST AL, CLIENT_LIST CL, 
                             ACCOUNT_RANK AR, SERVICE_PACKAGE SP WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID 
                             AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND AL.ACCNT_NO=''">
          </asp:SqlDataSource>
          <asp:SqlDataSource ID="sdsNewAccount" runat="server" 
              ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              SelectCommand="">
          </asp:SqlDataSource>
          <asp:SqlDataSource ID="sdsOldAccnt" runat="server" 
              ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
              ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
              SelectCommand="">
              <%--SELECT ACCOUNT_LIST.ACCNT_NO FROM ACCOUNT_LIST, ACCOUNT_HIERARCHY, ACCOUNT_LIST ACCOUNT_LIST_1 WHERE ACCOUNT_LIST.ACCNT_ID = ACCOUNT_HIERARCHY.ACCNT_ID AND ACCOUNT_HIERARCHY.HIERARCHY_ACCNT_ID = ACCOUNT_LIST_1.ACCNT_ID--%>
              </asp:SqlDataSource>
         <div style="BACKGROUND-COLOR: royalblue">             
             <strong>
           <span style="COLOR: white">&nbsp;Manage Hierarchy Transfer &nbsp;
               <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </span></strong>
         </div>
        <table runat="server">
         <tr>
         <td valign="top">
        <fieldset style="border-color: #FFFFFF; height:auto;">
          <legend ><strong style="color: #666666">Previous Parent</strong></legend>
            <asp:Label ID="lblOldAccount" runat="server" Text="Account Wallet ID"></asp:Label>
            <asp:TextBox ID="txtOldWalletID" runat="server"></asp:TextBox>
            <asp:Button ID="txtOldWalletSearch" runat="server" Text="Search" 
                onclick="txtOldWalletSearch_Click" />
            <br />
            <asp:DetailsView ID="dtvOldWalletInfo" runat="server" Height="120px"  Visible="false"
                 AutoGenerateRows="False" DataSourceID="sdsOldWalletID"
                CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                <PagerStyle CssClass="pgr" />
                <Fields>
                    <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                        SortExpression="CLINT_NAME" />
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" 
                        SortExpression="ACCNT_NO" />
                    <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank Title" 
                        SortExpression="RANK_TITEL" />
                    <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                        SortExpression="SERVICE_PKG_NAME" />
                </Fields>
                <AlternatingRowStyle CssClass="alt" />
            </asp:DetailsView>           
        </fieldset>        
        </td>
        <td valign="top" style="padding-top:15px;" >        
            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" 
                onclick="btnTransfer_Click"  />        
        </td> 
        <td valign="top">
        <fieldset  style="border-color: #FFFFFF;height:auto;">
         <legend><strong style="color: #666666">New Parent</strong></legend>
          <asp:Label ID="lblNewWalletID" runat="server" Text="Account Wallet ID"></asp:Label>
          <asp:TextBox ID="txtNewWalletID" runat="server"></asp:TextBox>
          <asp:Button ID="btnNewWalletSearch" runat="server" Text="Search" 
                onclick="btnNewWalletSearch_Click" />
          <br />
            <asp:DetailsView ID="dtvNewWalletInfo" runat="server" Height="120px"  Visible="false"
                 AutoGenerateRows="False" DataSourceID="sdsNewWalletID"
                CssClass="mGrid" PagerStyle-CssClass="pgr"   AlternatingRowStyle-CssClass="alt">
                <PagerStyle CssClass="pgr" />
                <Fields>
                    <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                        SortExpression="CLINT_NAME" />
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Account No" 
                        SortExpression="ACCNT_NO" />
                    <asp:BoundField DataField="RANK_TITEL" HeaderText="Account Rank Title" 
                        SortExpression="RANK_TITEL" />
                    <asp:BoundField DataField="SERVICE_PKG_NAME" HeaderText="Service Package" 
                        SortExpression="SERVICE_PKG_NAME" />
                </Fields>
                <AlternatingRowStyle CssClass="alt" />
            </asp:DetailsView>            
        </fieldset>
        </td> 
        </tr>
        <tr>
           <td valign="top">
            <fieldset id="fldpreviousChild" runat="server" visible="false" style="border-color: #FFFFFF;height:auto;">
             <legend><strong style="color: #666666">Child</strong></legend>
                <asp:GridView ID="gdvOldAccount" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsOldAccnt" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                  GridLines="None" AlternatingRowStyle-CssClass="alt"   BorderStyle="None">
                <Columns>                   
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server"  Text="All" onclick="javascript:SelectAllCheckboxes(this);"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIndividual" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet" 
                        SortExpression="ACCNT_NO" />
                    <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                     SortExpression="CLINT_NAME" />   
                   <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank"
                    SortExpression="RANK_TITEL" />   
                </Columns>
                  <PagerStyle CssClass="pgr" />
                  <HeaderStyle Font-Bold="False" />
                  <AlternatingRowStyle CssClass="alt" />
             </asp:GridView>
            </fieldset>
           </td>
           <td></td>
           <td valign="top" >
            <fieldset id="fldNewChild" runat="server" visible="false" style="border-color: #FFFFFF;height:auto;">
              <legend><strong style="color: #666666">Child</strong></legend>
               <asp:GridView ID="gdvNewAccount" runat="server"  DataSourceID="sdsNewAccount"
                  AutoGenerateColumns="false" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                  AlternatingRowStyle-CssClass="alt" GridLines="None"  BorderStyle="None" >
                    <Columns>
                        <asp:BoundField DataField="ACCNT_NO" HeaderText="Wallet" 
                                SortExpression="ACCNT_NO" />
                        <asp:BoundField DataField="CLINT_NAME" HeaderText="Client Name" 
                                SortExpression="CLINT_NAME" />   
                        <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank"
                                SortExpression="RANK_TITEL" />               
                   </Columns>
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle Font-Bold="False" />
                    <AlternatingRowStyle CssClass="alt" />          
            </asp:GridView>
           </fieldset>
          </td>
        </tr>
        </table> 
      </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
