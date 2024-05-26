<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUserGroupReport.aspx.cs" Inherits="COMMON_frmUserGroupReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Group Report</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
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
         <asp:SqlDataSource ID="sdsUserGroup" runat="server" 
             ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
             ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
             SelectCommand="SELECT &quot;SYS_USR_GRP_ID&quot;, &quot;SYS_USR_GRP_TITLE&quot; FROM &quot;CM_SYSTEM_USER_GROUP&quot;">
         </asp:SqlDataSource>
     
       <div style="BACKGROUND-COLOR: royalblue">
        <strong>
         <span style="COLOR: white">
           &nbsp;Manage User Group Report&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
          &nbsp;
         </span>
        </strong>
       </div>
      <table>
         <tr>
            <td>
                <fieldset style="border-color: #FFFFFF; width:300px; height:100px">
            <legend><strong style="color: #666666">Group Wise User</strong></legend>
           <asp:Label ID="lblSelectGroup" runat="server" Text="Select Group"></asp:Label>
           <asp:DropDownList ID="ddlGroup" runat="server" DataSourceID="sdsUserGroup" 
                DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID">
           </asp:DropDownList><br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" />
       </fieldset>
            </td>
            <td>
                 <fieldset style="border-color: #FFFFFF; width:300px; height:100px">
            <legend><strong style="color: #666666">Group Wise Permission</strong></legend>
            <asp:Label ID="lblGroup" runat="server" Text="Select Group"></asp:Label>
           <asp:DropDownList ID="ddlSelectGroup" runat="server" DataSourceID="sdsUserGroup" 
                DataTextField="SYS_USR_GRP_TITLE" DataValueField="SYS_USR_GRP_ID">
           </asp:DropDownList><br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnReport" runat="server" Text="View" 
                onclick="btnReport_Click"  />
       </fieldset> 
            
            </td>
         </tr>
      </table>
      
      
           
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
