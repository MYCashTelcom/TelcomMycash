<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmREBMeterRentBL.aspx.cs" Inherits="UBP_frmREBMeterRentBL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REB Meter Rent BL</title>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />

     <style type="text/css">
         .hidden {
            display: none;
        }
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
         .auto-style2 {
             width: 285px;
         }
         .auto-style4 {
             width: 183px;
         }
         .auto-style5 {
             width: 86px;
         }
    </style> 

     <script type="text/jscript">
         function Count(text) {
             //debugger;
             //asp.net textarea maxlength doesnt work; do it by hand
             var maxlength = 100; //set your value here (or add a parm and pass it in)
             var object = document.getElementById(text.id)  //get your object
             if (object.value.length > maxlength) {
                 object.focus(); //set focus to prevent jumping
                 object.value = text.value.substring(0, maxlength); //truncate the value
                 object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                 return false;
             }
             return true;
         }
         function toggleContentDiv() {
             var contentDiv = document.getElementById('contentDiv');
             //if (contentDiv.style.display === 'none') {
             //    contentDiv.style.display = 'block';
             //} else {
             //    contentDiv.style.display = 'none';
             //}
             contentDiv.style.display = 'block';
         }
         function CountAccountId(text) {
             //asp.net textarea maxlength doesnt work; do it by hand
             var maxlength = 1808; //set your value here (or add a parm and pass it in)
             var object = document.getElementById(text.id)  //get your object
             if (object.value.length > maxlength) {
                 object.focus(); //set focus to prevent jumping
                 object.value = text.value.substring(0, maxlength); //truncate the value
                 object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                 return false;
             }
             return true;
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="True"
            AsyncPostBackTimeout="36000">
        </ajaxToolkit:ToolkitScriptManager> 
    <asp:UpdatePanel id="upnlREBAccount"  UpdateMode="Conditional" runat="server">
    <contenttemplate>
        <div class="row">
            <asp:Label runat="server" Width="8%" Text="Search Existing Data" style="vertical-align:top;"  Font-Bold="true" ></asp:Label>
        </div>
        <div class="row" style="background-color: royalblue">
            <table style="width:100%">
                <tr style="width:100%">
                  <td style="width:10%">
                        <strong><span style="color: white">SMS ACCOUNT NUMBER</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtsmsAccNoSearch" runat="server" Width="100px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         &nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" OnClick="btnSearch_Click" Text="Search" Width="77px" />&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnShow" runat="server" Text="Add New" Width="77px" OnClientClick="toggleContentDiv(); return false;" />
                            
                     </td>
                    <td style="width:1%">
                           
                    </td>
                     <td class="auto-style2">
                            
                     </td>
                         <td style="width:15%">    
                              <strong><span style="color: red"><asp:Label runat="server" ID="Label1"></asp:Label></span></strong>  
                             &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                      
                              <strong><span style="color: white"><asp:Label runat="server" ID="Label2"></asp:Label></span></strong>                            
                             </td>

                        <td style=" text-align:right; width:10%;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    
                                    <img alt="Loading" src="../resources/images/loading.gif" />                  
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                </tr>
            </table>
        </div>

        <asp:GridView ID="gdvBulkBillPay" runat="server"   OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting" GridLines="Both" CellPadding="6"
                    AutoGenerateColumns="false" 
                    AllowPaging="true"  PageSize="8" AllowSorting="true"
                    
                    OnRowEditing="gdvBulkBillPay_RowEditing"
                    OnRowUpdating="gdvBulkBillPay_RowUpdating"
                    OnRowCancelingEdit="gdvBulkBillPay_RowCancelingEdit"
                    HeaderStyle-BackColor="#4D4D4D"
                    HeaderStyle-ForeColor="White"
                    BorderColor="#E0E0E0" 
                    CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt">
                <Columns>            
                    <asp:TemplateField HeaderText="UBP_REB_MR_ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_MR_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_MR_ID") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAutoID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="SMS_ACC_NO" HeaderStyle-HorizontalAlign="Center"   SortExpression="SMS_ACC_NO"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMS_ACC_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMS_ACC_NO") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAccountID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="MRENT"  HeaderStyle-HorizontalAlign="Center" SortExpression="MRENT"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MRENT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                              <asp:TextBox ID="txtMrent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MRENT") %>'></asp:TextBox>                                           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAccountStatus" runat="server" Width="150px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Edit/Update"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>                        
                             <asp:LinkButton ID="imgbtnEdit" runat="server" CommandName="Edit" Text="Edit" />
                             <%--<asp:LinkButton ID="imgbtnDelete" runat="server" CommandName="Delete" Text="Delete"
                            OnClientClick="return confirm('Are you sure you want to delete this record?')"
                            CausesValidation="false" />--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:LinkButton ID="imgbtnUpdate" runat="server" CommandName="Update" Text="Update"  OnClientClick="return confirm('Are you sure you want to update this record?')" />
                        <asp:LinkButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Width="80px"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>   
                </Columns>      
            </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;
        <%--<div class="row" style="background-color: royalblue">
            <asp:Button ID="Button3" runat="server" OnClick="btnShow_Click" Text="Add New " Width="77px" />
            </div>--%>

        <div>
            
            
            <div id="contentDiv" class="hidden" style="background-color: royalblue;">
                <table style="width: 100%;">
                   <tr style="width:100%">
                  <td style="width:10%">
                        <strong><span style="color: white">SMS ACCOUNT NUMBER</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtSMSAccNoAdd" runat="server" Width="138px" Height="16px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         <td style="width:10%">
                        <strong><span style="color: white">MRENT</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtMRENTAdd" runat="server" Width="100px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         &nbsp;&nbsp;
                            <asp:Button ID="Button2"  runat="server" OnClientClick="toggleContentDiv(); return true;" OnClick="btnSave_Click"    Text="Add" Width="77px"  />&nbsp;&nbsp;&nbsp;&nbsp;
                            
                     </td>
                    <td style="width:1%">
                           
                    </td>
                     <td class="auto-style2">
                            
                     </td>
                         <td style="width:15%">    
                              <strong><span style="color: red"><asp:Label runat="server" ID="Label3"></asp:Label></span></strong>  
                             &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                      
                              <strong><span style="color: white"><asp:Label runat="server" ID="Label4"></asp:Label></span></strong>                            
                             </td>

                        <td style=" text-align:right; width:10%;">
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                <ProgressTemplate>
                                    
                                    <img alt="Loading" src="../resources/images/loading.gif" />                  
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                </tr>
                </table>
            </div>
        </div>














       <%-- <div  class="row hidden" style="background-color: royalblue">
            <table style="width:100%">
                <tr style="width:100%">
                  <td style="width:10%">
                        <strong><span style="color: white">SMS ACCOUNT NUMBER</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtSMSAccNoAdd" runat="server" Width="138px" Height="16px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         <td style="width:10%">
                        <strong><span style="color: white">MRENT</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtMRENTAdd" runat="server" Width="100px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         &nbsp;&nbsp;
                            <asp:Button ID="Button2" runat="server" OnClick="btnSave_Click" Text="Add" Width="77px" />&nbsp;&nbsp;&nbsp;&nbsp;
                            
                     </td>
                    <td style="width:1%">
                           
                    </td>
                     <td class="auto-style2">
                            
                     </td>
                         <td style="width:15%">    
                              <strong><span style="color: red"><asp:Label runat="server" ID="Label3"></asp:Label></span></strong>  
                             &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                      
                              <strong><span style="color: white"><asp:Label runat="server" ID="Label4"></asp:Label></span></strong>                            
                             </td>

                        <td style=" text-align:right; width:10%;">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    
                                    <img alt="Loading" src="../resources/images/loading.gif" />                  
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                </tr>
            </table>
        </div>--%>


        </contenttemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
