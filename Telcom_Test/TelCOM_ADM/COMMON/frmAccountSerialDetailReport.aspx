<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccountSerialDetailReport.aspx.cs" Inherits="COMI_DISP_frmAccountSerialDetailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script language="javascript" type="text/javascript">
        function ShowProgressBar() {
            var iFrame = document.getElementById('Iframe1');
            iFrame.src = "frmShowMessage.aspx";
        } 
      function ValidateElement()
       {
        var mobileNumber=document.getElementById('txtMobileNo').value;
        var lengthMobile=mobileNumber.length;
        if(lengthMobile < 14)
        {
        document.getElementById("lblResult").innerText=" Mobile Number is Less than 14";
       var ok= confirm("Enter Correct Mobile number."); 
         if(ok)
         {
          return false;
          document.getElementById("lblResult").focus();
         }
         else
         {
         
         }
        }
       }       
    </script>
    
    <title>Account Serial Details Report</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 170px;
        }
    </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
            <div style="background-color: royalblue;">
                <strong><span style="color: white">Account Serial Details.  
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblResult" runat="server" Font-Bold="True"></asp:Label>
                </span></strong>
            </div>
            <fieldset style="border-color: #FFFFFF; width:430px;">
            <legend>Account Serial Details.</legend>
            <table>
                <tr style="background-color: lightgrey">
                    <td>
                        <table>
                            <tr>
                                <td align="right">                                    
                                    <asp:Label ID="lblSelectionType" runat="server" Font-Size="10pt" Text="Type  Selection:"></asp:Label>
                                </td>
                                <td align="left" valign="bottom">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButtonList ID="rdbSelectiontype"  Font-Size="10pt" runat="server" AutoPostBack="True"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                        <asp:ListItem Selected="True" Value="U">Used</asp:ListItem>
                                        <asp:ListItem  Value="A">UnUsed</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>                           
                            <tr>
                                <td valign="middle" align="right">
                                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Text="Serial No From:"></asp:Label>                                  
                                </td>
                                <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtStartSLNo" runat="server" Width="105px"></asp:TextBox>
                                     &nbsp;&nbsp;<asp:Label ID="lblTo" runat="server" Font-Size="10pt" Text="To"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtEndSLNo" runat="server" Width="105px"></asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                             <td align="right"> 
                                 <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No:" Font-Size="10pt"></asp:Label>
                             </td>
                             <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMobileNo" runat="server" Width="246px" ></asp:TextBox>
                             </td>
                            </tr>
                <tr>
                    <td>
                    
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnReportPrint" runat="server"  OnClick="btnReportPrint_Click" Text="Report Generate" />
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
