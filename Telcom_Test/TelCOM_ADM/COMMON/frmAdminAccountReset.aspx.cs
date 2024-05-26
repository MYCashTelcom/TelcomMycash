using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMMON_frmAdminAccountReset : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
    protected void btnAdmin_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strSql = " SELECT SU.SYS_USR_DNAME, SU.LOCKED_STATUS, SU.PASSWORD_EXPIRED_DATE, SU.CLICK_FAILURE FROM CM_SYSTEM_USERS SU WHERE SYS_USR_LOGIN_NAME = '" + txtUserName.Text.ToString() + "'";

        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

        dtvClient.DataSource = oDs;
        dtvClient.DataBind();

        if (dtvClient.Rows.Count > 0)
        {
            dtvClient.Visible = true;
            btnResetAdminPIN.Visible = true;
        }
        else
        {
            lblMessage.Text = "Information Not Found.";
            btnResetAdminPIN.Visible = false;
            dtvClient.Visible = false;
        }
    }
    protected void btnResetAdminPIN_Click(object sender, EventArgs e)
    {
        string strSysUserLoginName = txtUserName.Text.Trim();

        string strResult = objServiceHandler.ResetSystemUserPassword(strSysUserLoginName);

        if (strResult.Equals("Successful"))
        {
            lblMessage.Text = "Account unlocked successfully. Please reset password";
        }

        SaveAuditInfo("Update", "PIN reset of " + strSysUserLoginName + " ");
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}