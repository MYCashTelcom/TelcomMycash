<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMISTopRequest.aspx.cs" Inherits="MIS_frmMISTopRequest" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Top Request</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    
     <style type="text/css">
    input[type="submit"]
    {
     height:30px;
     width:80px;
    }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
      <asp:SqlDataSource ID="sdsRankTitle" runat="server"
                ConnectionString="<%$ ConnectionStrings:oracleConString %>"
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="SELECT RANK_TITEL FROM ACCOUNT_RANK where STATUS='A' ORDER BY ACCNT_RANK_ID ">
      </asp:SqlDataSource>
      <asp:SqlDataSource ID="sdsDistrict" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
                <%--SELECT &quot;DISTRICT_ID&quot;, &quot;DISTRICT_NAME&quot; FROM &quot;MANAGE_DISTRICT&quot;--%>
      </asp:SqlDataSource>           
      <asp:SqlDataSource ID="sdsThana" runat="server" 
                ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
                ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
                SelectCommand="">
                <%--SELECT &quot;THANA_ID&quot;, &quot;THANA_NAME&quot;, &quot;DISTRICT_ID&quot; FROM &quot;MANAGE_THANA&quot;--%>
      </asp:SqlDataSource>
      <div style="background-color: royalblue">
       <strong>
            <span style="color: white">Top Performer Report:</span>
       </strong>
      </div>
      
       <fieldset style="border-color: #FFFFFF; width:500px; height:190px;float:left;">
           <legend>Performance Report</legend>
           <table>
            <tr>
             <td>
               <asp:RadioButtonList ID="rblAllRank" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">All Rank</asp:ListItem>
                    <asp:ListItem Value="1">Individual Rank</asp:ListItem>
               </asp:RadioButtonList>
           </td>
            <td>
                &nbsp;&nbsp;&nbsp;
               <asp:DropDownList ID="ddlRankList" runat="server" DataSourceID="sdsRankTitle" 
                      DataTextField="RANK_TITEL" DataValueField="RANK_TITEL">
               </asp:DropDownList>
            </td>
           </tr>
           </table>
            
              <asp:RadioButton ID="rbtnDateRange" runat="server"  Text="Date Range" 
                  Checked="True"/>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblFromDate" runat="server" Text="From Date" ></asp:Label>                                   
           &nbsp;&nbsp;&nbsp;&nbsp;
             <cc1:GMDatePicker ID="dptFromDate" runat="server"  CalendarTheme="Silver" 
                 DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
                 TextBoxWidth="90" Height="18px" Font-Size="X-Small">
               <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
             </cc1:GMDatePicker>&nbsp;
           To Date &nbsp;
            <cc1:GMDatePicker ID="dtpToDate" runat="server" CalendarTheme="Silver" 
                 DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
                 TextBoxWidth="90" Height="18px" Font-Size="X-Small">
                 <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
            </cc1:GMDatePicker>
           <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblFirst" runat="server" Text="Top Performer"></asp:Label>
           
           <asp:TextBox ID="txtFirst" runat="server" Width="90px"></asp:TextBox>
           <br /> 
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp; 
                
           <asp:Button ID="btnView" runat="server" Text=" View " onclick="btnView_Click" />
          </fieldset>
          <fieldset style="border-color: #FFFFFF; width:500px; height:190px">
       <legend>Areawise Performance Report</legend>
       <table>
         <tr>
            <td style="height: 60px;width:100px;">
                <asp:RadioButtonList ID="rblAllArea" RepeatDirection="Vertical" AutoPostBack="true"
                    runat="server" onselectedindexchanged="rblAllArea_SelectedIndexChanged" >
                    <asp:ListItem Selected="True" Value="0">District Wise</asp:ListItem>                                        
                    <asp:ListItem Value="1">ThanaWise</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="height: 60px;">
                Select District:
                <asp:DropDownList ID="ddlDistrict" runat="server" Width="185px"  DataSourceID="sdsDistrict" 
                   AutoPostBack="true"  DataTextField="DISTRICT_NAME" DataValueField="DISTRICT_ID" 
                    onselectedindexchanged="ddlDistrict_SelectedIndexChanged" >
                </asp:DropDownList><br />                                    
                Select  Thana :
                <asp:DropDownList ID="ddlThana" runat="server" Width="185px"  DataSourceID="sdsThana" 
                    DataTextField="THANA_NAME" DataValueField="THANA_ID">
                </asp:DropDownList>                                    
            </td>
        </tr>
        <tr>
         <td style="width:175px;">
           <asp:RadioButtonList ID="rblAllIndiThanaDist" runat="server" 
                RepeatDirection="Horizontal">
                <asp:ListItem Value="0" Selected="True">All Rank</asp:ListItem>
                <asp:ListItem Value="1">Individual Rank</asp:ListItem>
           </asp:RadioButtonList>
       </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
           <asp:DropDownList ID="ddlIndividualRank" runat="server" Width="185px" DataSourceID="sdsRankTitle" 
                  DataTextField="RANK_TITEL" DataValueField="RANK_TITEL"/>
        </td>
       </tr>
       </table>
        &nbsp;
          <asp:RadioButton ID="rbtnThanaDistDateRange" runat="server"  Text="Date Range" 
              Checked="True"/>
      &nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="Label1" runat="server" Text="From Date" ></asp:Label>                                   
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <cc1:GMDatePicker ID="dtpThanaDistFdate" runat="server"  CalendarTheme="Silver" 
             DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative;" 
             TextBoxWidth="70" Height="18px" Font-Size="X-Small">
           <calendartitlestyle backcolor="#FFFFC0"  Font-Size="X-Small" />
         </cc1:GMDatePicker>&nbsp;
       To Date &nbsp;
        <cc1:GMDatePicker ID="dtpThanaDistTdate" runat="server" CalendarTheme="Silver" 
             DateFormat="dd/MMM/yyyy" MinDate="1900-01-01" Style="position: relative" 
             TextBoxWidth="70" Height="18px" Font-Size="X-Small">
             <calendartitlestyle backcolor="#FFFFC0" Font-Size="X-Small" />
        </cc1:GMDatePicker>
       <br />
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="Label2" runat="server" Text="Top Performer:"></asp:Label>       
       <asp:TextBox ID="txtTopPerformer" runat="server" Width="90px"></asp:TextBox>
       <br /> 
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp; 
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;     
       <asp:Button ID="btnViewThanaDist" runat="server" Text=" View " 
                  onclick="btnViewThanaDist_Click" />
      </fieldset>
      
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
