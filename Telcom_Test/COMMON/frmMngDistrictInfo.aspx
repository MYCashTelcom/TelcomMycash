<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMngDistrictInfo.aspx.cs" Inherits="Forms_Default" Title="Manage District" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage District</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
       <div style="BACKGROUND-COLOR: royalblue"><strong><span style="COLOR: white">
       Manage District </span></strong>&nbsp;
       </div>
     <div>
        <asp:SqlDataSource id="sdsBankAccType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:oracleConString %>"
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
            SelectCommand='SELECT * FROM "MANAGE_DISTRICT"' 
             DeleteCommand="DELETE FROM &quot;MANAGE_DISTRICT&quot; WHERE &quot;DISTRICT_ID&quot; = :DISTRICT_ID" 
             InsertCommand="INSERT INTO &quot;MANAGE_DISTRICT&quot; (&quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot;, &quot;DISTRICT_CODE&quot;, &quot;POSTAL_CODE&quot;) VALUES (:DISTRICT_ID, :DISTRICT_NAME, :DISTRICT_CODE, :POSTAL_CODE)" 
             
             UpdateCommand="UPDATE &quot;MANAGE_DISTRICT&quot; SET &quot;DISTRICT_NAME&quot; = :DISTRICT_NAME, &quot;DISTRICT_CODE&quot; = :DISTRICT_CODE, &quot;POSTAL_CODE&quot; = :POSTAL_CODE WHERE &quot;DISTRICT_ID&quot; = :DISTRICT_ID">
            <DeleteParameters>
                <asp:Parameter Name="DISTRICT_ID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="DISTRICT_NAME" Type="String" />
                <asp:Parameter Name="DISTRICT_CODE" Type="String" />
                <asp:Parameter Name="POSTAL_CODE" Type="String" />
                <asp:Parameter Name="DISTRICT_ID" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="DISTRICT_ID" Type="String" />
                <asp:Parameter Name="DISTRICT_NAME" Type="String" />
                <asp:Parameter Name="DISTRICT_CODE" Type="String" />
                <asp:Parameter Name="POSTAL_CODE" Type="String" />
            </InsertParameters>
     </asp:SqlDataSource> 
        <asp:GridView id="GridView1" runat="server" AllowSorting="True"  AllowPaging="true" PageSize="15"
            AutoGenerateColumns="False" BorderColor="White"   Width="800px"
             DataKeyNames="DISTRICT_ID" DataSourceID="sdsBankAccType"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            onrowdeleted="GridView1_RowDeleted" onrowupdated="GridView1_RowUpdated">
            <Columns>
                <asp:BoundField DataField="DISTRICT_ID" HeaderText="DISTRICT_ID" 
                    ReadOnly="True" SortExpression="DISTRICT_ID" Visible="false" />
                <asp:BoundField DataField="DISTRICT_NAME" 
                    HeaderText="District Name" SortExpression="DISTRICT_NAME" />
                <asp:BoundField DataField="DISTRICT_CODE" HeaderText="District Code" 
                    SortExpression="DISTRICT_CODE" />
                    <asp:BoundField DataField="POSTAL_CODE" HeaderText="Post Code" 
                    SortExpression="POSTAL_CODE" />
                 <asp:CommandField ButtonType="Button" ShowEditButton="true" />   
            </Columns>
            <PagerStyle CssClass="pgr" />
            <AlternatingRowStyle CssClass="alt" />
         </asp:GridView> 
         
         </div>
         <div style="BACKGROUND-COLOR: royalblue">
            <strong>
             <span style="COLOR: white">Add New&nbsp;District&nbsp;</span>
            </strong>
         </div>
            <div>
        <asp:DetailsView id="dlvServiceType" runat="server" BorderColor="Silver" 
            DataKeyNames="DISTRICT_ID" DataSourceID="sdsBankAccType"  
             AutoGenerateRows="False" DefaultMode="Insert"  CssClass="mGrid" PagerStyle-CssClass="pgr"  
                                 AlternatingRowStyle-CssClass="alt" GridLines="None" BorderStyle="None"
            Font-Names="Times New Roman" Font-Size="11pt"  Width="250px"
            oniteminserted="dlvServiceType_ItemInserted">
             <PagerStyle CssClass="pgr" />
           <Fields>
            <asp:BoundField DataField="DISTRICT_ID" HeaderText="DISTRICT_ID" ReadOnly="True" 
                                SortExpression="DISTRICT_ID" Visible="false"></asp:BoundField>
            <asp:BoundField DataField="DISTRICT_NAME" HeaderText="District Name" 
                                SortExpression="DISTRICT_NAME">
            </asp:BoundField>
            <asp:BoundField DataField="DISTRICT_CODE" HeaderText="District Code" 
                                SortExpression="DISTRICT_CODE">
            </asp:BoundField>
               <asp:BoundField DataField="POSTAL_CODE" HeaderText="Post Code" 
                   SortExpression="POSTAL_CODE" />
           <asp:CommandField ButtonType="Button" ShowInsertButton="true" />        
           </Fields>
           <AlternatingRowStyle CssClass="alt" />
        </asp:DetailsView>
      </div>
   </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>