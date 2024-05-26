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

public partial class COMMON_frmUserGroupReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
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
    //------------------------- Group wise User Report----------------
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql = " SELECT UG.SYS_USR_GRP_TITLE,UG.SYS_USR_GRP_ID,SU.SYS_USR_DNAME,SU.SYS_USR_LOGIN_NAME,SU.SYS_USR_EMAIL,SU.STATUS FROM CM_SYSTEM_USER_GROUP UG,"
               + " CM_SYSTEM_USERS SU WHERE UG.SYS_USR_GRP_ID=SU.SYS_USR_GRP_ID AND UG.SYS_USR_GRP_ID='" + ddlGroup.SelectedValue.ToString() + "'"
               + " AND SU.SYS_USR_PASS IS NOT NULL ";
        try
        {
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmUserGroupReport.aspx";
            Session["ReportFile"] = "../COMMON/USER_GROUP_LIST.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("View", "User Group Report");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    //----------------------------- Group wise Permission Report ------------------
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql = " SELECT CSM.SYS_MENU_TITLE,CSM.SYS_MENU_ID,CUG.SYS_USR_GRP_TITLE FROM CM_SYSTEM_MENU CSM,"
               + " CM_SYSTEM_ACCESS_POLICY ACP,CM_SYSTEM_USER_GROUP CUG WHERE ACP.SYS_MENU_ID=CSM.SYS_MENU_ID "
               + " AND CUG.SYS_USR_GRP_ID=ACP.SYS_USR_GRP_ID AND CUG.SYS_USR_GRP_ID='" + ddlSelectGroup.SelectedValue.ToString() + "'";
        try
        {
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmUserGroupReport.aspx";
            Session["ReportFile"] = "../COMMON/USER_GROUP_PERMISSION.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("View", "User Group Report");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
