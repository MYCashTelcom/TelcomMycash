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

public partial class COMMON_frmEmployeeReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strSql= "";
        try
        {
            strSql = " SELECT DISTINCT CL.CLINT_NAME,ACCNT_NO,ACCNT_STATE,CLINT_ADDRESS1,CL.CREATION_DATE, '"
                   + ddlEmployee.SelectedItem.ToString()+"' REPORT_NAME"
                   + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE SERVICE_PKG_ID='" + ddlEmployee.SelectedValue.ToString() + "' "
                   + "  AND AL.CLINT_ID =CL.CLINT_ID ORDER BY CREATION_DATE ASC ";

            SaveAuditInfo("View", " Employee Report");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmEmployeeReport.aspx";
            Session["ReportFile"] = "../COMMON/EMPLOYEE_REPORT.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
       objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
