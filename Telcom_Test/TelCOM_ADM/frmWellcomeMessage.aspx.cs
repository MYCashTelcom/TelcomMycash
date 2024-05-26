using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_frmWellcomeMessage : System.Web.UI.Page
{
    clsGlobalSetup objGlobalSetup = new clsGlobalSetup();
    string strWelComeMsg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetWelComeMsg();
        lblWelComeMsg.Text = strWelComeMsg;

    }
    public string GetWelComeMsg()
    {
        strWelComeMsg = objGlobalSetup.GetWelComeMsg();
        return strWelComeMsg;
    }
}
