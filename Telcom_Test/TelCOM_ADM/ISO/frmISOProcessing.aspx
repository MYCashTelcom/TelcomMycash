<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmISOProcessing.aspx.cs" Inherits="COMMON_frmISOProcessing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Information of Iso Processing Code</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
            width: 162px;
        }
        .style2
        {
            width: 36px;
        }
        .style3
        {
            width: 202px;
        }
        .style4
        {
            width: 47px;
        }
    </style>
</head>


<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
             
             <asp:SqlDataSource runat="server" ID="sdsBankList"
                 ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                 ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                 SelectCommand="SELECT DISTINCT BANK_ID, BANK_NAME, BANK_STATUS FROM BANK_LIST WHERE BANK_STATUS = 'A' ORDER BY BANK_NAME"
             ></asp:SqlDataSource>
             
             <asp:SqlDataSource runat="server" ID="sdsServiceList"
                 ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                 ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                 SelectCommand="SELECT DISTINCT SERVICE_ID, SERVICE_TITLE, SERVICE_ACCESS_CODE FROM SERVICE_LIST ORDER BY SERVICE_TITLE"
             ></asp:SqlDataSource>
             
             <asp:SqlDataSource runat="server" ID="sdsProcessingCode"
                 ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                 ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>"
                 DeleteCommand="DELETE FROM ISO_PROCESSING_CODE WHERE  (ISO_PRO_CODE_ID =  :ISO_PRO_CODE_ID)"
                 InsertCommand="INSERT INTO ISO_PROCESSING_CODE (SERVICE_ID, BANK_ID, PROCESSING_CODE, REMARKS ) VALUES ( :SERVICE_ID, :BANK_ID, :PROCESSING_CODE, :REMARKS)"
                 UpdateCommand="UPDATE ISO_PROCESSING_CODE SET 
                            SERVICE_ID = :SERVICE_ID, 
                            BANK_ID = :BANK_ID, 
                            PROCESSING_CODE = :PROCESSING_CODE, 
                            REMARKS = :REMARKS  
                            WHERE (ISO_PRO_CODE_ID = :ISO_PRO_CODE_ID)">
                <DeleteParameters>
                    <asp:Parameter Name="ISO_PRO_CODE_ID" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SERVICE_ID" Type="String" />
                    <asp:Parameter Name="BANK_ID" Type="String" />
                    <asp:Parameter Name="PROCESSING_CODE" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />                
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="SERVICE_ID" Type="String" />
                    <asp:Parameter Name="BANK_ID" Type="String" />
                    <asp:Parameter Name="PROCESSING_CODE" Type="String" />
                    <asp:Parameter Name="REMARKS" Type="String" />  
                </InsertParameters>
             </asp:SqlDataSource>
             <br/>
             
             <asp:Panel runat="server" ID="panelTop" CssClass="Top_Panel" Height="25px">
                 <table style="width: 100%" align="left">
                     <tr>
                         <td align="left" class="style1">
                             <asp:Label runat="server" ID="lblTitle"><strong>Iso Process Code</strong></asp:Label>
                         </td>
                         <td align="left" class="style2">
                             <asp:Label runat="server" ID="lblBankList"><strong>Bank:</strong></asp:Label>
                         </td>
                         <td align="left" class="style3">
                             <asp:DropDownList runat="server" ID="ddlBankList" DataSourceID="sdsBankList"
                             DataTextField="BANK_NAME" DataValueField="BANK_ID" AutoPostBack="True" 
                                 onselectedindexchanged="ddlBankList_SelectedIndexChanged"/>
                         </td>
                         <td align="left" class="style4">
                             <asp:Label runat="server" ID="lblService"><strong>Service:</strong></asp:Label>
                         </td>
                         <td align="left">
                             <asp:DropDownList runat="server" ID="ddlServiceList" DataSourceID="sdsServiceList"
                             DataTextField="SERVICE_TITLE" DataValueField="SERVICE_ID" AutoPostBack="True" 
                                 onselectedindexchanged="ddlServiceList_SelectedIndexChanged">
                           </asp:DropDownList>
                         </td>
                         <td align="left">
                           <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                            <ProgressTemplate>
                             <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;</ProgressTemplate>
                           </asp:UpdateProgress>
                       </td>
                       <td align="left">
                           <asp:Label runat="server" ID="lblMsg"></asp:Label>
                       </td>
                     </tr>
                 </table>
             </asp:Panel>
             <br/>
             
             <asp:Panel runat="server" ID="panelView">
             
                        <asp:GridView ID="grdIsoProcess" runat="server" AutoGenerateColumns="False" 
                            CssClass="mGrid" GridLines="None" Width="600px"
                    DataKeyNames="ISO_PRO_CODE_ID" DataSourceID="sdsProcessingCode" 
                            OnRowCancelingEdit="grdIsoProcess_RowCancelingEdit" BorderColor="#E0E0E0"
                    OnRowDeleted="grdIsoProcess_RowDeleted" OnRowEditing="grdIsoProcess_RowEditing" OnRowUpdated="grdIsoProcess_RowUpdated"
                    OnRowUpdating="grdIsoProcess_RowUpdating" AllowPaging="True" PageSize="15" 
                            onpageindexchanged="grdIsoProcess_PageIndexChanged" 
                            onpageindexchanging="grdIsoProcess_PageIndexChanging">                    
                    <Columns>
                        <asp:TemplateField HeaderText="Iso Processing Code Id" 
                            SortExpression="ISO_PRO_CODE_ID">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ISO_PRO_CODE_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ISO_PRO_CODE_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Id" SortExpression="SERVICE_ID">
                            <%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("SERVICE_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Id" SortExpression="BANK_ID">
                            <%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BANK_ID") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("BANK_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Processing Code" 
                            SortExpression="PROCESSING_CODE">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PROCESSING_CODE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("PROCESSING_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
             
             
             
             
                 
             </asp:Panel>
             <br/>
             <fieldset style="width: 400px">
                 <legend><strong>Insert ISO Process Code</strong></legend>
                 <table style="width: 400px">
                     <tr>
                         <td>
                             <asp:Label runat="server" ID="lblProcessCode"><strong>Processing Code</strong></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox runat="server" ID="txtProcessCode" Width="200px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:Label runat="server" ID="lblRemarks"><strong>Remarks</strong></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="200px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td></td>
                         <td>
                             <asp:Button runat="server" ID="btnInsertPrcCode" Text="Insert" Width="70px" 
                                 onclick="btnInsertPrcCode_Click"/>
                         </td>
                     </tr>
                 </table>
             </fieldset>
         </ContentTemplate>
        </asp:UpdatePanel> 
    
    
    
    </form>
</body>
</html>
