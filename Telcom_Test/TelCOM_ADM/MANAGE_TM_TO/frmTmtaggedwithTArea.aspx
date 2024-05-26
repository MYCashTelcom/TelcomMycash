<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTmtaggedwithTArea.aspx.cs" Inherits="MANAGE_TM_TO_frmTmtaggedwithTArea" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Define TM tagging with Territory Area</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
      <style type="text/css">
         .table
         {
         	background-color:#fcfcfc ;
         	margin: 5px 0 10px 0;
         	border: solid 1px #525252;
            text-align: left;
            border-collapse:collapse;
            border-color:White;
         	}
        .table td{ padding: 2px; border: solid 1px #c1c1c1; color: #717171; font-size: 11px;}
         .div
         {
         	margin:5px 0 0 0;
         	}	
         .td
         {
         	text-align:right;
         	width:125px;
         	}	
         .style1
         {
             width: 672px;
         }
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
         	width:817px;         	
         	}	
         .Inser_Panel	
         {
             background-color:cornflowerblue;	
             font-weight:bold;
         	 color:White;
         }
     </style>
</head>
<body style="background-color: lightgrey;">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <asp:Panel ID="Panel1" runat="server" >
           <table width="100%" class="Top_Panel">
              <tr>
                <td> <strong>Define TM tagging with Territory Area</strong> </td>
                <td>
                </td>
                <td>
                </td>
                <td align="left">
                  <asp:Label ID="lblMessage" runat="server"  Text=""></asp:Label>
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
         
         <table>
          <tr>
           <td>
            <fieldset>
             <legend><strong></strong></legend>   
             <table>
              <tr>
               <td>
                <strong>Select Terrotory Maneger</strong>   
               </td>   
               <td>
                 <asp:DropDownList runat="server" ID="drpTmList" DataTextField="UPPER_HIERARCHY_INFO" DataValueField="ACCNT_ID"/>  
               </td>   
              </tr>
              <tr>
               <td>
                <strong>Select Area</strong>   
               </td>   
               <td>
                <asp:DropDownList runat="server" ID="drpArea" DataTextField="AREA_NAME" DataValueField="AREA_ID"/>   
                <asp:Button runat="server" ID="btnAreaInfo" Text="View Area Information" 
                       onclick="btnAreaInfo_Click"/>
               </td>   
              </tr>
              <tr>
                <td></td>  
                <td>
                  <asp:DetailsView ID="dtvAreaInfo" runat="server" AutoGenerateRows="False" DataKeyNames="AREA_NAME"
                          CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                               BorderStyle="None" Visible="False" >   
                           <PagerStyle CssClass="pgr" /> 
                           
                           <Fields>
                               <asp:BoundField DataField="AREA_NAME" HeaderText="Area Name" />
                               <asp:BoundField DataField="REGION_NAME" HeaderText="Region Name" />
                               <asp:BoundField DataField="TMINFO" HeaderText="TM Information" />
                           </Fields>
                           
                           <AlternatingRowStyle CssClass="alt" />
                          </asp:DetailsView>  
                </td>
              </tr>
              <tr>
               <td></td>   
               <td>
                 <asp:Button runat="server" ID="btnSave" Text="Save " onclick="btnSave_Click"/>  
               </td>   
              </tr>   
             </table>
            </fieldset>   
           </td>   
           <td></td>   
          </tr>   
         </table>
         
         
         </ContentTemplate>
        </asp:UpdatePanel> 
    
    
    </form>
</body>
</html>
