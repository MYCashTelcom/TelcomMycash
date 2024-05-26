<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Mid_Tran.aspx.cs" Inherits="UBP_Mid_Tran" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DPS REPORT</title>
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
         .auto-style4 {
             width: 148px;
         }
         .auto-style7 {
             width: 253px;
         }
         .auto-style15 {
             width: 311px;
         }
         .auto-style17 {
             width: 80px;
         }
         .auto-style18 {
             width: 81px;
         }
         .auto-style19 {
             width: 79px;
         }
    </style> 

</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
            AsyncPostBackTimeout="36000">
        </ajaxToolkit:ToolkitScriptManager>   
    <asp:UpdatePanel id="upnDPS_Report" runat="server">
          <Triggers>
                <asp:PostBackTrigger ControlID="btnExportReport" />
            </Triggers>
    <contenttemplate>
        <div class="row"  style="background-color: royalblue">
               <table>
       <tr>
        <td class="auto-style15">
   
            <table>
                <tr>
                    <td>   <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Request Id"></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"   ID="requestIdTxt" Width="79px"></asp:TextBox></span></strong>
           
                 </td>
               
                
               
                    <td>   <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="User Id"></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"   ID="UserIdtxt" Width="72px"></asp:TextBox></span></strong>
           
                 </td>
               
                
                
                    <td>   <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Pos Id"></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"   ID="PosIdtxt" Height="16px" Width="79px"></asp:TextBox></span></strong>
           
                 </td>
                    <td>   <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Meter Id"></asp:Label></span></strong>
            <strong>
            <span class="Font_Color" style="color:white;"><asp:TextBox runat="server"   ID="txtMeter" Height="16px" Width="79px"></asp:TextBox></span></strong>
           
                 </td>
                </tr>
            </table>
             </td>
                        <td class="auto-style17">
                          <strong><span class="Font_Color" style="color:white;">From</span></strong>  
                            <cc1:GMDatePicker ID="dtpFromDate" runat="server" CalendarTheme="Silver" 
                                                        DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01"  Style="position: relative" 
                                                        TextBoxWidth="130" >
                                                        <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                                                    </cc1:GMDatePicker>
                            </td>
                            <td class="auto-style19">
                            <strong><span class="Font_Color" style="color:white;">To  </span></strong>
                            <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" DateFormat="dd/MMM/yyyy HH:mm:ss" MinDate="1900-01-01" Style="position: relative" TextBoxWidth="130">
                                <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
                            </cc1:GMDatePicker>
                                &nbsp;
                            
    

 
                        </td>
           <td>
                  <strong>
            <span class="Font_Color" style="color:white;"><asp:Label runat="server" Text="Response Msg"></asp:Label></span></strong>
               <asp:DropDownList ID="DropDownList1" runat="server" >  
            <asp:ListItem Value="All">All</asp:ListItem>  
            <asp:ListItem Value="Success">Success</asp:ListItem>                  
            <asp:ListItem Value="Failed">Failed</asp:ListItem>
             <asp:ListItem  Value="Unreachable">Unreachable</asp:ListItem>
             


               </asp:DropDownList> 
                
           </td>
                         <td class="auto-style7">
                             <asp:Button
        ID="btnInquiry" runat="server"   Text="Search" UseSubmitBehavior="false" Width="110px" OnClick="btnInquiry_Click"/>&nbsp;&nbsp;
        <asp:Button
        ID="Button1" runat="server"   Text="Show All" UseSubmitBehavior="false" Width="110px" Visible="false" OnClick="btnAll_Click"/>

            <asp:Button
        ID="btnExportReport" runat="server"  Text="Export All" UseSubmitBehavior="false" Width="110px" Visible="True" OnClick="btnAllExport_Click"/> 
             &nbsp;&nbsp;           
            
                         </td>
                         
              
                        <td class="auto-style4">  
                               
                            </strong><strong><span class="Font_Color" style="color:white;">
                            &nbsp;
                                <asp:Label ID="lblprocessqty" runat="server"></asp:Label>
                            </span></strong></td>
                          
                                       <td>
                                           <asp:UpdateProgress  ID="upbREBBiilInquiry" runat="server">
                                               <ProgressTemplate>
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                   <img alt="Loading"  src="../resources/images/loading.gif" />
                                                   &nbsp;&nbsp;
                                               </ProgressTemplate>
                                           </asp:UpdateProgress>
                                       </td>
           </tr>
                   </table>
        </div>

       

<div class="row"  style="background-color: royalblue; text-align:left;">
   
    
      
   
        </div>
    </div>
   
         <asp:GridView ID="grvRequestList" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No data found....." CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="grvRequestList_RowDataBound" OnRowUpdating="grvRequestList_RowUpdating" DataKeyNames="REQUEST_ID"  OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting" GridLines="None"  AllowPaging="true" PageSize="20" AllowSorting="True">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="SERIAL NO.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sl") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                  <asp:TemplateField  HeaderText="SERIAL NO.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
								<asp:TemplateField HeaderText="REQUEST ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQUEST_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REQUEST_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AAMRA USER ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAAMRA_USER_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AAMRA_USER_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
                                <asp:TemplateField HeaderText="DPDC POS ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDPDC_POS_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DPDC_POS_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="METER NUMBER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMETER_NUMBER" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "METER_NUMBER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BILL AMT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBILL_AMT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CAS_TRAN_AMT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AAMRA REF NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAAMRA_REF_NO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REF_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RESPONSE CODE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRESPONSE_CODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RESPONSE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RESPONSE MSG">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRESPONSE_MSG" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RESPONSE_MSG") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="REQUEST TIME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQUEST_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REQUEST_TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RESPONSE_TIME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRESPONSE_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RESPONSE_TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BILL_TYPE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBILL_TYPES" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BILL_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IS REVERSED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIS_REVERSED" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IS_REVERSED") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ACTION">
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" CommandName="update" Text="Reverse" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

</contenttemplate>      
    </asp:UpdatePanel>
    </form>
</body>
</html>
