<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title id="title" >       
        </title>
    <link rel="stylesheet" type="text/css" href="resources/css/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
           <asp:SqlDataSource ID="sqlLogin" runat="server" 
            SelectCommand="SELECT TITLE_BFR_LOGIN FROM CM_SYSTEM_INFO">
            </asp:SqlDataSource>
            
            
        <ext:Panel ID="Panel4" IDMode="Ignore" runat="server" Header="false" Border="false" /> <%--Html="<div class='message'>&nbsp;&nbsp;</div><div id='header'><h1>Microtechnology Financial System (FiS)</h1></div>"--%>
        <%--<div runat='server' id='header'></div>--%>
        <ext:Panel ID="Panel2" runat="server" Height="250" Header="false" HideBorders="True" BodyStyle="padding:50px 0;">
        <Body>  
        <%----%>          
            <ext:CenterLayout ID="CenterLayout1" runat="server">            
            <ext:Panel 
                ID="pnlLogin" 
                runat="server" 
                Title="Login"
                BodyStyle="padding:5px 20px;"
                Width="350"
                AutoHeight="true"
                Frame="true"
                ButtonAlign="Center"
                Icon="Lock" >
                <Body>
                    <ext:FormLayout ID="FormLayout1" runat="server">                        
                        <Anchors>                            
                            <ext:Anchor Horizontal="100%">
                                <ext:TextField 
                                    ID="txtUsername" 
                                    runat="server" 
                                    FieldLabel="Username" 
                                    AllowBlank="false"
                                    BlankText="Your username is required."                                    
                                    />
                            </ext:Anchor>
                            <ext:Anchor Horizontal="100%">
                                <ext:TextField 
                                    ID="txtPassword" 
                                    runat="server" 
                                    InputType="Password" 
                                    FieldLabel="Password" 
                                    AllowBlank="false" 
                                    BlankText="Your password is required."                                    
                                    />
                            </ext:Anchor>
                        </Anchors>
                    </ext:FormLayout>
                </Body>
                <Buttons>
                    <ext:Button ID="btnLogin" runat="server" Text="Login" Icon="Accept">
                        <Listeners>
                            <Click Handler="
                                if(!#{txtUsername}.validate() || !#{txtPassword}.validate()) {
                                    Ext.Msg.alert('Error','The Username and Password fields are both required');
                                    // return false to prevent the btnLogin_Click Ajax Click event from firing.
                                    return false; 
                                }" />
                        </Listeners>
                        <AjaxEvents>
                            <Click OnEvent="btnLogin_Click">
                                <EventMask ShowMask="true" Msg="Login..." MinDelay="500" />
                            </Click>
                        </AjaxEvents>
                    </ext:Button>
                    <ext:Button ID="btnClear" runat="server" Text="Clear" Icon="Decline">
                        <AjaxEvents>
                            <Click OnEvent="btnClear_Click" />
                        </AjaxEvents>
                    </ext:Button>
                </Buttons>
            </ext:Panel>
            </ext:CenterLayout>
        </Body>
    </ext:Panel>
   <%-- <ext:Panel ID="Panel5" IDMode="Ignore" runat="server" Header="false" Border="false" Html="<div class='copyrightmessage'></div>">
   
     <%--Copyright © 2009 Bangladesh Microtechnology Ltd.--%>      
    <%--</ext:Panel>--%>
     <div id="CopyRightMsg" runat="server" class="copyrightmessage"></div>
    </form>
</body>
</html>




