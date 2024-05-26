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

public partial class MIS_frmMISServiceWiseTransaction : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();   
    string strUserName = string.Empty;
    string strPassword = string.Empty;
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
        string strQuery = "", strProcedure = "", strDateRange = "", strMsg = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        // ----------------  Procedure calling and inserting data in temp table -----------------
        if (ddlServiceCode.SelectedValue == "CN")
        {
            // strProcedure = " PKG_MIS_REPORTS.MIS_TRANSACTIONS_REPORT_CN('" + ddlServiceCode.SelectedValue + "','" + dptFrom.DateString + "','" + dtpTo.DateString + "')";
        }
        else if (ddlServiceCode.SelectedValue == "FM")
        {
            //strProcedure = " PKG_MIS_REPORTS.MIS_TRANSACTIONS_REPORT_FM('" + ddlServiceCode.SelectedValue + "','" + dptFrom.DateString + "','" + dtpTo.DateString + "')";
        }
        else if (ddlServiceCode.SelectedValue == "FT")
        {
            //strProcedure = " PKG_MIS_REPORTS.MIS_TRANSACTIONS_REPORT_FT('" + ddlServiceCode.SelectedValue + "','" + dptFrom.DateString + "','" + dtpTo.DateString + "')";
        }

        //strMsg = objServiceHandler.ExecuteProcedure(strProcedure);
        //if (strMsg != "")
        //{            
        string strServiceName = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("SERVICE_LIST", "SERVICE_TITLE", "SERVICE_ACCESS_CODE", ddlServiceCode.SelectedValue.ToString());
        //------------------ main query start here ------------------------
        strDateRange = " AND TO_CHAR(TO_DATE(TRANSACTION_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('"
                     + dptFrom.DateString + "') AND TO_DATE('" + dtpTo.DateString + "')";
        strQuery = " SELECT TM.*,'" + strServiceName + "' REPORT_NAME,'" + dptFrom.DateString
                 + "' F_DATE,'" + dtpTo.DateString + "' T_DATE FROM TEMP_MIS_TRANSACTIONS_REPORT TM WHERE SERVICE_CODE='"
                 + ddlServiceCode.SelectedValue.ToString() + "' " + strDateRange;
        try
        {
            Session["ReportSQL"] = strQuery;
            SaveAuditInfo("View", "MIS Service Wise Transaction");
            Session["RequestForm"] = "../MIS/frmMISServiceWiseTransaction.aspx";
            Session["ReportFile"] = "../MIS/MIS_SERVICEWISE_REPORT.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        // }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
