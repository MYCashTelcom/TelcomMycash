<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmMngCilentList2.aspx.cs"
    Inherits="Forms_frmMngCilentList2" Title="Manage Client Register" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KYC Update</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" /> 
    <style type="text/css">
      .TestRow
      {
      	font-size:6px;
      	
      	}
    </style>   
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btnClinetList" runat="server" OnClick="btnClinetList_Click" Text="Account List"
        BackColor="LightSteelBlue" Visible="false" BorderColor="LightSlateGray" BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black"  />
    <asp:Button ID="btnNewClient" runat="server" OnClick="btnNewClient_Click" Text="New Account"
        BackColor="LightSteelBlue" BorderColor="LightSlateGray" 
        BorderStyle="Solid" Font-Bold="False"
        ForeColor="Black" Visible="False" />
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           

                <asp:SqlDataSource ID="sdsClientList" runat="server" SelectCommand='SELECT * FROM "CLIENT_LIST"'                    
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                    InsertCommand='INSERT INTO CLIENT_LIST(CLINT_FATHER_NAME, CLINT_MOTHER_NAME, CLIENT_DOB, CLIENT_OFFIC_ADDRESS, OCCUPATION, PUR_OF_TRAN, CLINT_ID, CLINT_NAME, CLINT_PASS, CLINT_ADDRESS1, CLINT_ADDRESS2, CLINT_TOWN, CLINT_POSTCODE, CLINT_CITY, CLINT_CONTACT_F_NAME, CLINT_CONTACT_M_NAME, CLINT_CONTACT_L_NAME, CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL, CLINT_LAND_LINE, CLINT_MOBILE, CLINT_FAX, CREATION_DATE, CLINT_M_NAME, CLINT_L_NAME, CLINT_CONTACT_TITLE, CLINT_PASSPORT_NO, CLI_ZONE_ID, CLINET_RSP_CODE, CLIENT_RSP_NAME, DISTRIBUTOR_NAME, DISTRIBUTOR_CODE, DISTRIBUTOR_ZONE_ID, OWNER_NAME, OWNER_MOBILE, OWNER_NID) 
                                                    VALUES (:CLINT_FATHER_NAME, :CLINT_MOTHER_NAME, :CLIENT_DOB, :CLIENT_OFFIC_ADDRESS, :OCCUPATION, :PUR_OF_TRAN, :CLINT_ID, :CLINT_NAME, :CLINT_PASS, :CLINT_ADDRESS1, :CLINT_ADDRESS2, :CLINT_TOWN, :CLINT_POSTCODE, :CLINT_CITY, :CLINT_CONTACT_F_NAME, :CLINT_CONTACT_M_NAME, :CLINT_CONTACT_L_NAME, :CLINT_JOB_TITLE, :CLINT_CONTACT_EMAIL, :CLINT_LAND_LINE, :CLINT_MOBILE, :CLINT_FAX, :CREATION_DATE, :CLINT_M_NAME, :CLINT_L_NAME, :CLINT_CONTACT_TITLE, :CLINT_PASSPORT_NO, :CLI_ZONE_ID, :CLINET_RSP_CODE, :CLIENT_RSP_NAME, :DISTRIBUTOR_NAME, :DISTRIBUTOR_CODE, :DISTRIBUTOR_ZONE_ID, :OWNER_NAME, :OWNER_MOBILE, :OWNER_NID)'
                    ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                    DeleteCommand='DELETE FROM "CLIENT_LIST" WHERE "CLINT_ID" = :CLINT_ID'>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="CLINT_ID"></asp:Parameter>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Type="String" Name="CLINT_ID"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_PASS"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_ADDRESS1"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_ADDRESS2"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_TOWN"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_POSTCODE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_CITY"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_CONTACT_F_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_CONTACT_M_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_CONTACT_L_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_JOB_TITLE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_CONTACT_EMAIL"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_LAND_LINE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_MOBILE"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_FAX"></asp:Parameter>
                        <asp:Parameter Type="DateTime" Name="CREATION_DATE"></asp:Parameter>
                        <asp:Parameter Name="CLINT_M_NAME" />
                        <asp:Parameter Name="CLINT_L_NAME" />
                        <asp:Parameter Name="CLINT_CONTACT_TITLE" />
                        <asp:Parameter Name="CLINT_PASSPORT_NO" />
                        <asp:Parameter Name="CLI_ZONE_ID" />
                        <asp:Parameter Name="CLINET_RSP_CODE" />
                        <asp:Parameter Name="CLIENT_RSP_NAME" />
                        <asp:Parameter Name="DISTRIBUTOR_NAME" />
                        <asp:Parameter Name="DISTRIBUTOR_CODE" />
                        <asp:Parameter Name="DISTRIBUTOR_ZONE_ID" />
                        <asp:Parameter Name="OWNER_NAME" />
                        <asp:Parameter Name="OWNER_MOBILE" />
                        <asp:Parameter Name="OWNER_NID" />
                        
                        <asp:Parameter Type="DateTime" Name="CLIENT_DOB"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_FATHER_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLINT_MOTHER_NAME"></asp:Parameter>
                        <asp:Parameter Type="String" Name="CLIENT_OFFIC_ADDRESS"></asp:Parameter>
                        <asp:Parameter Type="String" Name="OCCUPATION"></asp:Parameter>
                        <asp:Parameter Type="String" Name="PUR_OF_TRAN"></asp:Parameter>                        
                    </InsertParameters>
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="sdsCountryList" runat="server" SelectCommand='SELECT "COUNTRY_ID", "COUNTRY_NMAE" FROM "COUNTRY_LIST"'
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" ConnectionString="<%$ ConnectionStrings:oracleConString %>">
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="sdsClientZone" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT * FROM CLIENT_ZONE C WHERE C.CLI_ZONE_TYPE='ARE'">
                </asp:SqlDataSource>
   
                <asp:SqlDataSource ID="sdsDistriZone" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                    ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" SelectCommand="SELECT * FROM CLIENT_ZONE C WHERE C.CLI_ZONE_TYPE='ZON'">
                </asp:SqlDataSource>
            <div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div style="background-color: royalblue; text-align: left;">
                            <strong><span style="color: white; font-size: 11px;"> Manage KYC Update </span></strong>
                        </div>
                        <div style=" width:700px; float:left;">
                          <fieldset  style="border-color: #FFFFFF; width:700px;height:auto;">
                           <legend><strong style="color: #666666; font-size:12px;">KYC Update</strong></legend>
                                <table width="710px" style="font-size:12px; background-color:#fff; ">
                                    <tr>
                                        <td colspan="4" align="left" style="font-size: 11px; font-weight: bold; color: Red; padding-left: 5px;"   >
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Client Name: 
                                        </td>                                       
                                        <td align="center">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        </td>
                                       <td align="right">
                                           Mobile Number:
                                       </td>  
                                       <td align="right">
                                           <asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
                                           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                       </td>
                                    </tr>
                                </table>
                            <asp:FormView ID="FormView1" runat="server" DataKeyNames="CLINT_ID"  
                                  DataSourceID="SqlDataSource1" DefaultMode="Edit"
                             Width="700px" AllowPaging="true"   OnItemUpdated="FormView1_ItemUpdated"
                              ondatabound="FormView1_DataBound"  onitemupdating="FormView1_ItemUpdating" 
                                BorderStyle="None"  BorderColor="#E0E0E0" CssClass="mGrid" 
                                  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  >
                              
                                <PagerTemplate>
                                
                                    <div  style=" width:700px;  padding-top:5px;  color: #fff; 
                                     padding-bottom: 5px; vertical-align: top;">                                     
                                            <asp:LinkButton ID="btnPrev" runat="server"   CommandArgument="Prev" CommandName="Page"
                                             Font-Size="12px" ForeColor="royalblue" >Previous</asp:LinkButton>  
                                             
                                        <asp:LinkButton ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                            Font-Size="12px" ForeColor="royalblue" >Next</asp:LinkButton>
                                    </div>
                                </PagerTemplate>
                                <ItemTemplate>
                                    <table style="font-size: 14px; padding-left: 15px;" width="700px">
                                        <tr>
                                            <td align="right">
                                                CID: 
                                            </td>                                            
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Account Name: 
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                Account Code: 
                                            </td>
                                            <td>
                                             <asp:DropDownList ID="ddlPurOfTran1" runat="server" AppendDataBoundItems="true" DataTextField="PUR_OF_TRAN" DataValueField="PUR_OF_TRAN" SelectedValue='<%# Bind("PUR_OF_TRAN") %>' > 
                                                <asp:ListItem Value="" Selected="True" Text=" "></asp:ListItem>
                                                <asp:ListItem Value="Personal" Text="Personal"></asp:ListItem>
                                                <asp:ListItem Value="Business" Text="Business"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Present Address: 
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                Permanent Adddress:
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                               Post Code: 
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                City:
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Area: 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList63" runat="server" DataSourceID="sdsClientZone"
                                                    SelectedValue='<%# Bind("CLI_ZONE_ID") %>' DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                               RSP Code: 
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                RSP Name: 
                                            </td>
                                            <td></td>
                                            <td align="right">
                                                Distributor Code: 
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Distributor Name: 
                                            </td>
                                            <td></td>
                                            <td align="right">
                                                Zone: 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList64" runat="server" DataSourceID="sdsDistriZone"
                                                    DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Owner Name: 
                                            </td>
                                            <td></td>
                                            <td align="right">
                                                Owner Mobile: 
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Owner National ID: 
                                            </td>
                                            <td></td>
                                            <td align="right">
                                                Contact Person:
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                               Contact Email: 
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" Font-Bold="True" Font-Size="14px"></asp:LinkButton>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                               <PagerSettings Position="Top" />
                                <RowStyle CssClass="alt" HorizontalAlign="Right" VerticalAlign="Top" />
                                <RowStyle VerticalAlign="Top" />
                                <EditItemTemplate>
                                    <table cellspacing="0px" cellpadding="0px"  border="0px" width="700px">
                                     <tr>
                                        <td>
                                        <asp:TextBox ID="frmVwClientID" Visible="false" Enabled="false" Text='<%# Bind("CLINT_ID") %>' 
                                                    runat="Server" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                     </tr>
                                        <tr>
                                            <td >
                                                 Mobile Number: 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox28" MaxLength="14" Text='<%# Bind("CLINT_MOBILE") %>'  Enabled="false" runat="Server" />
                                            </td>
                                            <td align="right">
                                                Name of the Client: 
                                            </td>
                                            
                                            <td align="center">
                                                <asp:TextBox ID="TextBox21" Text='<%# Bind("CLINT_NAME") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Father's Name: 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox20" Text='<%# Bind("CLINT_FATHER_NAME") %>' runat="Server" />
                                            </td>
                                            <td align="right">
                                                Mother's Name: 
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="TextBox22" Text='<%# Bind("CLINT_MOTHER_NAME") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Date of Birth: 
                                            </td>
                                            
                                            <td>
                                            <asp:TextBox ID="TextBox25" Text='<%# Bind("CLIENT_DOB", "{0:dd-MMM-yyyy}") %>' runat="Server" />
                                            </td>
                                             <td align="right">
                                                Occupation: 
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="TextBox26" Text='<%# Bind("OCCUPATION") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               Work/Edu/Business:
                                            </td>
                                            <td colspan="3" align="left">
                                               <asp:TextBox runat="server" Width="460" ID="txtWorkEduBusines" Text='<%# Bind("WORK_EDU_BUSINESS") %>'>'></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                         <tr>
                                            <td  valign="top">
                                                Purpose of Transction: 
                                            </td>
                                            
                                            <td colspan="3">
                                               <asp:dropdownlist id="ddlPurOfTran"  runat="server" AppendDataBoundItems="true" DataTextField="PUR_OF_TRAN" DataValueField="PUR_OF_TRAN" SelectedValue='<%# Eval("PUR_OF_TRAN") %>'> 
                                                <asp:listitem value="" selected="true" text=""></asp:listitem>
                                                <asp:listitem value="Personal" text="Personal"></asp:listitem>
                                                <asp:listitem value="Business" text="Business"></asp:listitem>
                                                <asp:listitem value="Self" text="Self"></asp:listitem>
                                                </asp:dropdownlist>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  valign="top">
                                               Official Address:
                                            </td>
                                           
                                            <td colspan="3">
                                                <asp:TextBox ID="TextBox27" Width="460" TextMode="MultiLine" Text='<%# Bind("CLIENT_OFFIC_ADDRESS") %>' runat="Server" />
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td valign="top">
                                                Present Address:
                                            </td>
                                            
                                            <td colspan="3">
                                                <asp:TextBox ID="TextBox23" Width="460" TextMode="MultiLine" Text='<%# Bind("CLINT_ADDRESS1") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                               Permanent Adddress: 
                                            </td>
                                            
                                            <td colspan="3">
                                                <asp:TextBox ID="TextBox24" Width="460" TextMode="MultiLine" Text='<%# Bind("CLINT_ADDRESS2") %>' runat="Server" />
                                            </td>
                                        </tr>                                  
                                        <%--<tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Post Code: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox25" Text='<%# Bind("CLINT_POSTCODE") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">City: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox26" Text='<%# Bind("CLINT_CITY") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Area: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList61" runat="server" DataSourceID="sdsClientZone"
                                                    SelectedValue='<%# Bind("CLI_ZONE_ID") %>' DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">RSP Code: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox27" Text='<%# Bind("CLINET_RSP_CODE") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">RSP Name: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox28" Text='<%# Bind("CLIENT_RSP_NAME") %>' runat="Server" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Distributor Code: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox29" Text='<%# Bind("DISTRIBUTOR_CODE") %>' runat="Server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Distributor Name: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox31" Text='<%# Bind("DISTRIBUTOR_NAME") %>' runat="Server" />
                                            </td>
                                        </tr>
                                       <tr>
                                            <td align="right">
                                                <span style="font-size: 14px; font-weight: bold;">Zone: </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList63" runat="server" DataSourceID="sdsDistriZone"
                                                    DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td></td>
                                            <td align="left">
                                            <asp:Button ID="UpdateButton" Text="Update" CausesValidation="true" CommandName="Update"
                                                    runat="server"  Font-Size="12px" />
                                                    
                                                  <asp:Button ID="CancelUpdateButton" CausesValidation="false" Text="Cancel"
                                                    CommandName="Cancel" runat="server"  Font-Size="12px" />
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                            <span style="font-size: 12px;">Page:
                                            <%#FormView1.PageIndex+1%>
                                         </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                               <PagerStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:FormView>
                         <div> 
                             <asp:GridView ID="grdBankAccount" runat="server" AllowPaging="True"  
                                 PageSize="3" Width="714px" AllowSorting="True"  AutoGenerateColumns="False"   
                                DataSourceID="sdsBankAcc"   HeaderStyle-Font-Bold="false" DataKeyNames="BANK_ACCNT_ID"  
                                    onrowediting="grdBankAccount_RowEditing" onrowcancelingedit="grdBankAccount_RowCancelingEdit" 
                                    onrowupdated="grdBankAccount_RowUpdated"   BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>                                        
                                        <asp:BoundField DataField="BANK_ACCNT_ID" HeaderText="Bank Acc ID" 
                                            ReadOnly="True"  SortExpression="BANK_ACCNT_ID"  Visible="false" />
                                             <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" 
                                            SortExpression="BANK_NAME" />
                                           <asp:BoundField DataField="BANK_BR_NAME" HeaderText="Bank Branch" 
                                            SortExpression="BANK_BR_NAME" />
                                        <asp:BoundField DataField="BANK_ACCNT_NO" HeaderText="Bank Acc No" 
                                            SortExpression="BANK_ACCNT_NO" />
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  
                                            SortExpression="REMARKS" >
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                    CommandName="Update"  Text="Update" 
                                                    />
                                                &#160;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" Text="Cancel" />
                                            </EditItemTemplate>
                                        
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" Text="Edit" />
                                            </ItemTemplate>
                                        
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                       <HeaderStyle Font-Bold="False" />
                                       <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                              
                               <div style="BACKGROUND-COLOR: royalblue;width: 714px;"><strong><span style="COLOR: white;font-size:12px;">Add New&nbsp;Bank&nbsp;</span></strong></div>
                                
                                <asp:DetailsView ID="dtvBankAccount" runat="server" 
                                 AutoGenerateRows="False"  DataKeyNames="CLIENT_ID"  
                                DefaultMode="Insert" DataSourceID="sdsBankAccount" 
                                 oniteminserted="dtvBankAccount_ItemInserted" oniteminserting="dtvBankAccount_ItemInserting"
                                CssClass="mGrid" PagerStyle-CssClass="pgr"  
                                 AlternatingRowStyle-CssClass="alt" GridLines="None" BorderStyle="None" 
                                 Width="400px"  >
                                    <PagerStyle CssClass="pgr" />
                                          <Fields>
                                        <asp:BoundField DataField="CLIENT_ID" Visible="false" HeaderText="Client ID" 
                                            SortExpression="CLIENT_ID" />
                                        <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name "                                             SortExpression="BANK_NAME" />    
                                        <asp:BoundField DataField="BANK_BR_NAME" HeaderText="Branch Name" 
                                            SortExpression="BANK_BR_NAME" />
                                        <asp:BoundField DataField="BANK_ACCNT_NO" HeaderText="Account No" 
                                            SortExpression="BANK_ACCNT_NO" />
                                        <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                                                                                      
                                            <EditItemTemplate>
                                                                                              
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                            
                                              </EditItemTemplate>
                                        
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="txtBankAccRemarks" TextMode="MultiLine" Height="23px" 
                                                    Width="180px" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                            
                                              </InsertItemTemplate>
                                        
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                            
                                              </ItemTemplate>
 
                                        </asp:TemplateField>
                                        <asp:CommandField ShowInsertButton="True" ButtonType="Button" />
                                    </Fields>
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:DetailsView>
                                
                            </div>                            
                            <div>
                             
                                 <asp:GridView ID="grdClientIdentification" runat="server" AllowPaging="True" PageSize="3"  Width="714px" 
                                    AllowSorting="True" DataSourceID="sdsCIden"  AutoGenerateColumns="False" DataKeyNames="CLINT_IDENT_ID" 
                                    onrowcancelingedit="grdClientIdentification_RowCancelingEdit"  onrowediting="grdClientIdentification_RowEditing" 
                                    onrowupdated="grdClientIdentification_RowUpdated" BorderColor="White" CssClass="mGrid" 
                                    PagerStyle-CssClass="pgr" 
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CLINT_IDENT_ID" SortExpression="CLINT_IDENT_ID" 
                                            Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("CLINT_IDENT_ID") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CLINT_IDENT_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Identification Name" 
                                            SortExpression="IDNTIFCTION_ID">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList6" DataTextField="IDNTIFCTION_NAME" 
                                                    DataValueField="IDNTIFCTION_ID" runat="server" 
                                                    DataSourceID="sdsClientIdentificationSetUp" 
                                                    SelectedValue='<%# Bind("IDNTIFCTION_ID") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList id="DropDownList5" runat="server" DataValueField="IDNTIFCTION_ID" DataTextField="IDNTIFCTION_NAME"
                                                 DataSourceID="sdsClientIdentificationSetUp" 
                                                SelectedValue='<%# Bind("IDNTIFCTION_ID") %>' Enabled="False"></asp:DropDownList>
                                            </ItemTemplate>                                        
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CLINT_IDENT_NAME" HeaderText="ID" 
                                            SortExpression="CLINT_IDENT_NAME" />
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                                            SortExpression="REMARKS" />
                                        <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                    </Columns>  
                                     <PagerStyle CssClass="pgr" />
                                     <AlternatingRowStyle CssClass="alt" />                                  
                                </asp:GridView>
                                
                                <div style="BACKGROUND-COLOR: royalblue;width: 714px;"><strong><span style="COLOR: white;font-size:12px;">Add New&nbsp;Identification&nbsp;</span></strong></div>
                                
                            <asp:DetailsView ID="dtvClientIdentification" runat="server" 
                                     AutoGenerateRows="False" DataKeyNames="CLINT_IDENT_ID" 
                                         DataSourceID="sdsClientIdentification" DefaultMode="Insert" oniteminserted="dtvClientIdentification_ItemInserted" 
                                    oniteminserting="dtvClientIdentification_ItemInserting" CssClass="mGrid" 
                                     PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                                     GridLines="None" BorderStyle="None" Width="400px">
                                          <PagerStyle CssClass="pgr" />
                                         <Fields>
                                             <asp:BoundField DataField="CLIENT_ID" HeaderText="Client ID"  Visible="false"
                                                 SortExpression="CLIENT_ID" />
                                             <asp:TemplateField HeaderText="Identification" 
                                                 SortExpression="IDNTIFCTION_ID">
                                                 <EditItemTemplate>
                                                     <asp:DropDownList ID="DropDownList1" runat="server"
                                                      DataSourceID="sdsClientIdentificationSetUp" DataTextField="IDNTIFCTION_NAME" 
                                                         DataValueField="IDNTIFCTION_ID" SelectedValue='<%# Bind("IDNTIFCTION_ID") %>' >
                                                     </asp:DropDownList>
                                                 </EditItemTemplate>
                                                 <InsertItemTemplate>
                                                     <asp:DropDownList ID="DropDownList2" runat="server" 
                                                      DataSourceID="sdsClientIdentificationSetUp" DataTextField="IDNTIFCTION_NAME" 
                                                         DataValueField="IDNTIFCTION_ID" SelectedValue='<%# Bind("IDNTIFCTION_ID") %>'>
                                                     </asp:DropDownList>
                                                 </InsertItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:DropDownList ID="DropDownList3" runat="server"
                                                      DataSourceID="sdsClientIdentificationSetUp" DataTextField="IDNTIFCTION_NAME" 
                                                         DataValueField="IDNTIFCTION_ID" SelectedValue='<%# Bind("IDNTIFCTION_ID") %>'>
                                                     </asp:DropDownList>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="CLINT_IDENT_NAME" HeaderText="ID" 
                                                 SortExpression="CLINT_IDENT_NAME" />
                                             <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <InsertItemTemplate>
                                                     <asp:TextBox ID="TextBox1" TextMode="MultiLine" Height="22px" Width="180px" 
                                                         runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                 </InsertItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>                                             
                                             <asp:CommandField ShowInsertButton="True" ButtonType="Button" />
                                         </Fields>
                                         <AlternatingRowStyle CssClass="alt" />
                                     </asp:DetailsView>
                            </div>
                            <div>
                                <asp:GridView ID="grdIntroducerInfo" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" DataSourceID="sdsIntroInfo" 
                                    AutoGenerateColumns="False" DataKeyNames="INTRODCR_ID" onrowcancelingedit="grdIntroducerInfo_RowCancelingEdit"  Width="714px"
                                     onrowediting="grdIntroducerInfo_RowEditing" onrowupdated="grdIntroducerInfo_RowUpdated" BorderColor="White" CssClass="mGrid" 
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:BoundField DataField="INTRODCR_ID" HeaderText="INTRODCR_ID" 
                                            ReadOnly="True" SortExpression="INTRODCR_ID" Visible="False" />
                                        <asp:BoundField DataField="INTRODCR_NAME" HeaderText="Introducer Name"  
                                            SortExpression="Introducer Name"  ControlStyle-Width="100px"/>
                                        <asp:BoundField DataField="INTRODCR_MOBILE" HeaderText="Mobile" 
                                            SortExpression="INTRODCR_MOBILE" ControlStyle-Width="100px" />
                                        <asp:BoundField DataField="INTRODCR_ADDRESS" HeaderText="Address"  
                                            SortExpression="INTRODCR_ADDRESS" ControlStyle-Width="100px" />
                                        <asp:BoundField DataField="INTRODCR_OCCUPATION" ControlStyle-Width="100px"  
                                            HeaderText="Occupation" SortExpression="INTRODCR_OCCUPATION" />
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  
                                            SortExpression="REMARKS" ControlStyle-Width="100px" />
                                        <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                     <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                                <div style="BACKGROUND-COLOR: royalblue;width: 714px;"><strong><span style="COLOR: white;font-size:12px;">Add New&nbsp;Introducer&nbsp;</span></strong></div>
                                  
                                 <asp:DetailsView ID="dtvIntroducerInfo" runat="server" 
                                    AutoGenerateRows="False" DataKeyNames="INTRODCR_ID" 
                                  DefaultMode="Insert" DataSourceID="sdsIntroducerInfo" oniteminserted="dtvIntroducerInfo_ItemInserted" 
                                    oniteminserting="dtvIntroducerInfo_ItemInserting" CssClass="mGrid" 
                                    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                                     GridLines="None" BorderStyle="None" Width="400px">
                                          <PagerStyle CssClass="pgr" />
                                              <Fields>
                                                   <asp:BoundField DataField="CLIENT_ID" HeaderText="Client ID" Visible="false" 
                                                      SortExpression="CLIENT_ID" />
                                                  <asp:BoundField DataField="INTRODCR_NAME" HeaderText="Introducer Name" 
                                                      SortExpression="INTRODCR_NAME" />
                                                  <asp:BoundField DataField="INTRODCR_MOBILE" HeaderText="Mobile" 
                                                      SortExpression="INTRODCR_MOBILE" />
                                                  <asp:BoundField DataField="INTRODCR_ADDRESS" HeaderText="Address" 
                                                      SortExpression="INTRODCR_ADDRESS" />
                                                  <asp:BoundField DataField="INTRODCR_OCCUPATION" 
                                                      HeaderText="Occupation" SortExpression="INTRODCR_OCCUPATION" />
                                                  <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <InsertItemTemplate>
                                                          <asp:TextBox ID="TextBox1" TextMode="MultiLine" Height="21px" Width="180px" 
                                                              runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                      </InsertItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  
                                                  </asp:TemplateField>
                                                  <asp:CommandField ShowInsertButton="True" ButtonType="Button" />
                                              </Fields>
                                      <AlternatingRowStyle CssClass="alt" />
                                  </asp:DetailsView>
                            </div>                            
                            <div>                           
                                <asp:GridView ID="grdNomineeInfo" runat="server" AllowPaging="True"  PageSize="3" AllowSorting="True" DataSourceID="sdsNomiInfo" 
                                    AutoGenerateColumns="False" DataKeyNames="NOMNE_ID"  onrowcancelingedit="grdNomineeInfo_RowCancelingEdit" 
                                    onrowediting="grdNomineeInfo_RowEditing"  onrowupdated="grdNomineeInfo_RowUpdated"  Width="714px"
                                    BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:BoundField DataField="NOMNE_ID" HeaderText="NOMNE_ID" ReadOnly="True" 
                                            SortExpression="NOMNE_ID" Visible="False" />
                                        <asp:BoundField DataField="NOMNE_NAME" HeaderText="Nominee Name" 
                                            SortExpression="Nominee Name" ControlStyle-Width="100px"  />                                        
                                        <asp:BoundField DataField="NOMNE_MOBILE" HeaderText="Mobile" 
                                            SortExpression="NOMNE_MOBILE" ControlStyle-Width="100px"  />
                                        <asp:BoundField DataField="RELATION" HeaderText="Relation" 
                                            SortExpression="RELATION" ControlStyle-Width="100px"  />
                                        <asp:BoundField DataField="PERCENTAGE" HeaderText="Percentage(%)" 
                                            SortExpression="PERCENTAGE" ControlStyle-Width="100px"  />
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                                            SortExpression="REMARKS" ControlStyle-Width="100px" />
                                        <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                              <div style="BACKGROUND-COLOR: royalblue; height: 14px; width: 714px;"><strong><span style="COLOR: white;font-size:12px;">Add New&nbsp;Nominee&nbsp;</span></strong></div>
                               
                               <asp:DetailsView ID="dtvNomineeInfo" runat="server" DefaultMode="Insert" 
                                    AutoGenerateRows="False" DataKeyNames="NOMNE_ID" Width="400px"
                                          DataSourceID="sdsNomineeInfo" 
                                    oniteminserted="dtvNomineeInfo_ItemInserted" oniteminserting="dtvNomineeInfo_ItemInserting"
                                          CssClass="mGrid" PagerStyle-CssClass="pgr"  
                                    AlternatingRowStyle-CssClass="alt" GridLines="None" BorderStyle="None" >
                                              <PagerStyle CssClass="pgr" />
                                              <Fields>
                                                  <asp:BoundField DataField="CLIENT_ID" HeaderText="Client ID" Visible="false" 
                                                      SortExpression="CLIENT_ID" />
                                                  <asp:BoundField DataField="NOMNE_NAME" HeaderText="Nominee Name" 
                                                      SortExpression="NOMNE_NAME" />
                                                  <asp:BoundField DataField="NOMNE_MOBILE" HeaderText="Mobile" 
                                                      SortExpression="NOMNE_MOBILE" />
                                                  <asp:BoundField DataField="RELATION" HeaderText="Relation" 
                                                      SortExpression="RELATION" />
                                                  <asp:BoundField DataField="PERCENTAGE" HeaderText="Percentage (%)" 
                                                      SortExpression="PERCENTAGE" />
                                                  <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <InsertItemTemplate>
                                                          <asp:TextBox ID="TextBox1" TextMode="MultiLine" Height="19px" Width="180px" 
                                                              runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                      </InsertItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:CommandField ShowInsertButton="True" ButtonType="Button" 
                                                      ShowDeleteButton="True" />
                                              </Fields>
                                              <AlternatingRowStyle CssClass="alt" />
                              </asp:DetailsView>
                            </div>
                          </fieldset>  
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            UpdateCommand='UPDATE CLIENT_LIST SET CLINT_NAME = :CLINT_NAME,                                                         
                                                        CLINT_ADDRESS1 = :CLINT_ADDRESS1, 
                                                        CLINT_ADDRESS2 = :CLINT_ADDRESS2,                                                         
                                                        OWNER_NID = :OWNER_NID,                                                        
                                                        CLINT_FATHER_NAME = :CLINT_FATHER_NAME,
                                                        CLINT_MOTHER_NAME = :CLINT_MOTHER_NAME,
                                                        CLIENT_DOB = :CLIENT_DOB,
                                                        CLIENT_OFFIC_ADDRESS = :CLIENT_OFFIC_ADDRESS,
                                                        OCCUPATION = :OCCUPATION,
                                                        CLINT_MOBILE = :CLINT_MOBILE,
                                                        REFERRED_MOBILE=:REFERRED_MOBILE,
                                                        WORK_EDU_BUSINESS=:WORK_EDU_BUSINESS,
                                                        PUR_OF_TRAN = :PUR_OF_TRAN,
                                                        SYS_USR_LOGIN_NAME=:SYS_USR_LOGIN_NAME
                                                        WHERE (CLINT_ID = :CLINT_ID)'>
                            <UpdateParameters>
                                <asp:Parameter Type="String" Name="CLINT_NAME"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLINT_ADDRESS1"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLINT_ADDRESS2"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLINT_ID" />
                                <asp:Parameter Type="String" Name="OWNER_NID" />
                                <asp:Parameter Type="String" Name="CLINT_MOBILE" />
                                <asp:Parameter Type="DateTime" Name="CLIENT_DOB"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLINT_FATHER_NAME"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLINT_MOTHER_NAME"></asp:Parameter>
                                <asp:Parameter Type="String" Name="CLIENT_OFFIC_ADDRESS"></asp:Parameter>
                                <asp:Parameter Type="String" Name="OCCUPATION"></asp:Parameter>
                                <asp:Parameter Type="String" Name="REFERRED_MOBILE"></asp:Parameter>
                                <asp:Parameter Type="String" Name="WORK_EDU_BUSINESS" />                                
                                <asp:Parameter Type="String" Name="PUR_OF_TRAN"></asp:Parameter>                           
                                <asp:Parameter Type="String" Name="SYS_USR_LOGIN_NAME" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        
                        <asp:SqlDataSource ID="sdsNomineeInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;NOMINEE_INFO&quot; WHERE &quot;NOMNE_ID&quot; = :NOMNE_ID" 
                            InsertCommand="INSERT INTO &quot;NOMINEE_INFO&quot; (&quot;NOMNE_ID&quot;, &quot;CLIENT_ID&quot;, &quot;NOMNE_NAME&quot;, &quot;NOMNE_MOBILE&quot;, &quot;RELATION&quot;, &quot;PERCENTAGE&quot;, &quot;REMARKS&quot;) VALUES (:NOMNE_ID, :CLIENT_ID, :NOMNE_NAME, :NOMNE_MOBILE, :RELATION, :PERCENTAGE, :REMARKS)" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;NOMINEE_INFO&quot;" 
                            UpdateCommand="UPDATE &quot;NOMINEE_INFO&quot; SET &quot;CLIENT_ID&quot; = :CLIENT_ID, &quot;NOMNE_NAME&quot; = :NOMNE_NAME, &quot;NOMNE_MOBILE&quot; = :NOMNE_MOBILE, &quot;RELATION&quot; = :RELATION, &quot;PERCENTAGE&quot; = :PERCENTAGE, &quot;REMARKS&quot; = :REMARKS WHERE &quot;NOMNE_ID&quot; = :NOMNE_ID">
                            <DeleteParameters>
                                <asp:Parameter Name="NOMNE_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CLIENT_ID" Type="String" />
                                <asp:Parameter Name="NOMNE_NAME" Type="String" />
                                <asp:Parameter Name="NOMNE_MOBILE" Type="String" />
                                <asp:Parameter Name="RELATION" Type="String" />
                                <asp:Parameter Name="PERCENTAGE" Type="Decimal" />
                                <asp:Parameter Name="REMARKS" Type="String" />
                                <asp:Parameter Name="NOMNE_ID" Type="String" />
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
                        <asp:SqlDataSource ID="sdsIntroducerInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;INTRODUCER_INFO&quot; WHERE &quot;INTRODCR_ID&quot; = :INTRODCR_ID" 
                            InsertCommand="INSERT INTO &quot;INTRODUCER_INFO&quot; (&quot;INTRODCR_ID&quot;, &quot;CLIENT_ID&quot;, &quot;INTRODCR_NAME&quot;, &quot;INTRODCR_MOBILE&quot;, &quot;INTRODCR_ADDRESS&quot;, &quot;INTRODCR_OCCUPATION&quot;, &quot;REMARKS&quot;) VALUES (:INTRODCR_ID, :CLIENT_ID, :INTRODCR_NAME, :INTRODCR_MOBILE, :INTRODCR_ADDRESS, :INTRODCR_OCCUPATION, :REMARKS)" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;INTRODUCER_INFO&quot;" 
                            UpdateCommand="UPDATE &quot;INTRODUCER_INFO&quot; SET &quot;CLIENT_ID&quot; = :CLIENT_ID, &quot;INTRODCR_NAME&quot; = :INTRODCR_NAME, &quot;INTRODCR_MOBILE&quot; = :INTRODCR_MOBILE, &quot;INTRODCR_ADDRESS&quot; = :INTRODCR_ADDRESS, &quot;INTRODCR_OCCUPATION&quot; = :INTRODCR_OCCUPATION, &quot;REMARKS&quot; = :REMARKS WHERE &quot;INTRODCR_ID&quot; = :INTRODCR_ID">
                            <DeleteParameters>
                                <asp:Parameter Name="INTRODCR_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CLIENT_ID" Type="String" />
                                <asp:Parameter Name="INTRODCR_NAME" Type="String" />
                                <asp:Parameter Name="INTRODCR_MOBILE" Type="String" />
                                <asp:Parameter Name="INTRODCR_ADDRESS" Type="String" />
                                <asp:Parameter Name="INTRODCR_OCCUPATION" Type="String" />
                                <asp:Parameter Name="REMARKS" Type="String" />
                                <asp:Parameter Name="INTRODCR_ID" Type="String" />
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
                        <asp:SqlDataSource ID="sdsBankAccount" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;BANK_ACCOUNT&quot; WHERE &quot;BANK_ACCNT_ID&quot; = :BANK_ACCNT_ID" 
                            InsertCommand="INSERT INTO &quot;BANK_ACCOUNT&quot; (&quot;BANK_ACCNT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;BANK_BR_NAME&quot;, &quot;BANK_ACCNT_NO&quot;,BANK_NAME, &quot;REMARKS&quot;) VALUES (:BANK_ACCNT_ID, :CLIENT_ID, :BANK_BR_NAME, :BANK_ACCNT_NO,:BANK_NAME, :REMARKS)"
                            
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;BANK_ACCOUNT&quot;" 
                            
                            UpdateCommand="UPDATE &quot;BANK_ACCOUNT&quot; SET &quot;CLIENT_ID&quot; = :CLIENT_ID, &quot;BANK_BR_NAME&quot; = :BANK_BR_NAME, &quot;BANK_ACCNT_NO&quot; = :BANK_ACCNT_NO,BANK_NAME=:BANK_NAME, &quot;REMARKS&quot; = :REMARKS WHERE &quot;BANK_ACCNT_ID&quot; = :BANK_ACCNT_ID">
                            <DeleteParameters>
                                <asp:Parameter Name="BANK_ACCNT_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CLIENT_ID" Type="String" />
                                <asp:Parameter Name="BANK_BR_NAME" Type="String" />
                                <asp:Parameter Name="BANK_ACCNT_NO" Type="String" />
                                <asp:Parameter Name="BANK_NAME" Type="String" />
                                <asp:Parameter Name="REMARKS" Type="String" />
                                <asp:Parameter Name="BANK_ACCNT_ID" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="BANK_ACCNT_ID" Type="String" />
                                <asp:Parameter Name="CLIENT_ID" Type="String" />
                                <asp:Parameter Name="BANK_NAME" Type="String" />
                                <asp:Parameter Name="BANK_BR_NAME" Type="String" />
                                <asp:Parameter Name="BANK_ACCNT_NO" Type="String" />
                                <asp:Parameter Name="REMARKS" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsClientIdentification" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;CLIENT_IDENTIFICATION&quot; WHERE &quot;CLINT_IDENT_ID&quot; = :original_CLINT_IDENT_ID" 
                            InsertCommand="INSERT INTO &quot;CLIENT_IDENTIFICATION&quot; (&quot;CLINT_IDENT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;CLINT_IDENT_NAME&quot;, &quot;REMARKS&quot;, &quot;IDNTIFCTION_ID&quot;) VALUES (:CLINT_IDENT_ID, :CLIENT_ID, :CLINT_IDENT_NAME, :REMARKS, :IDNTIFCTION_ID)" 
                            OldValuesParameterFormatString="original_{0}" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT &quot;CLINT_IDENT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;CLINT_IDENT_NAME&quot;, &quot;REMARKS&quot;, &quot;IDNTIFCTION_ID&quot; FROM &quot;CLIENT_IDENTIFICATION&quot;" 
                            UpdateCommand="UPDATE &quot;CLIENT_IDENTIFICATION&quot; SET &quot;CLIENT_ID&quot; = :CLIENT_ID, &quot;CLINT_IDENT_NAME&quot; = :CLINT_IDENT_NAME, &quot;REMARKS&quot; = :REMARKS, &quot;IDNTIFCTION_ID&quot; = :IDNTIFCTION_ID WHERE &quot;CLINT_IDENT_ID&quot; = :original_CLINT_IDENT_ID">
                            <DeleteParameters>
                                <asp:Parameter Name="original_CLINT_IDENT_ID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CLIENT_ID" Type="String" />
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
                        
                        <asp:SqlDataSource ID="sdsBankAcc" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;BANK_ACCOUNT&quot; WHERE &quot;BANK_ACCNT_ID&quot; = :original_BANK_ACCNT_ID" 
                            InsertCommand="INSERT INTO &quot;BANK_ACCOUNT&quot; (&quot;BANK_ACCNT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;BANK_BR_NAME&quot;, &quot;BANK_ACCNT_NO&quot;,BANK_NAME, &quot;REMARKS&quot;) VALUES (:BANK_ACCNT_ID, :CLIENT_ID, :BANK_BR_NAME, :BANK_ACCNT_NO,:BANK_NAME, :REMARKS)" 
                            OldValuesParameterFormatString="original_{0}" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT BANK_ACCNT_ID,CLIENT_ID,BANK_BR_NAME,BANK_ACCNT_NO,REMARKS FROM BANK_ACCOUNT " 
                            
                            UpdateCommand="UPDATE &quot;BANK_ACCOUNT&quot; SET &quot;BANK_BR_NAME&quot; = :BANK_BR_NAME, &quot;BANK_ACCNT_NO&quot; = :BANK_ACCNT_NO,BANK_NAME=:BANK_NAME, &quot;REMARKS&quot; = :REMARKS WHERE &quot;BANK_ACCNT_ID&quot; = :original_BANK_ACCNT_ID ">
                           
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
                                <asp:Parameter Name="BANK_NAME" Type="String" />
                                <asp:Parameter Name="BANK_BR_NAME" Type="String" />
                                <asp:Parameter Name="BANK_ACCNT_NO" Type="String" />
                                <asp:Parameter Name="REMARKS" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsNomiInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;NOMINEE_INFO&quot; WHERE &quot;NOMNE_ID&quot; = :original_NOMNE_ID" 
                            InsertCommand="INSERT INTO &quot;NOMINEE_INFO&quot; (&quot;NOMNE_ID&quot;, &quot;CLIENT_ID&quot;, &quot;NOMNE_NAME&quot;, &quot;NOMNE_MOBILE&quot;, &quot;RELATION&quot;, &quot;PERCENTAGE&quot;, &quot;REMARKS&quot;) VALUES (:NOMNE_ID, :CLIENT_ID, :NOMNE_NAME, :NOMNE_MOBILE, :RELATION, :PERCENTAGE, :REMARKS)" 
                            OldValuesParameterFormatString="original_{0}" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;NOMINEE_INFO&quot;" 
                            
                            UpdateCommand="UPDATE &quot;NOMINEE_INFO&quot; SET  &quot;NOMNE_NAME&quot; = :NOMNE_NAME, &quot;NOMNE_MOBILE&quot; = :NOMNE_MOBILE, &quot;RELATION&quot; = :RELATION, &quot;PERCENTAGE&quot; = :PERCENTAGE, &quot;REMARKS&quot; = :REMARKS WHERE &quot;NOMNE_ID&quot; = :original_NOMNE_ID">
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
                        <asp:SqlDataSource ID="sdsIntroInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;INTRODUCER_INFO&quot; WHERE &quot;INTRODCR_ID&quot; = :original_INTRODCR_ID" 
                            InsertCommand="INSERT INTO &quot;INTRODUCER_INFO&quot; (&quot;INTRODCR_ID&quot;, &quot;CLIENT_ID&quot;, &quot;INTRODCR_NAME&quot;, &quot;INTRODCR_MOBILE&quot;, &quot;INTRODCR_ADDRESS&quot;, &quot;INTRODCR_OCCUPATION&quot;, &quot;REMARKS&quot;) VALUES (:INTRODCR_ID, :CLIENT_ID, :INTRODCR_NAME, :INTRODCR_MOBILE, :INTRODCR_ADDRESS, :INTRODCR_OCCUPATION, :REMARKS)" 
                            OldValuesParameterFormatString="original_{0}" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT &quot;INTRODCR_ID&quot;, &quot;CLIENT_ID&quot;, &quot;INTRODCR_NAME&quot;, &quot;INTRODCR_MOBILE&quot;, &quot;INTRODCR_ADDRESS&quot;, &quot;INTRODCR_OCCUPATION&quot;, &quot;REMARKS&quot; FROM &quot;INTRODUCER_INFO&quot;" 
                            
                            
                            UpdateCommand="UPDATE &quot;INTRODUCER_INFO&quot; SET  &quot;INTRODCR_NAME&quot; = :INTRODCR_NAME, &quot;INTRODCR_MOBILE&quot; = :INTRODCR_MOBILE, &quot;INTRODCR_ADDRESS&quot; = :INTRODCR_ADDRESS, &quot;INTRODCR_OCCUPATION&quot; = :INTRODCR_OCCUPATION, &quot;REMARKS&quot; = :REMARKS WHERE &quot;INTRODCR_ID&quot; = :original_INTRODCR_ID">
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
                        <asp:SqlDataSource ID="sdsCIden" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            DeleteCommand="DELETE FROM &quot;CLIENT_IDENTIFICATION&quot; WHERE &quot;CLINT_IDENT_ID&quot; = :original_CLINT_IDENT_ID" 
                            InsertCommand="INSERT INTO &quot;CLIENT_IDENTIFICATION&quot; (&quot;CLINT_IDENT_ID&quot;, &quot;CLIENT_ID&quot;, &quot;CLINT_IDENT_NAME&quot;, &quot;REMARKS&quot;, &quot;IDNTIFCTION_ID&quot;) VALUES (:CLINT_IDENT_ID, :CLIENT_ID, :CLINT_IDENT_NAME, :REMARKS, :IDNTIFCTION_ID)" 
                            OldValuesParameterFormatString="original_{0}" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;CLIENT_IDENTIFICATION&quot;" 
                            
                            UpdateCommand="UPDATE CLIENT_IDENTIFICATION SET  CLINT_IDENT_NAME = :CLINT_IDENT_NAME,REMARKS = :REMARKS, IDNTIFCTION_ID = :IDNTIFCTION_ID WHERE (CLINT_IDENT_ID = :original_CLINT_IDENT_ID)">
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
                        <asp:SqlDataSource ID="sdsClientIdentificationSetUp" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                            SelectCommand="SELECT * FROM &quot;IDENTIFICATION_SETUP&quot;"></asp:SqlDataSource>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div style="background-color: royalblue; text-align: left;">
                            <strong><span style="color: white; font-size: 11px;">  :: Add New  Account  ::</span></strong></div>
                        <div style="padding-top: 5px;">
                            <div style="margin-left: 15px; padding-left: 15px; padding-top: 5px; padding-right: 15px;
                                padding-bottom: 5px; width: 952px;">
                                <table cellpadding="0" cellspacing="4">
                                    <tr>
                                        <td align="center" style="font-size: 11px; font-weight: bold; color: Red; padding-left: 5px;"
                                            class="style1">
                                            <asp:Label ID="lblMsg1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="margin-left: 15px; padding-right: 15px; padding-top: 0px; width: 430px;
                            padding-bottom: 15px; margin-top: 15px;">
                            <asp:DetailsView ID="dtvNewClient" runat="server" DataSourceID="sdsClientList" DataKeyNames="CLINT_ID"
                                BorderColor="#E0E0E0" DefaultMode="Insert" AutoGenerateRows="False" Width="430px"
                                Font-Size="14px" GridLines="None" Height="360px"
                                oniteminserted="dtvNewClient_ItemInserted" CellPadding="1"><%----%> 
                                <%--<FieldHeaderStyle Font-Size="X-Small" />--%>
                                <Fields>
                                    <asp:BoundField ReadOnly="True" DataField="CLINT_ID" Visible="False" SortExpression="CLINT_ID"
                                        HeaderText="CLINT_ID"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Name of the Client:     " SortExpression="CLINT_NAME"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:TextBox>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            &#160;<asp:Label ID="Label3" runat="server" Text="**" ForeColor="Red" Font-Bold="True"
                                                Font-Size="12px"></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("CLINT_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemStyle Wrap="False" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Owner Name:     " SortExpression="OWNER_NAME"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("OWNER_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("OWNER_NAME") %>'></asp:TextBox>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <%--&#160;<asp:Label ID="Label32" runat="server" Text="**" ForeColor="Red" Font-Bold="True"
                                                Font-Size="12px"></asp:Label>--%>
 
                                            

                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("OWNER_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CLINT_FATHER_NAME" HeaderText="Father's Name:     " SortExpression="CLINT_FATHER_NAME">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CLINT_MOTHER_NAME" HeaderText="Mother's Name:     " SortExpression="CLINT_MOTHER_NAME">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date of Birth:     " SortExpression="CLIENT_DOB"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox89" runat="server" Text='<%# Bind("CLIENT_DOB", "{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox86" runat="server" Text='<%# Bind("CLIENT_DOB", "{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                            
                                                                                    
                                            
 
                                            
 
                                            

                                            
                                                                                    
                                           <%-- <cc1:GMDatePicker ID="gmdpBirthDate" runat="server" AutoPosition="True" CalendarOffsetX="-200px"
                                                CalendarOffsetY="25px" CalendarTheme="Green" CalendarWidth="200px" CallbackEventReference=""
                                                Culture="English (United States)" Date='<%# Bind("CLIENT_DOB") %>' DateFormat="dd-MMM-yyyy"
                                                DateString='<%# Bind("CLIENT_DOB") %>' EnableDropShadow="True" MaxDate="9999-12-31"
                                                MinDate="" NextMonthText="&gt;" NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True"
                                                Style="position: relative" TextBoxWidth="100" ZIndex="1">
                                            </cc1:GMDatePicker>--%>
 
                                            

                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label88" runat="server" Text='<%# Bind("CLIENT_DOB", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OCCUPATION" HeaderText="Occupation:     " SortExpression="OCCUPATION">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Client Middle Name:     " SortExpression="CLINT_M_NAME"
                                        Visible="False">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("CLINT_M_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:BoundField DataField="CLINT_L_NAME" HeaderText="Client Last Name" SortExpression="CLINT_L_NAME"
                                        Visible="False"></asp:BoundField><asp:BoundField DataField="CLINT_PASSPORT_NO" HeaderText="Passport No" SortExpression="CLINT_PASSPORT_NO"
                                        Visible="False">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:BoundField><asp:TemplateField SortExpression="CLINT_PASS" Visible="False" HeaderText="CLINT_PASS">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox runat="server" Text='<%# Bind("CLINT_GENDER") %>' ID="TextBox4"></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:DropDownList ID="DropDownList21" runat="server" SelectedValue='<%# Bind("CLINT_GENDER") %>'>
                                                
                                                                                              
                                                
 
                                                
 
                                                

                                                
                                                                                              
                                                <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                                                
                                                                                              
                                                
 
                                                
 
                                                

                                                
                                                                                              
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                
                                                                                              
                                                
 
                                                
 
                                                

                                                
                                                                                              
                                                <asp:ListItem Value="O">Other</asp:ListItem>
                                                
                                                                                      
                                                
 
                                                
 
                                                

                                                
                                                                                      
                                            </asp:DropDownList>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CLINT_GENDER") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="Official Address:     " SortExpression="CLIENT_OFFIC_ADDRESS"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox40" runat="server" Text='<%# Bind("CLIENT_OFFIC_ADDRESS") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox47" TextMode="MultiLine" runat="server" Text='<%# Bind("CLIENT_OFFIC_ADDRESS") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Labe49" runat="server" Text='<%# Bind("CLIENT_OFFIC_ADDRESS") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="Present Address:     " SortExpression="CLINT_ADDRESS1"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox37" TextMode="MultiLine" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Labe39" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="Permanent Adddress:     " SortExpression="CLINT_ADDRESS2"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox7" TextMode="MultiLine" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("CLINT_ADDRESS2") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="Mobile Number:     " SortExpression="CLINT_MOBILE"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox6" MaxLength="14" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:TextBox>
                                            
                                                                                      
                                               
                                            
                                                                                       
                                            
 
                                            
 
                                            
 
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                               
                                            
                                                                                       
                                            <asp:Label ID="Label27" runat="server" ForeColor="Red" Font-Bold="True"
                                                Font-Size="12px" Text=' (+8801.........)'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("CLINT_MOBILE") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="Purpose of Transction:     " SortExpression="PUR_OF_TRAN"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox70" runat="server" Text='<%# Bind("PUR_OF_TRAN") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox77" TextMode="MultiLine" runat="server" Text='<%# Bind("PUR_OF_TRAN") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label79" runat="server" Text='<%# Bind("PUR_OF_TRAN") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="CLINT_TOWN :     " SortExpression="CLINT_TOWN"
                                        Visible="False">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CLINT_PASS") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("CLINT_TOWN") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:BoundField Visible="false" DataField="CLINT_POSTCODE" SortExpression="CLINT_POSTCODE" HeaderText="Post Code:     "
                                        ItemStyle-Font-Bold="False" HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemStyle Font-Bold="False" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:BoundField><asp:TemplateField Visible="false" SortExpression="CLINT_CITY" HeaderText="City:     " HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <%--<EditItemTemplate>
                                            <asp:TextBox runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>' ID="TextBox10"></asp:TextBox>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>' ID="TextBox7"></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("CLINT_ADDRESS1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />--%>
 
                                        

                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_CITY") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CLINT_CITY") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label runat="server" Text='<%# Bind("CLINT_CITY") %>' ID="Label2"></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" SortExpression="CLI_ZONE_ID" HeaderText="Area:     " HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CLI_ZONE_ID") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:DropDownList ID="DropDownList16" runat="server" DataSourceID="sdsClientZone"
                                                SelectedValue='<%# Bind("CLI_ZONE_ID") %>' DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID">
                                                
                                                                                      
                                                
 
                                                
 
                                                

                                                
                                                                                      
                                            </asp:DropDownList>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:DropDownList ID="DropDownList15" runat="server" DataSourceID="sdsClientZone"
                                                DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("CLI_ZONE_ID") %>'>
                                                
                                                                                      
                                                
 
                                                
 
                                                

                                                
                                                                                      
                                            </asp:DropDownList>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="RSP Code:     " SortExpression="CLINET_RSP_CODE"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("CLINET_RSP_CODE") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="RSP Name:     " SortExpression="CLIENT_RSP_NAME"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("CLIENT_RSP_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
 
                                        
 
                                        

                                        
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="Distributor Code:     " SortExpression="DISTRIBUTOR_CODE"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:TextBox>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:TextBox>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <%--&#160;<asp:Label ID="Label30" runat="server" Text="**" ForeColor="Red" Font-Bold="True"
                                                Font-Size="12px"></asp:Label>--%>
 
                                            

                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
 
                                            
 
                                            

                                            
                                                                                      
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("DISTRIBUTOR_CODE") %>'></asp:Label>
                                            
                                                                              
                                            
 
                                            
 
                                            

                                            
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
 
                                        
 
                                        

                                        
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                     
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="Distributor Name:     " SortExpression="DISTRIBUTOR_NAME"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                    
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                          
                                                                                      
                                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:TextBox>
                                            
                                      
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                    
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                        
                                                                                      
                                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                     
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                   
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
                                         
                                                                                      
                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("DISTRIBUTOR_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
                                       
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
                                 
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
                                    
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="Zone:     " SortExpression="DISTRIBUTOR_ZONE_ID"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
                                    
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
                                           
                                                                                      
                                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'></asp:TextBox>
                                            
                                                                              
                                            
                                         
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
                                    
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
                                         
                                                                                      
                                            <asp:DropDownList ID="DropDownList22" runat="server" DataSourceID="sdsDistriZone"
                                                DataTextField="CLI_ZONE_TITLE" DataValueField="CLI_ZONE_ID" SelectedValue='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'>
                                                
                                                                                      
                                                
                                             
                                                                                      
                                            </asp:DropDownList>
                                            
                                                                              
                                            
                                      
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
                                      
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
                                          
                                                                                      
                                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("DISTRIBUTOR_ZONE_ID") %>'></asp:Label>
                                            
                                                                              
                                       
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                      
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
                                    
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="Owner Mobile:     " SortExpression="OWNER_MOBILE"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
                                     
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
                                        
                                                                                      
                                            <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:TextBox>
                                            
                                                                              
                                            
                                       
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                    
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
                                        
                                                                                      
                                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:TextBox>                                            
                                                                                                                               
                                            
                                                                                                                          
                                                                                                                            
                                            
                                                                                                                          
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
                                     
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
                                          
                                                                                      
                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("OWNER_MOBILE") %>'></asp:Label>
                                            
                                                                              
                                            
                                          
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
                                  
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
                                       
                                                                      
                                    </asp:TemplateField><asp:TemplateField HeaderText="National ID:     " SortExpression="OWNER_NID"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
                                     
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
                                       
                                                                                      
                                            <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:TextBox>
                                            
                                                                              
                                         
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
                                 
                                                                              
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
                                                                                    
                                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:TextBox>
                                            
                                                                              
                                            
                                         
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
                                    
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
                                          
                                                                                      
                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("OWNER_NID") %>'></asp:Label>
                                            
                                                                              
                                            
                                          
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
                                       
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                      
                                        
                                    
                                                                      
                                    </asp:TemplateField><asp:TemplateField Visible="false" HeaderText="Contact Person:     " SortExpression="CLINT_CONTACT_F_NAME"
                                        HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
                                    
                                                                              
                                        <EditItemTemplate>
                                            
                                                                                      
                                            
                                          
                                                                                      
                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
                                        
                                                                              
                                        </EditItemTemplate>
                                        
                                                                              
                                        
                                                                       
                                        <InsertItemTemplate>
                                            
                                                                                      
                                            
                                         
                                                                                      
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:TextBox>
                                            
                                                                              
                                            
                                          
                                                                              
                                        </InsertItemTemplate>
                                        
                                                                              
                                        
                                      
                                                                              
                                        <ItemTemplate>
                                            
                                                                                      
                                            
                                           
                                                                                      
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("CLINT_CONTACT_F_NAME") %>'></asp:Label>
                                            
                                                                              
                                            
                                          
                                                                              
                                        </ItemTemplate>
                                        
                                                                              
                                        
                                     
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                      
                                        
                                
                                                                      
                                    </asp:TemplateField><asp:BoundField DataField="CLINT_CONTACT_M_NAME" HeaderText="Contact Middle Name"
                                        SortExpression="CLINT_CONTACT_M_NAME" Visible="False">
                                        
                                                                              
                                        
                                   
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                              
                                        
                                     
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                                        
                                                                      
                                        
                                     
                                                                      
                                    </asp:BoundField><asp:BoundField DataField="CLINT_CONTACT_L_NAME" HeaderText="Contact Last Name" SortExpression="CLINT_CONTACT_L_NAME"
                                        Visible="False">
                                        
                                                                              
                                   
                                                                              
                                        <HeaderStyle HorizontalAlign="Right" />
                                        
                                                                      
                                        
                                    
                                                                      
                                    </asp:BoundField><asp:BoundField DataField="CLINT_JOB_TITLE" HeaderText="CLINT_JOB_TITLE" SortExpression="CLINT_JOB_TITLE"
                                        Visible="False" />
                                    <asp:BoundField Visible="false" DataField="CLINT_CONTACT_EMAIL" SortExpression="CLINT_CONTACT_EMAIL"
                                        HeaderText="Contact Email:     " HeaderStyle-Font-Bold="True">
                                        
                                                                              
                                        
                                     
                                                                              
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                        
                                                                      
                                        
                                     
                                                                      
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CLINT_LAND_LINE" SortExpression="CLINT_LAND_LINE" HeaderText="Land Line"
                                        Visible="False">
                                        
                                                                              
                                    
                                                                              
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                        
                                                                      
                                        
                                   
                                                                      
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="CLINT_FAX" SortExpression="CLINT_FAX" HeaderText="Fax"
                                        Visible="False">
                                        
                                                                              
                                        
                                      
                                                                              
                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                        
                                                                      
                                        
                                
                                                                      
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CREATION_DATE" Visible="False" SortExpression="CREATION_DATE"
                                        HeaderText="CREATION_DATE"></asp:BoundField>
                                    <asp:CommandField InsertText="Add Account" ButtonType="Button" ShowInsertButton="True">
                                        
                                                                              
                                        
                                     
                                                                              
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        
                                                                      
                                        
                                  
                                                                      
                                    </asp:CommandField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </asp:View>
                </asp:MultiView>  
            </div>
            
                           
            
            
            

            
                           
        </ContentTemplate>
        
        
        
        
    </asp:UpdatePanel>
        
    </form>
</body>
</html>
