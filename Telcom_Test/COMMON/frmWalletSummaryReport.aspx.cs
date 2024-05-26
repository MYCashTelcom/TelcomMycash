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

public partial class COMMON_frmKYCReport : System.Web.UI.Page
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSrvHandler = new clsServiceHandler();
        string strSql = "",strMsg="";

        strMsg=objSrvHandler.ExecuteProcedure("WALLET_SUMMERY");
        if (strMsg != "")
        {

            strSql = " SELECT * FROM TEMP_WALLET_SUMMERY ORDER BY RANK_TITEL ASC";
            try
            {
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmWalletSummaryReport.aspx";
                Session["ReportFile"] = "../COMMON/WALLET_SUMMARY.rpt";
                SaveAuditInfo("View", "Wallet Summary Report");
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
            lblMessage.Text = "Procedure not execute successfully.";
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnCustomizeRpt_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSrvHandler = new clsServiceHandler();
        string strSql = "", strMsg = "";

        strMsg = objSrvHandler.ExecuteProcedure("APSNG101.WALLET_SUMMERY_WITH_DATERANGE('"+dtpFromDate.DateString+"', '"+dtpToDate.DateString+"')");
        if (strMsg != "")
        {

            strSql = " SELECT * FROM TEMP_WALLET_SUMMERY ORDER BY RANK_TITEL ASC";
            try
            {
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmWalletSummaryReport.aspx";
                Session["ReportFile"] = "../COMMON/WALLET_SUMMARY.rpt";
                SaveAuditInfo("View", "Rank Wise Wallet Summary Report");
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
            lblMessage.Text = "Procedure not execute successfully.";
        }
    }
}
