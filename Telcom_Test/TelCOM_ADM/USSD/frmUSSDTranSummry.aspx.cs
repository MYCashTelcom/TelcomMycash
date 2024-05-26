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

public partial class USSD_frmUSSDTranSummry : System.Web.UI.Page
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
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string strDateRange = "";
        if (rbtnAllDateRange.SelectedValue == "0")
        {
            strSql = "  SELECT SERVICE_ACCESS_CODE,SERVICE_TITLE,COUNT(REQUEST_ID)NUMBER_OF_TRAN,SUM(Round(AIRTEL_COMMISSION,3)) AIRTEL_COMMISSION FROM BDMIT_ERP_101.CAS_COMMISSION_DISBURSEMENT  GROUP BY SERVICE_ACCESS_CODE,SERVICE_TITLE ";

        }
        else if (rbtnAllDateRange.SelectedValue == "1")
        {
            // strDateRange = " AND CL.CREATION_DATE BETWEEN TO_DATE ('" + dptFromDate.DateString + "','dd/mm/yyyy HH24:MI:SS') AND TO_DATE ('" + dtpToDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";
            strSql = " SELECT SERVICE_ACCESS_CODE,SERVICE_TITLE,COUNT(REQUEST_ID)NUMBER_OF_TRAN,SUM(Round(AIRTEL_COMMISSION,3)) AIRTEL_COMMISSION FROM BDMIT_ERP_101.CAS_COMMISSION_DISBURSEMENT WHERE  TO_CHAR(TO_DATE(CAS_TRAN_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dptFromDate.DateString + "') AND TO_DATE('" + dtpToDate.DateString + "') GROUP BY SERVICE_ACCESS_CODE,SERVICE_TITLE ";
        }

        try
        {
            // Session["CompanyBranch"] = "ROBI";
            SaveAuditInfo("View", "Wallet Reports");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../USSD/frmUSSDTranSummry.aspx";
            Session["ReportFile"] = "../USSD/crptUSSDTranSummary.rpt";            
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
