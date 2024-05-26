using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Monitor_frmBGScheduleMaintain : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
            lblMessage.Text = "";
        }
        // Start - Check active session
        try
        {
            string sess_id = HttpContext.Current.Session.SessionID;
            string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

            if (strSessID == Session["Sess_ID"].ToString())
            {
                Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
            }
            else
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
        // End - Check active session
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
    protected void btnStartProcess_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
        string strScript = b.CommandArgument;
        clsServiceHandler objSrv = new clsServiceHandler();

        lblMessage.Text = objSrv.ExecuteScript(strScript);
    }
    protected void btnStopProcess_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
        string strScript = b.CommandArgument;
        clsServiceHandler objSrv = new clsServiceHandler();

        lblMessage.Text = objSrv.ExecuteScript(strScript);
    }
}
