using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SYS_System_Audit : System.Web.UI.Page
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
                DateTime dt = DateTime.Now;
                if (dptFromDate.DateString != "")
                {
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);//.AddMinutes(-120));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);//.AddMinutes(5));
                    LoadRequestList();
                }
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

    private void LoadRequestList()
    {
        string strSQL = "",strRemarks="";
        if (txtRemarks.Text != "")
        {
            strRemarks = "AND SA.REMARKS LIKE '%" + txtRemarks.Text.ToString() + "%'";
        }
        strSQL = " SELECT SA.*,SU.SYS_USR_LOGIN_NAME FROM CM_SYSTEM_AUDIT SA,CM_SYSTEM_USERS SU "
               + " WHERE SA.SYS_USR_ID=SU.SYS_USR_ID "+strRemarks
               + " AND SA.OPERATION_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString 
               + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString 
               + "\',\'dd/mm/yyyy HH24:MI:SS\') "
               + " ORDER BY CM_SYS_AUDIT_ID DESC ";
        try
        {
            sdsSystemAudit.SelectCommand = strSQL;
            sdsSystemAudit.DataBind();
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
        SaveAuditInfo("View", "Audit Info");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LoadRequestList();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadRequestList();
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
