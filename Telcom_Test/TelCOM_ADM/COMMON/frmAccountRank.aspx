<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountRank.aspx.cs" Inherits="COMMON_frmAccountRank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Rank</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <div style="background-color: royalblue"> <strong><span style="color: white">Manage Account Rank</span></strong></div>
    <asp:SqlDataSource ID="sdsRankInfo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        InsertCommand="INSERT INTO ACCOUNT_RANK (RANK_TITEL,HIERARCHY,GRADE,SHORT_CODE,REMARKS,STATUS,SHORT_CODE_NAME)
                        VALUES (:RANK_TITEL,:HIERARCHY,:GRADE,:SHORT_CODE,:REMARKS,:STATUS,:SHORT_CODE_NAME) " 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        SelectCommand="SELECT ACCNT_RANK_ID,RANK_TITEL,HIERARCHY,GRADE,SHORT_CODE,REMARKS,STATUS,SHORT_CODE_NAME FROM ACCOUNT_RANK WHERE ACCNT_RANK_ID NOT IN '120519000000000001' ORDER BY ACCNT_RANK_ID" 
        
        UpdateCommand="UPDATE ACCOUNT_RANK SET RANK_TITEL=:RANK_TITEL,HIERARCHY=:HIERARCHY,GRADE=:GRADE,SHORT_CODE=:SHORT_CODE,REMARKS=:REMARKS,STATUS=:STATUS,SHORT_CODE_NAME=:SHORT_CODE_NAME WHERE ACCNT_RANK_ID=:ACCNT_RANK_ID">
        <UpdateParameters>
            <asp:Parameter Name="RANK_TITEL" />
            <asp:Parameter Name="HIERARCHY" />
            <asp:Parameter Name="REMARKS" />
            <asp:Parameter Name="ACCNT_RANK_ID" />
            <asp:Parameter Name="GRADE" />
            <asp:Parameter Name="SHORT_CODE" />
            <asp:Parameter Name="STATUS" />
            <asp:Parameter Name="SHORT_CODE_NAME" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="RANK_TITEL" />
            <asp:Parameter Name="HIERARCHY" />
            <asp:Parameter Name="REMARKS" />
            <asp:Parameter Name="GRADE" />
            <asp:Parameter Name="SHORT_CODE" />
            <asp:Parameter Name="STATUS" />
            <asp:Parameter Name="SHORT_CODE_NAME" />
        </InsertParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsParent" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT ACCNT_RANK_ID,RANK_TITEL FROM ACCOUNT_RANK ORDER BY ACCNT_RANK_ID">
      <%--SelectCommand="SELECT ACCNT_RANK_ID,RANK_TITEL FROM ACCOUNT_RANK WHERE SHORT_CODE IN ('NA','D','SD','SA','A','C') ORDER BY ACCNT_RANK_ID">--%>
    </asp:SqlDataSource>
        

    <asp:SqlDataSource ID="sdsShortCode" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        SelectCommand="SELECT DISTINCT &quot;SHORT_CODE&quot; FROM &quot;ACCOUNT_RANK&quot;">
     </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsShortRank" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
        ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
        
        SelectCommand="SELECT DISTINCT SHORT_CODE, DECODE(SHORT_CODE,'D','MBL Branch','SD','MBL Distributor','SA','MBL DSE','A','MBL Agent','C','MBL Customer',SHORT_CODE) SHORT_CODE_RANK FROM ACCOUNT_RANK WHERE SHORT_CODE IN ('D','SD','SA','A','C') ORDER BY SHORT_CODE_RANK ">
    </asp:SqlDataSource>

    <asp:GridView ID="gdvAccntRank" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
        BorderColor="#E0E0E0" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt" DataKeyNames="ACCNT_RANK_ID" 
        DataSourceID="sdsRankInfo" onrowupdated="gdvAccntRank_RowUpdated" 
        onrowupdating="gdvAccntRank_RowUpdating">
        <Columns>
            <asp:BoundField DataField="ACCNT_RANK_ID" HeaderText="ACCNT_RANK_ID" 
                ReadOnly="True" SortExpression="ACCNT_RANK_ID" Visible="False" />
            <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank Title" 
                SortExpression="RANK_TITEL" />
            <asp:TemplateField HeaderText="Rank Type" SortExpression="SHORT_CODE">
              <EditItemTemplate>
                  <asp:DropDownList ID="DropDownList6" runat="server" DataTextField="SHORT_CODE" DataValueField="SHORT_CODE"
                DataSourceID="sdsShortCode" SelectedValue='<%# Bind("SHORT_CODE") %>'>
                  </asp:DropDownList>
              </EditItemTemplate>
              <ItemTemplate>
                  <asp:DropDownList ID="DropDownList7" runat="server" DataTextField="SHORT_CODE" DataValueField="SHORT_CODE"
                DataSourceID="sdsShortCode" SelectedValue='<%# Bind("SHORT_CODE") %>' Enabled="false">
                  </asp:DropDownList>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent" SortExpression="HIERARCHY" >
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsParent"                     
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" 
                        SelectedValue='<%# Bind("HIERARCHY") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList20" runat="server" DataSourceID="sdsParent" AppendDataBoundItems="true" 
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" Enabled="False" 
                        SelectedValue='<%# Bind("HIERARCHY") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Grade" SortExpression="GRADE">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList5" runat="server" 
                        SelectedValue='<%# Bind("GRADE") %>'>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("GRADE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SHORT_CODE" HeaderText="Short Code" 
                SortExpression="SHORT_CODE" >
                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="SHORT_CODE_NAME" HeaderText="Short Code Name" 
                SortExpression="SHORT_CODE_NAME" >
                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
            </asp:BoundField>
            
             <asp:TemplateField HeaderText="Status" SortExpression="STATUS">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("STATUS") %>'>
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Idle</asp:ListItem>                       
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>                    
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("STATUS") %>' Enabled="false">
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Idle</asp:ListItem>                        
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" 
                SortExpression="REMARKS" />
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
        </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    </asp:GridView>
    
     <div style="background-color: royalblue"> 
       <strong>
        <span style="color: white">
          Account Rank Creation
        </span>
       </strong>
     </div>
        <asp:DetailsView ID="dtvRank" runat="server" AutoGenerateRows="False" 
        DataKeyNames="ACCNT_RANK_ID" DataSourceID="sdsRankInfo" DefaultMode="Insert" 
        GridLines="None" Height="50px" Width="350px" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" BorderStyle="None" 
            oniteminserted="dtvRank_ItemInserted" 
            oniteminserting="dtvRank_ItemInserting">
            <PagerStyle CssClass="pgr" />
        <Fields>
            <asp:BoundField DataField="ACCNT_RANK_ID" HeaderText="ACCNT_RANK_ID" 
                ReadOnly="True" SortExpression="ACCNT_RANK_ID" Visible="False" />
            <asp:BoundField DataField="RANK_TITEL" HeaderText="Rank Title" 
                SortExpression="RANK_TITEL" >
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Rank Type">
            <InsertItemTemplate>
                <asp:DropDownList ID="ddlShortCode" runat="server"  DataTextField="SHORT_CODE_RANK" DataValueField="SHORT_CODE"
                DataSourceID="sdsShortRank" SelectedValue='<%# Bind("SHORT_CODE") %>'>
                </asp:DropDownList>
            </InsertItemTemplate>
             <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent" SortExpression="HIERARCHY">
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsParent" 
                        DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID" 
                        SelectedValue='<%# Bind("HIERARCHY") %>'>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grade" SortExpression="REWARD">
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        SelectedValue='<%# Bind("GRADE") %>'>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
             <asp:BoundField DataField="SHORT_CODE" HeaderText="Short Code" 
                SortExpression="SHORT_CODE" >
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="SHORT_CODE_NAME" HeaderText="Short Code Name" 
                SortExpression="SHORT_CODE_NAME" >
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Status" SortExpression="STATUS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("STATUS") %>'>
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Idle</asp:ListItem>                       
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("STATUS") %>'>
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Idle</asp:ListItem>                        
                    </asp:DropDownList>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("REMARKS") %>' 
                        Height="53px" TextMode="MultiLine"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowInsertButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Fields>
            <AlternatingRowStyle CssClass="alt" />
    </asp:DetailsView>
    </form>
</body>
</html>
