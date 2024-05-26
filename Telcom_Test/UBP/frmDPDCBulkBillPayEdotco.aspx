<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDPDCBulkBillPayEdotco.aspx.cs" Inherits="UBP_frmDPDCBulkBillPayEdotco" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>DPDC Bulk Bill Pay</title>
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
         .panel-style {
  background-color: #f5f5f5;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.panel-style .row {
  margin-bottom: 10px;
}

.panel-style .col-12 {
  display: flex;
  align-items: center;
}

.panel-style .btn {
  margin-right: 10px;
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
                        <strong><span style="color: white">DPDC Bulk Bill Pay Edotco</span></strong>
                    </td>
                    <%--<td style="width:2%">
                           <asp:TextBox ID="txtRebID" runat="server" Width="100px"></asp:TextBox>
                    </td>--%>
                     <%--<td class="auto-style4">
                         &nbsp;&nbsp;
                            <asp:Button ID="btnInquiry" runat="server" OnClick="btnInquiry_Click" Text="Inquiry" Width="77px" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" Text="Payment" Width="78px" />
                     </td>--%>
                    <td style="width:1%">
                           
                    </td>
                     <td class="auto-style2">
                            
                     </td>
                         <td style="width:15%">    
                              <%--<strong><span style="color: white"><asp:Label runat="server" ID="lblsuccesprocess"></asp:Label></span></strong>  
                             &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;    --%>                  
                              <strong><span style="color: white"><asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label></span></strong>                            
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
   
        


         
                      
        <br />

<%--        <div class="row" style="background-color: royalblue">
            <table>
                <tr>
                    <td class="auto-style5">
                          </span></strong>
                          <asp:Label runat="server" ID="lblMessage" Text=""  ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>--%>
        <br />

          <%--<div class="row">
            <div class="col-12">                
                <asp:Label runat="server" Width="8%" Text="Select File" style="vertical-align:top;"  Font-Bold="true" ></asp:Label>      
                 <asp:FileUpload ID="FileUpload2" runat="server" Height="24px" />  
                <asp:Button ID="Button3" runat="server" Text="Upload Status" OnClick="btnReport_Click" Width="97px" />
                </td>       
                <asp:Button ID="Button4" runat="server" Text="Upload File" OnClick="btnUploadFile_Click" Width="81px" />       
            </div>
        </div> --%>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel-style">
    <div class="row" style="padding-left: 30px;">
        <div class="col-12">                
            <asp:Label runat="server" Width="8%" Text="Select File" style="vertical-align: top; font-weight: bold;"></asp:Label>      
            <asp:FileUpload ID="FileUpload2" runat="server" Height="24px" />  
            <asp:Button ID="Button3" runat="server" Text="Upload Status" OnClick="btnReport_Click" Width="97px" CssClass="btn btn-primary" />
            <asp:Button ID="Button4" runat="server" Text="Upload File" OnClick="btnUploadFile_Click" Width="81px" CssClass="btn btn-success" />
        </div>
    </div>
</asp:Panel>

        <br />  
        <br />
        <asp:GridView ID="gdvBulkBillPay" runat="server"   OnPageIndexChanging="Gridpaging" OnSorting="Gridsorting" GridLines="Both"              CellPadding="6"
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
                    <asp:TemplateField HeaderText="ACCOUNT_NUMBER" HeaderStyle-HorizontalAlign="Center"   SortExpression="ACCOUNT_NUMBER"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACCOUNT_NUMBER") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditAutoID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACCOUNT_NUMBER") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAutoID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                   <asp:TemplateField HeaderText="LOCATION_ID" HeaderStyle-HorizontalAlign="Center"   SortExpression="LOCATION_ID"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LOCATION_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditAccountID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LOCATION_ID") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAccountID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="BILL_TYPE" HeaderStyle-HorizontalAlign="Center"   SortExpression="BILL_TYPE"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblBillType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BILL_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtBillType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BILL_TYPE") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtBillType" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                     
                     <asp:TemplateField HeaderText="ACCOUNT_STATUS"  HeaderStyle-HorizontalAlign="Center" SortExpression="ACCOUNT_STATUS"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACCOUNT_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                              <asp:Label ID="lblAccountStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACCOUNT_STATUS") %>'></asp:Label>                                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAccountStatus" runat="server" Width="150px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="REMARK"  HeaderStyle-HorizontalAlign="Center"  SortExpression="REMARK"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REMARK") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditRemarkse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REMARK") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddRemarks" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="PURPOSE"  HeaderStyle-HorizontalAlign="Center"  SortExpression="PURPOSE"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblPurpose" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PURPOSE") %>'></asp:Label>
                        </ItemTemplate>
                        <%--<EditItemTemplate>            
                            <asp:TextBox ID="txtEditPurpose" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UBP_REB_PURPOSE") %>'></asp:TextBox>            
                        </EditItemTemplate>   --%>                    
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddPurpose" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="MESSAGE"  HeaderStyle-HorizontalAlign="Center"  SortExpression="MESSAGE"  HeaderStyle-Font-Underline="true" HeaderStyle-BackColor="#A3B6DD" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="#0A11D5">
                        <ItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MESSAGE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditMessage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MESSAGE") %>'></asp:TextBox>            
                        </EditItemTemplate>                       
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEditMessage" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    

                    
                    
                </Columns>      
                
            </asp:GridView>   

        </contenttemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="Button4" />
            </Triggers>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
