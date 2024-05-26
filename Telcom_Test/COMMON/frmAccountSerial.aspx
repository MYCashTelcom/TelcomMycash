<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountSerial.aspx.cs" Inherits="COMMON_frmAccountSerial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Serial Creation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
     .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
        .style1
        {
            width: 80px;
        }
        .style2
        {
            width: 212px;
        }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        
        <asp:SqlDataSource ID="sdsClientList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
            ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand=" SELECT DISTINCT ASM.ACCNT_SL_MSTR_ID,ASM.START_SL_NO,ASM.END_SL_NO,ASM.CREATE_BY,ASM.CREATE_DATE,ASM.REMARKS,ASM.STATUS,ASD.BANK_CODE
                            FROM ACCOUNT_SERIAL_MASTER ASM ,ACCOUNT_SERIAL_DETAIL ASD WHERE ASM.ACCNT_SL_MSTR_ID=ASD.ACCNT_SL_MSTR_ID(+) " 
                
             InsertCommand="INSERT INTO ACCOUNT_SERIAL_MASTER (START_SL_NO,END_SL_NO,CREATE_BY,CREATE_DATE,REMARKS)
                               VALUES (:START_SL_NO,:END_SL_NO,:CREATE_BY,:CREATE_DATE,:REMARKS)">
            
        </asp:SqlDataSource>
        
        <asp:SqlDataSource runat="server" ID="sdsBankList"
         ConnectionString="<%$ ConnectionStrings:oracleConString %>"
         ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
         SelectCommand="SELECT DISTINCT BANK_INTERNAL_CODE FROM BANK_LIST where BANK_STATUS='A'">
        </asp:SqlDataSource>
       
        <asp:Panel ID="Panel1" runat="server" CssClass="Top_Panel">
         <table width="100%">
          <tr>
          <td class="style2">
            Manage Account Serial Activation
          </td>
          <td></td>
          <td class="style1">
            <asp:Label runat="server" ID="lblBank" Text="Select Bank:"></asp:Label>
          </td> 
          <td>
            <asp:DropDownList runat="server" ID="drpBankList" DataSourceID="sdsBankList"
              DataTextField="BANK_INTERNAL_CODE" DataValueField="BANK_INTERNAL_CODE"/>
          </td>  
          <td align="right">
            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ></asp:Label>
          </td>
          <td>
           <asp:UpdateProgress ID="UpdateProgress1" runat="server">
             <ProgressTemplate>
               <img alt="Loading" src="../resources/images/loading.gif" />                    
             </ProgressTemplate>
           </asp:UpdateProgress>
          </td>
         </tr>
        </table>  
       </asp:Panel>
       
      
      <asp:GridView ID="gdvSerialList" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
         BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="ACCNT_SL_MSTR_ID" DataSourceID="sdsClientList">
        <Columns>
            <asp:BoundField DataField="ACCNT_SL_MSTR_ID" HeaderText="Account SL Master ID" 
                ReadOnly="True" SortExpression="ACCNT_SL_MSTR_ID" />
            <asp:BoundField DataField="START_SL_NO" HeaderText="Start Serial No" 
                SortExpression="START_SL_NO" />
            <asp:BoundField DataField="END_SL_NO" HeaderText="End Serial No" 
                SortExpression="END_SL_NO" />
             <asp:BoundField DataField="BANK_CODE" HeaderText="Bank Code" SortExpression="BANK_CODE" />
            
            <asp:BoundField DataField="CREATE_BY" HeaderText="Create By" 
                SortExpression="CREATE_BY" />
            <asp:BoundField DataField="CREATE_DATE" HeaderText="Creation Date" 
                SortExpression="CREATE_DATE" />
            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                SortExpression="REMARKS" />
           
           
            <asp:TemplateField HeaderText="Activation Status" SortExpression="STATUS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="False" 
                        SelectedValue='<%# Bind("STATUS") %>'>
                        <asp:ListItem Value="I">Idle</asp:ListItem>
                        <asp:ListItem Value="A">Active</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnActive" runat="server" 
                        CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ACCNT_SL_MSTR_ID") %>' 
                        onclick="btnActive_Click" Text="Active" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <div style="background-color: royalblue"> <strong><span style="color: white">Account Serial Creation</span></strong></div>   
    
     <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
            DataKeyNames="ACCNT_SL_MSTR_ID" DataSourceID="sdsClientList" 
            DefaultMode="Insert" Height="50px" Width="319px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" GridLines="None" BorderStyle="None" 
            oniteminserted="DetailsView1_ItemInserted" OnItemInserting="DetailsView1_ItemInserting">
        <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:BoundField DataField="ACCNT_SL_MSTR_ID" HeaderText="ACCNT_SL_MSTR_ID" 
                ReadOnly="True" SortExpression="ACCNT_SL_MSTR_ID" Visible="False" />
            <asp:BoundField DataField="START_SL_NO" HeaderText="Start Serial No" 
                SortExpression="START_SL_NO" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle ForeColor="Black" />
            </asp:BoundField>
            <asp:BoundField DataField="END_SL_NO" HeaderText="End Serial No" 
                SortExpression="END_SL_NO" >
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Create Date" SortExpression="CREATE_DATE">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CREATE_DATE") %>'></asp:TextBox>
                </EditItemTemplate>
                
                <InsertItemTemplate>
                    <cc1:GMDatePicker ID="txtFromDate" runat="server" DateFormat="dd-MMM-yyyy" 
                        MinDate="1980-01-04" AutoPosition="True" CalendarOffsetX="-200px" 
                        CalendarOffsetY="25px" CalendarTheme="None" CalendarWidth="200px" 
                        CallbackEventReference="" Culture="English (United States)" 
                        Date='<%# Bind("CREATE_DATE") %>' DateString='<%# Bind("CREATE_DATE") %>' 
                        EnableDropShadow="True" MaxDate="9999-12-31" NextMonthText="&gt;" 
                        NoneButtonText="None" ShowNoneButton="True" ShowTodayButton="True" 
                        TextBoxWidth="100" ZIndex="1">
                                    </cc1:GMDatePicker>
                </InsertItemTemplate>
                
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CREATE_DATE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Create By" SortExpression="CREATE_BY">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CREATE_BY") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtCreateBy" runat="server" Height="18px" 
                        Text='<%# Bind("CREATE_BY") %>' Width="195px"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("CREATE_BY") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("REMARKS") %>' 
                        TextMode="MultiLine" Height="66px" Width="217px"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                    <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            </Fields>
            <AlternatingRowStyle CssClass="alt" />
         </asp:DetailsView>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
