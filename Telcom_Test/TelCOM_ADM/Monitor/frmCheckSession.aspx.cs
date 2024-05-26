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

public partial class Monitor_frmCheckSession : System.Web.UI.Page
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
        sdsDataBaseSession.DataBind();
        gdvDatabaseSession.DataBind();
    }

    protected void btnRemoveRow_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
        string strRowID = b.CommandArgument;
        clsServiceHandler objSrv = new clsServiceHandler();
        string strScript = "";
        string strTem = "";

        foreach (GridViewRow row in gdvDatabaseSession.Rows)
        {
            strTem=row.Cells[1].Text;
            if (strTem.Equals(strRowID) == true)
            {
                strScript = "ALTER SYSTEM DISCONNECT SESSION '" + strRowID + "," + row.Cells[2].Text + "' IMMEDIATE";
                lblMessage.Text = objSrv.ExecuteScript(strScript);                
            }

        }
        sdsDataBaseSession.DataBind();
        gdvDatabaseSession.DataBind();
    }
}
