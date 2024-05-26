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

public partial class COMMON_frmMngSrvNotification : System.Web.UI.Page
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
    protected void btnPrintPreview_Click(object sender, EventArgs e)
    {
        String strSQL = "";
        
         strSQL = "SELECT S.SERVICE_ID, S.SERVICE_ACCESS_CODE, S.SERVICE_SHORT_CODE," 
                      + "S.SERVICE_TITLE, S.SERVICE_CREATION_DATE, S.SERVICE_STATE, "
                      + "S.SERVICE_TYPE_ID, S.SERVICE_INTERNAL_CODE, S.SERVICE_REMARKS, "
                      + "S.SERVICE_OTP, S.SERVICE_SQA, S.SERVICE_REQF_MESSAGE, "
                      + "S.SERVICE_REPLY_CODE, S.NOTIFI_FOR_APARTY, S.NOTIFI_FOR_BPARTY, "
                      + "S.NOTIFI_FOR_CPARTY, S.NOTIFI_FOR_DUPLICATE, S.NOTIFI_FOR_EXPIRY, "
                      + "S.SERVICE_REQ_VALID_PERIOD, S.ALLOW_DUPLICATE_REQ, S.CDR_RECONCIL_PERIOD "
                      + "FROM SERVICE_LIST S WHERE S.SERVICE_TYPE_ID='" + ddlServiceType.SelectedValue.ToString() + "'";

         try
         {
             Session["CompanyBranch"] = "ROBI";
             Session["ReportSQL"] = strSQL;
             Session["RequestForm"] = "../COMMON/frmMngSrvNotification.aspx";
             Session["ReportFile"] = "../COMMON/COM_Service_Notification.rpt";
             SaveAuditInfo("Preview", "Service Notification");
             Response.Redirect("../COM/COM_ReportView.aspx");
            
         }
         catch (Exception ex)
         {
             ex.Message.ToString();
         }
    }
    protected void gdvServiceList_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Service Notification");
        
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
