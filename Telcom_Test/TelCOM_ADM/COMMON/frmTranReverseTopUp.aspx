<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTranReverseTopUp.aspx.cs" Inherits="COMMON_frmTranReverseTopUp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html >

<html>
<head runat="server">
    <title>Transaction Reverse</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    
    <style type="text/css" >        
        .Top_Panel
         {
         	background-color: royalblue;          	
         	height:20px;
         	font-weight:bold;
         	font-size:12px;
         	color:White;
         	}
         .View_Panel
         {
         	background-color:powderblue;
         	height:20px;
         	 width:100%;  
         	 font-size:12px;
         	 font-weight:bold; 
         	 color:White;    	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         	 width:100%;
         	 font-size:12px;
         	 font-weight:bold; 
         }     
        .style1
        {
            width: 200px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
		 <asp:SqlDataSource ID="sdsBranch" runat="server" 
				 ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
				 ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
				 SelectCommand="SELECT * FROM CM_CMP_BRANCH">
		</asp:SqlDataSource>
			<asp:Panel ID="pnlTop" runat="server" >
		   <table style="width:100%" class="Top_Panel">
			<tr>
			 <td class="style1">
			  Topup Transaction Reverse
			 </td>
			 <td style="text-align:left;">&nbsp;&nbsp;
				 <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
				 <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="sdsBranch"  AutoPostBack="true"
					 DataTextField="CMP_BRANCH_NAME" DataValueField="CMP_BRANCH_ID" Enabled="false" >
				 </asp:DropDownList>
			 </td>
			 <td>
			 
			 </td>
			 <td style="text-align:left;">
			  <asp:Label ID="lblMsg" runat="server" ></asp:Label>
			 </td>
			 <td style="text-align:left;">
			  <asp:UpdateProgress ID="UpdateProgress3" runat="server">
				 <ProgressTemplate>
					<img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
				 </ProgressTemplate>
			  </asp:UpdateProgress>
			 </td>
			</tr>
		   </table>
		</asp:Panel>   
		 <asp:Panel ID="pnlView" runat="server"> 
		   <table style="font-size:12px;">
			 <tr>
			  <td>
				  <asp:Label ID="lblReqID" runat="server" Text="Request ID"></asp:Label>
			  </td>
			  <td>
				  <asp:TextBox ID="txtReqID" runat="server" 
					  Placeholder="Please insert request id" Width="561px" Columns="250" Rows="5" TextMode="MultiLine"></asp:TextBox>
				  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Insert a Request ID. "
						ControlToValidate="txtReqID" ></asp:RequiredFieldValidator>    
			  </td>
			 </tr>
			 <tr>
			  <td>
			  </td>
			  <td>
					<asp:Button ID="btnReverse" runat="server" Text="Reverse" onclick="btnReverse_Click" />
					<asp:Button ID="btnGetStatus" runat="server" Text="Reverse Status" CausesValidation="false" OnClick="btnGetStatus_Click" />  
			  </td>
			 </tr>
		   </table>
		 </asp:Panel>
		 <asp:Panel ID="pnlStatus" runat="server">
			<table>
				<tr>
					<td>
						<asp:GridView ID="gdvReverseStatus" CssClass="mGrid" EmptyDataText="No data found......" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="false">
							<Columns>
								<asp:TemplateField HeaderText="Topup Transaction Id">
									<ItemTemplate>
										<asp:Label ID="lblTopupTranId" runat="server" Text='<%# Bind("TOPUP_TRAN_ID") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Request Id">
									<ItemTemplate>
										<asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("REQUEST_ID") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Source Account">
									<ItemTemplate>
										<asp:Label ID="lblSourceAccount" runat="server" Text='<%# Bind("SOURCE_ACCNT_NO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Transaction Date">
									<ItemTemplate>
										<asp:Label ID="lblTransactionDate" runat="server" Text='<%# Bind("TRAN_DATE") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Transaction Amount">
									<ItemTemplate>
										<asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Bind("TRAN_AMOUNT") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Subscriber Mobile">
									<ItemTemplate>
										<asp:Label ID="lblSubscriberMobile" runat="server" Text='<%# Bind("SUBSCRIBER_MOBILE_NO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Reverse Status">
									<ItemTemplate>
										<asp:Label ID="lblReverseStatus" runat="server" Text='<%# Bind("REVERSE_STATUS") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Reverse Note">
									<ItemTemplate>
										<asp:Label ID="lblReverseNote" runat="server" Text='<%# Bind("REVERSE_NOTE") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</td>
				</tr>
			</table>
		</asp:Panel>
        
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
