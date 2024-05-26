<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKgdclReport.aspx.cs" Inherits="UBP_frmKgdclReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>REB Bill Inquiry Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

     <style type="text/css">
       .font_Color
       {
       	color:White;
       	}
       	.GridViewClass { width: 100%; background-color: #fff; margin: 1px 0 10px 0; 
        border: solid 1px #525252; border-collapse:collapse;
            text-align: left;
        }
            .GridViewClass td { padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
            .GridViewClass th
        {
	        padding: 4px 2px;
	        color: #fff;
	        background: url(grd_head1.png) activecaption repeat-x 50% top;
	        border-left: solid 0px #525252;
	        font-size: 11px;
         }
    </style> 

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
            AsyncPostBackTimeout="36000">
        </ajaxToolkit:ToolkitScriptManager>   
    <asp:UpdatePanel id="upnlREBBillPayInquiry" runat="server">
          <Triggers>
                <asp:PostBackTrigger ControlID="btnExportReport" />
            </Triggers>
    <contenttemplate>
        <%--<div class="row"  style="background-color: royalblue">
              <table style="width:100%">
       <tr  style="width:100%">
        <td  style="width:80%">
   <strong><span style="color: white">Process Inquiry</span></strong>
    
            <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Wallet ID."></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"  placeholder="00000000070" ID="txtWaletNumber" ReadOnly="True">00000000070</asp:TextBox></span></strong>
             <asp:Button
        ID="btnInquiry" runat="server" OnClick="btnInquiry_Click"  Text="Process Inquiry"  OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>            
            </td>
        
              
                        <td style="width:15%">
                            </strong><strong><span class="Font_Color" style="color:white;">
                            <asp:Label ID="lblprocessqty" runat="server"></asp:Label>
                            </span></strong></td>
                          
                                       <td style="width:5%; text-align:right;">
                                           <asp:UpdateProgress  ID="upbREBBiilInquiry" runat="server">
                                               <ProgressTemplate>
                                                   <img alt="Loading"  src="../resources/images/loading.gif" />
                                                   &nbsp;&nbsp;
                                               </ProgressTemplate>
                                           </asp:UpdateProgress>
                                       </td>
           </tr>
                   </table>
        </div>

        <br />--%>

<div class="row"  style="background-color: royalblue; text-align:left;">
   <strong><span style="color: white">REB Bill Pay Report</span></strong>
    
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>             
      <%--<span class="Font_Color" style="color:white;">From Date</span>
               <cc1:GMDatePicker ID="dtpFromDate" runat="server"  CalendarTheme="Silver" 
                                DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                TextBoxWidth="130">
                                <calendartitlestyle  />
                            </cc1:GMDatePicker>--%>
   <span class="Font_Color" style="color:white;">Fromdate</span>
    
    <cc1:GMDatePicker ID="dtpFromDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
           <span class="Font_Color" style="color:white;">Todate</span>
  <cc1:GMDatePicker ID="dtpTodate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
    <asp:Button
        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"/>
           <asp:Button
        ID="btnExportReport" OnClick="btnExportReport_Click" runat="server" Text="Export Report"/>
      </strong>
   
        </div>
    </div>
   
       




     <asp:SqlDataSource id="sdsBulkBillPayReport" runat="server" 
ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
SelectCommand='SELECT "SR"."REQUEST_ID","BBS"."UBP_REB_BBP_ID","BBS"."UBP_REB_BBPS_MONTH","BBS"."UBP_REB_BBPS_STATUS","BBS"."UBP_REB_BBPS_REF","BBS"."UBP_REB_BBPS_PURPOSE","BBS"."UBP_REB_BBPS_TIME","UT"."TOTAL_BILL_AMOUNT","UT"."CHEQUE_REMARKS" FROM "UBP_REB_BULK_BILL_PAY_STATUS" "BBS","SERVICE_REQUEST" "SR", "UTILITY_TRANSACTION" "UT"'>
     
</asp:SqlDataSource>

    

    <%--<asp:GridView ID="gdvREBAccountsReport" runat="server" AllowPaging="True" GridLines="None" PageSize="20" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="UBP_REB_BBP_ID" 
                        BorderColor="#E0E0E0" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt"
        OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting">
         <Columns>
              <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" SortExpression="REQUEST_ID">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>
                              
                           
                   <asp:BoundField DataField="UBP_REB_ACCOUNT_ID" HeaderText="REB Account ID" SortExpression="UBP_REB_ACCOUNT_ID">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="UBP_REB_BBPS_MONTH" HeaderText="Month Of Bill" SortExpression="UBP_REB_BBPS_MONTH">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

              <asp:BoundField DataField="UBP_REB_BBPS_STATUS" HeaderText="REB_Status" SortExpression="UBP_REB_BBPS_STATUS">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

              <asp:BoundField DataField="UBP_REB_BBPS_REF" HeaderText="REB_Referance" SortExpression="UBP_REB_BBPS_REF">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

               <asp:BoundField DataField="UBP_REB_BBPS_PURPOSE" HeaderText="REB_Purpose" SortExpression="UBP_REB_BBPS_PURPOSE">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

               <asp:BoundField DataField="UBP_REB_BBPS_TIME" HeaderText="REB_Date" SortExpression="UBP_REB_BBPS_TIME">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_BILL_AMOUNT" HeaderText="Total Bill Amount" SortExpression="TOTAL_BILL_AMOUNT">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

               <asp:BoundField DataField="CHEQUE_REMARKS_OLD" HeaderText="Cheque Remarks Old" SortExpression="CHEQUE_REMARKS_OLD">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

             <asp:BoundField DataField="CHEQUE_REMARKS" HeaderText="Cheque Remarks" SortExpression="CHEQUE_REMARKS">
                                  <HeaderStyle  HorizontalAlign="Center"/>
                                  <ItemStyle HorizontalAlign="left" />
                              </asp:BoundField>

                          </Columns>     
        <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />  
    </asp:GridView> --%>

        <asp:GridView ID="gdvREBAccountsReport" runat="server" BorderColor="#E0E0E0" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="20" AllowSorting="True" OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting" DataKeyNames="SOURCE_ACC_NO" >
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
</contenttemplate>      
    </asp:UpdatePanel>
    </form>
</body>
</html>
