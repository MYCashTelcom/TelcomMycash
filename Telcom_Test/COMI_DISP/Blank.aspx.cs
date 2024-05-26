using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMI_DISP_Blank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout) - 10));
        string strMessage;
        strMessage = Session["Progress Msg"].ToString();
        Response.Write(strMessage);
    }
}
