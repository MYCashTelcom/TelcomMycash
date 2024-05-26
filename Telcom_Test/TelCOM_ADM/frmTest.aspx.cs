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
using System.IO;

public partial class Forms_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strDirectory = String.Format("{0:yyyyMMdd}", System.DateTime.Today);
        //################### Creat directory yyyymmdd_234 ######################
       // Directory.CreateDirectory(Server.MapPath("~/Region/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + ""));
       // Directory.CreateDirectory(Server.MapPath("~/Region/" +  + "" + TextBox2.Text + " " + strDirectory + "/" + "" + TextBox3.Text + ""));
        //Directory.CreateDirectory(Server.MapPath("~/Region/" + "" + TextBox1.Text + "/ "  +"" + TextBox2.Text + " " + strDirectory + "/" + "" + TextBox3.Text + ""));

    }
}
