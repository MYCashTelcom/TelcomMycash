<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRebBulkBillPay.aspx.cs" Inherits="UBP_frmRebBulkBillPay" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>REB Bulk Bill Pay</title>
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
        <div class="row" style="background-color: royalblue">
            <table style="width:100%">
                <tr style="width:100%">
                  <td style="width:10%">
                        <strong><span style="color: white">Utility Bulk Bill Pay</span></strong>
                    </td>
                    <td style="width:2%">
                           <asp:TextBox ID="txtRebID" runat="server" Width="100px"></asp:TextBox>
                    </td>
                     <td class="auto-style4">
                         &nbsp;&nbsp;
                            <asp:Button ID="btnInquiry" runat="server" OnClick="btnInquiry_Click" Text="Inquiry" Width="77px" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" Text="Payment" Width="78px" />
                     </td>
                    <td style="width:1%">
                           
                    </td>
                     <td class="auto-style2">
                            
                     </td>
                         <td style="width:15%">    
                              <strong><span style="color: white"><asp:Label runat="server" ID="lblsuccesprocess"></asp:Label></span></strong>  
                             &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                      
                              <strong><span style="color: white"><asp:Label runat="server" ID="lblunsuccesprocess"></asp:Label></span></strong>                            
                             </td>

                        <td style=" text-align:right; width:10%;">
                                            <asp:UpdateProgress ID="upbREBBiilPay" runat="server">
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
                    OnRowDeleting="gdvBulkBillPay_RowDeleting"
                    OnRowEditing="gdvBulkBillPay_RowEditing"
                    OnRowUpdating="gdvBulkBillPay_RowUpdating"
                    OnRowCancelingEdit="gdvBulkBillPay_RowCancelingEdit"
                    HeaderStyle-BackColor="#4D4D4D"
                    HeaderStyle-ForeColor="White"
                    BorderColor="#E0E0E0" 
                    CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt">
                <Columns>            
                    <asp:TemplateField HeaderText="UBP_REB_BBP_ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_BBP_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_BBP_ID") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAutoID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="REB Account Id" HeaderStyle-HorizontalAlign="Center"   SortExpression="UBP_REB_ACCOUNT_ID"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_ACCOUNT_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_ACCOUNT_ID") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAccountID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="REB Account Status"  HeaderStyle-HorizontalAlign="Center" SortExpression="UBP_REB_ACCOUNT_STATUS"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_ACCOUNT_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                              <asp:Label ID="lblAccountStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_ACCOUNT_STATUS") %>'></asp:Label>                                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAccountStatus" runat="server" Width="150px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Remarks"  HeaderStyle-HorizontalAlign="Center"  SortExpression="UBP_REB_REMARKS"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditRemarkse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_REMARKS") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddRemarks" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Purpose"  HeaderStyle-HorizontalAlign="Center"  SortExpression="UBP_REB_REMARKS"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblPurpose" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_PURPOSE") %>'></asp:Label>
                        </ItemTemplate>
                        <%--<EditItemTemplate>            
                            <asp:TextBox ID="txtEditPurpose" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_PURPOSE") %>'></asp:TextBox>            
                        </EditItemTemplate>   --%>                    
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddPurpose" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    

                    <asp:TemplateField HeaderText="Edit/Update"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>                        
                             <asp:LinkButton ID="imgbtnEdit" runat="server" CommandName="Edit" Text="Edit" />
                             <asp:LinkButton ID="imgbtnDelete" runat="server" CommandName="Delete" Text="Delete"
                            OnClientClick="return confirm('Are you sure you want to delete this record?')"
                            CausesValidation="false" />
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
                      
        <br />

        <div class="row" style="background-color: royalblue">
            <table>
                <tr>
                    <td>
                        <strong><span style="color: white">Add New Bulk Bill Pay</span></strong>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="RadioButton1" ForeColor="White" runat="server" Text="Inquiry" GroupName="Reb_Bill_Radio" />  
                        <asp:RadioButton ID="RadioButton2" ForeColor="White" runat="server" Text="Payment" GroupName="Reb_Bill_Radio" />
                    </td>
                    <td>
                          
                    </td>

                    <td class="auto-style5">
                          <asp:Label runat="server" ID="Label1"  ForeColor="Red"></asp:Label></span></strong>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="row">
            <div class="col-12">                
                       <asp:Label runat="server" Width="8%" Text="REB Account ID" style="vertical-align:top;"  Font-Bold="true" ></asp:Label>
                      <asp:TextBox runat="server"  TextMode="MultiLine" Width="70%"  Rows="200" onKeyUp="javascript:CountAccountId(this);" onChange="javascript:CountAccountId(this);" Height="130"  id="txtaccountid"></asp:TextBox>
                     <asp:Label runat="server" ForeColor="Red" Width="20%"   style="vertical-align:top;" Text="*Account ID will be comma seperated (such as: 10000090987871,10000090987872) and Max 100 REB Account ID can be saved at a time."></asp:Label>                      
            </div>
        </div>
        <br />

           <div class="row">
            <div class="col-12">
                       <asp:Label runat="server" Width="8%" style="vertical-align:top;" Text="Remarks"  Font-Bold="true" ></asp:Label>
                      <asp:TextBox runat="server" TextMode="MultiLine" Width="60%"  Rows="3" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"  Height="130"   id="txtremarks"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12">
               <asp:Label runat="server" Width="35%" style="vertical-align:top;"   Font-Bold="true" ></asp:Label>
                       <asp:Button ID="btnsave"   OnClick="btnsave_Click"   meta:resourcekey="BtnUserDeleteResource1" OnClientClick="if ( !confirm('Are you sure you want to save?')) return false;" ForeColor="Black" Font-Names="Eras ITC" Width="90px" CssClass="btn btn-default" runat="server" Text="Save"></asp:Button>

            </div>
        </div> 
        <br />   

        </contenttemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
