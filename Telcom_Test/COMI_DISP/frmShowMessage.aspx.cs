using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMI_DISP_frmShowMessage : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 10));
    }

}
