<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSMSOption.aspx.cs" Inherits="COMMON_frmSMSOption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
         .Table
         {
         	 font-size:12px;
         	 text-align:right;         	
         	}    
        .style1
        {
            width: 151px;
        }
    </style>
    <script type="text/javascript">
       window.onload=function test ()
        {
        var dt = new Date();
        document.getElementById("hidden").value =dt.toString();
        }
        
//       ===============sms_Count======== 
//       Developed By: Md. Asaduzzaman Dated: 01-03-2013
//       Modified By: Tanjil Alam; Dated: 08-07-2013
//       ===============================================
        function Counts(x, maxLength)
            {
            document.getElementById("lblCharac").innerHTML = 
            document.getElementById(x).value.length;             
            
          if(document.getElementById(x).value.length > 160 )
          {
          if (document.getElementById(x).value.length > maxLength)
          {
          
            alert("Max characters allowed are " + maxLength);
            document.getElementById(x).value = document.getElementById(x).value.substr(0, maxLength);
            document.getElementById("lblCharac").innerHTML=document.getElementById(x).value.length;
            
//            if(document.getElementById("lblCharac").innerHTML == 0)
//            {
//            document.getElementById("lblSMSno").innerHTML =0;
//            }
//            else if(document.getElementById(x).value.length > 0 && document.getElementById(x).value.length < 161)
//            {
//            document.getElementById("lblSMSno").innerHTML =1;
//            }
            return false;
           }
         }
        }
        
        function CountsCharacter(x, maxLength)
            {
            document.getElementById("lblCharacterCount").innerHTML = 
            document.getElementById(x).value.length;             
            
          if(document.getElementById(x).value.length > 160 )
          {
          if (document.getElementById(x).value.length > maxLength)
          {
          
            alert("Max characters allowed are " + maxLength);
            document.getElementById(x).value = document.getElementById(x).value.substr(0, maxLength);
            document.getElementById("lblCharacterCount").innerHTML=document.getElementById(x).value.length;            
            return false;
           }
         }
        }
    </script>
    <title>Manage SMS Broadcast</title>
    <style type="text/css">
        .style1
        {
            width: 170px;
            
        }
    </style>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">  
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
       <asp:SqlDataSource ID="sdsAcccntRank" runat="server" 
           ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
           ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
           SelectCommand="SELECT ACCNT_RANK_ID,RANK_TITEL FROM ACCOUNT_RANK WHERE REMARKS='MBL' AND STATUS='A' AND SHORT_CODE IN('D','SD','SA','A','C')">
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="sdsAccPackage" runat="server" 
           ConnectionString="<%$ ConnectionStrings:oracleConString %>" 
           ProviderName="<%$ ConnectionStrings:oracleConString.ProviderName %>" 
           SelectCommand="SELECT SERVICE_PKG_ID,SERVICE_PKG_NAME FROM SERVICE_PACKAGE WHERE SERVICE_PKG_STATUS='A'">
       </asp:SqlDataSource>
         <asp:Panel ID="pnlTop" runat="server" >
           <table width="100%" class="Top_Panel">
            <tr>
             <td class="style1">
                Manage SMS Broadcast
             </td>
             <td align="left">&nbsp;&nbsp;                
                 
             </td>
             <td>             
             </td>
             <td align="left">
              <asp:Label ID="lblMsg" runat="server" Font-Bold="True">
                  </asp:Label>
             </td>
             <td align="left">
              <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                 <ProgressTemplate>
                    <img alt="Loading"  src="../resources/images/loading.gif" />&nbsp;&nbsp;                    
                 </ProgressTemplate>
              </asp:UpdateProgress>
             </td>
             </tr>
            </table>
          </asp:Panel> 
            <fieldset style="border-color: #FFFFFF; width:530px;">
            <legend>SMS Broadcast.</legend>
            <table>
                <tr style="background-color: lightgrey">
                 <td>
                  <table>
                   <tr>
                    <td align="right">                                    
                     <asp:Label ID="lblSelectionType" runat="server"  Text="Select Account Type"></asp:Label>
                    </td>
                    <td align="left" valign="top">
                      <asp:RadioButtonList ID="rdbSelectiontype"   runat="server" AutoPostBack="True"
                           RepeatDirection="Horizontal" RepeatLayout="Flow" >
                            <asp:ListItem Selected="True" Value="ALL">All Account</asp:ListItem>
                            <asp:ListItem  Value="A">Active Account</asp:ListItem>
                            <asp:ListItem  Value="I">Idle Account</asp:ListItem>
                      </asp:RadioButtonList>
                    </td>
                   </tr> 
                   <tr>
                     <td align="right">
                         <asp:Label ID="lblAccntRank" runat="server" Text="Account Rank"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlRank" runat="server" DataSourceID="sdsAcccntRank" 
                             DataTextField="RANK_TITEL" DataValueField="ACCNT_RANK_ID">
                         </asp:DropDownList>
                     </td>
                   </tr>                          
                   <tr>
                    <td valign="middle" align="right">
                       <asp:Label ID="Label1" runat="server"  Text="SMS Content"></asp:Label> 
                    </td>
                    <td align="left">
                      <asp:TextBox ID="txtSMSContent" runat="server" onkeyup="Counts(this.id,160)" TextMode="MultiLine" Width="310px" 
                            Height="77px" MaxLength="160"></asp:TextBox>
                       </td>
                  </tr>  
                  <tr>
                    <td>
                    <%--  <asp:Label ID="lblSMSno" runat="server" Text="Label"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblCharac" runat="server" Text=""></asp:Label>
                        
                    </td>
                  </tr>                
                <tr>
                    <td>
                    
                    </td>
                    <td align="left">
                       <asp:Button ID="btnSMSSend" runat="server"  OnClick="btnSMSSend_Click" Text="Send SMS" /><br />
                        
                    </td>
                </tr>                
              </table>
            </td> 
            </tr>
           </table>
           </fieldset>   
           <br />
            <fieldset style="border-color: #FFFFFF; width:530px;">
            <legend>SMS Broadcast by Package.</legend>
            <table>
                <tr style="background-color: lightgrey">
                 <td>
                  <table>
                   <tr>
                    <td align="right">                                    
                     <asp:Label ID="Label2" runat="server"  Text="Select Account Type"></asp:Label>
                    </td>
                    <td align="left" valign="top">
                      <asp:RadioButtonList ID="rblPkgAccType"   runat="server" AutoPostBack="True"
                           RepeatDirection="Horizontal" RepeatLayout="Flow" >
                            <asp:ListItem Selected="True" Value="ALL">All Account</asp:ListItem>
                            <asp:ListItem  Value="A">Active Account</asp:ListItem>
                            <asp:ListItem  Value="I">Idle Account</asp:ListItem>
                      </asp:RadioButtonList>
                          <asp:CheckBox ID="chkActive" runat="server" Text="Active Only" />
                    </td>
                   </tr> 
                   <tr>
                     <td align="right">
                         <asp:Label ID="Label3" runat="server" Text="Account Package"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlPkgList" runat="server" DataSourceID="sdsAccPackage" 
                             DataTextField="SERVICE_PKG_NAME" DataValueField="SERVICE_PKG_ID">
                         </asp:DropDownList>
                     </td>
                   </tr>                          
                   <tr>
                    <td valign="middle" align="right">
                       <asp:Label ID="Label4" runat="server"  Text="SMS Content"></asp:Label> 
                    </td>
                    <td align="left">
                      <asp:TextBox ID="txtPkgSMSContent" runat="server" onkeyup="CountsCharacter(this.id,160)" TextMode="MultiLine" Width="310px" 
                            Height="77px" MaxLength="160"></asp:TextBox>
                       </td>
                  </tr>  
                  <tr>
                    <td>
                    <%--  <asp:Label ID="lblSMSno" runat="server" Text="Label"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblCharacterCount" runat="server" Text=""></asp:Label>
                        
                    </td>
                  </tr>                
                <tr>
                    <td>
                    
                    </td>
                    <td align="left">
                       <asp:Button ID="btnPkgSMSSend" runat="server"  Text="Send SMS" onclick="btnPkgSMSSend_Click" /><br />                        
                    </td>
                </tr>                
              </table>
            </td> 
            </tr>
           </table>
          </fieldset>
     </ContentTemplate>       
    </asp:UpdatePanel>      
    </form>
</body>
</html>
